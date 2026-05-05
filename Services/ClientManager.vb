Imports System.Transactions
Imports GymPaymentControl.Data
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class ClientManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        '|----------------------------------------------------------------------------------------------------
        '| CONSULTA PARA OBTENER EL NOMBRE DE LAS TARIFAS DIARIAS - CLASES SUELTAS
        '|------------------------------------------------------------------------
        Public Function GetDailyPrice() As DataTable

            Dim sqlQuerie As String = "SELECT id_trfa, tipo_trfa FROM trfa_dscto WHERE tipo_trfa LIKE '%DIARIO%'"

            'Return SqlRepository.ExecuteDataTable(sqlQuerie, Nothing)
            Return ExecuteDataTable(sqlQuerie, Nothing)

        End Function

        '|----------------------------------------------------------------------------------------------------
        '| CONSULTA PARA OBTENER EN UNA LISTA EL NOMBRE DE LOS GRUPOS FAMILIARES
        '|----------------------------------------------------------------------
        Public Function GetNameGroupFamily() As DataTable

            Dim sqlQuerie As String = "SELECT * FROM grp_familiar ORDER BY id_grp DESC"

            'Return SqlRepository.ExecuteDataTable(sqlQuerie, Nothing)
            Return ExecuteDataTable(sqlQuerie, Nothing)

        End Function

        '|----------------------------------------------------------------------------------------------------
        '| CONSULTA PARA MOSTRAR TODOS LOS GRUPOS FAMILIARES DE LA BBDD
        '|-------------------------------------------------------------
        Public Function SearchFamilyGroup(nameSearch As String) As DataTable

            ' Usamos el comodín % para el LIKE dentro del valor del parámetro
            Dim sqlQuery As String = "SELECT * FROM grp_familiar WHERE nom_grp LIKE @name ORDER BY nom_grp"

            Dim parameter As New List(Of MySqlParameter) From
            {
            New MySqlParameter("@name", nameSearch & "%")
            }

            'Return SqlRepository.ExecuteDataTable(sqlQuery, parameter)
            Return ExecuteDataTable(sqlQuery, parameter)

        End Function

        '|-----------------------------------------------------------------------
        '| REGISTRO COMPLETO DE CLIENTES, PAGOS Y ACTUALIZACIÍN DE GRUPO FAMILIAR
        '|-----------------------------------------------------------------------
        Public Sub RegisterClientPayment(data As ClientPaymentDTO)

            Using connection = GetConnection()

                connection.Open()

                Using transaction = connection.BeginTransaction()

                    Try
                        '| * INSERT CLIENTE
                        data.IdNewClient = InsertClient(connection, transaction, data)

                        '| * OBTENER TARIFA
                        Dim tariff = GetRate(connection, transaction, data)

                        '| * INSERT PAGO
                        InsertPayment(connection, transaction, data, tariff)

                        '| * UPDATE GRUPO (solo si aplica)
                        If data.IsGroup Then UpdateGroup(connection, transaction, data)

                        transaction.Commit()

                    Catch

                        transaction.Rollback()
                        Throw

                    End Try

                End Using

            End Using

        End Sub

        '|-------------------------------------------------
        '| FUNCIÓN PARA INSERTAR UN CLIENTE Y OBTENER SU ID
        '|-------------------------------------------------
        Private Function InsertClient(connection As MySqlConnection,
                                      transaction As MySqlTransaction,
                                      data As ClientPaymentDTO) As Integer

            ' 1. Una sola consulta SQL para ambos casos (Individual o Grupal)
            Dim sqlQuery As String = "INSERT INTO clientes(nom_cli, ape_cli, fdn_cli, tlf_cli, eml_cli, dir_cli, mpg_cli, fdi_cli, std_cli, id_grp)
                                    VALUES(@nom, @ape, @fdn, @tlf, @eml, @dir, @mpg, @fdi, @std, @idgrp);
                                    SELECT LAST_INSERT_ID();"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                ' 2. Añadimos parámetros especificando el tipo de dato
                command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = data.FirstName
                command.Parameters.Add("@ape", MySqlDbType.VarChar).Value = data.LastName
                command.Parameters.Add("@fdn", MySqlDbType.Date).Value = data.BirthDate
                command.Parameters.Add("@tlf", MySqlDbType.VarChar).Value = data.Phone
                command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = data.Email
                command.Parameters.Add("@dir", MySqlDbType.VarChar).Value = data.Address
                command.Parameters.Add("@mpg", MySqlDbType.VarChar).Value = data.PaymentMethod
                command.Parameters.Add("@fdi", MySqlDbType.Date).Value = data.RegistrationDate
                command.Parameters.Add("@std", MySqlDbType.VarChar).Value = data.State

                ' 3. Manejo del Grupo: Si es grupo, pasamos el ID; si no, pasamos DBNull
                If data.IsGroup AndAlso data.IdGroup.HasValue Then
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = data.IdGroup.Value
                Else
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = DBNull.Value
                End If

                ' 4. Ejecutamos y retornamos el ID recién creado
                Return Convert.ToInt32(command.ExecuteScalar())

            End Using
        End Function

        '|--------------------------------------------
        '| FUNCIÓN PARA ACTUALIZAR DATOS DE UN CLIENTE
        '|--------------------------------------------
        Public Function UpdateClientProcess(data As ClientPaymentDTO,
                                            isNewEnrollment As Boolean,
                                            expandGroup As Boolean) As Boolean
            Using connection = GetConnection()
                connection.Open()
                Using transaction = connection.BeginTransaction()

                    Try
                        UpdateClient(connection, transaction, data)

                        If isNewEnrollment Then
                            data.ShouldExpandGroup = expandGroup
                            UpdateGroup(connection, transaction, data)
                        End If

                        transaction.Commit()
                        Return True

                    Catch ex As Exception
                        transaction.Rollback()
                        Return False
                    End Try

                End Using
            End Using
        End Function

        '|--------------------------------------------
        '| FUNCIÓN PARA ACTUALIZAR DATOS DE UN CLIENTE
        '|--------------------------------------------
        Private Sub UpdateClient(connection As MySqlConnection,
                                 transaction As MySqlTransaction,
                                 client As ClientPaymentDTO)

            Dim sqlQuery As String = "UPDATE clientes SET
                                        nom_cli = @nom, ape_cli = @ape, fdn_cli = @fdn, tlf_cli = @tlf, eml_cli = @eml,
                                        dir_cli = @dir, mpg_cli = @mpg, fdi_cli = @fdi, std_cli = @std, id_grp = @idgrp
                                        WHERE id_cli = @id"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)
                command.Parameters.AddWithValue("@nom", client.FirstName)
                command.Parameters.AddWithValue("@ape", client.LastName)
                command.Parameters.AddWithValue("@fdn", client.BirthDate)
                command.Parameters.AddWithValue("@tlf", client.Phone)
                command.Parameters.AddWithValue("@eml", client.Email)
                command.Parameters.AddWithValue("@dir", client.Address)
                command.Parameters.AddWithValue("@mpg", client.PaymentMethod)
                command.Parameters.AddWithValue("@fdi", client.RegistrationDate)
                command.Parameters.AddWithValue("@std", client.State)
                command.Parameters.AddWithValue("@id", client.IdNewClient)

                ' Lógica de grupo consistente con tu InsertClient
                If client.PaymentMethod = "GRUPAL" AndAlso client.IdGroup.HasValue Then
                    command.Parameters.AddWithValue("@idgrp", client.IdGroup.Value)
                Else
                    command.Parameters.AddWithValue("@idgrp", DBNull.Value)
                End If

                command.ExecuteNonQuery()
            End Using

        End Sub

        '|-------------------------------------------------------------------
        '| CONSULTAR LA TARIFA CORRESPONDIENTE AL CLIENTE O AL GRUPO FAMILIAR
        '|-------------------------------------------------------------------
        Private Function GetRate(connection As MySqlConnection,
                         transaction As MySqlTransaction,
                         data As ClientPaymentDTO) As RateResult

            Dim result As New RateResult With {.Exists = False, .Price = 0, .Discount = 0}
            Dim sqlQuery As String ' = ""

            ' 1. Determinar la consulta principal
            Select Case data.PaymentMethod
                Case "MENSUAL"
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE emin_trfa <= @val AND emax_trfa >= @val LIMIT 1"

                Case "GRUPAL"
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE nperson_trfa = @val LIMIT 1"

                Case Else 'DIARIO
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE tipo_trfa = @tipo LIMIT 1"

            End Select

            ' 2. Ejecutar consulta principal
            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                If data.PaymentMethod = "MENSUAL" Then
                    command.Parameters.AddWithValue("@val", data.Age)

                ElseIf data.PaymentMethod = "GRUPAL" Then
                    command.Parameters.AddWithValue("@val", data.GroupMembers)

                Else
                    command.Parameters.AddWithValue("@tipo", data.PaymentMethod)

                End If

                FillResultFromReader(command, result)

            End Using

            ' 3. FALLBACK: Si no existía por edad/grupo, buscar por nombre de tipo_trfa
            If Not result.Exists Then

                Using cmdFallback As New MySqlCommand("SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE tipo_trfa = @tipo LIMIT 1", connection, transaction)

                    cmdFallback.Parameters.AddWithValue("@tipo", data.PaymentMethod)
                    FillResultFromReader(cmdFallback, result)

                End Using

            End If

            Return result
        End Function

        ' Sub-función auxiliar para no repetir código de lectura
        Private Sub FillResultFromReader(command As MySqlCommand, ByRef result As RateResult)

            Using reader = command.ExecuteReader()

                If reader.Read() Then
                    result.Exists = True
                    result.Price = If(reader.IsDBNull(0), 0D, reader.GetDecimal(0))
                    result.Discount = If(reader.IsDBNull(1), 0D, reader.GetDecimal(1))
                End If

            End Using ' El reader se cierra aquí automáticamente

        End Sub

        '|----------------------------------------------
        '| INSERTAR PAGO DEL CLIENTE O AL GRUPO FAMILIAR - GEMINI
        '|----------------------------------------------
        Private Sub InsertPayment(connection As MySqlConnection,
                              transaction As MySqlTransaction,
                              data As ClientPaymentDTO,
                              tarifa As RateResult)

            ' 1. Si es GRUPAL, verificamos si ya existe una deuda para este grupo en el mes actual
            If data.IsGroup Then

                Dim sqlCheck As String = "SELECT COUNT(*) FROM pagos WHERE id_grp = @idGrp
                                        AND MONTH(fdi_pgs) = MONTH(CURDATE()) AND YEAR(fdi_pgs) = YEAR(CURDATE())"

                Using cmdCheck As New MySqlCommand(sqlCheck, connection, transaction)

                    cmdCheck.Parameters.AddWithValue("@idGrp", data.IdGroup.Value)
                    Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                    ' Si ya existe el pago del grupo para este mes, salimos de la función sin insertar nada
                    If exists > 0 Then Exit Sub

                End Using

            End If

            ' 2. Si llegamos aquí, es porque es un cliente individual O es el primer integrante del grupo
            Dim sqlQuery As String = "INSERT INTO pagos(fdi_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
                                    VALUES(@fdi, @mtd, @prc, @dsc, @idcli, @idgrp, @iduser)"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                ' Cálculo del precio (Si es grupo, multiplicamos)
                Dim finalPrice As Decimal = tarifa.Price
                If data.IsGroup Then
                    finalPrice = tarifa.Price * data.GroupMembers
                End If

                command.Parameters.Add("@fdi", MySqlDbType.Date).Value = DateTime.Today
                command.Parameters.Add("@mtd", MySqlDbType.VarChar).Value = data.PaymentMethod
                command.Parameters.Add("@prc", MySqlDbType.Decimal).Value = finalPrice
                command.Parameters.Add("@dsc", MySqlDbType.Decimal).Value = tarifa.Discount

                ' Asignación de IDs
                If data.IsGroup Then
                    command.Parameters.Add("@idcli", MySqlDbType.Int16).Value = DBNull.Value
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = data.IdGroup.Value
                Else
                    command.Parameters.Add("@idcli", MySqlDbType.Int16).Value = data.IdNewClient
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = DBNull.Value
                End If

                command.Parameters.Add("@iduser", MySqlDbType.Int16).Value = UserSession.IdUser

                command.ExecuteNonQuery()
            End Using
        End Sub

        '|------------------------------------------------------------------------------------
        '| INSERTAR PAGO DEL CLIENTE O AL GRUPO FAMILIAR
        '|----------------------------------------------
        Private Sub UpdateGroup(connection As MySqlConnection,
                                transaction As MySqlTransaction,
                                data As ClientPaymentDTO)

            Dim sqlQuery As String

            ' SQL Atómico: Sumamos directamente en la DB
            If data.ShouldExpandGroup Then
                sqlQuery = "UPDATE grp_familiar
                            SET intgrntes_reg_grp = intgrntes_reg_grp + 1, num_intgrntes_grp = num_intgrntes_grp + 1
                            WHERE id_grp = @idgrp"
            Else
                sqlQuery = "UPDATE grp_familiar
                            SET intgrntes_reg_grp = intgrntes_reg_grp + 1
                            WHERE id_grp = @idgrp"
            End If

            Using command As New MySqlCommand(sqlQuery, connection, transaction)
                command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = data.IdGroup.Value
                command.ExecuteNonQuery()
            End Using

        End Sub

        '|-----------------------------------------------------------------------
        '| VERIFICAR SI EXISTEN CLIENTES (Para habilitar/deshabilitar botón)
        '|-----------------------------------------------------------------------
        Public Function HasClients() As Boolean

            Try
                Using connection = GetConnection()

                    connection.Open()

                    ' Un simple COUNT(1) es lo más rápido que existe en SQL
                    Dim sqlQuery As String = "SELECT COUNT(1) FROM clientes"

                    Using command As New MySqlCommand(sqlQuery, connection)
                        Return Convert.ToInt32(command.ExecuteScalar()) > 0
                    End Using

                End Using

            Catch ex As Exception
                ' Manejo de errores profesional
                Throw New Exception("Error al verificar tabla clientes: " & ex.Message)

            End Try

        End Function

        Public Function GetClientsForSearch() As List(Of IndividualPaymentDTO)

            Dim customerList As New List(Of IndividualPaymentDTO)

            ' La consulta debe traer TODOS los campos que antes tenías en columnas ocultas
            Dim sqlQuery As String = "SELECT * FROM clientes"

            Try
                Using connection = GetConnection()

                    Dim command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()
                            ' Creamos el objeto y mapeamos cada columna de la BD a la propiedad del DTO
                            Dim customerData As New IndividualPaymentDTO With
                                {
                                    .IdCli = dataReader.GetInt32("id_cli"),
                                    .FirstName = dataReader.GetString("nom_cli"),
                                    .LastName = dataReader.GetString("ape_cli"),
                                    .BirthDate = dataReader.GetDateTime("fdn_cli"),
                                    .Age = CalculateClientAge(dataReader.GetDateTime("fdn_cli")),
                                    .Phone = dataReader.GetString("tlf_cli"),
                                    .Email = dataReader.GetString("eml_cli"),
                                    .Address = dataReader.GetString("dir_cli"),
                                    .PaymentMethod = dataReader.GetString("mpg_cli"),
                                    .RegistrationDate = dataReader.GetDateTime("fdi_cli"),
                                    .State = dataReader.GetString("std_cli"),
                                    .IdGroup = If(dataReader.IsDBNull(dataReader.GetOrdinal("id_grp")), Nothing, dataReader.GetInt32("id_grp"))
                                }
                            customerList.Add(customerData)

                        End While

                    End Using
                End Using

            Catch ex As Exception
                MsgBox("Error crítico de mapeo en ClientManager: " & ex.Message)

            End Try

            Return customerList

        End Function

        Public Function GetGroupName(idGroup As Integer) As String

            Dim sqlQuery As String = "SELECT nom_grp FROM grp_familiar WHERE id_grp = @idGrp"

            Try

                Using connection = GetConnection()

                    Using command As New MySqlCommand(sqlQuery, connection)

                        command.Parameters.AddWithValue("@idGrp", idGroup)
                        connection.Open()

                        Dim result = command.ExecuteScalar()

                        Return If(result IsNot Nothing, result.ToString(), "SIN GRUPO")

                    End Using
                End Using

            Catch ex As Exception
                Return "ERROR AL CARGAR NOMBRE DEL GRUPO"

            End Try

        End Function
        ''
        ''
        ''
    End Class
End Namespace