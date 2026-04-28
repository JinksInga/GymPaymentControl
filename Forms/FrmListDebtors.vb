Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.Utils

Public Class FrmListDebtors
    '
    Private ReadOnly _paymentManager As New PaymentManager()
    Private ReadOnly _fontSummary As Font = New Font("Arial", 10, FontStyle.Bold)
    Private _isFiltering As Boolean ' = False

    ' Variables para guardar la carga completa original
    Private listIndividualPayment As List(Of IndividualPaymentDTO)
    Private listGroupPayment As List(Of GroupPaymentDTO)
    ''
    ''
    ''
    Private Sub FrmListDebtors_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '|
        '|
        '|

        LblErrorProvider.Text = String.Empty
        UploadIndividualDebts()
        UploadGroupDebts()

    End Sub
    ''
    ''
    ''
    Private Sub CmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFilter.SelectedIndexChanged
        '

        TxtSearch.Focus() 'ENVIAR ENFOQUE AL TEXBOX

    End Sub
    ''
    ''
    ''
    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged

        '|
        '|
        '|

        Dim searchCriteria As String = TxtSearch.Text.Trim()

        ' --- BÚSQUEDA INDIVIDUAL ---
        If RbPayIndividual.Checked AndAlso listIndividualPayment IsNot Nothing Then

            ' Iniciamos el modo "silencio"
            _isFiltering = True

            Dim filteredRecords =
                listIndividualPayment.Where(Function(x)
                                                ' Lógica de coincidencia según ComboBox
                                                Dim coincideWith As Boolean = False

                                                If CmbFilter.SelectedIndex = 0 Then
                                                    coincideWith = (x.FirstName IsNot Nothing AndAlso x.FirstName.Contains(searchCriteria))
                                                Else
                                                    coincideWith = (x.LastName IsNot Nothing AndAlso x.LastName.Contains(searchCriteria))
                                                End If

                                                ' Mantener visible la fila de resumen si el cliente coincide
                                                Dim isVisibleSummary =
                                                  x.IsSummaryRow AndAlso
                                                  listIndividualPayment.Any(Function(client)

                                                                                Return client.IdCli =
                                                                                x.IdCli AndAlso Not client.IsSummaryRow AndAlso
                                                                                ((CmbFilter.SelectedIndex = 0 AndAlso client.FirstName IsNot Nothing AndAlso client.FirstName.Contains(searchCriteria)) OrElse
                                                                                (CmbFilter.SelectedIndex = 1 AndAlso client.LastName IsNot Nothing AndAlso client.LastName.Contains(searchCriteria)))
                                                                            End Function)

                                                Return coincideWith OrElse isVisibleSummary
                                            End Function).ToList()

            DgvIndividual.DataSource = Nothing
            DgvIndividual.DataSource = filteredRecords

            'COMPROBAR SI HAY REGISTROS EN LA GRILLA
            If DgvIndividual.RowCount > 0 Then
                ' Limpiamos cualquier error previo antes de mover la celda
                ErrorProvider.Clear()

                Select Case CmbFilter.SelectedIndex
                    Case 0 : DgvIndividual.CurrentCell = DgvIndividual.Item(0, 0)'NOMBRE
                    Case 1 : DgvIndividual.CurrentCell = DgvIndividual.Item(1, 0) 'APELLIDO
                End Select

                BtnCollectMonth.Enabled = True

            Else
                BtnCollectMonth.Enabled = False

            End If

            _isFiltering = False ' Fin del modo silencio

        End If

        ' --- BÚSQUEDA GRUPAL ---
        If RbPayGroup.Checked AndAlso listGroupPayment IsNot Nothing Then

            ' Iniciamos el modo "silencio"
            _isFiltering = True

            Dim filteredRecords =
                listGroupPayment.Where(Function(x)

                                           Dim coincideWith As Boolean = False

                                           If CmbFilter.SelectedIndex = 0 Then
                                               coincideWith = (x.GroupMembers IsNot Nothing AndAlso x.GroupMembers.Contains(searchCriteria))
                                           Else
                                               coincideWith = (x.GroupName IsNot Nothing AndAlso x.GroupName.Contains(searchCriteria))
                                           End If

                                           Dim isVisibleSummary =
                                              x.IsSummaryRow AndAlso
                                              listGroupPayment.Any(Function(group)
                                                                       Return group.IdGrp =
                                                                       x.IdGrp AndAlso Not group.IsSummaryRow AndAlso
                                                                       ((CmbFilter.SelectedIndex = 0 AndAlso group.GroupMembers IsNot Nothing AndAlso group.GroupMembers.Contains(searchCriteria)) OrElse
                                                                       (CmbFilter.SelectedIndex = 1 AndAlso group.GroupName IsNot Nothing AndAlso group.GroupName.Contains(searchCriteria)))
                                                                   End Function)

                                           Return coincideWith OrElse isVisibleSummary
                                       End Function).ToList()

            DgvFamilyGroup.DataSource = Nothing
            DgvFamilyGroup.DataSource = filteredRecords

            'COMPROBAR SI HAY REGISTROS EN LA GRILLA
            If DgvFamilyGroup.RowCount > 0 Then
                ' Limpiamos cualquier error previo antes de mover la celda
                ErrorProvider.Clear()

                Select Case CmbFilter.SelectedIndex
                    Case 0 : DgvFamilyGroup.CurrentCell = DgvFamilyGroup.Item(0, 0)'INTEGRANTES
                    Case 1 : DgvFamilyGroup.CurrentCell = DgvFamilyGroup.Item(1, 0) 'NOMBRE DEL GRUPO
                End Select

                BtnCollectMonth.Enabled = True

            Else
                BtnCollectMonth.Enabled = False

            End If

            _isFiltering = False ' Fin del modo silencio

        End If

        ActualizarStatusBar(searchCriteria) ' Actualizar conteo
        '
    End Sub
    Private Sub ActualizarStatusBar(criterio As String)

        '|
        '|
        '|
        ' Detectamos qué DGV estamos usando
        Dim dgvActivo = If(RbPayIndividual.Checked, DgvIndividual, DgvFamilyGroup)

        ' Usamos la interfaz común para contar sin importar el tipo de DTO
        Dim lista = TryCast(dgvActivo.DataSource, IEnumerable(Of IPaymentSummary))

        If lista Is Nothing Then
            SlblMessage.Text = "0 - Registros."
            Exit Sub
        End If

        Dim totalReg = lista.Count(Function(x) Not x.IsSummaryRow)

        If String.IsNullOrWhiteSpace(criterio) Then
            SlblTitle.Text = "Nº de Registros"
            SlblMessage.Text = $" {totalReg} - Registros pendientes de pago."
        Else
            SlblTitle.Text = "Buscando..."
            SlblMessage.Text = $" {totalReg} - Resultado(s) encontrado(s)."
        End If

    End Sub
    ''
    ''
    ''
    Private Sub BtnClean_Click(sender As Object, e As EventArgs) Handles BtnClean.Click

        '|
        '|
        '|
        '
        TxtSearch.Clear()
        TxtSearch.Focus()

    End Sub
    ''
    ''
    ''
    Private Sub RbPayIndividual_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayIndividual.CheckedChanged

        '|
        '|
        '|

        If RbPayIndividual.Checked Then

            ' 1. Visualización
            RbPayIndividual.BringToFront()
            DgvIndividual.Visible = True
            DgvFamilyGroup.Visible = False

            ' 1. Limpiamos el texto sin disparar filtros innecesarios si es posible
            TxtSearch.Clear()

            ' 2. En lugar de ir a la BD, refrescamos el enlace con la lista que ya tenemos
            ' Esto limpia cualquier filtro que haya quedado pegado en el Grid
            DgvIndividual.DataSource = Nothing
            DgvIndividual.DataSource = listIndividualPayment

            ' 3. Ajustes finales de UI
            CmbFilter.Items.Clear()
            CmbFilter.Items.AddRange({"   NOMBRE", "   APELLIDO"})
            CmbFilter.SelectedIndex = 0

            TxtSearch.Focus()

            ' 4. 
            DgvIndividual.CurrentCell = Nothing
            BtnCollectMonth.Enabled = False

            ActualizarStatusBar("")
        End If

    End Sub
    ''
    ''
    ''
    Private Sub RbPayGroup_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayGroup.CheckedChanged

        '|
        '|
        '|

        If RbPayGroup.Checked Then

            ' 1. Visualización
            RbPayGroup.BringToFront()
            DgvIndividual.Visible = False
            DgvFamilyGroup.Visible = True

            ' 3. Limpiar búsqueda anterior
            TxtSearch.Clear()

            ' 2. En lugar de ir a la BD, refrescamos el enlace con la lista que ya tenemos
            ' Esto limpia cualquier filtro que haya quedado pegado en el Grid
            DgvFamilyGroup.DataSource = Nothing
            DgvFamilyGroup.DataSource = listGroupPayment

            ' 2. Configurar ComboBox
            CmbFilter.Items.Clear()
            CmbFilter.Items.AddRange({"   INTEGRANTES", "   NOMBRE GRUPO"})
            CmbFilter.SelectedIndex = 0

            TxtSearch.Focus()

            ' 4. 
            DgvFamilyGroup.CurrentCell = Nothing
            BtnCollectMonth.Enabled = False

            ActualizarStatusBar("")
        End If

    End Sub
    ''
    ''
    ''
    Private Sub DgvIndividual_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvIndividual.CellContentClick
    End Sub
    Private Sub DgvIndividual_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvIndividual.CellFormatting

        '|
        '|
        '|

        If e.RowIndex < 0 Then Exit Sub

        Dim dataGridView = DirectCast(sender, DataGridView)
        Dim row = TryCast(dataGridView.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)
        If row Is Nothing OrElse Not row.IsSummaryRow Then Exit Sub

        ' Estilo base para fila resumen
        e.CellStyle.ForeColor = Color.OrangeRed
        e.CellStyle.BackColor = Color.LightSalmon
        e.CellStyle.SelectionBackColor = Color.LightSalmon
        e.CellStyle.Font = _fontSummary

        Select Case dataGridView.Columns(e.ColumnIndex).Name

            Case "AgeText", "PrcPgs", "DscPgs"
                e.Value = ""
                e.FormattingApplied = True

            Case "Total"
                e.Value = "DEBE"
                e.FormattingApplied = True

            Case "daysOfMonthInv"
                Dim amount = row.NumberMonths

                If row.MtdPgs.Contains("MENSUAL") Then
                    e.Value = If(amount = 1, "1 MES", $"{amount} MESES")
                Else
                    e.Value = If(amount = 1, "1 DIA", $"{amount} DIAS")
                End If

                e.FormattingApplied = True
        End Select

    End Sub
    Private Sub DgvIndividual_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DgvIndividual.DataBindingComplete

        '|
        '|
        '|
        ' Recorremos las filas una vez que los datos están cargados
        ' Limpiar selección después de cargar los datos

        Dim dataGridView = DirectCast(sender, DataGridView)
        For Each row As DataGridViewRow In dataGridView.Rows
            Dim rowSumary = TryCast(row.DataBoundItem, IndividualPaymentDTO)
            row.Height = 25
        Next

        If DgvIndividual.Rows.Count > 0 Then DgvIndividual.CurrentCell = Nothing

    End Sub
    Private Sub DgvIndividual_SelectionChanged(sender As Object, e As EventArgs) Handles DgvIndividual.SelectionChanged

        '|
        '|
        '|
        ' Si estamos buscando, no queremos que aparezcan iconos de error
        If _isFiltering Then Exit Sub
        CheckRowDataGridView(DgvIndividual, LblErrorProvider, BtnCollectMonth, ErrorProvider, "Selecciona una fila que contenga un PAGO.")

    End Sub
    ''
    ''
    ''
    Private Sub DgvFamilyGroup_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFamilyGroup.CellContentClick
    End Sub
    Private Sub DgvFamilyGroup_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvFamilyGroup.CellFormatting

        '|
        '|
        '|

        If e.RowIndex < 0 Then Exit Sub

        Dim dataGridView = DirectCast(sender, DataGridView)
        Dim row = TryCast(dataGridView.Rows(e.RowIndex).DataBoundItem, GroupPaymentDTO)

        ' Si la fila es nula o NO es una fila de resumen (naranja), no aplicamos cambios especiales
        If row Is Nothing OrElse Not row.IsSummaryRow Then Exit Sub

        ' 2. Estilo visual para la fila de resumen
        e.CellStyle.ForeColor = Color.OrangeRed
        e.CellStyle.BackColor = Color.LightSalmon
        e.CellStyle.SelectionBackColor = Color.LightSalmon
        e.CellStyle.Font = _fontSummary ' Asegúrate de tener esta variable definida

        ' 3. Personalización de celdas según tus marcas de colores
        Select Case dataGridView.Columns(e.ColumnIndex).Name

        ' --- MARCAS EN ROJO: Deben quedar vacías ---
            Case "PrcPgsGf", "DscPgsGf"
                e.Value = ""
                e.FormattingApplied = True

        ' --- MARCA VERDE (TOTAL): Mostrar "DEBE" ---
            Case "TtlPgsGf"
                e.Value = "DEBE"
                e.FormattingApplied = True

        ' --- MARCA VERDE (Nº DE DIAS): Mostrar sumatoria de registros ---
            Case "daysOfMonthGrp"
                Dim amount = row.NumberMonths
                ' Lógica: "1 MES" si es uno, "X MESES" si son varios
                e.Value = If(amount = 1, "1 MES", $"{amount} MESES")
                e.FormattingApplied = True

        End Select

    End Sub
    Private Sub DgvFamilyGroup_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DgvFamilyGroup.DataBindingComplete

        '|
        '|
        '|
        ' Activa el salto de línea en la columna de INTEGRANTES (ajusta el índice si no es la 0)
        ' Permite que las filas crezcan a lo alto para mostrar todo el texto
        With DgvFamilyGroup

            .Columns("members").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .RowsDefaultCellStyle.Padding = New Padding(0, 3, 0, 3)

            If .Rows.Count > 0 Then .CurrentCell = Nothing

        End With

    End Sub
    Private Sub DgvFamilyGroup_SelectionChanged(sender As Object, e As EventArgs) Handles DgvFamilyGroup.SelectionChanged
        '
        ' Si estamos buscando, no queremos que aparezcan iconos de error
        If _isFiltering Then Exit Sub
        CheckRowDataGridView(DgvFamilyGroup, LblErrorProvider, BtnCollectMonth, ErrorProvider, "Selecciona una fila que contenga un PAGO.")

    End Sub
    ''
    ''
    ''
    Private Sub BtnCollectMonth_Click(sender As Object, e As EventArgs) Handles BtnCollectMonth.Click

        '|
        '|
        '|
        If RbPayIndividual.Checked Then

            Dim selectedPayment = TryCast(DgvIndividual.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

            If selectedPayment IsNot Nothing AndAlso Not selectedPayment.IsSummaryRow Then
                ' Aquí le pasamos la función que refresca los deudores individuales
                OpenFrmCollectMembership(selectedPayment, AddressOf RefreshDgvIndividual)
            Else
                ShowMessageBox()
            End If

        End If

        If RbPayGroup.Checked Then

            Dim selectedPayment = TryCast(DgvFamilyGroup.CurrentRow?.DataBoundItem, GroupPaymentDTO)

            If selectedPayment IsNot Nothing AndAlso Not selectedPayment.IsSummaryRow Then
                ' Aquí le pasamos la función que refresca los deudores individuales
                OpenFrmCollectMembership(selectedPayment, AddressOf RefreshDgvFamilyGroup)
            Else
                ShowMessageBox()
            End If
        End If

    End Sub
    ''
    ''
    ''
    Private Sub BtnPaymentGenerator_Click(sender As Object, e As EventArgs) Handles BtnPaymentGenerator.Click

        '|
        '|
        '|

        Try
            Dim newMonth = Date.Now.ToString("MMMM").ToUpper
            Dim generator As New PaymentGenerator()

            ' 1. PRIMERO VALIDAMOS (Sin guardar nada aún)
            ' Usamos la función de chequeo que no inserta registros
            If Not generator.HasPendingMassivePayments() Then

                MessageBox.Show($"   Las membresías de {newMonth} ya están registradas en" & Environment.NewLine &
                                "   la base de datos." & Environment.NewLine &
                                Environment.NewLine &
                                "   No es posible duplicar pagos existentes." & Environment.NewLine &
                                "   ________________________________________________________" & Environment.NewLine &
                                Environment.NewLine &
                                "                                                          Operación cancelada.",
                                "Aviso - Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub

            End If

            ' 2. SI HAY PENDIENTES, PEDIMOS PERMISO
            Dim strMsgBox As String = "                            ¡ ¡ ¡  ATENCIÓN  ! ! !" & Environment.NewLine & Environment.NewLine &
                                      $"   Se crearán nuevos pagos de {newMonth} para todos los" & Environment.NewLine &
                                      "   clientes y grupos familiares en actividad." & Environment.NewLine &
                                      "   __________________________________________________________" & Environment.NewLine & Environment.NewLine &
                                      "      ¿Desea continuar con la creación masiva de registros?"

            If MessageBox.Show(strMsgBox, "Registrar pagos nuevos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                ' 3. SÓLO AQUÍ EJECUTAMOS EL GUARDADO REAL
                ' Esta es la única vez que llamamos a la función que hace INSERT
                Dim registrosCreados As Integer = generator.GenerateNewMonthPayments()

                MessageBox.Show($"Se han generado {registrosCreados} nuevos pagos correctamente.",
                                "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Actualizamos la interfaz
                RefreshDgvIndividual()
                RefreshDgvFamilyGroup()
                ActualizarStatusBar("")
            End If

        Catch ex As Exception
            MessageBox.Show("Error al generar deudas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

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
    ''
    ''
    ''
    Private Sub UploadIndividualDebts()

        '|
        '|
        '|

        Try
            listIndividualPayment = _paymentManager.GetListIndividualDebtors()
            ConfigureDataGridView(DgvIndividual, "PrcPgs", "DscPgs", "Total", "APagar")
            LoadDataGridView(DgvIndividual, listIndividualPayment)
            ActualizarStatusBar("")

        Catch ex As Exception
            MessageBox.Show($"Pagos individuales: {Environment.NewLine}{ex.Message}", "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''
    ''
    ''
    Private Sub UploadGroupDebts()

        '|
        '|
        '|

        Try
            listGroupPayment = _paymentManager.GetListGroupDebtors()
            ConfigureDataGridView(DgvFamilyGroup, "PrcPgsGf", "DscPgsGf", "TtlPgsGf", "ApgrGf")
            LoadDataGridView(DgvFamilyGroup, listGroupPayment)
            ActualizarStatusBar("")

        Catch ex As Exception
            MessageBox.Show($"Pagos grupales:{Environment.NewLine}{ex.Message}", "Error al cargar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''
    ''
    ''
    Private Sub CheckRowDataGridView(dataGridView As DataGridView, label As Label, button As Button, errorProvider As ErrorProvider, errorMessage As String)

        '|
        '|
        '|

        Dim dto = TryCast(dataGridView.CurrentRow?.DataBoundItem, ISelectableRow)

        Dim isValid = dto IsNot Nothing AndAlso Not dto.IsSummaryRow AndAlso dto.IdPayment > 0

        button.Enabled = isValid

        errorProvider.Clear()

        If Not isValid Then
            errorProvider.SetError(label, errorMessage)
        End If

    End Sub

    Private Sub ConfigureDataGridView(dataGridView As DataGridView, ParamArray currencyFormatColumn As String())

        '|
        '|
        '|

        dataGridView.AutoGenerateColumns = False

        For Each nameColumn In currencyFormatColumn
            If dataGridView.Columns.Contains(nameColumn) Then
                dataGridView.Columns(nameColumn).DefaultCellStyle.Format = "C2"
            End If
        Next
    End Sub

    Private Sub LoadDataGridView(Of T)(dataGridView As DataGridView, list As List(Of T))

        '|
        '|
        '|

        dataGridView.DataSource = Nothing
        dataGridView.DataSource = list

    End Sub

    Private Sub RefreshDgvIndividual()

        TxtSearch.Clear()
        listIndividualPayment = _paymentManager.GetListIndividualDebtors()
        DgvIndividual.DataSource = Nothing
        DgvIndividual.DataSource = listIndividualPayment

    End Sub

    Private Sub RefreshDgvFamilyGroup()

        TxtSearch.Clear()
        listGroupPayment = _paymentManager.GetListGroupDebtors()
        DgvFamilyGroup.DataSource = Nothing
        DgvFamilyGroup.DataSource = listGroupPayment

    End Sub

    Private Sub ShowMessageBox()

        '|
        '|
        '|

        Dim strMsgBox As String = "   Para cobrar la cuota mensual a un cliente" & Environment.NewLine &
                                  Environment.NewLine &
                                  "   Selecciona un registro válido de la lista de morosos" & Environment.NewLine &
                                  "   _____________________________________________________" & Environment.NewLine &
                                  Environment.NewLine &
                                  "                     La fila RESUMEN no es un registro válido"

        MessageBox.Show(strMsgBox, "Seleccionar registro", MessageBoxButtons.OK, MessageBoxIcon.Error)

    End Sub
    ''
    ''
    ''
End Class