Imports GymPaymentControl.Models
Imports GymPaymentControl.Services

Public Class FrmListDebtors
    '
    Private ReadOnly _paymentManager As New PaymentManager()
    Private ReadOnly _fontResumen As Font = New Font("Segoe UI", 9, FontStyle.Bold)

    ' Variables para guardar la carga completa original
    Private listaIndividualCompleta As List(Of IndividualPaymentDTO)
    Private listaGrupalCompleta As List(Of GroupPaymentDTO)
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
        '1
    End Sub
    '
    '
    '
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged

        Dim criterio As String = TxtSearch.Text.Trim() '.ToUpper()

        ' --- BÚSQUEDA INDIVIDUAL ---
        If RbPayIndividual.Checked AndAlso listaIndividualCompleta IsNot Nothing Then
            Dim filtrados = listaIndividualCompleta.Where(Function(x)
                                                              ' Lógica de coincidencia según ComboBox
                                                              Dim coincide As Boolean = False
                                                              If CmbFilter.SelectedIndex = 0 Then ' NOMBRE
                                                                  coincide = (x.Nombre IsNot Nothing AndAlso x.Nombre.Contains(criterio))
                                                              Else ' APELLIDO
                                                                  coincide = (x.Apellido IsNot Nothing AndAlso x.Apellido.Contains(criterio))
                                                              End If

                                                              ' Mantener visible la fila de resumen si el cliente coincide
                                                              Dim esResumenVisible = x.EsFilaResumen AndAlso listaIndividualCompleta.Any(Function(cli)
                                                                                                                                             Return cli.IdCli = x.IdCli AndAlso Not cli.EsFilaResumen AndAlso
                                                                                                                                                    ((CmbFilter.SelectedIndex = 0 AndAlso cli.Nombre IsNot Nothing AndAlso cli.Nombre.Contains(criterio)) OrElse
                                                                                                                                                     (CmbFilter.SelectedIndex = 1 AndAlso cli.Apellido IsNot Nothing AndAlso cli.Apellido.Contains(criterio)))
                                                                                                                                         End Function)

                                                              Return coincide OrElse esResumenVisible
                                                          End Function).ToList()

            DgvIndividual.DataSource = Nothing
            DgvIndividual.DataSource = filtrados
            AplicarFormatoDgvIndividual() ' Re-aplicar rojos
        End If

        ' --- BÚSQUEDA GRUPAL ---
        If RbPayGroup.Checked AndAlso listaGrupalCompleta IsNot Nothing Then
            Dim filtradosGrp = listaGrupalCompleta.Where(Function(x)
                                                             Dim coincideGrp = (x.NombreGrupo IsNot Nothing AndAlso x.NombreGrupo.Contains(criterio))

                                                             Dim esResumenVisible = x.EsFilaResumen AndAlso listaGrupalCompleta.Any(Function(g)
                                                                                                                                        Return g.IdGrp = x.IdGrp AndAlso Not g.EsFilaResumen AndAlso
                                                                                                                                               g.NombreGrupo IsNot Nothing AndAlso g.NombreGrupo.Contains(criterio)
                                                                                                                                    End Function)

                                                             Return coincideGrp OrElse esResumenVisible
                                                         End Function).ToList()

            DgvFamilyGroup.DataSource = Nothing
            DgvFamilyGroup.DataSource = filtradosGrp
            AplicarFormatoDgvGrupal() ' Re-aplicar verdes
        End If

        ActualizarStatusBar(criterio) ' Actualizar conteo
        '
    End Sub
    '
    '
    '
    Private Sub BtnClean_Click(sender As Object, e As EventArgs) Handles BtnClean.Click
        '3
    End Sub
    '
    '
    '
    Private Sub RbPayIndividual_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayIndividual.CheckedChanged
        '
        If RbPayIndividual.Checked Then
            ' 1. Visualización
            DgvIndividual.Visible = True
            DgvFamilyGroup.Visible = False

            ' 2. Configurar ComboBox
            CmbFilter.Items.Clear()
            CmbFilter.Items.AddRange({"NOMBRE", "APELLIDO"})
            CmbFilter.SelectedIndex = 0 ' Por defecto NOMBRE

            ' 3. Limpiar búsqueda anterior
            TxtSearch.Clear()
        End If
        '
    End Sub
    '
    '
    '
    Private Sub RbPayGroup_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayGroup.CheckedChanged
        '
        If RbPayGroup.Checked Then
            ' 1. Visualización
            DgvIndividual.Visible = False
            DgvFamilyGroup.Visible = True

            ' 2. Configurar ComboBox
            CmbFilter.Items.Clear()
            CmbFilter.Items.Add("NOMBRE GRUPO")
            CmbFilter.SelectedIndex = 0

            ' 3. Limpiar búsqueda anterior
            TxtSearch.Clear()
        End If
        '
    End Sub
    '
    '
    '
    Private Sub DgvIndividual_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvIndividual.CellContentClick
    End Sub
    Private Sub DgvIndividual_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvIndividual.CellFormatting

        If e.RowIndex < 0 Then Exit Sub

        Dim dgv = DirectCast(sender, DataGridView)
        Dim fila = TryCast(dgv.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)
        If fila Is Nothing OrElse Not fila.EsFilaResumen Then Exit Sub

        ' Estilo base para fila resumen
        e.CellStyle.BackColor = Color.Orange
        e.CellStyle.SelectionBackColor = Color.Orange
        e.CellStyle.Font = _fontResumen

        Select Case dgv.Columns(e.ColumnIndex).Name

            Case "PrcPgs", "DscPgs"
                e.Value = ""
                e.FormattingApplied = True

            Case "Total"
                e.Value = "DEBE"
                e.FormattingApplied = True

            Case "DiasMes"
                Dim cantidad = fila.CantidadMeses

                If fila.MtdPgs.Contains("MENSUAL") Then
                    e.Value = If(cantidad = 1, "1 MES", $"{cantidad} MESES")
                Else
                    e.Value = If(cantidad = 1, "1 DIA", $"{cantidad} DIAS")
                End If

                e.FormattingApplied = True
        End Select

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
    Private Sub DgvFamilyGroup_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFamilyGroup.CellContentClick
    End Sub
    Private Sub DgvFamilyGroup_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvFamilyGroup.CellFormatting

        If e.RowIndex < 0 Then Exit Sub

        Dim dgv = DirectCast(sender, DataGridView)
        Dim fila = TryCast(dgv.Rows(e.RowIndex).DataBoundItem, GroupPaymentDTO)

        ' Si la fila es nula o NO es una fila de resumen (naranja), no aplicamos cambios especiales
        If fila Is Nothing OrElse Not fila.EsFilaResumen Then Exit Sub

        ' 2. Estilo visual para la fila de resumen
        e.CellStyle.BackColor = Color.Orange
        e.CellStyle.Font = _fontResumen ' Asegúrate de tener esta variable definida

        ' 3. Personalización de celdas según tus marcas de colores
        Select Case dgv.Columns(e.ColumnIndex).Name

        ' --- MARCAS EN ROJO: Deben quedar vacías ---
            Case "PrcPgsGf", "DscPgsGf"
                e.Value = ""
                e.FormattingApplied = True

        ' --- MARCA VERDE (TOTAL): Mostrar "DEBE" ---
            Case "TtlPgsGf"
                e.Value = "DEBE"
                e.FormattingApplied = True

        ' --- MARCA VERDE (Nº DE DIAS): Mostrar sumatoria de registros ---
            Case "NumDiasGf"
                Dim cantidad = fila.CantidadMeses
                ' Lógica: "1 MES" si es uno, "X MESES" si son varios
                e.Value = If(cantidad = 1, "1 MES", $"{cantidad} MESES")
                e.FormattingApplied = True

        End Select

    End Sub
    ''
    ''
    ''
    Private Sub BtnPayMonth_Click(sender As Object, e As EventArgs) Handles BtnPayMonth.Click
        '111
    End Sub
    ''
    ''
    ''
    Private Sub BtnPaymentGenerator_Click(sender As Object, e As EventArgs) Handles BtnPaymentGenerator.Click

        Dim msg As String = "Se generarán los pagos mensuales para clientes individuales y grupos familiares. ¿Desea continuar?"

        If MessageBox.Show(msg, "Generación Masiva", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim generator As New Services.PaymentGenerator()
                ' Pasamos el ID del usuario actual (id_user) que tienes en tu sesión
                generator.GenerarPagosDelMes(UserSession.IdUser)

                MessageBox.Show("Pagos generados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ' Aquí podrías llamar a tu función CargarGrupales() para refrescar el DGV
            Catch ex As Exception
                MessageBox.Show("Error al generar pagos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub
    ''
    ''
    ''
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click

        '|-----------------------------------------------------------------------------
        '| CERRAR FORMULARIO LISTA DE MOROSOS
        '|-----------------------------------
        '| * 

        Me.Close()

    End Sub
    '
    '
    '
    Private Sub CargarIndividuales()
        Try
            listaIndividualCompleta = _paymentManager.ObtenerListaMorososIndividuales()

            ConfigurarGrid()
            CargarGrid(listaIndividualCompleta)
            ActualizarResumen(listaIndividualCompleta)
            AplicarFormatoDgvIndividual()

        Catch ex As Exception
            MessageBox.Show(
                $"Error al cargar individuales:{Environment.NewLine}{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            )
        End Try
    End Sub
    Private Sub ConfigurarGrid()
        DgvIndividual.AutoGenerateColumns = False

        DgvIndividual.Columns("PrcPgs").DefaultCellStyle.Format = "C2"
        DgvIndividual.Columns("DscPgs").DefaultCellStyle.Format = "C2"
        DgvIndividual.Columns("Total").DefaultCellStyle.Format = "C2"
        DgvIndividual.Columns("APagar").DefaultCellStyle.Format = "C2"
    End Sub
    Private Sub CargarGrid(lista As List(Of IndividualPaymentDTO))
        DgvIndividual.DataSource = lista
    End Sub
    Private Sub ActualizarResumen(lista As List(Of IndividualPaymentDTO))

        Dim numMorosos = lista.Where(Function(x) Not x.EsFilaResumen).Count()
        LblSolitos.Text = $"{numMorosos} - Registros pendientes de pago."

    End Sub

    Private Sub CargarGrupales()
        Try
            ' Llamamos al nuevo método que creamos en el Manager/Servicio
            listaGrupalCompleta = _paymentManager.ObtenerListaMorososGrupales()

            ConfigurarGridGrupal()
            CargarGridGrupal(listaGrupalCompleta)
            ActualizarResumenGrupal(listaGrupalCompleta)
            AplicarFormatoDgvGrupal()

        Catch ex As Exception
            MessageBox.Show(
            $"Error al cargar pagos grupales:{Environment.NewLine}{ex.Message}",
            "Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        )
        End Try
    End Sub

    Private Sub ConfigurarGridGrupal()
        DgvFamilyGroup.AutoGenerateColumns = False

        DgvFamilyGroup.Columns("PrcPgsGf").DefaultCellStyle.Format = "C2"
        DgvFamilyGroup.Columns("DscPgsGf").DefaultCellStyle.Format = "C2"
        DgvFamilyGroup.Columns("TtlPgsGf").DefaultCellStyle.Format = "C2"
        DgvFamilyGroup.Columns("ApgrGf").DefaultCellStyle.Format = "C2"

    End Sub

    Private Sub CargarGridGrupal(lista As List(Of GroupPaymentDTO))
        ' Es importante limpiar el DataSource antes de asignar el nuevo
        DgvFamilyGroup.DataSource = Nothing
        DgvFamilyGroup.DataSource = lista
    End Sub

    Private Sub ActualizarResumenGrupal(lista As List(Of GroupPaymentDTO))
        ' Contamos cuántas cuotas/meses hay pendientes (excluyendo las filas de resumen)
        Dim numPendientes = lista.Where(Function(x) Not x.EsFilaResumen).Count()

        ' Contamos cuántos grupos distintos tienen deuda
        Dim numGrupos = lista.Where(Function(x) Not x.EsFilaResumen) _
                         .Select(Function(x) x.IdGrp) _
                         .Distinct().Count()

        LblToditos.Text = $"{numPendientes} cuotas pendientes de {numGrupos} grupos familiares."
    End Sub

    Private Sub ActualizarStatusBar(criterio As String)
        Dim totalReg As Integer = DgvIndividual.RowCount + DgvFamilyGroup.RowCount

        If criterio = "" Then
            SlblTitle.Text = If(totalReg = 0, "Lista vacía", "Nº de Registros")
            SlblMessage.Text = $" {totalReg} - Registros pendientes de pago."
        Else
            SlblTitle.Text = "Buscando..."
            SlblMessage.Text = $" {totalReg} - Registro(s) que coincide(n) con su búsqueda."
        End If
    End Sub

    Private Sub AplicarFormatoDgvIndividual()
        For Each row As DataGridViewRow In DgvIndividual.Rows
            ' Obtenemos el objeto DTO vinculado a la fila
            Dim dto = TryCast(row.DataBoundItem, IndividualPaymentDTO)

            If dto IsNot Nothing AndAlso dto.EsFilaResumen Then
                ' Formato para la fila de "DEBE : X MESES"
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192) ' Rojo suave
                row.DefaultCellStyle.ForeColor = Color.DarkRed
                row.DefaultCellStyle.Font = New Font(DgvIndividual.Font, FontStyle.Bold)
            End If
        Next
    End Sub

    Private Sub AplicarFormatoDgvGrupal()
        For Each row As DataGridViewRow In DgvFamilyGroup.Rows
            Dim dto = TryCast(row.DataBoundItem, GroupPaymentDTO)

            If dto IsNot Nothing AndAlso dto.EsFilaResumen Then
                row.DefaultCellStyle.BackColor = Color.FromArgb(192, 255, 192) ' Verde suave
                row.DefaultCellStyle.ForeColor = Color.DarkGreen
                row.DefaultCellStyle.Font = New Font(DgvFamilyGroup.Font, FontStyle.Bold)
            End If
        Next
    End Sub

End Class