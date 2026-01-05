Imports System.Configuration
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class ClientService

        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

        '|----------------------------------------------------------------------------------------------------
        '| CONSULTA PARA OBTENER EL NOMBRE DE LAS TARIFAS DIARIAS - CLASES SUELTAS
        '|------------------------------------------------------------------------
        Public Function GetDailyPrice() As DataTable

            Dim sqlQuerie As String = "SELECT id_trfa, tipo_trfa FROM trfa_dscto WHERE tipo_trfa LIKE '%DIARIO%'"

            Return SqlRepository.ExecuteDataTable(sqlQuerie, Nothing)

        End Function

        '|----------------------------------------------------------------------------------------------------
        '| CONSULTA PARA OBTENER EN UNA LISTA EL NOMBRE DE LOS GRUPOS FAMILIARES
        '|----------------------------------------------------------------------
        Public Function GetNameGroupFamily() As DataTable

            Dim sqlQuerie As String = "SELECT * FROM grp_familiar ORDER BY id_grp DESC"

            Return SqlRepository.ExecuteDataTable(sqlQuerie, Nothing)

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

            Return SqlRepository.ExecuteDataTable(sqlQuery, parameter)

        End Function

        '|-----------------------------------------------------------------------
        '| REGISTRO COMPLETO DE CLIENTES, PAGOS Y ACTUALIZACIÍN DE GRUPO FAMILIAR
        '|-----------------------------------------------------------------------
        Public Sub RegisterClientPayment(data As ClientPaymentDTO)

            Using connectionString As New MySqlConnection(_connectionString)

                connectionString.Open()

                Using transaction = connectionString.BeginTransaction()

                    Try
                        '| * INSERT CLIENTE
                        data.IdClienteCreado = InsertClient(connectionString, transaction, data)

                        '| * OBTENER TARIFA
                        Dim tarifa = GetRate(connectionString, transaction, data)

                        '| * INSERT PAGO
                        InsertPayment(connectionString, transaction, data, tarifa)

                        '| * UPDATE GRUPO (solo si aplica)
                        If data.IsGroup Then UpdateGroup(connectionString, transaction, data)

                        transaction.Commit()

                    Catch

                        transaction.Rollback()
                        Throw

                    End Try

                End Using

            End Using

        End Sub

        '|-------------------------------------------------
        '| FUNCIÓN PARA INSERTAR UN CLIENTE Y OBTENER SU ID - GEMINI
        '|-------------------------------------------------
        Private Function InsertClient(connection As MySqlConnection,
                                  transaction As MySqlTransaction,
                                data As ClientPaymentDTO) As Integer

            ' 1. Una sola consulta SQL para ambos casos (Individual o Grupal)
            Dim sqlQuery As String = "INSERT INTO clientes(nom_cli, ape_cli, fdn_cli, tlf_cli, eml_cli, dir_cli, mpg_cli, fdi_cli, std_cli, id_grp)
                                    VALUES(@nom, @ape, @fdn, @tlf, @eml, @dir, @mpg, @fdi, @std, @idgrp);
                                    SELECT LAST_INSERT_ID();"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                ' 2. Añadimos parámetros especificando el tipo de dato (Más seguro para fechas)
                command.Parameters.Add("@nom", MySqlDbType.VarChar).Value = data.Nombre
                command.Parameters.Add("@ape", MySqlDbType.VarChar).Value = data.Apellido
                command.Parameters.Add("@fdn", MySqlDbType.Date).Value = data.FechaNacimiento
                command.Parameters.Add("@tlf", MySqlDbType.VarChar).Value = data.Telefono
                command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = data.Email
                command.Parameters.Add("@dir", MySqlDbType.VarChar).Value = data.Direccion
                command.Parameters.Add("@mpg", MySqlDbType.VarChar).Value = data.MetodoPago
                command.Parameters.Add("@fdi", MySqlDbType.Date).Value = data.FechaInscripcion
                command.Parameters.Add("@std", MySqlDbType.VarChar).Value = data.Estado

                ' 3. Manejo del Grupo: Si es grupo, pasamos el ID; si no, pasamos DBNull
                If data.IsGroup AndAlso data.IdGrupo.HasValue Then
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = data.IdGrupo.Value
                Else
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = DBNull.Value
                End If

                ' 4. Ejecutamos y retornamos el ID recién creado
                Return Convert.ToInt32(command.ExecuteScalar())

            End Using
        End Function

        '|-------------------------------------------------------------------
        '| CONSULTAR LA TARIFA CORRESPONDIENTE AL CLIENTE O AL GRUPO FAMILIAR - GEMINI
        '|-------------------------------------------------------------------
        Private Function GetRate(connection As MySqlConnection,
                         transaction As MySqlTransaction,
                         data As ClientPaymentDTO) As RateResult

            Dim result As New RateResult With {.Exists = False, .Price = 0, .Discount = 0}
            Dim sqlQuery As String ' = ""

            ' 1. Determinar la consulta principal
            Select Case data.MetodoPago
                Case "MENSUAL"
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE emin_trfa <= @val AND emax_trfa >= @val LIMIT 1"

                Case "GRUPAL"
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE nperson_trfa = @val LIMIT 1"

                Case Else 'DIARIO
                    sqlQuery = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE tipo_trfa = @tipo LIMIT 1"

            End Select

            ' 2. Ejecutar consulta principal
            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                If data.MetodoPago = "MENSUAL" Then
                    command.Parameters.AddWithValue("@val", data.Edad)

                ElseIf data.MetodoPago = "GRUPAL" Then
                    command.Parameters.AddWithValue("@val", data.IntegrantesGrupo)

                Else
                    command.Parameters.AddWithValue("@tipo", data.MetodoPago)

                End If

                FillResultFromReader(command, result)

            End Using

            ' 3. FALLBACK: Si no existía por edad/grupo, buscar por nombre de tipo_trfa
            If Not result.Exists Then

                Using cmdFallback As New MySqlCommand("SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE tipo_trfa = @tipo LIMIT 1", connection, transaction)

                    cmdFallback.Parameters.AddWithValue("@tipo", data.MetodoPago)
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
            End Using
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

                    cmdCheck.Parameters.AddWithValue("@idGrp", data.IdGrupo.Value)
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
                    finalPrice = tarifa.Price * data.IntegrantesGrupo
                End If

                command.Parameters.Add("@fdi", MySqlDbType.Date).Value = DateTime.Today
                command.Parameters.Add("@mtd", MySqlDbType.VarChar).Value = data.MetodoPago
                command.Parameters.Add("@prc", MySqlDbType.Decimal).Value = finalPrice
                command.Parameters.Add("@dsc", MySqlDbType.Decimal).Value = tarifa.Discount

                ' Asignación de IDs
                If data.IsGroup Then
                    command.Parameters.Add("@idcli", MySqlDbType.Int16).Value = DBNull.Value
                    command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = data.IdGrupo.Value
                Else
                    command.Parameters.Add("@idcli", MySqlDbType.Int16).Value = data.IdClienteCreado
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

            ' Solo incrementamos el contador de los que ya están registrados
            Dim sqlQuery As String = "UPDATE grp_familiar SET intgrntes_reg_grp = intgrntes_reg_grp + 1 WHERE id_grp = @idgrp"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                command.Parameters.Add("@idgrp", MySqlDbType.Int16).Value = data.IdGrupo.Value
                command.ExecuteNonQuery()

            End Using
        End Sub

    End Class

End Namespace
