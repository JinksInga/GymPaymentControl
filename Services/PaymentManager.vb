Imports GymPaymentControl.Data
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services

    ' Servicio encargado de:
    ' - Obtener pagos pendientes (individuales y grupales)
    ' - Aplicar reglas de negocio (cálculos, prorrateos)
    ' - Construir filas de resumen para presentación
    Public Class PaymentManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ' Propiedad de solo lectura que permite leer la cadena de conexión desde fuera de la clase, sin permitir su modificación.
        Public ReadOnly Property ConnectionString As String
            Get
                Return _connectionString
            End Get
        End Property

        ' =====================================================
        ' ========== PAGOS INDIVIDUALES =======================
        ' =====================================================

        ' Método público:
        ' Devuelve la lista final de morosos individuales,
        ' incluyendo filas de resumen por cliente.
        Public Function GetListIndividualDebtors() As List(Of IndividualPaymentDTO)
            ' 1. Obtener datos base desde la BBDD
            Dim baseData = GetBaseDataIndividual()
            ' 2. Aplicar reglas de negocio y cálculos
            CalculateIndividualPayments(baseData)
            ' 3. Construir lista final con filas de resumen
            Return BuildFinalIndividualList(baseData)

        End Function

        ' Obtiene los pagos individuales pendientes desde la base de datos
        ' (clientes activos, sin grupo, sin pago finalizado)
        Private Function GetBaseDataIndividual() As List(Of IndividualPaymentDTO)

            Dim listIndividualPayment As New List(Of IndividualPaymentDTO)

            Dim sqlQuery As String = "SELECT c.nom_cli, c.ape_cli, c.fdn_cli,
                                        p.id_pgs, p.fdi_pgs, p.mtd_pgs, p.prc_pgs, p.dsc_pgs, p.id_cli
                                        FROM clientes c
                                        INNER JOIN pagos p ON c.id_cli = p.id_cli
                                        WHERE c.std_cli = 'ACTIVO'
                                        AND (p.frm_pgs IS NULL OR p.frm_pgs = '')
                                        AND c.id_grp IS NULL
                                        ORDER BY p.id_cli, p.fdi_pgs"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()
                            ' Mapeo de datos a DTO
                            Dim dto As New IndividualPaymentDTO With
                                {
                                    .IdPgs = dataReader("id_pgs"),
                                    .IdCli = dataReader("id_cli"),
                                    .FirstName = dataReader("nom_cli").ToString(),
                                    .LastName = dataReader("ape_cli").ToString(),
                                    .Age = CalculateClientAge(dataReader.GetDateTime("fdn_cli")),
                                    .MtdPgs = dataReader("mtd_pgs").ToString(),
                                    .PrcPgs = Convert.ToDecimal(dataReader("prc_pgs")),
                                    .DscPgs = Convert.ToDecimal(dataReader("dsc_pgs")),
                                    .FdiPgs = dataReader.GetDateTime("fdi_pgs")
                                }
                            ' Formato de fecha largo para presentación
                            dto.LongFdiPgs = ConvertLongDate(dto.FdiPgs)

                            listIndividualPayment.Add(dto)

                        End While

                    End Using
                End Using
            End Using

            Return listIndividualPayment

        End Function

        ' Aplica cálculos a todos los pagos individuales
        Private Sub CalculateIndividualPayments(items As List(Of IndividualPaymentDTO))

            For Each item In items
                CalculatePaymentAmount(item)
            Next

        End Sub

        ' Construye la lista final agrupando por cliente
        ' y añadiendo una fila resumen por cada uno
        Private Function BuildFinalIndividualList(baseData As List(Of IndividualPaymentDTO)
                                             ) As List(Of IndividualPaymentDTO)

            Dim result As New List(Of IndividualPaymentDTO)

            For Each group In baseData.GroupBy(Function(x) x.IdCli)

                result.AddRange(group)
                result.Add(CreateIndividualSummaryRow(group))

            Next

            Return result

        End Function

        ' Crea la fila resumen (fila naranja) de un cliente
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

        ' =====================================================
        ' ========== PAGOS GRUPALES ===========================
        ' =====================================================

        ' Método público:
        ' Devuelve la lista final de morosos grupales,
        ' incluyendo filas de resumen por grupo familiar.
        Public Function GetListGroupDebtors() As List(Of GroupPaymentDTO)

            Dim baseData = GetBaseDataGroup()
            CalculateGroupPayments(baseData)

            Return BuildFinalGroupList(baseData)

        End Function

        ' Obtiene los pagos grupales pendientes desde la BBDD
        Private Function GetBaseDataGroup() As List(Of GroupPaymentDTO)

            Dim listGroupPayment As New List(Of GroupPaymentDTO)

            Dim sqlQuery As String = "SELECT GROUP_CONCAT(c.nom_cli SEPARATOR ', ') AS INTEGRANTES,
                                        g.id_grp, g.nom_grp,
                                        p.fdi_pgs, p.mtd_pgs, p.prc_pgs, p.dsc_pgs, p.id_pgs 
                                        FROM clientes c
                                        INNER JOIN grp_familiar g ON c.id_grp = g.id_grp
                                        INNER JOIN pagos p ON g.id_grp = p.id_grp
                                        WHERE (p.frm_pgs IS NULL OR p.frm_pgs = '')
                                        GROUP BY p.id_pgs
                                        ORDER BY g.id_grp, p.fdi_pgs ASC"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()

                            Dim dto As New GroupPaymentDTO With
                                {
                                    .IdPgs = dataReader("id_pgs"),
                                    .IdGrp = dataReader("id_grp"),
                                    .GroupName = dataReader("nom_grp").ToString(),
                                    .GroupMembers = dataReader("INTEGRANTES").ToString(),
                                    .MtdPgs = dataReader("mtd_pgs"),
                                    .PrcPgs = Convert.ToDecimal(dataReader("prc_pgs")),
                                    .DscPgs = Convert.ToDecimal(dataReader("dsc_pgs")),
                                    .FdiPgs = dataReader.GetDateTime("fdi_pgs")
                                }
                            dto.LongDate = ConvertLongDate(dto.FdiPgs)
                            listGroupPayment.Add(dto)

                        End While

                    End Using
                End Using
            End Using

            Return listGroupPayment

        End Function

        ' Aplica cálculos a todos los pagos grupales
        Private Sub CalculateGroupPayments(items As List(Of GroupPaymentDTO))

            For Each item In items
                CalculatePaymentAmount(item)
            Next

        End Sub

        ' Construye la lista final agrupando por grupo familiar
        Private Function BuildFinalGroupList(baseData As List(Of GroupPaymentDTO)
                                             ) As List(Of GroupPaymentDTO)

            Dim result As New List(Of GroupPaymentDTO)

            For Each group In baseData.GroupBy(Function(x) x.IdGrp)

                result.AddRange(group)
                result.Add(CreateGroupSummaryRow(group))

            Next

            Return result

        End Function

        ' Crea la fila resumen (fila naranja) de un grupo familiar
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


        ''' <summary>
        ''' Punto único de inserción/actualización de pagos para evitar discrepancias.
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
        Public Function SavePaymentTransaction(payment As IPaymentCalculable, mode As TransactionMode,
                                               idUser As Integer, paymentMethod As String,
                                               Optional externalConn As MySqlConnection = Nothing,
                                               Optional externalTrans As MySqlTransaction = Nothing) As Boolean

            ' Gestionar conexión dinámica (si viene de una transacción externa como el alta de cliente)
            Dim conn = If(externalConn, GetConnection())
            Dim closeConn As Boolean = (externalConn Is Nothing)

            Try
                If conn.State <> ConnectionState.Open Then conn.Open()

                '| =================================================================
                '| * CONTROL DE DUPLICADOS PARA GRUPOS (Solo aplica en NUEVOS PAGOS)
                '| =================================================================
                If mode = TransactionMode.NewPayment AndAlso TypeOf payment Is GroupPaymentDTO Then

                    Dim groupPayment = DirectCast(payment, GroupPaymentDTO)

                    ' Comprobamos si ya existe una deuda/pago para este ID de grupo en el mes y año de la fecha de inicio (FdiPgs)
                    ' Usamos MONTH(@fdi) en lugar de CURDATE() para que también funcione correctamente en el generador masivo
                    Dim sqlCheck As String = "SELECT COUNT(*) FROM pagos WHERE id_grp = @idGrp
                                              AND MONTH(fdi_pgs) = MONTH(@fdi) AND YEAR(fdi_pgs) = YEAR(@fdi)"

                    Using cmdCheck As New MySqlCommand(sqlCheck, conn, externalTrans)

                        cmdCheck.Parameters.AddWithValue("@idGrp", groupPayment.IdGrp)
                        cmdCheck.Parameters.AddWithValue("@fdi", groupPayment.FdiPgs)

                        ' Si ya existe el pago del grupo para este periodo, salimos retornando True o False.
                        ' Retornamos True para que el proceso llamador (bucle) piense que se gestionó
                        ' correctamente y no lance un error.
                        Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                        If exists > 0 Then Return True

                    End Using
                End If

                '| =====================================
                '| * LÓGICA DE INSERCIÓN / ACTUALIZACIÓN
                '| =====================================

                Dim sqlQuery As String

                If mode = TransactionMode.NewPayment Then
                    ' SQL COMPLETO
                    sqlQuery = "INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, mtd_pgs,
                                                   prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
                                VALUES (@fdi, @fdp, @frm, @mtd, @prc, @dsc, @idCli, @idGrp, @idUser)"
                Else
                    ' UPDATE: fdp_pgs y mtd_pgs añadidos por si se corrigen en edición
                    sqlQuery = "UPDATE pagos SET fdi_pgs=@fdi, fdp_pgs=@fdp, frm_pgs=@frm, mtd_pgs=@mtd,
                                prc_pgs=@prc, dsc_pgs=@dsc, id_user=@idUser WHERE id_pgs=@idPgs"
                End If

                Using command As New MySqlCommand(sqlQuery, conn, externalTrans)
                    ' Parámetros Comunes
                    command.Parameters.AddWithValue("@fdi", payment.FdiPgs)
                    command.Parameters.AddWithValue("@fdp", payment.FdpPgs)
                    command.Parameters.AddWithValue("@frm", paymentMethod)
                    command.Parameters.AddWithValue("@mtd", payment.MtdPgs)
                    command.Parameters.AddWithValue("@prc", payment.PrcPgs)
                    command.Parameters.AddWithValue("@dsc", payment.DscPgs)
                    command.Parameters.AddWithValue("@idUser", idUser)

                    If mode = TransactionMode.UpdatePayment Then
                        command.Parameters.AddWithValue("@idPgs", payment.IdPgs)
                    Else
                        ' Lógica de IDs para nuevos registros
                        Dim idCli As Object = DBNull.Value
                        Dim idGrp As Object = DBNull.Value

                        If TypeOf payment Is IndividualPaymentDTO Then
                            idCli = DirectCast(payment, IndividualPaymentDTO).IdCli
                        ElseIf TypeOf payment Is GroupPaymentDTO Then
                            idGrp = DirectCast(payment, GroupPaymentDTO).IdGrp
                        End If

                        command.Parameters.AddWithValue("@idCli", idCli)
                        command.Parameters.AddWithValue("@idGrp", idGrp)
                    End If

                    Return command.ExecuteNonQuery() > 0

                End Using

            Catch ex As Exception
                Throw ex

            Finally
                If closeConn AndAlso conn IsNot Nothing Then conn.Dispose()

            End Try

        End Function


        Public Function GetPaymentHistory(idClient As Integer, idGroup As Integer?) As List(Of IndividualPaymentDTO)

            Dim historyList As New List(Of IndividualPaymentDTO)

            ' El cerebro decide el SQL: Si hay grupo, prioriza grupo, si no, por cliente
            Dim sqlQuery As String
            Dim idForQuery As Integer

            ' Definimos la base de la consulta para no repetir texto
            Dim sqQueryBase As String = "SELECT p.*, u.nom_user FROM pagos p
                                         LEFT JOIN usuarios u ON p.id_user = u.id_user "

            If idGroup.HasValue AndAlso idGroup.Value > 0 Then
                sqlQuery = sqQueryBase & "WHERE p.id_grp = @id ORDER BY p.fdi_pgs DESC"
                idForQuery = idGroup.Value
            Else
                sqlQuery = sqQueryBase & "WHERE p.id_cli = @id ORDER BY p.fdi_pgs DESC"
                idForQuery = idClient
            End If

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.AddWithValue("@id", idForQuery)
                    connection.Open()

                    Using dr = command.ExecuteReader()

                        While dr.Read()

                            Dim strFrmPgs = If(dr.IsDBNull(dr.GetOrdinal("frm_pgs")), "IMPAGO", dr.GetString("frm_pgs"))

                            historyList.Add(New IndividualPaymentDTO With
                                            {
                                                .IdPgs = dr.GetInt32("id_pgs"),
                                                .FdiPgs = dr.GetDateTime("fdi_pgs"),
                                                .LongFdiPgs = ConvertLongDate(dr.GetDateTime("fdi_pgs")),
                                                .FdpPgs = If(dr.IsDBNull(dr.GetOrdinal("fdp_pgs")), Date.MinValue, dr.GetDateTime("fdp_pgs")),
                                                .LongFdpPgs = If(dr.GetDateTime("fdp_pgs") = Date.MinValue, "SIN FECHA", ConvertLongDate(dr.GetDateTime("fdp_pgs"))),
                                                .MtdPgs = dr.GetString("mtd_pgs"),
                                                .FrmPgs = If(dr.IsDBNull(dr.GetOrdinal("frm_pgs")), "IMPAGO", dr.GetString("frm_pgs")),
                                                .HasDebtCustomer = (strFrmPgs = "IMPAGO"),
                                                .PrcPgs = dr.GetDecimal("prc_pgs"),
                                                .DscPgs = dr.GetDecimal("dsc_pgs"),
                                                .NomUser = If(dr.IsDBNull(dr.GetOrdinal("nom_user")), "N/A", dr.GetString("nom_user"))
                                            })
                        End While

                    End Using
                End Using
            End Using

            ' ¡AQUÍ REUTILIZAMOS TUS FUNCIONES!
            ' Al pasar la lista por aquí, se calcularán nDias y TotalToPay para cada registro
            CalculateIndividualPayments(historyList)

            Return historyList

        End Function


    End Class
End Namespace