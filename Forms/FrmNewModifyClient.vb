
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.Utils

Public Class FrmNewModifyClient

    ' --- Variables de "Herramientas" (Nivel Formulario) ---
    Private ReadOnly _clientManager As New ClientManager()
    Private _clientPaymentDTO As New ClientPaymentDTO()

    ' --- Variable privada para guardar la "llave" del refresco con parámetro ---
    Private _onSuccessAction As Action(Of Integer)

    ' --- Variables de "Estado" ---
    Private _isSwitching As Boolean = False
    Private _shouldExpandGroup As Boolean

    ' --- Variables de Memoria (Nivel Formulario) ---
    Private _selectedGroupId As Integer? = Nothing
    Private _currentGroupMaxMembers As Integer = 0
    ''
    ''
    Private Sub FrmNewModifyClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Usamos DateTime.Today para evitar problemas con las horas
        Dim today = DateTime.Today
        Dim currentYear = today.Year

        ' Configuración de límites de fecha (Mucho más limpio)
        DtpBirthdate.MinDate = New Date(currentYear - 90, 1, 1)
        DtpBirthdate.MaxDate = today

        ' Si estamos en modo "NUEVO" (Botón guardar visible)
        If BtnSaveCustomerData.Visible Then
            ' Formato vacío para que el usuario sepa que debe elegir una fecha
            DtpBirthdate.Format = DateTimePickerFormat.Custom
            DtpBirthdate.CustomFormat = " "

            ' Fecha sugerida (25 años atrás)
            DtpBirthdate.Value = New Date(currentYear - 25, 7, 1)
            TxtCustomerAge.Text = ""

            ' Límites para la fecha de registro (2 años atrás, 2 adelante)
            DtpRegistrationDate.MinDate = New Date(currentYear - 2, 1, 1)
            DtpRegistrationDate.MaxDate = New Date(currentYear + 2, 12, 31)
            DtpRegistrationDate.Value = today
        Else
            ' Modo edición: Formato descriptivo
            DtpBirthdate.Format = DateTimePickerFormat.Custom
            DtpBirthdate.CustomFormat = "dd ' de ' MMMM ' de ' yyyy"
        End If

    End Sub
    'Private Sub FrmNewModifyClient_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
    '    'Close()
    'End Sub
    ''
    ''
    Private Sub TxtFirstName_TextChanged(sender As Object, e As EventArgs) Handles TxtFirstName.TextChanged
        '1 TxtFirstName_TextChanged
    End Sub
    ''
    ''
    Private Sub TxtLastName_TextChanged(sender As Object, e As EventArgs) Handles TxtLastName.TextChanged
        '2 TxtLastName_TextChanged
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
        '| * Almacenamos en la variable dtDateOfBirth la fecha de nacimiento que se obtiene del DtpFdn
        '| * Para calcular los años llamamos a la función Fun_Calculate_Age() y le pasamos la variable _
        '|   _ dtDateOfBirth, está función nos devuelve un valor entero que lo mostramos en el label TxtEdad.  

        Dim dtDateOfBirth As Date = DtpBirthdate.Value
        TxtCustomerAge.Text = CalculateClientAge(dtDateOfBirth) & " años"

    End Sub
    Private Sub DtpBirthdate_GotFocus(sender As Object, e As EventArgs) Handles DtpBirthdate.GotFocus

        '| -------------------------------------------------------------------------------
        '| CAMBIAR EL COLOR Y DAR FORMATO AL DATETIMEPICKER
        '| ------------------------------------------------
        '| * Al recibir el emfoque cambiammos el color del fondo del Textbox y le damos _
        '|   _ formato al DtpFdn con una fecha personalizada.

        TxtCustomerAge.BackColor = Color.Beige
        DtpBirthdate.CustomFormat = "' ' dd ' de  ' MMMM ' de  ' yyyy"

    End Sub

    Private Sub DtpBirthdate_LostFocus(sender As Object, e As EventArgs) Handles DtpBirthdate.LostFocus

        '| -----------------------------------------------------------------------------------
        '| VALAIDACIONES AL PERDER EL ENFOQUE
        '| ----------------------------------
        '| * Llamamos a la subrutina Sub_TxtLost_Focus() y le pasamos como el Label (TxtEdad)
        Sub_TxtLost_Focus(TxtCustomerAge)

    End Sub
    ''
    ''
    Private Sub TxtPhone_TextChanged(sender As Object, e As EventArgs) Handles TxtPhone.TextChanged
        '4-TxtPhone_TextChanged
    End Sub
    ''
    ''
    Private Sub TxtEmail_TextChanged(sender As Object, e As EventArgs) Handles TxtEmail.TextChanged
        '5 TxtEmail_TextChanged
    End Sub
    ''
    ''
    Private Sub TxtAddress_TextChanged(sender As Object, e As EventArgs) Handles TxtAddress.TextChanged
        '6 TxtAddress_TextChanged
    End Sub
    ''
    ''
    Private Sub DtpRegistrationDate_ValueChanged(sender As Object, e As EventArgs) Handles DtpRegistrationDate.ValueChanged
        '7 DtpRegistrationDate_ValueChanged
    End Sub
    ''
    ''
    Private Sub RbActiveStatus_CheckedChanged(sender As Object, e As EventArgs) Handles RbActiveStatus.CheckedChanged
        '8 RbActiveStatus_CheckedChanged
    End Sub
    ''
    ''
    Private Sub RbInactiveState_CheckedChanged(sender As Object, e As EventArgs) Handles RbInactiveState.CheckedChanged
        '9 RbInactiveState_CheckedChanged
    End Sub
    ''
    ''
    Private Sub RbDailyPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbDailyPayment.CheckedChanged

        If RbDailyPayment.Checked Then
            ' 1. Configuramos UI
            GbListaGrupoFamiliar.Text = "Lista clases sueltas"
            DgvListGroupsDailyPayment.Enabled = True
            'strToolTipText = "CLIC PARA SELECCIONAR UN PAGO DIARIO"

            ' 2. Cargamos datos con nuestra Función Camaleón
            ConfigureGridColumns("DIARIO")
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetDailyPrice()

            ' 3. ¡IMPORTANTE!: Si ya hay algo escrito en el textbox (por ejemplo al editar), 
            ' actualizamos strMtdPgs de inmediato.
            'strMtdPgs = TxtListGroupsDailyPayment.Text
        End If

    End Sub
    Private Sub RbDailyPayment_Click(sender As Object, e As EventArgs) Handles RbDailyPayment.Click


        '| ---------------------------------------------------------------------------------------------------------
        '| LIMPIAR CUADRO DE TEXTO
        '| -----------------------
        '| * Al hacer click en el RadioButton 'RbMonthlyPayment' comprobamos el valor de la variable '_isSwitching';
        '|   la razón de esta comprobación es porque solo se debe limpiar el cuadro de texto si vamos a registrar un
        '|   nuevo cliente, ya que si estamos actualizando los datos del cliente no podemos cambiar su método de pago
        '|   si pertenece a un grupo familiar; por esa razon en el evento CheckedChanged del RbGroupPayment cambiamos
        '|   el valor de la variable '_isSwitching = True' para no borrar el nombre del grupo al que pertenece el
        '|   cliente en cuestión.

        If Not _isSwitching Then TxtListGroupsDailyPayment.Clear()

    End Sub
    ''
    ''
    Private Sub RbMonthlyPayment_CheckedChanged(sender As Object, e As EventArgs) Handles RbMonthlyPayment.CheckedChanged

        If RbMonthlyPayment.Checked Then

            _selectedGroupId = Nothing
            _currentGroupMaxMembers = 0
            '_strAddMembersAction = ""

            TxtListGroupsDailyPayment.Text = ""
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
            GbListaGrupoFamiliar.Text = "Lista de grupos familiares"

            BtnAddGroup.Enabled = True
            TxtListGroupsDailyPayment.Enabled = True
            DgvListGroupsDailyPayment.Enabled = True

            ' Cargar datos
            ConfigureGridColumns("GRUPAL")
            DgvListGroupsDailyPayment.DataSource = _clientManager.GetNameGroupFamily()

            TxtListGroupsDailyPayment.Focus()
            'strToolTipText = "DOBLE CLIC PARA SELECCIONAR UN GRUPO"
        Else
            ' Si estamos editando un cliente grupal, NO dejamos cambiar el método
            If BtnUpdateCustomerData.Visible Then
                MessageBox.Show("No se puede cambiar el método de pago de un cliente grupal." & vbCrLf &
                                "Primero debe eliminar el grupo familiar.", "Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning)

                _isSwitching = True
                RbGroupPayment.Checked = True ' Re-marcamos
                _isSwitching = False
            Else
                ' Modo nuevo: simplemente deshabilitamos
                BtnAddGroup.Enabled = False
                TxtListGroupsDailyPayment.Enabled = False
                LblNumberMembers.Text = ""
            End If
        End If
    End Sub
    ''
    ''
    Private Sub BtnAddGroup_Click(sender As Object, e As EventArgs) Handles BtnAddGroup.Click
        '13 BtnAddGroup_Click
    End Sub
    ''
    ''
    Private Sub TxtListGroupsDailyPayment_TextChanged(sender As Object, e As EventArgs) Handles TxtListGroupsDailyPayment.TextChanged

        If Not RbGroupPayment.Checked Then Exit Sub

        ' 1. Filtrar el Grid mientras el usuario escribe
        ConfigureGridColumns("GRUPAL")
        Dim searchText = TxtListGroupsDailyPayment.Text.Trim()
        DgvListGroupsDailyPayment.DataSource = _clientManager.SearchFamilyGroup(searchText)

        ' 2. Si está vacío, limpiamos
        If String.IsNullOrWhiteSpace(searchText) Then
            LblNumberMembers.Text = ""
            Exit Sub
        End If

        ' 3. ¿Hay coincidencia exacta? (Para saber si el grupo está seleccionado)
        If DgvListGroupsDailyPayment.RowCount > 0 Then
            ' Buscamos entre las filas del grid la que coincida exactamente con el texto
            Dim match = DgvListGroupsDailyPayment.Rows.Cast(Of DataGridViewRow)().
                        FirstOrDefault(Function(r) r.Cells("colNameDailyGroup").Value?.ToString().ToUpper() = searchText.ToUpper())

            If match IsNot Nothing Then
                Dim total = CInt(match.Cells("colNumMembers").Value)
                Dim reg = CInt(match.Cells("colMembersReg").Value)
                LblNumberMembers.Text = $"{reg} de {total}"

                '' Valores para el guardado posterior
                _selectedGroupId = CInt(match.Cells("colIdDailyGroup").Value)

                ' Por defecto, al buscar/cargar, asumimos que NO expandimos.
                ' Si se necesita expandir, el usuario lo decidirá haciendo Doble Clic en el Grid.
                _currentGroupMaxMembers = total
                _shouldExpandGroup = False

            Else
                ' Si el texto no coincide con nada exacto, reseteamos el ID
                _selectedGroupId = 0
                LblNumberMembers.Text = "..."

            End If
        End If
    End Sub

    Private Sub DgvListGroupsDailyPayment_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListGroupsDailyPayment.CellContentClick
        '15 DgvListGroupsDailyPayment_CellContentClick
    End Sub
    Private Sub DgvListGroupsDailyPayment_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListGroupsDailyPayment.CellClick

        If e.RowIndex < 0 Then Exit Sub

        If RbDailyPayment.Checked Then
            TxtListGroupsDailyPayment.Text = DgvListGroupsDailyPayment.CurrentRow.Cells("colNameDailyGroup").Value.ToString()
        End If

    End Sub
    Private Sub DgvListGroupsDailyPayment_DoubleClick(sender As Object, e As EventArgs) Handles DgvListGroupsDailyPayment.DoubleClick

        If DgvListGroupsDailyPayment.CurrentRow Is Nothing Then Exit Sub

        If RbGroupPayment.Checked Then
            Dim row = DgvListGroupsDailyPayment.CurrentRow
            Dim nameGroup As String = row.Cells("colNameDailyGroup").Value.ToString()
            Dim numMembers As Integer = CInt(row.Cells("colNumMembers").Value)
            Dim membersReg As Integer = CInt(row.Cells("colMembersReg").Value)

            ' --- NUEVO: Guardamos el ID para el DTO ---
            _selectedGroupId = CInt(row.Cells("colIdDailyGroup").Value) ' Asume que tienes esta columna

            If numMembers = membersReg Then
                ' Preparamos el mensaje usando interpolación de strings (más moderno)
                Dim strMsgBox As String =
                    $"    Nombre del grupo  : {nameGroup}{vbCr}" &
                    $"    Nº de Integrantes   : {numMembers}{vbCr}{vbCr}" &
                    "    El grupo seleccionado ya tiene los integrantes completos." & vbCr &
                    "    ___________________________________________________________" & vbCr & vbCr &
                    "                        ¿Seguro que quieres añadir otro integrante?"


                If MsgBox(strMsgBox, vbExclamation + vbYesNo + vbDefaultButton2, "Comprobar datos") = vbYes Then
                    TxtListGroupsDailyPayment.Text = nameGroup
                    _currentGroupMaxMembers = numMembers + 1 ' <--- Necesario para el precio
                    _shouldExpandGroup = True
                    'Else
                    '    _shouldExpandGroup = False ' <-- El usuario canceló la expansión
                    ' (Aquí podrías limpiar el campo de búsqueda si quieres)
                End If

            Else
                TxtListGroupsDailyPayment.Text = nameGroup
                LblNumberMembers.Text = $"{membersReg} de {numMembers}"
                _currentGroupMaxMembers = numMembers ' Mantiene el cupo actual
                _shouldExpandGroup = False
            End If

        End If

    End Sub
    ''
    ''
    ' Cuando el usuario hace clic en GUARDAR y la DB responde OK:
    Private Sub BtnSaveCustomerData_Click(sender As Object, e As EventArgs) Handles BtnSaveCustomerData.Click

        '| ---------------------------------------------------------------------------------------------
        '| COMPROBAMOS SI HAY INFORMACION DEL CLIENTE ANTES DE GUARDAR
        '| -----------------------------------------------------------
        '| * Llamamos a la función FunMsgBox() y le pasamos los parámetros, según sea el caso, para _
        '|   _ verificar que toda la información del cliente sea correcta antes de guardar el registro.
        ' --- 1. VALIDACIONES DE INTERFAZ (Fuera del Try) ---
        If FunMsgBox(LblNombre.Text, BtnSaveCustomerData.Text, TxtFirstName) Then Exit Sub
        If FunMsgBox(LblApellido.Text, BtnSaveCustomerData.Text, TxtLastName) Then Exit Sub
        If FunMsgBox(LblFnacimiento.Text, BtnSaveCustomerData.Text, TxtCustomerAge, DtpBirthdate) Then Exit Sub
        If FunMsGbox(BtnSaveCustomerData.Text, RbDailyPayment, RbMonthlyPayment, RbGroupPayment) Then Exit Sub
        If FunMsgBox(RbDailyPayment.Text, BtnSaveCustomerData.Text, TxtListGroupsDailyPayment, RbDailyPayment) Then Exit Sub
        If FunMsgBox(RbGroupPayment.Text, BtnSaveCustomerData.Text, TxtListGroupsDailyPayment, RbGroupPayment) Then Exit Sub


        ' --- 2. PROCESO DE GUARDADO ---
        Try
            ' Creamos el "bolso" con los datos
            ' Recolectamos los datos usando la función que acabamos de crear
            Dim data As ClientPaymentDTO = CreateClientPaymentDTO()

            ' Enviamos al Manager (él se encarga de la DB)
            _clientManager.RegisterClientPayment(data)

            ' --- 3. REACCIÓN DE LA INTERFAZ ---
            ' Mostramos el mensaje chulo de confirmación usando el IdNewClient que el Manager llenó
            ShowSuccessMessage(data.IdNewClient)
            'MessageBox.Show("Cliente registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' ¡MAGIA! En lugar de strFlags, usamos la acción que nos pasaron al abrir el form
            _onSuccessAction?.Invoke(data.IdNewClient)

            ' Cerramos el formulario ya que la tarea terminó
            Me.Close()

        Catch ex As Exception
            'MessageBox.Show("Error al registrar el cliente:" & vbCrLf & ex.Message, "Error de Base de Datos",
            '                MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Esto te dirá el mensaje Y el StackTrace (la lista de llamadas y líneas)
            Dim lineaError As String = ex.StackTrace.ToString()

            MessageBox.Show("Error: " & ex.Message & vbCrLf & vbCrLf &
                            "Ubicación: " & lineaError,
                            "Error de Depuración",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''
    ''
    Private Sub BtnUpdateCustomerData_Click(sender As Object, e As EventArgs) Handles BtnUpdateCustomerData.Click
        'BtnUpdateCustomerData_Click
    End Sub

    Private Sub BtnCancelRegistration_Click(sender As Object, e As EventArgs) Handles BtnCancelRegistration.Click
        'BtnCancelRegistration_Click
    End Sub
    ''
    ''
    '| ---------------------------------------------------------------- |'
    '| ---------->>>>>>>>>> SUBRUTINAS Y FUNCIONES <<<<<<<<<<---------- |'
    '| ---------------------------------------------------------------- |'

    Sub Sub_TxtLost_Focus(lblLabel As Label)

        '| ------------------------------------------------------------------------
        '| * Limpiamos cualquier error previo.
        '|
        '| IF : Si el label está vacio
        '|      * Activamos el ErrorProvider y cambiamos el color del label que nos
        '|        indica error.
        '| ELSE :
        '|      * Cambiamos el color del label que indica que el valor es correcto.

        ErrorProvider.Clear()

        If String.IsNullOrWhiteSpace(lblLabel.Text) Then
            ErrorProvider.SetError(lblLabel, "El campo no puede estar vacío.")
            lblLabel.BackColor = Color.MistyRose
        Else
            lblLabel.BackColor = Color.Azure
        End If

    End Sub

    ' Método que llama el módulo NavigateToForm
    Public Sub PrepareForNewClient()
        ' Limpiar cajas de texto
        TxtFirstName.Clear()
        TxtLastName.Clear()
        TxtPhone.Clear()
        ' ... etc ...

        ' Resetear el DTO para que no tenga basura del cliente anterior
        _clientPaymentDTO = New ClientPaymentDTO()

        ' Configurar botones
        BtnSaveCustomerData.Visible = True
        BtnUpdateCustomerData.Visible = False ' El de actualizar datos

        ' Configurar fechas por defecto
        DtpBirthdate.Format = DateTimePickerFormat.Custom
        DtpBirthdate.CustomFormat = " " ' Lo dejamos "vacío" para obligar a elegir
        DtpRegistrationDate.Value = DateTime.Today

        ' Cambiamos el título de la ventana para que el usuario sepa qué hace
        'Me.Text = "Registro de Nuevo Cliente"
    End Sub
    ''
    ''
    Private Function CreateClientPaymentDTO() As ClientPaymentDTO

        ' 1. Datos Básicos - Instanciamos tu clase DTO
        Dim data As New ClientPaymentDTO With
            {
            .FirstName = TxtFirstName.Text.Trim(),
            .LastName = TxtLastName.Text.Trim(),
            .BirthDate = DtpBirthdate.Value,
            .Age = CInt(Val(TxtCustomerAge.Text)),
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
    Public Sub PrepareToModifyClient(clientData As ClientPaymentDTO)
        ' 1. Guardamos el DTO para saber qué ID estamos editando
        _clientPaymentDTO = clientData

        ' 2. Llenamos los campos con la información recibida
        TxtFirstName.Text = clientData.FirstName
        TxtLastName.Text = clientData.LastName
        DtpBirthdate.Value = clientData.BirthDate
        TxtPhone.Text = clientData.Phone
        TxtEmail.Text = clientData.Email
        TxtAddress.Text = clientData.Address
        DtpRegistrationDate.Value = clientData.RegistrationDate

        ' 3. Estado (Activo/Inactivo)
        If clientData.State = "ACTIVO" Then
            RbActiveStatus.Checked = True
        Else
            RbInactiveState.Checked = True
        End If

        ' 4. Método de Pago
        Select Case clientData.PaymentMethod
            Case "MENSUAL" : RbMonthlyPayment.Checked = True

            Case "GRUPAL"
                RbGroupPayment.Checked = True
                TxtListGroupsDailyPayment.Text = clientData.GroupName

            Case "DIARIO"
                RbDailyPayment.Checked = True
                ' En caso de tarifa diaria, también usamos el buscador
                'TxtListGroupsDailyPayment.Text = clientData.PaymentMethodDetail ' O como se llame en tu DTO
        End Select

        ' 5. Ajustes Visuales
        BtnSaveCustomerData.Visible = False
        BtnUpdateCustomerData.Visible = True
        Me.Text = "Modificando Datos: " & clientData.FirstName & " " & clientData.LastName
    End Sub
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
        ' Si el nombre o el apellido no están vacíos, consideramos que hay cambios
        Return Not String.IsNullOrWhiteSpace(TxtFirstName.Text) OrElse
               Not String.IsNullOrWhiteSpace(TxtLastName.Text)
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
    Private Sub ShowSuccessMessage(idClient As Integer)
        ' 1. Preparamos el texto del cuerpo (Guardado vs Actualizado)
        Dim strActionText As String = If(BtnSaveCustomerData.Visible, "GUARDADOS", "ACTUALIZADOS")
        Dim strIdFormatted As String = $"CLI - {idClient:000}"

        ' 2. Construimos el "Ticket" de confirmación (usando los datos que ya tenemos en los campos)
        Dim strMsgBox As String = "DATOS DEL CLIENTE" & vbCrLf & vbCrLf &
                            "   NOMBRE   :  " & TxtFirstName.Text & " " & TxtLastName.Text & vbCrLf &
                            "   CÓDIGO   :  " & strIdFormatted & vbCrLf &
                            "   -----------------------------------------------" & vbCrLf &
                            "   Datos " & strActionText & " correctamente."

        MessageBox.Show(strMsgBox, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' 3. ¡LA CLAVE!: Notificamos al formulario de pagos que debe refrescarse
        ' Esto ejecutará el RefreshPaymentHistory que pasamos por AddressOf
        _onSuccessAction?.Invoke(idClient)

        ' 4. Cerramos
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
        '|      * Extraemos el nombre del botón BtnGuardar o BtnActualizar, utilizando Substring() y lo _
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
        '|      * Extraemos el nombre del botón BtnGuardar o BtnActualizar según sea el caso, utilizando Substring() y lo convertimos en _
        '|        _ minúsculas usando LCase, para mostrarlo en el título de mensaje.
        '|      * Mostramos el mensaje con los datos recibidos por parámetro, enviamos el enfoque al dateTimePicker que corresponda.
        '|      * Return True para salir de la función y no ejecutar el resto del código.
        '| ELSE : Si el TextBox tiene datos
        '|      * Return False para seguir ejecutando el resto del código.

        If String.IsNullOrEmpty(label.Text) Then
            clientData = UCase(clientData)
            titleMsgbox = LCase(titleMsgbox.Substring(1, titleMsgbox.Length - 1))
            MsgBox(" Verifica la información del cliente" & vbCr & vbCr &
                   " El campo " & clientData & " no puede estar vacío.", vbCritical, "Error al " & titleMsgbox)
            dateTimePicker.Focus()
            Return True
        Else
            Return False
        End If
    End Function
    Overloads Function FunMsGbox(titleMsgbox As String, rb1 As RadioButton, rb2 As RadioButton, rb3 As RadioButton) As Boolean

        '| -------------------------------------------------------------------------------------------------------------------
        '| IF : Comprobamos si los RadioButton no están seleccionados.
        '|      * Extraemos el nombre del botón BtnGuardar o BtnActualizar según sea el caso, utilizando Substring() y lo _
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
        '|      * Extraemos el nombre del botón BtnGuardar o BtnActualizar según sea el caso, utilizando Substring() y lo convertimos en _
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
    ''
    ''
End Class