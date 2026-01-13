Imports System.Configuration
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class PaymentManager

        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

        'Public Sub GenerarMorososMensuales()

        '    Using connectionString As New MySqlConnection(_connectionString)

        '        connectionString.Open()
        '        ' Iniciamos una transacción para asegurar que se graben ambos o ninguno
        '        Using transaction = connectionString.BeginTransaction()

        '            Try
        '                ' 1. Generar deudas para clientes individuales
        '                Dim sqlIndiv As String = "
        '                INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
        '                SELECT CURDATE(), '0101-01-01', '', '', t.prcio_trfa, t.dscto_trfa, c.id_cli, NULL, @userId
        '                FROM clientes c
        '                INNER JOIN trfa_dscto t ON (TIMESTAMPDIFF(YEAR, c.fdn_cli, CURDATE()) BETWEEN t.emin_trfa AND t.emax_trfa)
        '                WHERE c.std_cli = 'SI' AND c.id_grp IS NULL
        '                AND NOT EXISTS (
        '                    SELECT 1 FROM pagos p 
        '                    WHERE p.id_cli = c.id_cli 
        '                    AND MONTH(p.fdi_pgs) = MONTH(CURDATE()) 
        '                    AND YEAR(p.fdi_pgs) = YEAR(CURDATE())
        '                );"

        '                ' 2. Generar deudas para grupos familiares (un solo pago por grupo)
        '                Dim sqlGrupos As String = "
        '                INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
        '                SELECT CURDATE(), '0101-01-01', '', '', t.prcio_trfa, t.dscto_trfa, NULL, g.id_grp, @userId
        '                FROM grp_familiar g
        '                INNER JOIN trfa_dscto t ON (g.num_intgrntes_grp = t.nperson_trfa)
        '                WHERE NOT EXISTS (
        '                    SELECT 1 FROM pagos p 
        '                    WHERE p.id_grp = g.id_grp 
        '                    AND MONTH(p.fdi_pgs) = MONTH(CURDATE()) 
        '                    AND YEAR(p.fdi_pgs) = YEAR(CURDATE())
        '                );"

        '                Using sqlCommand As New MySqlCommand(sqlIndiv, connectionString, transaction)

        '                    sqlCommand.Parameters.AddWithValue("@userId", UserSession.IdUser)
        '                    sqlCommand.ExecuteNonQuery() ' Ejecuta individuales

        '                    sqlCommand.CommandText = sqlGrupos
        '                    sqlCommand.ExecuteNonQuery() ' Ejecuta grupos
        '                End Using


        '                transaction.Commit()

        '            Catch ex As Exception

        '                transaction.Rollback()
        '                Throw New Exception("Error al generar morosos: " & ex.Message)

        '            End Try

        '        End Using

        '    End Using

        'End Sub

        'Public Function ObtenerPagosPendientes(soloIndividuales As Boolean) As List(Of PaymentDTO)

        '    Dim lista As New List(Of PaymentDTO)

        '    ' Consulta SQL que une Pagos con Clientes y traduce fechas a nombres de meses
        '    Dim sql As String = "
        'SELECT 
        '    c.nom_cli, c.ape_cli, 
        '    CONCAT(TIMESTAMPDIFF(YEAR, c.fdn_cli, CURDATE()), ' años') as edad,
        '    DATE_FORMAT(p.fdi_pgs, '%M %Y') as mes_anio,
        '    p.prc_pgs, p.dsc_pgs,
        '    (p.prc_pgs - p.dsc_pgs) as total,
        '    p.id_pgs, p.id_cli
        'FROM pagos p
        'INNER JOIN clientes c ON p.id_cli = c.id_cli
        'WHERE p.fdp_pgs = '0101-01-01' " ' Solo los no pagados

        '    ' Filtramos según la pestaña
        '    If soloIndividuales Then
        '        sql &= " AND c.id_grp IS NULL"
        '    Else
        '        sql &= " AND c.id_grp IS NOT NULL"
        '    End If

        '    Using conn As New MySqlConnection(_connectionString)

        '        Dim cmd As New MySqlCommand(sql, conn)
        '        conn.Open()

        '        Using dr = cmd.ExecuteReader()

        '            While dr.Read()

        '                lista.Add(New PaymentDTO With
        '                          {
        '                            .Nombre = dr("nom_cli").ToString(),
        '                            .Apellido = dr("ape_cli").ToString(),
        '                            .Edad = dr("edad").ToString(),
        '                            .MesAnio = dr("mes_anio").ToString(),
        '                            .PrcPgs = dr("prc_pgs"),
        '                            .DscPgs = dr("dsc_pgs"),
        '                            .Total = dr("total"),
        '                            .IdPgs = dr("id_pgs")
        '                        })
        '            End While

        '        End Using

        '    End Using

        '    Return lista

        'End Function
        '    Public Function ObtenerPagosPendientes(soloIndividuales As Boolean) As List(Of PaymentDTO)
        '        Dim lista As New List(Of PaymentDTO)

        '        ' Añadimos p.mtd_pgs y p.fdi_pgs a la consulta
        '        Dim sql As String = "
        'SELECT 
        '    c.nom_cli, c.ape_cli, 
        '    CONCAT(TIMESTAMPDIFF(YEAR, c.fdn_cli, CURDATE()), ' años') as edad,
        '    DATE_FORMAT(p.fdi_pgs, '%M %Y') as mes_anio,
        '    p.fdi_pgs, p.mtd_pgs,
        '    p.prc_pgs, p.dsc_pgs,
        '    (p.prc_pgs - p.dsc_pgs) as total,
        '    p.id_pgs, p.id_cli
        'FROM pagos p
        'INNER JOIN clientes c ON p.id_cli = c.id_cli
        'WHERE p.fdp_pgs = '0000-00-00' OR p.fdp_pgs IS NULL" 'WHERE p.fdp_pgs = '0101-01-01' "

        '        If soloIndividuales Then
        '            sql &= " AND c.id_grp IS NULL"
        '        Else
        '            sql &= " AND c.id_grp IS NOT NULL"
        '        End If

        '        Using conn As New MySqlConnection(_connectionString)
        '            Dim cmd As New MySqlCommand(sql, conn)
        '            conn.Open()
        '            Using dr = cmd.ExecuteReader()
        '                While dr.Read()
        '                    ' El mapeo ahora es 1 a 1 con el SQL y el DTO
        '                    lista.Add(New PaymentDTO With {
        '                .Nombre = dr("nom_cli").ToString(),
        '                .Apellido = dr("ape_cli").ToString(),
        '                .Edad = dr("edad").ToString(),
        '                .MesAnio = dr("mes_anio").ToString(),
        '                .FdiPgs = dr.GetDateTime("fdi_pgs"), ' Para calcular días
        '                .MtdPgs = dr("mtd_pgs").ToString(), ' Para la regla de "ASISTENCIA"
        '                .PrcPgs = dr("prc_pgs"),
        '                .DscPgs = dr("dsc_pgs"),
        '                .Total = dr("total"),
        '                .IdPgs = dr("id_pgs"),
        '                .IdCli = dr("id_cli")
        '            })
        '                End While
        '            End Using
        '        End Using
        '        Return lista
        '    End Function

        Public Function ObtenerListaMorososIndividuales() As List(Of IndividualPaymentDTO)
            Dim listaFinal As New List(Of IndividualPaymentDTO)
            Dim datosBase As New List(Of IndividualPaymentDTO)

            ' 1. CONSULTA SQL (Tu lógica original de ACTIVO y frm_pgs vacía)
            Dim sql As String = “SELECT
                                    c.nom_cli,
                                    c.ape_cli,
                                    c.fdn_cli,
                                    p.id_pgs,
                                    p.fdi_pgs,
                                    p.mtd_pgs,
                                    p.prc_pgs,
                                    p.dsc_pgs,
                                    p.id_cli
                                    FROM clientes c INNER JOIN pagos p ON c.id_cli = p.id_cli
                                    WHERE c.std_cli = 'ACTIVO'
                                    AND p.frm_pgs = ''
                                    AND c.id_grp IS NULL
                                    ORDER BY p.id_cli, p.fdi_pgs”


            ' 2. CONEXIÓN Y CARGA DE DATOS
            Using conn As New MySqlConnection(_connectionString)
                Dim cmd As New MySqlCommand(sql, conn)
                conn.Open()
                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        ' Variables temporales para calcular
                        Dim fdn = dr.GetDateTime("fdn_cli")
                        Dim fdi = dr.GetDateTime("fdi_pgs")
                        Dim mtd = dr("mtd_pgs").ToString().ToUpper()

                        ' Cálculo de EDAD EXACTA en VB
                        Dim edad As Integer = Now.Year - fdn.Year
                        If Now < fdn.AddYears(edad) Then edad -= 1

                        ' Creamos el objeto y lo añadimos a la lista base
                        Dim dto As New IndividualPaymentDTO()
                        dto.IdPgs = dr("id_pgs")
                        dto.IdCli = dr("id_cli")
                        dto.Nombre = dr("nom_cli").ToString()
                        dto.Apellido = dr("ape_cli").ToString()
                        dto.Edad = edad & " años"
                        dto.MtdPgs = dr("mtd_pgs").ToString()
                        dto.PrcPgs = dr("prc_pgs")
                        dto.DscPgs = dr("dsc_pgs")
                        dto.Total = dto.PrcPgs - dto.DscPgs
                        dto.FdiPgs = fdi
                        dto.MesAnio = fdi.ToString("d MMMM yyyy").ToUpper() ' Mes en ESPAÑOL
                        dto.Edad = edad & " años"

                        datosBase.Add(dto)
                    End While
                End Using
            End Using

            ' 3. AGRUPACIÓN PARA LAS FILAS NARANJAS
            For Each grupo In datosBase.GroupBy(Function(p) p.IdCli)

                For Each item In grupo
                    ' --- TU CÓDIGO ANTIGUO INTEGRADO AQUÍ ---
                    Dim totalDiferencia As Decimal = item.PrcPgs - item.DscPgs

                    If item.MtdPgs.ToUpper().Contains("MENSUAL") Then
                        ' 1. Días totales del mes
                        Dim diasTotalesMes As Integer = DateTime.DaysInMonth(item.FdiPgs.Year, item.FdiPgs.Month)

                        ' 2. Lógica de proporcionalidad
                        Dim prcDia As Decimal = totalDiferencia / diasTotalesMes
                        Dim diaInicio As Integer = item.FdiPgs.Day ' El día que empezó

                        item.DiasMes = diasTotalesMes - diaInicio + 1 ' Días restantes
                        item.APagar = prcDia * item.DiasMes ' Precio proporcional
                    Else
                        ' 3. Caso DIARIO
                        item.DiasMes = 1
                        item.APagar = totalDiferencia
                    End If
                    ' ---------------------------------------

                    listaFinal.Add(item)
                Next

                ' 4. Fila de resumen naranja
                Dim totalDeudaGrupo As Decimal = grupo.Sum(Function(x) x.APagar) ' Sumamos los proporcionales ya calculados

                Dim resumen As New IndividualPaymentDTO With {
        .IdCli = grupo.Key,
        .MtdPgs = grupo.First().MtdPgs,
        .EsFilaResumen = True,
        .CantidadMeses = grupo.Count(),
        .SumaTotalDeuda = totalDeudaGrupo, ' Para control interno
        .APagar = totalDeudaGrupo          ' ESTO es lo que hace que aparezca en la columna del Grid
    }
                listaFinal.Add(resumen)

            Next

            Return listaFinal
        End Function

        Public Function ObtenerListaGrupalesCompacta() As List(Of GroupPaymentDTO)
            Dim listaFinal As New List(Of GroupPaymentDTO)
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

            ' SQL con GROUP_CONCAT para evitar filas repetidas por integrante
            ' Ordenamos por id_grp para que los pagos del mismo grupo salgan juntos
            Dim sql As String = "SELECT g.id_grp, g.nom_grp, GROUP_CONCAT(c.nom_cli SEPARATOR ', ') as miembros, " &
                        "p.id_pgs, p.fdi_pgs, p.prc_pgs, p.dsc_pgs " &
                        "FROM pagos p " &
                        "INNER JOIN grp_familiar g ON p.id_grp = g.id_grp " &
                        "INNER JOIN clientes c ON g.id_grp = c.id_grp " &
                        "WHERE p.fdp_pgs IS NULL " &
                        "GROUP BY p.id_pgs " &
                        "ORDER BY g.id_grp, p.fdi_pgs ASC"

            Using conn As New MySqlConnection(connectionString)
                conn.Open()
                Dim cmd As New MySqlCommand(sql, conn)
                Dim dr As MySqlDataReader = cmd.ExecuteReader()

                Dim ultimoIdGrp As Integer = -1
                Dim acumuladoDinero As Decimal = 0
                Dim contadorMeses As Integer = 0
                While dr.Read()
                    Dim idGrpActual As Integer = dr.GetInt32("id_grp")

                    ' --- DETECCIÓN DE CAMBIO DE GRUPO ---
                    If ultimoIdGrp <> -1 AndAlso ultimoIdGrp <> idGrpActual Then
                        InsertarFilaResumen(listaFinal, acumuladoDinero, contadorMeses)
                        acumuladoDinero = 0
                        contadorMeses = 0
                    End If

                    Dim dto As New GroupPaymentDTO()
                    Dim fInicio As DateTime = dr.GetDateTime("fdi_pgs")
                    Dim diasTotalesMes As Integer = DateTime.DaysInMonth(fInicio.Year, fInicio.Month)

                    ' Datos básicos
                    dto.IdPgs = dr.GetInt32("id_pgs")
                    dto.IdGrp = idGrpActual
                    dto.NomGrp = dr.GetString("nom_grp").ToUpper()
                    dto.Integrantes = dr.GetString("miembros").ToUpper()
                    dto.FechaLarga = fInicio.ToString("d 'DE' MMMM 'DE' yyyy").ToUpper()
                    dto.PrcPgs = dr.GetDecimal("prc_pgs")
                    dto.DscPgs = dr.GetDecimal("dsc_pgs")
                    ' --- AQUÍ COLOCAS EL PRIMER BLOQUE ---
                    ' Esto llena las columnas "TOTAL" y "Nº DE DIAS" para las filas blancas
                    dto.TextoColTotal = (dto.PrcPgs - dto.DscPgs).ToString("N2")
                    dto.TextoColDias = ((diasTotalesMes - fInicio.Day) + 1).ToString()
                    '
                    dto.DiasDelMes = (diasTotalesMes - fInicio.Day) + 1
                    dto.TotalSinProrrateo = dto.PrcPgs - dto.DscPgs
                    dto.APagarProrrateo = Math.Round((dto.TotalSinProrrateo / diasTotalesMes) * dto.DiasDelMes, 2)

                    listaFinal.Add(dto)

                    ' Acumuladores
                    acumuladoDinero += dto.APagarProrrateo
                    contadorMeses += 1
                    ultimoIdGrp = idGrpActual
                End While

                ' --- LA FILA QUE FALTABA PARA MOSTRAR EL RESUMEN ---
                If ultimoIdGrp <> -1 Then
                    InsertarFilaResumen(listaFinal, acumuladoDinero, contadorMeses)
                End If

            End Using
            Return listaFinal
        End Function

        ' Sub-función que ahora sí recibe los 3 argumentos correctamente
        Private Sub InsertarFilaResumen(ByRef lista As List(Of GroupPaymentDTO), total As Decimal, meses As Integer)
            'Dim resumen As New GroupPaymentDTO()
            'resumen.EsFilaResumen = True

            '' Obligamos a que estas propiedades no tengan valores que el DGV quiera formatear como moneda
            'resumen.PrcPgs = 0
            'resumen.DscPgs = 0
            'resumen.TotalSinProrrateo = 0
            'resumen.DiasDelMes = 0

            '' El valor real va aquí
            'resumen.APagarProrrateo = total

            '' Guardamos el texto en la propiedad auxiliar
            'resumen.TextoAuxiliar = meses & IIf(meses = 1, " MES", " MESES")

            'lista.Add(resumen)
            Dim resumen As New GroupPaymentDTO()
            resumen.EsFilaResumen = True
            resumen.APagarProrrateo = total

            ' Inyectamos los textos aquí directamente
            resumen.TextoColTotal = "DEBE :"
            resumen.TextoColDias = meses & IIf(meses = 1, " MES", " MESES")

            lista.Add(resumen)
        End Sub

    End Class
End Namespace