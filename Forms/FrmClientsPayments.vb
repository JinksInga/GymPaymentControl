Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.Utils

Public Class FrmClientsPayments

    ' Instanciamos los servicios necesarios y acceder a la lógica de negocio.
    Private ReadOnly _clientManager As New ClientManager()
    Private ReadOnly _paymentManager As New PaymentManager()
    Private _clientList As List(Of IndividualPaymentDTO)
    Private _selectedClient As IndividualPaymentDTO

    'variables
    Private _isCleaning As Boolean ' = False
    Private strState As String

    Private Sub FrmClientsPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' 1. Limpieza inicial
            DisableButtons()
            CleanControls()

            ' 2. Verificación inteligente
            ' Usamos el manager que ya tienes instanciado
            BtnFindClient.Enabled = _clientManager.HasClients()

            ' Si hay clientes, cargamos la lista inicial en memoria
            If BtnFindClient.Enabled Then _clientList = _clientManager.GetClientsForSearch()

            ' 1. Evitamos que el DGV cree columnas extra
            DgvPaymentList.AutoGenerateColumns = False

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

    End Sub

    Private Sub BtnFindClient_Click(sender As Object, e As EventArgs) Handles BtnFindClient.Click

        '| ---------------------------------------------------------------------------------------
        '| PREPARAMOS LOS CONTROLES PARA LA BUSQUEDA
        '| -----------------------------------------
        '| * Llamamos a las subrutinas Sub_Search_Record(), Sub_Disable_Buttons() y Sub_Clean_Controls()
        '|   para activar/desactivar, mostrar/ocultar y limpiar los controles.

        _isCleaning = True

        ActivateSearchRecord()
        DisableButtons()
        CleanControls()
        RefreshDgvClientList()

        _isCleaning = False

    End Sub

    Private Sub CmbFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFilter.SelectedIndexChanged
        '
        If _isCleaning Then Exit Sub

        If CmbFilter.SelectedIndex = 0 Then
            TxtSearch.Text = "SELECCIONA UN FILTRO PARA LA BUSQUEDA"
        Else
            TxtSearch.Clear()
            TxtSearch.Focus()
        End If

    End Sub

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtSearch.TextChanged
        '
        If _isCleaning Then Exit Sub

        If CmbFilter.SelectedIndex = 0 Then TxtSearch.Text = "SELECCIONA UN FILTRO PARA LA BUSQUEDA"

        RefreshDgvClientList()

    End Sub

    Private Sub TxtSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSearch.KeyPress

        '| --------------------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE CARACTERES
        '| --------------------------------
        '| * Almacenamos en la variable 'strAllowKey' los caracteres que queremos PERMITIR.
        '| * Almacenamos en la variable 'strLockKey' los caracteres que queremos EXCLUIR.
        '| * Llamamos a la subrutina Fun_Only_Letters() y Sub_Only_Numbers () según sea el caso y le 
        '|   pasamos las variables como parámetros.

        Select Case CmbFilter.SelectedIndex  'strFilter
            Case 1, 2 '"NAME" "LASTNAME"
                Dim strAllowKey As String = " "
                Dim strLockKey As String = "ºª"
                ValidateOnlyLetters(strAllowKey, strLockKey, e)

            Case 3 '"PHONE"
                Dim strAllowKey As String = "(-) "
                ValidateIntegerNumbers(strAllowKey, e)

        End Select

    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        '4
    End Sub

    Private Sub DgvClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvClientList.CellContentClick
    End Sub
    Private Sub DgvClientList_DoubleClick(sender As Object, e As EventArgs) Handles DgvClientList.DoubleClick
        '
        UploadDataAndPayments()
    End Sub

    Private Sub RbActive_CheckedChanged(sender As Object, e As EventArgs) Handles RbActive.CheckedChanged
        '
        strState = If(RbActive.Checked, "ACTIVO", "INACTIVO")

        If _isCleaning Then Exit Sub

        TxtSearch.Clear()
        TxtSearch.Focus()

        RefreshDgvClientList()

    End Sub

    Private Sub RbInactive_CheckedChanged(sender As Object, e As EventArgs) Handles RbInactive.CheckedChanged
        '7
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
        '8
    End Sub

    Private Sub BtnModifyData_Click(sender As Object, e As EventArgs) Handles BtnModifyData.Click
        '9
    End Sub

    Private Sub BtnDeleteClient_Click(sender As Object, e As EventArgs) Handles BtnDeleteClient.Click
        '10
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
        '    ' 2. El botón solo se activa si NO tiene fecha asignada (es un impago)
        '    BtnCollectMonth.Enabled = IsDateNotAssigned(payment.FdpPgs)
        'Else
        '    ' 3. Si no hay selección válida, desactivamos
        '    BtnCollectMonth.Enabled = False
        'End If
    End Sub
    ''
    ''
    Private Sub BtnCollectMonth_Click(sender As Object, e As EventArgs) Handles BtnCollectMonth.Click
        ''
        '' Obtenemos el objeto de la fila actual
        Dim selectedPayment = TryCast(DgvPaymentList.CurrentRow?.DataBoundItem, IndividualPaymentDTO)

        If selectedPayment IsNot Nothing AndAlso _selectedClient IsNot Nothing Then
            ' Ahora estas líneas no darán error porque el DTO "hijo" tiene sus propias propiedades String
            selectedPayment.GroupName = _selectedClient.GroupName

            ' Aseguramos los datos básicos para el DisplayName
            selectedPayment.Name = _selectedClient.Name
            selectedPayment.LastName = _selectedClient.LastName
            selectedPayment.Age = _selectedClient.Age

            NavigateToForm.OpenFrmCollectMembership(selectedPayment, AddressOf RefreshPaymentHistory)
        End If

    End Sub

    Private Sub BtnNewPayment_Click(sender As Object, e As EventArgs) Handles BtnNewPayment.Click
        '13
    End Sub

    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles BtnCloseWindow.Click
        '14
    End Sub

    '| -------------------------------------------------------------------------- '
    '| ---------->>>>>>>>>> FUNCIONES - MÉTODOS - SUBRUTINAS <<<<<<<<<<---------- '
    '| -------------------------------------------------------------------------- '

    Private Sub RefreshDgvClientList()

        ' Si la lista maestra está vacía, no hacemos nada
        If _clientList Is Nothing Then Exit Sub

        ' 1. Filtramos primero por el estado (Activo/Inactivo)
        Dim filteredList = _clientList.Where(Function(c) c.State = strState)

        Dim strSearch = TxtSearch.Text.Trim()

        If Not String.IsNullOrEmpty(strSearch) Then

            Select Case CmbFilter.SelectedIndex 'strFilter

                Case 1 '"NAME"
                    filteredList = filteredList.Where(Function(c) c.Name.StartsWith(strSearch))

                Case 2 '"LASTNAME"
                    filteredList = filteredList.Where(Function(c) c.LastName.StartsWith(strSearch))

                Case 3 '"PHONE"
                    filteredList = filteredList.Where(Function(c) c.Phone.StartsWith(strSearch))

                Case Else
                    filteredList = filteredList.Where(Function(c) c.Name.StartsWith(strSearch))

            End Select

        End If

        Dim listCounter = filteredList.ToList()

        TxtSearch.BackColor = If(listCounter.Count = 0, Color.MistyRose, Color.Snow)
        TxtSearch.ForeColor = If(listCounter.Count = 0, Color.Red, Color.MediumBlue)
        LblResult.ForeColor = If(listCounter.Count = 0, Color.Red, Color.Gray)

        If listCounter.Count = 1 Then
            LblResult.Text = $"{listCounter.Count} - Registro que coincide con la búsqueda."
        Else
            LblResult.Text = $"{listCounter.Count} - Registros que coinciden con la búsqueda."
        End If

        DgvClientList.AutoGenerateColumns = False
        DgvClientList.DataSource = Nothing
        DgvClientList.DataSource = listCounter

        ' 4. Posicionamos la selección en la columna que se está filtrando
        If listCounter.Count > 0 Then
            Try

                Select Case CmbFilter.SelectedIndex 'strFilter
                    Case 1 '"NAME"
                        DgvClientList.CurrentCell = DgvClientList.Rows(0).Cells("NomCli")

                    Case 2 '"LASTNAME"
                        DgvClientList.CurrentCell = DgvClientList.Rows(0).Cells("ApeCli")

                    Case 3 '"PHONE"
                        DgvClientList.CurrentCell = DgvClientList.Rows(0).Cells("TlfCli")

                    Case Else
                        DgvClientList.CurrentCell = Nothing
                End Select

            Catch ex As Exception
                ' Si algo falla (ej: la columna no existe), el programa no se cierra
                Debug.WriteLine("Error al posicionar celda: " & ex.Message)
            End Try

        End If

        ' 5. Devolvemos el foco al buscador para poder seguir escribiendo
        TxtSearch.Focus()
        TxtSearch.SelectionStart = TxtSearch.Text.Length

    End Sub

    Sub UploadDataAndPayments()

        '| * Llenamos la variable 'strFlags' con la cadena "SKIP_SEARCH" que se usará en 'TxtBuscar' para
        '|   hacer comprobaciones y evitar hacer consultas inecesarias.
        '| * Pasamos la información del cliente seleccionado, del DgvClientes a los Labels, llamando a
        '|   la subrutina Sub_Fill_Data_DgvClientes().
        '|
        '| IF : Comprobamos si el 'LblGrpFamCli' tiene información para buscar un grupo familiar, si se cumple
        '|      la condición hacemos la consulta a la BBDD y lo almacenamos en la variable 'sqlConsulta'.
        '|      * Llamamos a la subrutina principal Sub_Crud_Sql() que va a recibir dos parametros, el primero
        '|        es la consulta y el segundo es la cadena 'SubFillGroupName' que servirá para el Select Case de
        '|        la subrutina principal que a su vez llama a la subrutina SubFillGroupName que se encarga de
        '|        recoger el nombre del grupo familiar.
        '|
        '| * Llamamos a las subrutina 'Sub_SelectRecord_CancelSearch()' y 'Sub_Activate_Buttons()' para
        '|   activar, desactivar y ocultar controles.
        '|
        '| * Llamamos a la subrutina Sub_View_Payment_List() para consultar los pagos del cliente.
        '|
        '| * Limpiamos la variable strFlags para futuras comprobaciones.

        _isCleaning = True
        ' 1. Obtenemos el objeto desde el Grid
        'Dim client = DirectCast(DgvClientList.CurrentRow.DataBoundItem, IndividualPaymentDTO)987
        _selectedClient = DirectCast(DgvClientList.CurrentRow.DataBoundItem, IndividualPaymentDTO)

        ' 2. Llenamos Labels de texto
        FillLabelsClientData(_selectedClient)

        If _selectedClient.IdGroup.HasValue Then
            'LblGrpFamCli.Text = _clientManager.GetGroupName(_selectedClient.IdGroup.Value)
            Dim strGroupName = _clientManager.GetGroupName(_selectedClient.IdGroup.Value)
            _selectedClient.GroupName = strGroupName
            LblGrpFamCli.Text = strGroupName
        Else
            _selectedClient.GroupName = ""
            LblGrpFamCli.Text = ""
        End If
        ' 4. Cargamos el historial de pagos (Usando PaymentManager)
        ' El DGV de pagos ahora recibe una LISTA de objetos, no un DataTable
        DgvPaymentList.DataSource = _paymentManager.GetPaymentHistory(_selectedClient.IdCli, _selectedClient.IdGroup)
        ' Lo ponemos aquí, una sola vez después de cargar
        DgvPaymentList.CurrentCell = Nothing

        DisableSearchRecord()
        ActivateButtons()

        _isCleaning = False

    End Sub

    Sub FillLabelsClientData(client As IndividualPaymentDTO)

        LblNomCli.Text = client.Name
        LblApeCli.Text = client.LastName
        LblFdnCli.Text = ConvertVeryLongDate(client.DateBirth)
        LblEdadCli.Text = client.AgeText
        LblTlfCli.Text = client.Phone
        LblEmlCli.Text = client.Email
        LblDirCli.Text = client.Address
        LblMtdPgoCli.Text = client.PaymentMethod
        LblFdiCli.Text = ConvertVeryLongDate(client.RegistrationDate)
        LblEstCli.Text = client.State
        LblGrpFamCli.Text = client.IdGroup

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
    Private Sub RefreshPaymentHistory()
        ' Verificamos que no sea Nothing por seguridad
        'If _selectedClient IsNot Nothing Then

        DgvPaymentList.DataSource = _paymentManager.GetPaymentHistory(_selectedClient.IdCli, _selectedClient.IdGroup)
            ' Lo ponemos aquí, una sola vez después de cargar
            DgvPaymentList.CurrentCell = Nothing

        'End If

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
        DgvPaymentList.DataSource = Nothing 'Rows.Clear()
        'strIdClient = ""
    End Sub

    Private Sub ChangeBackColorLabel()

        'COMPROBAMOS EL BACKCOLOR
        If LblNomCli.BackColor = Color.MintCream Then
            'RECORREMOS TODOS LOS LABEL QUE CUMPLAN LA CONDIÓN PARA CAMBIAR EL COLOR
            For Each control As Control In GbDataClient.Controls
                If TypeOf (control) Is Label Then
                    If control.Name.Contains("Cli") Then
                        control.BackColor = Color.WhiteSmoke
                    End If
                End If
            Next
        Else
            'RECORREMOS TODOS LOS LABEL QUE CUMPLAN LA CONDIÓN PARA CAMBIAR EL COLOR
            For Each control As Control In GbDataClient.Controls
                If TypeOf (control) Is Label Then
                    If control.Name.Contains("Cli") Then
                        control.BackColor = Color.MintCream
                    End If
                End If
            Next
        End If

    End Sub
    ''
    ''
    ''
End Class