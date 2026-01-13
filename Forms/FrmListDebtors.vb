Imports System.ComponentModel
Imports System.Configuration
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports MySql.Data.MySqlClient
Imports System.Linq

Public Class FrmListDebtors
    '
    '
    '
    Private Sub FrmListDebtors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        DgvIndividual.RowsDefaultCellStyle.Padding = New Padding(0, 2, 0, 2)
        CargarIndividuales()

        '
        DgvIndividual.RowsDefaultCellStyle.Padding = New Padding(0, 2, 0, 2)
        CargarGrupales()

    End Sub
    '
    '
    '
    Private Sub CmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFilter.SelectedIndexChanged
        '
    End Sub
    '
    '
    '
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        '
    End Sub
    '
    '
    '
    Private Sub BtnClean_Click(sender As Object, e As EventArgs) Handles BtnClean.Click
        '
    End Sub
    '
    '
    '
    Private Sub RbPayIndividual_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayIndividual.CheckedChanged
        '
    End Sub
    '
    '
    '
    Private Sub RbPayGroup_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayGroup.CheckedChanged
        '
    End Sub
    '
    '
    '
    Private Sub DgvIndividual_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvIndividual.CellContentClick
    End Sub
    Private Sub DgvIndividual_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvIndividual.CellFormatting

        Dim dgv = DirectCast(sender, DataGridView)
        If e.RowIndex < 0 Then Exit Sub

        Dim fila = TryCast(dgv.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)
        If fila Is Nothing Then Exit Sub

        ' Solo actuamos si es la fila naranja de resumen
        If fila.EsFilaResumen Then
            ' Estilo visual general para el resumen
            e.CellStyle.BackColor = Color.Orange
            e.CellStyle.SelectionBackColor = Color.Orange ' Para que no cambie de color al hacer clic
            e.CellStyle.Font = New Font(dgv.Font, FontStyle.Bold)

            Select Case dgv.Columns(e.ColumnIndex).Name

                Case "PrcPgs", "DscPgs"
                    e.Value = "" ' Limpiamos estas columnas
                    e.FormattingApplied = True

                Case "Total"
                    e.Value = "DEBE" ' Ponemos la etiqueta
                    e.FormattingApplied = True

                Case "DiasMes"
                    ' Lógica de etiquetas de tiempo
                    Dim cantidad = fila.CantidadMeses
                    If fila.MtdPgs.ToUpper().Contains("MENSUAL") Then
                        e.Value = If(cantidad = 1, "1 MES", cantidad & " MESES")
                    Else
                        e.Value = If(cantidad = 1, "1 DIA", cantidad & " DIAS")
                    End If
                    e.FormattingApplied = True

                    ' Nota: "APagar" no se incluye porque ya viene con su valor del Manager
            End Select
        End If

    End Sub
    Private Sub DgvIndividual_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DgvIndividual.DataBindingComplete

        Dim dgv = DirectCast(sender, DataGridView)

        ' Recorremos las filas una vez que los datos están cargados
        For Each row As DataGridViewRow In dgv.Rows
            Dim fila = TryCast(row.DataBoundItem, IndividualPaymentDTO)

            ' Si la fila es de resumen, le damos el protagonismo visual
            If fila IsNot Nothing AndAlso fila.EsFilaResumen Then
                ' Aquí aplicamos el tamaño más grande que usabas antes
                row.Height = 28
            Else
                ' Altura normal para las filas blancas
                row.Height = 26
            End If
        Next
    End Sub
    '
    '
    '
    Private Sub BtnPayMonth_Click(sender As Object, e As EventArgs) Handles BtnPayMonth.Click
        '
    End Sub
    '
    '
    '
    'Private Sub BtnNewMonthlyPayments_Click(sender As Object, e As EventArgs) Handles BtnNewMonthlyPayments.Click
    '    '
    '    ' 1. Mensaje de confirmación
    '    Dim respuesta As DialogResult = MessageBox.Show(
    '        "¿Desea generar los cargos de mensualidad para el mes actual? " & vbCrLf &
    '        "Esto afectará a todos los clientes y grupos activos.",
    '        "Confirmar Generación Masiva",
    '        MessageBoxButtons.YesNo,
    '        MessageBoxIcon.Question)

    '    If respuesta = DialogResult.Yes Then
    '        Try
    '            ' 2. Llamamos al método que ya tenemos en el PaymentManager
    '            Dim payMgr As New PaymentManager()
    '            payMgr.GenerarMorososMensuales()

    '            MessageBox.Show("Mensualidades generadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '            ' 3. Refrescamos el DataGridView para ver los nuevos morosos
    '            CargarIndividuales()
    '            CargarDatosGrupales()
    '            'CargarGrids()

    '        Catch ex As Exception
    '            MessageBox.Show("Hubo un error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End If
    'End Sub
    '
    '
    '
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        '
    End Sub
    '
    '
    '
    Private Sub CargarIndividuales()
        Try
            Dim payMgr As New PaymentManager()

            DgvIndividual.AutoGenerateColumns = False ' Evita que se añadan columnas extra

            ' Usamos la declaración explícita para mayor claridad
            Dim listaIndiv As List(Of IndividualPaymentDTO) = payMgr.ObtenerListaMorososIndividuales()

            DgvIndividual.DataSource = Nothing
            DgvIndividual.DataSource = listaIndiv ' Carga tus datos en tu diseño
            ' Esto respeta tu diseño pero asegura el formato profesional
            DgvIndividual.Columns("PrcPgs").DefaultCellStyle.Format = "C2"
            DgvIndividual.Columns("DscPgs").DefaultCellStyle.Format = "C2"
            DgvIndividual.Columns("Total").DefaultCellStyle.Format = "C2"
            DgvIndividual.Columns("APagar").DefaultCellStyle.Format = "C2"

            'DataGridView1.Columns("Total").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            ' El bucle para contar (Opción sin errores de Count)
            Dim numMorosos As Integer = 0
            For Each item As IndividualPaymentDTO In listaIndiv
                If Not item.EsFilaResumen Then
                    numMorosos += 1
                End If
            Next

            LblSolitos.Text = numMorosos & " - Registros pendientes de pago."

        Catch ex As Exception
            MsgBox("Error al cargar individuales: " & ex.Message)
        End Try
    End Sub
    '
    '
    ' En FrmListaMorosos
    Private Sub CargarGrupales()
        Try
            Dim payMgr As New PaymentManager()
            Dim listaGrup As List(Of GroupPaymentDTO) = payMgr.ObtenerListaGrupalesCompacta()

            With DgvFamilyGroup

                .AutoGenerateColumns = False ' Evita que se añadan columnas extra
                ' 1. Limpiamos y asignamos
                .DataSource = Nothing
                .DataSource = listaGrup

                ' 2. IMPORTANTE: Solo formateamos si hay datos y las columnas existen
                'If .Columns.Contains("APagarProrrateo") Then
                '    .Columns("APagarProrrateo").DefaultCellStyle.Format = "C2"
                '    .Columns("APagarProrrateo").HeaderText = "A PAGAR"
                'End If

                'If DgvPrueba.Columns.Contains("PrcPgs") Then
                '    .Columns("PrcPgs").DefaultCellStyle.Format = "C2"
                '    .Columns("PrcPgs").HeaderText = "PRECIO"
                'End If

                ' Contamos solo las filas donde NomGrp no está vacío (que es la cabecera de cada pago real)
                ' Usamos .Where para filtrar y luego .Count para contar el resultado
                ' LINQ: Contamos solo las filas que NO son resumen
                Dim totalPagos = listaGrup.Where(Function(x) x.EsFilaResumen = False).Count()
                LblToditos.Text = totalPagos & " - Pagos pendientes."

                .DataSource = Nothing
                .DataSource = listaGrup
            End With


        Catch ex As Exception
            MsgBox("Error al cargar grupales: " & ex.Message)
        End Try
    End Sub

    Private Sub DgvPrueba_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPrueba.CellContentClick

    End Sub

    Private Sub DgvPrueba_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvPrueba.CellFormatting



    End Sub

    Private Sub DgvFamilyGroup_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFamilyGroup.CellContentClick

    End Sub

    Private Sub DgvFamilyGroup_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvFamilyGroup.CellFormatting

        Dim dgv = DirectCast(sender, DataGridView)
        If e.RowIndex < 0 OrElse dgv.Rows(e.RowIndex).DataBoundItem Is Nothing Then Return

        Dim filaObj = DirectCast(dgv.Rows(e.RowIndex).DataBoundItem, GroupPaymentDTO)
        Dim colName As String = dgv.Columns(e.ColumnIndex).Name

        ' --- LÓGICA PARA LA FILA NARANJA (RESUMEN) ---
        If filaObj.EsFilaResumen Then
            e.CellStyle.BackColor = Color.FromArgb(255, 192, 128)
            e.CellStyle.ForeColor = Color.Red
            e.CellStyle.Font = New Font(dgv.Font, FontStyle.Bold)

            ' AQUÍ ESTÁ EL "CASE": Decide qué mostrar en cada columna
            Select Case colName
                Case "total_pgs_gf" ' <--- Nombre (Name) de la columna TOTAL
                    e.Value = filaObj.TextoColTotal ' Muestra "DEBE :"
                    e.FormattingApplied = True

                Case "dias_mes_gf" ' <--- Nombre (Name) de la columna Nº DE DIAS
                    e.Value = filaObj.TextoColDias ' Muestra "X MESES"
                    e.FormattingApplied = True

                Case "a_pagar_gf" ' <--- Nombre (Name) de la columna A PAGAR
                    ' Aquí no tocamos e.Value para que se vea el monto acumulado
                    e.FormattingApplied = False

                Case Else
                    ' Todo lo demás (Integrantes, Grupo, Precio) se limpia
                    e.Value = ""
                    e.FormattingApplied = True
            End Select

            ' --- LÓGICA PARA LAS FILAS NORMALES (BLANCAS) ---
        Else
            ' Aseguramos que las columnas de moneda se vean bien
            If colName = "total_pgs_gf" Or colName = "a_pagar_gf" Then
                e.CellStyle.Format = "N2"
            End If
        End If

    End Sub

    'Private Sub DgvFamilyGroup_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles DgvFamilyGroup.CellPainting
    '    ' 1. Validaciones básicas
    '    If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return
    '    Dim dgv = DirectCast(sender, DataGridView)

    '    If dgv.Rows(e.RowIndex).DataBoundItem Is Nothing Then Return
    '    Dim filaObj = DirectCast(dgv.Rows(e.RowIndex).DataBoundItem, GroupPaymentDTO)

    '    ' 2. SOLO PARA LA FILA RESUMEN (NARANJA)
    '    If filaObj.EsFilaResumen Then
    '        ' --- CORRECCIÓN DEL ERROR DE SINTAXIS ---
    '        ' En VB.NET se usa "And Not" en lugar de "& ~"
    '        e.Paint(e.ClipBounds, DataGridViewPaintParts.All And Not DataGridViewPaintParts.ContentForeground)

    '        Dim colName As String = dgv.Columns(e.ColumnIndex).Name
    '        Dim textoADibujar As String = ""

    '        ' 3. DETERMINAR QUÉ TEXTO PINTAR
    '        Select Case colName
    '            Case "TotalSinProrrateo"
    '                textoADibujar = "DEBE :"
    '            Case "DiasDelMes"
    '                textoADibujar = filaObj.TextoAuxiliar ' Ejemplo: "2 MESES"
    '            Case "APagarProrrateo"
    '                ' Para el monto total, dejamos que el sistema lo pinte normal
    '                e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentForeground)
    '                e.Handled = True
    '                Return
    '        End Select

    '        ' 4. DIBUJAR EL TEXTO MANUALMENTE
    '        If Not String.IsNullOrEmpty(textoADibujar) Then
    '            ' Configuramos la fuente y el color rojo
    '            Using fontNegrita As New Font(dgv.Font, FontStyle.Bold)
    '                ' Alineamos a la derecha y centrado verticalmente
    '                Dim flags As TextFormatFlags = TextFormatFlags.Right Or TextFormatFlags.VerticalCenter
    '                TextRenderer.DrawText(e.Graphics, textoADibujar, fontNegrita, e.CellBounds, Color.Red, flags)
    '            End Using
    '        End If

    '        e.Handled = True ' Confirmamos que ya pintamos la celda
    '    End If
    'End Sub
End Class