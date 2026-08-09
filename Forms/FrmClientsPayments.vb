Imports GymPaymentControl.Constants
Imports GymPaymentControl.Enums
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

Public Class FrmClientsPayments

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Servicios de Negocio (Managers) ---
    Private ReadOnly _clientManager As New ClientManager()
    Private ReadOnly _paymentManager As New PaymentManager()

    ' --- Colecciones de Datos en Memoria (Listas) ---
    Private _clientList As List(Of IndividualPaymentDTO)
    Private _historyList As List(Of IndividualPaymentDTO)

    ' --- Contexto del Cliente Seleccionado ---
    Private _selectedClient As IndividualPaymentDTO
    Private _currentState As String

    ' --- Banderas de Control de Flujo (UI Flags) ---
    Private _isCleaning As Boolean

#End Region

#Region " EVENTOS DEL FORMULARIO (Handlers) "
    Private Sub FrmClientsPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '| -------------------------
        '| | CONFIGURACIÓN INICIAL |
        '| -------------------------

        Try
            DisableButtons()
            ProcessLabelsRecursive(PnlDataClient, "Cli",
                                   Sub(label) label.Text = "")
            DgvPaymentList.DataSource = Nothing
            FetchClientsFromDatabase()

            DgvPaymentList.AutoGenerateColumns = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Inicio")
        End Try

    End Sub


    Private Sub BtnFindClient_Click(sender As Object, e As EventArgs) Handles BtnFindClient.Click

        '| -----------------------------------------
        '| PREPARAMOS LOS CONTROLES PARA LA BUSQUEDA
        '| -----------------------------------------

        _isCleaning = True

        ActivateSearchRecord()
        DisableButtons()
        ProcessLabelsRecursive(PnlDataClient, "Cli",
                                   Sub(label) label.Text = "")
        DgvPaymentList.DataSource = Nothing
        FilterAndRenderClientGridUI()

        _isCleaning = False

    End Sub


    Private Sub CmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFilter.SelectedIndexChanged

        '| ---------------------------------------
        '| | ACTUALIZAR BUSCADOR SEGÚN EL FILTRO |
        '| ---------------------------------------

        '| Evita ejecutar filtros mientras el formulario está limpiando o restaurando controles.
        If _isCleaning Then Exit Sub

        If String.IsNullOrWhiteSpace(CmbFilter.Text) Then

            '| Mensaje informativo.
            TxtSearch.Text = AppMessages.SelectSearchFilter

            UpdateSearchVisualFeedback(0)
            DgvClientList.DataSource = Nothing
        Else
            TxtSearch.Clear()
            TxtSearch.Focus()
        End If

    End Sub


    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged

        '| -----------------------------------
        '| | FILTRAR CLIENTES EN TIEMPO REAL |
        '| -----------------------------------

        '| Evita ejecutar filtros mientras el formulario está limpiando o restaurando controles.
        If _isCleaning Then Exit Sub

        If String.IsNullOrWhiteSpace(CmbFilter.Text) Then

            '| Mensaje informativo.
            TxtSearch.Text = AppMessages.SelectSearchFilter

            UpdateSearchVisualFeedback(0)
        Else
            FilterAndRenderClientGridUI()
        End If

    End Sub
    Private Sub TxtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSearch.KeyPress

        '| ------------------------------------
        '| | VALIDAR EL INGRESO DE CARACTERES |
        '| ------------------------------------
        '| * Restringe los caracteres permitidos según el tipo de filtro seleccionado.
        '|   - Nombre/Apellido: solo letras y algunos caracteres especiales.
        '|   - Teléfono: solo números y símbolos permitidos para teléfonos.
        '|
        '| * Se bloquean º y ª porque algunos teclados españoles los generan accidentalmente
        '|   al usar búsquedas rápidas.

        Select Case CmbFilter.Text.Trim

            Case SearchFilters.ByName, SearchFilters.ByLastName

                Dim strAllowKey As String = " "
                Dim strLockKey As String = "ºª"
                AllowOnlyLetters(e, strAllowKey, strLockKey)

            Case SearchFilters.ByPhone

                Dim strAllowKey As String = "(-) "
                AllowOnlyIntegers(e, strAllowKey)

        End Select

    End Sub


    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        LoadClientAndPaymentsData()
    End Sub


    Private Sub DgvClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvClientList.CellContentClick
    End Sub
    Private Sub DgvClientList_DoubleClick(sender As Object, e As EventArgs) Handles DgvClientList.DoubleClick
        LoadClientAndPaymentsData()
    End Sub


    Private Sub RbActive_CheckedChanged(sender As Object, e As EventArgs) Handles RbActive.CheckedChanged

        '| ----------------------------------------
        '| | MOSTRAR CLIENTES ACTIVOS / INACTIVOS |
        '| ----------------------------------------

        _currentState = If(RbActive.Checked, EntityStatus.Active, EntityStatus.Inactive)

        If RbActive.Checked Then

            ProcessLabelsRecursive(PnlDataClient, "Cli",
                                   Sub(label) label.ForeColor = Color.MediumBlue)
            DgvClientList.RowsDefaultCellStyle.BackColor = Color.Lavender
            DgvClientList.Refresh()

        End If

        If _isCleaning Then Exit Sub

        TxtSearch.Clear()
        TxtSearch.Focus()

        FilterAndRenderClientGridUI()

    End Sub


    Private Sub RbInactive_CheckedChanged(sender As Object, e As EventArgs) Handles RbInactive.CheckedChanged

        If RbInactive.Checked Then

            ProcessLabelsRecursive(PnlDataClient, "Cli",
                                   Sub(label) label.ForeColor = Color.DarkRed)
            DgvClientList.RowsDefaultCellStyle.BackColor = Color.MistyRose
            DgvClientList.Refresh()

        End If

    End Sub


    Private Sub BtnCancelSearch_Click(sender As Object, e As EventArgs) Handles BtnCancelSearch.Click

        '| ------------------------
        '| | CANCELAR LA BUSQUEDA |
        '| ------------------------
        '| Restablece el estado visual del buscador y limpia el filtro actual.

        _isCleaning = True

        DisableSearchRecord()

        TxtSearch.Clear()

        If String.IsNullOrWhiteSpace(CmbFilter.Text) Then TxtSearch.BackColor = Color.Snow

        _isCleaning = False

    End Sub


    Private Sub BtnNewClient_Click(sender As Object, e As EventArgs) Handles BtnNewClient.Click

        '| --------------------------------------------------
        '| | REGISTRAR UN NUEVO CLIENTE EN LA BASE DE DATOS |
        '| --------------------------------------------------

        ProcessLabelsRecursive(PnlDataClient, "Cli",
                                   Sub(label) label.Text = "")
        DgvPaymentList.DataSource = Nothing
        DisableButtons()

        NavigateToForm.OpenFrmNewClient(AddressOf GlobalRefresh)

    End Sub


    Private Sub BtnModifyData_Click(sender As Object, e As EventArgs) Handles BtnModifyData.Click

        '| --------------------------------------
        '| | ACTUALIZAR INFORMACION DEL CLIENTE |
        '| --------------------------------------

        If _selectedClient IsNot Nothing Then

            ' Verifica si el cliente tiene deudas pendientes antes de abrir el formulario de edición.
            ' Busca en el historial que acabamos de cargar si hay algún "IMPAGO"
            Dim hasDebt As Boolean = _historyList.Any(Function(p) p.HasDebtCustomer)

            ' Marcamos la bandera en el objeto que vamos a enviar al formulario de edición
            _selectedClient.HasDebtCustomer = hasDebt

            ' Abrimos el formulario (usando tu método de navegación)
            NavigateToForm.OpenFrmModifyClient(_selectedClient, AddressOf GlobalRefresh)

        End If

    End Sub


    Private Sub BtnDeleteClient_Click(sender As Object, e As EventArgs) Handles BtnDeleteClient.Click

        '| ---------------------------------
        '| | ELIMINAR O DESACTIVAR CLIENTE |
        '| ---------------------------------

        '| Validación de seguridad.
        If _selectedClient Is Nothing Then Exit Sub

        '| Preparamos los parametros para el mensaje.
        Dim fullName As String = $"{LblNomCli.Text} {LblApeCli.Text}"
        Dim customerCode As String = $"CLI - {_selectedClient.IdCli:D3}"

        '| Construcción del mensaje de confirmación.
        Dim result = MessageBox.Show(DeleteOrInactivateCustomerWarning(fullName, customerCode), "Eliminar registro",
                                     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)

        '| Procesamos la acción seleccionada.
        Select Case result

            '| ELIMINACIÓN TOTAL (Hard Delete)
            Case DialogResult.Yes

                Dim exito As Boolean = _clientManager.DeleteClientPermanently(_selectedClient.IdCli)

                If exito Then

                    ProcessLabelsRecursive(PnlDataClient, "Cli",
                                   Sub(label) label.Text = "")
                    DgvPaymentList.DataSource = Nothing
                    DisableButtons()
                    FetchClientsFromDatabase()
                    MessageBox.Show("Cliente e historial de pagos eliminados correctamente.", "Registro borrado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            '| PASAR A INACTIVO (Soft Delete)
            Case DialogResult.No

                _selectedClient.IdNewClient = _selectedClient.IdCli

                _selectedClient.State = EntityStatus.Inactive

                Dim exito As Boolean = _clientManager.UpdateClientProcess(_selectedClient, False, False)

                If exito Then
                    MessageBox.Show("El estado del cliente se ha cambiado a INACTIVO. Ya no generará deudas.",
                        "Cliente desactivado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    LoadClientVisualData(_selectedClient)

                    ProcessLabelsRecursive(PnlDataClient, "Cli",
                                           Sub(label) label.ForeColor = Color.DarkRed)
                Else
                    ' Si falló la BBDD, revertimos el cambio en memoria.
                    _selectedClient.State = EntityStatus.Active

                    MessageBox.Show("No se pudo actualizar el estado en la base de datos.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            '| CANCELAR ACCIÓN
            Case DialogResult.Cancel
                Exit Sub

        End Select

    End Sub


    Private Sub DgvPaymentList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPaymentList.CellContentClick
    End Sub
    Private Sub DgvPaymentList_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvPaymentList.CellFormatting

        '| ----------------------------------------
        '| | RESALTA VISUALMENTE PAGOS PENDIENTES |
        '| ----------------------------------------

        If e.RowIndex < 0 Then Exit Sub

        Dim dgv = DirectCast(sender, DataGridView)
        Dim payment = DirectCast(dgv.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)

        If IsDateNotAssigned(payment.FdpPgs) Then

            dgv.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Red
            dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
            dgv.Rows(e.RowIndex).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)

        End If

        '| Formato monetario automático para columnas Decimal.
        If TypeOf e.Value Is Decimal Then e.CellStyle.Format = "C2"

    End Sub
    Private Sub DgvPaymentList_SelectionChanged(sender As Object, e As EventArgs) Handles DgvPaymentList.SelectionChanged

        '| ---------------------------------------
        '| | ACTIVAR O DESACTIVAR BOTON DE COBRO |
        '| ---------------------------------------
        '| Solo se permite seleccionar pagos pendientes.

        Dim payment = TryCast(DgvPaymentList.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

        If payment Is Nothing Then
            BtnCollectMonth.Enabled = False
            Exit Sub
        End If

        Dim hasDebt As Boolean = IsDateNotAssigned(payment.FdpPgs)

        BtnCollectMonth.Enabled = hasDebt

        If Not hasDebt Then
            DgvPaymentList.ClearSelection()
            DgvPaymentList.CurrentCell = Nothing
        End If

    End Sub


    Private Sub BtnCollectMonth_Click(sender As Object, e As EventArgs) Handles BtnCollectMonth.Click

        '| --------------------------------
        '| | COBRO DE CUOTA O MENSUALIDAD |
        '| --------------------------------

        Dim selectedPayment = TryCast(DgvPaymentList.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

        If selectedPayment IsNot Nothing AndAlso _selectedClient IsNot Nothing Then

            '| Copiamos información adicional del cliente necesaria para el formulario de cobro.
            selectedPayment.GroupName = _selectedClient.GroupName
            selectedPayment.FirstName = _selectedClient.FirstName
            selectedPayment.LastName = _selectedClient.LastName
            selectedPayment.Age = _selectedClient.Age

            NavigateToForm.OpenFrmCollectMembership(selectedPayment, AddressOf RefreshPaymentHistory)

        End If

        BtnCollectMonth.Enabled = False

    End Sub


    Private Sub BtnNewPayment_Click(sender As Object, e As EventArgs) Handles BtnNewPayment.Click

        '| ----------------------
        '| | GENERAR NUEVO PAGO |
        '| ----------------------

        '| Validaciones previas de negocio.
        If Not ValidateClientBeforePayment() Then Exit Sub

        '| Obtener tarifa base correspondiente.
        Dim rate = _clientManager.GetApplicableRate(_selectedClient)

        If Not rate.Exists Then
            MessageBox.Show("No se encontró una TARIFA válida en la BBDD.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        '| Preparación de variables de cálculo.
        Dim paymentMethod = _selectedClient.PaymentMethod.ToUpper()
        Dim proposedDate = CalculateProposedDate(paymentMethod)
        Dim groupPrice = rate.Price

        '| Multiplicación de precio si es grupal.
        If paymentMethod.Contains(PaymentMethods.Grupal) AndAlso _selectedClient.IdGroup.HasValue Then

            Dim mumberMembers = _clientManager.GetNumberMembers(_selectedClient.IdGroup.Value)

            If mumberMembers > 0 Then groupPrice = rate.Price * mumberMembers

        End If

        '| Fabricar el pago final usando función limpia.
        Dim newPayment = CreatePaymentDTO(paymentMethod, proposedDate, groupPrice, rate.Discount)

        '| Lanzar formulario.
        NavigateToForm.OpenFrmCollectMembership(newPayment, AddressOf RefreshPaymentHistory,
                                                FrmCollectMembership.TransactionMode.NewPayment)

    End Sub


    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles BtnCloseWindow.Click
        Me.Close()
    End Sub

#End Region

    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " 1. PUNTOS DE ENTRADA Y ORQUESTACIÓN DE CARGA "
    ' Métodos públicos o mayores encargados de coordinar
    ' las cargas masivas coordinando datos e interfaz.

    ''' <summary>
    ''' Refresca la lista general de clientes y vuelve a cargar
    ''' la información visual del cliente especificado.
    ''' </summary>
    ''' <param name="clientId">
    ''' Identificador del cliente que debe localizarse y recargarse.
    ''' </param>
    ''' <param name="refreshPayments">
    ''' Indica si también debe refrescarse el historial de pagos.
    ''' </param>
    Public Sub GlobalRefresh(clientId As Integer,
                             Optional refreshPayments As Boolean = True)

        FetchClientsFromDatabase()

        Dim client = _clientList.FirstOrDefault(Function(c) c.IdCli = clientId)

        If client IsNot Nothing Then LoadClientAndPaymentsData(client, refreshPayments)

    End Sub


    ''' <summary>
    ''' Carga la información visual del cliente seleccionado y, opcionalmente,
    ''' actualiza su historial de pagos en la interfaz.
    ''' </summary>
    ''' <param name="client">
    ''' Cliente que se utilizará para cargar la información.
    ''' Si es Nothing, se intentará obtener el cliente seleccionado
    ''' actualmente en el DataGridView.
    ''' </param>
    ''' <param name="refreshPayments">
    ''' Indica si también debe actualizarse el historial de pagos.
    ''' </param>
    ''' <remarks>
    ''' Esta función centraliza la actualización visual del formulario
    ''' y utiliza la variable _isCleaning como semáforo para evitar
    ''' ejecuciones innecesarias de eventos durante la carga de datos.
    ''' </remarks>
    Sub LoadClientAndPaymentsData(Optional client As IndividualPaymentDTO = Nothing,
                                  Optional refreshPayments As Boolean = True)

        _isCleaning = True

        '| Determina el cliente que se debe cargar.
        If client IsNot Nothing Then
            _selectedClient = client

        ElseIf DgvClientList.CurrentRow IsNot Nothing Then
            _selectedClient = DirectCast(DgvClientList.CurrentRow.DataBoundItem, IndividualPaymentDTO)

        Else
            Exit Sub
        End If

        LoadClientVisualData(_selectedClient)

        If refreshPayments Then LoadClientPaymentsHistory(_selectedClient)

        DisableSearchRecord()
        ActivateButtons()

        TxtSearch.Clear()

        _isCleaning = False

    End Sub


    ''' <summary>
    ''' Actualiza la lista general de clientes disponible
    ''' para búsquedas y operaciones del formulario.
    ''' </summary>
    ''' <remarks>
    ''' También habilita o deshabilita el botón de búsqueda
    ''' dependiendo de si existen clientes registrados.
    ''' </remarks>
    Private Sub FetchClientsFromDatabase()

        BtnFindClient.Enabled = _clientManager.HasClients()

        If BtnFindClient.Enabled Then _clientList = _clientManager.GetClientsForSearch()

    End Sub

#End Region

#Region " 2. CARGA DE DATOS Y ESTADO VISUAL DEL CLIENTE "
    ' Métodos expertos en tomar un cliente específico y volcar su información tanto en el historial como en las etiquetas.

    ''' <summary>
    ''' Actualiza los controles visuales del formulario con la información
    ''' principal del cliente seleccionado.
    ''' </summary>
    ''' <param name="client">
    ''' Cliente cuyos datos serán mostrados en pantalla.
    ''' </param>
    Private Sub LoadClientVisualData(client As IndividualPaymentDTO)

        FillLabelsClientData(client)

        If client.IdGroup.HasValue Then
            Dim groupName = _clientManager.GetGroupName(client.IdGroup.Value)
            client.GroupName = groupName
            LblGrpFamCli.Text = groupName
        Else
            client.GroupName = ""
            LblGrpFamCli.Text = ""
        End If

    End Sub


    ''' <summary>
    ''' Obtiene y muestra el historial de pagos del cliente seleccionado.
    ''' </summary>
    ''' <param name="client">
    ''' Cliente del cual se cargará el historial de pagos.
    ''' </param>
    ''' <remarks>
    ''' También actualiza la propiedad HasDebtCustomer para reflejar
    ''' si el cliente posee pagos pendientes.
    ''' </remarks>
    Private Sub LoadClientPaymentsHistory(client As IndividualPaymentDTO)

        _historyList = _paymentManager.GetPaymentHistory(client.IdCli, client.IdGroup)

        client.HasDebtCustomer = _historyList.Any(Function(p) p.HasDebtCustomer)

        DgvPaymentList.DataSource = _historyList
        DgvPaymentList.CurrentCell = Nothing

    End Sub


    ''' <summary>
    ''' Muestra en los Labels del formulario la información
    ''' básica del cliente seleccionado.
    ''' </summary>
    ''' <param name="client">
    ''' Cliente cuyos datos serán asignados a los controles visuales.
    ''' </param>
    Sub FillLabelsClientData(client As IndividualPaymentDTO)

        LblNomCli.Text = client.FirstName
        LblApeCli.Text = client.LastName
        LblFdnCli.Text = FormatLongDate(client.BirthDate)
        LblEdadCli.Text = client.AgeText
        LblTlfCli.Text = client.Phone
        LblEmlCli.Text = client.Email
        LblDirCli.Text = client.Address
        LblMtdPgoCli.Text = client.PaymentMethod
        LblFdiCli.Text = FormatLongDate(client.RegistrationDate)
        LblEstCli.Text = GetStatusDescription(client.State)
        LblGrpFamCli.Text = client.IdGroup

    End Sub




#End Region

#Region " 3. COSMÉTICA, HELPERS VISUALES Y CONTROL DE COMPONENTES UI "
    ' Métodos dedicados al control fino de la interfaz: activar o desactivar botones,
    ' cajas de búsqueda y dar retroalimentación visual al usuario en tiempo real.

    ''' <summary>
    ''' Habilita los controles principales para operar
    ''' con el cliente seleccionado.
    ''' </summary>
    Private Sub ActivateButtons()

        BtnFindClient.Enabled = True
        BtnModifyData.Enabled = True
        BtnDeleteClient.Enabled = True
        BtnNewPayment.Enabled = True
        DgvPaymentList.Enabled = True

    End Sub


    ''' <summary>
    ''' Deshabilita los controles que requieren
    ''' un cliente seleccionado.
    ''' </summary>
    Private Sub DisableButtons()

        BtnModifyData.Enabled = False
        BtnDeleteClient.Enabled = False
        BtnCollectMonth.Enabled = False
        BtnNewPayment.Enabled = False
        DgvPaymentList.Enabled = False

    End Sub


    ''' <summary>
    ''' Activa el modo búsqueda y prepara los controles
    ''' visuales necesarios para filtrar clientes.
    ''' </summary>
    Private Sub ActivateSearchRecord()

        BtnNewClient.Enabled = False
        BtnFindClient.Visible = False
        BtnCancelSearch.Visible = True
        PnlBuscar.Enabled = True
        GbState.Enabled = True
        CmbFilter.SelectedIndex = 1
        RbActive.Checked = True
        DgvClientList.Visible = True
        LblResult.Visible = True
        DgvClientList.BringToFront()

    End Sub


    ''' <summary>
    ''' Restaura el formulario al estado normal,
    ''' desactivando el modo búsqueda.
    ''' </summary>
    ''' <remarks>
    ''' Esta función:
    ''' - Oculta controles de búsqueda.
    ''' - Restablece filtros.
    ''' - Deshabilita opciones visuales relacionadas.
    ''' </remarks>
    Private Sub DisableSearchRecord()

        BtnNewClient.Enabled = True
        BtnFindClient.Visible = True
        BtnCancelSearch.Visible = False
        PnlBuscar.Enabled = False
        CmbFilter.SelectedIndex = 0
        RbActive.Checked = False
        DgvClientList.Visible = False
        LblResult.Visible = False
        GbState.Enabled = False

    End Sub


    ''' <summary>
    ''' Actualiza los colores y el texto informativo del buscador
    ''' según la cantidad de registros encontrados.
    ''' </summary>
    Private Sub UpdateSearchVisualFeedback(resultCount As Integer)

        Dim hasResults As Boolean = resultCount > 0

        TxtSearch.BackColor = If(hasResults, Color.Snow, Color.MistyRose)
        TxtSearch.ForeColor = If(hasResults, Color.MediumBlue, Color.Red)
        LblResult.ForeColor = If(hasResults, Color.Gray, Color.Red)

        Dim resultText As String = If(resultCount = 1,
                                        AppMessages.SearchSingleResult,
                                        AppMessages.SearchMultipleResults)

        LblResult.Text = $"{resultCount} - {resultText}"

    End Sub

#End Region

#Region " 4. REFRESCO DE LISTAS Y GRIDS (Renderizado de Tablas) "
    ' Métodos encargados de sincronizar las colecciones de la base de datos
    ' con los controles visuales de la interfaz.

    ''' <summary>
    ''' Filtra y actualiza la lista de clientes según el estado
    ''' seleccionado y el texto de búsqueda ingresado por el usuario.
    ''' 
    ''' También:
    ''' - Actualiza el feedback visual de la búsqueda.
    ''' - Refresca el DataGridView.
    ''' - Posiciona la selección en la primera coincidencia.
    ''' - Devuelve el foco al cuadro de búsqueda.
    ''' </summary>
    Private Sub FilterAndRenderClientGridUI()

        If _clientList Is Nothing Then Exit Sub

        ' 1. Filtrado inicial por Estado
        Dim filteredList = _clientList.Where(Function(c) c.State = _currentState)
        Dim strSearch = TxtSearch.Text.Trim().ToUpper()

        ' 2. Obtenemos la configuración del filtro (columna y la función lambda)
        Dim config = GetFilterConfiguration()

        ' Aplicamos el filtro dinámico solo si el usuario escribió algo
        If Not String.IsNullOrEmpty(strSearch) Then
            filteredList = filteredList.Where(config.predicate)
        End If

        ' 3. Conversión a lista y feedback visual
        Dim listCounter = filteredList.ToList()
        UpdateSearchVisualFeedback(listCounter.Count)

        ' 4. Enlace de datos al DataGridView
        DgvClientList.AutoGenerateColumns = False
        DgvClientList.DataSource = listCounter

        ' 5. Posicionar la selección en la primera coincidencia
        If listCounter.Count > 0 Then SelectFirstCell(config.columnName)

        ' 6. Mantener el cursor listo en el buscador
        TxtSearch.Focus()
        TxtSearch.SelectionStart = TxtSearch.Text.Length

    End Sub



    ''' <summary>
    ''' Refresca el historial de pagos del cliente actualmente seleccionado
    ''' y sincroniza la información visual de la interfaz.
    ''' </summary>
    ''' <remarks>
    ''' También actualiza la propiedad HasDebtCustomer para mantener
    ''' sincronizado el estado financiero del cliente en memoria.
    ''' </remarks>
    Private Sub RefreshPaymentHistory()

        '| Si no hay cliente seleccionado, no existe historial que actualizar.
        If _selectedClient IsNot Nothing Then

            Dim updatedHistory = _paymentManager.GetPaymentHistory(_selectedClient.IdCli, _selectedClient.IdGroup)

            _historyList = updatedHistory

            DgvPaymentList.DataSource = _historyList
            DgvPaymentList.CurrentCell = Nothing

            _selectedClient.HasDebtCustomer = _historyList.Any(Function(p) p.HasDebtCustomer)

        End If

    End Sub


    ''' <summary>
    ''' Selecciona la primera celda visible del DataGridView
    ''' según el nombre de columna indicado.
    ''' </summary>
    Private Sub SelectFirstCell(columnName As String)

        If String.IsNullOrWhiteSpace(columnName) Then Exit Sub

        If DgvClientList.Rows.Count = 0 Then Exit Sub

        Try
            DgvClientList.CurrentCell = DgvClientList.Rows(0).Cells(columnName)

        Catch ex As Exception
            Debug.WriteLine($"Error al seleccionar celda: {ex.Message}")
        End Try

    End Sub

#End Region

#Region " 5. CONFIGURACIÓN DE FILTROS Y VALIDACIONES "
    ' La capa de seguridad que decide si las acciones son permitidas
    ' y cómo se debe filtrar la información.

    ''' <summary>
    ''' Valida si el cliente seleccionado puede registrar
    ''' un nuevo pago.
    ''' </summary>
    ''' <returns>
    ''' True si el proceso puede continuar;
    ''' False si existe alguna restricción.
    ''' </returns>
    ''' <remarks>
    ''' Se validan:
    ''' - Cliente seleccionado.
    ''' - Ventanas duplicadas de cobro.
    ''' - Existencia de pagos pendientes.
    ''' </remarks>
    Private Function ValidateClientBeforePayment() As Boolean

        If _selectedClient Is Nothing Then Return False

        Dim frmOpen = FrmMdiMain.MdiChildren.OfType(Of FrmCollectMembership)().FirstOrDefault()

        If frmOpen IsNot Nothing Then
            frmOpen.BringToFront()
            frmOpen.Activate()
            Return False
        End If

        If _selectedClient.HasDebtCustomer Then

            MessageBox.Show(PendingDebtWarning("Antes de cobrar una nueva mensualidad"),
                            "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False

        End If

        Return True

    End Function


    ''' <summary>
    ''' Obtiene la configuración del filtro de búsqueda seleccionada
    ''' por el usuario.
    ''' 
    ''' Devuelve:
    ''' - El nombre de la columna que debe seleccionarse en el DataGridView.
    ''' - La condición de filtrado que se aplicará sobre la lista de clientes.
    ''' </summary>
    Private Function GetFilterConfiguration() As (
        columnName As String,
        predicate As Func(Of IndividualPaymentDTO, Boolean))

        Dim searchText = TxtSearch.Text.Trim()

        Select Case CmbFilter.Text.Trim

            Case SearchFilters.ByName

                Return ("NomCli", Function(c) c.FirstName.StartsWith(searchText))

            Case SearchFilters.ByLastName

                Return ("ApeCli", Function(c) c.LastName.StartsWith(searchText))

            Case SearchFilters.ByPhone

                Return ("TlfCli", Function(c) c.Phone.StartsWith(searchText))

            Case Else
                Return (Nothing, Function(c) True)

        End Select

    End Function

#End Region

#Region " 6. FÁBRICA DE DATOS Y CÁLCULOS DE NEGOCIO (Factory) "
    ' Funciones puras que procesan fechas, calculan períodos
    ' o construyen nuevas instancias de objetos de pago.

    ''' <summary>
    ''' Calcula la fecha sugerida para registrar
    ''' un nuevo pago según el método de pago.
    ''' </summary>
    ''' <param name="paymentMethod">
    ''' Método de pago del cliente.
    ''' </param>
    ''' <returns>
    ''' Fecha inicial sugerida para el nuevo pago.
    ''' </returns>
    Private Function CalculateProposedDate(paymentMethod As String) As Date

        If paymentMethod.Contains(PaymentMethods.Daily) Then
            Return Date.Today

        Else
            Dim nextMonth = Date.Today.AddMonths(1)
            Return New Date(nextMonth.Year, nextMonth.Month, 1)

        End If

    End Function

    ''' <summary>
    ''' Fabrica el DTO correspondiente según el tipo
    ''' de pago del cliente seleccionado.
    ''' </summary>
    ''' <param name="paymentMethod">
    ''' Método de pago que determinará el tipo de DTO.
    ''' </param>
    ''' <param name="startDate">
    ''' Fecha inicial del nuevo pago.
    ''' </param>
    ''' <param name="price">
    ''' Importe calculado del pago.
    ''' </param>
    ''' <param name="discount">
    ''' Descuento aplicado al pago.
    ''' </param>
    ''' <returns>
    ''' Un objeto que implementa IPaymentCalculable.
    ''' </returns>
    ''' <remarks>
    ''' - Los pagos grupales generan un GroupPaymentDTO.
    ''' - Los pagos individuales generan un IndividualPaymentDTO.
    ''' </remarks>
    Private Function CreatePaymentDTO(paymentMethod As String, startDate As Date,
                                      price As Decimal, discount As Decimal) As IPaymentCalculable

        If paymentMethod.Contains(PaymentMethods.Grupal) AndAlso _selectedClient.IdGroup.HasValue Then

            ' SI ES GRUPAL: Instanciamos el DTO de grupos familiares
            Return New GroupPaymentDTO With
                {
                    .IdGrp = _selectedClient.IdGroup.Value,
                    .GroupName = _selectedClient.GroupName,
                    .GroupMembers = _clientManager.GetGroupMembersNames(_selectedClient.IdGroup.Value),'CAPTURAMOS LOS INTEGRANTES
                    .MtdPgs = paymentMethod,
                    .FdiPgs = startDate,
                    .PrcPgs = price,
                    .DscPgs = discount
                }
        Else
            ' SI ES INDIVIDUAL (Mensual o Diario): Instanciamos el DTO individual
            Return New IndividualPaymentDTO With
                {
                    .IdCli = _selectedClient.IdCli,
                    .FirstName = _selectedClient.FirstName,
                    .LastName = _selectedClient.LastName,
                    .Age = _selectedClient.Age,
                    .MtdPgs = paymentMethod,
                    .FdiPgs = startDate,
                    .PrcPgs = price,
                    .DscPgs = discount
                }
        End If
    End Function

#End Region

End Class