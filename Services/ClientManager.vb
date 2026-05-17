Imports GymPaymentControl.Data
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class ClientManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ' Declaramos el Manager de Pagos para poder usar el método maestro
        Private ReadOnly _paymentManager As New PaymentManager()

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
                        ' 1. INSERTAR EL CLIENTE (Obtenemos su ID recién generado)
                        data.IdNewClient = InsertClient(connection, transaction, data)

                        ' 2. OBTENER LA TARIFA (Según edad o número de integrantes)
                        Dim tariff = GetRate(connection, transaction, data)

                        ' 3. PREPARAR EL DTO DE PAGO CORRECTO SEGÚN EL TIPO DE CLIENTE
                        ' Declaramos la interfaz común para que sirva para ambos casos
                        Dim paymentDto As IPaymentCalculable

                        'Dim precioFinal As Decimal = If(data.IsGroup, tariff.Price * data.GroupMembers, tariff.Price)

                        If data.IsGroup Then
                            ' Si es grupal, usamos el DTO de grupos familiares
                            paymentDto = New GroupPaymentDTO With
                                {
                                .FdiPgs = DateTime.Today,
                                .MtdPgs = data.PaymentMethod,
                                .PrcPgs = tariff.Price * data.GroupMembers,
                                .DscPgs = tariff.Discount,
                                .IdGrp = data.IdGroup.Value
                                }
                        Else
                            ' Si es individual, usamos el DTO individual vinculando el ID del nuevo cliente
                            paymentDto = New IndividualPaymentDTO With
                                {
                                .FdiPgs = DateTime.Today,
                                .MtdPgs = data.PaymentMethod,
                                .PrcPgs = tariff.Price,
                                .DscPgs = tariff.Discount,
                                .IdCli = data.IdNewClient
                                }
                        End If

                        ' 4. LLAMADA AL MÉTODO MAESTRO UNIFICADO EN PAYMENTMANAGER
                        ' Ahora le pasamos 'paymentDto' en lugar de 'data'
                        Dim success = _paymentManager.SavePaymentTransaction(paymentDto, TransactionMode.NewPayment,
                                                                             UserSession.IdUser, Nothing,
                                                                             connection, transaction)

                        If Not success Then Throw New Exception("Error al generar el pago del cliente.")

                        ' 5. UPDATE GRUPO (Solo si aplica)
                        If data.IsGroup Then UpdateGroup(connection, transaction, data)

                        ' SI TODO SALIÓ BIEN, GUARDAMOS CAMBIOS
                        transaction.Commit()

                    Catch ex As Exception
                        ' SI ALGO FALLA (Cliente o Pago), NO SE GUARDA NADA
                        transaction.Rollback()
                        Throw ex

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
                Case PaymentMethods.Monthly
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE emin_trfa <= @val AND emax_trfa >= @val LIMIT 1"

                Case PaymentMethods.Grupal
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE nperson_trfa = @val LIMIT 1"

                Case Else 'DIARIO
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE tipo_trfa = @tipo LIMIT 1"

            End Select

            ' 2. Ejecutar consulta principal
            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                If data.PaymentMethod = PaymentMethods.Monthly Then
                    command.Parameters.AddWithValue("@val", data.Age)

                ElseIf data.PaymentMethod = PaymentMethods.Grupal Then
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

        ' Función auxiliar para no repetir código de lectura
        Private Sub FillResultFromReader(command As MySqlCommand, ByRef result As RateResult)

            Using reader = command.ExecuteReader()

                If reader.Read() Then
                    result.Exists = True
                    result.Price = If(reader.IsDBNull(0), 0D, reader.GetDecimal(0))
                    result.Discount = If(reader.IsDBNull(1), 0D, reader.GetDecimal(1))
                End If

            End Using ' El reader se cierra aquí automáticamente

        End Sub

        ''' <summary>
        ''' Consulta la tabla de tarifas basándose en el perfil del cliente (Edad, Método, Integrantes)
        ''' </summary>
        Public Function GetApplicableRate(data As ClientPaymentDTO) As RateResult

            Try
                Using connection = GetConnection()

                    connection.Open()

                    ' Si es un grupo y no sabemos cuántos son, llamamos a nuestra nueva función
                    If data.PaymentMethod = PaymentMethods.Grupal AndAlso data.GroupMembers <= 0 Then

                        If data.IdGroup.HasValue AndAlso data.IdGroup.Value > 0 Then
                            data.GroupMembers = GetNumberMembers(data.IdGroup.Value)
                        End If

                    End If

                    ' Como es una consulta SELECT simple, no necesitamos Transaction
                    ' Pasamos Nothing en el parámetro de la transacción
                    Return GetRate(connection, Nothing, data)

                End Using

            Catch ex As Exception
                Throw New Exception("Error al obtener la tarifa desde la base de datos", ex)
            End Try

        End Function


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


        Public Function GetNumberMembers(idGroup As Integer) As Integer

            Dim count As Integer = 0
            Dim sql As String = "SELECT num_intgrntes_grp FROM grp_familiar WHERE id_grp = @idGrp"

            Try
                Using connection = GetConnection()

                    Using command As New MySqlCommand(sql, connection)

                        command.Parameters.AddWithValue("@idGrp", idGroup)
                        connection.Open()

                        Dim scalarResult As Object = command.ExecuteScalar()

                        If scalarResult IsNot Nothing AndAlso Not IsDBNull(scalarResult) Then
                            count = Convert.ToInt32(scalarResult)
                        End If

                    End Using
                End Using

            Catch ex As Exception
                ' Puedes manejar el error o loguearlo según tu sistema
                Throw New Exception("Error al obtener el número de integrantes del grupo", ex)
            End Try

            Return count

        End Function


        Public Function GetGroupMembersNames(idGroup As Integer) As String

            Dim nombres As String = ""
            Dim sql As String = "SELECT GROUP_CONCAT(nom_cli SEPARATOR ', ') FROM clientes WHERE id_grp = @idGrp"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sql, connection)

                    command.Parameters.AddWithValue("@idGrp", idGroup)
                    connection.Open()

                    Dim result = command.ExecuteScalar()

                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        nombres = result.ToString()
                    End If

                End Using
            End Using

            Return nombres

        End Function


    End Class
End Namespace