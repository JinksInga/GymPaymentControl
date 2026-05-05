
Imports System.Runtime.CompilerServices
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.Utils
Imports Mysqlx.XDevAPI.Common

Public Class FrmNewModifyClient

    ' --- Variables de "Herramientas" (Nivel Formulario) ---
    Private ReadOnly _clientManager As New ClientManager()
    Private _customerData As New ClientPaymentDTO()
    Private _originalDataCustomer As New ClientPaymentDTO()

    ' --- Variable privada para guardar la "llave" del refresco con parámetro ---
    Private _onSuccessAction As Action(Of Integer)

    ' --- Variables de "Estado" ---
    Private _isSwitching As Boolean = False
    Private _shouldExpandGroup As Boolean

    ' --- Variables de Memoria (Nivel Formulario) ---
    Private _selectedGroupId As Integer? = Nothing
    Private _currentGroupMaxMembers As Integer = 0
    Private _strNameGroup As String
    Private _intNumMembers, _intMembersReg As Integer

    ''
    ''
    Private Sub FrmNewModifyClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Usamos DateTime.Today para evitar problemas con las horas
        Dim today = DateTime.Today
        Dim currentYear = today.Year

        ' Configuración de límites de fecha (Mucho más limpio)
        DtpBirthdate.MinDate = New Date(currentYear - 90, 1, 1)
        DtpBirthdate.MaxDate = today

        ' Si estamos en modo "NUEVO/GUARDAR"
        If BtnSaveCustomerData.Enabled Then

            ' Fecha sugerida (25 años atrás)
            DtpBirthdate.Value = New Date(currentYear - 25, 7, 1)

            ' Limpiamos y preparamos para el nuevo registro
            LblCustomerAge.Text = ""
            LblCustomerAge.BackColor = Color.Azure

            ' Límites para la fecha de registro (2 años atrás, 2 adelante)
            DtpRegistrationDate.MinDate = New Date(currentYear - 2, 1, 1)
            DtpRegistrationDate.MaxDate = New Date(currentYear + 2, 12, 31)
            DtpRegistrationDate.Value = today

        End If

        '
        SetupTextBoxEvents() ' Activamos las "luces" de los campos
        ErrorProvider.Clear()

    End Sub
    Private Sub FrmNewModifyClient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        ' Solo preguntamos si el cierre es por el usuario (X o Me.Close) y hay cambios
        If e.CloseReason = CloseReason.UserClosing AndAlso HasUnsavedChanges() Then

            ' Construimos el cuerpo del mensaje tal cual tu imagen
            Dim strMsgbox As String = "                                 ¡ ¡ ¡  ATENCIÓN  ! ! !" & Environment.NewLine & Environment.NewLine &
                        "     Hay cambios en el formulario que no han sido guardados." & Environment.NewLine &
                        "     _________________________________________________________" & Environment.NewLine & Environment.NewLine &
                        "     ¿Realmente deseas salir y descartar la información?" & Environment.NewLine & Environment.NewLine &
                        "           SI : Cancelar los cambios y cerrar la ventana." & Environment.NewLine & Environment.NewLine &
                        "           NO : Volver al formulario para guardar."

            Dim result = MessageBox.Show(strMsgbox, "Cambios sin guardar",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button2)
            If result = DialogResult.No Then
                ' Si el usuario dice que NO, ¡DETENEMOS el cierre de la ventana!
                e.Cancel = True
            End If
        End If
        ' Si el usuario dice que SÍ, no hacemos nada extra, 
        ' dejamos que el evento siga su curso y la ventana se cierre.
    End Sub
    ''
    ''
    Private Sub TxtFirstName_TextChanged(sender As Object, e As EventArgs) Handles TxtFirstName.TextChanged
    End Sub
    Private Sub TxtFirstName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFirstName.KeyPress

        '| -----------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE LETRAS Y ESPACIO
        '| ---------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Almacenamos en la variable strLockKey los caracteres que queremos EXCLUIR.
        '| * Llamamos a la subrutina Fun_Only_Letters y le pasamos las variables como parámetro.

        Dim strAllowKey As String = " "
        Dim strLockKey As String = "ºª"
        ValidateOnlyLetters(strAllowKey, strLockKey, e)

    End Sub
    ''
    ''
    Private Sub TxtLastName_TextChanged(sender As Object, e As EventArgs) Handles TxtLastName.TextChanged
    End Sub
    Private Sub TxtLastName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtLastName.KeyPress

        '| -----------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE LETRAS Y ESPACIO
        '| ---------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Almacenamos en la variable strLockKey los caracteres que queremos EXCLUIR.
        '| * Llamamos a la subrutina Fun_Only_Letters y le pasamos las variables como parámetro.

        Dim strAllowKey As String = " "
        Dim strLockKey As String = "ºª"
        ValidateOnlyLetters(strAllowKey, strLockKey, e)

    End Sub
    ''
    ''
    Private Sub DtpBirthdate_ValueChanged(sender As Object, e As EventArgs) Handles DtpBirthdate.ValueChanged
        '3' En cuanto el usuario cambia la fecha, restauramos el formato
        'DtpBirthdate.CustomFormat = "dd ' de ' MMMM ' de ' yyyy"
        ''

        '| ---------------------------------------------------------------------------------------
        '| CALCULAR LA EDAD DEL CLIENTE
        '| ----------------------------
        '| * Almacenamos en la variable dtDateOfBirth la fecha de nacimiento que se obtiene del DtpBirthdate
        '| * Para calcular los años llamamos a la función Fun_Calculate_Age() y le pasamos la variable _
        '|   _ dtDateOfBirth, está función nos devuelve un valor entero que lo mostramos en el label TxtCustomerAge.  

        Dim dtDateOfBirth As Date = DtpBirthdate.Value
        LblCustomerAge.Text = CalculateClientAge(dtDateOfBirth) & " años"

        If CInt(Val(LblCustomerAge.Text)) < 5 Then
            ErrorProvider.SetError(LblCustomerAge, "La edad del cliente no puede ser inferior a 5 años")
            LblCustomerAge.BackColor = Color.MistyRose

        Else
            LblCustomerAge.BackColor = Color.Beige
            ErrorProvider.Clear()
        End If

    End Sub
    Private Sub DtpBirthdate_GotFocus(sender As Object, e As EventArgs) Handles DtpBirthdate.GotFocus

        '| -------------------------------------------------------------------------------
        '| CAMBIAR EL COLOR Y DAR FORMATO AL DATETIMEPICKER
        '| ------------------------------------------------
        '| * Al recibir el emfoque cambiammos el color del fondo del Textbox y le damos _
        '|   _ formato al DtpBirthdate con una fecha personalizada.

        LblCustomerAge.BackColor = Color.Beige
        DtpBirthdate.CustomFormat = "'  'dd'  de  'MMMM'  de  'yyyy"

    End Sub
    Private Sub DtpBirthdate_LostFocus(sender As Object, e As EventArgs) Handles DtpBirthdate.LostFocus

        '| --------------------------------------------------------------------------------------------
        '| VALAIDACIONES AL PERDER EL ENFOQUE
        '| ----------------------------------
        '| * Llamamos a la subrutina Sub_TxtLost_Focus() y le pasamos el Label (LblCustomerAge)
        ValidateCustomerAge(LblCustomerAge, ErrorProvider)

    End Sub
    ''
    ''
    Private Sub TxtPhone_TextChanged(sender As Object, e As EventArgs) Handles TxtPhone.TextChanged
    End Sub
    Private Sub TxtPhone_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPhone.KeyPress

        '| -----------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE NÚMEROS, PARÉNTESIS, GUION Y ESPACIO
        '| ----------------------------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Llamamos a la subrutina Sub_Only_Numbers y le pasamos la variable como parámetro.

        Dim strAllowKey As String = "(-) "
        ValidateIntegerNumbers(strAllowKey, e)

    End Sub
    ''
    ''
    Private Sub TxtEmail_TextChanged(sender As Object, e As EventArgs) Handles TxtEmail.TextChanged
    End Sub
    Private Sub TxtEmail_KeyUp(sender As Object, e As KeyEventArgs) Handles TxtEmail.KeyUp

        '| -----------------------------------------------------------------------------------
        '| VALAIDACIONES AL SOLTAR LA TECLA PRESIONADA
        '| -------------------------------------------
        '| IF : Comrpobamos si Fun_IsValid_Email no cumple con el formato del E-Mail.
        '|      * Mostrar el error si el formato es incorrecto.
        '|      * Cambiamos el color del fondo.
        '| ELSE : 
        '|      * Limpiamos el error.
        '|      * Cambiamos el color del fondo.

        If Not IsValidEmail(TxtEmail.Text) Then
            ErrorProvider.SetError(TxtEmail, "Ingresa un formato de E-Mail válido (usuario@dominio.com)")
            TxtEmail.BackColor = Color.MistyRose
        Else
            ErrorProvider.Clear()
            TxtEmail.BackColor = Color.Beige
        End If

    End Sub
    Private Sub TxtEmail_LostFocus(sender As Object, e As EventArgs) Handles TxtEmail.LostFocus

        '| -----------------------------------------------------------------------------------
        '| VALAIDACIONES AL PERDER EL ENFOQUE
        '| ----------------------------------
        '| * Llamamos a la subrutina Sub_TxtLost_Focus() y le pasamos como parámetro el TextBox'Sub_TxtLost_Focus(TxtEmail)
        '| IF : Comrpobamos si el TxtEmail no está vacio Y si Fun_IsValid_Email no cumple con el formato del E-Mail
        '|      * Mostrar el error si el formato es incorrecto.
        '|      * Cambiamos el color del fondo.

        If Not String.IsNullOrWhiteSpace(TxtEmail.Text) And Not IsValidEmail(TxtEmail.Text) Then
            ErrorProvider.SetError(TxtEmail, "Ingresa un formato de E-Mail válido (usuario@dominio.com)")
            TxtEmail.BackColor = Color.MistyRose
        End If

    End Sub
    ''
    ''
    Private Sub TxtAddress_TextChanged(sender As Object, e As EventArgs) Handles TxtAddress.TextChanged
    End Sub
    Private Sub TxtAddress_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtAddress.KeyPress

        '| -----------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE CARACTERES PARA LA DIRECCIÓN
        '| --------------------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Llamamos a la subrutina Fun_Only_Letters y le pasamos las variables como parámetro.

        Dim strAllowKey As String = "(&'.-/) "
        ValidateNumbersAndLetters(strAllowKey, e)

    End Sub
    ''
    ''
    Private Sub RbDailyPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbDailyPayment.CheckedChanged

        If RbDailyPayment.Checked Then
            ' 1. Configuramos UI
            GbListaGrupoFamiliar.Text = "Lista de pagos diarios:"
            TxtListGroupsDailyPayment.Clear()
            DgvListGroupsDailyPayment.Enabled = True

            ' 2. Cargamos datos con nuestra Función Camaleón
            ConfigureGridColumns("DIARIO")
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetDailyPrice()
            DgvListGroupsDailyPayment.CurrentCell = Nothing

        End If

    End Sub
    ''
    ''
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
    ''
    ''
    Private Sub RbGroupPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbGroupPayment.CheckedChanged

        If _isSwitching Then Exit Sub

        If RbGroupPayment.Checked Then
            ' Preparar Interfaz
            TxtListGroupsDailyPayment.Clear()
            LblNumberMembers.Text = ""
            GbListaGrupoFamiliar.Text = "Lista de grupos familiares:"

            BtnAddGroup.Enabled = True
            TxtListGroupsDailyPayment.Enabled = True
            DgvListGroupsDailyPayment.Enabled = True

            ' Cargar datos
            ConfigureGridColumns("GRUPAL")
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetNameGroupFamily()
            DgvListGroupsDailyPayment.CurrentCell = Nothing

            TxtListGroupsDailyPayment.Focus()
        Else
            If BtnSaveCustomerData.Enabled Then
                ' NUEVO/GUARDAR : Simplemente deshabilitamos
                BtnAddGroup.Enabled = False
                TxtListGroupsDailyPayment.Enabled = False
                LblNumberMembers.Text = ""
                ResetGroupUI(True)

            Else
                ' MODIFICAR/ACTUALIZAR
                ' NO dejamos cambiar el método de pago
                MessageBox.Show("   No se puede cambiar el MÉTODO de pago de un cliente que" & vbCrLf &
                                "   pertenece a un grupo familiar." & vbCrLf & vbCrLf &
                                "   Si quieres cambiar tienes que eliminar el grupo FAMILIAR.",
                                "Error al cambiar método de pago", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                _isSwitching = True
                RbGroupPayment.Checked = True
                _isSwitching = False

            End If
        End If

    End Sub
    ''
    ''
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
    ''
    ''
    Private Sub TxtListGroupsDailyPayment_TextChanged(sender As Object, e As EventArgs) Handles TxtListGroupsDailyPayment.TextChanged

        If Not RbGroupPayment.Checked Then Exit Sub

        ' 1. Filtrar el Grid mientras el usuario escribe
        ConfigureGridColumns("GRUPAL")
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
            _strNameGroup = matchRow.Cells("colNameDailyGroup").Value.ToString()
            _intNumMembers = CInt(matchRow.Cells("colNumMembers").Value)
            _intMembersReg = CInt(matchRow.Cells("colMembersReg").Value)

            LblNumberMembers.Text = $"Registrados {_intMembersReg} de {_intNumMembers}"

            UpdateExpansionUI(_intNumMembers = _intMembersReg)

            _currentGroupMaxMembers = _intNumMembers
        Else
            ' --- SIN COINCIDENCIA EXACTA ---
            ResetGroupUI(False)
        End If

        ' Siempre reiniciamos la intención de expandir al cambiar el texto
        _shouldExpandGroup = False

    End Sub
    ''
    ''
    Private Sub BtnExpandCapacity_Click(sender As Object, e As EventArgs) Handles BtnExpandCapacity.Click

        ' Preparamos el mensaje usando interpolación de strings (más moderno)
        Dim strMsgBox As String =
                $"    Nombre del grupo  : {_strNameGroup}{vbCrLf}" &
                $"    Nº de Integrantes   : {_intNumMembers}{vbCrLf}{vbCrLf}" &
                "    El grupo seleccionado ya tiene los integrantes completos." & vbCrLf &
                "    ___________________________________________________________" & vbCrLf & vbCrLf &
                "                        ¿Seguro que quieres añadir otro integrante?"


        If MsgBox(strMsgBox, vbExclamation + vbYesNo + vbDefaultButton2, "Comprobar datos") = vbYes Then
            'TxtListGroupsDailyPayment.Text = nameGroup
            _currentGroupMaxMembers += 1
            _shouldExpandGroup = True

            ' UI Feedback
            LblNumberMembers.ForeColor = Color.FromArgb(255, 128, 0) 'Color.Orange
            LblNumberMembers.Text = "1 Cupo pendiente por registrar."
            BtnExpandCapacity.Enabled = False
            ErrorProvider.Clear()

        End If

    End Sub
    ''
    ''
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
            _strNameGroup = row.Cells("colNameDailyGroup").Value.ToString()
            _intNumMembers = CInt(row.Cells("colNumMembers").Value)
            _intMembersReg = CInt(row.Cells("colMembersReg").Value)

            TxtListGroupsDailyPayment.Text = _strNameGroup
        End If

    End Sub
    ''
    ''
    ' Cuando el usuario hace clic en GUARDAR y la DB responde OK:
    Private Sub BtnSaveCustomerData_Click(sender As Object, e As EventArgs) Handles BtnSaveCustomerData.Click

        '| -----------------------------------------------------------
        '| COMPROBAMOS SI HAY INFORMACION DEL CLIENTE ANTES DE GUARDAR
        '| -----------------------------------------------------------
        '| * Llamamos a la función FunMsgBox() y le pasamos los parámetros, según sea el caso, para verificar que toda la
        '|   información del cliente sea correcta antes de guardar el registro.

        If FunMsgBox(LblNombre.Text, BtnSaveCustomerData.Text, TxtFirstName) Then Exit Sub
        If FunMsgBox(LblApellido.Text, BtnSaveCustomerData.Text, TxtLastName) Then Exit Sub
        If FunMsgBox(LblFnacimiento.Text, BtnSaveCustomerData.Text, LblCustomerAge, DtpBirthdate) Then Exit Sub
        If FunMsGbox(BtnSaveCustomerData.Text, RbDailyPayment, RbMonthlyPayment, RbGroupPayment) Then Exit Sub
        If FunMsgBox(RbDailyPayment.Text, BtnSaveCustomerData.Text, TxtListGroupsDailyPayment, RbDailyPayment) Then Exit Sub
        If FunMsgBox(RbGroupPayment.Text, BtnSaveCustomerData.Text, TxtListGroupsDailyPayment, RbGroupPayment) Then Exit Sub
        If FunMsgBox(BtnSaveCustomerData.Text, BtnExpandCapacity) Then Exit Sub

        '| -------------------
        '| PROCESO DE GUARDADO
        '| -------------------

        Try
            ' Creamos el "bolso" con los datos, recolectamos datos usando la función CreateClientPaymentDTO
            Dim data As ClientPaymentDTO = CreateClientPaymentDTO()

            ' Enviamos al Manager (Se encarga de la BBDD)
            _clientManager.RegisterClientPayment(data)

            ' Mostramos el mensaje de confirmación usando el IdNewClient
            ShowSuccessMessage(data.IdNewClient)

        Catch ex As Exception
            MessageBox.Show("Error INSERT cliente:" & vbCrLf & ex.Message, "Error al registrar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''
    ''
    Private Sub BtnUpdateCustomerData_Click(sender As Object, e As EventArgs) Handles BtnUpdateCustomerData.Click

        '| -------------------------------------------------------------------------------------------------------------------
        '| COMPROBAMOS SI HAY INFORMACION DEL CLIENTE ANTES DE ACTUALIZAR
        '| --------------------------------------------------------------
        '| * Llamamos a la función FunMsgBox() y le pasamos los parámetros, según sea el caso, para verificar que toda la
        '|   información del cliente sea correcta antes de actualizar el registro.

        If FunMsgBox(LblNombre.Text, BtnUpdateCustomerData.Text, TxtFirstName) Then Exit Sub
        If FunMsgBox(LblApellido.Text, BtnUpdateCustomerData.Text, TxtLastName) Then Exit Sub
        If FunMsgBox(LblFnacimiento.Text, BtnUpdateCustomerData.Text, LblCustomerAge, DtpBirthdate) Then Exit Sub
        If FunMsGbox(BtnUpdateCustomerData.Text, RbDailyPayment, RbMonthlyPayment, RbGroupPayment) Then Exit Sub
        If FunMsgBox(RbDailyPayment.Text, BtnUpdateCustomerData.Text, TxtListGroupsDailyPayment, RbDailyPayment) Then Exit Sub
        If FunMsgBox(RbGroupPayment.Text, BtnUpdateCustomerData.Text, TxtListGroupsDailyPayment, RbGroupPayment) Then Exit Sub

        '| -------------------------------------------------------------------------------------------------------------------
        '| ACTUALIZAR EL REGISTRO EN LA TABLA CLIENTES
        '| -------------------------------------------

        ' Crear el DTO con los nuevos cambios, recolectamos datos usando la función UpdateClientPaymentDTO
        Dim data As ClientPaymentDTO = UpdateClientPaymentDTO()

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
                MessageBox.Show("Hubo un error al actualizar los datos en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show("Error UPDATE cliente:" & vbCrLf & ex.Message, "Error al actualizar", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    ''
    ''
    Private Sub BtnCancelRegistration_Click(sender As Object, e As EventArgs) Handles BtnCancelRegistration.Click

        ' Close disparará el evento FormClosinghaciendo haciendo las comprobaciones necesarias
        ' para determinar si hay cambios pendientes por guardar o actualizar.
        Me.Close()

    End Sub
    ''
    ''
    '| ---------------------------------------------------------------- |'
    '| ---------->>>>>>>>>> SUBRUTINAS Y FUNCIONES <<<<<<<<<<---------- |'
    '| ---------------------------------------------------------------- |'

    Public Sub PrepareForNewClient()

        ' Limpiar cajas de texto
        TxtFirstName.Clear()
        TxtLastName.Clear()
        TxtPhone.Clear()
        ' ... etc ...

        ' Resetear el DTO para que no tenga datos del cliente anterior
        _customerData = New ClientPaymentDTO()

        ' Configurar botones
        BtnSaveCustomerData.Enabled = True
        BtnUpdateCustomerData.Enabled = False
        BtnUpdateCustomerData.BackColor = Color.Silver

        ' Configurar fechas por defecto
        DtpBirthdate.Format = DateTimePickerFormat.Custom
        DtpBirthdate.CustomFormat = " "
        DtpRegistrationDate.Value = DateTime.Today

    End Sub
    ''
    ''
    Private Function CreateClientPaymentDTO() As ClientPaymentDTO

        ' 1. Datos Básicos - Instanciamos la clase DTO
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
            .State = If(RbActiveStatus.Checked, "ACTIVO", "INACTIVO")
            }

        ' 2. Método de Pago y Lógica de Grupo
        If RbMonthlyPayment.Checked Then
            data.PaymentMethod = "MENSUAL"
            data.IsGroup = False

        ElseIf RbGroupPayment.Checked Then
            data.PaymentMethod = "GRUPAL"
            data.IsGroup = True
            data.IdGroup = _selectedGroupId
            data.GroupName = TxtListGroupsDailyPayment.Text.Trim()
            data.GroupMembers = _currentGroupMaxMembers
            data.ShouldExpandGroup = _shouldExpandGroup

        ElseIf RbDailyPayment.Checked Then
            data.PaymentMethod = TxtListGroupsDailyPayment.Text.Trim()
            data.IsGroup = False

        End If

        Return data

    End Function
    ''
    ''
    Public Sub PrepareToModifyClient(clientData As IndividualPaymentDTO)

        'Guardamos los datos originales
        _originalDataCustomer = clientData

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
        If clientData.State = "ACTIVO" Then
            RbActiveStatus.Checked = True
        Else
            RbInactiveState.Checked = True
        End If

        ' 3. Método de Pago
        Select Case clientData.PaymentMethod
            Case "MENSUAL" : RbMonthlyPayment.Checked = True

            Case "GRUPAL"
                RbGroupPayment.Checked = True
                TxtListGroupsDailyPayment.Text = clientData.GroupName

            Case Else '"DIARIO"
                RbDailyPayment.Checked = True
                TxtListGroupsDailyPayment.Text = clientData.PaymentMethod
                DgvListGroupsDailyPayment.CurrentCell = Nothing

        End Select

        ' 4. Ajustes Visuales
        BtnSaveCustomerData.Enabled = False
        BtnSaveCustomerData.BackColor = Color.Silver
        BtnUpdateCustomerData.Enabled = True

    End Sub
    ''
    ''
    Private Function UpdateClientPaymentDTO() As ClientPaymentDTO

        ' Instanciamos la clase DTO con los datos Básicos 
        Dim data As New ClientPaymentDTO With
           {
           .IdNewClient = _customerData.IdNewClient,
           .FirstName = TxtFirstName.Text.Trim(),
           .LastName = TxtLastName.Text.Trim(),
           .BirthDate = DtpBirthdate.Value,
           .Phone = TxtPhone.Text.Trim(),
           .Email = TxtEmail.Text.Trim(),
           .Address = TxtAddress.Text.Trim(),
           .RegistrationDate = DtpRegistrationDate.Value,
           .State = If(RbActiveStatus.Checked, "ACTIVO", "INACTIVO"),
           .IdGroup = _selectedGroupId
           }

        ' Comprobar el método de Pago
        If RbMonthlyPayment.Checked Then
            data.PaymentMethod = "MENSUAL"

        ElseIf RbGroupPayment.Checked Then
            data.PaymentMethod = "GRUPAL"

        ElseIf RbDailyPayment.Checked Then
            data.PaymentMethod = TxtListGroupsDailyPayment.Text.Trim()
        End If

        Return data

    End Function
    ''
    ''
    ' Método para recibir la acción desde el módulo
    Public Sub SetRefreshAction(action As Action(Of Integer))
        _onSuccessAction = action
    End Sub
    ''
    ''
    'Función para comprobar si hay información en los cuadros de texto
    Public Function HasUnsavedChanges() As Boolean

        ' CASO NUEVO: Si no hay foto original, usamos la lógica de "campos vacíos"
        If _originalDataCustomer Is Nothing Then
            ' Si el nombre o el apellido NO están vacíos, consideramos que hay cambios
            Return Not String.IsNullOrWhiteSpace(TxtFirstName.Text) OrElse
                   Not String.IsNullOrWhiteSpace(TxtLastName.Text)
        End If

        ' CASO EDICIÓN: Comparamos el texto actual vs el original
        ' Si alguno es diferente, devolvemos TRUE (hay cambios)
        If TxtFirstName.Text.Trim() <> _originalDataCustomer.FirstName Then Return True
        If TxtLastName.Text.Trim() <> _originalDataCustomer.LastName Then Return True
        If TxtPhone.Text.Trim() <> _originalDataCustomer.Phone Then Return True
        If TxtEmail.Text.Trim() <> _originalDataCustomer.Email Then Return True
        If TxtAddress.Text.Trim() <> _originalDataCustomer.Address Then Return True

        ' --- Fechas (DTPicker) ---
        ' Usamos .Date para comparar solo la fecha sin preocuparnos por la hora
        If DtpBirthdate.Value.Date <> _originalDataCustomer.BirthDate.Date Then Return True
        If DtpRegistrationDate.Value.Date <> _originalDataCustomer.RegistrationDate.Date Then Return True

        ' --- Estado (RadioButtons / Toggle) ---
        ' Asumiendo que _originalData.IsActive es un Booleano
        Dim currentStatus As String = If(RbActiveStatus.Checked, "ACTIVO", "INACTIVO")
        If currentStatus <> _originalDataCustomer.State Then Return True

        ' --- Método de Pago ---
        ' Comparas el valor seleccionado dentro de HasUnsavedChanges contra el original
        If GetCurrentPaymentMethod() <> _originalDataCustomer.PaymentMethod Then Return True

        ' Si llegó hasta aquí, es que todo es idéntico
        Return False
    End Function
    ''
    ''
    Private Sub ConfigureGridColumns(methodPay As String)

        ' Limpiamos cualquier vinculación previa
        DgvListGroupsDailyPayment.AutoGenerateColumns = False

        If methodPay = "DIARIO" Then
            ' Configuración para Pagos diarios
            With DgvListGroupsDailyPayment
                .Columns("colIdDailyGroup").DataPropertyName = "id_trfa"
                .Columns("colNameDailyGroup").DataPropertyName = "tipo_trfa"
            End With

        ElseIf methodPay = "GRUPAL" Then
            ' Configuración para Grupos Familiares
            With DgvListGroupsDailyPayment
                .Columns("colIdDailyGroup").DataPropertyName = "id_grp"
                .Columns("colNameDailyGroup").DataPropertyName = "nom_grp"
                .Columns("colNumMembers").DataPropertyName = "num_intgrntes_grp"
                .Columns("colMembersReg").DataPropertyName = "intgrntes_reg_grp"
            End With
        End If

    End Sub
    ''
    ''
    Private Sub UpdateExpansionUI(isFull As Boolean)

        If isFull Then
            LblNumberMembers.ForeColor = Color.FromArgb(192, 0, 0) 'COLOR ROJO
            BtnExpandCapacity.Enabled = True
            ErrorProvider.SetError(BtnExpandCapacity,
                                   $"El grupo {_strNameGroup} está lleno.
                                       {Environment.NewLine}Haga clic en el botón [Ampliar cupo] para agregar un integrante.")
        End If

    End Sub
    ''
    ''
    Private Sub ResetGroupUI(isFullClear As Boolean)

        _selectedGroupId = 0
        _currentGroupMaxMembers = 0
        BtnExpandCapacity.Enabled = False
        ErrorProvider.Clear()

        LblNumberMembers.ForeColor = Color.FromArgb(0, 64, 0) 'COLOR VERDE
        If isFullClear Then
            LblNumberMembers.Text = ""
        Else
            LblNumberMembers.Text = "Buscando grupo ..."
        End If

    End Sub
    ''
    ''
    Private Function GetCurrentPaymentMethod() As String

        If RbMonthlyPayment.Checked Then
            Return "MENSUAL"

        ElseIf RbGroupPayment.Checked Then
            Return "GRUPAL"

        ElseIf RbDailyPayment.Checked Then
            ' Como el TextBox ya tiene el formato "DIARIO 5", lo usamos tal cual
            Return TxtListGroupsDailyPayment.Text.Trim()
        End If

        Return "DESCONOCIDO"

    End Function
    ''
    ''
    Private Sub SetupTextBoxEvents()

        ' Creamos una lista de los TextBox que queremos validar
        Dim fieldsToValidate As TextBox() = {TxtFirstName, TxtLastName, TxtPhone, TxtEmail, TxtAddress, TxtListGroupsDailyPayment}

        For Each textBox In fieldsToValidate
            ' Suscribimos el evento GotFocus (Cambio a Beige)
            AddHandler textBox.GotFocus, Sub(s, e)
                                             DirectCast(s, TextBox).BackColor = Color.Beige
                                         End Sub

            ' Suscribimos el evento LostFocus (Validación y Azure)
            AddHandler textBox.LostFocus, Sub(s, e)
                                              ValidateCustomerData(DirectCast(s, TextBox), Me.ErrorProvider)
                                          End Sub
        Next

    End Sub
    ''
    ''
    Private Sub ShowSuccessMessage(idClient As Integer)

        ' 1. Preparamos el texto del cuerpo (Guardado vs Actualizado)
        Dim strActionText As String = If(BtnSaveCustomerData.Enabled, "GUARDADOS", "ACTUALIZADOS")
        Dim strIdFormatted As String = $"CLI - {idClient:000}"

        ' 2. Construimos el "Ticket" de confirmación (usando los datos que ya tenemos en los campos)
        Dim strMsgBox As String = "DATOS DEL CLIENTE" & vbCrLf & vbCrLf &
                                    "   NOMBRE   :  " & TxtFirstName.Text & " " & TxtLastName.Text & vbCrLf &
                                    "   CÓDIGO   :  " & strIdFormatted & vbCrLf &
                                    "   -----------------------------------------------" & vbCrLf &
                                    "   Datos " & strActionText & " correctamente."

        MessageBox.Show(strMsgBox, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' 3. Notificamos al formulario que debe refrescarse, esto ejecutará la funciónque pasamos por AddressOf
        _onSuccessAction?.Invoke(idClient)

        ' 4. Cerramos el formulario
        Me.Close()

    End Sub
    ''
    ''
    Overloads Function FunMsgBox(clientData As String, titleMsgbox As String, textBox As TextBox) As Boolean

        '| -------------------------------------------------------------------------------------------------
        '| IF : Comprobamos si el TextBox está vacío.
        '|
        '|      * Convertimos el texto de la variable clientData y titleMsgbox en mayúsculsa y minúsculas _
        '|        _ respectivamente usando UCase() y LCase(), también se puede usar ToUpper() y ToLower().
        '|      * Extraemos el nombre del botón BtnGuardar o BtnUpdateCustomerData, utilizando Substring() y lo _
        '|        _ convertimos en minúsculas usando LCase, para mostrarlo en el título de mensaje.
        '|      * Mostramos el mensaje con los datos recibidos por parámetro, enviamos el enfoque al _
        '|        _ textbox que corresponda.
        '|      * Return True para salir de la función y no ejecutar el resto del código.
        '|
        '| ELSE : Si el TextBox tiene datos
        '|
        '|      * Return False para seguir ejecutando el resto del código.

        If String.IsNullOrWhiteSpace(textBox.Text) Then
            clientData = UCase(clientData)
            titleMsgbox = LCase(titleMsgbox.Substring(1, titleMsgbox.Length - 1))
            MsgBox(" Verifica la información del cliente" & vbCr & vbCr &
                   " El campo " & clientData & " no puede estar vacío.", vbCritical, "Error al " & titleMsgbox)
            textBox.Focus()
            Return True
        Else
            Return False
        End If

    End Function
    Overloads Function FunMsgBox(clientData As String, titleMsgbox As String, label As Label, dateTimePicker As DateTimePicker) As Boolean

        '| -------------------------------------------------------------------------------------------------------------------------------
        '| IF : Comprobamos si el Label está vacío.
        '|      * Convertimos el texto de la variable clientData y titleMsgbox en mayúsculsa y minúsculas respectivamente usando UCase() _
        '|        _ y LCase(), también se puede usar ToUpper() y ToLower().
        '|      * Extraemos el nombre del botón BtnGuardar o BtnUpdateCustomerData según sea el caso, utilizando Substring() y lo convertimos en _
        '|        _ minúsculas usando LCase, para mostrarlo en el título de mensaje.
        '|      * Mostramos el mensaje con los datos recibidos por parámetro, enviamos el enfoque al dateTimePicker que corresponda.
        '|      * Return True para salir de la función y no ejecutar el resto del código.
        '| ELSE : Si el TextBox tiene datos
        '|      * Return False para seguir ejecutando el resto del código.

        If String.IsNullOrEmpty(label.Text) Or CInt(Val(label.Text)) < 5 Then
            clientData = UCase(clientData)
            titleMsgbox = LCase(titleMsgbox.Substring(1, titleMsgbox.Length - 1))
            MsgBox(" Verifica la " & clientData & " del cliente :" & vbCrLf & vbCrLf &
                   " 1. El campo no puede estar vacío." & vbCrLf &
                   " 2. No puede ser nemor de 5 años.", vbCritical, "Error al " & titleMsgbox)
            dateTimePicker.Focus()
            Return True
        Else
            Return False
        End If
    End Function
    Overloads Function FunMsGbox(titleMsgbox As String, rb1 As RadioButton, rb2 As RadioButton, rb3 As RadioButton) As Boolean

        '| -------------------------------------------------------------------------------------------------------------------
        '| IF : Comprobamos si los RadioButton no están seleccionados.
        '|      * Extraemos el nombre del botón BtnSaveCustomerData o BtnUpdateCustomerData según sea el caso, utilizando Substring() y lo _
        '|        _ convertimos en minúsculas usando LCase, para mostrarlo en el título de mensaje.
        '|      * Mostramos el mensaje con los datos recibidos por parámetro.
        '|      * Return True para salir de la función y no ejecutar el resto del código.
        '| ELSE : Si uno de los RadioButton está seleccionado.
        '|      * Return False para seguir ejecutando el resto del código.

        If Not rb1.Checked And Not rb2.Checked And Not rb3.Checked Then
            titleMsgbox = LCase(titleMsgbox.Substring(1, titleMsgbox.Length - 1))
            MsgBox("Selecciona un MÉTODO de pago.", vbCritical, "Error al " & titleMsgbox)
            Return True
        Else
            Return False
        End If
    End Function
    Overloads Function FunMsgBox(clientData As String, titleMsgbox As String, textBox As TextBox, radioButton As RadioButton) As Boolean

        '| -----------------------------------------------------------------------------------------------------------------------------
        '| IF : Comprobamos si está activado el RadioButton y el TextBox está vacío.
        '|      * Convertimos el texto de la variable clientData y titleMsgbox en mayúsculsa y minúsculas respectivamente usando UCase() _
        '|        _ y LCase(), también se puede usar ToUpper() y ToLower()
        '|      * Comprobamos si la variable clientData = "DIARIO" para agragar el texto "pago ".
        '|      * Extraemos el nombre del botón BtnGuardar o BtnUpdateCustomerData según sea el caso, utilizando Substring() y lo convertimos en _
        '|        _ minúsculas usando LCase, para mostrarlo en el título de mensaje.
        '|      * Mostramos el mensaje con los datos recibidos por parámetro, enviamos el enfoque al textbox que corresponda.
        '|      * Return True para salir de la función y no ejecutar el resto del código.
        '| ELSE : Si el TextBox tiene datos
        '|      * Return False para seguir ejecutando el resto del código.

        If radioButton.Checked And textBox.Text = "" Then
            clientData = UCase(clientData)
            If clientData = "DIARIO" Then clientData = "pago " & clientData
            titleMsgbox = LCase(titleMsgbox.Substring(1, titleMsgbox.Length - 1))
            MsgBox(" Verifica la información del cliente" & vbCr & vbCr &
                   " Selecciona un " & clientData & " de la lista.", vbCritical, "Error al " & titleMsgbox)
            textBox.Focus()
            Return True
        Else
            Return False
        End If
    End Function
    Overloads Function FunMsgBox(titleMsgbox As String, button As Button) As Boolean

        '| -------------------------------------------------------------------------------------------------
        '| IF : Comprobamos si está activado el Button.
        '|      * Extraemos el nombre del botón BtnSaveCustomerData, utilizando Substring() y lo convertimos
        '|        en minúsculas usando LCase, para mostrarlo en el título de mensaje.
        '|      * Mostramos el mensaje con los datos recibidos por parámetro.
        '|      * Enviamos el enfoque al button.
        '|      * Return True para salir de la función y no ejecutar el resto del código.
        '| ELSE : Si el Button está desactivado
        '|      * Return False para seguir ejecutando el resto del código.

        If button.Enabled = True Then
            titleMsgbox = LCase(titleMsgbox.Substring(1, titleMsgbox.Length - 1))
            MsgBox(" No se puede registrar un cliente en un GRUPO FAMILIAR completo." & vbCrLf & vbCrLf &
                   " Haz clic en el botón [Ampliar cupo] para admitir a un nuevo integrante." _
                   , vbCritical, "Error al " & titleMsgbox)
            button.Focus()
            Return True
        Else
            Return False
        End If
    End Function
    ''
    ''
End Class