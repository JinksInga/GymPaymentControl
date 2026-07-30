Imports System.Text.RegularExpressions
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

Public Class FrmFamilyGroup

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Componentes de Negocio y Reglas Fijas ---
    Private ReadOnly _familyGroupManager As New FamilyGroupManager()

    ' --- Control de Flujo y Modos de Pantalla ---
    Private _currentMode As TransactionMode?

    '
    Private _currentGroupId As Integer ' = 0

    ' --- Variables de Validación (Estado del Botón Guardar) ---
    Private _isLoadingData As Boolean ' = False
    Private _isGroupNameValid As Boolean ' = False
    Private _isNumberMembersValid As Boolean ' = False
    Private _isGridMembersCountValid As Boolean ' = False


#End Region

#Region " EVENTOS DEL FORMULARIO (Handlers) "


    Private Sub FrmFamilyGroups_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        UpdateGroupList()

        ConfigureStandbyMode()

    End Sub


    Private Sub BtnNewGroup_Click(sender As Object, e As EventArgs) Handles BtnNewGroup.Click

        Try
            _currentMode = TransactionMode.NewRecord

            ResetFamilyGroupForm()

            ConfigureNewMode()

            UpdateSaveButtonState()

        Catch ex As Exception
            MsgBox($"ERROR AL CREAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnSaveGroup_Click(sender As Object, e As EventArgs) Handles BtnSaveGroup.Click

        If Not IsGroupConfigurationValid() Then Exit Sub

        Try
            ' Extraemos los IDs de los integrantes.
            Dim listMemberIDs As New List(Of Integer)

            For Each row As DataGridViewRow In DgvListOfMembers.Rows

                If row.Cells("ListClientID").Value IsNot Nothing Then
                    listMemberIDs.Add(Convert.ToInt32(row.Cells("ListClientID").Value))
                End If

            Next

            Dim success As Boolean = _familyGroupManager.InsertFamilyGroup(TxtFamilyGroupName.Text,
                                                                           CInt(NudNumberMembers.Value),
                                                                           listMemberIDs)
            If success Then
                ' Sincronización externa: Pasamos el nombre al formulario de clientes de inmediato
                FrmNewModifyClient.TxtListGroupsDailyPayment.Text = TxtFamilyGroupName.Text

                UpdateGroupList()

                ConfigureStandbyMode()

                _currentMode = Nothing

                MessageBox.Show("El nuevo grupo familiar se ha registrado correctamente.", "Guardado Exitoso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As InvalidOperationException
            ' No hay tarifa asociada al número de integrantes
            If MessageBox.Show($"{ex.Message}{vbCrLf}¿Deseas registrar una tarifa de descuento para esta cantidad de personas ahora?",
                            "Tarifa No Encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                FrmPricesAndDiscounts.Show()
            End If

        Catch ex As Exception
            MsgBox($"ERROR AL GUARDAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnModifyGroup_Click(sender As Object, e As EventArgs) Handles BtnModifyGroup.Click

        Try
            _currentMode = TransactionMode.EditRecord

            UpdateGroupList()

            ConfigureEditMode()

            UpdateSaveButtonState()

        Catch ex As Exception
            MsgBox($"ERROR AL MODIFICAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnUpdateGroup_Click(sender As Object, e As EventArgs) Handles BtnUpdateGroup.Click

        If Not IsGroupConfigurationValid() Then Exit Sub

        Try
            ' Extraemos los IDs de los integrantes.
            Dim listMemberIDs As New List(Of Integer)

            For Each row As DataGridViewRow In DgvListOfMembers.Rows
                If row.Cells("ListClientID").Value IsNot Nothing Then
                    listMemberIDs.Add(Convert.ToInt32(row.Cells("ListClientID").Value))
                End If

            Next

            Dim success As Boolean = _familyGroupManager.UpdateFamilyGroup(0, TxtFamilyGroupName.Text,
                                                                           CInt(NudNumberMembers.Value),
                                                                           listMemberIDs)
            If success Then
                ' 3. Limpieza estricta y restablecimiento del formulario

                ' Pasamos el nombre del grupo al formulario externo
                FrmNewModifyClient.TxtListGroupsDailyPayment.Text = TxtFamilyGroupName.Text.Trim()

                UpdateSaveButtonState()

                ConfigureStandbyMode()

                _currentMode = Nothing

                MessageBox.Show("El grupo familiar se ha actualizado correctamente.", "Actualización Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As InvalidOperationException
            If MessageBox.Show($"{ex.Message}{vbCrLf}¿Deseas registrar una tarifa de descuento para esta cantidad de personas ahora?",
                            "Tarifa No Encontrada", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                FrmPricesAndDiscounts.Show()
            End If

        Catch ex As Exception
            MsgBox($"ERROR AL ACTUALIZAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click

        Try
            _currentMode = TransactionMode.DeleteRecord

            UpdateGroupList()

            ConfigureDeleteMode()

        Catch ex As Exception
            MsgBox($"ERROR AL ELIMINAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnDeleteGroup_Click(sender As Object, e As EventArgs) Handles BtnDeleteGroup.Click

        _currentMode = Nothing

        ConfigureStandbyMode()

    End Sub


    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        _currentMode = Nothing

        ResetFamilyGroupForm()

        ConfigureStandbyMode()

    End Sub


    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        Me.Close()

    End Sub


    Private Sub TxtFamilyGroupName_TextChanged(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.TextChanged

        If _currentMode Is Nothing OrElse _isLoadingData Then Exit Sub

        Select Case _currentMode

            Case TransactionMode.NewRecord
                ValidateAndRenderGroupDuplicates(TxtFamilyGroupName.Text)

            Case TransactionMode.EditRecord, TransactionMode.DeleteRecord
                FilterGroupsForSelection(TxtFamilyGroupName.Text)

        End Select

    End Sub
    Private Sub TxtFamilyGroupName_GotFocus(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.GotFocus

        If _isGroupNameValid Then TxtFamilyGroupName.BackColor = Color.Beige

    End Sub
    Private Sub TxtFamilyGroupName_LostFocus(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.LostFocus

        'TxtFamilyGroupName.Text = Regex.Replace(TxtFamilyGroupName.Text.Trim(), "\s+", " ")

        'TxtFamilyGroupName.BackColor = If(String.IsNullOrEmpty(TxtFamilyGroupName.Text),
        '                                                        Color.MistyRose, Color.Azure)
        ' Si el campo está vacío al salir, el mensaje es de campo obligatorio
        ' Si no está vacío pero _isGroupNameValid es False, mantenemos el mensaje de duplicado
        Dim errorMessage As String = ""

        If String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text) Then
            errorMessage = "El nombre del grupo es obligatorio."
        ElseIf Not _isGroupNameValid Then
            errorMessage = "El nombre de este grupo familiar ya existe."
        End If

        ' Aplicamos el estado visual centralizado desde FormHelpers
        FormHelpers.UpdateValidationState(TxtFamilyGroupName, _isGroupNameValid, errorMessage, ErrorProvider)

    End Sub
    Private Sub TxtFamilyGroupName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFamilyGroupName.KeyPress
        'AL PRESIONAR LA TECLA DE RETROCESO CAMBIAMOS DE COLOR EL TEXTBOX
        'If e.KeyChar = ControlChars.Back Then TxtListNomGrupo.BackColor = Color.Beige
    End Sub


    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged

        If _isLoadingData Then Exit Sub

        LblNumberMembers.Text = $"{DgvListOfMembers.RowCount} de {NudNumberMembers.Value}"

        '' REGLA 2: ¿Es mayor o igual a 3?
        '_isNumberMembersValid = (NudNumberMembers.Value >= 3)

        '' REGLA 4: ¿El conteo de la grilla es igual al valor seleccionado?
        '_isGridMembersCountValid = (DgvListOfMembers.RowCount = NudNumberMembers.Value)

        ' Notificamos al árbitro
        UpdateSaveButtonState()

    End Sub
    Private Sub NudNumberMembers_GotFocus(sender As Object, e As EventArgs) Handles NudNumberMembers.GotFocus
        NudNumberMembers.BackColor = Color.Beige
    End Sub
    Private Sub NudNumberMembers_LostFocus(sender As Object, e As EventArgs) Handles NudNumberMembers.LostFocus
        NudNumberMembers.BackColor = If(NudNumberMembers.Value < 3, Color.MistyRose, Color.Azure)
    End Sub


    Private Sub ChkEmptyGroup_CheckedChanged(sender As Object, e As EventArgs) Handles ChkEmptyGroup.CheckedChanged

        If _currentMode Is Nothing Then Exit Sub

        Dim isChecked As Boolean = ChkEmptyGroup.Checked

        GbMembersOfGroup.Enabled = Not isChecked

        ' Limpieza de seguridad: Si se marca como vacío, limpiamos los miembros que tuviera colgados
        If isChecked Then
            DgvListOfMembers.Rows.Clear()
            LblNumberMembers.Text = $"0 de {NudNumberMembers.Value}"
            TxtFamilyGroupName.Focus()
        Else
            'LblNumberMembers.Text = $"{DgvListOfMembers.RowCount} de {NudNumberMembers.Value}"
            TxtSearchMembers.Focus()
        End If

        ' Función que se encargará de los Enabled/Disabled
        UpdateSaveButtonState()

    End Sub


    Private Sub DgvListFamilyGroups_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListFamilyGroups.CellContentClick
    End Sub
    Private Sub DgvListFamilyGroups_DoubleClick(sender As Object, e As EventArgs) Handles DgvListFamilyGroups.DoubleClick

        'If _currentMode = TransactionMode.NewRecord Then Exit Sub
        If DgvListFamilyGroups.CurrentRow Is Nothing Then Exit Sub

        Try
            _isLoadingData = True

            _currentGroupId = CInt(DgvListFamilyGroups.CurrentRow.Cells("ColGroupId").Value)
            Dim groupName As String = DgvListFamilyGroups.CurrentRow.Cells("ColGroupName").Value.ToString()
            Dim numberMembers As Integer = CInt(DgvListFamilyGroups.CurrentRow.Cells("ColNumberMembers").Value)
            Dim dtMembers As DataTable = _familyGroupManager.GetMembersByGroupId(_currentGroupId)

            DgvListOfMembers.Rows.Clear()

            For Each row As DataRow In dtMembers.Rows

                Dim nRow As Integer = DgvListOfMembers.Rows.Add()
                Dim fullName As String = $"{row("nom_cli")} {row("ape_cli")}"

                DgvListOfMembers.Rows(nRow).Cells("ListClientID").Value = row("id_cli").ToString()
                DgvListOfMembers.Rows(nRow).Cells("ListFullName").Value = fullName
                DgvListOfMembers.Rows(nRow).Cells("ListGroupId").Value = row("id_grp").ToString()

            Next

            TxtFamilyGroupName.Text = groupName
            NudNumberMembers.Value = numberMembers
            NudNumberMembers.BackColor = Color.Azure

            LblNumberMembers.Text = $"{DgvListOfMembers.Rows.Count} de {NudNumberMembers.Value}"

            If _currentMode = TransactionMode.EditRecord Then

                GbNumberMembers.Enabled = True
                GbMembersOfGroup.Enabled = True
                TxtSearchMembers.Focus()

            ElseIf _currentMode = TransactionMode.DeleteRecord Then

                GbNumberMembers.Enabled = False
                GbMembersOfGroup.Enabled = False
                BtnDeleteGroup.Focus()

            End If

            DgvListFamilyGroups.Visible = False

        Catch ex As Exception
            MsgBox($"ERROR AL CARGAR DATOS :{vbCrLf}{ex.Message}")

        Finally
            _isLoadingData = False
            UpdateSaveButtonState()

        End Try

    End Sub


    Private Sub TxtSearchMembers_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchMembers.TextChanged
        ' Si el formulario está en reposo, ignoramos cambios accidentales
        If _currentMode Is Nothing Then Exit Sub

        ' Delegamos la búsqueda y el renderizado a su función dedicada
        SearchAndRenderMembersPredictive(TxtSearchMembers.Text.Trim())

    End Sub
    Private Sub TxtSearchMembers_GotFocus(sender As Object, e As EventArgs) Handles TxtSearchMembers.GotFocus
        TxtSearchMembers.BackColor = Color.Beige
    End Sub
    Private Sub TxtSearchMembers_LostFocus(sender As Object, e As EventArgs) Handles TxtSearchMembers.LostFocus
        TxtSearchMembers.BackColor = Color.Azure
    End Sub


    Private Sub DgvSearchMembers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSearchMembers.CellContentClick
    End Sub
    Private Sub DgvSearchMembers_DoubleClick(sender As Object, e As EventArgs) Handles DgvSearchMembers.DoubleClick

        If DgvSearchMembers.CurrentRow Is Nothing Then Exit Sub

        ErrorProvider.SetError(TxtSearchMembers, String.Empty)

        Dim clientId As String = DgvSearchMembers.CurrentRow.Cells("SearchClientId").Value.ToString()
        Dim fullName As String = DgvSearchMembers.CurrentRow.Cells("SearchFullName").Value.ToString()

        For Each row As DataGridViewRow In DgvListOfMembers.Rows

            If row.Cells("ListClientID").Value IsNot Nothing AndAlso row.Cells("ListClientID").Value.ToString() = clientId Then

                ErrorProvider.SetError(TxtSearchMembers, $"{fullName} ya se encuentra agregado en este grupo.")

                TxtSearchMembers.Focus()

                TxtSearchMembers.SelectAll()

                Exit Sub

            End If

        Next

        Dim groupId As String = If(DgvSearchMembers.CurrentRow.Cells("SearchGroupId").Value IsNot Nothing,
                                   DgvSearchMembers.CurrentRow.Cells("SearchGroupId").Value.ToString(),
                                   String.Empty)

        DgvListOfMembers.Rows.Add(clientId, fullName, groupId)

        LblNumberMembers.Text = $"{DgvListOfMembers.RowCount} de {NudNumberMembers.Value}"

        BeginInvoke(Sub()
                        DgvListOfMembers.CurrentCell = Nothing
                    End Sub)

        UpdateSaveButtonState()

        TxtSearchMembers.Clear()

        TxtSearchMembers.Focus()

    End Sub


    Private Sub DgvListOfMembers_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListOfMembers.CellContentClick
    End Sub
    Private Sub DgvListOfMembers_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DgvListOfMembers.RowsAdded
        DgvListOfMembers.ClearSelection()
    End Sub


    Private Sub BtnRemoveMember_Click(sender As Object, e As EventArgs) Handles BtnRemoveMember.Click

        If DgvListOfMembers.CurrentRow Is Nothing OrElse DgvListOfMembers.CurrentRow.IsNewRow Then
            MessageBox.Show("Selecciona un integrante de la lista para poder quitarlo.", "Lista de Integrantes",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim clientId As String = DgvListOfMembers.CurrentRow.Cells("ListClientID").Value.ToString()
        Dim fullName As String = DgvListOfMembers.CurrentRow.Cells("ListFullName").Value.ToString()

        Dim messageBody As String = $"Grupo: {TxtFamilyGroupName.Text}{vbCrLf}" &
                            $"Integrante: {fullName}{vbCrLf}" &
                            $"__________________________________________{vbCrLf}{vbCrLf}" &
                            $"¿Seguro que quieres quitar a este integrante de la lista?"

        Dim msgBoxResponse As DialogResult = MessageBox.Show(messageBody, "Quitar Integrante",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question,
                                                    MessageBoxDefaultButton.Button2)

        If msgBoxResponse = DialogResult.Yes Then

            DgvListOfMembers.Rows.Remove(DgvListOfMembers.CurrentRow)

            DgvListOfMembers.CurrentCell = Nothing

            LblNumberMembers.Text = $"{DgvListOfMembers.RowCount} de {NudNumberMembers.Value}"

            TxtSearchMembers.Focus()

            UpdateSaveButtonState()

            BtnRemoveMember.Enabled = (DgvListOfMembers.RowCount > 0)

        End If

    End Sub

#End Region


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " ??. METODOS DE VALIDACION Y REGLAS DE NEGOCIO "

    ''' <summary>
    ''' Evalúa el estado actual de los controles del formulario para determinar si la configuración 
    ''' del grupo familiar cumple con todas las reglas de negocio requeridas.
    ''' </summary>
    ''' <returns>
    ''' True si el nombre del grupo es válido y la cantidad de integrantes coincide con lo estipulado;
    ''' de lo contrario nos devuel False.
    ''' </returns>
    ''' <remarks>
    ''' Esta función centraliza las validaciones de la interfaz de usuario, evitando código repetido 
    ''' en los eventos de cambio de texto, foco o interacción de grillas.
    ''' </remarks>
    Private Function IsGroupConfigurationValid() As Boolean
        ' 1. VALIDACIÓN BÁSICA DEL NOMBRE
        Dim isNameValid As Boolean = Not String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text)

        Dim totalStipulated As Integer = Convert.ToInt32(NudNumberMembers.Value)
        Dim currentRegistered As Integer = DgvListOfMembers.RowCount

        Dim isMembersSectionValid As Boolean = False

        ' 2. EVALUACIÓN DE REGLAS MATEMÁTICAS SEGÚN EL CHECKBOX
        If ChkEmptyGroup.Checked Then
            ' Si el grupo se guarda vacío, ignoramos la grilla, solo exigimos el mínimo del NUD
            If totalStipulated >= 3 Then
                isMembersSectionValid = True
            End If
        Else
            ' Si NO está checkeado, la cantidad en la grilla DEBE ser exactamente igual al NUD
            If totalStipulated >= 3 AndAlso currentRegistered = totalStipulated Then
                isMembersSectionValid = True
            End If
        End If

        ' 3. CONTROL VISUAL DEL SEMÁFORO (Colores e Iconos de Estado)
        If isMembersSectionValid Then
            LblNumberMembers.BackColor = Color.Azure
            ErrorProvider.SetError(LblNumberMembers, String.Empty) 'PicIntgrntes.Image = My.Resources.ic_okay_28x28
        Else
            LblNumberMembers.BackColor = Color.MistyRose
            ErrorProvider.SetError(LblNumberMembers, "La cantidad de integrantes no coincide con la lista de integrantes") 'PicIntgrntes.Image = My.Resources.ic_cancel_c_28x28
        End If

        ' El formulario general es válido SI el nombre es correcto Y las reglas de miembros se cumplen
        Return isNameValid AndAlso isMembersSectionValid

    End Function


    ''' <summary>
    ''' Actualiza en tiempo real el estado de habilitación de los botones de guardado (BtnSaveGroup) 
    ''' y actualización (BtnUpdateGroup) en función del modo de transacción activo y las reglas de negocio.
    ''' </summary>
    ''' <remarks>
    ''' Actúa como el árbitro visual del formulario. Se debe invocar cada vez que el usuario altere 
    ''' el nombre del grupo, el NumericUpDown o modifique los integrantes de la grilla definitiva.
    ''' </remarks>
    Private Sub UpdateSaveButtonState()
        ' 1. Le pedimos al árbitro matemático que evalúe el formulario
        Dim isValid As Boolean = IsGroupConfigurationValid()

        ' 2. Activamos el botón correspondiente según el modo activo
        If _currentMode = TransactionMode.NewRecord Then
            BtnSaveGroup.Enabled = isValid

        ElseIf _currentMode = TransactionMode.EditRecord Then
            BtnUpdateGroup.Enabled = isValid

        Else
            ' Si está en reposo (Nothing), ambos se apagan por seguridad
            BtnSaveGroup.Enabled = False
            BtnUpdateGroup.Enabled = False
        End If
    End Sub

#End Region


#Region " ??. LOGICA DE CARGA Y RENDERIZADO DE DATOS (Backend Bridge) "

    Private Sub ValidateAndRenderGroupDuplicates(groupName As String)

        If String.IsNullOrWhiteSpace(groupName) Then

            'ErrorProvider.SetError(TxtFamilyGroupName, "NO PUEDE QUEDAR VACIO")
            'TxtFamilyGroupName.BackColor = Color.MistyRose
            _isGroupNameValid = False
            FormHelpers.UpdateValidationState(TxtFamilyGroupName, False, "El nombre del grupo es obligatorio.", ErrorProvider)
            UpdateSaveButtonState()
            Exit Sub

        End If

        Try
            Dim exactMatch As Boolean = False

            Dim dtCoincidencias As DataTable = _familyGroupManager.GetGroupsByNameMatch(groupName)

            If dtCoincidencias IsNot Nothing AndAlso dtCoincidencias.Rows.Count > 0 Then

                exactMatch = dtCoincidencias.AsEnumerable().Any(
                    Function(row) row.Field(Of String)("nom_grp").Equals(groupName, StringComparison.OrdinalIgnoreCase))

            End If


            If exactMatch Then

                Dim textErrorProvider As String = $"NOMBRE DUPLICADO : {groupName}" & Environment.NewLine &
                                                  "El nombre de este grupo familiar ya existe." & Environment.NewLine &
                                                  "Elija otro nombre para continuar."
                'TxtFamilyGroupName.BackColor = Color.MistyRose
                'ErrorProvider.SetError(TxtFamilyGroupName, textErrorProvider)
                _isGroupNameValid = False
                FormHelpers.UpdateValidationState(TxtFamilyGroupName, False, textErrorProvider, ErrorProvider)

            Else

                'TxtFamilyGroupName.BackColor = Color.Beige
                'ErrorProvider.SetError(TxtFamilyGroupName, String.Empty)
                _isGroupNameValid = True
                FormHelpers.UpdateValidationState(TxtFamilyGroupName, True, String.Empty, ErrorProvider)

            End If

        Catch ex As Exception
            _isGroupNameValid = False
            MsgBox($"ERROR DE VALIDACIÓN :{vbCrLf}{ex.Message}")
        End Try

        UpdateSaveButtonState()

    End Sub


    ''' <summary>
    ''' Filtra dinámicamente la grilla de grupos en modos Modificar y Eliminar.
    ''' </summary>
    Private Sub FilterGroupsForSelection(groupName As String)

        Try
            ' Si el campo está vacío, mostramos todos los grupos o la lista completa sin filtrar
            Dim dtCoincidencias As DataTable = _familyGroupManager.GetGroupsByNameMatch(groupName)

            If dtCoincidencias IsNot Nothing AndAlso dtCoincidencias.Rows.Count > 0 Then

                DgvListFamilyGroups.AutoGenerateColumns = False
                DgvListFamilyGroups.DataSource = dtCoincidencias
                DgvListFamilyGroups.Visible = True
                DgvListFamilyGroups.BringToFront()

            Else
                ' Si no hay coincidencia alguna con lo escrito, ocultamos o vaciamos la grilla
                DgvListFamilyGroups.DataSource = Nothing
                DgvListFamilyGroups.Visible = False

            End If

            TxtFamilyGroupName.BackColor = Color.Azure

        Catch ex As Exception
            MsgBox($"ERROR AL BUSCAR GRUPOS :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub SearchAndRenderMembersPredictive(searchText As String)

        ErrorProvider.SetError(DgvSearchMembers, String.Empty)

        ' 1. Si el buscador está vacío, limpiamos y ocultamos todo
        If String.IsNullOrWhiteSpace(searchText) Then
            DgvSearchMembers.DataSource = Nothing
            DgvSearchMembers.Visible = False
            BtnRemoveMember.Enabled = (DgvListOfMembers.RowCount > 0)
            Exit Sub
        End If

        Try
            ' Bloqueamos el botón de quitar mientras se realiza una nueva búsqueda
            BtnRemoveMember.Enabled = False

            ' 2. Consultamos al Manager de forma blindada contra inyección SQL y comillas
            Dim dtClientes As DataTable = _familyGroupManager.SearchAvailableMembersByName(searchText)

            If dtClientes.Rows.Count > 0 Then
                ' 3. Apagamos la autogeneración para respetar tus columnas del diseñador
                DgvSearchMembers.AutoGenerateColumns = False
                DgvSearchMembers.DataSource = dtClientes

                ' Traemos al frente estéticamente la lista predictiva
                DgvSearchMembers.Visible = True
                DgvSearchMembers.BringToFront()

                ' Restauramos el color original por si venía de un error previo
                TxtSearchMembers.BackColor = Color.Beige
            Else
                ' Si no hay filas coincidentes, ocultamos la grilla predictiva
                DgvSearchMembers.DataSource = Nothing
                DgvSearchMembers.Visible = False
            End If

        Catch ex As Exception
            ' Si ocurre un error (como el de la comilla en el código viejo), se captura aquí de forma segura
            TxtSearchMembers.BackColor = Color.MistyRose
            DgvSearchMembers.DataSource = Nothing
            DgvSearchMembers.Visible = False
        End Try

    End Sub



#End Region


#Region " ??. METODOS DE LIMPIEZA Y CONTROL VISUAL (UI) "

    ''' <summary>
    ''' Restablece los controles del formulario a su estado inicial.
    ''' La configuración aplicada depende del modo de transacción actual
    ''' (nuevo registro o estado neutro).
    ''' </summary>
    Private Sub ResetFamilyGroupForm()

        If _currentMode = TransactionMode.NewRecord Then
            NudNumberMembers.Value = 3

        Else
            NudNumberMembers.Value = 0
            LblNumberMembers.Text = String.Empty
            ErrorProvider.SetError(LblNumberMembers, String.Empty)

        End If

        TxtFamilyGroupName.Clear()
        TxtSearchMembers.Clear()
        DgvListOfMembers.Rows.Clear()
        ChkEmptyGroup.Checked = False

    End Sub

    '' OTRA ALTERNATIVA AL MÉTODO SetInputControlsBackColor ES CREAR UNA FUNCIÓN QUE
    '' RECIBA UNA COLECCIÓN DE CONTROLES Ejemplo: SetBackColor
    'Private Sub SetBackColor(backColor As Color, ParamArray controls() As Control)
    '    For Each ctrl As Control In controls
    '        ctrl.BackColor = backColor
    '    Next
    'End Sub
    '' Y SE USARÍA DE LA SIGUIENTE MANERA
    'SetBackColor(Color.Azure, TxtFamilyGroupName, NudNumberMembers, LblNumberMembers, TxtSearchMembers)
    Sub SetInputControlsBackColor(backColor As Color)

        TxtFamilyGroupName.BackColor = backColor
        NudNumberMembers.BackColor = backColor
        LblNumberMembers.BackColor = backColor
        TxtSearchMembers.BackColor = backColor

    End Sub

    ''' <summary>
    ''' Restablece la interfaz al estado de reposo, ocultando los controles
    ''' de transacción y habilitando únicamente las acciones disponibles
    ''' según existan registros almacenados.
    ''' </summary>
    Private Sub ConfigureStandbyMode()

        Dim hasRecords As Boolean = (DgvListFamilyGroups.RowCount > 0)

        BtnNewGroup.Visible = True
        BtnModifyGroup.Visible = True
        BtnDelete.Visible = True

        BtnSaveGroup.Visible = False
        BtnUpdateGroup.Visible = False
        BtnDeleteGroup.Visible = False
        BtnCancel.Visible = False

        DgvListFamilyGroups.Visible = False

        BtnModifyGroup.Enabled = hasRecords
        BtnDelete.Enabled = hasRecords

        TxtFamilyGroupName.Enabled = False
        GbNumberMembers.Enabled = False
        GbMembersOfGroup.Enabled = False
        ChkEmptyGroup.Enabled = True

        BtnNewGroup.Focus()

        SetInputControlsBackColor(Color.Azure) 'Color.FromName("Control") 'SystemColors.Control y/o Window

    End Sub


    ''' <summary>
    ''' Configura los botones principales al iniciar una transacción,
    ''' ocultando las acciones generales y mostrando el botón Cancelar.
    ''' </summary>
    Private Sub ConfigureActionButtons()
        BtnNewGroup.Visible = False
        BtnModifyGroup.Visible = False
        BtnDelete.Visible = False
        BtnCancel.Visible = True
    End Sub


    ''' <summary>
    ''' Configura la interfaz para la creación de un nuevo grupo familiar,
    ''' habilitando los controles necesarios para introducir sus datos.
    ''' </summary>
    Private Sub ConfigureNewMode()

        ConfigureActionButtons()
        BtnSaveGroup.Visible = True

        TxtFamilyGroupName.Enabled = True
        GbNumberMembers.Enabled = True
        GbMembersOfGroup.Enabled = Not ChkEmptyGroup.Checked

        TxtFamilyGroupName.Focus()

    End Sub


    ''' <summary>
    ''' Muestra el listado de grupos familiares disponibles y la sitúa en primer plano
    ''' para permitir la selección del registro sobre el que se realizará una operación
    ''' </summary>
    Private Sub ShowFamilyGroupList()

        DgvListFamilyGroups.Visible = True
        DgvListFamilyGroups.BringToFront()

    End Sub


    ''' <summary>
    ''' Prepara la interfaz para la selección de un grupo familiar,
    ''' habilitando los controles necesarios y mostrando la lista de grupos.
    ''' </summary>
    Private Sub EnableGroupSelection()

        TxtFamilyGroupName.Enabled = True
        ShowFamilyGroupList()
        TxtFamilyGroupName.Focus()

    End Sub


    ''' <summary>
    ''' Configura la interfaz para la modificación de un grupo familiar,
    ''' habilitando la selección del registro y la edición de sus datos.
    ''' </summary>
    Private Sub ConfigureEditMode()

        ConfigureActionButtons()
        BtnUpdateGroup.Visible = True
        ChkEmptyGroup.Enabled = False
        EnableGroupSelection()

    End Sub


    ''' <summary>
    ''' Configura la interfaz para la eliminación de un grupo familiar,
    ''' permitiendo seleccionar el registro que se desea eliminar.
    ''' </summary>
    Private Sub ConfigureDeleteMode()

        ConfigureActionButtons()
        BtnDeleteGroup.Visible = True
        EnableGroupSelection()

    End Sub


#End Region


#Region " ?? ESTRUCTURAS Y ENUMS AUXILIARES "
    ' Tipos de datos personalizados que definen los estados y reglas del formulario.

    Public Enum TransactionMode
        NewRecord
        EditRecord
        DeleteRecord
    End Enum

#End Region


    ''' <summary>
    ''' Consulta al backend la lista completa de grupos familiares y los renderiza en la grilla izquierda.
    ''' </summary>
    Private Sub UpdateGroupList()
        Try
            ' 1. Traemos todos los grupos de la base de datos pasándole una cadena vacía o usando tu método general
            Dim dtGrupos As DataTable = _familyGroupManager.GetGroupsByNameMatch(String.Empty)

            ' 2. Cargamos la grilla de navegación izquierda
            DgvListFamilyGroups.AutoGenerateColumns = False
            DgvListFamilyGroups.DataSource = dtGrupos

        Catch ex As Exception
            MsgBox($"ERROR AL CARGAR :{vbCrLf}{ex.Message}")
        End Try

    End Sub







    'Sub ChangeColorsCancelDelete()
    '    'CAMBIA EL COLOR DE FONDO
    '    NudNumIntgrntes.BackColor = Color.FromName("Control")
    '    LblNumIntgrntes.BackColor = Color.FromName("Control")
    '    TxtBscrIntgrntes.BackColor = Color.FromName("Control")
    '    'CAMBIA EL COLOR DE LA LETRA
    '    NudNumIntgrntes.ForeColor = Color.FromName("Control")
    '    LblNumIntgrntes.ForeColor = Color.FromName("Control")
    '    'QUITA LA IMAGEN
    '    PicIntgrntes.Image = Nothing
    'End Sub

End Class