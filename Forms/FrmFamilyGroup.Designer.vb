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
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblNumberMembers = New System.Windows.Forms.Label()
        Me.NudNumberMembers = New System.Windows.Forms.NumericUpDown()
        Me.TxtFamilyGroupName = New System.Windows.Forms.TextBox()
        Me.LblBuscarIntgrntes = New System.Windows.Forms.Label()
        Me.TxtSearchMembers = New System.Windows.Forms.TextBox()
        Me.DgvSearchMembers = New System.Windows.Forms.DataGridView()
        Me.SearchClientId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SearchFullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SearchGroupId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlButtonPanel = New System.Windows.Forms.Panel()
        Me.BtnDeleteGroup = New System.Windows.Forms.Button()
        Me.BtnUpdateGroup = New System.Windows.Forms.Button()
        Me.BtnCloseWindow = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnSaveGroup = New System.Windows.Forms.Button()
        Me.BtnNewGroup = New System.Windows.Forms.Button()
        Me.BtnModifyGroup = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.ChkEmptyGroup = New System.Windows.Forms.CheckBox()
        Me.BtnRemoveMember = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DgvListOfMembers = New System.Windows.Forms.DataGridView()
        Me.ListClientID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ListFullName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ListGroupId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvListFamilyGroups = New System.Windows.Forms.DataGridView()
        Me.ColGroupId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColGroupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNumberMembers = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblNomGrupo = New System.Windows.Forms.Label()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GbNumberMembers = New System.Windows.Forms.GroupBox()
        Me.GbMembersOfGroup = New System.Windows.Forms.GroupBox()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvSearchMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlButtonPanel.SuspendLayout()
        CType(Me.DgvListOfMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvListFamilyGroups, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbNumberMembers.SuspendLayout()
        Me.GbMembersOfGroup.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblNumberMembers
        '
        Me.LblNumberMembers.BackColor = System.Drawing.SystemColors.Window
        Me.LblNumberMembers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblNumberMembers.Location = New System.Drawing.Point(102, 33)
        Me.LblNumberMembers.Margin = New System.Windows.Forms.Padding(8, 16, 0, 16)
        Me.LblNumberMembers.Name = "LblNumberMembers"
        Me.LblNumberMembers.Size = New System.Drawing.Size(70, 26)
        Me.LblNumberMembers.TabIndex = 0
        Me.LblNumberMembers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NudNumberMembers
        '
        Me.NudNumberMembers.BackColor = System.Drawing.SystemColors.Window
        Me.NudNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudNumberMembers.Location = New System.Drawing.Point(24, 33)
        Me.NudNumberMembers.Margin = New System.Windows.Forms.Padding(24, 16, 0, 16)
        Me.NudNumberMembers.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NudNumberMembers.Name = "NudNumberMembers"
        Me.NudNumberMembers.Size = New System.Drawing.Size(70, 26)
        Me.NudNumberMembers.TabIndex = 0
        Me.NudNumberMembers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtFamilyGroupName
        '
        Me.TxtFamilyGroupName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtFamilyGroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtFamilyGroupName.Enabled = False
        Me.TxtFamilyGroupName.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFamilyGroupName.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtFamilyGroupName.Location = New System.Drawing.Point(57, 54)
        Me.TxtFamilyGroupName.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.TxtFamilyGroupName.MaxLength = 30
        Me.TxtFamilyGroupName.Name = "TxtFamilyGroupName"
        Me.TxtFamilyGroupName.Size = New System.Drawing.Size(524, 26)
        Me.TxtFamilyGroupName.TabIndex = 0
        Me.TxtFamilyGroupName.WordWrap = False
        '
        'LblBuscarIntgrntes
        '
        Me.LblBuscarIntgrntes.AutoSize = True
        Me.LblBuscarIntgrntes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuscarIntgrntes.Location = New System.Drawing.Point(28, 33)
        Me.LblBuscarIntgrntes.Margin = New System.Windows.Forms.Padding(28, 16, 0, 0)
        Me.LblBuscarIntgrntes.Name = "LblBuscarIntgrntes"
        Me.LblBuscarIntgrntes.Size = New System.Drawing.Size(121, 16)
        Me.LblBuscarIntgrntes.TabIndex = 3
        Me.LblBuscarIntgrntes.Text = "Buscar por nombre"
        Me.LblBuscarIntgrntes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtSearchMembers
        '
        Me.TxtSearchMembers.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSearchMembers.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearchMembers.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearchMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtSearchMembers.Location = New System.Drawing.Point(24, 57)
        Me.TxtSearchMembers.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.TxtSearchMembers.MaxLength = 30
        Me.TxtSearchMembers.Name = "TxtSearchMembers"
        Me.TxtSearchMembers.Size = New System.Drawing.Size(481, 26)
        Me.TxtSearchMembers.TabIndex = 0
        Me.TxtSearchMembers.WordWrap = False
        '
        'DgvSearchMembers
        '
        Me.DgvSearchMembers.AllowUserToAddRows = False
        Me.DgvSearchMembers.AllowUserToDeleteRows = False
        Me.DgvSearchMembers.AllowUserToResizeColumns = False
        Me.DgvSearchMembers.AllowUserToResizeRows = False
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvSearchMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle17
        Me.DgvSearchMembers.ColumnHeadersHeight = 4
        Me.DgvSearchMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvSearchMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SearchClientId, Me.SearchFullName, Me.SearchGroupId})
        Me.DgvSearchMembers.Location = New System.Drawing.Point(24, 85)
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
        Me.DgvSearchMembers.Size = New System.Drawing.Size(481, 214)
        Me.DgvSearchMembers.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.DgvSearchMembers, "DOBLE CLICK PARA SELECCIONAR UN CLIENTE")
        Me.DgvSearchMembers.Visible = False
        '
        'SearchClientId
        '
        Me.SearchClientId.DataPropertyName = "id_cli"
        Me.SearchClientId.HeaderText = "SearchClientId"
        Me.SearchClientId.Name = "SearchClientId"
        Me.SearchClientId.ReadOnly = True
        Me.SearchClientId.Visible = False
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
        Me.SearchFullName.Width = 455
        '
        'SearchGroupId
        '
        Me.SearchGroupId.DataPropertyName = "id_grp"
        Me.SearchGroupId.HeaderText = "SearchGroupId"
        Me.SearchGroupId.Name = "SearchGroupId"
        Me.SearchGroupId.ReadOnly = True
        Me.SearchGroupId.Visible = False
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
        Me.PnlButtonPanel.Location = New System.Drawing.Point(621, 0)
        Me.PnlButtonPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlButtonPanel.Name = "PnlButtonPanel"
        Me.PnlButtonPanel.Size = New System.Drawing.Size(156, 543)
        Me.PnlButtonPanel.TabIndex = 0
        '
        'BtnDeleteGroup
        '
        Me.BtnDeleteGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_family_group_53x30
        Me.BtnDeleteGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteGroup.Location = New System.Drawing.Point(8, 48)
        Me.BtnDeleteGroup.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
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
        Me.BtnUpdateGroup.Location = New System.Drawing.Point(8, 48)
        Me.BtnUpdateGroup.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
        Me.BtnUpdateGroup.Name = "BtnUpdateGroup"
        Me.BtnUpdateGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnUpdateGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnUpdateGroup.TabIndex = 4
        Me.BtnUpdateGroup.Text = "&Actualizar grupo"
        Me.BtnUpdateGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnUpdateGroup.UseVisualStyleBackColor = True
        Me.BtnUpdateGroup.Visible = False
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
        Me.BtnCloseWindow.Location = New System.Drawing.Point(8, 414)
        Me.BtnCloseWindow.Margin = New System.Windows.Forms.Padding(8, 0, 8, 48)
        Me.BtnCloseWindow.Name = "BtnCloseWindow"
        Me.BtnCloseWindow.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnCloseWindow.Size = New System.Drawing.Size(136, 64)
        Me.BtnCloseWindow.TabIndex = 7
        Me.BtnCloseWindow.Text = "Cerrar &ventana"
        Me.BtnCloseWindow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCloseWindow.UseVisualStyleBackColor = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_28x28
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancel.Location = New System.Drawing.Point(8, 127)
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
        'BtnSaveGroup
        '
        Me.BtnSaveGroup.Enabled = False
        Me.BtnSaveGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_save_28x28
        Me.BtnSaveGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSaveGroup.Location = New System.Drawing.Point(8, 47)
        Me.BtnSaveGroup.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
        Me.BtnSaveGroup.Name = "BtnSaveGroup"
        Me.BtnSaveGroup.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnSaveGroup.Size = New System.Drawing.Size(136, 64)
        Me.BtnSaveGroup.TabIndex = 3
        Me.BtnSaveGroup.Text = "&Guardar grupo"
        Me.BtnSaveGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSaveGroup.UseVisualStyleBackColor = True
        Me.BtnSaveGroup.Visible = False
        '
        'BtnNewGroup
        '
        Me.BtnNewGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_24x32
        Me.BtnNewGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewGroup.Location = New System.Drawing.Point(8, 48)
        Me.BtnNewGroup.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
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
        Me.BtnModifyGroup.Location = New System.Drawing.Point(8, 128)
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
        Me.BtnDelete.Location = New System.Drawing.Point(8, 208)
        Me.BtnDelete.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnDelete.Size = New System.Drawing.Size(136, 64)
        Me.BtnDelete.TabIndex = 2
        Me.BtnDelete.Text = "&Eliminar grupo"
        Me.BtnDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'ChkEmptyGroup
        '
        Me.ChkEmptyGroup.AutoSize = True
        Me.ChkEmptyGroup.Font = New System.Drawing.Font("Malgun Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkEmptyGroup.Location = New System.Drawing.Point(196, 33)
        Me.ChkEmptyGroup.Margin = New System.Windows.Forms.Padding(24, 16, 16, 0)
        Me.ChkEmptyGroup.Name = "ChkEmptyGroup"
        Me.ChkEmptyGroup.Size = New System.Drawing.Size(352, 25)
        Me.ChkEmptyGroup.TabIndex = 1
        Me.ChkEmptyGroup.Text = "Guardar el nuevo grupo SIN INTEGRANTES."
        Me.ChkEmptyGroup.UseVisualStyleBackColor = True
        '
        'BtnRemoveMember
        '
        Me.BtnRemoveMember.FlatAppearance.BorderSize = 0
        Me.BtnRemoveMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemoveMember.Image = Global.GymPaymentControl.My.Resources.Resources.ic_remove_30x30
        Me.BtnRemoveMember.Location = New System.Drawing.Point(513, 161)
        Me.BtnRemoveMember.Margin = New System.Windows.Forms.Padding(8, 144, 0, 0)
        Me.BtnRemoveMember.Name = "BtnRemoveMember"
        Me.BtnRemoveMember.Size = New System.Drawing.Size(35, 35)
        Me.BtnRemoveMember.TabIndex = 2
        Me.ToolTip.SetToolTip(Me.BtnRemoveMember, "QUITAR FAMILIAR DE LA LISTA")
        Me.BtnRemoveMember.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(26, 99)
        Me.Label1.Margin = New System.Windows.Forms.Padding(26, 16, 0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Lista de los integrantes"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DgvListOfMembers
        '
        Me.DgvListOfMembers.AllowUserToAddRows = False
        Me.DgvListOfMembers.AllowUserToDeleteRows = False
        Me.DgvListOfMembers.AllowUserToResizeColumns = False
        Me.DgvListOfMembers.AllowUserToResizeRows = False
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle18
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvListOfMembers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.DgvListOfMembers.ColumnHeadersHeight = 30
        Me.DgvListOfMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListOfMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ListClientID, Me.ListFullName, Me.ListGroupId})
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle20.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListOfMembers.DefaultCellStyle = DataGridViewCellStyle20
        Me.DgvListOfMembers.Location = New System.Drawing.Point(24, 123)
        Me.DgvListOfMembers.Margin = New System.Windows.Forms.Padding(0, 8, 0, 24)
        Me.DgvListOfMembers.MultiSelect = False
        Me.DgvListOfMembers.Name = "DgvListOfMembers"
        Me.DgvListOfMembers.ReadOnly = True
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvListOfMembers.RowHeadersDefaultCellStyle = DataGridViewCellStyle21
        Me.DgvListOfMembers.RowHeadersWidth = 4
        Me.DgvListOfMembers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.RowsDefaultCellStyle = DataGridViewCellStyle22
        Me.DgvListOfMembers.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.RowTemplate.DividerHeight = 2
        Me.DgvListOfMembers.RowTemplate.Height = 30
        Me.DgvListOfMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListOfMembers.Size = New System.Drawing.Size(481, 176)
        Me.DgvListOfMembers.TabIndex = 1
        Me.ToolTip.SetToolTip(Me.DgvListOfMembers, "CLICK PARA SELECCIONAR UN FAMILIAR")
        '
        'ListClientID
        '
        Me.ListClientID.DataPropertyName = "id_cli"
        Me.ListClientID.HeaderText = "ListClientID"
        Me.ListClientID.Name = "ListClientID"
        Me.ListClientID.ReadOnly = True
        Me.ListClientID.Visible = False
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
        Me.ListFullName.Width = 455
        '
        'ListGroupId
        '
        Me.ListGroupId.DataPropertyName = "id_grp"
        Me.ListGroupId.HeaderText = "ListGroupId"
        Me.ListGroupId.Name = "ListGroupId"
        Me.ListGroupId.ReadOnly = True
        Me.ListGroupId.Visible = False
        '
        'DgvListFamilyGroups
        '
        Me.DgvListFamilyGroups.AllowUserToAddRows = False
        Me.DgvListFamilyGroups.AllowUserToDeleteRows = False
        DataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListFamilyGroups.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle23
        Me.DgvListFamilyGroups.ColumnHeadersHeight = 4
        Me.DgvListFamilyGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListFamilyGroups.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColGroupId, Me.ColGroupName, Me.ColNumberMembers})
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListFamilyGroups.DefaultCellStyle = DataGridViewCellStyle24
        Me.DgvListFamilyGroups.Location = New System.Drawing.Point(57, 82)
        Me.DgvListFamilyGroups.Margin = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.DgvListFamilyGroups.MultiSelect = False
        Me.DgvListFamilyGroups.Name = "DgvListFamilyGroups"
        Me.DgvListFamilyGroups.ReadOnly = True
        Me.DgvListFamilyGroups.RowHeadersWidth = 4
        Me.DgvListFamilyGroups.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvListFamilyGroups.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListFamilyGroups.RowTemplate.DividerHeight = 1
        Me.DgvListFamilyGroups.RowTemplate.Height = 25
        Me.DgvListFamilyGroups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListFamilyGroups.Size = New System.Drawing.Size(524, 220)
        Me.DgvListFamilyGroups.TabIndex = 3
        Me.ToolTip.SetToolTip(Me.DgvListFamilyGroups, "DOBLE CLICK PARA SELECCIONAR UN GRUPO")
        Me.DgvListFamilyGroups.Visible = False
        '
        'ColGroupId
        '
        Me.ColGroupId.DataPropertyName = "id_grp"
        Me.ColGroupId.HeaderText = "ColGroupId"
        Me.ColGroupId.Name = "ColGroupId"
        Me.ColGroupId.ReadOnly = True
        Me.ColGroupId.Visible = False
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
        Me.ColGroupName.Width = 498
        '
        'ColNumberMembers
        '
        Me.ColNumberMembers.DataPropertyName = "num_intgrntes_grp"
        Me.ColNumberMembers.HeaderText = "ColNumberMembers"
        Me.ColNumberMembers.Name = "ColNumberMembers"
        Me.ColNumberMembers.ReadOnly = True
        Me.ColNumberMembers.Visible = False
        '
        'LblNomGrupo
        '
        Me.LblNomGrupo.AutoSize = True
        Me.LblNomGrupo.Font = New System.Drawing.Font("Malgun Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNomGrupo.Location = New System.Drawing.Point(60, 25)
        Me.LblNomGrupo.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblNomGrupo.Name = "LblNomGrupo"
        Me.LblNomGrupo.Size = New System.Drawing.Size(217, 21)
        Me.LblNomGrupo.TabIndex = 1
        Me.LblNomGrupo.Text = "Nombre del grupo familiar"
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'GbNumberMembers
        '
        Me.GbNumberMembers.Controls.Add(Me.ChkEmptyGroup)
        Me.GbNumberMembers.Controls.Add(Me.NudNumberMembers)
        Me.GbNumberMembers.Controls.Add(Me.LblNumberMembers)
        Me.GbNumberMembers.Enabled = False
        Me.GbNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbNumberMembers.Location = New System.Drawing.Point(33, 96)
        Me.GbNumberMembers.Margin = New System.Windows.Forms.Padding(24, 16, 24, 0)
        Me.GbNumberMembers.Name = "GbNumberMembers"
        Me.GbNumberMembers.Padding = New System.Windows.Forms.Padding(0)
        Me.GbNumberMembers.Size = New System.Drawing.Size(564, 75)
        Me.GbNumberMembers.TabIndex = 1
        Me.GbNumberMembers.TabStop = False
        Me.GbNumberMembers.Text = "Número de integrantes"
        '
        'GbMembersOfGroup
        '
        Me.GbMembersOfGroup.Controls.Add(Me.BtnRemoveMember)
        Me.GbMembersOfGroup.Controls.Add(Me.LblBuscarIntgrntes)
        Me.GbMembersOfGroup.Controls.Add(Me.DgvListOfMembers)
        Me.GbMembersOfGroup.Controls.Add(Me.Label1)
        Me.GbMembersOfGroup.Controls.Add(Me.TxtSearchMembers)
        Me.GbMembersOfGroup.Controls.Add(Me.DgvSearchMembers)
        Me.GbMembersOfGroup.Enabled = False
        Me.GbMembersOfGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.GbMembersOfGroup.Location = New System.Drawing.Point(33, 187)
        Me.GbMembersOfGroup.Margin = New System.Windows.Forms.Padding(24, 16, 24, 24)
        Me.GbMembersOfGroup.Name = "GbMembersOfGroup"
        Me.GbMembersOfGroup.Padding = New System.Windows.Forms.Padding(0)
        Me.GbMembersOfGroup.Size = New System.Drawing.Size(564, 323)
        Me.GbMembersOfGroup.TabIndex = 2
        Me.GbMembersOfGroup.TabStop = False
        Me.GbMembersOfGroup.Text = "Integrantes del grupo familiar"
        '
        'FrmFamilyGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(777, 543)
        Me.Controls.Add(Me.GbMembersOfGroup)
        Me.Controls.Add(Me.TxtFamilyGroupName)
        Me.Controls.Add(Me.LblNomGrupo)
        Me.Controls.Add(Me.GbNumberMembers)
        Me.Controls.Add(Me.PnlButtonPanel)
        Me.Controls.Add(Me.DgvListFamilyGroups)
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
        Me.GbNumberMembers.ResumeLayout(False)
        Me.GbNumberMembers.PerformLayout()
        Me.GbMembersOfGroup.ResumeLayout(False)
        Me.GbMembersOfGroup.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblNumberMembers As Label
    Friend WithEvents NudNumberMembers As NumericUpDown
    Friend WithEvents TxtFamilyGroupName As TextBox
    Friend WithEvents LblBuscarIntgrntes As Label
    Friend WithEvents TxtSearchMembers As TextBox
    Friend WithEvents BtnCloseWindow As Button
    Friend WithEvents DgvSearchMembers As DataGridView
    Friend WithEvents PnlButtonPanel As Panel
    Friend WithEvents ChkEmptyGroup As CheckBox
    Friend WithEvents BtnRemoveMember As Button
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents Label1 As Label
    Friend WithEvents DgvListOfMembers As DataGridView
    Friend WithEvents LblNomGrupo As Label
    Friend WithEvents DgvListFamilyGroups As DataGridView
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents GbNumberMembers As GroupBox
    Friend WithEvents GbMembersOfGroup As GroupBox
    Friend WithEvents SearchClientId As DataGridViewTextBoxColumn
    Friend WithEvents SearchFullName As DataGridViewTextBoxColumn
    Friend WithEvents SearchGroupId As DataGridViewTextBoxColumn
    Friend WithEvents ListClientID As DataGridViewTextBoxColumn
    Friend WithEvents ListFullName As DataGridViewTextBoxColumn
    Friend WithEvents ListGroupId As DataGridViewTextBoxColumn
    Friend WithEvents ColGroupId As DataGridViewTextBoxColumn
    Friend WithEvents ColGroupName As DataGridViewTextBoxColumn
    Friend WithEvents ColNumberMembers As DataGridViewTextBoxColumn
    Friend WithEvents BtnModifyGroup As Button
    Friend WithEvents BtnNewGroup As Button
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents BtnSaveGroup As Button
    Friend WithEvents BtnDeleteGroup As Button
    Friend WithEvents BtnUpdateGroup As Button
End Class
