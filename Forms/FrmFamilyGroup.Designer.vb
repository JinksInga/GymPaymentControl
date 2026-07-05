<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFamilyGroup
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblNumberMembers = New System.Windows.Forms.Label()
        Me.NudNumberMembers = New System.Windows.Forms.NumericUpDown()
        Me.TxtFamilyGroupName = New System.Windows.Forms.TextBox()
        Me.LblBuscarIntgrntes = New System.Windows.Forms.Label()
        Me.TxtSearchMembers = New System.Windows.Forms.TextBox()
        Me.DgvSearchMembers = New System.Windows.Forms.DataGridView()
        Me.ColBidCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColBnomApeCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColIdGrpBscr = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlGrpFamiliar = New System.Windows.Forms.Panel()
        Me.LblIdGroup = New System.Windows.Forms.Label()
        Me.ChkEmptyGroup = New System.Windows.Forms.CheckBox()
        Me.LblIdClient = New System.Windows.Forms.Label()
        Me.PicIntgrntes = New System.Windows.Forms.PictureBox()
        Me.BtnRemoveMember = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DgvListOfMembers = New System.Windows.Forms.DataGridView()
        Me.ColIdCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNomApeCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColIdGrp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LblNomGrupo = New System.Windows.Forms.Label()
        Me.LblIntegrantes = New System.Windows.Forms.Label()
        Me.DgvListFamilyGroupNames = New System.Windows.Forms.DataGridView()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnCerrar = New System.Windows.Forms.Button()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.BtnModifyGroup = New System.Windows.Forms.Button()
        Me.BtnNewGroup = New System.Windows.Forms.Button()
        Me.BtnSaveGroup = New System.Windows.Forms.Button()
        Me.BtnUpdateGroup = New System.Windows.Forms.Button()
        Me.BtnDeleteGroup = New System.Windows.Forms.Button()
        Me.ColIdGrupo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNomGrupo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNumIntgrntes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvSearchMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlGrpFamiliar.SuspendLayout()
        CType(Me.PicIntgrntes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvListOfMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvListFamilyGroupNames, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblNumberMembers
        '
        Me.LblNumberMembers.BackColor = System.Drawing.SystemColors.Control
        Me.LblNumberMembers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberMembers.ForeColor = System.Drawing.SystemColors.Control
        Me.LblNumberMembers.Location = New System.Drawing.Point(393, 42)
        Me.LblNumberMembers.Margin = New System.Windows.Forms.Padding(0)
        Me.LblNumberMembers.Name = "LblNumberMembers"
        Me.LblNumberMembers.Size = New System.Drawing.Size(70, 26)
        Me.LblNumberMembers.TabIndex = 0
        Me.LblNumberMembers.Text = "0 DE 0"
        Me.LblNumberMembers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NudNumberMembers
        '
        Me.NudNumberMembers.BackColor = System.Drawing.SystemColors.Control
        Me.NudNumberMembers.Enabled = False
        Me.NudNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudNumberMembers.ForeColor = System.Drawing.SystemColors.Control
        Me.NudNumberMembers.Location = New System.Drawing.Point(320, 42)
        Me.NudNumberMembers.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.NudNumberMembers.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NudNumberMembers.Name = "NudNumberMembers"
        Me.NudNumberMembers.Size = New System.Drawing.Size(70, 26)
        Me.NudNumberMembers.TabIndex = 1
        Me.NudNumberMembers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtFamilyGroupName
        '
        Me.TxtFamilyGroupName.BackColor = System.Drawing.SystemColors.Control
        Me.TxtFamilyGroupName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtFamilyGroupName.Enabled = False
        Me.TxtFamilyGroupName.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFamilyGroupName.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtFamilyGroupName.Location = New System.Drawing.Point(23, 42)
        Me.TxtFamilyGroupName.Margin = New System.Windows.Forms.Padding(20, 6, 10, 0)
        Me.TxtFamilyGroupName.MaxLength = 30
        Me.TxtFamilyGroupName.Name = "TxtFamilyGroupName"
        Me.TxtFamilyGroupName.Size = New System.Drawing.Size(287, 26)
        Me.TxtFamilyGroupName.TabIndex = 0
        Me.TxtFamilyGroupName.WordWrap = False
        '
        'LblBuscarIntgrntes
        '
        Me.LblBuscarIntgrntes.AutoSize = True
        Me.LblBuscarIntgrntes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuscarIntgrntes.Location = New System.Drawing.Point(20, 121)
        Me.LblBuscarIntgrntes.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblBuscarIntgrntes.Name = "LblBuscarIntgrntes"
        Me.LblBuscarIntgrntes.Size = New System.Drawing.Size(171, 16)
        Me.LblBuscarIntgrntes.TabIndex = 3
        Me.LblBuscarIntgrntes.Text = "Buscar integrante del grupo"
        Me.LblBuscarIntgrntes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtSearchMembers
        '
        Me.TxtSearchMembers.BackColor = System.Drawing.SystemColors.Control
        Me.TxtSearchMembers.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearchMembers.Enabled = False
        Me.TxtSearchMembers.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearchMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtSearchMembers.Location = New System.Drawing.Point(23, 143)
        Me.TxtSearchMembers.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        Me.TxtSearchMembers.MaxLength = 30
        Me.TxtSearchMembers.Name = "TxtSearchMembers"
        Me.TxtSearchMembers.Size = New System.Drawing.Size(440, 26)
        Me.TxtSearchMembers.TabIndex = 4
        Me.TxtSearchMembers.WordWrap = False
        '
        'DgvSearchMembers
        '
        Me.DgvSearchMembers.AllowUserToAddRows = False
        Me.DgvSearchMembers.AllowUserToDeleteRows = False
        Me.DgvSearchMembers.AllowUserToResizeColumns = False
        Me.DgvSearchMembers.AllowUserToResizeRows = False
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvSearchMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle12
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvSearchMembers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle13
        Me.DgvSearchMembers.ColumnHeadersHeight = 4
        Me.DgvSearchMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvSearchMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColBidCli, Me.ColBnomApeCli, Me.ColIdGrpBscr})
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvSearchMembers.DefaultCellStyle = DataGridViewCellStyle14
        Me.DgvSearchMembers.Location = New System.Drawing.Point(23, 170)
        Me.DgvSearchMembers.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.DgvSearchMembers.MultiSelect = False
        Me.DgvSearchMembers.Name = "DgvSearchMembers"
        Me.DgvSearchMembers.ReadOnly = True
        Me.DgvSearchMembers.RowHeadersWidth = 4
        Me.DgvSearchMembers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvSearchMembers.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvSearchMembers.RowTemplate.DividerHeight = 1
        Me.DgvSearchMembers.RowTemplate.Height = 25
        Me.DgvSearchMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvSearchMembers.Size = New System.Drawing.Size(440, 208)
        Me.DgvSearchMembers.TabIndex = 5
        Me.ToolTip.SetToolTip(Me.DgvSearchMembers, "DOBLE CLICK PARA SELECCIONAR UN CLIENTE")
        Me.DgvSearchMembers.Visible = False
        '
        'ColBidCli
        '
        Me.ColBidCli.HeaderText = "id_cli"
        Me.ColBidCli.Name = "ColBidCli"
        Me.ColBidCli.ReadOnly = True
        Me.ColBidCli.Visible = False
        '
        'ColBnomApeCli
        '
        Me.ColBnomApeCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColBnomApeCli.HeaderText = ""
        Me.ColBnomApeCli.Name = "ColBnomApeCli"
        Me.ColBnomApeCli.ReadOnly = True
        Me.ColBnomApeCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColBnomApeCli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColBnomApeCli.Width = 414
        '
        'ColIdGrpBscr
        '
        Me.ColIdGrpBscr.HeaderText = "ColIdGrpBscr"
        Me.ColIdGrpBscr.Name = "ColIdGrpBscr"
        Me.ColIdGrpBscr.ReadOnly = True
        Me.ColIdGrpBscr.Visible = False
        '
        'PnlGrpFamiliar
        '
        Me.PnlGrpFamiliar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlGrpFamiliar.Controls.Add(Me.LblIdGroup)
        Me.PnlGrpFamiliar.Controls.Add(Me.ChkEmptyGroup)
        Me.PnlGrpFamiliar.Controls.Add(Me.LblIdClient)
        Me.PnlGrpFamiliar.Controls.Add(Me.PicIntgrntes)
        Me.PnlGrpFamiliar.Controls.Add(Me.BtnRemoveMember)
        Me.PnlGrpFamiliar.Controls.Add(Me.Label1)
        Me.PnlGrpFamiliar.Controls.Add(Me.DgvListOfMembers)
        Me.PnlGrpFamiliar.Controls.Add(Me.LblNomGrupo)
        Me.PnlGrpFamiliar.Controls.Add(Me.LblIntegrantes)
        Me.PnlGrpFamiliar.Controls.Add(Me.LblNumberMembers)
        Me.PnlGrpFamiliar.Controls.Add(Me.NudNumberMembers)
        Me.PnlGrpFamiliar.Controls.Add(Me.TxtFamilyGroupName)
        Me.PnlGrpFamiliar.Controls.Add(Me.LblBuscarIntgrntes)
        Me.PnlGrpFamiliar.Controls.Add(Me.TxtSearchMembers)
        Me.PnlGrpFamiliar.Controls.Add(Me.DgvSearchMembers)
        Me.PnlGrpFamiliar.Controls.Add(Me.DgvListFamilyGroupNames)
        Me.PnlGrpFamiliar.Location = New System.Drawing.Point(29, 29)
        Me.PnlGrpFamiliar.Margin = New System.Windows.Forms.Padding(20, 20, 0, 0)
        Me.PnlGrpFamiliar.Name = "PnlGrpFamiliar"
        Me.PnlGrpFamiliar.Size = New System.Drawing.Size(515, 404)
        Me.PnlGrpFamiliar.TabIndex = 0
        '
        'LblIdGroup
        '
        Me.LblIdGroup.AutoSize = True
        Me.LblIdGroup.Location = New System.Drawing.Point(185, 22)
        Me.LblIdGroup.Name = "LblIdGroup"
        Me.LblIdGroup.Size = New System.Drawing.Size(59, 13)
        Me.LblIdGroup.TabIndex = 8
        Me.LblIdGroup.Text = "LblIdGroup"
        Me.LblIdGroup.Visible = False
        '
        'ChkEmptyGroup
        '
        Me.ChkEmptyGroup.AutoSize = True
        Me.ChkEmptyGroup.Enabled = False
        Me.ChkEmptyGroup.Font = New System.Drawing.Font("Malgun Gothic", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkEmptyGroup.Location = New System.Drawing.Point(33, 80)
        Me.ChkEmptyGroup.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.ChkEmptyGroup.Name = "ChkEmptyGroup"
        Me.ChkEmptyGroup.Size = New System.Drawing.Size(15, 14)
        Me.ChkEmptyGroup.TabIndex = 3
        Me.ChkEmptyGroup.UseVisualStyleBackColor = True
        '
        'LblIdClient
        '
        Me.LblIdClient.AutoSize = True
        Me.LblIdClient.Location = New System.Drawing.Point(466, 203)
        Me.LblIdClient.Name = "LblIdClient"
        Me.LblIdClient.Size = New System.Drawing.Size(56, 13)
        Me.LblIdClient.TabIndex = 7
        Me.LblIdClient.Text = "LblIdClient"
        Me.LblIdClient.Visible = False
        '
        'PicIntgrntes
        '
        Me.PicIntgrntes.Location = New System.Drawing.Point(466, 42)
        Me.PicIntgrntes.Name = "PicIntgrntes"
        Me.PicIntgrntes.Size = New System.Drawing.Size(24, 24)
        Me.PicIntgrntes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PicIntgrntes.TabIndex = 7
        Me.PicIntgrntes.TabStop = False
        '
        'BtnRemoveMember
        '
        Me.BtnRemoveMember.Enabled = False
        Me.BtnRemoveMember.FlatAppearance.BorderSize = 0
        Me.BtnRemoveMember.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemoveMember.Image = Global.GymPaymentControl.My.Resources.Resources.ic_remove_30x30
        Me.BtnRemoveMember.Location = New System.Drawing.Point(466, 235)
        Me.BtnRemoveMember.Margin = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.BtnRemoveMember.Name = "BtnRemoveMember"
        Me.BtnRemoveMember.Size = New System.Drawing.Size(35, 35)
        Me.BtnRemoveMember.TabIndex = 7
        Me.ToolTip.SetToolTip(Me.BtnRemoveMember, "QUITAR FAMILIAR DE LA LISTA")
        Me.BtnRemoveMember.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 181)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
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
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle15
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvListOfMembers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.DgvListOfMembers.ColumnHeadersHeight = 30
        Me.DgvListOfMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListOfMembers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColIdCli, Me.ColNomApeCli, Me.ColIdGrp})
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListOfMembers.DefaultCellStyle = DataGridViewCellStyle17
        Me.DgvListOfMembers.Enabled = False
        Me.DgvListOfMembers.Location = New System.Drawing.Point(23, 203)
        Me.DgvListOfMembers.Margin = New System.Windows.Forms.Padding(0, 6, 0, 20)
        Me.DgvListOfMembers.MultiSelect = False
        Me.DgvListOfMembers.Name = "DgvListOfMembers"
        Me.DgvListOfMembers.ReadOnly = True
        DataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvListOfMembers.RowHeadersDefaultCellStyle = DataGridViewCellStyle18
        Me.DgvListOfMembers.RowHeadersWidth = 4
        Me.DgvListOfMembers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.RowsDefaultCellStyle = DataGridViewCellStyle19
        Me.DgvListOfMembers.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListOfMembers.RowTemplate.DividerHeight = 2
        Me.DgvListOfMembers.RowTemplate.Height = 30
        Me.DgvListOfMembers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListOfMembers.Size = New System.Drawing.Size(440, 175)
        Me.DgvListOfMembers.TabIndex = 6
        Me.ToolTip.SetToolTip(Me.DgvListOfMembers, "CLICK PARA SELECCIONAR UN FAMILIAR")
        '
        'ColIdCli
        '
        Me.ColIdCli.HeaderText = "idCli"
        Me.ColIdCli.Name = "ColIdCli"
        Me.ColIdCli.ReadOnly = True
        Me.ColIdCli.Visible = False
        '
        'ColNomApeCli
        '
        Me.ColNomApeCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColNomApeCli.HeaderText = "Nombre y Apellido"
        Me.ColNomApeCli.Name = "ColNomApeCli"
        Me.ColNomApeCli.ReadOnly = True
        Me.ColNomApeCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColNomApeCli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColNomApeCli.Width = 414
        '
        'ColIdGrp
        '
        Me.ColIdGrp.HeaderText = "ColIdGrp"
        Me.ColIdGrp.Name = "ColIdGrp"
        Me.ColIdGrp.ReadOnly = True
        Me.ColIdGrp.Visible = False
        '
        'LblNomGrupo
        '
        Me.LblNomGrupo.AutoSize = True
        Me.LblNomGrupo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNomGrupo.Location = New System.Drawing.Point(20, 20)
        Me.LblNomGrupo.Margin = New System.Windows.Forms.Padding(20, 20, 0, 0)
        Me.LblNomGrupo.Name = "LblNomGrupo"
        Me.LblNomGrupo.Size = New System.Drawing.Size(162, 16)
        Me.LblNomGrupo.TabIndex = 1
        Me.LblNomGrupo.Text = "Nombre del grupo familiar"
        '
        'LblIntegrantes
        '
        Me.LblIntegrantes.AutoSize = True
        Me.LblIntegrantes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIntegrantes.Location = New System.Drawing.Point(320, 20)
        Me.LblIntegrantes.Margin = New System.Windows.Forms.Padding(0)
        Me.LblIntegrantes.Name = "LblIntegrantes"
        Me.LblIntegrantes.Size = New System.Drawing.Size(143, 16)
        Me.LblIntegrantes.TabIndex = 2
        Me.LblIntegrantes.Text = "Número de integrantes"
        Me.LblIntegrantes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DgvListFamilyGroupNames
        '
        Me.DgvListFamilyGroupNames.AllowUserToAddRows = False
        Me.DgvListFamilyGroupNames.AllowUserToDeleteRows = False
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListFamilyGroupNames.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle20
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvListFamilyGroupNames.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle21
        Me.DgvListFamilyGroupNames.ColumnHeadersHeight = 4
        Me.DgvListFamilyGroupNames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListFamilyGroupNames.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColIdGrupo, Me.ColNomGrupo, Me.ColNumIntgrntes})
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListFamilyGroupNames.DefaultCellStyle = DataGridViewCellStyle22
        Me.DgvListFamilyGroupNames.Location = New System.Drawing.Point(23, 69)
        Me.DgvListFamilyGroupNames.Margin = New System.Windows.Forms.Padding(0, 1, 0, 0)
        Me.DgvListFamilyGroupNames.MultiSelect = False
        Me.DgvListFamilyGroupNames.Name = "DgvListFamilyGroupNames"
        Me.DgvListFamilyGroupNames.ReadOnly = True
        Me.DgvListFamilyGroupNames.RowHeadersWidth = 4
        Me.DgvListFamilyGroupNames.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvListFamilyGroupNames.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvListFamilyGroupNames.RowTemplate.DividerHeight = 1
        Me.DgvListFamilyGroupNames.RowTemplate.Height = 25
        Me.DgvListFamilyGroupNames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListFamilyGroupNames.Size = New System.Drawing.Size(440, 309)
        Me.DgvListFamilyGroupNames.TabIndex = 2
        Me.ToolTip.SetToolTip(Me.DgvListFamilyGroupNames, "DOBLE CLICK PARA SELECCIONAR UN GRUPO")
        Me.DgvListFamilyGroupNames.Visible = False
        '
        'BtnCancel
        '
        Me.BtnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_28x28
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCancel.Location = New System.Drawing.Point(564, 114)
        Me.BtnCancel.Margin = New System.Windows.Forms.Padding(0, 24, 0, 0)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Padding = New System.Windows.Forms.Padding(16, 0, 14, 0)
        Me.BtnCancel.Size = New System.Drawing.Size(150, 45)
        Me.BtnCancel.TabIndex = 3
        Me.BtnCancel.Text = "&Cancelar"
        Me.BtnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancel.UseVisualStyleBackColor = True
        Me.BtnCancel.Visible = False
        '
        'BtnCerrar
        '
        Me.BtnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCerrar.ForeColor = System.Drawing.Color.Brown
        Me.BtnCerrar.Image = Global.GymPaymentControl.My.Resources.Resources.ic_close_22x22
        Me.BtnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCerrar.Location = New System.Drawing.Point(564, 369)
        Me.BtnCerrar.Margin = New System.Windows.Forms.Padding(0, 0, 20, 20)
        Me.BtnCerrar.Name = "BtnCerrar"
        Me.BtnCerrar.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.BtnCerrar.Size = New System.Drawing.Size(150, 40)
        Me.BtnCerrar.TabIndex = 7
        Me.BtnCerrar.Text = "Cerrar &ventana"
        Me.BtnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCerrar.UseVisualStyleBackColor = False
        '
        'BtnDelete
        '
        Me.BtnDelete.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Image = Global.GymPaymentControl.My.Resources.Resources.ic_delete_28x32
        Me.BtnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDelete.Location = New System.Drawing.Point(564, 143)
        Me.BtnDelete.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.BtnDelete.Size = New System.Drawing.Size(150, 45)
        Me.BtnDelete.TabIndex = 2
        Me.BtnDelete.Text = "&Eliminar"
        Me.BtnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'BtnModifyGroup
        '
        Me.BtnModifyGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnModifyGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_modify_28x32
        Me.BtnModifyGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnModifyGroup.Location = New System.Drawing.Point(564, 86)
        Me.BtnModifyGroup.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnModifyGroup.Name = "BtnModifyGroup"
        Me.BtnModifyGroup.Padding = New System.Windows.Forms.Padding(16, 0, 14, 0)
        Me.BtnModifyGroup.Size = New System.Drawing.Size(150, 45)
        Me.BtnModifyGroup.TabIndex = 1
        Me.BtnModifyGroup.Text = "&Modificar"
        Me.BtnModifyGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnModifyGroup.UseVisualStyleBackColor = True
        '
        'BtnNewGroup
        '
        Me.BtnNewGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_24x32
        Me.BtnNewGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnNewGroup.Location = New System.Drawing.Point(564, 29)
        Me.BtnNewGroup.Margin = New System.Windows.Forms.Padding(20, 0, 0, 0)
        Me.BtnNewGroup.Name = "BtnNewGroup"
        Me.BtnNewGroup.Padding = New System.Windows.Forms.Padding(22, 0, 22, 0)
        Me.BtnNewGroup.Size = New System.Drawing.Size(150, 45)
        Me.BtnNewGroup.TabIndex = 0
        Me.BtnNewGroup.Text = "&Nuevo"
        Me.BtnNewGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNewGroup.UseVisualStyleBackColor = True
        '
        'BtnSaveGroup
        '
        Me.BtnSaveGroup.Enabled = False
        Me.BtnSaveGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_save_28x28
        Me.BtnSaveGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSaveGroup.Location = New System.Drawing.Point(564, 29)
        Me.BtnSaveGroup.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnSaveGroup.Name = "BtnSaveGroup"
        Me.BtnSaveGroup.Padding = New System.Windows.Forms.Padding(18, 0, 16, 0)
        Me.BtnSaveGroup.Size = New System.Drawing.Size(150, 45)
        Me.BtnSaveGroup.TabIndex = 4
        Me.BtnSaveGroup.Text = "&Guardar"
        Me.BtnSaveGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSaveGroup.UseVisualStyleBackColor = True
        Me.BtnSaveGroup.Visible = False
        '
        'BtnUpdateGroup
        '
        Me.BtnUpdateGroup.Enabled = False
        Me.BtnUpdateGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdateGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_update_28x27
        Me.BtnUpdateGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdateGroup.Location = New System.Drawing.Point(564, 29)
        Me.BtnUpdateGroup.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnUpdateGroup.Name = "BtnUpdateGroup"
        Me.BtnUpdateGroup.Padding = New System.Windows.Forms.Padding(13, 0, 12, 0)
        Me.BtnUpdateGroup.Size = New System.Drawing.Size(150, 45)
        Me.BtnUpdateGroup.TabIndex = 5
        Me.BtnUpdateGroup.Text = "&Actualizar"
        Me.BtnUpdateGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUpdateGroup.UseVisualStyleBackColor = True
        Me.BtnUpdateGroup.Visible = False
        '
        'BtnDeleteGroup
        '
        Me.BtnDeleteGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_family_group_53x30
        Me.BtnDeleteGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteGroup.Location = New System.Drawing.Point(564, 29)
        Me.BtnDeleteGroup.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnDeleteGroup.Name = "BtnDeleteGroup"
        Me.BtnDeleteGroup.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.BtnDeleteGroup.Size = New System.Drawing.Size(150, 70)
        Me.BtnDeleteGroup.TabIndex = 6
        Me.BtnDeleteGroup.Text = "&Eliminar grupo"
        Me.BtnDeleteGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDeleteGroup.UseVisualStyleBackColor = True
        Me.BtnDeleteGroup.Visible = False
        '
        'ColIdGrupo
        '
        Me.ColIdGrupo.DataPropertyName = "id_grp"
        Me.ColIdGrupo.HeaderText = "ColIdGrupo"
        Me.ColIdGrupo.Name = "ColIdGrupo"
        Me.ColIdGrupo.ReadOnly = True
        Me.ColIdGrupo.Visible = False
        '
        'ColNomGrupo
        '
        Me.ColNomGrupo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColNomGrupo.DataPropertyName = "nom_grp"
        Me.ColNomGrupo.HeaderText = ""
        Me.ColNomGrupo.Name = "ColNomGrupo"
        Me.ColNomGrupo.ReadOnly = True
        Me.ColNomGrupo.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ColNomGrupo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColNomGrupo.Width = 414
        '
        'ColNumIntgrntes
        '
        Me.ColNumIntgrntes.DataPropertyName = "num_intgrntes_grp"
        Me.ColNumIntgrntes.HeaderText = "ColNumIntgrntes"
        Me.ColNumIntgrntes.Name = "ColNumIntgrntes"
        Me.ColNumIntgrntes.ReadOnly = True
        Me.ColNumIntgrntes.Visible = False
        '
        'FrmFamilyGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(743, 462)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnCerrar)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.PnlGrpFamiliar)
        Me.Controls.Add(Me.BtnNewGroup)
        Me.Controls.Add(Me.BtnSaveGroup)
        Me.Controls.Add(Me.BtnUpdateGroup)
        Me.Controls.Add(Me.BtnDeleteGroup)
        Me.Controls.Add(Me.BtnModifyGroup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFamilyGroup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AGREGAR, MODIFICAR O ELIMINAR UN GRUPO FAMILIAR"
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvSearchMembers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlGrpFamiliar.ResumeLayout(False)
        Me.PnlGrpFamiliar.PerformLayout()
        CType(Me.PicIntgrntes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvListOfMembers, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvListFamilyGroupNames, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnCancel As Button
    Friend WithEvents LblNumberMembers As Label
    Friend WithEvents NudNumberMembers As NumericUpDown
    Friend WithEvents TxtFamilyGroupName As TextBox
    Friend WithEvents LblBuscarIntgrntes As Label
    Friend WithEvents TxtSearchMembers As TextBox
    Friend WithEvents BtnCerrar As Button
    Friend WithEvents BtnDelete As Button
    Friend WithEvents DgvSearchMembers As DataGridView
    Friend WithEvents ColBidCli As DataGridViewTextBoxColumn
    Friend WithEvents ColBnomApeCli As DataGridViewTextBoxColumn
    Friend WithEvents ColIdGrpBscr As DataGridViewTextBoxColumn
    Friend WithEvents PnlGrpFamiliar As Panel
    Friend WithEvents LblIdGroup As Label
    Friend WithEvents ChkEmptyGroup As CheckBox
    Friend WithEvents LblIdClient As Label
    Friend WithEvents PicIntgrntes As PictureBox
    Friend WithEvents BtnRemoveMember As Button
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents Label1 As Label
    Friend WithEvents DgvListOfMembers As DataGridView
    Friend WithEvents ColIdCli As DataGridViewTextBoxColumn
    Friend WithEvents ColNomApeCli As DataGridViewTextBoxColumn
    Friend WithEvents ColIdGrp As DataGridViewTextBoxColumn
    Friend WithEvents LblNomGrupo As Label
    Friend WithEvents LblIntegrantes As Label
    Friend WithEvents DgvListFamilyGroupNames As DataGridView
    Friend WithEvents BtnUpdateGroup As Button
    Friend WithEvents BtnModifyGroup As Button
    Friend WithEvents BtnDeleteGroup As Button
    Friend WithEvents BtnNewGroup As Button
    Friend WithEvents BtnSaveGroup As Button
    Friend WithEvents ColIdGrupo As DataGridViewTextBoxColumn
    Friend WithEvents ColNomGrupo As DataGridViewTextBoxColumn
    Friend WithEvents ColNumIntgrntes As DataGridViewTextBoxColumn
End Class
