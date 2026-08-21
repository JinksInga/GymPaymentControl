Imports GymPaymentControl.Data
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services

    ''' <summary>
    ''' Servicio de dominio encargado de consultar deudas pendientes, 
    ''' procesar reglas de negocio para el cálculo de cobros
    ''' y gestionar la persistencia de transacciones de pago.
    ''' </summary>
    Public Class PaymentManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ''' <summary>
        ''' Obtiene la cadena de conexión configurada en el repositorio base.
        ''' </summary>
        Public ReadOnly Property ConnectionString As String
            Get
                Return _connectionString
            End Get
        End Property

#Region " PAGOS INDIVIDUALES "

        ''' <summary>
        ''' Recupera el listado completo de deudores individuales activos,
        ''' calculando sus importes y adjuntando filas de resumen por cada cliente.
        ''' </summary>
        ''' <returns>Una lista de <see cref="IndividualPaymentDTO"/> formateada para su presentación en UI.</returns>
        Public Function GetListIndividualDebtors() As List(Of IndividualPaymentDTO)

            Dim baseData = GetBaseDataIndividual()
            CalculateIndividualPayments(baseData)

            Return BuildFinalIndividualList(baseData)

        End Function


        ''' <summary>
        ''' Obtiene desde la base de datos la lista de pagos individuales
        ''' pendientes de cobro (clientes activos, sin grupo, sin pago finalizado).
        ''' </summary>
        Private Function GetBaseDataIndividual() As List(Of IndividualPaymentDTO)

            Dim listIndividualPayment As New List(Of IndividualPaymentDTO)

            Dim sqlQuery As String = "SELECT c.nom_cli, c.ape_cli, c.fdn_cli, " &
                                            "p.id_pgs, p.fdi_pgs, p.mtd_pgs, " &
                                            "p.prc_pgs, p.dsc_pgs, p.id_cli " &
                                     "FROM clientes c " &
                                     "INNER JOIN pagos p ON c.id_cli = p.id_cli " &
                                     "WHERE (p.frm_pgs IS NULL OR p.frm_pgs = '') " &
                                     "ORDER BY p.id_cli, p.fdi_pgs"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)
                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()
                            ' Mapeo de datos a DTO
                            Dim dto As New IndividualPaymentDTO With
                                {
                                    .IdPgs = dataReader.GetInt32("id_pgs"),
                                    .IdCli = dataReader.GetInt32("id_cli"),
                                    .FirstName = dataReader.GetString("nom_cli"),
                                    .LastName = dataReader.GetString("ape_cli"),
                                    .Age = CalculateClientAge(dataReader.GetDateTime("fdn_cli")),
                                    .MtdPgs = dataReader.GetString("mtd_pgs"),
                                    .PrcPgs = dataReader.GetDecimal("prc_pgs"),'Convert.ToDecimal(dataReader("prc_pgs")),
                                    .DscPgs = dataReader.GetDecimal("dsc_pgs"),'Convert.ToDecimal(dataReader("dsc_pgs")),
                                    .FdiPgs = dataReader.GetDateTime("fdi_pgs")
                                }
                            ' Formato de fecha largo para presentación
                            dto.LongFdiPgs = FormatDateUppercase(dto.FdiPgs)

                            listIndividualPayment.Add(dto)

                        End While

                    End Using
                End Using
            End Using

            Return listIndividualPayment

        End Function


        ''' <summary>
        ''' Aplica la lógica de cálculo de montos finales a cada ítem de pago individual.
        ''' </summary>
        Private Sub CalculateIndividualPayments(items As List(Of IndividualPaymentDTO))

            For Each item In items
                CalculatePaymentAmount(item)
            Next

        End Sub


        ''' <summary>
        ''' Agrupa los pagos individuales por cliente e
        ''' inyecta la fila de resumen por cada uno.
        ''' </summary>
        Private Function BuildFinalIndividualList(baseData As List(Of IndividualPaymentDTO)
                                             ) As List(Of IndividualPaymentDTO)

            Dim result As New List(Of IndividualPaymentDTO)

            For Each group In baseData.GroupBy(Function(x) x.IdCli)

                result.AddRange(group)
                result.Add(CreateIndividualSummaryRow(group))

            Next

            Return result

        End Function


        ''' <summary>
        ''' Crea la entidad DTO que representa la fila de resumen visual
        ''' para un cliente individual.
        ''' </summary>
        Private Function CreateIndividualSummaryRow(group As IGrouping(Of Integer, IndividualPaymentDTO)
                                         ) As IndividualPaymentDTO

            Return New IndividualPaymentDTO With
                {
                    .IdCli = group.Key,
                    .MtdPgs = group.First().MtdPgs,
                    .IsSummaryRow = True,
                    .NumberMonths = group.Count(),
                    .TotalToPay = group.Sum(Function(x) x.TotalToPay)
                }

        End Function

