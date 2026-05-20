Imports GymPaymentControl.Constants
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

Public Class FrmClientsPayments

    ' Instanciamos los servicios necesarios y acceder a la lógica de negocio.
    Private ReadOnly _clientManager As New ClientManager()
    Private ReadOnly _paymentManager As New PaymentManager()

    Private _clientList As List(Of IndividualPaymentDTO)
    Private _historyList As List(Of IndividualPaymentDTO)
    Private _selectedClient As IndividualPaymentDTO

    'variables
    Private _isCleaning As Boolean
    Private strState As String

    Private Sub FrmClientsPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            ' 1. Limpieza inicial
            DisableButtons()
            CleanControls()

            ' 2. Verificación inteligente
            ' Usamos el manager que ya tienes instanciado
            ' Si hay clientes, cargamos la lista inicial en memoria
            RefreshCustomerList()

            ' 1. Evitamos que el DGV cree columnas extra
            DgvPaymentList.AutoGenerateColumns = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Inicio")
        End Try

    End Sub

    Private Sub BtnFindClient_Click(sender As Object, e As EventArgs) Handles BtnFindClient.Click

        '| ---------------------------------------------------------------------------------------
        '| PREPARAMOS LOS CONTROLES PARA LA BUSQUEDA
        '| -----------------------------------------
        '| * Llamamos a las subrutinas ActivateSearchRecord, DisableButtons y CleanControls()
        '|   para activar/desactivar, mostrar/ocultar y limpiar los controles.

        _isCleaning = True

        ActivateSearchRecord()
        DisableButtons()
        CleanControls()
        RefreshClientList()

        _isCleaning = False

    End Sub

    Private Sub CmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFilter.SelectedIndexChanged
        '
        If _isCleaning Then Exit Sub

        If CmbFilter.SelectedIndex = 0 Then
            TxtSearch.Text = AppTexts.SelectSearchFilter
        Else
            TxtSearch.Clear()
            TxtSearch.Focus()
        End If

    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged

        If _isCleaning Then  Exit Sub

        If CmbFilter.SelectedIndex = 0 Then TxtSearch.Text = AppTexts.SelectSearchFilter

        RefreshClientList()

    End Sub

    Private Sub TxtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSearch.KeyPress

        '| --------------------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE CARACTERES
        '| --------------------------------
        '| * Almacenamos en la variable 'strAllowKey' los caracteres que queremos PERMITIR.
        '| * Almacenamos en la variable 'strLockKey' los caracteres que queremos EXCLUIR.
        '| * Llamamos a la subrutina Fun_Only_Letters() y Sub_Only_Numbers () según sea el caso y le 
        '|   pasamos las variables como parámetros.

        Select Case CmbFilter.SelectedIndex
            Case 1, 2 '"NAME" "LASTNAME"
                Dim strAllowKey As String = " "
                Dim strLockKey As String = "ºª"
                AllowOnlyLetters(e, strAllowKey, strLockKey)

            Case 3 '"PHONE"
                Dim strAllowKey As String = "(-) "
                AllowOnlyIntegers(e, strAllowKey)

        End Select

    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        '4
    End Sub


    Private Sub DgvClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvClientList.CellContentClick
    End Sub
    Private Sub DgvClientList_DoubleClick(sender As Object, e As EventArgs) Handles DgvClientList.DoubleClick

        ' 1. Cargamos los datos y el historial de pagos
        LoadClientAndPaymentsData()

    End Sub


    Private Sub RbActive_CheckedChanged(sender As Object, e As EventArgs) Handles RbActive.CheckedChanged

        strState = If(RbActive.Checked, CustomerStates.Active, CustomerStates.Inactive)

        If RbActive.Checked Then

            PaintLabelsRecursive(PnlDataClient, Color.MediumBlue)
            DgvClientList.RowsDefaultCellStyle.BackColor = Color.Lavender
            DgvClientList.Refresh()

        End If

        If _isCleaning Then Exit Sub

        TxtSearch.Clear()
        TxtSearch.Focus()

        RefreshClientList()

    End Sub

    Private Sub RbInactive_CheckedChanged(sender As Object, e As EventArgs) Handles RbInactive.CheckedChanged

        If RbInactive.Checked Then

            PaintLabelsRecursive(PnlDataClient, Color.DarkRed)
            DgvClientList.RowsDefaultCellStyle.BackColor = Color.MistyRose
            DgvClientList.Refresh()

        End If

    End Sub

    Private Sub BtnCancelSearch_Click(sender As Object, e As EventArgs) Handles BtnCancelSearch.Click

        '| ------------------------------------------------------------------------------------------
        '| CANCELAR LA BUSQUEDA
        '| --------------------
        '| * Llenamos la variable strFlags con la cadena "SKIP_SEARCH" que se usará en TxtBuscar para
        '|   hacer comprobaciones y evitar hacer consultas innecesarias.
        '|
        '| * Llamamos a las subrutina Sub_SelectRecord_CancelSearch() para activar, desactivar y
        '|   ocultar controles.
        '|
        '| * Limpiamos la variable strFlags para otras comprobaciones.

        _isCleaning = True ' Activamos la protección
        DisableSearchRecord()
        TxtSearch.Clear()
        If CmbFilter.SelectedIndex = 0 Then TxtSearch.BackColor = Color.Snow
        _isCleaning = False ' Desactivamos la protección para la siguiente pulsación de tecla.

    End Sub

    Private Sub BtnNewClient_Click(sender As Object, e As EventArgs) Handles BtnNewClient.Click

        '| -------------------------------------------------------------------------------------
        '| REGISTRAR UN NUEVO CLIENTE EN LA BASE DE DATOS
        '| ----------------------------------------------
        '| * Limpiamos los controles y todos los labels del contenedor PnlDatosCliente llamando
        '|   a la subrutina CleanControls().
        '|
        '| * Desactivamos los botones llamando a la subrutina DisableButtons().
        '|
        '| * 
        CleanControls()
        DisableButtons()

        NavigateToForm.OpenFrmNewClient(AddressOf GlobalRefresh)
        'NavigateToForm.OpenFrmNewClient(AddressOf GlobalRefreshAfterSave)

    End Sub


    Private Sub BtnModifyData_Click(sender As Object, e As EventArgs) Handles BtnModifyData.Click

        ' Suponiendo que ya tienes cargado tu DTO del cliente seleccionado
        If _selectedClient IsNot Nothing Then

            ' Verificamos si en el historial que acabamos de cargar hay algún "IMPAGO"
            ' Nota: Usamos nuestra nueva _historyList
            Dim hasDebt As Boolean = _historyList.Any(Function(p) p.HasDebtCustomer)

            ' Marcamos la bandera en el objeto que vamos a enviar al formulario de edición
            _selectedClient.HasDebtCustomer = hasDebt

            ' Abrimos el formulario (usando tu método de navegación)
            NavigateToForm.OpenFrmModifyClient(_selectedClient, AddressOf GlobalRefresh)
            'NavigateToForm.OpenFrmModifyClient(_selectedClient, AddressOf GlobalRefreshAfterUpdate)
        End If

    End Sub


    Private Sub BtnDeleteClient_Click(sender As Object, e As EventArgs) Handles BtnDeleteClient.Click

        ' 1. Filtro de seguridad por si no hay nadie seleccionado en el Grid
        If _selectedClient Is Nothing Then Exit Sub

        ' 2. Preparamos los parametros para el mensaje.
        Dim fullName As String = $"{LblNomCli.Text} {LblApeCli.Text}"
        Dim customerCode As String = $"CLI - {_selectedClient.IdCli:D3}"

        ' 3. Lanzamos el mensaje personalizado y capturamos la decisión.
        Dim result = MessageBox.Show(DeleteOrInactivateCustomerWarning(fullName, customerCode), "Eliminar registro",
                                     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)

        ' 4. Procesamos la acción elegida
        Select Case result

            ' ELIMINACIÓN TOTAL (Hard Delete)
            Case DialogResult.Yes

                ' Aquí llamas a tu mánager para borrar físicamente de la base de datos
                Dim exito As Boolean = _clientManager.DeleteClientPermanently(_selectedClient.IdCli)

                If exito Then

                    ' 1. Limpiamos la pantalla y congelamos botones
                    CleanControls()
                    DisableButtons()

                    ' 2. Refrescamos la lista interna y el Grid visual
                    RefreshCustomerList()

                    MessageBox.Show("Cliente e historial de pagos eliminados correctamente.", "Registro borrado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            ' PASAR A INACTIVO (Soft Delete)
            Case DialogResult.No

                ' 1. Copiamos el ID del Grid a la propiedad que usa tu comando SQL
                _selectedClient.IdNewClient = _selectedClient.IdCli

                ' 2. Modificamos el estado en el DTO que tenemos en memoria
                _selectedClient.State = CustomerStates.Inactive

                ' 3. Proceso del mánager pasando el DTO actualizado
                ' Pasamos False porque solo queremos actualizar los datos base
                Dim exito As Boolean = _clientManager.UpdateClientProcess(_selectedClient, False, False)

                If exito Then
                    MessageBox.Show("El estado del cliente se ha cambiado a INACTIVO. Ya no generará deudas.",
                        "Cliente desactivado", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' 4. Refrescar la lista, 
                    LoadClientVisualData(_selectedClient)

                    ' 5. Cambiamos el color de los textos
                    PaintLabelsRecursive(PnlDataClient, Color.DarkRed)

                Else
                    ' Si falló la BBDD, revertimos el cambio en memoria
                    _selectedClient.State = CustomerStates.Active
                    MessageBox.Show("No se pudo actualizar el estado en la base de datos.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If

            ' CANCELAR
            Case DialogResult.Cancel
                ' El usuario cerró el diálogo o pulsó Cancelar. Salimos sin tocar nada.
                Exit Sub

        End Select

    End Sub


    Private Sub DgvPaymentList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPaymentList.CellContentClick
    End Sub
    Private Sub DgvPaymentList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPaymentList.CellClick

        '| ----------------------------------------------------------------------------------------------------------------
        '| ACTIVAR O DESACTIVAR EL BOTON PARA PAGAR
        '| ----------------------------------------
        '|
        '| IF : Validamos que el clic no sea en el encabezado.
        '|
        '| * Usamos DirectCast para obtener el objeto completo de la fila y lo guardamos en 'payment'.
        '|
        '| IF : Evaluamos si el registro seleccionado está pagado o no para activar o desactivar 'BtnPBtnCollectMonth,
        '|      usando la PROPIEDAD del objeto.

        If e.RowIndex < 0 Then Exit Sub

        Dim payment = DirectCast(DgvPaymentList.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)

        If IsDateNotAssigned(payment.FdpPgs) Then
            BtnCollectMonth.Enabled = True
        Else
            BtnCollectMonth.Enabled = False
            DgvPaymentList.CurrentCell = Nothing
        End If

    End Sub
    Private Sub DgvPaymentList_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvPaymentList.CellFormatting

        If e.RowIndex < 0 Then Exit Sub

        ' Obtenemos el objeto vinculado a la fila
        Dim dgv = DirectCast(sender, DataGridView)
        Dim payment = DirectCast(dgv.Rows(e.RowIndex).DataBoundItem, IndividualPaymentDTO)

        ' 1. Verificamos si es un IMPAGO usando tu función existente
        If IsDateNotAssigned(payment.FdpPgs) Then
            ' Aplicamos tu estilo visual antiguo
            dgv.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Red
            dgv.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.MistyRose
            dgv.Rows(e.RowIndex).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)

        End If
        ' 3. Formato de Moneda (Solo si el valor es numérico y son las columnas de dinero)
        ' He simplificado la detección: si es Decimal, le ponemos moneda.
        If TypeOf e.Value Is Decimal Then e.CellStyle.Format = "C2"

    End Sub
    Private Sub DgvPaymentList_SelectionChanged(sender As Object, e As EventArgs) Handles DgvPaymentList.SelectionChanged
        ' ESTE BLOQUE DE CÓDIGO CUANDO SE USA EL TECLADO PARA CONSEGUIR ACTIVAR O DESACTIVAR EL BOTON DE PAGO
        '' 1. Intentamos obtener el pago de la fila actual de forma segura
        'Dim payment = TryCast(DgvPaymentList.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

        'If payment IsNot Nothing Then
        '    ' 2. El botón solo se activa si NO tiene startDate asignada (es un impago)
        '    BtnCollectMonth.Enabled = IsDateNotAssigned(payment.FdpPgs)
        'Else
        '    ' 3. Si no hay selección válida, desactivamos
        '    BtnCollectMonth.Enabled = False
        'End If
    End Sub


    Private Sub BtnCollectMonth_Click(sender As Object, e As EventArgs) Handles BtnCollectMonth.Click
        ''
        '' Obtenemos el objeto de la fila actual
        Dim selectedPayment = TryCast(DgvPaymentList.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

        If selectedPayment IsNot Nothing AndAlso _selectedClient IsNot Nothing Then
            ' Ahora estas líneas no darán error porque el DTO "hijo" tiene sus propias propiedades String
            selectedPayment.GroupName = _selectedClient.GroupName

            ' Aseguramos los datos básicos para el DisplayName
            selectedPayment.FirstName = _selectedClient.FirstName
            selectedPayment.LastName = _selectedClient.LastName
            selectedPayment.Age = _selectedClient.Age

            NavigateToForm.OpenFrmCollectMembership(selectedPayment, AddressOf RefreshPaymentHistory)
        End If

        'SOLO PARA PROBAR EL FUNCIONAMIENTO
        BtnCollectMonth.Enabled = False

    End Sub


    Private Sub BtnNewPayment_Click(sender As Object, e As EventArgs) Handles BtnNewPayment.Click

        ' 1. Filtro de seguridad y validaciones de negocio
        If Not ValidateClientBeforePayment() Then Exit Sub

        ' 2. Obtención de tarifa base
        Dim rate = _clientManager.GetApplicableRate(_selectedClient)
        If Not rate.Exists Then

            MessageBox.Show("No se encontró una TARIFA válida en la BBDD.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub

        End If

        ' 3. Preparación de variables de cálculo
        Dim paymentMethod = _selectedClient.PaymentMethod.ToUpper()
        Dim proposedDate = CalculateProposedDate(paymentMethod)
        Dim groupPrice = rate.Price

        ' 4. Multiplicación de price si es grupal
        If paymentMethod.Contains(PaymentMethods.Grupal) AndAlso _selectedClient.IdGroup.HasValue Then

            Dim mumberMembers = _clientManager.GetNumberMembers(_selectedClient.IdGroup.Value)

            If mumberMembers > 0 Then groupPrice = rate.Price * mumberMembers

        End If

        ' 5. Fabricamos el pago final usando nuestra función limpia
        Dim newPayment = CreatePaymentDTO(paymentMethod, proposedDate, groupPrice, rate.Discount)

        ' 6. Lanzamos el formulario
        NavigateToForm.OpenFrmCollectMembership(newPayment, AddressOf RefreshPaymentHistory,
                                                TransactionMode.NewPayment)

    End Sub


    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles BtnCloseWindow.Click

        ' Como este formulario suele ser el "Buscador Principal", Close es directo.
        Me.Close()

    End Sub

    '| -------------------------------------------------------------------------- '
    '| ---------->>>>>>>>>> FUNCIONES - MÉTODOS - SUBRUTINAS <<<<<<<<<<---------- '
    '| -------------------------------------------------------------------------- '

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
    Private Sub RefreshClientList()

        If _clientList Is Nothing Then Exit Sub

        ' 1. Filtrado inicial por Estado
        Dim filteredList = _clientList.Where(Function(c) c.State = strState)
        Dim strSearch = TxtSearch.Text.Trim().ToUpper()

        ' 2. 🚀 CONSUMIMOS TU OBRA DE ARTE
        ' Obtenemos la configuración del filtro (columna y la función lambda mágica)
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
        'DgvClientList.DataSource = Nothing
        DgvClientList.DataSource = listCounter

        ' 5. Foco quirúrgico en la celda correspondiente gracias a tu tupla
        ' Si el buscador está vacío, config.columnName será Nothing y saltará limpiamente
        If listCounter.Count > 0 Then SelectFirstCell(config.columnName)

        ' 6. Mantener el cursor listo en el buscador
        TxtSearch.Focus()
        TxtSearch.SelectionStart = TxtSearch.Text.Length
    End Sub


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

        Dim searchText = TxtSearch.Text.Trim() '.ToUpper()

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


    ''' <summary>
    ''' Actualiza los colores y el texto informativo del buscador
    ''' según la cantidad de registros encontrados.
    ''' </summary>
    Private Sub UpdateSearchVisualFeedback(resultCount As Integer)

        Dim hasResults As Boolean = resultCount > 0

        TxtSearch.BackColor = If(hasResults, Color.Snow, Color.MistyRose)
        TxtSearch.ForeColor = If(hasResults, Color.MediumBlue, Color.Red)
        LblResult.ForeColor = If(hasResults, Color.Gray, Color.Red)

        Dim resultText As String =
        If(resultCount = 1,
           AppTexts.SearchSingleResult,
           AppTexts.SearchMultipleResults)

        LblResult.Text = $"{resultCount} - {resultText}"

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
    ''' Indica si también debe actualizarse el historial de pagos
    ''' del cliente seleccionado.
    ''' </param>
    ''' <remarks>
    ''' Esta función centraliza la actualización visual de la pantalla:
    ''' 
    ''' 1. Determina el cliente que se debe cargar.
    ''' 2. Actualiza los datos visuales del cliente.
    ''' 3. Refresca el historial de pagos si es necesario.
    ''' 4. Ajusta el estado de los controles y botones de la interfaz.
    ''' 
    ''' La variable _isCleaning se utiliza como semáforo para evitar
    ''' ejecuciones no deseadas de eventos mientras se actualizan
    ''' los controles del formulario.
    ''' </remarks>
    Sub LoadClientAndPaymentsData(Optional client As IndividualPaymentDTO = Nothing,
                              Optional refreshPayments As Boolean = True)

        _isCleaning = True

        ' 1. Decisión inteligente: ¿Me pasaron un cliente o lo busco en el Grid?
        If client IsNot Nothing Then
            _selectedClient = client

        ElseIf DgvClientList.CurrentRow IsNot Nothing Then
            _selectedClient = DirectCast(DgvClientList.CurrentRow.DataBoundItem, IndividualPaymentDTO)

        Else
            ' Si no hay nada, salimos
            Exit Sub
        End If

        ' 2. Actualizamos la información del cliente
        LoadClientVisualData(_selectedClient)

        ' 3. Solo se refresca el historial de pagos si es un nuevo registro
        If refreshPayments Then LoadClientPaymentsHistory(_selectedClient)

        ' 4. Estados de la interfaz
        DisableSearchRecord()
        ActivateButtons()
        TxtSearch.Clear()

        _isCleaning = False

    End Sub


    ''' <summary>
    ''' Selecciona la primera celda visible del DataGridView
    ''' según el nombre de columna indicado.
    ''' </summary>
    Private Sub SelectFirstCell(columnName As String)

        If String.IsNullOrWhiteSpace(columnName) Then Exit Sub

        If DgvClientList.Rows.Count = 0 Then Exit Sub

        Try
            DgvClientList.CurrentCell =
            DgvClientList.Rows(0).Cells(columnName)

        Catch ex As Exception
            Debug.WriteLine($"Error al seleccionar celda: {ex.Message}")
        End Try

    End Sub


    Private Sub LoadClientVisualData(client As IndividualPaymentDTO)

        ' 1. Llenamos Labels de texto principales
        FillLabelsClientData(client)

        ' 2. Lógica de Grupo
        If client.IdGroup.HasValue Then
            Dim groupName = _clientManager.GetGroupName(client.IdGroup.Value)
            client.GroupName = groupName
            LblGrpFamCli.Text = groupName
        Else
            client.GroupName = ""
            LblGrpFamCli.Text = ""
        End If

    End Sub


    Private Sub LoadClientPaymentsHistory(client As IndividualPaymentDTO)

        ' 1. Buscamos el historial en la base de datos
        _historyList = _paymentManager.GetPaymentHistory(client.IdCli, client.IdGroup)

        ' 2. Actualizamos la propiedad de deuda
        client.HasDebtCustomer = _historyList.Any(Function(p) p.HasDebtCustomer)

        ' 3. Cargamos el Grid de pagos
        DgvPaymentList.DataSource = _historyList
        DgvPaymentList.CurrentCell = Nothing

    End Sub


    Sub FillLabelsClientData(client As IndividualPaymentDTO)

        LblNomCli.Text = client.FirstName
        LblApeCli.Text = client.LastName
        LblFdnCli.Text = ConvertVeryLongDate(client.BirthDate)
        LblEdadCli.Text = client.AgeText
        LblTlfCli.Text = client.Phone
        LblEmlCli.Text = client.Email
        LblDirCli.Text = client.Address
        LblMtdPgoCli.Text = client.PaymentMethod
        LblFdiCli.Text = ConvertVeryLongDate(client.RegistrationDate)
        LblEstCli.Text = client.State
        LblGrpFamCli.Text = client.IdGroup

    End Sub


    Private Sub RefreshPaymentHistory()

        ' Verificamos que no sea Nothing por seguridad, si no hay cliente, no hay historial que buscar.
        If _selectedClient IsNot Nothing Then
            ' 1. Traemos la info fresca
            Dim updatedHistory = _paymentManager.GetPaymentHistory(_selectedClient.IdCli, _selectedClient.IdGroup)

            ' 2. ACTUALIZAMOS LA VARIABLE GLOBAL
            _historyList = updatedHistory

            ' 3. Refrescamos visualmente el Grid
            DgvPaymentList.DataSource = _historyList
            DgvPaymentList.CurrentCell = Nothing

            ' 4. Sincronizamos el objeto seleccionado
            _selectedClient.HasDebtCustomer = _historyList.Any(Function(p) p.HasDebtCustomer)
        End If

    End Sub


    Public Sub GlobalRefresh(clientId As Integer,
                             Optional refreshPayments As Boolean = True)

        ' 1. Refrescamos la lista general de clientes (esto es obligatorio para ambos casos)
        RefreshCustomerList()

        ' 2. Buscamos el cliente actualizado dentro de la nueva lista
        Dim client = _clientList.FirstOrDefault(Function(c) c.IdCli = clientId)

        ' 3. Llamamos a nuestra función orquesta pasándole el control de pagos
        If client IsNot Nothing Then LoadClientAndPaymentsData(client, refreshPayments)

    End Sub


    Private Sub RefreshCustomerList()

        ' 1. Activar/Desactivar el botón que depende de la comprobación de HasClients()
        BtnFindClient.Enabled = _clientManager.HasClients()

        ' 2. Si el boton está activado llamamos a GetClientsForSearch()
        If BtnFindClient.Enabled Then _clientList = _clientManager.GetClientsForSearch()

    End Sub


    Private Sub ActivateButtons()

        '| -----------------------------
        '| * ACTIVAMOS LOS BOTONES
        '| -----------------------------

        BtnFindClient.Enabled = True
        BtnModifyData.Enabled = True
        BtnDeleteClient.Enabled = True
        'BtnCollectMonth.Enabled = True
        BtnNewPayment.Enabled = True
        DgvPaymentList.Enabled = True

    End Sub

    Private Sub DisableButtons()

        '| -----------------------------
        '| * DESACTIVAMOS LOS CONTROLES
        '| -----------------------------

        'BtnFindClient.Enabled = False
        BtnModifyData.Enabled = False
        BtnDeleteClient.Enabled = False
        BtnCollectMonth.Enabled = False
        BtnNewPayment.Enabled = False
        DgvPaymentList.Enabled = False

    End Sub

    Private Sub DisableSearchRecord()

        '| * Activamos el botón BtnNewClient.
        '| * Mostramos el botón BtnCancelSearch y ocultamos el botón BtnFindClient.
        '| * Desactivamos el contenedor PnlBuscar.
        '| * Limpiamos el CmbFiltrar cambiando el index a cero.
        '| * Desactivamos el RbActivo, para que al momento de hacer clic en BtnBuscar nos muestre todos los clientes.
        '| * Ocultamos el DgvClientes.
        '| * Desactivvamos el GbEstado para no cambiar de valor los RadioButton.

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

    Private Sub ActivateSearchRecord()

        '| * Desactivamos el botón BtnNewClient.
        '| * Ocultamos el botón BtnFindClient y mostramos el botón BtnCancelSearch.
        '| * Activamos los dos contenedores PnlBuscar y GbEstado.
        '| * Cambiamos el texto del PnlBuscar seleccionando el index 1 (NOMBRE).
        '| * Activamos el RbActivo para mostrar y buscar los clientes en actividad.
        '| * Mostramos el DgvClientes y lo ponemos delante de los otros controles usando BringToFront.

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
    ''
    ''
    Private Sub CleanControls()

        '| * Recorremos todos los controles que están dentro del contenedor PnlDatosCliente.
        '|    * Comprobamos si los controles son de tipo Label.
        '|       * Comprobamos si el Name del control contiene "Cli".
        '|       * Limpiamos el texto del label.
        ''
        '| * Limpiamos la variable que contiene el id del cliente para hacer comprobaciones _
        '|   _ cuando se hacen cambios en el TxtBuscar.

        For Each control As Control In PnlDataClient.Controls
            If TypeOf (control) Is Label Then
                If control.Name.Contains("Cli") Then
                    control.Text = ""
                End If
            End If
        Next

        DgvPaymentList.DataSource = Nothing

    End Sub


    ' Función auxiliar recursiva: busca y pinta TODO, sin importar dónde esté escondido
    Private Sub PaintLabelsRecursive(parent As Control, targetColor As Color)

        For Each ctrl As Control In parent.Controls

            ' Si es un Label y cumple tu excelente regla de nomenclatura "Cli"
            If TypeOf ctrl Is Label AndAlso ctrl.Name.Contains("Cli") Then
                ctrl.ForeColor = targetColor
            End If

            ' Si el control es a su vez un contenedor (un Panel, otro GroupBox, etc.),
            ' se vuelve a llamar a sí misma para revisar lo que hay dentro.
            If ctrl.HasChildren Then PaintLabelsRecursive(ctrl, targetColor)

        Next

    End Sub


    ' 1. Soportes de Validación
    Private Function ValidateClientBeforePayment() As Boolean

        ' 1. Verificamos que haya un cliente seleccionado, si el objeto es Nothing, salimos sin ruido.
        If _selectedClient Is Nothing Then Return False

        ' 2. Verificación de Ventana Abierta (Prioridad: No abrir duplicados)
        Dim frmOpen = FrmMdiMain.MdiChildren.OfType(Of FrmCollectMembership)().FirstOrDefault()

        If frmOpen IsNot Nothing Then
            frmOpen.BringToFront()
            frmOpen.Activate()
            Return False
        End If

        ' 3. Comprobamos deudas pendientes usando nuestra propiedad sincronizada
        If _selectedClient.HasDebtCustomer Then

            MessageBox.Show(PendingDebtWarning("Antes de cobrar una nueva mensualidad"),
                            "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return False

        End If

        Return True

    End Function


    ' 2. Soporte para calcular la startDate sugerida
    Private Function CalculateProposedDate(paymentMethod As String) As Date

        If paymentMethod.Contains(PaymentMethods.Daily) Then
            Return Date.Today
        Else
            Dim nextMonth = Date.Today.AddMonths(1)
            Return New Date(nextMonth.Year, nextMonth.Month, 1)
        End If

    End Function


    ' 3. Soporte de fábrica de DTOs (Crea el objeto final)
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


End Class