Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

''' <summary>
''' Formulario encargado de registrar nuevos clientes y modificar
''' la información de clientes existentes.
'''
''' Permite gestionar:
''' - Datos personales.
''' - Método de pago.
''' - Asignación a grupos familiares.
''' - Validaciones visuales.
''' - Confirmación de cambios sin guardar.
''' </summary>
Public Class FrmNewModifyClient

#Region "Campos privados"
    ' Herramientas
    Private ReadOnly _clientManager As New ClientManager()
    Private _customerData As New ClientPaymentDTO()
    Private _originalDataCustomer As IndividualPaymentDTO

    ' Privada para guardar la "llave" del refresco con parámetro
    Private _onSuccessAction As Action(Of Integer)

    ' Variables de "Estado"
    Private _isSaving As Boolean = False
    Private _isSwitching As Boolean = False
    Private _shouldExpandGroup As Boolean

    ' Variables de Memoria
    Private _selectedGroupId As Integer? = Nothing
    Private _currentGroupMaxMembers As Integer = 0
    Private _groupName As String
    Private _groupMemberLimit As Integer
    Private _registeredMembers As Integer
#End Region

    Private Sub FrmNewModifyClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '| * Llamada a las funciones para configurar limites de fecha
        ConfigureBirthDatePicker()
        ConfigureRegistrationDatePicker()

        '| * Activamos las "luces" de los campos y limpieza del ErrorProvider
        SetupTextBoxEvents()
        ErrorProvider.Clear()

    End Sub
    Private Sub FrmNewModifyClient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        ' Si venimos de la función ShowSuccessMessage, _isSaving será TRUE
        If _isSaving Then Exit Sub

        ' Solo preguntamos si el cierre es por el usuario (X o Me.Close) y hay cambios
        If e.CloseReason = CloseReason.UserClosing AndAlso HasUnsavedChanges() Then

            ' Llenamos la variable con el estado del botón guardar.
            Dim isSaveMode As Boolean = BtnSaveCustomerData.Enabled

            ' Construimos el cuerpo del mensaje.
            Dim answer = MessageBox.Show(UnsavedChangesWarning(If(isSaveMode, "guardados", "actualizados"), If(isSaveMode, "guardar", "actualizar")),
                                         "Cambios sin guardar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            '* SI : ¡DETENEMOS el cierre de la ventana!
            '* NO : No hacemos nada extra, dejamos que el evento siga su curso y la ventana se cierre.
            If answer = DialogResult.Yes Then e.Cancel = True

        End If

    End Sub


    Private Sub TxtFirstName_TextChanged(sender As Object, e As EventArgs) Handles TxtFirstName.TextChanged
    End Sub
    Private Sub TxtFirstName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFirstName.KeyPress

        '| --------------------------------------
        '| VALIDAR EL INGRESO DE LETRAS Y ESPACIO
        '| --------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Almacenamos en la variable strLockKey los caracteres que queremos EXCLUIR.
        '| * Llamamos a la subrutina Fun_Only_Letters y le pasamos las variables como parámetro.

        Dim strAllowKey As String = " "
        Dim strLockKey As String = "ºª"
        AllowOnlyLetters(e, strAllowKey, strLockKey)

    End Sub


    Private Sub TxtLastName_TextChanged(sender As Object, e As EventArgs) Handles TxtLastName.TextChanged
    End Sub
    Private Sub TxtLastName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtLastName.KeyPress

        '| --------------------------------------
        '| VALIDAR EL INGRESO DE LETRAS Y ESPACIO
        '| --------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Almacenamos en la variable strLockKey los caracteres que queremos EXCLUIR.
        '| * Llamamos a la subrutina Fun_Only_Letters y le pasamos las variables como parámetro.

        Dim strAllowKey As String = " "
        Dim strLockKey As String = "ºª"
        AllowOnlyLetters(e, strAllowKey, strLockKey)

    End Sub


    Private Sub DtpBirthdate_ValueChanged(sender As Object, e As EventArgs) Handles DtpBirthdate.ValueChanged

        '| ----------------------------
        '| CALCULAR LA EDAD DEL CLIENTE
        '| ----------------------------
        '| * Almacenamos en la variable dtDateOfBirth la fecha de nacimiento que se obtiene del DtpBirthdate
        '| * Para calcular los años llamamos a la función CalculateClientAge y le pasamos la variable dtDateOfBirth,
        '|   está función nos devuelve un valor entero que lo mostramos en el label LblCustomerAge.  
        '| * Para validar la edad usamos la función ValidateCustomerAgeUI.

        Dim dtDateOfBirth As Date = DtpBirthdate.Value
        LblCustomerAge.Text = CalculateClientAge(dtDateOfBirth) & " años"
        ValidateCustomerAgeUI(LblCustomerAge, ErrorProvider, Color.Beige)

    End Sub
    Private Sub DtpBirthdate_GotFocus(sender As Object, e As EventArgs) Handles DtpBirthdate.GotFocus

        '| ------------------------------------------------
        '| CAMBIAR EL COLOR Y DAR FORMATO AL DATETIMEPICKER
        '| ------------------------------------------------
        '| * Al recibir el emfoque cambiammos el color del fondo del Textbox.
        '| * Le damos formato al DtpBirthdate con una fecha personalizada.

        LblCustomerAge.BackColor = Color.Beige
        DtpBirthdate.CustomFormat = "'  'dd'  de  'MMMM'  de  'yyyy"

    End Sub
    Private Sub DtpBirthdate_LostFocus(sender As Object, e As EventArgs) Handles DtpBirthdate.LostFocus
        ' Al perder el enfoque llamamos a la función ValidateCustomerAgeUI.
        ValidateCustomerAgeUI(LblCustomerAge, ErrorProvider, Color.Azure)
    End Sub


    Private Sub TxtPhone_TextChanged(sender As Object, e As EventArgs) Handles TxtPhone.TextChanged
    End Sub
    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress

        '| -----------------------------------------------
        '| VALIDAR DE NÚMEROS, PARÉNTESIS, GUION Y ESPACIO
        '| -----------------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Llamamos a la función AllowOnlyIntegers y le pasamos la variable como parámetro.

        Dim strAllowKey As String = "(-) "
        AllowOnlyIntegers(e, strAllowKey)

    End Sub


    Private Sub TxtEmail_TextChanged(sender As Object, e As EventArgs) Handles TxtEmail.TextChanged
    End Sub
    Private Sub TxtEmail_GotFocus(sender As Object, e As EventArgs) Handles TxtEmail.GotFocus

        '| * Al recibir el enfoque comrpobamos si el texto cumple con el formato del E-Mail
        ValidateEmailUI(TxtEmail, ErrorProvider, True)

    End Sub
    Private Sub TxtEmail_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtEmail.KeyUp

        '| * Al soltar la tecla comrpobamos si el texto ingresado cumple con el formato del E-Mail
        ValidateEmailUI(TxtEmail, ErrorProvider, True)

    End Sub
    Private Sub TxtEmail_LostFocus(sender As Object, e As EventArgs) Handles TxtEmail.LostFocus

        '| * Al perder el enfoque comrpobamos si el texto ingresado cumple con el formato del E-Mail
        ValidateEmailUI(TxtEmail, ErrorProvider, False)

    End Sub


    Private Sub TxtAddress_TextChanged(sender As Object, e As EventArgs) Handles TxtAddress.TextChanged
    End Sub
    Private Sub TxtAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtAddress.KeyPress

        '| -----------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE CARACTERES PARA LA DIRECCIÓN
        '| --------------------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Llamamos a la subrutina Fun_Only_Letters y le pasamos las variables como parámetro.

        Dim strAllowKey As String = "(&'.-/) "
        AllowLettersAndDigits(e, strAllowKey)

    End Sub


    Private Sub ChkRegistrationDate_CheckedChanged(sender As Object, e As EventArgs) Handles ChkRegistrationDate.CheckedChanged

        '| * Llamamos a la función ToggleControl que se encarga de activar/desactivar
        '|   el control y mostrar un tooltip informativo.
        ToggleControl(DtpRegistrationDate, ChkRegistrationDate, ToolTip,
                      "Desactiva la fecha de inscripción.",
                      "Activa la fecha de inscripción.")
    End Sub


    Private Sub RbDailyPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbDailyPayment.CheckedChanged

        If RbDailyPayment.Checked Then
            ' 1. Configuramos UI
            GbListaGrupoFamiliar.Text = "Lista de pagos diarios:"
            TxtListGroupsDailyPayment.Clear()
            DgvListGroupsDailyPayment.Enabled = True

            ' 2. Cargamos datos con nuestra Función Camaleón
            ConfigureGridColumns(PaymentMethods.Daily)
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetDailyPrice()

            ' Si estamos en modo edición y tenemos la "Foto" original
            If _originalDataCustomer IsNot Nothing Then
                SelectCurrentPrice(_originalDataCustomer.PaymentMethod)
            Else
                ' Si es NUEVO, mantenemos el comportamiento de limpieza absoluta
                DgvListGroupsDailyPayment.ClearSelection()
                DgvListGroupsDailyPayment.CurrentCell = Nothing
            End If

        End If

    End Sub


    Private Sub RbMonthlyPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbMonthlyPayment.CheckedChanged

        If RbMonthlyPayment.Checked Then

            _selectedGroupId = Nothing
            _currentGroupMaxMembers = 0

            TxtListGroupsDailyPayment.Clear()
            GbListaGrupoFamiliar.Text = "Lista vacia"
            DgvListGroupsDailyPayment.Enabled = False
            DgvListGroupsDailyPayment.DataSource = Nothing

        End If
    End Sub


    Private Sub RbGroupPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbGroupPayment.CheckedChanged

        ' Si el sistema está cargando los datos iniciales, salimos sin validar nada
        If _isSwitching Then Exit Sub

        If RbGroupPayment.Checked Then

            ' 2. Verificamos si estamos en edición y si hay deuda
            If _originalDataCustomer IsNot Nothing AndAlso _originalDataCustomer.HasDebtCustomer Then
                ' 3. Preparamos el mensaje
                MessageBox.Show(PendingDebtWarning("Para hacer el cambio de método de pago"),
                                "Acción denegada", MessageBoxButtons.OK, MessageBoxIcon.Stop)

                ' 4. REVERSIÓN: Volvemos al método que tiene el "Clone"
                RestorePaymentMethod()
                Return
            End If

            ' Preparar Interfaz
            TxtListGroupsDailyPayment.Clear()
            LblNumberMembers.Text = ""
            GbListaGrupoFamiliar.Text = "Lista de grupos familiares:"

            BtnAddGroup.Enabled = True
            TxtListGroupsDailyPayment.Enabled = True
            DgvListGroupsDailyPayment.Enabled = True

            ' Cargar datos
            ConfigureGridColumns(PaymentMethods.Grupal)
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetNameGroupFamily()
            DgvListGroupsDailyPayment.CurrentCell = Nothing

            TxtListGroupsDailyPayment.Focus()
        Else

            If BtnSaveCustomerData.Enabled Then
                ' NUEVO/GUARDAR : Simplemente deshabilitamos
                BtnAddGroup.Enabled = False
                TxtListGroupsDailyPayment.Enabled = False
                TxtListGroupsDailyPayment.BackColor = Color.Azure
                LblNumberMembers.Text = ""
                ResetGroupUI(True)

            Else
                ' MODIFICAR/ACTUALIZAR : NO dejamos cambiar el método de pago
                MessageBox.Show(GroupPaymentChangeNotAllowed, "Error al actualizar",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _isSwitching = True
                RbGroupPayment.Checked = True
                _isSwitching = False

            End If

        End If

    End Sub


    Private Sub BtnAddGroup_Click(sender As Object, e As EventArgs) Handles BtnAddGroup.Click
        'FrmFamilyGroup.Show()
        ' Buscamos si ya está abierto
        Dim frmGroup = FrmMdiMain.MdiChildren.OfType(Of FrmFamilyGroup)().FirstOrDefault()

        If frmGroup Is Nothing Then
            ' Si no existe, lo creamos
            frmGroup = New FrmFamilyGroup()
            frmGroup.MdiParent = FrmMdiMain
            frmGroup.Show()
        Else
            ' Si ya existe, lo traemos al frente y le damos el foco
            frmGroup.BringToFront()
            frmGroup.Activate()

            ' Si la ventana está minimizada, esto la restaura
            If frmGroup.WindowState = FormWindowState.Minimized Then
                frmGroup.WindowState = FormWindowState.Normal
            End If
        End If

    End Sub


    Private Sub TxtListGroupsDailyPayment_TextChanged(sender As Object, e As EventArgs) Handles TxtListGroupsDailyPayment.TextChanged

        If Not RbGroupPayment.Checked Then Exit Sub

        ' 1. Filtrar el Grid mientras el usuario escribe
        ConfigureGridColumns(PaymentMethods.Grupal)
        Dim searchText = TxtListGroupsDailyPayment.Text.Trim()
        DgvListGroupsDailyPayment.DataSource = _clientManager.SearchFamilyGroup(searchText)

        ' 2. Verificación de texto vacío y Limpieza total
        If String.IsNullOrWhiteSpace(searchText) Then
            ResetGroupUI(True)
            DgvListGroupsDailyPayment.CurrentCell = Nothing
            Exit Sub
        End If

        ' 3. Buscar coincidencia exacta en los resultados
        Dim matchRow = DgvListGroupsDailyPayment.Rows.Cast(Of DataGridViewRow)().
                       FirstOrDefault(Function(r) r.Cells("colNameDailyGroup").Value?.ToString().ToUpper() = searchText.ToUpper())

        If matchRow IsNot Nothing Then
            ' --- COINCIDENCIA ENCONTRADA ---
            _selectedGroupId = CInt(matchRow.Cells("colIdDailyGroup").Value)
            _groupName = matchRow.Cells("colNameDailyGroup").Value.ToString()
            _groupMemberLimit = CInt(matchRow.Cells("colNumMembers").Value)
            _registeredMembers = CInt(matchRow.Cells("colMembersReg").Value)

            TxtListGroupsDailyPayment.BackColor = Color.Azure
            ErrorProvider.Clear()
            LblNumberMembers.Text = $"Registrados {_registeredMembers} de {_groupMemberLimit}"

            UpdateExpansionUI(_groupMemberLimit = _registeredMembers)

            _currentGroupMaxMembers = _groupMemberLimit
        Else
            ' --- SIN COINCIDENCIA EXACTA ---
            ResetGroupUI(False)
        End If

        ' Siempre reiniciamos la intención de expandir al cambiar el texto
        _shouldExpandGroup = False

    End Sub


    Private Sub BtnExpandCapacity_Click(sender As Object, e As EventArgs) Handles BtnExpandCapacity.Click

        ' Preparamos el mensaje
        If MsgBox(ConfirmAddExtraGroupMember(_groupName, _groupMemberLimit), vbExclamation _
                  + vbYesNo + vbDefaultButton2, "Comprobar datos") = vbYes Then

            _currentGroupMaxMembers += 1
            _shouldExpandGroup = True

            ' UI Feedback
            LblNumberMembers.ForeColor = Color.FromArgb(255, 128, 0) 'Color.Orange
            LblNumberMembers.Text = "1 Cupo pendiente por registrar."
            BtnExpandCapacity.Enabled = False
            ErrorProvider.Clear()

        End If

    End Sub


    Private Sub DgvListGroupsDailyPayment_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListGroupsDailyPayment.CellContentClick
    End Sub
    Private Sub DgvListGroupsDailyPayment_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListGroupsDailyPayment.CellClick

        ' Verificaciones
        If e.RowIndex < 0 Then Exit Sub
        Dim row As DataGridViewRow = DgvListGroupsDailyPayment.Rows(e.RowIndex)
        If row Is Nothing Then Exit Sub

        ' PAGO DIARIO
        If RbDailyPayment.Checked Then
            TxtListGroupsDailyPayment.Text = row.Cells("colNameDailyGroup").Value.ToString()
        End If

        ' PAGO GRUPAL
        If RbGroupPayment.Checked Then

            _selectedGroupId = CInt(row.Cells("colIdDailyGroup").Value)
            _groupName = row.Cells("colNameDailyGroup").Value.ToString()
            _groupMemberLimit = CInt(row.Cells("colNumMembers").Value)
            _registeredMembers = CInt(row.Cells("colMembersReg").Value)

            TxtListGroupsDailyPayment.Text = _groupName
        End If

    End Sub


    Private Sub BtnSaveCustomerData_Click(sender As Object, e As EventArgs) Handles BtnSaveCustomerData.Click

        '| * Validación de la información del cliente antes de guardar el registro.
        If Not ValidateForm("guardar") Then Exit Sub
        If Not ValidateGroupCapacity("guardar", BtnExpandCapacity) Then Exit Sub

        '| -------------------
        '| PROCESO DE GUARDADO
        '| -------------------

        Try
            ' Creamos el "bolso" con los datos, recolectamos datos usando la función CreateClientPaymentDTO
            Dim data As ClientPaymentDTO = GetClientDataFromForm(isUpdate:=False) 'CreateClientPaymentDTO()

            ' Enviamos al Manager (Se encarga de la BBDD)
            _clientManager.RegisterClientPayment(data)

            ' Mostramos el mensaje de confirmación usando el IdNewClient
            ShowSuccessMessage(data.IdNewClient)

        Catch ex As Exception
            MessageBox.Show("Error INSERT cliente:" & vbCrLf & ex.Message, "Error al registrar")
        End Try

    End Sub


    Private Sub BtnUpdateCustomerData_Click(sender As Object, e As EventArgs) Handles BtnUpdateCustomerData.Click

        '| * Validación de la información del cliente antes de actualizar el registro.
        If Not ValidateForm("actualizar") Then Exit Sub

        '| ------------------------
        '| PROCESO DE ACTUALIZACIÓN
        '| ------------------------

        ' Crear el DTO con los nuevos cambios, recolectamos datos usando la función UpdateClientPaymentDTO
        Dim data As ClientPaymentDTO = GetClientDataFromForm(isUpdate:=True) 'UpdateClientPaymentDTO()

        ' 2. Lógica Refinada: Solo es nueva inscripción si el cliente NO tenía grupo 
        ' y ahora se le ha asignado uno (data.IdGroup > 0)
        Dim isNewEnrollment As Boolean = False

        If (_customerData.IdGroup Is Nothing OrElse _customerData.IdGroup = 0) AndAlso
            (data.IdGroup.HasValue AndAlso data.IdGroup > 0) Then
            isNewEnrollment = True
        End If

        ' 3. Si el cliente YA TENÍA el mismo ID de grupo, isNewEnrollment DEBE ser False
        ' para que el Manager no ejecute la suma +1
        If _customerData.IdGroup = data.IdGroup Then
            isNewEnrollment = False
        End If

        Try
            ' El método en tu ClientManager se llama UpdateClientProcess
            Dim success = _clientManager.UpdateClientProcess(data, isNewEnrollment, _shouldExpandGroup)

            If success Then
                ' Mostramos el mensaje de confirmación usando el IdNewClient
                ShowSuccessMessage(data.IdNewClient)

            Else
                MessageBox.Show("Error al actualizar los datos.", "Error")
            End If

        Catch ex As Exception
            MessageBox.Show("Error UPDATE cliente:" & vbCrLf & ex.Message, "Error al actualizar")
        End Try

    End Sub


    Private Sub BtnCancelRegistration_Click(sender As Object, e As EventArgs) Handles BtnCancelRegistration.Click

        ' Close disparará el evento FormClosinghaciendo haciendo las comprobaciones necesarias
        ' para determinar si hay cambios pendientes por guardar o actualizar.
        Me.Close()

    End Sub



    '| ---------------------------------------------------------------- |'
    '| ---------->>>>>>>>>> SUBRUTINAS Y FUNCIONES <<<<<<<<<<---------- |'
    '| ---------------------------------------------------------------- |'

    ''' <summary>
    ''' Método para recibir la acción desde el módulo
    ''' </summary>
    Public Sub SetRefreshAction(action As Action(Of Integer))
        _onSuccessAction = action
    End Sub


    ''' <summary>
    ''' Engañamos al vigilante.
    ''' La llave maestra que silencia el FormClosing
    ''' </summary>
    Public Sub ForceClose()
        _isSaving = True
        Me.Close()
    End Sub


    ''' <summary>
    ''' Configura los límites y valores iniciales del
    ''' selector de fecha de nacimiento.
    ''' </summary>
    Private Sub ConfigureBirthDatePicker()

        Dim today As Date = Date.Today

        ' Límites para la fecha de nacimiento
        DtpBirthdate.MinDate = New Date(today.Year - 90, 1, 1)
        DtpBirthdate.MaxDate = today

        ' ESTO SOLO SI ES UN REGISTRO NUEVO
        If BtnSaveCustomerData.Enabled Then
            ' Asignamos una fecha sugerida (25 años atrás)
            DtpBirthdate.Value = New Date(today.Year - 25, 7, 1)
            ' Limpiamos y preparamos para el nuevo registro
            LblCustomerAge.Text = ""
            LblCustomerAge.BackColor = Color.Azure
        End If

    End Sub


    ''' <summary>
    ''' Configura los límites y valores iniciales del
    ''' selector de fecha de registro.
    ''' </summary>
    Private Sub ConfigureRegistrationDatePicker()

        Dim today As Date = Date.Today

        ' Límites para la fecha de registro
        DtpRegistrationDate.MinDate = New Date(today.Year - 2, 1, 1)
        DtpRegistrationDate.MaxDate = New Date(today.Year + 2, 12, 31)

        ' Comprueba si estamos en modo "NUEVO/GUARDAR"
        If BtnSaveCustomerData.Enabled Then DtpRegistrationDate.Value = today

    End Sub


    ''' <summary>
    ''' Prepara el formulario para registrar un nuevo cliente.
    ''' Reinicia el estado interno, limpia la referencia del cliente
    ''' en edición y restablece la configuración visual por defecto.
    ''' </summary>
    Public Sub PrepareForNewClient()

        ' Resetear el DTO para que no tenga datos del cliente anterior
        _customerData = New ClientPaymentDTO()
        ' Borramos los datos de la copia
        _originalDataCustomer = Nothing

        ' Configurar botones
        BtnSaveCustomerData.Enabled = True
        BtnUpdateCustomerData.Enabled = False
        BtnUpdateCustomerData.BackColor = Color.Silver

        ' Configurar fechas por defecto
        DtpBirthdate.Format = DateTimePickerFormat.Custom
        DtpBirthdate.CustomFormat = " "
        DtpRegistrationDate.Value = DateTime.Today

    End Sub


    ''' <summary>
    ''' Carga en el formulario la información de un cliente existente
    ''' para permitir su modificación.
    ''' También conserva una copia del estado original para detectar cambios.
    ''' </summary>
    ''' <param name="clientData">
    ''' Datos originales del cliente seleccionados para edición.
    ''' </param>
    Public Sub PrepareToModifyClient(clientData As IndividualPaymentDTO)

        _isSwitching = True ' Levantamos el semáforo

        ' Guardamos los datos originales
        _originalDataCustomer = clientData.Clone()

        ' 1. Llenamos los campos con la información recibida
        _customerData.IdNewClient = clientData.IdCli
        TxtFirstName.Text = clientData.FirstName
        TxtLastName.Text = clientData.LastName
        DtpBirthdate.Value = clientData.BirthDate
        TxtPhone.Text = clientData.Phone
        TxtEmail.Text = clientData.Email
        TxtAddress.Text = clientData.Address
        DtpRegistrationDate.Value = clientData.RegistrationDate
        _customerData.IdGroup = clientData.IdGroup

        ' 2. Estado (Activo/Inactivo)
        If clientData.State = CustomerStates.Active Then
            RbActiveStatus.Checked = True
        Else
            RbInactiveState.Checked = True
        End If

        ' 3. Método de Pago
        Select Case clientData.PaymentMethod
            Case PaymentMethods.Monthly
                RbMonthlyPayment.Checked = True

            Case PaymentMethods.Grupal
                RbGroupPayment.Checked = True
                TxtListGroupsDailyPayment.Text = clientData.GroupName

            Case Else '"DIARIO"
                RbDailyPayment.Checked = True
                TxtListGroupsDailyPayment.Text = clientData.PaymentMethod

        End Select

        ' 4. Ajustes Visuales
        BtnSaveCustomerData.Enabled = False
        BtnSaveCustomerData.BackColor = Color.Silver
        BtnUpdateCustomerData.Enabled = True

        ' Evita que los eventos CheckedChanged se ejecuten mientras el formulario se carga programáticamente.
        ' Bajamos el semáforo para que el usuario ya pueda interactuar
        _isSwitching = False
    End Sub


    ''' <summary>
    ''' Obtiene la información ingresada en el formulario
    ''' y construye un objeto ClientPaymentDTO.
    ''' </summary>
    ''' <param name="isUpdate">
    ''' Indica si los datos corresponden a una actualización
    ''' de cliente existente.
    ''' </param>
    ''' <returns>
    ''' Objeto ClientPaymentDTO con los datos actuales
    ''' del formulario.
    ''' </returns>
    ''' <remarks>
    ''' Este método centraliza el mapeo entre los controles
    ''' de la interfaz y el DTO utilizado por la lógica
    ''' de negocio.
    '''
    ''' También asigna automáticamente:
    ''' - Método de pago.
    ''' - Información de grupo familiar.
    ''' - Estado del cliente.
    ''' </remarks>
    Private Function GetClientDataFromForm(Optional isUpdate As Boolean = False) As ClientPaymentDTO

        Dim data As New ClientPaymentDTO With
            {
                .FirstName = TxtFirstName.Text.Trim(),
                .LastName = TxtLastName.Text.Trim(),
                .BirthDate = DtpBirthdate.Value,
                .Age = CInt(Val(LblCustomerAge.Text)),
                .Phone = TxtPhone.Text.Trim(),
                .Email = TxtEmail.Text.Trim(),
                .Address = TxtAddress.Text.Trim(),
                .RegistrationDate = DtpRegistrationDate.Value,
                .State = If(RbActiveStatus.Checked, CustomerStates.Active, CustomerStates.Inactive),
                .IdGroup = _selectedGroupId ' Lo asignamos siempre, sea 0 o un ID real
            }

        ' Si es actualización, asignamos el ID del cliente que estamos editando
        If isUpdate Then data.IdNewClient = _customerData.IdNewClient 'End If

        ' Lógica de Método de Pago (Unificada)
        If RbMonthlyPayment.Checked Then
            data.PaymentMethod = PaymentMethods.Monthly
            data.IsGroup = False

        ElseIf RbGroupPayment.Checked Then
            data.PaymentMethod = PaymentMethods.Grupal
            data.IsGroup = True
            data.GroupName = TxtListGroupsDailyPayment.Text.Trim()
            data.GroupMembers = _currentGroupMaxMembers
            data.ShouldExpandGroup = _shouldExpandGroup

        ElseIf RbDailyPayment.Checked Then
            data.PaymentMethod = TxtListGroupsDailyPayment.Text.Trim()
            data.IsGroup = False

        End If

        Return data

    End Function


    ''' <summary>
    ''' Comprueba si existen cambios no guardados en el formulario
    ''' comparando los valores actuales con la copia original del cliente.
    ''' </summary>
    ''' <returns>
    ''' True si existe al menos una modificación pendiente;
    ''' de lo contrario, False.
    ''' </returns>
    Public Function HasUnsavedChanges() As Boolean

        ' CASO NUEVO: Si no hay foto original, usamos la lógica de "campos vacíos"
        If _originalDataCustomer Is Nothing Then
            ' Si el nombre o el apellido NO están vacíos, consideramos que hay cambios
            Return Not String.IsNullOrWhiteSpace(TxtFirstName.Text) OrElse
                   Not String.IsNullOrWhiteSpace(TxtLastName.Text)
        End If

        ' CASO EDICIÓN: Comparamos el texto actual vs el original
        ' Si alguno es diferente, devolvemos TRUE (hay cambios)
        If SafeTrim(TxtFirstName.Text) <> SafeTrim(_originalDataCustomer.FirstName) Then Return True
        If SafeTrim(TxtLastName.Text) <> SafeTrim(_originalDataCustomer.LastName) Then Return True
        If SafeTrim(TxtPhone.Text) <> SafeTrim(_originalDataCustomer.Phone) Then Return True
        If SafeTrim(TxtEmail.Text) <> SafeTrim(_originalDataCustomer.Email) Then Return True
        If SafeTrim(TxtAddress.Text) <> SafeTrim(_originalDataCustomer.Address) Then Return True

        ' --- Fechas (DTPicker) ---
        ' Usamos .Date para comparar solo la fecha sin preocuparnos por la hora
        If DtpBirthdate.Value.Date <> _originalDataCustomer.BirthDate.Date Then Return True
        If DtpRegistrationDate.Value.Date <> _originalDataCustomer.RegistrationDate.Date Then Return True

        ' --- Estado (RadioButtons / Toggle) ---
        ' Asumiendo que _originalData.IsActive es un Booleano
        Dim currentStatus As String = If(RbActiveStatus.Checked, CustomerStates.Active, CustomerStates.Inactive)
        If currentStatus <> _originalDataCustomer.State Then Return True

        ' --- Método de Pago ---
        ' Comparas el valor seleccionado dentro de HasUnsavedChanges contra el original
        If GetCurrentPaymentMethod() <> _originalDataCustomer.PaymentMethod Then Return True

        ' Si llegó hasta aquí, es que todo es idéntico
        Return False

    End Function


    ''' <summary>
    ''' Restaura el método de pago original del cliente
    ''' evitando disparar eventos recursivos.
    ''' </summary>
    Private Sub RestorePaymentMethod()

        ' Bloqueamos eventos temporalmente para no entrar en bucles
        RemoveHandler RbGroupPayment.CheckedChanged, AddressOf RbGroupPayment_CheckedChanged

        ' Consultamos nuestro "Clone" para saber qué radio button marcar
        Select Case _originalDataCustomer.PaymentMethod
            Case PaymentMethods.Monthly
                RbMonthlyPayment.Checked = True

            Case Else '"DIARIO"
                RbDailyPayment.Checked = True
                TxtListGroupsDailyPayment.Text = _originalDataCustomer.PaymentMethod

        End Select

        ' Re-conectamos el evento para futuras interacciones
        AddHandler RbGroupPayment.CheckedChanged, AddressOf RbGroupPayment_CheckedChanged

        ' NOTA IMPORTANTE
        ' StartsWith es como un detective de texto: pregunta si una cadena de texto comienza
        ' por una palabra o letra específica.
        ' Case _originalDataCustomer.PaymentMethod.StartsWith("DIARIO")
    End Sub


    ''' <summary>
    ''' Obtiene el método de pago actualmente seleccionado en el formulario.
    ''' </summary>
    ''' <returns>
    ''' El método de pago seleccionado:
    ''' - MENSUAL
    ''' - GRUPAL
    ''' - Pago diario seleccionado
    ''' </returns>
    Private Function GetCurrentPaymentMethod() As String

        If RbMonthlyPayment.Checked Then
            Return PaymentMethods.Monthly

        ElseIf RbGroupPayment.Checked Then
            Return PaymentMethods.Grupal

        ElseIf RbDailyPayment.Checked Then
            Return TxtListGroupsDailyPayment.Text.Trim()
        End If

        Return "DESCONOCIDO"

    End Function


    ''' <summary>
    ''' Configura las columnas del DataGridView según
    ''' el tipo de pago seleccionado.
    ''' </summary>
    ''' <param name="methodPay">
    ''' Método de pago utilizado para determinar
    ''' el origen de datos de las columnas.
    ''' </param>
    Private Sub ConfigureGridColumns(methodPay As String)

        ' Limpiamos cualquier vinculación previa
        DgvListGroupsDailyPayment.AutoGenerateColumns = False

        If methodPay = PaymentMethods.Daily Then
            ' Configuración para Pagos diarios
            With DgvListGroupsDailyPayment
                .Columns("colIdDailyGroup").DataPropertyName = "id_trfa"
                .Columns("colNameDailyGroup").DataPropertyName = "tipo_trfa"
            End With

        ElseIf methodPay = PaymentMethods.Grupal Then
            ' Configuración para Grupos Familiares
            With DgvListGroupsDailyPayment
                .Columns("colIdDailyGroup").DataPropertyName = "id_grp"
                .Columns("colNameDailyGroup").DataPropertyName = "nom_grp"
                .Columns("colNumMembers").DataPropertyName = "num_intgrntes_grp"
                .Columns("colMembersReg").DataPropertyName = "intgrntes_reg_grp"
            End With
        End If

    End Sub


    ''' <summary>
    ''' Busca y selecciona en el DataGridView
    ''' la tarifa actualmente asignada al cliente.
    ''' </summary>
    ''' <param name="contractedPayment">
    ''' Nombre del tipo de pago contratado.
    ''' </param>
    Private Sub SelectCurrentPrice(contractedPayment As String)

        ' 1. Limpiamos cualquier selección previa
        DgvListGroupsDailyPayment.ClearSelection()
        DgvListGroupsDailyPayment.CurrentCell = Nothing

        ' 2. Buscamos el valor en el Grid
        ' (Asumo que la columna con el nombre "DIARIO X" es la primera o tiene un nombre específico)
        For Each row As DataGridViewRow In DgvListGroupsDailyPayment.Rows
            If row.Cells("colNameDailyGroup").Value?.ToString() = contractedPayment Then
                ' 3. ¡Lo encontramos! Marcamos la fila
                row.Selected = True
                ' Establecemos la celda actual para que el foco visual sea perfecto
                DgvListGroupsDailyPayment.CurrentCell = row.Cells("colNameDailyGroup")

                ' 4. Hacemos scroll automático si la lista es larga para que se vea la fila
                DgvListGroupsDailyPayment.FirstDisplayedScrollingRowIndex = row.Index
                Exit For
            End If
        Next

    End Sub


    ''' <summary>
    ''' Actualiza la interfaz visual cuando
    ''' un grupo familiar alcanza el límite de integrantes.
    ''' </summary>
    Private Sub UpdateExpansionUI(isFull As Boolean)

        If isFull Then
            LblNumberMembers.ForeColor = Color.FromArgb(192, 0, 0) 'COLOR ROJO
            BtnExpandCapacity.Enabled = True
            ErrorProvider.SetError(BtnExpandCapacity, FullFamilyGroup(_groupName))
        End If

    End Sub


    ''' <summary>
    ''' Restablece el estado visual y lógico relacionado
    ''' con la selección de grupos familiares.
    ''' </summary>
    ''' <param name="clearLabel">
    ''' Indica si el texto informativo debe limpiarse
    ''' completamente o mostrar el mensaje de búsqueda.
    ''' </param>
    Private Sub ResetGroupUI(clearLabel As Boolean)

        _selectedGroupId = 0
        _currentGroupMaxMembers = 0
        BtnExpandCapacity.Enabled = False
        ErrorProvider.Clear()

        LblNumberMembers.ForeColor = Color.FromArgb(0, 64, 0) 'COLOR VERDE
        LblNumberMembers.Text = If(clearLabel, "", AppTexts.SearchingGroup)

    End Sub


    ''' <summary>
    ''' Asigna dinámicamente los eventos de validación
    ''' y cambio visual a los controles TextBox del formulario.
    ''' </summary>
    Private Sub SetupTextBoxEvents()

        ' Creamos una lista de los TextBox que queremos validar
        Dim requiredFields As TextBox() = {TxtFirstName, TxtLastName, TxtListGroupsDailyPayment}
        Dim optionalFields As TextBox() = {TxtPhone, TxtAddress} 'TxtEmail,

        For Each textBox In requiredFields
            ' Suscribimos el evento GotFocus (Cambio a Beige)
            AddHandler textBox.GotFocus, Sub(s, e) DirectCast(s, TextBox).BackColor = Color.Beige
            ' Suscribimos el evento LostFocus (Validación y Azure)
            AddHandler textBox.LostFocus, Sub(s, e) ValidateRequiredFieldUI(DirectCast(s, TextBox), Me.ErrorProvider)
        Next

        For Each textBox In optionalFields
            ' Suscribimos el evento GotFocus (Cambio a Beige)
            AddHandler textBox.GotFocus, Sub(s, e) DirectCast(s, TextBox).BackColor = Color.Beige
            ' Suscribimos el evento LostFocus (Validación y Azure)
            AddHandler textBox.LostFocus, Sub(s, e) ValidateOptionalFieldUI(DirectCast(s, TextBox), Me.ErrorProvider)
        Next

    End Sub


    ''' <summary>
    ''' Muestra un mensaje de confirmación después
    ''' de registrar o actualizar correctamente un cliente.
    ''' También notifica al formulario principal para refrescar
    ''' la información y cierra la ventana actual.
    ''' </summary>
    ''' <param name="idClient">
    ''' Identificador del cliente procesado.
    ''' </param>
    Private Sub ShowSuccessMessage(idClient As Integer)

        ' 1. Preparamos el texto del cuerpo (Guardado vs Actualizado)
        Dim idFormatted As String = $"CLI - {idClient:000}"
        Dim actionText As String = If(BtnSaveCustomerData.Enabled, "GUARDADOS", "ACTUALIZADOS")

        ' 2. Construimos el "Ticket" de confirmación (usando los datos que ya tenemos en los campos)
        MessageBox.Show(ClientOperationSuccess(TxtFirstName.Text, TxtLastName.Text, idFormatted, actionText),
                        "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' 3. Notificamos al formulario que debe refrescarse, esto ejecutará la funciónque pasamos por AddressOf
        _onSuccessAction?.Invoke(idClient)

        ' 4. Cerramos el formulario
        _isSaving = True
        Me.Close()

    End Sub


    ''' <summary>
    ''' Ejecuta las validaciones principales del formulario antes
    ''' de registrar o actualizar la información del cliente.
    ''' </summary>
    ''' <param name="actionText">
    ''' Texto descriptivo de la acción actual. Ejemplo: "guardar" o "actualizar".
    ''' </param>
    ''' <returns>
    ''' True si todas las validaciones fueron superadas correctamente;
    ''' de lo contrario, False.
    ''' </returns>
    ''' <remarks>
    ''' Esta función centraliza las validaciones relacionadas con:
    ''' - Campos obligatorios.
    ''' - Edad mínima permitida.
    ''' - Método de pago seleccionado.
    ''' - Selección de tarifas o grupos familiares.
    ''' - Disponibilidad de cupos en grupos familiares.
    '''
    ''' El proceso se detiene inmediatamente al encontrar
    ''' una validación inválida.
    ''' </remarks>
    Private Function ValidateForm(actionText As String) As Boolean

        If Not ValidateRequiredField("NOMBRE", actionText, TxtFirstName) Then Return False
        If Not ValidateRequiredField("APELLIDO", actionText, TxtLastName) Then Return False
        If Not ValidateCustomerAge(actionText, LblCustomerAge, DtpBirthdate) Then Return False
        If Not ValidateEmail(actionText, TxtEmail) Then Return False
        If Not ValidatePaymentMethod(actionText, RbDailyPayment, RbMonthlyPayment, RbGroupPayment) Then Return False
        If Not ValidateRequiredSelection(PaymentMethods.Daily, actionText, TxtListGroupsDailyPayment, RbDailyPayment) Then Return False
        If Not ValidateRequiredSelection(PaymentMethods.FamilyGroup, actionText, TxtListGroupsDailyPayment, RbGroupPayment) Then Return False
        Return True

    End Function

End Class