Imports GymPaymentControl.Constants
Imports GymPaymentControl.Data
Imports GymPaymentControl.Enums
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient
Imports Mysqlx

Namespace Services

    ''' <summary>
    ''' Gestiona las operaciones de acceso a datos y lógica de negocio relacionadas con los clientes, 
    ''' sus suscripciones, asignación a grupos familiares y validaciones de tarifas asociadas.
    ''' </summary>
    Public Class ClientManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ''' <summary>
        ''' Gestor auxiliar para procesar y registrar las transacciones de pago asociadas a los clientes.
        ''' </summary>
        Private ReadOnly _paymentManager As New PaymentManager()


        ''' <summary>
        ''' Obtiene el listado de tarifas configuradas para la modalidad de acceso diario o clases sueltas.
        ''' </summary>
        ''' <returns>Un objeto <see cref="DataTable"/> con los identificadores y descripciones de las tarifas diarias.</returns>
        Public Function GetDailyPrice() As DataTable

            Dim sqlQuerie As String = "SELECT id_trfa, tipo_trfa FROM trfa_dscto WHERE tipo_trfa LIKE '%DIARIO%'"

            Return ExecuteDataTable(sqlQuerie, Nothing)

        End Function


        ''' <summary>
        ''' Obtiene la lista completa de grupos familiares registrados en el sistema, ordenados en forma descendente por su ID.
        ''' </summary>
        ''' <returns>Un objeto <see cref="DataTable"/> con la información de todos los grupos familiares.</returns>
        Public Function GetNameGroupFamily() As DataTable

            Dim sqlQuerie As String = "SELECT * FROM grp_familiar ORDER BY id_grp DESC"

            'Return SqlRepository.ExecuteDataTable(sqlQuerie, Nothing)
            Return ExecuteDataTable(sqlQuerie, Nothing)

        End Function


        ''' <summary>
        ''' Busca grupos familiares cuyo nombre comience con el texto especificado.
        ''' </summary>
        ''' <param name="nameSearch">El patrón de texto para filtrar los grupos familiares por nombre.</param>
        ''' <returns>Un objeto <see cref="DataTable"/> con los grupos coincidentes ordenados alfabéticamente.</returns>
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


        ''' <summary>
        ''' Realiza de forma atómica (transaccional) el alta de un cliente, el cálculo de su primera cuota/tarifa,
        ''' la generación de su registro de pago y la actualización del contador del grupo familiar correspondiente.
        ''' </summary>
        ''' <param name="data">Objeto DTO con la información técnica, personal y de pago del cliente a registrar.</param>
        ''' <exception cref="Exception">Lanza una excepción si falla el registro del cliente o la generación de la transacción de pago.</exception>
        Public Sub RegisterClientPayment(data As ClientPaymentDTO)

            Using connection = GetConnection()

                connection.Open()

                Using transaction = connection.BeginTransaction()

                    Try
                        data.IdNewClient = InsertClient(connection, transaction, data)

                        Dim tariff = GetRate(connection, transaction, data)

                        '| * PREPARAR EL DTO DE PAGO CORRECTO SEGÚN EL TIPO DE CLIENTE.
                        '|   Declaramos la interfaz común para que sirva para ambos casos.
                        Dim paymentDto As IPaymentCalculable

                        '| * Comprobamos si se trata de un grupo y si es una ampliación.
                        Dim paymentStartDate As DateTime = DateTime.Today

                        If data.IsGroup AndAlso data.ShouldExpandGroup Then

                            paymentStartDate = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1)

                        End If

                        '| * Comprobar si son pagos grupales o individuales.
                        If data.IsGroup Then ' DTO de grupos familiares

                            paymentDto = New GroupPaymentDTO With
                                {
                                .FdiPgs = paymentStartDate,'DateTime.Today,
                                .MtdPgs = data.PaymentMethod,
                                .PrcPgs = tariff.Price * data.GroupMembers,
                                .DscPgs = tariff.Discount,
                                .IdGrp = data.IdGroup.Value
                                }
                        Else
                            ' DTO individual vinculando el ID del nuevo cliente
                            paymentDto = New IndividualPaymentDTO With
                                {
                                .FdiPgs = DateTime.Today,
                                .MtdPgs = data.PaymentMethod,
                                .PrcPgs = tariff.Price,
                                .DscPgs = tariff.Discount,
                                .IdCli = data.IdNewClient
                                }
                        End If

                        '| * MÉTODO MAESTRO UNIFICADO EN PAYMENTMANAGER
                        Dim success = _paymentManager.SavePaymentTransaction(paymentDto, TransactionMode.NewPayment,
                                                                             UserSession.IdUser, Nothing,
                                                                             connection, transaction)

                        If Not success Then Throw New Exception("Error al generar el pago del cliente.")

                        '| * UPDATE GRUPO (Solo si aplica)
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


        ''' <summary>
        ''' Inserta los datos personales de un nuevo cliente en la base de datos y retorna el ID autogenerado.
        ''' </summary>
        ''' <param name="connection">Conexión activa a MySQL.</param>
        ''' <param name="transaction">Transacción SQL en curso para garantizar atomicidad.</param>
        ''' <param name="data">DTO con la información del cliente a insertar.</param>
        ''' <returns>El identificador único (<c>id_cli</c>) generado para el nuevo cliente.</returns>
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
                command.Parameters.Add("@std", MySqlDbType.Byte).Value = CByte(data.State)

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


        ''' <summary>
        ''' Procesa la actualización de los datos de un cliente existente y, si aplica, expande el cupo de su grupo familiar.
        ''' </summary>
        ''' <param name="data">DTO con la información actualizada del cliente.</param>
        ''' <param name="isNewEnrollment">Indica si se trata de un nuevo ingreso al grupo que requiere actualizar contadores.</param>
        ''' <param name="expandGroup">Indica si la adición del cliente expande la capacidad nominal total del grupo.</param>
        ''' <returns><c>True</c> si la actualización finalizó con éxito; de lo contrario, <c>False</c>.</returns>
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


        ''' <summary>
        ''' Actualiza los campos de un registro de cliente específico dentro de una transacción.
        ''' </summary>
        ''' <param name="connection">Conexión activa a MySQL.</param>
        ''' <param name="transaction">Transacción SQL en curso.</param>
        ''' <param name="client">DTO con los datos actualizados del cliente.</param>
        Private Sub UpdateClient(connection As MySqlConnection,
                                 transaction As MySqlTransaction,
                                 client As ClientPaymentDTO)

            Dim sqlQuery As String = "UPDATE clientes SET
                                        nom_cli = @nom, ape_cli = @ape, fdn_cli = @fdn, tlf_cli = @tlf, eml_cli = @eml,
                                        dir_cli = @dir, mpg_cli = @mpg, fdi_cli = @fdi, std_cli = @std, id_grp = @idgrp
                                        WHERE id_cli = @id"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                '| * USO RECOMENDADO: Especificar explícitamente el tipo de dato
                '|   evita conversiones implícitas en el driver.
                '|   command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = client.FirstName

                '| * EVITAR: AddWithValue infiere el tipo automáticamente en tiempo de ejecución. 
                '|   Puede causar un mal uso de índices en la base de datos si confunde tipos
                '|   de texto/fecha o conversiones no deseadas.
                '|   command.Parameters.AddWithValue("@nom", client.FirstName)

                command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = client.FirstName
                command.Parameters.Add("@ape", MySqlDbType.VarChar).Value = client.LastName
                command.Parameters.Add("@fdn", MySqlDbType.Date).Value = client.BirthDate
                command.Parameters.Add("@tlf", MySqlDbType.VarChar).Value = client.Phone
                command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = client.Email
                command.Parameters.Add("@dir", MySqlDbType.VarChar).Value = client.Address
                command.Parameters.Add("@mpg", MySqlDbType.VarChar).Value = client.PaymentMethod
                command.Parameters.Add("@fdi", MySqlDbType.Date).Value = client.RegistrationDate
                command.Parameters.Add("@std", MySqlDbType.Byte).Value = CByte(client.State)
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = client.IdNewClient

                ' Lógica de grupo consistente con tu InsertClient
                If client.PaymentMethod = "GRUPAL" AndAlso client.IdGroup.HasValue Then
                    command.Parameters.AddWithValue("@idgrp", client.IdGroup.Value)
                Else
                    command.Parameters.AddWithValue("@idgrp", DBNull.Value)
                End If

                command.ExecuteNonQuery()
            End Using

        End Sub


        ''' <summary>
        ''' Elimina permanentemente un cliente y todo su historial de pagos asociado mediante una operación atómica.
        ''' </summary>
        ''' <param name="idCli">El identificador único del cliente a eliminar.</param>
        ''' <returns><c>True</c> si el cliente existía y fue eliminado junto a sus pagos; de lo contrario, <c>False</c>.</returns>
        Public Function DeleteClientPermanently(idCli As Integer) As Boolean

            Dim sqlDeletePayments As String = "DELETE FROM pagos WHERE id_cli = @id"
            Dim sqlDeleteClient As String = "DELETE FROM clientes WHERE id_cli = @id"

            Try
                Using connection = GetConnection()

                    connection.Open()

                    Using transaction As MySqlTransaction = connection.BeginTransaction()

                        Try
                            ' 1. Eliminamos primero los pagos para evitar errores de clave foránea
                            Using cmdPayments As New MySqlCommand(sqlDeletePayments, connection, transaction)
                                cmdPayments.Parameters.AddWithValue("@id", idCli)
                                cmdPayments.ExecuteNonQuery()
                            End Using

                            ' 2. Eliminamos la ficha del cliente
                            Dim filasAfectadas As Integer = 0
                            Using cmdClient As New MySqlCommand(sqlDeleteClient, connection, transaction)
                                cmdClient.Parameters.AddWithValue("@id", idCli)
                                filasAfectadas = cmdClient.ExecuteNonQuery()
                            End Using

                            ' Si el cliente existía y se eliminó, consolidamos la operación
                            If filasAfectadas > 0 Then
                                transaction.Commit()
                                Return True
                            Else
                                transaction.Rollback()
                                Return False
                            End If

                        Catch ex As Exception
                            transaction.Rollback()
                            Throw ex
                        End Try

                    End Using

                End Using

            Catch ex As Exception
                MessageBox.Show("Error al eliminar el cliente permanentemente :" & vbCrLf & ex.Message)
                Return False
            End Try

        End Function


        ''' <summary>
        ''' Consulta la tarifa y el descuento aplicables en función de la modalidad de pago, edad o número de integrantes del grupo.
        ''' </summary>
        ''' <param name="connection">Conexión activa a la base de datos.</param>
        ''' <param name="transaction">Transacción activa o <c>Nothing</c> si se consulta fuera de transacción.</param>
        ''' <param name="data">DTO del cliente con la información requerida para el cálculo tarifario.</param>
        ''' <returns>Un objeto <see cref="RateResult"/> con el precio, el descuento y un indicador de existencia de la tarifa.</returns>
        Private Function GetRate(connection As MySqlConnection,
                                 transaction As MySqlTransaction,
                                 data As ClientPaymentDTO) As RateResult

            Dim result As New RateResult With
                {
                    .Exists = False,
                    .Price = 0,
                    .Discount = 0
                }
            Dim sqlQuery As String

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


        ''' <summary>
        ''' Lee los valores de precio y descuento desde la ejecución de un lector de datos y actualiza el objeto de resultado.
        ''' </summary>
        ''' <param name="command">Comando SQL configurado para ejecutar.</param>
        ''' <param name="result">Referencia al objeto <see cref="RateResult"/> que será completado.</param>
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
        ''' Obtiene la tarifa aplicable para un cliente o grupo abriendo una conexión dedicada de lectura.
        ''' </summary>
        ''' <param name="data">DTO del cliente con los criterios de evaluación.</param>
        ''' <returns>Un objeto <see cref="RateResult"/> con la información del precio y descuento aplicable.</returns>
        ''' <exception cref="Exception">Lanza una excepción si falla la consulta en la base de datos.</exception>
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


        ''' <summary>
        ''' Incrementa los contadores de miembros registrados y capacidad total en la tabla del grupo familiar.
        ''' </summary>
        ''' <param name="connection">Conexión activa a la base de datos.</param>
        ''' <param name="transaction">Transacción activa en curso.</param>
        ''' <param name="data">DTO del cliente con la información sobre la expansión del grupo.</param>
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


        ''' <summary>
        ''' Verifica si existen registros en la tabla de clientes.
        ''' </summary>
        ''' <returns><c>True</c> si existe al menos un cliente en la base de datos; de lo contrario, <c>False</c>.</returns>
        ''' <exception cref="Exception">Lanza una excepción si ocurre un error al consultar la base de datos.</exception>
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


        ''' <summary>
        ''' Obtiene la lista completa de clientes mapeados como objetos <see cref="IndividualPaymentDTO"/>.
        ''' </summary>
        ''' <returns>Una lista de DTOs con la información técnica y personal de todos los clientes.</returns>
        Public Function GetClientsForSearch() As List(Of IndividualPaymentDTO)

            Dim customerList As New List(Of IndividualPaymentDTO)

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
                                    .State = CType(Convert.ToByte(dataReader("std_cli")), EntityStatus),
                                    .IdGroup = If(dataReader.IsDBNull(dataReader.GetOrdinal("id_grp")), Nothing, dataReader.GetInt32("id_grp"))
                                }
                            customerList.Add(customerData)

                        End While

                    End Using
                End Using

            Catch ex As Exception
                Throw New Exception("ERROR AL MAPEAR EN ClientManager: ", ex)
            End Try

            Return customerList

        End Function


        ''' <summary>
        ''' Obtiene el nombre comercial de un grupo familiar mediante su identificador único.
        ''' </summary>
        ''' <param name="idGroup">Identificador del grupo familiar.</param>
        ''' <returns>El nombre del grupo, "SIN GRUPO" si no existe, o un mensaje de error si falla la consulta.</returns>
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


        ''' <summary>
        ''' Consulta la cantidad total de integrantes nominales configurados para un grupo familiar.
        ''' </summary>
        ''' <param name="idGroup">Identificador del grupo familiar.</param>
        ''' <returns>El número de integrantes asignados al grupo.</returns>
        ''' <exception cref="Exception">Lanza una excepción si falla la lectura en base de datos.</exception>
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


        ''' <summary>
        ''' Obtiene una lista delimitada por comas con los nombres de todos los clientes pertenecientes a un grupo.
        ''' </summary>
        ''' <param name="idGroup">Identificador del grupo familiar.</param>
        ''' <returns>Una cadena con los nombres concatenados de los miembros del grupo.</returns>
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


        ''' <summary>
        ''' Evalúa si existe una tarifa configurada para una cantidad específica de integrantes.
        ''' </summary>
        ''' <param name="numberMembers">Número de integrantes del grupo familiar.</param>
        ''' <returns><c>True</c> si existe una tarifa registrada para dicha cantidad; de lo contrario, <c>False</c>.</returns>
        Public Function HasGroupRate(numberMembers As Integer) As Boolean

            Using connection As MySqlConnection = GetConnection()

                connection.Open()

                Dim paymentGenerator As New Services.PaymentGenerator()

                Dim tariffRow As DataRow = paymentGenerator.GetGroupRate(connection, Nothing, numberMembers)

                Return tariffRow IsNot Nothing

            End Using

        End Function


        '''' <summary>
        '''' Determina si un grupo familiar tiene cuotas de pago pendientes de cobro.
        '''' </summary>
        '''' <param name="groupId">Identificador del grupo familiar.</param>
        '''' <returns><c>True</c> si el grupo registra pagos pendientes (forma de pago vacía o nula); de lo contrario, <c>False</c>.</returns>
        'Public Function HasPendingGroupDebt(groupId As Integer) As Boolean

        '    Const sqlQuery As String = "SELECT EXISTS (SELECT 1 " &
        '                               "FROM pagos " &
        '                               "WHERE id_grp = @id_grp " &
        '                               "AND (frm_pgs IS NULL OR frm_pgs = ''))"

        '    Using connection = GetConnection()

        '        Using command As New MySqlCommand(sqlQuery, connection)

        '            command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId

        '            connection.Open()

        '            Return Convert.ToBoolean(command.ExecuteScalar())

        '        End Using

        '    End Using

        'End Function

    End Class

End Namespace