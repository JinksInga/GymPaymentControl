Imports GymPaymentControl.Constants
Imports GymPaymentControl.Data
Imports GymPaymentControl.Enums
Imports MySql.Data.MySqlClient

Namespace Services

    Public Class FamilyGroupManager

        Inherits BaseRepository


        ''' <summary>
        ''' Busca grupos que coincidan exactamente con el nombre proporcionado (para validar duplicados).
        ''' </summary>
        Public Function GetGroupsByNameMatch(groupName As String) As DataTable

            Dim sqlQuery As String = "SELECT * FROM grp_familiar " &
                                     "WHERE nom_grp LIKE @nom_grp " &
                                     "ORDER BY nom_grp"

            Dim parameters As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@nom_grp", $"%{groupName}%")
                }

            Return ExecuteDataTable(sqlQuery, parameters)

        End Function


        ''' <summary>
        ''' Obtiene los miembros (clientes) asociados a un grupo familiar específico mediante su ID.
        ''' </summary>
        Public Function GetMembersByGroupId(groupId As Integer) As DataTable

            Dim sqlQuery As String = "SELECT id_cli, nom_cli, ape_cli, id_grp " &
                                     "FROM clientes " &
                                     "WHERE id_grp = @id_grp"

            Dim parameterList As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@id_grp", MySqlDbType.Int32) With {.Value = groupId}
                }

            Return ExecuteDataTable(sqlQuery, parameterList)

        End Function


        Public Function SearchAvailableMembersByName(searchText As String) As DataTable

            Dim sqlQuery As String = "SELECT id_cli, CONCAT(nom_cli, ' ', ape_cli) AS full_name " &
                                     "FROM clientes " &
                                     "WHERE CONCAT(nom_cli, ' ', ape_cli) LIKE @SearchText AND id_grp IS NULL " &
                                     "ORDER BY nom_cli"

            Dim parameters As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@SearchText", $"{searchText}%")
                }

            Return ExecuteDataTable(sqlQuery, parameters)

        End Function


        ''' <summary>
        ''' Registra un nuevo grupo familiar en el sistema y vincula a sus integrantes en una transacción única.
        ''' </summary>
        Public Function InsertFamilyGroup(groupName As String, numberMembers As Integer,
                                          registeredMembers As List(Of Integer),
                                          groupStatus As EntityStatus,
                                          ByRef newGroupId As Integer) As FamilyGroupSaveResult

            Using connection As MySqlConnection = GetConnection()

                connection.Open()

                Using transaction As MySqlTransaction = connection.BeginTransaction()

                    Try
                        ' ====================
                        ' | COMPROBAR TARIFA |
                        ' ====================
                        Dim paymentGen As New Services.PaymentGenerator()

                        Dim tariffRow As DataRow = paymentGen.GetGroupRate(connection, transaction, numberMembers)

                        If tariffRow Is Nothing Then

                            transaction.Rollback()
                            Return FamilyGroupSaveResult.GroupRateNotFound

                        End If

                        ' ================================
                        ' | INSERCIÓN DEL GRUPO FAMILIAR |
                        ' ================================
                        Dim sqlInsertGroup As String = "INSERT INTO grp_familiar " &
                                                       "(nom_grp, num_intgrntes_grp, intgrntes_reg_grp, std_grp) " &
                                                       "VALUES " &
                                                       "(@nom_grp, @num_intgrntes_grp, @intgrntes_reg_grp, @std_grp);"

                        Dim generatedGroupId As Integer

                        Using command As New MySqlCommand(sqlInsertGroup, connection, transaction)

                            command.Parameters.Add("@nom_grp", MySqlDbType.VarChar).Value = groupName
                            command.Parameters.Add("@num_intgrntes_grp", MySqlDbType.Int32).Value = numberMembers
                            command.Parameters.Add("@intgrntes_reg_grp", MySqlDbType.Int32).Value = registeredMembers.Count
                            command.Parameters.Add("@std_grp", MySqlDbType.Byte).Value = CByte(groupStatus)
                            command.ExecuteNonQuery()

                            generatedGroupId = Convert.ToInt32(command.LastInsertedId)
                            '|* command.CommandText = "SELECT LAST_INSERT_ID();"
                            '|* generatedGroupId = Convert.ToInt32(command.ExecuteScalar())
                            '|
                            '| ¿Qué es ExecuteScalar() (junto con SELECT LAST_INSERT_ID())?
                            '| ExecuteScalar() es un método genérico de ADO.NET diseñado para ejecutar una consulta SQL
                            '| y devolver la primera columna de la primera fila del resultado.
                            '|
                            '| Lo que estás haciendo es indicarle a la base de datos:
                            '| "Por favor, compila esta nueva consulta de selección, ejecútala en el servidor,
                            '| tráeme el resultado por la red, conviértelo a objeto y entrégamelo".
                            '|
                            '| ¿Cómo funciona en la práctica?
                            '| Realiza un ciclo completo de petición-respuesta (Round-trip) hacia el servidor de MySQL.
                            '|
                            '| Costo/Rendimiento:
                            '| Requiere tiempo de procesamiento de red y CPU adicional en la base de datos.
                        End Using

                        newGroupId = generatedGroupId

                        ' ==========================
                        ' VINCULACIÓN DE INTEGRANTES
                        ' ==========================
                        If registeredMembers.Count > 0 Then

                            Dim sqlUpdateClients As String = "UPDATE clientes " &
                                                             "SET mpg_cli = @mpg_cli, std_cli = @std_cli, id_grp = @id_grp " &
                                                             "WHERE id_cli = @id_cli;"

                            Using command As New MySqlCommand(sqlUpdateClients, connection, transaction)

                                command.Parameters.Add("@mpg_cli", MySqlDbType.VarChar).Value = PaymentMethods.Grupal
                                command.Parameters.Add("@std_cli", MySqlDbType.Byte).Value = CByte(groupStatus)
                                command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = generatedGroupId
                                Dim clientParam As MySqlParameter = command.Parameters.Add("@id_cli", MySqlDbType.Int32)

                                For Each id As Integer In registeredMembers

                                    clientParam.Value = id
                                    command.ExecuteNonQuery()

                                Next

                            End Using

                        End If

                        transaction.Commit()
                        Return FamilyGroupSaveResult.Success

                    Catch
                        transaction.Rollback()
                        Throw
                    End Try

                End Using
            End Using

        End Function


        ''' <summary>
        ''' Actualiza los datos de un grupo familiar existente y reestructura sus integrantes de manera transaccional.
        ''' </summary>
        Public Function UpdateFamilyGroup(groupId As Integer,
                                          groupName As String, numberMembers As Integer,
                                          registeredMembers As List(Of Integer),
                                          groupStatus As EntityStatus) As FamilyGroupSaveResult

            Using connection As MySqlConnection = GetConnection()

                connection.Open()

                Using transaction As MySqlTransaction = connection.BeginTransaction()

                    Try
                        ' ====================
                        ' | COMPROBAR TARIFA |
                        ' ====================
                        Dim paymentGenerator As New Services.PaymentGenerator()

                        Dim tariffRow As DataRow = paymentGenerator.GetGroupRate(connection, transaction, numberMembers)

                        If tariffRow Is Nothing Then

                            transaction.Rollback()
                            Return FamilyGroupSaveResult.GroupRateNotFound

                        End If

                        ' ================================
                        ' | ACTUALIZACIÓN DEL ENCABEZADO |
                        ' ================================
                        Dim sqlUpdateGroup As String = "UPDATE grp_familiar " &
                                                       "SET nom_grp = @nom_grp, num_intgrntes_grp = @num_intgrntes_grp, " &
                                                       "intgrntes_reg_grp = @intgrntes_reg_grp, std_grp = @std_grp " &
                                                       "WHERE id_grp = @id_grp;"

                        Using command As New MySqlCommand(sqlUpdateGroup, connection, transaction)

                            command.Parameters.Add("@nom_grp", MySqlDbType.VarChar).Value = groupName
                            command.Parameters.Add("@num_intgrntes_grp", MySqlDbType.Int32).Value = numberMembers
                            command.Parameters.Add("@intgrntes_reg_grp", MySqlDbType.Int32).Value = registeredMembers.Count
                            command.Parameters.Add("@std_grp", MySqlDbType.Byte).Value = CByte(groupStatus)
                            command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId
                            command.ExecuteNonQuery()

                        End Using

                        ' ================================
                        ' | LIBERAR INTEGRANTES ACTUALES |
                        ' ================================
                        Dim sqlReleaseClients As String = "UPDATE clientes " &
                                                          "SET mpg_cli = @mpg_cli, std_cli = @std_cli, id_grp = NULL " &
                                                          "WHERE id_grp = @id_grp;"

                        Using command As New MySqlCommand(sqlReleaseClients, connection, transaction)

                            command.Parameters.Add("@mpg_cli", MySqlDbType.VarChar).Value = PaymentMethods.Monthly
                            command.Parameters.Add("@std_cli", MySqlDbType.Byte).Value = CByte(EntityStatus.Active)
                            command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId
                            command.ExecuteNonQuery()

                        End Using

                        ' ===============================
                        ' | VINCULAR NUEVOS INTEGRANTES |
                        ' ===============================

                        If registeredMembers.Count > 0 Then

                            Dim sqlUpdateClients As String = "UPDATE clientes " &
                                                             "SET mpg_cli = @mpg_cli, std_cli = @std_cli , id_grp = @id_grp " &
                                                             "WHERE id_cli = @id_cli;"

                            Using command As New MySqlCommand(sqlUpdateClients, connection, transaction)

                                command.Parameters.Add("@mpg_cli", MySqlDbType.VarChar).Value = PaymentMethods.Grupal
                                command.Parameters.Add("@std_cli", MySqlDbType.Byte).Value = CByte(groupStatus)
                                command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId
                                Dim clientParam As MySqlParameter = command.Parameters.Add("@id_cli", MySqlDbType.Int32)

                                For Each id As Integer In registeredMembers

                                    clientParam.Value = id
                                    command.ExecuteNonQuery()

                                Next

                            End Using

                        End If

                        transaction.Commit()

                        Return FamilyGroupSaveResult.Success

                    Catch
                        transaction.Rollback()
                        Throw
                    End Try

                End Using
            End Using

        End Function


        ''' <summary>
        ''' Elimina un grupo familiar de la base de datos y libera a sus integrantes a modalidad MENSUAL.
        ''' </summary>
        Public Function DeleteFamilyGroup(groupId As Integer) As Boolean

            Using connection As MySqlConnection = GetConnection()

                connection.Open()

                Using transaction As MySqlTransaction = connection.BeginTransaction()

                    Try
                        ' =================================
                        ' | LIBERAR INTEGRANTES DEL GRUPO |
                        ' =================================
                        Dim sqlUpdateGroup As String = "UPDATE clientes " &
                                                       "SET mpg_cli = @mpg_cli, std_cli = @std_cli, id_grp = NULL " &
                                                       "WHERE id_grp = @id_grp;"

                        Using command As New MySqlCommand(sqlUpdateGroup, connection, transaction)

                            command.Parameters.Add("@mpg_cli", MySqlDbType.VarChar).Value = PaymentMethods.Monthly
                            command.Parameters.Add("@std_cli", MySqlDbType.Byte).Value = CByte(EntityStatus.Active)
                            command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId
                            command.ExecuteNonQuery()

                        End Using

                        ' ===========================
                        ' | ELIMINAR GRUPO FAMILIAR |
                        ' ===========================
                        Dim sqlDeleteGroup As String = "DELETE FROM grp_familiar " &
                                                       "WHERE id_grp = @id_grp;"

                        Using command As New MySqlCommand(sqlDeleteGroup, connection, transaction)

                            command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId
                            command.ExecuteNonQuery()

                        End Using

                        transaction.Commit()

                        Return True

                    Catch
                        transaction.Rollback()
                        Throw
                    End Try

                End Using
            End Using
        End Function


    End Class
End Namespace
