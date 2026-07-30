Imports GymPaymentControl.Data
Imports MySql.Data.MySqlClient

Namespace Services

    Public Class FamilyGroupManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository


        ''' <summary>
        ''' Busca grupos que coincidan exactamente con el nombre proporcionado (para validar duplicados).
        ''' </summary>
        Public Function GetGroupsByNameMatch(groupName As String) As DataTable

            Dim sqlQuery As String = "SELECT id_grp, nom_grp, num_intgrntes_grp FROM grp_familiar
                                        WHERE nom_grp LIKE @GroupName ORDER BY nom_grp"

            ' Preparamos el parámetro de forma segura para evitar inyección SQL y el fallo de la comilla (')
            Dim parameters As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@GroupName", $"%{groupName}%")
                }

            ' Delegamos la ejecución completa a la infraestructura del búnker
            Return ExecuteDataTable(sqlQuery, parameters)

        End Function


        ''' <summary>
        ''' Obtiene los miembros (clientes) asociados a un grupo familiar específico mediante su ID.
        ''' </summary>
        Public Function GetMembersByGroupId(groupId As Integer) As DataTable
            ' 1. La consulta SQL parametrizada
            Dim sqlQuery As String = "SELECT id_cli, nom_cli, ape_cli, id_grp FROM clientes WHERE id_grp = @GroupId"

            ' 2. Creamos la lista de parámetros usando la clase nativa MySqlParameter que requiere tu BaseRepository
            Dim parameterList As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@GroupId", MySqlDbType.Int32) With {.Value = groupId}
                }

            ' 3. Ejecutamos la consulta heredada de BaseRepository de forma directa y segura
            Return ExecuteDataTable(sqlQuery, parameterList)
        End Function


        Public Function SearchAvailableMembersByName(searchText As String) As DataTable

            ' Concatenamos Nombre y Apellido con un espacio desde MySQL y filtramos solo los que no tienen grupo asignado
            Dim sqlQuery As String = "SELECT id_cli, CONCAT(nom_cli, ' ', ape_cli) AS full_name, id_grp
                                      FROM clientes
                                      WHERE CONCAT(nom_cli, ' ', ape_cli) LIKE @SearchText
                                      AND id_grp IS NULL
                                      ORDER BY nom_cli"

            ' Buscamos que empiece por el texto ingresado (tal como tenías tu lógica con %)
            Dim parameters As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@SearchText", $"{searchText}%")
                }

            Return ExecuteDataTable(sqlQuery, parameters)

        End Function


        ''' <summary>
        ''' Registra un nuevo grupo familiar en el sistema y vincula a sus integrantes en una transacción única.
        ''' </summary>
        Public Function InsertFamilyGroup(groupName As String, totalMembers As Integer,
                                          memberIds As List(Of Integer)) As Boolean

            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using transaction As MySqlTransaction = connection.BeginTransaction()

                    Try
                        ' REUTILIZACIÓN DIRECTA: Usamos tu PaymentGenerator existente
                        Dim paymentGen As New Services.PaymentGenerator()
                        Dim tariffRow As DataRow = paymentGen.GetGroupRate(connection, transaction, totalMembers)

                        ' Si no devuelve fila, es que no existe esa cantidad de integrantes en trfa_dscto
                        If tariffRow Is Nothing Then
                            Throw New InvalidOperationException($"No existe ninguna tarifa registrada para {totalMembers} integrantes.")
                        End If

                        ' =========================================================================
                        ' INSERCIÓN DEL GRUPO
                        ' =========================================================================
                        Dim sqlGroup As String = "INSERT INTO grp_familiar (nom_grp, num_intgrntes_grp, intgrntes_reg_grp) " &
                                                 "VALUES (@GroupName, @TotalMembers, @RegisteredMembers);"
                        Dim generatedGroupId As Integer

                        Using cmd As New MySqlCommand(sqlGroup, connection, transaction)
                            cmd.Parameters.AddWithValue("@GroupName", groupName)
                            cmd.Parameters.AddWithValue("@TotalMembers", totalMembers)
                            cmd.Parameters.AddWithValue("@RegisteredMembers", memberIds.Count)
                            cmd.ExecuteNonQuery()

                            cmd.CommandText = "SELECT LAST_INSERT_ID();"
                            generatedGroupId = Convert.ToInt32(cmd.ExecuteScalar())
                        End Using

                        ' =========================================================================
                        ' VINCULACIÓN DE INTEGRANTES
                        ' =========================================================================
                        If memberIds.Count > 0 Then
                            Dim sqlUpdateClients As String = "UPDATE clientes SET mpg_cli = 'GRUPAL', id_grp = @GroupId WHERE id_cli = @ClientId;"
                            Using cmd As New MySqlCommand(sqlUpdateClients, connection, transaction)
                                cmd.Parameters.AddWithValue("@GroupId", generatedGroupId)
                                Dim clientParam As MySqlParameter = cmd.Parameters.Add(New MySqlParameter("@ClientId", MySqlDbType.Int32))

                                For Each id As Integer In memberIds
                                    clientParam.Value = id
                                    cmd.ExecuteNonQuery()
                                Next
                            End Using
                        End If

                        transaction.Commit()
                        Return True
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using
        End Function


        ''' <summary>
        ''' Actualiza los datos de un grupo familiar existente y reestructura sus integrantes de manera transaccional.
        ''' </summary>
        Public Function UpdateFamilyGroup(groupId As Integer, groupName As String, totalMembers As Integer, memberIds As List(Of Integer)) As Boolean

            Using connection As MySqlConnection = GetConnection()
                connection.Open()
                Using transaction As MySqlTransaction = connection.BeginTransaction()
                    Try
                        ' REUTILIZACIÓN DIRECTA: Misma validación usando tu PaymentGenerator
                        Dim paymentGen As New Services.PaymentGenerator()
                        Dim tariffRow As DataRow = paymentGen.GetGroupRate(connection, transaction, totalMembers)

                        If tariffRow Is Nothing Then
                            Throw New InvalidOperationException($"No existe ninguna tarifa registrada para {totalMembers} integrantes.")
                        End If

                        ' =========================================================================
                        ' ACTUALIZACIÓN DEL ENCABEZADO
                        ' =========================================================================
                        Dim sqlUpdateGroup As String = "UPDATE grp_familiar SET nom_grp = @GroupName, " &
                                                       "num_intgrntes_grp = @TotalMembers, intgrntes_reg_grp = @RegisteredMembers " &
                                                       "WHERE id_grp = @GroupId;"
                        Using cmd As New MySqlCommand(sqlUpdateGroup, connection, transaction)
                            cmd.Parameters.AddWithValue("@GroupName", groupName)
                            cmd.Parameters.AddWithValue("@TotalMembers", totalMembers)
                            cmd.Parameters.AddWithValue("@RegisteredMembers", memberIds.Count)
                            cmd.Parameters.AddWithValue("@GroupId", groupId)
                            cmd.ExecuteNonQuery()
                        End Using

                        ' =========================================================================
                        ' REESTRUCTURACIÓN DE INTEGRANTES
                        ' =========================================================================
                        Dim sqlRelease As String = "UPDATE clientes SET mpg_cli = 'MENSUAL', id_grp = NULL WHERE id_grp = @GroupId;"

                        Using cmd As New MySqlCommand(sqlRelease, connection, transaction)
                            cmd.Parameters.AddWithValue("@GroupId", groupId)
                            cmd.ExecuteNonQuery()
                        End Using

                        If memberIds.Count > 0 Then

                            Dim sqlUpdateClients As String = "UPDATE clientes SET mpg_cli = 'GRUPAL', id_grp = @GroupId WHERE id_cli = @ClientId;"

                            Using cmd As New MySqlCommand(sqlUpdateClients, connection, transaction)
                                cmd.Parameters.AddWithValue("@GroupId", groupId)
                                Dim clientParam As MySqlParameter = cmd.Parameters.Add(New MySqlParameter("@ClientId", MySqlDbType.Int32))

                                For Each id As Integer In memberIds
                                    clientParam.Value = id
                                    cmd.ExecuteNonQuery()
                                Next
                            End Using
                        End If

                        transaction.Commit()
                        Return True
                    Catch ex As Exception
                        transaction.Rollback()
                        Throw ex
                    End Try
                End Using
            End Using
        End Function


    End Class
End Namespace
