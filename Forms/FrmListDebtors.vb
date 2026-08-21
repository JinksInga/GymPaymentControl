Imports GymPaymentControl.Constants
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

Public Class FrmListDebtors

    ' --- Instancias de Servicios y Gestión de Datos ---
    Private ReadOnly _paymentManager As New PaymentManager()
    Private _listIndividualPayment As List(Of IndividualPaymentDTO)
    Private _listGroupPayment As List(Of GroupPaymentDTO)

    ' --- Control de Flujo y Banderas de UI ---
    Private _isFiltering As Boolean

    ' --- Recursos Visuales y UI ---
    Private ReadOnly _fontSummary As New Font("Arial", 10, FontStyle.Bold)


    ' =====================================
    ' | EVENTOS DEL FORMULARIO (Handlers) |
    ' =====================================
    Private Sub FrmListDebtors_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LblErrorProvider.Text = String.Empty

        UploadIndividualDebts()

        UploadGroupDebts()

    End Sub


    Private Sub CmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFilter.SelectedIndexChanged
        TxtSearch.Focus()
    End Sub


    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged

        Dim searchCriteria As String = TxtSearch.Text.Trim()

        ' --- BÚSQUEDA INDIVIDUAL ---
        If RbPayIndividual.Checked AndAlso _listIndividualPayment IsNot Nothing Then

            Dim filteredRecords =
                _listIndividualPayment.Where(Function(x)
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
                                                  _listIndividualPayment.Any(Function(client)

                                                                                 Return client.IdCli =
                                                                                x.IdCli AndAlso Not client.IsSummaryRow AndAlso
                                                                                ((CmbFilter.SelectedIndex = 0 AndAlso client.FirstName IsNot Nothing AndAlso client.FirstName.Contains(searchCriteria)) OrElse
                                                                                (CmbFilter.SelectedIndex = 1 AndAlso client.LastName IsNot Nothing AndAlso client.LastName.Contains(searchCriteria)))
                                                                             End Function)

                                                 Return coincideWith OrElse isVisibleSummary
                                             End Function).ToList()

            ' Infiere T como IndividualPaymentDTO
            ApplyGridFilterState(DgvIndividual, filteredRecords)

        End If

        ' --- BÚSQUEDA GRUPAL ---
        If RbPayGroup.Checked AndAlso _listGroupPayment IsNot Nothing Then

            Dim filteredRecords =
                _listGroupPayment.Where(Function(x)

                                            Dim coincideWith As Boolean = False

                                            If CmbFilter.SelectedIndex = 0 Then
                                                coincideWith = (x.GroupMembers IsNot Nothing AndAlso x.GroupMembers.Contains(searchCriteria))
                                            Else
                                                coincideWith = (x.GroupName IsNot Nothing AndAlso x.GroupName.Contains(searchCriteria))
                                            End If

                                            Dim isVisibleSummary =
                                              x.IsSummaryRow AndAlso
                                              _listGroupPayment.Any(Function(group)
                                                                        Return group.IdGrp =
                                                                       x.IdGrp AndAlso Not group.IsSummaryRow AndAlso
                                                                       ((CmbFilter.SelectedIndex = 0 AndAlso group.GroupMembers IsNot Nothing AndAlso group.GroupMembers.Contains(searchCriteria)) OrElse
                                                                       (CmbFilter.SelectedIndex = 1 AndAlso group.GroupName IsNot Nothing AndAlso group.GroupName.Contains(searchCriteria)))
                                                                    End Function)

                                            Return coincideWith OrElse isVisibleSummary
                                        End Function).ToList()

            ' Infiere T como GroupPaymentDTO
            ApplyGridFilterState(DgvFamilyGroup, filteredRecords)

        End If

        UpdateStatusBar(searchCriteria)

    End Sub


    Private Sub BtnClean_Click(sender As Object, e As EventArgs) Handles BtnClean.Click

        TxtSearch.Clear()
        TxtSearch.Focus()

    End Sub


    Private Sub RbPayIndividual_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayIndividual.CheckedChanged

        If RbPayIndividual.Checked Then

            RbPayIndividual.BringToFront()
            DgvIndividual.Visible = True
            DgvFamilyGroup.Visible = False

            TxtSearch.Clear()

            LoadDataGridView(DgvIndividual, _listIndividualPayment)

            CmbFilter.Items.Clear()
            CmbFilter.Items.AddRange({$"   {SearchFilters.ByName}", $"   {SearchFilters.ByLastName}"})
            CmbFilter.SelectedIndex = 0

            TxtSearch.Focus()

            DgvIndividual.CurrentCell = Nothing
            BtnCollectFee.Enabled = False

            UpdateStatusBar("")

        End If

    End Sub


    Private Sub RbPayGroup_CheckedChanged(sender As Object, e As EventArgs) Handles RbPayGroup.CheckedChanged

        If RbPayGroup.Checked Then

            RbPayGroup.BringToFront()
            DgvIndividual.Visible = False
            DgvFamilyGroup.Visible = True

            TxtSearch.Clear()

            LoadDataGridView(DgvFamilyGroup, _listGroupPayment)

            CmbFilter.Items.Clear()
            CmbFilter.Items.AddRange({$"   {SearchFilters.ByMembers}", $"   {SearchFilters.ByGroupName}"})
            CmbFilter.SelectedIndex = 0

            TxtSearch.Focus()

            DgvFamilyGroup.CurrentCell = Nothing
            BtnCollectFee.Enabled = False

            UpdateStatusBar("")

        End If

    End Sub


    Private Sub DgvIndividual_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvIndividual.CellContentClick
    End Sub
    Private Sub DgvIndividual_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvIndividual.CellFormatting

        If e.RowIndex < 0 Then Exit Sub

        Dim dataGridView = DirectCast(sender, DataGridView)
        Dim row = TryCast(dataGridView.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)
        If row Is Nothing OrElse Not row.IsSummaryRow Then Exit Sub

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

                If row.MtdPgs.Contains(PaymentMethods.Monthly) Then
                    e.Value = If(amount = 1, "1 MES", $"{amount} MESES")
                Else
                    e.Value = If(amount = 1, "1 DIA", $"{amount} DIAS")
                End If

                e.FormattingApplied = True
        End Select

    End Sub
    Private Sub DgvIndividual_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DgvIndividual.DataBindingComplete

        Dim dataGridView = DirectCast(sender, DataGridView)
        For Each row As DataGridViewRow In dataGridView.Rows
            Dim rowSumary = TryCast(row.DataBoundItem, IndividualPaymentDTO)
            row.Height = 25
        Next

        If DgvIndividual.Rows.Count > 0 Then

            DgvIndividual.CurrentCell = Nothing
            BtnCollectFee.Enabled = False

        End If

    End Sub
    Private Sub DgvIndividual_SelectionChanged(sender As Object, e As EventArgs) Handles DgvIndividual.SelectionChanged

        If _isFiltering Then Exit Sub

        CheckRowDataGridView(DgvIndividual, LblErrorProvider, BtnCollectFee, ErrorProvider, AppMessages.SelectRecord)

    End Sub


    Private Sub DgvFamilyGroup_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvFamilyGroup.CellContentClick
    End Sub
    Private Sub DgvFamilyGroup_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvFamilyGroup.CellFormatting

        If e.RowIndex < 0 Then Exit Sub

        Dim dataGridView = DirectCast(sender, DataGridView)
        Dim row = TryCast(dataGridView.Rows(e.RowIndex).DataBoundItem, GroupPaymentDTO)

        If row Is Nothing OrElse Not row.IsSummaryRow Then Exit Sub

        e.CellStyle.ForeColor = Color.OrangeRed
        e.CellStyle.BackColor = Color.LightSalmon
        e.CellStyle.SelectionBackColor = Color.LightSalmon
        e.CellStyle.Font = _fontSummary

        Select Case dataGridView.Columns(e.ColumnIndex).Name

            Case "PrcPgsGf", "DscPgsGf"

                e.Value = ""
                e.FormattingApplied = True

            Case "TtlPgsGf"

                e.Value = "DEBE"
                e.FormattingApplied = True

            Case "daysOfMonthGrp"

                Dim amount = row.NumberMonths

                e.Value = If(amount = 1, "1 MES", $"{amount} MESES")
                e.FormattingApplied = True

        End Select

    End Sub
    Private Sub DgvFamilyGroup_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DgvFamilyGroup.DataBindingComplete

        ' Activa el salto de línea en la columna de INTEGRANTES (ajusta el índice si no es la 0)
        ' Permite que las filas crezcan a lo alto para mostrar todo el texto
        With DgvFamilyGroup

            .Columns("members").DefaultCellStyle.WrapMode = DataGridViewTriState.True
            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .RowsDefaultCellStyle.Padding = New Padding(0, 3, 0, 3)

            If .Rows.Count > 0 Then

                .CurrentCell = Nothing
                BtnCollectFee.Enabled = False

            End If

        End With

    End Sub
    Private Sub DgvFamilyGroup_SelectionChanged(sender As Object, e As EventArgs) Handles DgvFamilyGroup.SelectionChanged

        If _isFiltering Then Exit Sub

        CheckRowDataGridView(DgvFamilyGroup, LblErrorProvider, BtnCollectFee, ErrorProvider, AppMessages.SelectRecord)

    End Sub


    Private Sub BtnCollectFee_Click(sender As Object, e As EventArgs) Handles BtnCollectFee.Click

        If RbPayIndividual.Checked Then

            Dim selectedPayment = TryCast(DgvIndividual.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

            If selectedPayment IsNot Nothing AndAlso Not selectedPayment.IsSummaryRow Then

                '| Pasamos la función que refresca los deudores individuales
                OpenFrmCollectMembership(selectedPayment, AddressOf RefreshDgvIndividual)

            Else

                MessageBox.Show(DialogMessages.SelectCorrectRow, "Seleccionar registro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End If

        If RbPayGroup.Checked Then

            Dim selectedPayment = TryCast(DgvFamilyGroup.CurrentRow?.DataBoundItem, GroupPaymentDTO)

            If selectedPayment IsNot Nothing AndAlso Not selectedPayment.IsSummaryRow Then

                '| Pasamos la función que refresca los deudores grupales
                OpenFrmCollectMembership(selectedPayment, AddressOf RefreshDgvFamilyGroup)

            Else

                MessageBox.Show(DialogMessages.SelectCorrectRow, "Seleccionar registro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End If

    End Sub


    Private Sub BtnPaymentGenerator_Click(sender As Object, e As EventArgs) Handles BtnPaymentGenerator.Click

        Try
            Dim newMonth = Date.Now.ToString("MMMM").ToUpper
            Dim generator As New PaymentGenerator()

            ' 1. PRIMERO VALIDAMOS (Sin guardar nada)
            ' Usamos la función de chequeo que no inserta registros
            If Not generator.HasPendingMassivePayments() Then

                MessageBox.Show(DialogMessages.DoNotDuplicatePayments(newMonth), "Aviso - Registro duplicado",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub

            End If

            '| * CONFIRMAR PARA CREAR DEUDAS MASIVAS
            Dim answer = MessageBox.Show(DialogMessages.AskBeforeRegisteringPayments(newMonth),
                                         "Aviso muy importante",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button2)
            If answer = DialogResult.Yes Then

                '| * GENERAMOS TODOS LAS MENSUALIDADES
                Dim recordsCreated As Integer = generator.GenerateNewMonthPayments()

                MessageBox.Show($"Se han generado {recordsCreated} nuevos pagos correctamente.",
                                    "Proceso finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                RefreshDgvIndividual()
                RefreshDgvFamilyGroup()
                UpdateStatusBar("")

            End If

        Catch ex As Exception
            MessageBox.Show("Error al generar deudas : " & ex.Message, "Error")
        End Try

    End Sub


    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

    ''' <summary>
    ''' Consulta la base de datos para obtener la lista de deudores individuales, 
    ''' aplica el formato de moneda a las columnas especificadas y la vincula a la grilla.
    ''' </summary>
    Private Sub UploadIndividualDebts()

        Try
            _listIndividualPayment = _paymentManager.GetListIndividualDebtors()

            ConfigureDataGridView(DgvIndividual, "PrcPgs", "DscPgs", "Total", "APagar")

            LoadDataGridView(DgvIndividual, _listIndividualPayment)

            UpdateStatusBar(String.Empty)

        Catch ex As Exception
            MessageBox.Show("ERROR AL CARGAR PAGOS INDIVIDUALES : " & vbCrLf & ex.Message)
        End Try

    End Sub


    ''' <summary>
    ''' Consulta la base de datos para obtener la lista de deudores grupales, 
    ''' aplica el formato de moneda a las columnas especificadas y la vincula a la grilla.
    ''' </summary>
    Private Sub UploadGroupDebts()

        Try
            _listGroupPayment = _paymentManager.GetListGroupDebtors()

            ConfigureDataGridView(DgvFamilyGroup, "PrcPgsGf", "DscPgsGf", "TtlPgsGf", "ApgrGf")

            LoadDataGridView(DgvFamilyGroup, _listGroupPayment)

            UpdateStatusBar(String.Empty)

        Catch ex As Exception
            MessageBox.Show("ERROR AL CARGAR PAGOS GRUPALES : " & vbCrLf & ex.Message)
        End Try

    End Sub


    ''' <summary>
    ''' Valida que la fila actualmente seleccionada en el DataGridView contenga un registro de pago válido.
    ''' Habilita o deshabilita el botón de acción y gestiona las alertas en la interfaz.
    ''' </summary>
    ''' <param name="dataGridView">DataGridView donde se realiza la inspección.</param>
    ''' <param name="label">Control de etiqueta donde se dibujará la indicación de error.</param>
    ''' <param name="button">Botón cuya disponibilidad depende de la validez del registro.</param>
    ''' <param name="errorProvider">Proveedor de errores para notificaciones visuales.</param>
    ''' <param name="errorMessage">Mensaje descriptivo que se mostrará en caso de fila no válida.</param>
    Private Sub CheckRowDataGridView(dataGridView As DataGridView, label As Label, button As Button,
                                     errorProvider As ErrorProvider, errorMessage As String)

        Dim dto = TryCast(dataGridView.CurrentRow?.DataBoundItem, ISelectableRow)

        Dim isValid = dto IsNot Nothing AndAlso Not dto.IsSummaryRow AndAlso dto.IdPayment > 0

        button.Enabled = isValid

        errorProvider.Clear()

        If Not isValid Then
            errorProvider.SetError(label, errorMessage)
        End If

    End Sub


    ''' <summary>
    ''' Configura las propiedades base de la grilla y asigna el formato de moneda local ("C2")
    ''' a una lista de columnas específicas por su nombre de encabezado.
    ''' </summary>
    ''' <param name="dataGridView">Instancia del DataGridView a formatear.</param>
    ''' <param name="currencyFormatColumn">Nombres de las columnas que recibirán el formato de moneda.</param>
    Private Sub ConfigureDataGridView(dataGridView As DataGridView, ParamArray currencyFormatColumn As String())

        dataGridView.AutoGenerateColumns = False

        For Each nameColumn In currencyFormatColumn

            If dataGridView.Columns.Contains(nameColumn) Then

                dataGridView.Columns(nameColumn).DefaultCellStyle.Format = "C2"

            End If

        Next

    End Sub


    ''' <summary>
    ''' Aplica los resultados del filtrado al DataGridView y actualiza el estado
    ''' visual y funcional de los controles relacionados con la selección.
    ''' </summary>
    ''' <typeparam name="T">
    ''' Tipo de los elementos contenidos en la lista filtrada.
    ''' </typeparam>
    ''' <param name="dataGridView">
    ''' DataGridView que recibirá los resultados del filtrado.
    ''' </param>
    ''' <param name="filteredList">
    ''' Lista de elementos filtrados que se asignará al DataGridView.
    ''' </param>
    ''' <remarks>
    ''' Durante la operación establece el estado interno de filtrado para evitar
    ''' respuestas no deseadas de los eventos asociados al DataGridView.
    ''' 
    ''' Si existen resultados, limpia los errores de validación y posiciona la
    ''' selección inicial según el filtro utilizado. También habilita o deshabilita
    ''' el botón de cobro en función de la existencia de resultados y de texto de búsqueda.
    ''' 
    ''' El estado de filtrado se restablece siempre al finalizar la operación,
    ''' incluso si se produce una excepción.
    ''' </remarks>
    Private Sub ApplyGridFilterState(Of T)(dataGridView As DataGridView,filteredList As List(Of T))

        _isFiltering = True

        Try
            LoadDataGridView(dataGridView, filteredList)

            Dim hasRows As Boolean = dataGridView.RowCount > 0
            Dim hasSearchText As Boolean = Not String.IsNullOrWhiteSpace(TxtSearch.Text)

            BtnCollectFee.Enabled = hasRows AndAlso hasSearchText

            If Not hasRows Then Return

            ErrorProvider.Clear()

            If hasSearchText Then
                ' Posiciona el foco en la columna correspondiente según el filtro seleccionado
                Select Case CmbFilter.SelectedIndex
                    Case 0 : dataGridView.CurrentCell = dataGridView.Item(0, 0)
                    Case 1 : dataGridView.CurrentCell = dataGridView.Item(1, 0)
                End Select
            Else
                dataGridView.CurrentCell = Nothing
            End If

        Finally
            _isFiltering = False
        End Try

    End Sub


    ''' <summary>
    ''' Asigna de forma genérica una lista fuertemente tipada como origen de datos del DataGridView.
    ''' </summary>
    ''' <typeparam name="T">Tipo de objeto contenedor de la lista.</typeparam>
    ''' <param name="dataGridView">Instancia de la grilla que recibirá los datos.</param>
    ''' <param name="list">Lista de elementos para vincular.</param>
    Private Sub LoadDataGridView(Of T)(dataGridView As DataGridView, list As List(Of T))

        dataGridView.DataSource = Nothing
        dataGridView.DataSource = list

    End Sub


    ''' <summary>
    ''' Restablece el filtro de búsqueda y refresca la información
    ''' de pagos individuales desde la base de datos.
    ''' </summary>
    Private Sub RefreshDgvIndividual()

        Try
            TxtSearch.Clear()

            _listIndividualPayment = _paymentManager.GetListIndividualDebtors()

            LoadDataGridView(DgvIndividual, _listIndividualPayment)

        Catch ex As Exception
            MessageBox.Show("ERROR AL REFRESCAR IMPAGOS INDIVIDUALES : " & vbCrLf & ex.Message)
        End Try

    End Sub


    ''' <summary>
    ''' Restablece el filtro de búsqueda y refresca la información
    ''' de pagos grupales desde la base de datos.
    ''' </summary>
    Private Sub RefreshDgvFamilyGroup()

        Try
            TxtSearch.Clear()

            _listGroupPayment = _paymentManager.GetListGroupDebtors()

            LoadDataGridView(DgvFamilyGroup, _listGroupPayment)

        Catch ex As Exception
            MessageBox.Show("ERROR AL REFRESCAR IMPAGOS GRUPALES : " & vbCrLf & ex.Message)
        End Try

    End Sub


    ''' <summary>
    ''' Actualiza la información de la barra de estado según el DataGridView activo 
    ''' y el contexto de búsqueda o filtrado actual.
    ''' </summary>
    ''' <param name="criterio">
    ''' Texto o término utilizado para filtrar los registros. 
    ''' Si se pasa una cadena vacía o <c>Nothing</c>, se muestra el contador general de pendientes.
    ''' </param>
    ''' <remarks>
    ''' El método identifica automáticamente la grilla visible (Individual o Grupal),
    ''' castea su origen de datos a la interfaz <see cref="IPaymentSummary"/> y descuenta 
    ''' las filas especiales de resumen antes de calcular el total.
    ''' </remarks>
    Private Sub UpdateStatusBar(criterio As String)

        Dim dgvActivo As DataGridView = If(RbPayIndividual.Checked, DgvIndividual, DgvFamilyGroup)

        Dim lista = TryCast(dgvActivo.DataSource, IEnumerable(Of IPaymentSummary))

        If lista Is Nothing Then

            SlblTitle.Text = "Nº de Registros"
            SlblMessage.Text = "0 - Registros."
            Exit Sub

        End If

        ' Contamos únicamente las filas de datos reales (excluyendo la fila de resumen)
        Dim totalReg As Integer = lista.Count(Function(x) Not x.IsSummaryRow)

        ' Actualizamos los textos de la StatusStrip según el contexto
        If String.IsNullOrWhiteSpace(criterio) Then

            SlblTitle.Text = "Nº de Registros"
            SlblMessage.Text = $" {totalReg} - Registros pendientes de pago."

        Else

            SlblTitle.Text = "Buscando..."
            SlblMessage.Text = $" {totalReg} - Resultado(s) encontrado(s)."

        End If

    End Sub


End Class