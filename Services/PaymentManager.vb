Imports System.Configuration
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class PaymentManager

        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString
        ''
        ''
        ''
        Public Function GetListIndividualDebtors() As List(Of IndividualPaymentDTO)

            Dim baseData = GetBaseDataIndividual()
            CalculatePayments(baseData)

            Return BuildFinalList(baseData)

        End Function

        ' ========= ACCESO A DATOS =========
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

                            Dim dto As New IndividualPaymentDTO With
                                {
                                    .IdPgs = dataReader("id_pgs"),
                                    .IdCli = dataReader("id_cli"),
                                    .Name = dataReader("nom_cli").ToString(),
                                    .LastName = dataReader("ape_cli").ToString(),
                                    .Age = CalculateClientAge(dataReader.GetDateTime("fdn_cli")),'.Age = age,
                                    .MtdPgs = dataReader("mtd_pgs").ToString(),
                                    .PrcPgs = dataReader("prc_pgs"),
                                    .DscPgs = dataReader("dsc_pgs"),
                                    .FdiPgs = dataReader.GetDateTime("fdi_pgs")
                                }

                            dto.LongDate = ConvertLongDate(dto.FdiPgs)

                            listIndividualPayment.Add(dto)

                        End While

                    End Using
                End Using
            End Using

            Return listIndividualPayment

        End Function

        Private Sub CalculatePayments(items As List(Of IndividualPaymentDTO))

            For Each item In items
                CalculateIndividualPayment(item)
            Next

        End Sub

        Private Sub CalculateIndividualPayment(item As IndividualPaymentDTO)

            If item.MtdPgs.Contains("MENSUAL") Then

                CalculateProratedPayment(item)

            Else
                item.DaysOfMonth = 1
                item.Total = item.PrcPgs
                item.TotalToPay = item.PrcPgs

            End If

        End Sub

        Private Function BuildFinalList(baseData As List(Of IndividualPaymentDTO)
                                             ) As List(Of IndividualPaymentDTO)

            Dim result As New List(Of IndividualPaymentDTO)

            For Each group In baseData.GroupBy(Function(x) x.IdCli)

                ' Detalle
                result.AddRange(group)

                ' Resumen
                result.Add(CreateSummaryRow(group))

            Next

            Return result

        End Function

        Private Function CreateSummaryRow(group As IGrouping(Of Integer, IndividualPaymentDTO)
                                          ) As IndividualPaymentDTO

            Dim totalDebt = group.Sum(Function(x) x.TotalToPay)

            Return New IndividualPaymentDTO With
                {
                    .IdCli = group.Key,
                    .MtdPgs = group.First().MtdPgs,
                    .IsSummaryRow = True,
                    .NumberMonths = group.Count(),
                    .TotalAmountDebt = totalDebt,
                    .TotalToPay = totalDebt
                }

        End Function
        ''
        ''
        ''
        ' ========= GRUPOS FAMILIARES ========= 

        Public Function GetListGroupDebtors() As List(Of GroupPaymentDTO)

            Dim baseData = GetBaseDataGroup()
            ' En grupos, el pago suele ser directo (Prc - Dsc), 
            ' pero mantenemos la estructura por si necesitas lógica adicional.
            CalculateGroupPayments(baseData)

            Return BuildFinalGroupList(baseData)
        End Function

        ' ========= ACCESO A DATOS =========
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

        ' ========= REGLAS DE NEGOCIO =========
        Private Sub CalculateGroupPayments(items As List(Of GroupPaymentDTO))

            For Each item In items
                CalculateGroupPayment(item)
            Next

        End Sub

        Private Sub CalculateGroupPayment(item As GroupPaymentDTO)

            CalculateProratedPayment(item)

        End Sub
        ' ========= CONSTRUCCIÓN DE RESULTADOS =========
        ' Agrupamos por Id de Grupo Familiar
        ' 1. Añadimos los meses/pagos individuales del grupo
        ' 2. Añadimos la fila de resumen de deuda del grupo
        Private Function BuildFinalGroupList(baseData As List(Of GroupPaymentDTO)
                                             ) As List(Of GroupPaymentDTO)

            Dim result As New List(Of GroupPaymentDTO)

            For Each group In baseData.GroupBy(Function(x) x.IdGrp)

                result.AddRange(group)
                result.Add(CreateGroupSummaryRow(group))

            Next

            Return result

        End Function

        Private Function CreateGroupSummaryRow(group As IGrouping(Of Integer, GroupPaymentDTO)
                                                ) As GroupPaymentDTO

            Dim totalDebt = group.Sum(Function(x) x.TotalToPay)

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
        ''
    End Class
End Namespace