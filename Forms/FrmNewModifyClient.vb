Imports GymPaymentControl.BusinessRules
Imports GymPaymentControl.Constants
Imports GymPaymentControl.Enums
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

#Region " CAMPOS DE ESTADO Y VARIABLES DE SESIÓN "

    ' --- Instancias de Servicios y DTOs ---
    Private ReadOnly _clientManager As New ClientManager()
    Private _customerData As New ClientPaymentDTO()
    Private _originalDataCustomer As IndividualPaymentDTO

    ' --- Control de Flujo y Navegación ---
    Private _currentMode As TransactionMode

    ' --- Flags de Estado y Banderas de UI ---
    Private _isSaving As Boolean = False
    Private _isSwitching As Boolean = False
    Private _shouldExpandGroup As Boolean

    ' --- Variables de Memoria Temporal e Indicadores de Grupo ---
    Private _selectedGroupId As Integer? = Nothing
    Private _currentGroupMaxMembers As Integer = 0
    Private _groupName As String
    Private _groupMemberLimit As Integer
    Private _registeredMembers As Integer

    ''' <summary>
    ''' Delegado de refresco invocado para notificar
    ''' al formulario padre tras una operación exitosa.
    ''' </summary>
    Private _onSuccessAction As Action(Of Integer)

#End Region


