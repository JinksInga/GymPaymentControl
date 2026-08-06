Imports GymPaymentControl.Enums
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers

Public Class FrmFamilyGroup

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Componentes de Negocio y Reglas Fijas ---
    Private ReadOnly _familyGroupManager As New FamilyGroupManager()

    ' --- Control de Flujo y Modos de Pantalla ---
    Private _currentMode As TransactionMode?

    '
    Private _currentGroupId As Integer

    ' --- Variables de Validación (Estado del Botón Guardar) ---
    Private _isLoadingData As Boolean
    Private _isGroupNameValid As Boolean
    Private _isGroupSelected As Boolean

    '
    Public Property NewGroupName As String


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

            _isGroupNameValid = False

            ConfigureNewMode()

            UpdateSaveButtonState()

        Catch ex As Exception
            MsgBox($"ERROR AL CREAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnSaveGroup_Click(sender As Object, e As EventArgs) Handles BtnSaveGroup.Click

        If Not IsGroupConfigurationValid() Then Exit Sub

        Try
            Dim statusToSave As EntityStatus = If(RbActiveState.Checked, EntityStatus.Active, EntityStatus.Inactive)

            Dim success As Boolean = _familyGroupManager.InsertFamilyGroup(TxtFamilyGroupName.Text, CInt(NudNumberMembers.Value),
                                                                           GetRegisteredMemberIds(), statusToSave)
            If success Then

                _currentMode = Nothing

                NewGroupName = TxtFamilyGroupName.Text

                UpdateGroupList()

                ConfigureStandbyMode()

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

            ResetFamilyGroupForm()

            ConfigureEditMode()

            UpdateSaveButtonState()

            FormHelpers.UpdateValidationState(LblNumberMembers, True, String.Empty, ErrorProvider)

        Catch ex As Exception
            MsgBox($"ERROR AL MODIFICAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnUpdateGroup_Click(sender As Object, e As EventArgs) Handles BtnUpdateGroup.Click

        If Not IsGroupConfigurationValid() Then Exit Sub

        Try
            Dim statusToSave As EntityStatus = If(RbActiveState.Checked, EntityStatus.Active, EntityStatus.Inactive)

            Dim success As Boolean = _familyGroupManager.UpdateFamilyGroup(_currentGroupId, TxtFamilyGroupName.Text,
                                                                           CInt(NudNumberMembers.Value),
                                                                           GetRegisteredMemberIds(), statusToSave)
            If success Then

                _currentMode = Nothing

                ConfigureStandbyMode()

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

            ResetFamilyGroupForm()

            ConfigureDeleteMode()

            UpdateSaveButtonState()

        Catch ex As Exception
            MsgBox($"ERROR AL ELIMINAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnDeleteGroup_Click(sender As Object, e As EventArgs) Handles BtnDeleteGroup.Click

        If _currentGroupId <= 0 Then Exit Sub

        Dim msg As String = $"¿Qué acción deseas realizar con el grupo '{TxtFamilyGroupName.Text.Trim()}'?{vbCrLf}{vbCrLf}" &
                             $"• [SÍ] - PASAR A INACTIVO (Pausa el grupo y a TODOS sus miembros por vacaciones).{vbCrLf}" &
                             $"• [NO] - ELIMINAR DEFINITIVAMENTE (Disuelve el grupo y pasa sus integrantes a Mensual Activo).{vbCrLf}" &
                             $"• [CANCELAR] - Salir sin realizar cambios."

        Dim result As DialogResult = MessageBox.Show(msg, "Gestión del Estado del Grupo",
                                                     MessageBoxButtons.YesNoCancel,
                                                     MessageBoxIcon.Question,
                                                     MessageBoxDefaultButton.Button1)

        Try
            Select Case result

                'Case DialogResult.Yes ' PASAR A INACTIVO (Grupo + Clientes)

                    'If _familyGroupManager.SetGroupStatus(_currentGroupId, "INACTIVO") Then
                    'MessageBox.Show("El grupo y todos sus integrantes han pasado a estado INACTIVO. El generador de deudas los ignorará automáticamente.",
                    '                    "Baja Temporal Registrada", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    ConfigureStandbyMode()
                    'End If

                Case DialogResult.Yes ' BORRADO DEFINITIVO (Disolver grupo)

                    If _familyGroupManager.DeleteFamilyGroup(_currentGroupId) Then
                        MessageBox.Show("El grupo ha sido eliminado y sus miembros pasaron a modalidad Mensual.",
                                        "Eliminación Completa", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ConfigureStandbyMode()
                    End If

                Case DialogResult.Cancel
                    Exit Sub

            End Select

        Catch ex As Exception
            MsgBox($"ERROR AL ELIMINAR EL GRUPO O CAMBIO DE ESTADO : {vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        _currentMode = Nothing

        ResetFamilyGroupForm()

        ConfigureStandbyMode()

        ErrorProvider.Clear()

    End Sub


    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles BtnCloseWindow.Click
        Me.Close()
    End Sub


    Private Sub TxtFamilyGroupName_TextChanged(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.TextChanged

        If _currentMode Is Nothing OrElse _isLoadingData Then Exit Sub

        Select Case _currentMode

            Case TransactionMode.NewRecord

                ValidateAndRenderGroupDuplicates(TxtFamilyGroupName.Text)

            Case TransactionMode.EditRecord

                If Not _isGroupSelected Then
                    RefreshGroupSearch(TxtFamilyGroupName.Text)
                Else
                    ValidateAndRenderGroupDuplicates(TxtFamilyGroupName.Text)
                End If

            Case TransactionMode.DeleteRecord
                'If Not _isGroupSelected Then RefreshGroupSearch(TxtFamilyGroupName.Text)
                RefreshGroupSearch(TxtFamilyGroupName.Text)

        End Select

    End Sub
    Private Sub TxtFamilyGroupName_GotFocus(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.GotFocus

        TxtFamilyGroupName.BackColor = Color.Beige

    End Sub
    Private Sub TxtFamilyGroupName_LostFocus(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.LostFocus

        If _currentMode Is Nothing Then Exit Sub

        Dim errorMessage As String = String.Empty

        If _currentMode = TransactionMode.NewRecord Then

            If String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text) Then

                errorMessage = "El nombre del grupo es obligatorio."
            ElseIf Not _isGroupNameValid Then
                errorMessage = "El nombre de este grupo familiar ya existe."

            End If

        Else

            If String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text) Then
                errorMessage = "Debe ingresar o seleccionar un grupo válido."
            End If

        End If

        FormHelpers.UpdateValidationState(TxtFamilyGroupName, _isGroupNameValid, errorMessage, ErrorProvider)

        UpdateSaveButtonState()

        If _currentMode = TransactionMode.EditRecord Then
            FormHelpers.UpdateValidationState(LblNumberMembers, True, String.Empty, ErrorProvider)
        End If

    End Sub
    Private Sub TxtFamilyGroupName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFamilyGroupName.KeyPress
        'AL PRESIONAR LA TECLA DE RETROCESO CAMBIAMOS DE COLOR EL TEXTBOX
        'If e.KeyChar = ControlChars.Back Then TxtListNomGrupo.BackColor = Color.Beige
    End Sub


    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged

        If _isLoadingData Then Exit Sub

        LblNumberMembers.Text = $"{DgvListOfMembers.RowCount} de {NudNumberMembers.Value}"

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

        If isChecked Then

            DgvListOfMembers.Rows.Clear()
            LblNumberMembers.Text = $"0 de {NudNumberMembers.Value}"
            TxtFamilyGroupName.Focus()
        Else
            TxtSearchMembers.Focus()

        End If

        UpdateSaveButtonState()

    End Sub


    Private Sub DgvListFamilyGroups_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListFamilyGroups.CellContentClick
    End Sub
    Private Sub DgvListFamilyGroups_DoubleClick(sender As Object, e As EventArgs) Handles DgvListFamilyGroups.DoubleClick

        If DgvListFamilyGroups.CurrentRow Is Nothing Then Exit Sub

        Try
            _isLoadingData = True

            '| Extraemos la fila subyacente del DataTable mediante DataRowView
            Dim rowView As DataRowView = CType(DgvListFamilyGroups.CurrentRow.DataBoundItem, DataRowView)

            _currentGroupId = CInt(rowView("id_grp"))
            Dim groupName As String = rowView("nom_grp").ToString()
            Dim numberMembers As Integer = CInt(rowView("num_intgrntes_grp"))
            Dim groupStatus As EntityStatus = CType(Convert.ToByte(rowView("std_grp")), EntityStatus)

            Dim dtMembers As DataTable = _familyGroupManager.GetMembersByGroupId(_currentGroupId)

            DgvListOfMembers.Rows.Clear()

            For Each row As DataRow In dtMembers.Rows

                Dim fullName As String = $"{row("nom_cli")} {row("ape_cli")}"
                Dim nRow As Integer = DgvListOfMembers.Rows.Add(fullName)
                DgvListOfMembers.Rows(nRow).Tag = CInt(row("id_cli"))

            Next

            DgvListOfMembers.CurrentCell = Nothing

            TxtFamilyGroupName.Text = groupName
            NudNumberMembers.Value = numberMembers
            LblNumberMembers.Text = $"{DgvListOfMembers.Rows.Count} de {NudNumberMembers.Value}"
            RbActiveState.Checked = (groupStatus = EntityStatus.Active)
            RbInactiveState.Checked = Not RbActiveState.Checked '(groupStatus = EntityStatus.Inactive)

            FormHelpers.UpdateValidationState(TxtFamilyGroupName, True, String.Empty, ErrorProvider)

            If _currentMode = TransactionMode.EditRecord Then

                EnableGroupInformationControls()
                GbMembersOfGroup.Enabled = True
                TxtSearchMembers.Focus()

            ElseIf _currentMode = TransactionMode.DeleteRecord Then

                TxtFamilyGroupName.Focus()

            End If

            DgvListFamilyGroups.Visible = False

            _isGroupNameValid = True

            UpdateSaveButtonState()

            _isGroupSelected = True
            _isLoadingData = False

        Catch ex As Exception
            MsgBox($"ERROR AL CARGAR DATOS :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub TxtSearchMembers_TextChanged(sender As Object, e As EventArgs) Handles TxtSearchMembers.TextChanged

        If _currentMode Is Nothing Then Exit Sub

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

        '| Leemos los datos directamente desde el DataRowView subyacente
        Dim rowView As DataRowView = CType(DgvSearchMembers.CurrentRow.DataBoundItem, DataRowView)
        Dim clientId As Integer = CInt(rowView("id_cli"))
        Dim fullName As String = rowView("full_name").ToString()

        '| Validar si el cliente ya está agregado (revisando el .Tag de las filas)
        For Each row As DataGridViewRow In DgvListOfMembers.Rows

            If row.Tag IsNot Nothing AndAlso CInt(row.Tag) = clientId Then

                ErrorProvider.SetError(TxtSearchMembers, $"{fullName} ya se encuentra agregado en este grupo.")
                TxtSearchMembers.Focus()
                TxtSearchMembers.SelectAll()

                Exit Sub
            End If
        Next

        Dim rowIndex As Integer = DgvListOfMembers.Rows.Add(fullName)

        DgvListOfMembers.Rows(rowIndex).Tag = clientId
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
            MessageBox.Show("Selecciona un integrante de la lista para poder quitarlo.", "Lista de Integrantes", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim fullName As String = DgvListOfMembers.CurrentRow.Cells("ListFullName").Value?.ToString()

        Dim messageBody As String = $"Grupo: {TxtFamilyGroupName.Text}{vbCrLf}" &
                                $"Integrante: {fullName}{vbCrLf}" &
                                $"__________________________________________{vbCrLf}{vbCrLf}" &
                                $"¿Seguro que quieres quitar a este integrante de la lista?"

        Dim msgBoxResponse As DialogResult = MessageBox.Show(messageBody, "Quitar Integrante",
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Question,
                                                         MessageBoxDefaultButton.Button2)

        If msgBoxResponse = DialogResult.Yes Then
            ' 1. Al remover la fila, el .Tag asignado a esta se desecha automáticamente
            DgvListOfMembers.Rows.Remove(DgvListOfMembers.CurrentRow)
            DgvListOfMembers.CurrentCell = Nothing

            ' 2. Actualizamos la etiqueta con la nueva cantidad de filas
            LblNumberMembers.Text = $"{DgvListOfMembers.Rows.Count} de {NudNumberMembers.Value}"

            ' 3. Refrescamos UI y botones
            TxtSearchMembers.Focus()
            UpdateSaveButtonState()
            BtnRemoveMember.Enabled = (DgvListOfMembers.Rows.Count > 0)
        End If

    End Sub

#End Region


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " ??. METODOS DE VALIDACION Y REGLAS DE NEGOCIO "

    ''' <summary>
    ''' Comprueba que la configuración de integrantes del grupo cumple
    ''' las reglas de negocio según el modo de transacción actual.
    ''' </summary>
    ''' <returns>
    ''' <c>True</c> si la configuración de integrantes es válida; en caso
    ''' contrario, <c>False</c>.
    ''' </returns>
    Private Function IsMembersConfigurationValid() As Boolean

        Dim totalStipulated As Integer = CInt(NudNumberMembers.Value)
        Dim currentRegistered As Integer = DgvListOfMembers.RowCount

        If _currentMode = TransactionMode.DeleteRecord Then Return True

        If ChkEmptyGroup.Checked Then Return totalStipulated >= 3

        Return totalStipulated >= 3 AndAlso currentRegistered = totalStipulated

    End Function


    ''' <summary>
    ''' Determina si la configuración actual del grupo familiar cumple
    ''' las reglas de negocio requeridas para la transacción en curso.
    ''' </summary>
    ''' <returns>
    ''' <c>True</c> si el nombre del grupo y la configuración de integrantes
    ''' son válidos; en caso contrario, <c>False</c>.
    ''' </returns>
    ''' <remarks>
    ''' Centraliza la validación global del formulario y sincroniza el
    ''' estado visual de la sección de integrantes según el resultado
    ''' obtenido.
    ''' </remarks>
    Private Function IsGroupConfigurationValid() As Boolean

        Dim isNameValid As Boolean = Not String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text)

        Dim isMembersSectionValid As Boolean = IsMembersConfigurationValid()

        Dim errorMessage As String = If(isMembersSectionValid, String.Empty,
            "La cantidad de integrantes no coincide con la lista de integrantes.")

        FormHelpers.UpdateValidationState(LblNumberMembers, isMembersSectionValid, errorMessage, ErrorProvider)

        Dim isConfigurationValid As Boolean = isNameValid AndAlso isMembersSectionValid

        If _currentMode = TransactionMode.NewRecord Then
            isConfigurationValid = isConfigurationValid AndAlso _isGroupNameValid
        End If

        Return isConfigurationValid

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

        Dim isValid As Boolean = IsGroupConfigurationValid()

        Select Case _currentMode

            Case TransactionMode.NewRecord : BtnSaveGroup.Enabled = isValid

            Case TransactionMode.EditRecord : BtnUpdateGroup.Enabled = isValid

            Case TransactionMode.DeleteRecord : BtnDeleteGroup.Enabled = isValid

        End Select

    End Sub

#End Region


#Region " ??. LOGICA DE CARGA Y RENDERIZADO DE DATOS (Backend Bridge) "


    Private Function GetRegisteredMemberIds() As List(Of Integer)

        Dim memberIds As New List(Of Integer)

        For Each row As DataGridViewRow In DgvListOfMembers.Rows
            If row.Tag IsNot Nothing Then memberIds.Add(CInt(row.Tag))
        Next

        Return memberIds

    End Function


    ''' <summary>
    ''' Recarga la grilla de grupos familiares con los registros
    ''' disponibles en el sistema.
    ''' </summary>
    Private Sub UpdateGroupList()

        Try
            Dim dtGrupos As DataTable = _familyGroupManager.GetGroupsByNameMatch(String.Empty)

            DgvListFamilyGroups.AutoGenerateColumns = False
            DgvListFamilyGroups.DataSource = dtGrupos
            DgvListFamilyGroups.CurrentCell = Nothing

        Catch ex As Exception
            MsgBox($"ERROR AL CARGAR :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub ValidateAndRenderGroupDuplicates(groupName As String)

        If String.IsNullOrWhiteSpace(groupName) Then

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

                _isGroupNameValid = False
                FormHelpers.UpdateValidationState(TxtFamilyGroupName, False, textErrorProvider, ErrorProvider)

            Else

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
    ''' Busca grupos familiares según el nombre indicado y actualiza
    ''' la lista de resultados junto con el estado de validación de la interfaz.
    ''' </summary>
    ''' <param name="groupName">
    ''' Nombre del grupo utilizado como criterio de búsqueda.
    ''' </param>
    Private Sub RefreshGroupSearch(groupName As String)

        Try
            Dim dtCoincidences As DataTable = _familyGroupManager.GetGroupsByNameMatch(groupName)

            Dim isValid As Boolean
            Dim warningMessage As String

            Dim hasResults = dtCoincidences IsNot Nothing AndAlso dtCoincidences.Rows.Count > 0

            If hasResults Then

                DgvListFamilyGroups.AutoGenerateColumns = False
                DgvListFamilyGroups.DataSource = dtCoincidences
                DgvListFamilyGroups.Visible = True
                DgvListFamilyGroups.BringToFront()
                DgvListFamilyGroups.CurrentCell = Nothing
                isValid = Not String.IsNullOrWhiteSpace(groupName)
                warningMessage = If(isValid, String.Empty, "Debe ingresar o seleccionar un nombre de grupo.")

            Else

                DgvListFamilyGroups.DataSource = Nothing
                isValid = False
                warningMessage = "El nombre del grupo ingresado no existe."

            End If

            _isGroupNameValid = isValid

            FormHelpers.UpdateValidationState(TxtFamilyGroupName, isValid, warningMessage, ErrorProvider)

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
            BtnRemoveMember.Enabled = False

            Dim dtClientes As DataTable = _familyGroupManager.SearchAvailableMembersByName(searchText)

            If dtClientes.Rows.Count > 0 Then

                DgvSearchMembers.AutoGenerateColumns = False
                DgvSearchMembers.DataSource = dtClientes
                DgvSearchMembers.Visible = True
                DgvSearchMembers.BringToFront()

                TxtSearchMembers.BackColor = Color.Beige
            Else

                DgvSearchMembers.DataSource = Nothing
                DgvSearchMembers.Visible = False

            End If

        Catch ex As Exception
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
            RbActiveState.Checked = True

        Else
            NudNumberMembers.Value = 0
            LblNumberMembers.Text = String.Empty
            ErrorProvider.SetError(LblNumberMembers, String.Empty)
            RbActiveState.Checked = False
            RbInactiveState.Checked = False

        End If

        TxtFamilyGroupName.Clear()
        TxtSearchMembers.Clear()
        DgvListOfMembers.Rows.Clear()
        ChkEmptyGroup.Checked = False

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

        GbGroupInformation.Enabled = False
        GbMembersOfGroup.Enabled = False

        EnableGroupInformationControls()
        ChkEmptyGroup.Enabled = True

        BtnNewGroup.Focus()

        FormHelpers.SetBackColor(Color.Azure, TxtFamilyGroupName, NudNumberMembers, LblNumberMembers, TxtSearchMembers)

        _isGroupSelected = False

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

        GbGroupInformation.Enabled = True
        GbMembersOfGroup.Enabled = True

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

        GbGroupInformation.Enabled = True
        ShowFamilyGroupList()
        TxtFamilyGroupName.Focus()

    End Sub


    ''' <summary>
    ''' Habilita los controles de información del grupos.
    ''' </summary>
    Private Sub EnableGroupInformationControls()

        NudNumberMembers.Enabled = True
        RbActiveState.Enabled = True
        RbInactiveState.Enabled = True

    End Sub


    ''' <summary>
    ''' Deshabilita los controles de información del grupos.
    ''' </summary>
    Private Sub DisableGroupInformationControls()

        NudNumberMembers.Enabled = False
        RbActiveState.Enabled = False
        RbInactiveState.Enabled = False
        ChkEmptyGroup.Enabled = False

        GbMembersOfGroup.Enabled = False

    End Sub


    ''' <summary>
    ''' Configura la interfaz para la modificación de un grupo familiar,
    ''' habilitando la selección del registro y la edición de sus datos.
    ''' </summary>
    Private Sub ConfigureEditMode()

        ConfigureActionButtons()
        BtnUpdateGroup.Visible = True
        EnableGroupSelection()
        DisableGroupInformationControls()

    End Sub


    ''' <summary>
    ''' Configura la interfaz para la eliminación de un grupo familiar,
    ''' permitiendo seleccionar el registro que se desea eliminar.
    ''' </summary>
    Private Sub ConfigureDeleteMode()

        ConfigureActionButtons()
        BtnDeleteGroup.Visible = True
        EnableGroupSelection()
        DisableGroupInformationControls()

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


End Class