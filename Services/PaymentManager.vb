Imports System.Configuration
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class PaymentManager

        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString
        ''
        ''

        ' ========= MÉTODO PÚBLICO =========
        Public Function ObtenerListaMorososIndividuales() As List(Of IndividualPaymentDTO)

            Dim datosBase = ObtenerDatosBase()
            CalcularPagos(datosBase)

            Return ConstruirListaFinal(datosBase)

        End Function

        ' ========= ACCESO A DATOS =========
        Private Function ObtenerDatosBase() As List(Of IndividualPaymentDTO)

            Dim lista As New List(Of IndividualPaymentDTO)

            Dim sql As String =
        "SELECT c.nom_cli, c.ape_cli, c.fdn_cli,
                p.id_pgs, p.fdi_pgs, p.mtd_pgs, p.prc_pgs, p.dsc_pgs, p.id_cli
         FROM clientes c
         INNER JOIN pagos p ON c.id_cli = p.id_cli
         WHERE c.std_cli = 'ACTIVO'
           AND (p.frm_pgs IS NULL OR p.frm_pgs = '')
           AND c.id_grp IS NULL
         ORDER BY p.id_cli, p.fdi_pgs"

            Using conn As New MySqlConnection(_connectionString)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()

                    Using dr = cmd.ExecuteReader()
                        While dr.Read()

                            Dim fdn = dr.GetDateTime("fdn_cli")
                            Dim edad = CalcularEdad(fdn)

                            Dim dto As New IndividualPaymentDTO With {
                        .IdPgs = dr("id_pgs"),
                        .IdCli = dr("id_cli"),
                        .Name = dr("nom_cli").ToString(),
                        .LastName = dr("ape_cli").ToString(),
                        .Age = edad,
                        .MtdPgs = dr("mtd_pgs").ToString(),
                        .PrcPgs = dr("prc_pgs"),
                        .DscPgs = dr("dsc_pgs"),
                        .FdiPgs = dr.GetDateTime("fdi_pgs")
                    }

                            dto.Total = dto.PrcPgs - dto.DscPgs
                            dto.LongDate = FormatearMesAnio(dto.FdiPgs)

                            lista.Add(dto)
                        End While
                    End Using
                End Using
            End Using

            Return lista

        End Function


        ' ========= REGLAS DE NEGOCIO =========
        Private Sub CalcularPagos(items As List(Of IndividualPaymentDTO))

            For Each item In items
                CalcularPagoIndividual(item)
            Next

        End Sub

        Private Sub CalcularPagoIndividual(item As IndividualPaymentDTO)

            Dim total = item.PrcPgs - item.DscPgs

            If item.MtdPgs.Contains("MENSUAL") Then

                Dim diasMes = DateTime.DaysInMonth(item.FdiPgs.Year, item.FdiPgs.Month)
                Dim precioDia = total / diasMes

                item.DaysOfMonth = diasMes - item.FdiPgs.Day + 1
                item.TotalToPay = precioDia * item.DaysOfMonth

            Else
                item.DaysOfMonth = 1
                item.TotalToPay = total
            End If

        End Sub

        ' ========= CONSTRUCCIÓN DE RESULTADOS =========
        Private Function ConstruirListaFinal(datosBase As List(Of IndividualPaymentDTO)
                                             ) As List(Of IndividualPaymentDTO)

            Dim resultado As New List(Of IndividualPaymentDTO)

            For Each grupo In datosBase.GroupBy(Function(x) x.IdCli)

                ' Detalle
                resultado.AddRange(grupo)

                ' Resumen
                resultado.Add(CrearFilaResumen(grupo))

            Next

            Return resultado

        End Function

        Private Function CrearFilaResumen(grupo As IGrouping(Of Integer, IndividualPaymentDTO)
                                          ) As IndividualPaymentDTO

            Dim totalDeuda = grupo.Sum(Function(x) x.TotalToPay)

            Return New IndividualPaymentDTO With {
                .IdCli = grupo.Key,
                .MtdPgs = grupo.First().MtdPgs,
                .IsSummaryRow = True,
                .NumberMonths = grupo.Count(),
                .TotalAmountDebt = totalDeuda,
                .TotalToPay = totalDeuda
            }
            '.IdCli = grupo.Key,
            '.Nombre = primerItem.Nombre, ' <-- Añade esto para evitar nulos en el buscador
            '.Apellido = primerItem.Apellido, ' <-- Añade esto también
            '.MtdPgs = grupo.First().MtdPgs,
        End Function

        ' ========= UTILIDADES =========
        Private Function CalcularEdad(fechaNacimiento As DateTime) As Integer

            Dim edad = Date.Now.Year - fechaNacimiento.Year
            If Date.Now < fechaNacimiento.AddYears(edad) Then edad -= 1
            Return edad

        End Function

        Private Function FormatearMesAnio(fecha As DateTime) As String

            Dim culturaEs = New Globalization.CultureInfo("es-ES")
            Return fecha.ToString("d MMMM yyyy", culturaEs).ToUpper()

        End Function

        ' ========= MÉTODO PÚBLICO =========
        ' ========= GRUPOS FAMILIARES =========

        Public Function ObtenerListaMorososGrupales() As List(Of GroupPaymentDTO)

            Dim datosBase = ObtenerDatosBaseGrupal()
            ' En grupos, el pago suele ser directo (Prc - Dsc), 
            ' pero mantenemos la estructura por si necesitas lógica adicional.
            CalcularPagosGrupales(datosBase)

            Return ConstruirListaFinalGrupal(datosBase)
        End Function

        ' ========= ACCESO A DATOS =========
        Private Function ObtenerDatosBaseGrupal() As List(Of GroupPaymentDTO)
            Dim lista As New List(Of GroupPaymentDTO)

            ' Nueva consulta según tu requerimiento
            Dim sql As String = "
        SELECT GROUP_CONCAT(c.nom_cli SEPARATOR ', ') AS INTEGRANTES, 
               g.id_grp, g.nom_grp, p.fdi_pgs, p.prc_pgs, p.dsc_pgs, p.id_pgs 
        FROM clientes c 
        INNER JOIN grp_familiar g ON c.id_grp = g.id_grp 
        INNER JOIN pagos p ON g.id_grp = p.id_grp 
        WHERE p.fdp_pgs IS NULL 
        GROUP BY p.id_pgs 
        ORDER BY g.id_grp, p.fdi_pgs ASC"

            Using conn As New MySqlConnection(_connectionString)
                Using cmd As New MySqlCommand(sql, conn)
                    conn.Open()
                    Using dr = cmd.ExecuteReader()
                        While dr.Read()
                            Dim dto As New GroupPaymentDTO With {
                                .IdPgs = dr("id_pgs"),
                                .IdGrp = dr("id_grp"),
                                .GroupName = dr("nom_grp").ToString(),
                                .Members = dr("INTEGRANTES").ToString(),
                                .PrcPgs = Convert.ToDecimal(dr("prc_pgs")),
                                .DscPgs = Convert.ToDecimal(dr("dsc_pgs")),
                                .FdiPgs = dr.GetDateTime("fdi_pgs")
                            }

                            'dto.Total = dto.PrcPgs - dto.DscPgs
                            'dto.APagar = dto.Total ' Simplificado: Pago grupal es el neto
                            dto.LongDate = FormatearMesAnio(dto.FdiPgs)

                            lista.Add(dto)
                        End While
                    End Using
                End Using
            End Using

            Return lista
        End Function

        ' ========= REGLAS DE NEGOCIO =========
        ' Este es el que llamas desde ObtenerListaMorososGrupales
        Private Sub CalcularPagosGrupales(items As List(Of GroupPaymentDTO))
            For Each item In items
                ' Solo calculamos si NO es fila de resumen
                'If Not item.EsFilaResumen Then
                CalcularPagoItemGrupal(item)
                'End If
            Next
        End Sub

        ' Este hace el trabajo matemático (Rectángulo Verde y Azul de tu imagen)
        Private Sub CalcularPagoItemGrupal(item As GroupPaymentDTO)
            ' 1. TOTAL = PRECIO - DSCTO
            item.Total = item.PrcPgs - item.DscPgs

            ' 2. Nº DE DIAS = (Días del mes - día de inicio) + 1
            Dim diasTotalesDelMes = DateTime.DaysInMonth(item.FdiPgs.Year, item.FdiPgs.Month)
            item.DaysOfMonth = (diasTotalesDelMes - item.FdiPgs.Day) + 1

            ' 3. A PAGAR = TOTAL / DIAS_DEL_MES * DiasMes
            'If diasTotalesDelMes > 0 Then
            item.TotalToPay = (item.Total / diasTotalesDelMes) * item.DaysOfMonth
            'Else
            '    item.APagar = item.Total
            'End If
        End Sub

        ' ========= CONSTRUCCIÓN DE RESULTADOS =========
        Private Function ConstruirListaFinalGrupal(datosBase As List(Of GroupPaymentDTO)) As List(Of GroupPaymentDTO)
            Dim resultado As New List(Of GroupPaymentDTO)

            ' Agrupamos por Id de Grupo Familiar
            For Each grupo In datosBase.GroupBy(Function(x) x.IdGrp)
                ' 1. Añadimos los meses/pagos individuales del grupo
                resultado.AddRange(grupo)

                ' 2. Añadimos la fila de resumen de deuda del grupo
                resultado.Add(CrearFilaResumenGrupal(grupo))
            Next

            Return resultado
        End Function

        Private Function CrearFilaResumenGrupal(grupo As IGrouping(Of Integer, GroupPaymentDTO)
                                                ) As GroupPaymentDTO

            Dim totalDeuda = grupo.Sum(Function(x) x.TotalToPay)

            Return New GroupPaymentDTO With
                {
                    .IdGrp = grupo.Key,'.NombreGrupo = "RESUMEN DEUDA",
                    .IsSummaryRow = True,
                    .NumberMonths = grupo.Count(), ' <-- Esto es vital para el CellFormatting
                    .TotalToPay = grupo.Sum(Function(x) x.TotalToPay)
                }
        End Function

        ''
        ''
    End Class
End Namespace