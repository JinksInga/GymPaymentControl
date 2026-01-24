Imports System.Configuration
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

        ' Cadena de conexión a MySQL tomada desde App.config
        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

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

            Using connection As New MySqlConnection(_connectionString)
                Using command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()
                            ' Mapeo de datos a DTO
                            Dim dto As New IndividualPaymentDTO With
                                {
                                    .IdPgs = dataReader("id_pgs"),
                                    .IdCli = dataReader("id_cli"),
                                    .Name = dataReader("nom_cli").ToString(),
                                    .LastName = dataReader("ape_cli").ToString(),
                                    .Age = CalculateClientAge(dataReader.GetDateTime("fdn_cli")),
                                    .MtdPgs = dataReader("mtd_pgs").ToString(),
                                    .PrcPgs = Convert.ToDecimal(dataReader("prc_pgs")),
                                    .DscPgs = Convert.ToDecimal(dataReader("dsc_pgs")),
                                    .FdiPgs = dataReader.GetDateTime("fdi_pgs")
                                }
                            ' Formato de fecha largo para presentación
                            dto.LongDate = ConvertLongDate(dto.FdiPgs)

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
                CalculateIndividualPayment(item)
            Next

        End Sub

        ' Aplica la lógica de cálculo según el método de pago
        Private Sub CalculateIndividualPayment(item As IndividualPaymentDTO)
            ' Pagos mensuales → cálculo prorrateado
            If item.MtdPgs.Contains("MENSUAL") Then
                CalculateProratedPayment(item)

            Else
                ' Pagos diarios u otros → pago completo
                item.DaysOfMonth = 1
                item.Total = item.PrcPgs
                item.TotalToPay = item.PrcPgs
            End If

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

            Dim sql As String = "SELECT GROUP_CONCAT(c.nom_cli SEPARATOR ', ') AS INTEGRANTES,
                                g.id_grp, g.nom_grp,
                                p.fdi_pgs, p.mtd_pgs, p.prc_pgs, p.dsc_pgs, p.id_pgs 
                                FROM clientes c
                                INNER JOIN grp_familiar g ON c.id_grp = g.id_grp
                                INNER JOIN pagos p ON g.id_grp = p.id_grp
                                WHERE p.fdp_pgs IS NULL
                                GROUP BY p.id_pgs
                                ORDER BY g.id_grp, p.fdi_pgs ASC"

            Using connection As New MySqlConnection(_connectionString)
                Using command As New MySqlCommand(sql, connection)

                    connection.Open()

                    Using dataReader = command.ExecuteReader()

                        While dataReader.Read()

                            Dim dto As New GroupPaymentDTO With
                                {
                                    .IdPgs = dataReader("id_pgs"),
                                    .IdGrp = dataReader("id_grp"),
                                    .GroupName = dataReader("nom_grp").ToString(),
                                    .Members = dataReader("INTEGRANTES").ToString(),
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
                CalculateGroupPayment(item)
            Next

        End Sub

        ' Todos los pagos grupales son prorrateados
        Private Sub CalculateGroupPayment(item As GroupPaymentDTO)

            CalculateProratedPayment(item)

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
        ''
        ''
        Public Function SaveTransaction(payment As IPaymentCalculable, mode As TransactionMode,
                                        idUser As Integer, paymentMethod As String) As Boolean
            Dim sqlQuery As String

            If mode = TransactionMode.NewPayment Then
                ' SQL de Inserción: Usamos los campos comunes
                ' Nota: He añadido @idOwner para que sirva tanto para id_cli como para id_grp
                sqlQuery = "INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
                            VALUES (@fdi, @fdp, @frm, @prc, @dsc, @idCli, @idGrp, @idUser)"
            Else
                ' SQL de Actualización para registros que ya existen
                sqlQuery = "UPDATE pagos
                            SET fdi_pgs=@fdi,
                                fdp_pgs=@fdp,
                                frm_pgs=@frm,
                                prc_pgs=@prc,
                                dsc_pgs=@dsc,
                                id_user=@idUser
                            WHERE id_pgs=@idPgs"
            End If

            Using connection As New MySqlConnection(_connectionString)

                Dim command As New MySqlCommand(sqlQuery, connection)

                ' Parámetros comunes de la Interfaz
                command.Parameters.AddWithValue("@fdi", payment.FdiPgs)
                command.Parameters.AddWithValue("@fdp", payment.FdpPgs)
                command.Parameters.AddWithValue("@frm", paymentMethod)
                command.Parameters.AddWithValue("@prc", payment.PrcPgs)
                command.Parameters.AddWithValue("@dsc", payment.DscPgs)
                command.Parameters.AddWithValue("@idUser", idUser)

                If mode = TransactionMode.UpdatePayment Then
                    command.Parameters.AddWithValue("@idPgs", payment.IdPgs)
                Else
                    ' --- LÓGICA PARA NUEVOS PAGOS (Detección de tipo) ---
                    If TypeOf payment Is IndividualPaymentDTO Then
                        command.Parameters.AddWithValue("@idCli", DirectCast(payment, IndividualPaymentDTO).IdCli)
                        command.Parameters.AddWithValue("@idGrp", DBNull.Value) ' No es un grupo
                    ElseIf TypeOf payment Is GroupPaymentDTO Then
                        command.Parameters.AddWithValue("@idCli", DBNull.Value) ' No es un cliente individual
                        command.Parameters.AddWithValue("@idGrp", DirectCast(payment, GroupPaymentDTO).IdGrp)
                    End If
                End If

                connection.Open()
                Return command.ExecuteNonQuery() > 0
            End Using
        End Function
        ''
        ''
        ''
    End Class
End Namespace