#Region " EVENTOS DEL FORMULARIO (Handlers) "

    Private Sub FrmNewModifyClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '| * Llamada a las funciones para configurar limites de fecha
        ConfigureBirthDatePicker()
        ConfigureRegistrationDatePicker()

        '| * Activamos las "luces" de los campos y limpieza del ErrorProvider
        SetupTextBoxEvents()
        ErrorProvider.Clear()

    End Sub
    Private Sub FrmNewModifyClient_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        Dim frmGroup = FrmMdiMain.MdiChildren.OfType(Of FrmFamilyGroup)().FirstOrDefault()

        If frmGroup IsNot Nothing Then
            If Not String.IsNullOrWhiteSpace(frmGroup.NewGroupName) Then

                TxtListGroupsDailyPayment.Text = frmGroup.NewGroupName
                frmGroup.NewGroupName = String.Empty

            End If
        End If

    End Sub
    Private Sub FrmNewModifyClient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        ' Si venimos de la función ShowSuccessMessage, _isSaving será TRUE
        If _isSaving Then Exit Sub

        ' Solo preguntamos si el cierre es por el usuario (X o Me.Close) y hay cambios
        If e.CloseReason = CloseReason.UserClosing AndAlso HasUnsavedChanges() Then

            ' Llenamos la variable con el estado del botón guardar.
            Dim isSaveMode As Boolean = BtnSaveCustomerData.Enabled

            ' Construimos el cuerpo del mensaje.
            Dim answer = MessageBox.Show(DialogMessages.UnsavedChangesWarning(If(isSaveMode, "guardados", "actualizados"), If(isSaveMode, "guardar", "actualizar")),
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

        If Not RbDailyPayment.Checked Then Exit Sub

        '| * CONFIGURAR INTERFAZ
        GbListaGrupoFamiliar.Text = "Lista de pagos diarios:"
        TxtListGroupsDailyPayment.Clear()
        DgvListGroupsDailyPayment.Enabled = True

        '| * CARGAR TARIFAS DIARIAS
        ConfigureGridColumns(PaymentMethods.Daily)
        DgvListGroupsDailyPayment.DataSource = _clientManager.GetDailyPrice()

        '| * COMPROBAR SI EXISTE ALGUNA TARIFA
        If DgvListGroupsDailyPayment.Rows.Count = 0 Then

            Dim response As DialogResult = MessageBox.Show("No existe ninguna tarifa para los pagos diarios." & vbCrLf & vbCrLf &
                                                           "¿Desea crear una nueva tarifa?",
                                                           "Tarifa no encontrada",
                                                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                                           MessageBoxDefaultButton.Button2)
            If response <> DialogResult.Yes Then
                RbDailyPayment.Checked = False
                Exit Sub
            End If

            '| * CREAR NUEVA TARIFA DIARIA
            Using frm As New FrmPricesAndDiscounts()

                frm.IsDailyRateRequest = True
                If frm.ShowDialog(Me) <> DialogResult.OK Then Exit Sub
                TxtListGroupsDailyPayment.Text = frm.CreatedRateName

            End Using

            '| * RECARGAR TARIFAS
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetDailyPrice()

        End If

        '| * SELECCIÓN SEGÚN EL MODO DEL FORMULARIO
        If _originalDataCustomer IsNot Nothing Then

            '| * ACTUALIZAR: Si estamos en modo edición y tenemos la "Foto" original
            SelectCurrentPrice(_originalDataCustomer.PaymentMethod)
        Else

            '| * NUEVO: Mantenemos el comportamiento de limpieza absoluta
            DgvListGroupsDailyPayment.ClearSelection()
            DgvListGroupsDailyPayment.CurrentCell = Nothing

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

        If _isSwitching Then Exit Sub

        If RbGroupPayment.Checked Then

            ' =============================
            ' | ENTRADA AL GRUPO FAMILIAR |
            ' =============================
            If _originalDataCustomer IsNot Nothing Then

                Dim canChange As Boolean = PaymentMethodRules.CanChangePaymentMethod(_originalDataCustomer.PaymentMethod,
                                                                  PaymentMethods.Grupal, _originalDataCustomer.HasDebtCustomer)
                If Not canChange Then

                    MessageBox.Show(DialogMessages.IndividualToGroupDebtWarning(), "Aviso importante",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    RestorePaymentMethod()
                    Return

                End If

            End If

            '| * PREPARACIÓN DELA INTERFAZ
            TxtListGroupsDailyPayment.Clear()
            LblNumberMembers.Text = ""
            GbListaGrupoFamiliar.Text = "Lista de grupos familiares:"

            BtnAddGroup.Enabled = True
            TxtListGroupsDailyPayment.Enabled = True
            DgvListGroupsDailyPayment.Enabled = True

            '| * CARGAR DATOS
            ConfigureGridColumns(PaymentMethods.Grupal)
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetNameGroupFamily()
            DgvListGroupsDailyPayment.CurrentCell = Nothing

            TxtListGroupsDailyPayment.Focus()

        Else

            If _originalDataCustomer Is Nothing Then Exit Sub

            '| * SI EL CLIENTE YA PERTENECE A UN GRUPO FAMILIAR
            '|   El cambio debe realizarse desde el formulario FrmFamilyGroup.
            If _originalDataCustomer.IdGroup.HasValue AndAlso _originalDataCustomer.IdGroup.Value > 0 Then

                MessageBox.Show(DialogMessages.GroupPaymentChangeNotAllowed(), "Aviso importante",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _isSwitching = True
                RbGroupPayment.Checked = True
                _isSwitching = False
                Return

            End If

            ' =============================
            ' | SALIDA DEL GRUPO FAMILIAR |
            ' =============================
            Select Case _currentMode

                Case TransactionMode.NewRecord

                    BtnAddGroup.Enabled = False
                    TxtListGroupsDailyPayment.Enabled = False
                    TxtListGroupsDailyPayment.BackColor = Color.Azure
                    LblNumberMembers.Text = ""
                    ResetGroupUI(True)

                Case TransactionMode.EditRecord

                    If _originalDataCustomer Is Nothing Then Exit Sub

                    Dim newPaymentMethod As String = If(RbMonthlyPayment.Checked, PaymentMethods.Monthly, PaymentMethods.Daily)

                    Dim canChange As Boolean = PaymentMethodRules.CanChangePaymentMethod(_originalDataCustomer.PaymentMethod,
                                                                                     newPaymentMethod,
                                                                                     _originalDataCustomer.HasDebtCustomer)
                    If Not canChange Then

                        MessageBox.Show(DialogMessages.GroupToIndividualDebtWarning(), "Aviso importante",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)

                        _isSwitching = True
                        RbGroupPayment.Checked = True
                        _isSwitching = False

                        Return

                    End If

            End Select

        End If

    End Sub


    Private Sub BtnAddGroup_Click(sender As Object, e As EventArgs) Handles BtnAddGroup.Click

        ' Buscamos si ya está abierto
        Dim frmGroup = FrmMdiMain.MdiChildren.OfType(Of FrmFamilyGroup)().FirstOrDefault()

        If frmGroup Is Nothing Then
            ' Si no existe, lo creamos
            frmGroup = New FrmFamilyGroup With
                {
                    .MdiParent = FrmMdiMain,
                    .IsNewGroupWithNoMembers = True
                }
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

        ConfigureGridColumns(PaymentMethods.Grupal)

        Dim searchText = TxtListGroupsDailyPayment.Text.Trim()

        DgvListGroupsDailyPayment.DataSource = _clientManager.SearchFamilyGroup(searchText)

        '| Verificación de texto vacío y Limpieza total
        If String.IsNullOrWhiteSpace(searchText) Then

            ResetGroupUI(True)
            DgvListGroupsDailyPayment.CurrentCell = Nothing

            Exit Sub

        End If

        '| Buscar coincidencia exacta en los resultados
        Dim matchRow = DgvListGroupsDailyPayment.Rows.Cast(Of DataGridViewRow)().
                       FirstOrDefault(Function(r) r.Cells("colNameDailyGroup").Value?.ToString() = searchText)

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

        '| * CONFIRMAR LA AMPLIACIÓN DEL CUPO
        Dim response As DialogResult = MessageBox.Show(DialogMessages.ConfirmAddExtraGroupMember(_groupName, _groupMemberLimit),
                                                       "Comprobar datos",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                                       MessageBoxDefaultButton.Button2)
        If response <> DialogResult.Yes Then Exit Sub

        '| * COMPROBAR QUE EXISTE TARIFA PARA EL NUEVO NÚMERO DE INTEGRANTES
        Dim newNumberMembers As Integer = _groupMemberLimit + 1

        If Not _clientManager.HasGroupRate(newNumberMembers) Then

            Dim rateResponse As DialogResult = MessageBox.Show(DialogMessages.AskBeforeRegisterNewRate(newNumberMembers),
                                                               "Tarifa no encontrada",
                                                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                                               MessageBoxDefaultButton.Button2)
            If rateResponse <> DialogResult.Yes Then Exit Sub

            Using frm As New FrmPricesAndDiscounts()

                frm.IsGroupRateRequest = True
                frm.SuggestedNumberMembers = newNumberMembers

                If frm.ShowDialog(Me) <> DialogResult.OK Then Exit Sub

            End Using

        End If

        '| * AMPLIAR CAPACIDAD
        _currentGroupMaxMembers = newNumberMembers
        _shouldExpandGroup = True

        LblNumberMembers.ForeColor = Color.FromArgb(255, 128, 0)
        LblNumberMembers.Text = "1 Cupo pendiente por registrar."

        BtnExpandCapacity.Enabled = False
        ErrorProvider.Clear()

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

        '| * VALIDACIONES DEL FORMULARIO : Datos del cliente antes de guardar el registro.
        If Not ValidateForm("guardar") Then Exit Sub
        If Not ValidateGroupCapacity("guardar", BtnExpandCapacity) Then Exit Sub

        '| * VALIDACIÓN DE AMPLIACIÓN
        If _shouldExpandGroup Then

            Dim result As DialogResult = MessageBox.Show(ShowCapacityExpansionWarning, "Aviso importante",
                                                         MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                                                         MessageBoxDefaultButton.Button2)
            If result = DialogResult.Cancel Then Exit Sub

        End If

        '| -------------------
        '| PROCESO DE GUARDADO
        '| -------------------
        Try
            Dim data As ClientPaymentDTO = GetClientDataFromForm(isUpdate:=False)

            _clientManager.RegisterClientPayment(data)

            ShowSuccessMessage(data.IdNewClient)

        Catch ex As Exception
            MessageBox.Show("Error INSERT cliente:" & vbCrLf & ex.Message, "Error al registrar")
        End Try

    End Sub


    Private Sub BtnUpdateCustomerData_Click(sender As Object, e As EventArgs) Handles BtnUpdateCustomerData.Click

        '| * VALIDACIONES DEL FORMULARIO : Datos del cliente antes de actualizar el registro.
        If Not ValidateForm("actualizar") Then Exit Sub

        '| * RECOLECTAR DATOS : Crear el DTO con los nuevos cambios.
        Dim data As ClientPaymentDTO = GetClientDataFromForm(isUpdate:=True)

        '| * DETERMINAR SI ES UNA NUEVA INCORPORACIÓN A GRUPO : Si el cliente NO tenía grupo
        '|   se le ha asigna uno (data.IdGroup > 0)
        Dim isNewEnrollment As Boolean = False

        If (_customerData.IdGroup Is Nothing OrElse _customerData.IdGroup = 0) AndAlso
            (data.IdGroup.HasValue AndAlso data.IdGroup > 0) Then
            isNewEnrollment = True
        End If

        '| * SI YA PERTENECE A UN GRUPO : isNewEnrollment DEBE ser False para que el Manager
        '|   no ejecute la suma +1
        If _customerData.IdGroup = data.IdGroup Then
            isNewEnrollment = False
        End If

        '| ------------------------
        '| PROCESO DE ACTUALIZACIÓN
        '| ------------------------
        Try
            Dim success = _clientManager.UpdateClientProcess(data, isNewEnrollment, _shouldExpandGroup)

            If success Then
                ShowSuccessMessage(data.IdNewClient)
            Else
                MessageBox.Show("Error al actualizar los datos.", "Error")
            End If


        Catch ex As Exception
            MessageBox.Show("Error UPDATE cliente:" & vbCrLf & ex.Message, "Error al actualizar")
        End Try

    End Sub


    Private Sub BtnCancelRegistration_Click(sender As Object, e As EventArgs) Handles BtnCancelRegistration.Click

        ' Close disparará el evento FormClosing haciendo las comprobaciones necesarias
        ' para determinar si hay cambios pendientes por guardar o actualizar.
        Me.Close()

    End Sub


#End Region


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " 1. CONFIGURACIÓN E INICIALIZACIÓN "

    ''' <summary>
    ''' Asigna el delegado de refresco que será invocado al finalizar
    ''' exitosamente el registro o edición.
    ''' </summary>
    Public Sub SetRefreshAction(action As Action(Of Integer))
        _onSuccessAction = action
    End Sub


    ''' <summary>
    ''' Prepara el formulario para registrar un nuevo cliente.
    ''' Reinicia el estado interno, limpia la referencia del cliente
    ''' en edición y restablece la configuración visual por defecto.
    ''' </summary>
    Public Sub PrepareForNewClient()

        _currentMode = TransactionMode.NewRecord

        ' Resetear el DTO para que no tenga datos del cliente anterior
        _customerData = New ClientPaymentDTO()
        _originalDataCustomer = Nothing

        ' Configurar botones
        BtnSaveCustomerData.Visible = True
        BtnUpdateCustomerData.Visible = False

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
    ''' DTO con datos originales del cliente seleccionados para edición.
    ''' </param>
    Public Sub PrepareToModifyClient(clientData As IndividualPaymentDTO)

        _currentMode = TransactionMode.EditRecord

        _isSwitching = True ' Levantamos el semáforo.

        _originalDataCustomer = clientData.Clone() ' Guardamos los datos originales.

        '| * DATOS DEL CLIENTE 1. Llenamos los campos con la información recibida

        _customerData.IdNewClient = clientData.IdCli
        TxtFirstName.Text = clientData.FirstName
        TxtLastName.Text = clientData.LastName
        DtpBirthdate.Value = clientData.BirthDate
        TxtPhone.Text = clientData.Phone
        TxtEmail.Text = clientData.Email
        TxtAddress.Text = clientData.Address
        DtpRegistrationDate.Value = clientData.RegistrationDate
        _customerData.IdGroup = clientData.IdGroup

        '| * ESTADO : (Activo/Inactivo)

        If clientData.State = EntityStatus.Active Then
            RbActiveStatus.Checked = True
        Else
            RbInactiveState.Checked = True
        End If

        '| * MÉTODO DE PAGO

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

        '| * AJUSTES VISUALES

        ChkRegistrationDate.Enabled = False ' MODO UPDATE : La fecha de inscripción NO se puede modificar.
        BtnSaveCustomerData.Visible = False
        BtnUpdateCustomerData.Visible = True

        ' Evita que los eventos CheckedChanged se ejecuten mientras el formulario se carga programáticamente.
        ' Bajamos el semáforo para que el usuario ya pueda interactuar
        _isSwitching = False

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
            AddHandler textBox.LostFocus, Sub(s, e) ValidateCustomerNameUI(DirectCast(s, TextBox), Me.ErrorProvider)
        Next

        For Each textBox In optionalFields
            ' Suscribimos el evento GotFocus (Cambio a Beige)
            AddHandler textBox.GotFocus, Sub(s, e) DirectCast(s, TextBox).BackColor = Color.Beige
            ' Suscribimos el evento LostFocus (Validación y Azure)
            AddHandler textBox.LostFocus, Sub(s, e) ValidateOptionalFieldUI(DirectCast(s, TextBox), Me.ErrorProvider)
        Next

    End Sub

#End Region


#Region " 2. MAPPING Y TRANSFORMACIÓN DE DATOS "

    ''' <summary>
    ''' Obtiene la información ingresada en el formulario y construye un objeto ClientPaymentDTO.
    ''' </summary>
    ''' <param name="isUpdate">
    ''' Indica si los datos corresponden a una actualización de cliente existente.
    ''' </param>
    ''' <returns>
    ''' Objeto ClientPaymentDTO con los datos actuales del formulario.
    ''' </returns>
    ''' <remarks>
    ''' Este método centraliza el mapeo entre los controles de la interfaz y
    ''' el DTO utilizado por la lógica de negocio.
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
                .State = If(RbActiveStatus.Checked, EntityStatus.Active, EntityStatus.Inactive),
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
    ''' Obtiene el método de pago actualmente seleccionado en el formulario.
    ''' </summary>
    ''' <returns>
    ''' Nombre del método de pago seleccionado:
    ''' - MENSUAL
    ''' - GRUPAL
    ''' - DIARIO
    ''' </returns>
    Private Function GetCurrentPaymentMethod() As String

        If RbMonthlyPayment.Checked Then Return PaymentMethods.Monthly

        If RbGroupPayment.Checked Then Return PaymentMethods.Grupal

        If RbDailyPayment.Checked Then Return TxtListGroupsDailyPayment.Text.Trim()

        Return "DESCONOCIDO"

    End Function

#End Region


#Region " 3. LÓGICA DE NEGOCIO Y ESTADO DEL FORMULARIO "

    ''' <summary>
    ''' Comprueba si existen cambios no guardados en el formulario
    ''' comparando los valores actuales con la copia original del cliente.
    ''' </summary>
    ''' <returns><c>True</c> si existe al menos una modificación pendiente;
    ''' de lo contrario, <c>False</c></returns>
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
        Dim currentStatus As String = If(RbActiveStatus.Checked, EntityStatus.Active, EntityStatus.Inactive)
        If currentStatus <> _originalDataCustomer.State Then Return True

        ' --- Método de Pago ---
        ' Comparas el valor seleccionado dentro de HasUnsavedChanges contra el original
        If GetCurrentPaymentMethod() <> _originalDataCustomer.PaymentMethod Then Return True

        ' Si llegó hasta aquí, es que todo es idéntico
        Return False

    End Function


    ''' <summary>
    ''' Cierra el formulario omitiendo los controles
    ''' defensivos de cambios pendientes.
    ''' </summary>
    Public Sub ForceClose()
        _isSaving = True
        Me.Close()
    End Sub

#End Region


#Region " 4. VALIDACIONES Y MENSAJES "

    ''' <summary>
    ''' Ejecuta las validaciones principales del formulario antes
    ''' de registrar o actualizar la información del cliente.
    ''' </summary>
    ''' <param name="actionText"> Texto descriptivo de la acción actual.
    ''' Ejemplo: "guardar" o "actualizar".</param>
    ''' <returns><c>True</c> si todas las validaciones fueron superadas correctamente;
    ''' de lo contrario, <c>False</c>.</returns>
    ''' <remarks>
    ''' Esta función centraliza las validaciones relacionadas con:
    ''' - Campos obligatorios.
    ''' - Edad mínima permitida.
    ''' - Método de pago seleccionado.
    ''' - Selección de tarifas o grupos familiares.
    ''' - Disponibilidad de cupos en grupos familiares.
    '''
    ''' El proceso se detiene inmediatamente al encontrar una validación inválida.
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


    ''' <summary>
    ''' Notifica el éxito de la operación, después de registrar o actualizar,
    ''' ejecuta el delegado de refresco y cierra el formulario.
    ''' </summary>
    ''' <param name="customerCode">
    ''' Identificador del cliente procesado.
    ''' </param>
    Private Sub ShowSuccessMessage(customerCode As Integer)

        '| Cuerpo del texto.
        Dim fullName As String = $"{TxtFirstName.Text} {TxtLastName.Text}"
        Dim idFormatted As String = $"CLI - {customerCode:000}"
        Dim actionText As String = If(BtnSaveCustomerData.Enabled, "GUARDADOS", "ACTUALIZADOS")

        '| Mensaje de confirmación.
        MessageBox.Show(DialogMessages.OperationSuccessMessage(EntityNames.Client, fullName, idFormatted, actionText),
                        "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

        '| Notificar al formulario, esto ejecutará la función que pasamos por AddressOf.
        _onSuccessAction?.Invoke(customerCode)

        '| Cerrar el formulario
        _isSaving = True
        Me.Close()

    End Sub

#End Region


#Region " 5. GESTIÓN VISUAL Y CONTROLES "

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
    ''' Actualiza el estado visual y la disponibilidad de la ampliación
    ''' de capacidad del grupo familiar según su ocupación
    ''' y la situación actual del cliente.
    ''' </summary>
    ''' <param name="isFull">Indica si el grupo familiar
    ''' alcanzó su cupo máximo.</param>
    Private Sub UpdateExpansionUI(isFull As Boolean)

        ' Por defecto, el botón permanece desactivado.
        BtnExpandCapacity.Enabled = False
        ErrorProvider.SetError(BtnExpandCapacity, String.Empty)

        If Not isFull Then
            LblNumberMembers.ForeColor = SystemColors.ControlText
            Exit Sub
        End If

        ' El grupo está completo.
        LblNumberMembers.ForeColor = Color.FromArgb(192, 0, 0)

        ' Un cliente que ya pertenece a un grupo nunca puede solicitar una ampliación.
        Dim clientAlreadyBelongsToGroup As Boolean = (_originalDataCustomer IsNot Nothing) AndAlso
                                                      _originalDataCustomer.IdGroup.HasValue AndAlso
                                                     (_originalDataCustomer.IdGroup.Value > 0)

        If clientAlreadyBelongsToGroup Then Exit Sub

        ' Grupo completo + cliente sin grupo = puede ampliar.
        BtnExpandCapacity.Enabled = True

        ErrorProvider.SetError(BtnExpandCapacity, DialogMessages.FullFamilyGroup(_groupName))

    End Sub


    ''' <summary>
    ''' Restablece el estado visual y lógico relacionado
    ''' con la selección de grupos familiares.
    ''' </summary>
    ''' <param name="clearLabel">Indica si el texto informativo
    ''' debe limpiarse completamente o mostrar
    ''' el mensaje de búsqueda.</param>
    Private Sub ResetGroupUI(clearLabel As Boolean)

        _selectedGroupId = 0
        _currentGroupMaxMembers = 0
        BtnExpandCapacity.Enabled = False
        ErrorProvider.Clear()

        LblNumberMembers.ForeColor = Color.FromArgb(0, 64, 0) 'COLOR VERDE
        LblNumberMembers.Text = If(clearLabel, "", AppMessages.SearchingGroup)

    End Sub


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

#End Region


#Region " 6. ESTRUCTURAS Y ENUMS AUXILIARES "

    ''' <summary>
    ''' Modos de transacción soportados por la interfaz de gestión de clientes.
    ''' </summary>
    Public Enum TransactionMode
        NewRecord
        EditRecord
    End Enum

#End Region


End Class