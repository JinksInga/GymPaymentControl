Imports System.Text.RegularExpressions
Imports GymPaymentControl.Services

Public Class FrmFamilyGroup

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Componentes de Negocio y Reglas Fijas ---
    Private ReadOnly _familyGroupManager As New FamilyGroupManager()

    ' --- Control de Flujo y Modos de Pantalla ---
    Private _currentMode As TransactionMode?

#End Region

#Region " EVENTOS DEL FORMULARIO (Handlers) "

    Private Sub BtnNewGroup_Click(sender As Object, e As EventArgs) Handles BtnNewGroup.Click

        ' Establecemos el modo con tu enumerador familiar
        _currentMode = TransactionMode.NewRecord

        ' 1. Limpieza total de estado y controles
        CleanControls()
        PicIntgrntes.Image = Nothing

        ' 2. Configuración de estados de controles específicos
        ChkEmptyGroup.Checked = False
        'ChkGrpVacioNombre.Enabled = True
        ChkEmptyGroup.Text = "Guardar el nuevo grupo sin integrantes."

        ' 3. Gestión visual de la interfaz
        SetInterfaceVisualState(isEditing:=True)
        'BtnSaveGroup.Visible = True

        ' 4. Enfoque inicial
        TxtFamilyGroupName.Focus()

    End Sub


    Private Sub TxtFamilyGroupName_TextChanged(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.TextChanged
        ' Si no estamos en modo edición, ignoramos los cambios internos
        If _currentMode Is Nothing Then Exit Sub

        'Dim hasText As Boolean = Not String.IsNullOrWhiteSpace(TxtFamilyGroupName.Text)
        '' Controlamos los estados de los elementos del flujo según haya texto o no
        'NudNumberMembers.Enabled = hasText

        ' Ejecutamos la búsqueda predictiva y validación en tiempo real
        ValidateAndRenderGroupDuplicates(TxtFamilyGroupName.Text.Trim())

    End Sub
    Private Sub TxtFamilyGroupName_GotFocus(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.GotFocus
        TxtFamilyGroupName.BackColor = Color.Beige
    End Sub
    Private Sub TxtFamilyGroupName_LostFocus(sender As Object, e As EventArgs) Handles TxtFamilyGroupName.LostFocus
        ' 1. Normalización del texto (Reemplaza múltiples espacios por uno solo)
        TxtFamilyGroupName.Text = Regex.Replace(TxtFamilyGroupName.Text.Trim(), "\s+", " ")

        ' 2. Alerta visual si queda vacío al salir
        TxtFamilyGroupName.BackColor = If(String.IsNullOrEmpty(TxtFamilyGroupName.Text), Color.MistyRose, Color.Azure)
    End Sub
    Private Sub TxtFamilyGroupName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtFamilyGroupName.KeyPress
        'AL PRESIONAR LA TECLA DE RETROCESO CAMBIAMOS DE COLOR EL TEXTBOX
        'If e.KeyChar = ControlChars.Back Then TxtListNomGrupo.BackColor = Color.Beige
    End Sub


    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged
        ' Actualizamos el Label dinámicamente con interpolación limpia
        ' Ej: "0 de 3"
        LblNumberMembers.Text = $"{DgvListOfMembers.RowCount} de {NudNumberMembers.Value}"
    End Sub
    Private Sub NudNumberMembers_GotFocus(sender As Object, e As EventArgs) Handles NudNumberMembers.GotFocus
        NudNumberMembers.BackColor = Color.Beige
    End Sub
    Private Sub NudNumberMembers_LostFocus(sender As Object, e As EventArgs) Handles NudNumberMembers.LostFocus
        ' Alerta visual si tiene menos de 3 miembros usando un operador condicional limpio
        NudNumberMembers.BackColor = If(NudNumberMembers.Value < 3, Color.MistyRose, Color.Azure)
    End Sub


#End Region


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " ??. LOGICA DE CARGA Y RENDERIZADO DE DATOS (Backend Bridge) "

    Private Sub ValidateAndRenderGroupDuplicates(groupName As String)

        If String.IsNullOrWhiteSpace(groupName) Then
            DgvListFamilyGroupNames.Visible = False
            NudNumberMembers.Enabled = False
            BtnSaveGroup.Enabled = False
            Exit Sub
        End If

        Try
            ' Consultamos al backend usando la búsqueda predictiva (LIKE)
            Dim dtCoincidencias As DataTable = _familyGroupManager.GetGroupsByNameMatch(groupName)

            If dtCoincidencias.Rows.Count > 0 Then

                DgvListFamilyGroupNames.AutoGenerateColumns = False

                DgvListFamilyGroupNames.DataSource = dtCoincidencias
                DgvListFamilyGroupNames.Visible = True
                DgvListFamilyGroupNames.BringToFront() ' Coloca la grilla al frente para tapar lo de abajo

                ' Revisamos en memoria si alguna coincidencia es EXACTA
                Dim exactMatch As Boolean = dtCoincidencias.AsEnumerable().Any(
                    Function(row)
                        Return row.Field(Of String)("nom_grp").Equals(groupName, StringComparison.OrdinalIgnoreCase)
                    End Function)

                If exactMatch Then
                    ' 🚨 Nombre idéntico encontrado: Bloqueamos
                    NudNumberMembers.Enabled = False
                    BtnSaveGroup.Enabled = False
                    TxtFamilyGroupName.BackColor = Color.MistyRose
                Else
                    ' Nombres parecidos sugeridos, pero este está disponible: Luz verde
                    NudNumberMembers.Enabled = True
                    BtnSaveGroup.Enabled = True
                    TxtFamilyGroupName.BackColor = Color.Azure
                End If
            Else
                ' Cero coincidencias: Todo libre
                DgvListFamilyGroupNames.DataSource = Nothing
                DgvListFamilyGroupNames.Visible = False
                NudNumberMembers.Enabled = True
                BtnSaveGroup.Enabled = True
                TxtFamilyGroupName.BackColor = Color.Azure
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub



#End Region


#Region " ??. METODOS DE LIMPIEZA Y CONTROL VISUAL (UI) "

    ''' <summary>
    ''' Limpia los controles y restablece el selector numérico a su valores base.
    ''' </summary>
    Sub CleanControls()

        LblIdGroup.Text = String.Empty
        TxtFamilyGroupName.Clear()
        NudNumberMembers.Value = 0
        TxtSearchMembers.Clear()
        LblIdClient.Text = String.Empty
        DgvListOfMembers.Rows.Clear()

        ' Para limpiar grillas vinculadas a datos (DataSource) es más seguro usar Nothing, 
        ' si es una grilla manual sin origen .Rows.Clear() está perfecto.
        'If DgvListIntgrntes.DataSource IsNot Nothing Then
        '    DgvListIntgrntes.DataSource = Nothing
        'Else
        '    DgvListIntgrntes.Rows.Clear()
        'End If
    End Sub

    ''' <summary>
    ''' Gestiona de forma centralizada la visibilidad y disponibilidad de los controles de la pantalla 
    ''' según el estado de la transacción actual.
    ''' </summary>
    ''' <param name="isEditing">
    ''' TRUE si el formulario entra en modo Creación/Edición.
    ''' FALSE para modo Consulta/Lectura.</param>
    Private Sub SetInterfaceVisualState(isEditing As Boolean)

        ' 1. EVALUAMOS LA EXISTENCIA DE DATOS EN LA TABLA PRINCIPAL
        Dim hasRows As Boolean = (DgvListFamilyGroupNames.RowCount > 0)

        ' 2. CONTROLES DE NAVEGACIÓN, EDICIÓN Y SELECCIÓN
        DgvListFamilyGroupNames.Enabled = Not isEditing AndAlso hasRows
        TxtFamilyGroupName.Enabled = isEditing

        ' Manejo del CheckBox dinámico según el modo
        ChkEmptyGroup.Enabled = isEditing

        ' 3. BOTONES DE ACCIÓN PRINCIPAL (Nuevo, Modificar, Eliminar)
        BtnNewGroup.Visible = Not isEditing
        BtnModifyGroup.Visible = Not isEditing AndAlso hasRows
        BtnDelete.Visible = Not isEditing AndAlso hasRows

        ' 4. BOTONES DE TRANSACCIÓN (Cancelar, Guardar, Actualizar)
        BtnCancel.Visible = isEditing

        If isEditing Then
            BtnSaveGroup.Visible = (_currentMode = TransactionMode.NewRecord)
            BtnUpdateGroup.Visible = (_currentMode = TransactionMode.EditRecord) ' Asumiendo nombre moderno para actualizar
        Else
            BtnSaveGroup.Visible = False
            BtnUpdateGroup.Visible = False
        End If

        ' 5. COLOFÓN ESTÉTICO (Absorbe ChangeColorsNewEdit de forma limpia)
        Dim editBackColor As Color = If(isEditing, Color.Azure, Color.FromName("Control"))
        Dim editForeColor As Color = If(isEditing, Color.MediumBlue, Color.FromName("Control"))

        NudNumberMembers.BackColor = editBackColor
        LblNumberMembers.BackColor = editBackColor
        TxtSearchMembers.BackColor = editBackColor

        NudNumberMembers.ForeColor = editForeColor
        LblNumberMembers.ForeColor = editForeColor

        ' 6. FOCOS ESTATÉGICOS AUTOMÁTICOS
        If Not isEditing Then BtnNewGroup.Focus()

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