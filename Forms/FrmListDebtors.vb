Imports System.ComponentModel
Imports System.Configuration
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports MySql.Data.MySqlClient

Public Class FrmListDebtors
    '
    '
    '
    Private Sub FrmListDebtors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '
        DgvIndividual.RowsDefaultCellStyle.Padding = New Padding(0, 2, 0, 2)
        CargarIndividuales()

        '
        CargarDatosGrupales()

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

        Dim fila = TryCast(dgv.Rows(e.RowIndex).DataBoundItem, PaymentDTO)
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
    '
    '
    '
    Private Sub BtnPayMonth_Click(sender As Object, e As EventArgs) Handles BtnPayMonth.Click
        '
    End Sub
    '
    '
    '
    Private Sub BtnNewMonthlyPayments_Click(sender As Object, e As EventArgs) Handles BtnNewMonthlyPayments.Click
        '
        ' 1. Mensaje de confirmación
        Dim respuesta As DialogResult = MessageBox.Show(
            "¿Desea generar los cargos de mensualidad para el mes actual? " & vbCrLf &
            "Esto afectará a todos los clientes y grupos activos.",
            "Confirmar Generación Masiva",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If respuesta = DialogResult.Yes Then
            Try
                ' 2. Llamamos al método que ya tenemos en el PaymentManager
                Dim payMgr As New PaymentManager()
                payMgr.GenerarMorososMensuales()

                MessageBox.Show("Mensualidades generadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' 3. Refrescamos el DataGridView para ver los nuevos morosos
                CargarIndividuales()
                CargarDatosGrupales()
                'CargarGrids()

            Catch ex As Exception
                MessageBox.Show("Hubo un error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
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
            Dim listaIndiv As List(Of PaymentDTO) = payMgr.ObtenerListaMorososIndividuales()

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
            For Each item As PaymentDTO In listaIndiv
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
    Private Sub CargarDatosGrupales()
        Try
            Dim payMgr As New PaymentManager()

            ' Usamos la declaración explícita que es más clara
            Dim listaGrupales As List(Of PaymentDTO) = payMgr.ObtenerListaMorososGrupales()

            DataGridView2.DataSource = Nothing
            DataGridView2.DataSource = listaGrupales

            ' Lógica de conteo manual para evitar el error de indización
            Dim numIntegrantesConDeuda As Integer = 0

            For Each fila As PaymentDTO In listaGrupales
                ' Solo contamos a las personas, no las filas naranjas de resumen
                ' Nota: En grupos, un solo integrante puede aparecer en varios meses de deuda
                If Not fila.EsFilaResumen Then
                    ' Si quieres contar cuántas líneas de cobro hay:
                    numIntegrantesConDeuda += 1
                End If
            Next

            ' Actualizamos el Label con el resultado
            LblToditos.Text = numIntegrantesConDeuda & " Registros de integrantes con deuda."

        Catch ex As Exception
            MessageBox.Show("Error al cargar datos grupales: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DgvIndividual_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DgvIndividual.DataBindingComplete

        Dim dgv = DirectCast(sender, DataGridView)

        ' Recorremos las filas una vez que los datos están cargados
        For Each row As DataGridViewRow In dgv.Rows
            Dim fila = TryCast(row.DataBoundItem, PaymentDTO)

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

End Class