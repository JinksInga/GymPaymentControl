Imports GymPaymentControl.Constants
Imports GymPaymentControl.Enums
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers

Public Class FrmFamilyGroup

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Servicios de Negocio (Managers) ---
    Private ReadOnly _familyGroupManager As New FamilyGroupManager()
    Private ReadOnly _paymentManager As New PaymentManager()

    ' --- Control de Flujo y Modos de Pantalla ---
    Private _currentMode As TransactionMode?

    ' --- Variable de Memoría ---
    Private _currentGroupId As Integer
    Private _originalNumberMembers As Integer

    ' --- Variables de Validación (Estado del botón) ---
    Private _isLoadingData As Boolean
    Private _isGroupNameValid As Boolean
    Private _isGroupSelected As Boolean

    ' --- Propiedad pública de salida ---
    Public Property IsNewGroupWithNoMembers As Boolean
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

        SaveFamilyGroup()

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

        If _currentMode = TransactionMode.EditRecord Then

            Dim currentCapacity As Integer = DgvListOfMembers.Rows.Count

            If currentCapacity > _originalNumberMembers Then

                Dim result As DialogResult = MessageBox.Show(ShowCapacityExpansionWarning, "Aviso importante",
                                                             MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,
                                                             MessageBoxDefaultButton.Button2)
                If result = DialogResult.Cancel Then Exit Sub

            End If

        End If

        SaveFamilyGroup()

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

        Dim result As DialogResult = MessageBox.Show(FamilyGroupDeletionConfirmation(TxtFamilyGroupName.Text, NudNumberMembers.Value),
                                                     "Eliminar grupo familiar",
                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)

        Try
            If result = DialogResult.Yes Then

                If _familyGroupManager.DeleteFamilyGroup(_currentGroupId) Then

                    MessageBox.Show(DialogMessages.FamilyGroupDeletedSuccessfully, "Operación completada",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ResetFamilyGroupForm()

                    ConfigureStandbyMode()

                End If

            End If

        Catch ex As Exception
            MsgBox($"ERROR AL ELIMINAR EL GRUPO : {vbCrLf}{ex.Message}")
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

                errorMessage = AppMessages.FamilyGroupNameRequired

            ElseIf Not _isGroupNameValid Then

                errorMessage = AppMessages.FamilyGroupNameAlreadyExists

            End If

        Else

            If String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text) Then
                errorMessage = AppMessages.InvalidFamilyGroupSelection
            End If

        End If

        FormHelpers.UpdateValidationState(TxtFamilyGroupName, _isGroupNameValid, errorMessage, ErrorProvider)

        UpdateSaveButtonState()

        If _currentMode = TransactionMode.EditRecord Then
            FormHelpers.UpdateValidationState(LblNumberMembers, True, String.Empty, ErrorProvider)
        End If

    End Sub
    Private Sub TxtFamilyGroupName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFamilyGroupName.KeyPress

        Dim strAllowKey As String = " "
        Dim strLockKey As String = "ºª"
        AllowOnlyLetters(e, strAllowKey, strLockKey)

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


    Private Sub RbInactiveState_CheckedChanged(sender As Object, e As EventArgs) Handles RbInactiveState.CheckedChanged
        LblWarning.Visible = RbInactiveState.Checked
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
            _originalNumberMembers = CInt(rowView("num_intgrntes_grp"))
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
            NudNumberMembers.Value = _originalNumberMembers
            LblNumberMembers.Text = $"{DgvListOfMembers.Rows.Count} de {NudNumberMembers.Value}"
            RbActiveState.Checked = (groupStatus = EntityStatus.Active)
            RbInactiveState.Checked = Not RbActiveState.Checked

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
    Private Sub TxtSearchMembers_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtSearchMembers.KeyPress

        Dim strAllowKey As String = " "
        Dim strLockKey As String = "ºª"
        AllowOnlyLetters(e, strAllowKey, strLockKey)

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

                ErrorProvider.SetError(TxtSearchMembers, $"{fullName}{AppMessages.CientAlreadyAddedToGroup}")

                TxtSearchMembers.Focus()
                TxtSearchMembers.SelectAll()

                Exit Sub

            End If

        Next

        '| Validar deuda individual antes de incorporarlo al grupo.
        If _paymentManager.HasPendingIndividualDebt(clientId) Then

            MessageBox.Show(DialogMessages.IndividualToGroupDebtWarning(), "Cambio no permitido",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)

            TxtSearchMembers.Focus()
            TxtSearchMembers.SelectAll()

            Exit Sub

        End If

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
            MessageBox.Show(DialogMessages.SelectMemberFromListRemove, "Seleccionar integrante",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' No permitimos modificar los integrantes si el grupo tiene una deuda pendiente.
        If _paymentManager.HasPendingGroupDebt(_currentGroupId) Then

            MessageBox.Show(DialogMessages.GroupHasPendingDebtCannotRemoveMember, "Acción no permitida",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        Dim fullName As String = DgvListOfMembers.CurrentRow.Cells("ListFullName").Value?.ToString()

        Dim response As DialogResult = MessageBox.Show(ConfirmRemoveGroupMember(fullName), "Quitar integrante",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

        If response = DialogResult.Yes Then

            '| Al remover la fila, el .Tag asignado se desecha automáticamente
            DgvListOfMembers.Rows.Remove(DgvListOfMembers.CurrentRow)
            DgvListOfMembers.CurrentCell = Nothing

            LblNumberMembers.Text = $"{DgvListOfMembers.Rows.Count} de {NudNumberMembers.Value}"

            TxtSearchMembers.Focus()
            UpdateSaveButtonState()
            BtnRemoveMember.Enabled = (DgvListOfMembers.Rows.Count > 0)

        End If

    End Sub

#End Region

    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " 1. METODOS DE VALIDACION Y REGLAS DE NEGOCIO "

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

        Dim errorMessage As String = If(isMembersSectionValid, String.Empty, AppMessages.NumberMembersNotMatchListMembers)

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



    ''' <summary>
    ''' Solicita al usuario autorización para registrar una tarifa de grupo
    ''' que no existe para la cantidad de integrantes indicada.
    ''' 
    ''' Abre <see cref="FrmPricesAndDiscounts"/> en modo de solicitud de tarifa,
    ''' indicando el número de integrantes que debe utilizarse como referencia.
    ''' 
    ''' Devuelve <see langword="True"/> cuando la tarifa ha sido registrada
    ''' correctamente y el formulario de tarifas devuelve <see cref="DialogResult.OK"/>.
    ''' </summary>
    ''' <returns>
    ''' <see langword="True"/> si la tarifa fue registrada correctamente;
    ''' en caso contrario, <see langword="False"/>.
    ''' </returns>
    Private Function AskToCreateGroupRate() As Boolean

        Dim numberMembers As Integer = CInt(NudNumberMembers.Value)

        Dim response As DialogResult = MessageBox.Show(DialogMessages.AskBeforeRegisterNewRate(numberMembers), "Tarifa no encontrada",
                                                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If response <> DialogResult.Yes Then Return False

        Using frm As New FrmPricesAndDiscounts()

            frm.IsGroupRateRequest = True
            frm.SuggestedNumberMembers = numberMembers

            Dim dialogResult As DialogResult = frm.ShowDialog(Me)

            Return dialogResult = DialogResult.OK

        End Using

    End Function

#End Region


#Region " 2. OPERACIONES DE DATOS Y BACKEND BRIDGE "

    ''' <summary>
    ''' Obtiene la lista de identificadores únicos (IDs) de los miembros
    ''' actualmente registrados en la grilla del grupo familiar.
    ''' </summary>
    ''' <returns>
    ''' Una lista de enteros (<see cref="List(Of Integer)"/>) que contiene
    ''' los IDs almacenados en la propiedad <see cref="DataGridViewRow.Tag"/>
    ''' de cada fila.
    ''' </returns>
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


    ''' <summary>
    ''' Valida que el nombre del grupo no esté vacío ni duplicado en la base de datos
    ''' y actualiza la interfaz visual en consecuencia.
    ''' </summary>
    ''' <param name="groupName">El texto con el nombre del grupo familiar a validar.</param>
    ''' <remarks>
    ''' Modifica la variable de estado <c>_isGroupNameValid</c>, notifica al <see cref="ErrorProvider"/> 
    ''' y actualiza el estado del botón de guardado invocando a <c>UpdateSaveButtonState()</c>.
    ''' </remarks>
    Private Sub ValidateAndRenderGroupDuplicates(groupName As String)

        If String.IsNullOrWhiteSpace(groupName) Then

            _isGroupNameValid = False
            FormHelpers.UpdateValidationState(TxtFamilyGroupName, False, AppMessages.FamilyGroupNameRequired, ErrorProvider)
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

                _isGroupNameValid = False
                FormHelpers.UpdateValidationState(TxtFamilyGroupName, False, FamilyGroupNameDuplicated(groupName), ErrorProvider)

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
                warningMessage = If(isValid, String.Empty, AppMessages.InvalidFamilyGroupSelection)

            Else

                DgvListFamilyGroups.DataSource = Nothing
                isValid = False
                warningMessage = AppMessages.FamilyGroupNameNotExist

            End If

            _isGroupNameValid = isValid

            FormHelpers.UpdateValidationState(TxtFamilyGroupName, isValid, warningMessage, ErrorProvider)

        Catch ex As Exception
            MsgBox($"ERROR AL BUSCAR GRUPOS :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    ''' <summary>
    ''' Realiza una búsqueda predictiva de miembros disponibles
    ''' según el texto ingresado y despliega la grilla de resultados.
    ''' </summary>
    ''' <param name="searchText">El criterio de búsqueda (nombre o fragmento)
    ''' para filtrar los miembros disponibles.</param>
    ''' <remarks>
    ''' Si la búsqueda devuelve registros, muestra la grilla flotante <see cref="DgvSearchMembers"/>
    ''' y altera el color de fondo de la caja de texto.
    ''' Si no hay resultados o el parámetro está vacío,
    ''' oculta la grilla de búsqueda y restablece la interacción de los controles.
    ''' </remarks>
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


    ''' <summary>
    ''' Guarda los datos del grupo familiar según el modo de operación actual.
    ''' 
    ''' En modo <see cref="TransactionMode.NewRecord"/> crea un nuevo grupo,
    ''' mientras que en modo <see cref="TransactionMode.EditRecord"/> actualiza
    ''' el grupo seleccionado.
    ''' 
    ''' Si no existe una tarifa para la cantidad de integrantes indicada,
    ''' solicita al usuario la creación de dicha tarifa y, una vez creada,
    ''' reintenta la operación de guardado conservando el modo de transacción actual.
    ''' </summary>
    Private Sub SaveFamilyGroup()

        Try

            Dim statusToSave As EntityStatus = If(RbActiveState.Checked, EntityStatus.Active, EntityStatus.Inactive)

            Dim result As FamilyGroupSaveResult

            Select Case _currentMode

                Case TransactionMode.NewRecord
                    result = _familyGroupManager.InsertFamilyGroup(TxtFamilyGroupName.Text, CInt(NudNumberMembers.Value),
                                                                   GetRegisteredMemberIds(), statusToSave, _currentGroupId)
                Case TransactionMode.EditRecord
                    result = _familyGroupManager.UpdateFamilyGroup(_currentGroupId, TxtFamilyGroupName.Text,
                                                                   CInt(NudNumberMembers.Value),
                                                                   GetRegisteredMemberIds(), statusToSave)
                Case Else
                    Return

            End Select


            Select Case result

                Case FamilyGroupSaveResult.Success

                    Select Case _currentMode

                        Case TransactionMode.NewRecord

                            NewGroupName = TxtFamilyGroupName.Text 'Propiedad para pasar el nombre en otro formulario.
                            UpdateGroupList()
                            ConfigureStandbyMode()
                            ShowSuccessMessage(_currentGroupId)

                        Case TransactionMode.EditRecord

                            ConfigureStandbyMode()
                            ShowSuccessMessage(_currentGroupId)

                    End Select

                    _currentMode = Nothing

                Case FamilyGroupSaveResult.GroupRateNotFound
                    ' La tarifa ya existe. Volvemos a intentar la operación original.
                    If AskToCreateGroupRate() Then SaveFamilyGroup()

            End Select

        Catch ex As Exception
            MsgBox($"ERROR AL GUARDAR / ACTUALIZAR EL GRUPO :{vbCrLf}{ex.Message}")
        End Try

    End Sub


#End Region


#Region " 3. METODOS DE LIMPIEZA Y CONTROL DE INTERFAZ (UI) "

    ''' <summary>
    ''' Restablece los controles del formulario a su estado inicial.
    ''' La configuración aplicada depende del modo de transacción actual
    ''' (nuevo registro o estado neutro).
    ''' </summary>
    Private Sub ResetFamilyGroupForm()

        TxtFamilyGroupName.Clear()
        TxtSearchMembers.Clear()
        DgvListOfMembers.Rows.Clear()
        DgvSearchMembers.DataSource = Nothing
        DgvSearchMembers.Visible = False
        ChkEmptyGroup.Checked = False

        If _currentMode = TransactionMode.NewRecord Then
            NudNumberMembers.Value = 3
            RbActiveState.Checked = True

        Else
            NudNumberMembers.Value = 0
            LblNumberMembers.Text = String.Empty

            RbActiveState.Checked = False
            RbInactiveState.Checked = False

            ErrorProvider.SetError(TxtFamilyGroupName, String.Empty)
            ErrorProvider.SetError(LblNumberMembers, String.Empty)

        End If

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

        _currentGroupId = 0
        _originalNumberMembers = 0

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

        ChkEmptyGroup.Checked = IsNewGroupWithNoMembers
        ChkEmptyGroup.Enabled = Not IsNewGroupWithNoMembers

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


    ''' <summary>
    ''' Muestra un mensaje de confirmación después
    ''' de registrar o actualizar correctamente un grupo familiar.
    ''' </summary>
    ''' <param name="groupCode">
    ''' Identificador del grupo familiar procesado.
    ''' </param>
    Private Sub ShowSuccessMessage(groupCode As Integer)

        Dim idFormatted As String = $"GRP - {groupCode:000}"
        Dim actionText As String = If(_currentMode = TransactionMode.NewRecord, "GUARDADOS", "ACTUALIZADOS")

        MessageBox.Show(OperationSuccessMessage(EntityNames.FamilyGroup, TxtFamilyGroupName.Text, idFormatted, actionText),
                        "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

#End Region


#Region " 4. ESTRUCTURAS Y ENUMS AUXILIARES "

    Public Enum TransactionMode
        NewRecord
        EditRecord
        DeleteRecord
    End Enum

#End Region

End Class