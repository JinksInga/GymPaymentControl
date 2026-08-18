<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFamilyGroup
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblNumberMembers = New System.Windows.Forms.Label()
        Me.NudNumberMembers = New System.Windows.Forms.NumericUpDown()
        Me.TxtFamilyGroupName = New System.Windows.Forms.TextBox()
        Me.LblSearchMembers = New System.Windows.Forms.Label()
        Me.TxtSearchMembers = New System.Windows.Forms.TextBox()
        Me.DgvSearchMembers = New System.Windows.Forms.DataGridView()
        Me.SearchFullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlButtonPanel = New System.Windows.Forms.Panel()
        Me.BtnCloseWindow = New System.Windows.Forms.Button()
        Me.BtnNewGroup = New System.Windows.Forms.Button()
        Me.BtnModifyGroup = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnDeleteGroup = New System.Windows.Forms.Button()
        Me.BtnUpdateGroup = New System.Windows.Forms.Button()
        Me.BtnSaveGroup = New System.Windows.Forms.Button()
        Me.ChkEmptyGroup = New System.Windows.Forms.CheckBox()
        Me.BtnRemoveMember = New System.Windows.Forms.Button()
        Me.LblListOfMembers = New System.Windows.Forms.Label()
        Me.DgvListOfMembers = New System.Windows.Forms.DataGridView()
        Me.ListFullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvListFamilyGroups = New System.Windows.Forms.DataGridView()
        Me.ColGroupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblFamilyGroupName = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GbGroupInformation = New System.Windows.Forms.GroupBox()
        Me.LblWarning = New System.Windows.Forms.Label()
        Me.GbEmptyGroup = New System.Windows.Forms.GroupBox()
        Me.RbInactiveState = New System.Windows.Forms.RadioButton()
        Me.RbActiveState = New System.Windows.Forms.RadioButton()
        Me.LblStatusGroup = New System.Windows.Forms.Label()
        Me.LblIntegrantes = New System.Windows.Forms.Label()
        Me.GbMembersOfGroup = New System.Windows.Forms.GroupBox()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvSearchMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlButtonPanel.SuspendLayout()
        CType(Me.DgvListOfMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvListFamilyGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbGroupInformation.SuspendLayout()
        Me.GbEmptyGroup.SuspendLayout()
        Me.GbMembersOfGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblNumberMembers
        '
        Me.LblNumberMembers.BackColor = System.Drawing.SystemColors.Window
        Me.LblNumberMembers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblNumberMembers.Location = New System.Drawing.Point(120, 179)
        Me.LblNumberMembers.Margin = New System.Windows.Forms.Padding(16, 8, 24, 0)
        Me.LblNumberMembers.Name = "LblNumberMembers"
        Me.LblNumberMembers.Size = New System.Drawing.Size(80, 26)
        Me.LblNumberMembers.TabIndex = 0
        Me.LblNumberMembers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NudNumberMembers
        '
        Me.NudNumberMembers.BackColor = System.Drawing.SystemColors.Window
        Me.NudNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudNumberMembers.Location = New System.Drawing.Point(24, 179)
        Me.NudNumberMembers.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.NudNumberMembers.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NudNumberMembers.Name = "NudNumberMembers"
        Me.NudNumberMembers.Size = New System.Drawing.Size(80, 26)
        Me.NudNumberMembers.TabIndex = 0
        Me.NudNumberMembers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtFamilyGroupName
        '
        Me.TxtFamilyGroupName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFamilyGroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtFamilyGroupName.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFamilyGroupName.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtFamilyGroupName.Location = New System.Drawing.Point(24, 61)
        Me.TxtFamilyGroupName.Margin = New System.Windows.Forms.Padding(24, 8, 24, 0)
        Me.TxtFamilyGroupName.MaxLength = 50
        Me.TxtFamilyGroupName.Name = "TxtFamilyGroupName"
        Me.TxtFamilyGroupName.Size = New System.Drawing.Size(351, 26)
        Me.TxtFamilyGroupName.TabIndex = 0
        Me.TxtFamilyGroupName.WordWrap = False
        '
        'LblSearchMembers
        '
        Me.LblSearchMembers.AutoSize = True
        Me.LblSearchMembers.Font = New System.Drawing.Font("Linux Biolinum G", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LblSearchMembers.Location = New System.Drawing.Point(28, 36)
        Me.LblSearchMembers.Margin = New System.Windows.Forms.Padding(28, 16, 0, 0)
        Me.LblSearchMembers.Name = "LblSearchMembers"
        Me.LblSearchMembers.Size = New System.Drawing.Size(138, 17)
        Me.LblSearchMembers.TabIndex = 3
        Me.LblSearchMembers.Text = "Buscar por nombre"
        Me.LblSearchMembers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtSearchMembers
        '
        Me.TxtSearchMembers.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSearchMembers.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearchMembers.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearchMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtSearchMembers.Location = New System.Drawing.Point(24, 61)
        Me.TxtSearchMembers.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.TxtSearchMembers.MaxLength = 50
        Me.TxtSearchMembers.Name = "TxtSearchMembers"
        Me.TxtSearchMembers.Size = New System.Drawing.Size(351, 26)
        Me.TxtSearchMembers.TabIndex = 0
        Me.TxtSearchMembers.WordWrap = False
        '
        'DgvSearchMembers
        '
        Me.DgvSearchMembers.AllowUserToAddRows = False
        Me.DgvSearchMembers.AllowUserToDeleteRows = False
        Me.DgvSearchMembers.AllowUserToResizeColumns = False
        Me.DgvSearchMembers.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvSearchMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvSearchMembers.ColumnHeadersHeight = 4
        Me.DgvSearchMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvSearchMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SearchFullName})
        Me.DgvSearchMembers.Location = New System.Drawing.Point(24, 89)
        Me.DgvSearchMembers.Margin = New System.Windows.Forms.Padding(24, 2, 0, 24)
        Me.DgvSearchMembers.MultiSelect = False
        Me.DgvSearchMembers.Name = "DgvSearchMembers"
        Me.DgvSearchMembers.ReadOnly = True
        Me.DgvSearchMembers.RowHeadersWidth = 4
        Me.DgvSearchMembers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvSearchMembers.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvSearchMembers.RowTemplate.DividerHeight = 1
        Me.DgvSearchMembers.RowTemplate.Height = 25
        Me.DgvSearchMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvSearchMembers.Size = New System.Drawing.Size(351, 255)
        Me.DgvSearchMembers.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.DgvSearchMembers, "DOBLE CLICK PARA SELECCIONAR UN CLIENTE")
        Me.DgvSearchMembers.Visible = False
        '
        'SearchFullName
        '
        Me.SearchFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.SearchFullName.DataPropertyName = "full_name"
        Me.SearchFullName.HeaderText = "SearchFullName"
        Me.SearchFullName.Name = "SearchFullName"
        Me.SearchFullName.ReadOnly = True
        Me.SearchFullName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SearchFullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.SearchFullName.Width = 327
        '
        'PnlButtonPanel
        '
        Me.PnlButtonPanel.AutoSize = True
        Me.PnlButtonPanel.BackColor = System.Drawing.Color.Silver
        Me.PnlButtonPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlButtonPanel.Controls.Add(Me.BtnCloseWindow)
        Me.PnlButtonPanel.Controls.Add(Me.BtnNewGroup)
        Me.PnlButtonPanel.Controls.Add(Me.BtnModifyGroup)
        Me.PnlButtonPanel.Controls.Add(Me.BtnDelete)
        Me.PnlButtonPanel.Controls.Add(Me.BtnCancel)
        Me.PnlButtonPanel.Controls.Add(Me.BtnDeleteGroup)
        Me.PnlButtonPanel.Controls.Add(Me.BtnUpdateGroup)
        Me.PnlButtonPanel.Controls.Add(Me.BtnSaveGroup)
        Me.PnlButtonPanel.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlButtonPanel.Location = New System.Drawing.Point(890, 0)
        Me.PnlButtonPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlButtonPanel.Name = "PnlButtonPanel"
        Me.PnlButtonPanel.Size = New System.Drawing.Size(156, 426)
        Me.PnlButtonPanel.TabIndex = 0
        '
        'BtnCloseWindow
        '
        Me.BtnCloseWindow.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCloseWindow.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnCloseWindow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnCloseWindow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnCloseWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCloseWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCloseWindow.ForeColor = System.Drawing.Color.Brown
        Me.BtnCloseWindow.Image = Global.GymPaymentControl.My.Resources.Resources.ic_close_22x22
        Me.BtnCloseWindow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCloseWindow.Location = New System.Drawing.Point(8, 311)
        Me.BtnCloseWindow.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.BtnCloseWindow.Name = "BtnCloseWindow"
        Me.BtnCloseWindow.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnCloseWindow.Size = New System.Drawing.Size(136, 64)
        Me.BtnCloseWindow.TabIndex = 7
        Me.BtnCloseWindow.Text = "Cerrar &ventana"
        Me.BtnCloseWindow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCloseWindow.UseVisualStyleBackColor = False
        '
        'BtnNewGroup
        '
        Me.BtnNewGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_24x32
        Me.BtnNewGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewGroup.Location = New System.Drawing.Point(8, 31)
        Me.BtnNewGroup.Margin = New System.Windows.Forms.Padding(8, 24, 8, 0)
        Me.BtnNewGroup.Name = "BtnNewGroup"
        Me.BtnNewGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnNewGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnNewGroup.TabIndex = 0
        Me.BtnNewGroup.Text = "&Nuevo grupo"
        Me.BtnNewGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewGroup.UseVisualStyleBackColor = True
        '
        'BtnModifyGroup
        '
        Me.BtnModifyGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnModifyGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_modify_28x32
        Me.BtnModifyGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnModifyGroup.Location = New System.Drawing.Point(8, 111)
        Me.BtnModifyGroup.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnModifyGroup.Name = "BtnModifyGroup"
        Me.BtnModifyGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnModifyGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnModifyGroup.TabIndex = 1
        Me.BtnModifyGroup.Text = "&Modificar grupo"
        Me.BtnModifyGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnModifyGroup.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Image = Global.GymPaymentControl.My.Resources.Resources.ic_delete_28x32
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDelete.Location = New System.Drawing.Point(8, 190)
        Me.BtnDelete.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnDelete.Size = New System.Drawing.Size(136, 64)
        Me.BtnDelete.TabIndex = 2
        Me.BtnDelete.Text = "&Eliminar grupo"
        Me.BtnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_28x28
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancel.Location = New System.Drawing.Point(8, 110)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnCancel.Size = New System.Drawing.Size(136, 64)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "&Cancelar"
        Me.BtnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancel.UseVisualStyleBackColor = True
        Me.BtnCancel.Visible = False
        '
        'BtnDeleteGroup
        '
        Me.BtnDeleteGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_family_group_53x30
        Me.BtnDeleteGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteGroup.Location = New System.Drawing.Point(8, 33)
        Me.BtnDeleteGroup.Margin = New System.Windows.Forms.Padding(8, 24, 8, 0)
        Me.BtnDeleteGroup.Name = "BtnDeleteGroup"
        Me.BtnDeleteGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnDeleteGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnDeleteGroup.TabIndex = 5
        Me.BtnDeleteGroup.Text = "&Eliminar grupo"
        Me.BtnDeleteGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDeleteGroup.UseVisualStyleBackColor = True
        Me.BtnDeleteGroup.Visible = False
        '
        'BtnUpdateGroup
        '
        Me.BtnUpdateGroup.Enabled = False
        Me.BtnUpdateGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdateGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_update_28x27
        Me.BtnUpdateGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUpdateGroup.Location = New System.Drawing.Point(8, 33)
        Me.BtnUpdateGroup.Margin = New System.Windows.Forms.Padding(8, 24, 8, 0)
        Me.BtnUpdateGroup.Name = "BtnUpdateGroup"
        Me.BtnUpdateGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnUpdateGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnUpdateGroup.TabIndex = 4
        Me.BtnUpdateGroup.Text = "&Actualizar grupo"
        Me.BtnUpdateGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnUpdateGroup.UseVisualStyleBackColor = True
        Me.BtnUpdateGroup.Visible = False
        '
        'BtnSaveGroup
        '
        Me.BtnSaveGroup.Enabled = False
        Me.BtnSaveGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_save_28x28
        Me.BtnSaveGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSaveGroup.Location = New System.Drawing.Point(8, 32)
        Me.BtnSaveGroup.Margin = New System.Windows.Forms.Padding(8, 24, 8, 0)
        Me.BtnSaveGroup.Name = "BtnSaveGroup"
        Me.BtnSaveGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnSaveGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnSaveGroup.TabIndex = 3
        Me.BtnSaveGroup.Text = "&Guardar grupo"
        Me.BtnSaveGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSaveGroup.UseVisualStyleBackColor = True
        Me.BtnSaveGroup.Visible = False
        '
        'ChkEmptyGroup
        '
        Me.ChkEmptyGroup.AutoSize = True
        Me.ChkEmptyGroup.Font = New System.Drawing.Font("Linux Biolinum G", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkEmptyGroup.Location = New System.Drawing.Point(7, 17)
        Me.ChkEmptyGroup.Margin = New System.Windows.Forms.Padding(8, 4, 8, 8)
        Me.ChkEmptyGroup.Name = "ChkEmptyGroup"
        Me.ChkEmptyGroup.Size = New System.Drawing.Size(335, 21)
        Me.ChkEmptyGroup.TabIndex = 1
        Me.ChkEmptyGroup.Text = "Guardar el nuevo grupo SIN INTEGRANTES."
        Me.ChkEmptyGroup.UseVisualStyleBackColor = True
        '
        'BtnRemoveMember
        '
        Me.BtnRemoveMember.FlatAppearance.BorderSize = 0
        Me.BtnRemoveMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemoveMember.Image = Global.GymPaymentControl.My.Resources.Resources.ic_remove_30x30
        Me.BtnRemoveMember.Location = New System.Drawing.Point(383, 164)
        Me.BtnRemoveMember.Margin = New System.Windows.Forms.Padding(8, 144, 8, 0)
        Me.BtnRemoveMember.Name = "BtnRemoveMember"
        Me.BtnRemoveMember.Size = New System.Drawing.Size(35, 35)
        Me.BtnRemoveMember.TabIndex = 2
        Me.ToolTip.SetToolTip(Me.BtnRemoveMember, "QUITAR FAMILIAR DE LA LISTA")
        Me.BtnRemoveMember.UseVisualStyleBackColor = True
        '
        'LblListOfMembers
        '
        Me.LblListOfMembers.AutoSize = True
        Me.LblListOfMembers.Font = New System.Drawing.Font("Linux Biolinum G", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LblListOfMembers.Location = New System.Drawing.Point(28, 103)
        Me.LblListOfMembers.Margin = New System.Windows.Forms.Padding(28, 16, 0, 0)
        Me.LblListOfMembers.Name = "LblListOfMembers"
        Me.LblListOfMembers.Size = New System.Drawing.Size(166, 17)
        Me.LblListOfMembers.TabIndex = 4
        Me.LblListOfMembers.Text = "Lista de los integrantes"
        Me.LblListOfMembers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DgvListOfMembers
        '
        Me.DgvListOfMembers.AllowUserToAddRows = False
        Me.DgvListOfMembers.AllowUserToDeleteRows = False
        Me.DgvListOfMembers.AllowUserToResizeColumns = False
        Me.DgvListOfMembers.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Malgun Gothic", 11.25!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvListOfMembers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvListOfMembers.ColumnHeadersHeight = 30
        Me.DgvListOfMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListOfMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ListFullName})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Malgun Gothic", 11.25!)
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListOfMembers.DefaultCellStyle = DataGridViewCellStyle4
        Me.DgvListOfMembers.Location = New System.Drawing.Point(24, 128)
        Me.DgvListOfMembers.Margin = New System.Windows.Forms.Padding(24, 8, 0, 24)
        Me.DgvListOfMembers.MultiSelect = False
        Me.DgvListOfMembers.Name = "DgvListOfMembers"
        Me.DgvListOfMembers.ReadOnly = True
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Malgun Gothic", 11.25!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvListOfMembers.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvListOfMembers.RowHeadersWidth = 4
        Me.DgvListOfMembers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DgvListOfMembers.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.RowTemplate.DividerHeight = 2
        Me.DgvListOfMembers.RowTemplate.Height = 30
        Me.DgvListOfMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListOfMembers.Size = New System.Drawing.Size(351, 216)
        Me.DgvListOfMembers.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.DgvListOfMembers, "CLICK PARA SELECCIONAR UN FAMILIAR")
        '
        'ListFullName
        '
        Me.ListFullName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ListFullName.DataPropertyName = "full_name"
        Me.ListFullName.HeaderText = "Nombre y Apellido"
        Me.ListFullName.Name = "ListFullName"
        Me.ListFullName.ReadOnly = True
        Me.ListFullName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ListFullName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ListFullName.Width = 327
        '
        'DgvListFamilyGroups
        '
        Me.DgvListFamilyGroups.AllowUserToAddRows = False
        Me.DgvListFamilyGroups.AllowUserToDeleteRows = False
        DataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListFamilyGroups.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.DgvListFamilyGroups.ColumnHeadersHeight = 4
        Me.DgvListFamilyGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListFamilyGroups.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColGroupName})
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Malgun Gothic", 11.25!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListFamilyGroups.DefaultCellStyle = DataGridViewCellStyle8
        Me.DgvListFamilyGroups.Location = New System.Drawing.Point(24, 89)
        Me.DgvListFamilyGroups.Margin = New System.Windows.Forms.Padding(24, 2, 24, 24)
        Me.DgvListFamilyGroups.MultiSelect = False
        Me.DgvListFamilyGroups.Name = "DgvListFamilyGroups"
        Me.DgvListFamilyGroups.ReadOnly = True
        Me.DgvListFamilyGroups.RowHeadersWidth = 4
        Me.DgvListFamilyGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvListFamilyGroups.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListFamilyGroups.RowTemplate.DividerHeight = 1
        Me.DgvListFamilyGroups.RowTemplate.Height = 25
        Me.DgvListFamilyGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListFamilyGroups.Size = New System.Drawing.Size(351, 255)
        Me.DgvListFamilyGroups.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.DgvListFamilyGroups, "DOBLE CLICK PARA SELECCIONAR UN GRUPO")
        Me.DgvListFamilyGroups.Visible = False
        '
        'ColGroupName
        '
        Me.ColGroupName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColGroupName.DataPropertyName = "nom_grp"
        Me.ColGroupName.HeaderText = "ColGroupName"
        Me.ColGroupName.Name = "ColGroupName"
        Me.ColGroupName.ReadOnly = True
        Me.ColGroupName.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColGroupName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColGroupName.Width = 327
        '
        'LblFamilyGroupName
        '
        Me.LblFamilyGroupName.AutoSize = True
        Me.LblFamilyGroupName.Font = New System.Drawing.Font("Linux Biolinum G", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFamilyGroupName.Location = New System.Drawing.Point(28, 36)
        Me.LblFamilyGroupName.Margin = New System.Windows.Forms.Padding(28, 16, 0, 0)
        Me.LblFamilyGroupName.Name = "LblFamilyGroupName"
        Me.LblFamilyGroupName.Size = New System.Drawing.Size(134, 17)
        Me.LblFamilyGroupName.TabIndex = 1
        Me.LblFamilyGroupName.Text = "Nombre del grupo"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'GbGroupInformation
        '
        Me.GbGroupInformation.Controls.Add(Me.LblWarning)
        Me.GbGroupInformation.Controls.Add(Me.GbEmptyGroup)
        Me.GbGroupInformation.Controls.Add(Me.RbInactiveState)
        Me.GbGroupInformation.Controls.Add(Me.LblFamilyGroupName)
        Me.GbGroupInformation.Controls.Add(Me.RbActiveState)
        Me.GbGroupInformation.Controls.Add(Me.LblStatusGroup)
        Me.GbGroupInformation.Controls.Add(Me.TxtFamilyGroupName)
        Me.GbGroupInformation.Controls.Add(Me.LblNumberMembers)
        Me.GbGroupInformation.Controls.Add(Me.NudNumberMembers)
        Me.GbGroupInformation.Controls.Add(Me.LblIntegrantes)
        Me.GbGroupInformation.Controls.Add(Me.DgvListFamilyGroups)
        Me.GbGroupInformation.Enabled = False
        Me.GbGroupInformation.Font = New System.Drawing.Font("Malgun Gothic", 11.25!)
        Me.GbGroupInformation.Location = New System.Drawing.Point(33, 25)
        Me.GbGroupInformation.Margin = New System.Windows.Forms.Padding(24, 16, 8, 24)
        Me.GbGroupInformation.Name = "GbGroupInformation"
        Me.GbGroupInformation.Padding = New System.Windows.Forms.Padding(0)
        Me.GbGroupInformation.Size = New System.Drawing.Size(399, 368)
        Me.GbGroupInformation.TabIndex = 1
        Me.GbGroupInformation.TabStop = False
        Me.GbGroupInformation.Text = "Información del grupo familiar"
        '
        'LblWarning
        '
        Me.LblWarning.Font = New System.Drawing.Font("Malgun Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWarning.ForeColor = System.Drawing.Color.DarkOrange
        Me.LblWarning.Location = New System.Drawing.Point(28, 290)
        Me.LblWarning.Margin = New System.Windows.Forms.Padding(28, 8, 0, 0)
        Me.LblWarning.Name = "LblWarning"
        Me.LblWarning.Size = New System.Drawing.Size(292, 41)
        Me.LblWarning.TabIndex = 14
        Me.LblWarning.Text = "⚠ Advertencia : El grupo familiar y todos sus      integrantes pasarán a estado I" &
    "nactivo. ⚠"
        Me.LblWarning.Visible = False
        '
        'GbEmptyGroup
        '
        Me.GbEmptyGroup.Controls.Add(Me.ChkEmptyGroup)
        Me.GbEmptyGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbEmptyGroup.Location = New System.Drawing.Point(24, 88)
        Me.GbEmptyGroup.Margin = New System.Windows.Forms.Padding(25, 0, 25, 0)
        Me.GbEmptyGroup.Name = "GbEmptyGroup"
        Me.GbEmptyGroup.Padding = New System.Windows.Forms.Padding(0)
        Me.GbEmptyGroup.Size = New System.Drawing.Size(349, 46)
        Me.GbEmptyGroup.TabIndex = 13
        Me.GbEmptyGroup.TabStop = False
        '
        'RbInactiveState
        '
        Me.RbInactiveState.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbInactiveState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbInactiveState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbInactiveState.Location = New System.Drawing.Point(192, 250)
        Me.RbInactiveState.Margin = New System.Windows.Forms.Padding(8, 8, 0, 0)
        Me.RbInactiveState.Name = "RbInactiveState"
        Me.RbInactiveState.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbInactiveState.Size = New System.Drawing.Size(160, 32)
        Me.RbInactiveState.TabIndex = 12
        Me.RbInactiveState.Text = "Inactivo"
        Me.RbInactiveState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RbInactiveState.UseVisualStyleBackColor = True
        '
        'RbActiveState
        '
        Me.RbActiveState.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbActiveState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbActiveState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbActiveState.Location = New System.Drawing.Point(24, 250)
        Me.RbActiveState.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.RbActiveState.Name = "RbActiveState"
        Me.RbActiveState.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbActiveState.Size = New System.Drawing.Size(160, 32)
        Me.RbActiveState.TabIndex = 11
        Me.RbActiveState.Text = "Activo"
        Me.RbActiveState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RbActiveState.UseVisualStyleBackColor = True
        '
        'LblStatusGroup
        '
        Me.LblStatusGroup.AutoSize = True
        Me.LblStatusGroup.Font = New System.Drawing.Font("Linux Biolinum G", 11.25!, System.Drawing.FontStyle.Bold)
        Me.LblStatusGroup.Location = New System.Drawing.Point(28, 225)
        Me.LblStatusGroup.Margin = New System.Windows.Forms.Padding(28, 20, 0, 0)
        Me.LblStatusGroup.Name = "LblStatusGroup"
        Me.LblStatusGroup.Size = New System.Drawing.Size(123, 17)
        Me.LblStatusGroup.TabIndex = 10
        Me.LblStatusGroup.Text = "Estado del grupo"
        Me.LblStatusGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblIntegrantes
        '
        Me.LblIntegrantes.AutoSize = True
        Me.LblIntegrantes.Font = New System.Drawing.Font("Linux Biolinum G", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIntegrantes.Location = New System.Drawing.Point(28, 154)
        Me.LblIntegrantes.Margin = New System.Windows.Forms.Padding(28, 20, 0, 0)
        Me.LblIntegrantes.Name = "LblIntegrantes"
        Me.LblIntegrantes.Size = New System.Drawing.Size(166, 17)
        Me.LblIntegrantes.TabIndex = 3
        Me.LblIntegrantes.Text = "Número de integrantes"
        Me.LblIntegrantes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GbMembersOfGroup
        '
        Me.GbMembersOfGroup.Controls.Add(Me.BtnRemoveMember)
        Me.GbMembersOfGroup.Controls.Add(Me.LblSearchMembers)
        Me.GbMembersOfGroup.Controls.Add(Me.LblListOfMembers)
        Me.GbMembersOfGroup.Controls.Add(Me.TxtSearchMembers)
        Me.GbMembersOfGroup.Controls.Add(Me.DgvListOfMembers)
        Me.GbMembersOfGroup.Controls.Add(Me.DgvSearchMembers)
        Me.GbMembersOfGroup.Enabled = False
        Me.GbMembersOfGroup.Font = New System.Drawing.Font("Malgun Gothic", 11.25!)
        Me.GbMembersOfGroup.Location = New System.Drawing.Point(448, 25)
        Me.GbMembersOfGroup.Margin = New System.Windows.Forms.Padding(8, 16, 16, 24)
        Me.GbMembersOfGroup.Name = "GbMembersOfGroup"
        Me.GbMembersOfGroup.Padding = New System.Windows.Forms.Padding(0)
        Me.GbMembersOfGroup.Size = New System.Drawing.Size(426, 368)
        Me.GbMembersOfGroup.TabIndex = 2
        Me.GbMembersOfGroup.TabStop = False
        Me.GbMembersOfGroup.Text = "Integrantes del grupo familiar"
        '
        'FrmFamilyGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1046, 426)
        Me.Controls.Add(Me.PnlButtonPanel)
        Me.Controls.Add(Me.GbMembersOfGroup)
        Me.Controls.Add(Me.GbGroupInformation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFamilyGroup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AGREGAR, MODIFICAR O ELIMINAR UN GRUPO FAMILIAR"
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvSearchMembers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlButtonPanel.ResumeLayout(False)
        CType(Me.DgvListOfMembers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvListFamilyGroups, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbGroupInformation.ResumeLayout(False)
        Me.GbGroupInformation.PerformLayout()
        Me.GbEmptyGroup.ResumeLayout(False)
        Me.GbEmptyGroup.PerformLayout()
        Me.GbMembersOfGroup.ResumeLayout(False)
        Me.GbMembersOfGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblNumberMembers As Label
    Friend WithEvents NudNumberMembers As NumericUpDown
    Friend WithEvents TxtFamilyGroupName As TextBox
    Friend WithEvents LblSearchMembers As Label
    Friend WithEvents TxtSearchMembers As TextBox
    Friend WithEvents BtnCloseWindow As Button
    Friend WithEvents DgvSearchMembers As DataGridView
    Friend WithEvents PnlButtonPanel As Panel
    Friend WithEvents ChkEmptyGroup As CheckBox
    Friend WithEvents BtnRemoveMember As Button
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents LblListOfMembers As Label
    Friend WithEvents DgvListOfMembers As DataGridView
    Friend WithEvents LblFamilyGroupName As Label
    Friend WithEvents DgvListFamilyGroups As DataGridView
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents GbGroupInformation As GroupBox
    Friend WithEvents GbMembersOfGroup As GroupBox
    Friend WithEvents BtnModifyGroup As Button
    Friend WithEvents BtnNewGroup As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnSaveGroup As Button
    Friend WithEvents BtnDeleteGroup As Button
    Friend WithEvents BtnUpdateGroup As Button
    Friend WithEvents LblIntegrantes As Label
    Friend WithEvents RbInactiveState As RadioButton
    Friend WithEvents RbActiveState As RadioButton
    Friend WithEvents LblStatusGroup As Label
    Friend WithEvents GbEmptyGroup As GroupBox
    Friend WithEvents ColGroupName As DataGridViewTextBoxColumn
    Friend WithEvents SearchFullName As DataGridViewTextBoxColumn
    Friend WithEvents ListFullName As DataGridViewTextBoxColumn
    Friend WithEvents LblWarning As Label
End Class