#End Region


#Region " PAGOS GRUPALES "

        ''' <summary>
        ''' Recupera el listado completo de grupos familiares deudores,
        ''' calculando sus importes y adjuntando filas de resumen por grupo.
        ''' </summary>
        ''' <returns>Una lista de <see cref="GroupPaymentDTO"/> formateada para su presentación en UI.</returns>
        Public Function GetListGroupDebtors() As List(Of GroupPaymentDTO)

            Dim baseData = GetBaseDataGroup()
            CalculateGroupPayments(baseData)

            Return BuildFinalGroupList(baseData)

        End Function


        ''' <summary>
        ''' Obtiene desde la base de datos la lista base de pagos grupales pendientes de cobro.
        ''' </summary>
        Private Function GetBaseDataGroup() As List(Of GroupPaymentDTO)

            Dim listGroupPayment As New List(Of GroupPaymentDTO)

            Dim sqlQuery As String = "SELECT GROUP_CONCAT(c.nom_cli SEPARATOR ', ') AS INTEGRANTES, " &
                                     "g.id_grp, g.nom_grp, " &
                                     "p.fdi_pgs, p.mtd_pgs, " &
                                     "p.prc_pgs, p.dsc_pgs, p.id_pgs " &
                                     "FROM clientes c " &
                                     "INNER JOIN grp_familiar g ON c.id_grp = g.id_grp " &
                                     "INNER JOIN pagos p ON g.id_grp = p.id_grp " &
                                     "WHERE (p.frm_pgs IS NULL OR p.frm_pgs = '') " &
                                     "GROUP BY p.id_pgs " &
                                     "ORDER BY g.id_grp, p.fdi_pgs ASC"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()

                            Dim dto As New GroupPaymentDTO With
                                {
                                    .IdPgs = dataReader.GetInt32("id_pgs"),
                                    .IdGrp = dataReader.GetInt32("id_grp"),
                                    .GroupName = dataReader.GetString("nom_grp"),
                                    .GroupMembers = dataReader.GetString("INTEGRANTES"),
                                    .MtdPgs = dataReader.GetString("mtd_pgs"),
                                    .PrcPgs = dataReader.GetDecimal("prc_pgs"),'Convert.ToDecimal(dataReader("prc_pgs")),
                                    .DscPgs = dataReader.GetDecimal("dsc_pgs"),'Convert.ToDecimal(dataReader("dsc_pgs")),
                                    .FdiPgs = dataReader.GetDateTime("fdi_pgs")
                                }
                            dto.LongDate = FormatDateUppercase(dto.FdiPgs)
                            listGroupPayment.Add(dto)

                        End While

                    End Using
                End Using
            End Using

            Return listGroupPayment

        End Function


        ''' <summary>
        ''' Aplica la lógica de cálculo de montos finales a cada ítem de pago grupal.
        ''' </summary>
        Private Sub CalculateGroupPayments(items As List(Of GroupPaymentDTO))

            For Each item In items
                CalculatePaymentAmount(item)
            Next

        End Sub


        ''' <summary>
        ''' Agrupa los pagos por grupo familiar e inyecta la fila de resumen totabilizadora.
        ''' </summary>
        Private Function BuildFinalGroupList(baseData As List(Of GroupPaymentDTO)
                                             ) As List(Of GroupPaymentDTO)

            Dim result As New List(Of GroupPaymentDTO)

            For Each group In baseData.GroupBy(Function(x) x.IdGrp)

                result.AddRange(group)
                result.Add(CreateGroupSummaryRow(group))

            Next

            Return result

        End Function


        ''' <summary>
        ''' Crea la entidad DTO que representa la fila de resumen visual para un grupo familiar.
        ''' </summary>
        Private Function CreateGroupSummaryRow(group As IGrouping(Of Integer, GroupPaymentDTO)
                                                ) As GroupPaymentDTO

            Return New GroupPaymentDTO With
                {
                    .IdGrp = group.Key,
                    .IsSummaryRow = True,
                    .NumberMonths = group.Count(),
                    .TotalToPay = group.Sum(Function(x) x.TotalToPay)
                }
        End Function

#End Region


#Region " PERSISTENCIA Y TRANSACCIONALIDAD "

        ''' <summary>
        ''' Punto centralizado de inserción y actualización de registros de pago en la BBDD.
        ''' </summary>
        ''' <remarks>
        ''' Esta función soporta:
        ''' - Inserción de nuevos pagos.
        ''' - Actualización de pagos existentes.
        ''' - Creación de menbresías masivas.
        ''' - Uso independiente o dentro de transacciones externas.
        '''
        ''' Centralizar esta lógica evita inconsistencias entre
        ''' distintos procesos de registro y cobro.
        ''' </remarks>
        ''' <param name="payment">Objeto que implementa la interfaz <see cref="IPaymentCalculable"/>.</param>
        ''' <param name="mode">Modo de transacción (<c>NewPayment</c> o <c>UpdatePayment</c>).</param>
        ''' <param name="idUser">Identificador del usuario que registra la operación.</param>
        ''' <param name="paymentMethod">Forma o método de pago aplicado (ej. "EFECTIVO", "TARJETA").</param>
        ''' <param name="externalConn">Conexión MySQL externa opcional para ejecuciones dentro de un bloque transaccional amplio.</param>
        ''' <param name="externalTrans">Transacción MySQL externa opcional.</param>
        ''' <returns><c>True</c> si la operación afectó al menos un registro; de lo contrario, <c>False</c>.</returns>
        Public Function SavePaymentTransaction(payment As IPaymentCalculable, mode As TransactionMode,
                                               idUser As Integer, paymentMethod As String,
                                               Optional externalConn As MySqlConnection = Nothing,
                                               Optional externalTrans As MySqlTransaction = Nothing) As Boolean

            Dim conn = If(externalConn, GetConnection())
            Dim closeConn As Boolean = (externalConn Is Nothing)

            Try
                If conn.State <> ConnectionState.Open Then conn.Open()

                '| * CONTROL DE DUPLICADOS PARA GRUPOS (Solo aplica en NUEVOS PAGOS)

                If mode = TransactionMode.NewPayment AndAlso
                    TypeOf payment Is GroupPaymentDTO Then

                    Dim groupPayment = DirectCast(payment, GroupPaymentDTO)

                    Dim sqlCheck As String = "SELECT COUNT(*) FROM pagos " &
                                             "WHERE id_grp = @idGrp " &
                                             "AND MONTH(fdi_pgs) = MONTH(@fdi) " &
                                             "AND YEAR(fdi_pgs) = YEAR(@fdi)"

                    Using cmdCheck As New MySqlCommand(sqlCheck, conn, externalTrans)

                        cmdCheck.Parameters.Add("@idGrp", MySqlDbType.Int32).Value = groupPayment.IdGrp
                        cmdCheck.Parameters.Add("@fdi", MySqlDbType.Date).Value = groupPayment.FdiPgs

                        Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())

                        If exists > 0 Then Return True

                    End Using

                End If


                '| * LÓGICA DE INSERCIÓN / ACTUALIZACIÓN

                Dim sqlQuery As String

                If mode = TransactionMode.NewPayment Then
                    sqlQuery = "INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, mtd_pgs, " &
                                                  "prc_pgs, dsc_pgs, id_cli, id_grp, id_user) " &
                               "VALUES (@fdi, @fdp, @frm, @mtd, @prc, @dsc, @idCli, @idGrp, @idUser)"
                Else
                    sqlQuery = "UPDATE pagos " &
                               "SET fdi_pgs=@fdi, fdp_pgs=@fdp, frm_pgs=@frm, mtd_pgs=@mtd, " &
                                   "prc_pgs=@prc, dsc_pgs=@dsc, id_user=@idUser " &
                               "WHERE id_pgs=@idPgs"
                End If

                Using command As New MySqlCommand(sqlQuery, conn, externalTrans)

                    command.Parameters.Add("@fdi", MySqlDbType.Date).Value = payment.FdiPgs
                    command.Parameters.Add("@fdp", MySqlDbType.Date).Value = payment.FdpPgs 'If(payment.FdpPgs.HasValue, payment.FdpPgs.Value, DBNull.Value)
                    command.Parameters.Add("@frm", MySqlDbType.VarChar).Value = paymentMethod
                    command.Parameters.Add("@mtd", MySqlDbType.VarChar).Value = payment.MtdPgs
                    command.Parameters.Add("@prc", MySqlDbType.Decimal).Value = payment.PrcPgs
                    command.Parameters.Add("@dsc", MySqlDbType.Decimal).Value = payment.DscPgs
                    command.Parameters.Add("@idUser", MySqlDbType.Int32).Value = idUser

                    If mode = TransactionMode.UpdatePayment Then
                        command.Parameters.Add("@idPgs", MySqlDbType.Int32).Value = payment.IdPgs
                    Else
                        ' Asignación dinámica de IDs según la naturaleza del pago.
                        Dim idCli As Object = DBNull.Value
                        Dim idGrp As Object = DBNull.Value

                        If TypeOf payment Is IndividualPaymentDTO Then
                            idCli = DirectCast(payment, IndividualPaymentDTO).IdCli
                        ElseIf TypeOf payment Is GroupPaymentDTO Then
                            idGrp = DirectCast(payment, GroupPaymentDTO).IdGrp
                        End If

                        command.Parameters.Add("@idCli", MySqlDbType.Int32).Value = idCli
                        command.Parameters.Add("@idGrp", MySqlDbType.Int32).Value = idGrp
                    End If

                    Return command.ExecuteNonQuery() > 0

                End Using

            Catch
                Throw
            Finally

                If closeConn AndAlso conn IsNot Nothing Then
                    conn.Dispose()
                End If

            End Try

        End Function


        ''' <summary>
        ''' Recupera el historial completo de pagos asociados a un cliente individual o a su grupo familiar.
        ''' </summary>
        ''' <param name="idClient">Identificador único del cliente.</param>
        ''' <param name="idGroup">Identificador opcional del grupo familiar al que pertenece.</param>
        ''' <returns>Una lista de <see cref="IndividualPaymentDTO"/> que representan los cobros históricos.</returns>
        Public Function GetPaymentHistory(idClient As Integer, idGroup As Integer?) As List(Of IndividualPaymentDTO)

            Dim historyList As New List(Of IndividualPaymentDTO)

            Dim sqlQuery As String = "SELECT p.*, u.nom_user " &
                                     "FROM pagos p " &
                                     "LEFT JOIN usuarios u ON p.id_user = u.id_user " &
                                     "WHERE p.id_cli = @idClient "

            If idGroup.HasValue AndAlso idGroup.Value > 0 Then
                sqlQuery &= "OR p.id_grp = @idGroup "
            End If

            sqlQuery &= "ORDER BY p.fdi_pgs DESC"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.Add("@idClient", MySqlDbType.Int32).Value = idClient

                    If idGroup.HasValue AndAlso idGroup.Value > 0 Then
                        command.Parameters.Add("@idGroup", MySqlDbType.Int32).Value = idGroup.Value
                    End If

                    connection.Open()

                    Using dr = command.ExecuteReader()

                        While dr.Read()

                            Dim fdpOrdinal As Integer = dr.GetOrdinal("fdp_pgs")
                            Dim fdpDate As Date = If(dr.IsDBNull(fdpOrdinal), Date.MinValue, dr.GetDateTime(fdpOrdinal))
                            Dim strFrmPgs As String = If(dr.IsDBNull(dr.GetOrdinal("frm_pgs")), "IMPAGO", dr.GetString("frm_pgs"))

                            historyList.Add(New IndividualPaymentDTO With
                                            {
                                                .IdPgs = dr.GetInt32("id_pgs"),
                                                .FdiPgs = dr.GetDateTime("fdi_pgs"),
                                                .LongFdiPgs = FormatDateUppercase(dr.GetDateTime("fdi_pgs")),
                                                .FdpPgs = fdpDate,
                                                .LongFdpPgs = If(fdpDate = Date.MinValue, "SIN FECHA", FormatDateUppercase(fdpDate)),
                                                .MtdPgs = dr.GetString("mtd_pgs"),
                                                .FrmPgs = strFrmPgs,
                                                .HasDebtCustomer = (strFrmPgs = "IMPAGO"),
                                                .PrcPgs = dr.GetDecimal("prc_pgs"),
                                                .DscPgs = dr.GetDecimal("dsc_pgs"),
                                                .NomUser = If(dr.IsDBNull(dr.GetOrdinal("nom_user")), "N/A", dr.GetString("nom_user"))
                                            })
                        End While

                    End Using
                End Using
            End Using

            CalculateIndividualPayments(historyList)
            Return historyList

        End Function

#End Region


#Region " FORMULARIO : FrmFamilyGroup "

        ''' <summary>
        ''' Determina si un grupo familiar tiene actualmente alguna deuda pendiente sin abonar.
        ''' </summary>
        ''' <param name="groupId">Identificador único del grupo familiar.</param>
        ''' <returns><c>True</c> si el grupo tiene al menos un cobro registrado sin forma de pago; de lo contrario, <c>False</c>.</returns>
        Public Function HasPendingGroupDebt(groupId As Integer) As Boolean

            Const sqlQuery As String = "SELECT EXISTS (SELECT 1 " &
                                       "FROM pagos " &
                                       "WHERE id_grp = @id_grp " &
                                       "AND (frm_pgs IS NULL OR frm_pgs = ''))"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.Add("@id_grp", MySqlDbType.Int32).Value = groupId
                    connection.Open()

                    Return Convert.ToBoolean(command.ExecuteScalar())

                End Using
            End Using

        End Function


        ''' <summary>
        ''' Determina si un cliente tiene actualmente alguna deuda individual pendiente.
        ''' Solo se consideran pagos asociados directamente al cliente y no los pagos
        ''' correspondientes a un grupo familiar.
        ''' </summary>
        ''' <param name="clientId">Identificador único del cliente.</param>
        ''' <returns><c>True</c> si el cliente tiene al menos una mensualidad
        ''' sin forma de pago asignada; de lo contrario, <c>False</c>.</returns>
        Public Function HasPendingIndividualDebt(clientId As Integer) As Boolean

            Const sqlQuery As String = "SELECT EXISTS (SELECT 1 " &
                                       "FROM pagos " &
                                       "WHERE id_cli = @id_cli " &
                                       "AND (frm_pgs IS NULL OR frm_pgs = ''))"

            Using connection As MySqlConnection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.Add("@id_cli", MySqlDbType.Int32).Value = clientId
                    connection.Open()

                    Return Convert.ToBoolean(command.ExecuteScalar())

                End Using
            End Using

        End Function

#End Region

    End Class

End Namespace