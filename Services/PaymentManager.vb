Imports System.Configuration
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class PaymentManager

        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

        Public Sub GenerarMorososMensuales()

            Using connectionString As New MySqlConnection(_connectionString)

                connectionString.Open()
                ' Iniciamos una transacción para asegurar que se graben ambos o ninguno
                Using transaction = connectionString.BeginTransaction()

                    Try
                        ' 1. Generar deudas para clientes individuales
                        Dim sqlIndiv As String = "
                        INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
                        SELECT CURDATE(), '0101-01-01', '', '', t.prcio_trfa, t.dscto_trfa, c.id_cli, NULL, @userId
                        FROM clientes c
                        INNER JOIN trfa_dscto t ON (TIMESTAMPDIFF(YEAR, c.fdn_cli, CURDATE()) BETWEEN t.emin_trfa AND t.emax_trfa)
                        WHERE c.std_cli = 'SI' AND c.id_grp IS NULL
                        AND NOT EXISTS (
                            SELECT 1 FROM pagos p 
                            WHERE p.id_cli = c.id_cli 
                            AND MONTH(p.fdi_pgs) = MONTH(CURDATE()) 
                            AND YEAR(p.fdi_pgs) = YEAR(CURDATE())
                        );"

                        ' 2. Generar deudas para grupos familiares (un solo pago por grupo)
                        Dim sqlGrupos As String = "
                        INSERT INTO pagos (fdi_pgs, fdp_pgs, frm_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
                        SELECT CURDATE(), '0101-01-01', '', '', t.prcio_trfa, t.dscto_trfa, NULL, g.id_grp, @userId
                        FROM grp_familiar g
                        INNER JOIN trfa_dscto t ON (g.num_intgrntes_grp = t.nperson_trfa)
                        WHERE NOT EXISTS (
                            SELECT 1 FROM pagos p 
                            WHERE p.id_grp = g.id_grp 
                            AND MONTH(p.fdi_pgs) = MONTH(CURDATE()) 
                            AND YEAR(p.fdi_pgs) = YEAR(CURDATE())
                        );"

                        Using sqlCommand As New MySqlCommand(sqlIndiv, connectionString, transaction)

                            sqlCommand.Parameters.AddWithValue("@userId", UserSession.IdUser)
                            sqlCommand.ExecuteNonQuery() ' Ejecuta individuales

                            sqlCommand.CommandText = sqlGrupos
                            sqlCommand.ExecuteNonQuery() ' Ejecuta grupos
                        End Using


                        transaction.Commit()

                    Catch ex As Exception

                        transaction.Rollback()
                        Throw New Exception("Error al generar morosos: " & ex.Message)

                    End Try

                End Using

            End Using

        End Sub

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
        Public Function ObtenerPagosPendientes(soloIndividuales As Boolean) As List(Of PaymentDTO)
            Dim lista As New List(Of PaymentDTO)

            ' Añadimos p.mtd_pgs y p.fdi_pgs a la consulta
            Dim sql As String = "
    SELECT 
        c.nom_cli, c.ape_cli, 
        CONCAT(TIMESTAMPDIFF(YEAR, c.fdn_cli, CURDATE()), ' años') as edad,
        DATE_FORMAT(p.fdi_pgs, '%M %Y') as mes_anio,
        p.fdi_pgs, p.mtd_pgs,
        p.prc_pgs, p.dsc_pgs,
        (p.prc_pgs - p.dsc_pgs) as total,
        p.id_pgs, p.id_cli
    FROM pagos p
    INNER JOIN clientes c ON p.id_cli = c.id_cli
    WHERE p.fdp_pgs = '0000-00-00' OR p.fdp_pgs IS NULL" 'WHERE p.fdp_pgs = '0101-01-01' "

            If soloIndividuales Then
                sql &= " AND c.id_grp IS NULL"
            Else
                sql &= " AND c.id_grp IS NOT NULL"
            End If

            Using conn As New MySqlConnection(_connectionString)
                Dim cmd As New MySqlCommand(sql, conn)
                conn.Open()
                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        ' El mapeo ahora es 1 a 1 con el SQL y el DTO
                        lista.Add(New PaymentDTO With {
                    .Nombre = dr("nom_cli").ToString(),
                    .Apellido = dr("ape_cli").ToString(),
                    .Edad = dr("edad").ToString(),
                    .MesAnio = dr("mes_anio").ToString(),
                    .FdiPgs = dr.GetDateTime("fdi_pgs"), ' Para calcular días
                    .MtdPgs = dr("mtd_pgs").ToString(), ' Para la regla de "ASISTENCIA"
                    .PrcPgs = dr("prc_pgs"),
                    .DscPgs = dr("dsc_pgs"),
                    .Total = dr("total"),
                    .IdPgs = dr("id_pgs"),
                    .IdCli = dr("id_cli")
                })
                    End While
                End Using
            End Using
            Return lista
        End Function



        Public Function ObtenerMorososGrupales() As List(Of PaymentDTO)
            Dim lista As New List(Of PaymentDTO)
            ' Usamos CHAR(10) que es el salto de línea (Line Feed) compatible con Windows/VB
            Dim sql As String = "
        SELECT 
            GROUP_CONCAT(c.nom_cli SEPARATOR CHAR(10)) as integrantes,
            g.nom_grp,
            p.fdi_pgs as fecha_deuda,
            p.prc_pgs, 
            p.dsc_pgs,
            (p.prc_pgs - p.dsc_pgs) as total_mes,
            p.id_pgs
        FROM pagos p
        INNER JOIN grp_familiar g ON p.id_grp = g.id_grp
        INNER JOIN clientes c ON g.id_grp = c.id_grp
        WHERE p.fdp_pgs = '0101-01-01'
        GROUP BY p.id_pgs, g.nom_grp, p.fdi_pgs
        ORDER BY p.fdi_pgs ASC"

            Using conn As New MySqlConnection(_connectionString)
                Dim cmd As New MySqlCommand(sql, conn)
                conn.Open()
                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        lista.Add(New PaymentDTO With {
                            .Integrantes = dr("integrantes").ToString(),
                            .NombreGrupo = dr("nom_grp").ToString(),
                            .MesAnio = Convert.ToDateTime(dr("fecha_deuda")).ToString("MMMM yyyy"),
                            .PrcPgs = dr("prc_pgs"),
                            .DscPgs = dr("dsc_pgs"),
                            .Total = dr("total_mes"),
                            .IdPgs = dr("id_pgs")
                        })
                    End While
                End Using
            End Using
            Return lista
        End Function

        Public Function ObtenerListaConTotales(soloIndividuales As Boolean) As List(Of PaymentDTO)

            ' 1. Traer los datos limpios de la BD
            Dim datosBase = ObtenerPagosPendientes(soloIndividuales)
            Dim listaProcesada As New List(Of PaymentDTO)

            ' 2. Agrupar por Cliente o por Grupo
            Dim grupos = datosBase.GroupBy(Function(p) If(soloIndividuales, p.IdCli.ToString(), p.IdGrp.ToString()))

            For Each g In grupos
                Dim totalAcumulado As Decimal = 0
                Dim contadorMeses As Integer = 0

                ' Añadir las filas de los meses individuales
                For Each pago In g
                    listaProcesada.Add(pago)
                    totalAcumulado += pago.Total
                    contadorMeses += 1
                Next

                ' 3. Insertar la "Fila Naranja" de resumen
                listaProcesada.Add(New PaymentDTO With {
                    .EsFilaResumen = True,
                    .MontoResumen = totalAcumulado
                })
            Next

            Return listaProcesada

        End Function

        Public Function ObtenerListaMorososIndividuales() As List(Of PaymentDTO)
            Dim listaFinal As New List(Of PaymentDTO)
            Dim datosBase As New List(Of PaymentDTO)

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
                        Dim dto As New PaymentDTO()
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

                Dim resumen As New PaymentDTO With {
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

        Public Function ObtenerListaMorososGrupales() As List(Of PaymentDTO)
            Dim listaFinal As New List(Of PaymentDTO)
            Dim datosBase As New List(Of PaymentDTO)

            ' 1. Carga desde la Vista que ya comprobamos que funciona
            Using conn As New MySqlConnection(_connectionString)
                Dim cmd As New MySqlCommand("SELECT * FROM vista_morosos_grupales", conn)
                conn.Open()
                Using dr = cmd.ExecuteReader()
                    While dr.Read()
                        datosBase.Add(New PaymentDTO With {
                            .Nombre = dr.GetString("nom_cli"),
                            .NombreGrupo = dr.GetString("nom_grp"),
                            .FdiPgs = dr.GetDateTime("fdi_pgs"),
                            .PrcPgs = dr.GetDecimal("prc_pgs"),
                            .DscPgs = dr.GetDecimal("dsc_pgs"),
                            .IdPgs = dr.GetInt32("id_pgs"),
                            .IdGrp = dr.GetInt32("id_grp")
                        })
                    End While
                End Using
            End Using

            ' 2. Agrupamiento por Familia (ID_GRP)
            Dim familias = datosBase.GroupBy(Function(x) x.IdGrp)

            For Each familia In familias
                Dim totalAcumuladoFamilia As Decimal = 0
                Dim contadorMeses As Integer = 0

                ' 3. Agrupamiento por Pago (ID_PGS) para juntar integrantes
                Dim pagosMensuales = familia.GroupBy(Function(x) x.IdPgs)

                For Each mes In pagosMensuales
                    Dim info = mes.First()
                    ' Juntamos los nombres (JINKIS, SARITA, MARJORIE...) en una sola celda
                    Dim integrantesStr = String.Join(Environment.NewLine, mes.Select(Function(m) m.Nombre))
                    Dim liquidoMes = info.PrcPgs - info.DscPgs

                    listaFinal.Add(New PaymentDTO With {
                        .Integrantes = integrantesStr,
                        .NombreGrupo = info.NombreGrupo,
                        .MesAnio = info.FdiPgs.ToString("MMMM yyyy").ToUpper(),
                        .PrcPgs = info.PrcPgs,
                        .DscPgs = info.DscPgs,
                        .Total = liquidoMes,
                        .EsFilaResumen = False
                    })

                    totalAcumuladoFamilia += liquidoMes
                    contadorMeses += 1
                Next

                ' 4. Añadimos la FILA NARANJA de cierre para esta familia
                listaFinal.Add(New PaymentDTO With {
                    .EsFilaResumen = True,
                    .SumaTotalDeuda = totalAcumuladoFamilia
                })
            Next

            Return listaFinal
        End Function


    End Class
End Namespace