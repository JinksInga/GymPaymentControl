<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmListDebtors
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
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle18 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle19 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblInformacion = New System.Windows.Forms.Label()
        Me.StsStatusBar = New System.Windows.Forms.StatusStrip()
        Me.SlblTitle = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SlblMessage = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.PnlSearch = New System.Windows.Forms.Panel()
        Me.CmbFilter = New System.Windows.Forms.ComboBox()
        Me.BtnClean = New System.Windows.Forms.Button()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.LblFiltrar = New System.Windows.Forms.Label()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.LblErrorProvider = New System.Windows.Forms.Label()
        Me.RbPayIndividual = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RbPayGroup = New System.Windows.Forms.RadioButton()
        Me.DgvFamilyGroup = New System.Windows.Forms.DataGridView()
        Me.DgvIndividual = New System.Windows.Forms.DataGridView()
        Me.PbLogo = New System.Windows.Forms.PictureBox()
        Me.BtnPayMonth = New System.Windows.Forms.Button()
        Me.BtnPaymentGenerator = New System.Windows.Forms.Button()
        Me.LblSolitos = New System.Windows.Forms.Label()
        Me.LblToditos = New System.Windows.Forms.Label()
        Me.members = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nom_grp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fdi_pgs_gf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrcPgsGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DscPgsGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TtlPgsGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.daysOfMonthGrp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.APgrGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.empty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Apellido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextoEdad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MesAnio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrcPgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DscPgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.daysOfMonthInv = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.APagar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StsStatusBar.SuspendLayout()
        Me.PnlSearch.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.DgvFamilyGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DgvIndividual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PbLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblInformacion
        '
        Me.LblInformacion.AutoSize = True
        Me.LblInformacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInformacion.ForeColor = System.Drawing.Color.Brown
        Me.LblInformacion.Location = New System.Drawing.Point(115, 25)
        Me.LblInformacion.Margin = New System.Windows.Forms.Padding(0)
        Me.LblInformacion.Name = "LblInformacion"
        Me.LblInformacion.Size = New System.Drawing.Size(671, 18)
        Me.LblInformacion.TabIndex = 3
        Me.LblInformacion.Text = "La siguiente lista muestra los pagos pendientes. Selecciona un cliente de la list" &
    "a para realizar el pago."
        '
        'StsStatusBar
        '
        Me.StsStatusBar.AutoSize = False
        Me.StsStatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StsStatusBar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SlblTitle, Me.SlblMessage})
        Me.StsStatusBar.Location = New System.Drawing.Point(0, 809)
        Me.StsStatusBar.Name = "StsStatusBar"
        Me.StsStatusBar.Size = New System.Drawing.Size(1310, 46)
        Me.StsStatusBar.SizingGrip = False
        Me.StsStatusBar.TabIndex = 4
        Me.StsStatusBar.Text = "stsBarra"
        '
        'SlblTitle
        '
        Me.SlblTitle.AutoSize = False
        Me.SlblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.SlblTitle.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.SlblTitle.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.SlblTitle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SlblTitle.Margin = New System.Windows.Forms.Padding(24, 2, 8, 2)
        Me.SlblTitle.Name = "SlblTitle"
        Me.SlblTitle.Size = New System.Drawing.Size(140, 42)
        Me.SlblTitle.Text = "Nº de Registros"
        '
        'SlblMessage
        '
        Me.SlblMessage.AutoSize = False
        Me.SlblMessage.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.SlblMessage.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.SlblMessage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.SlblMessage.Margin = New System.Windows.Forms.Padding(8, 2, 0, 2)
        Me.SlblMessage.Name = "SlblMessage"
        Me.SlblMessage.Size = New System.Drawing.Size(904, 42)
        Me.SlblMessage.Text = " n Registros pendientes de pago."
        Me.SlblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'BtnClose
        '
        Me.BtnClose.BackColor = System.Drawing.SystemColors.Control
        Me.BtnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Brown
        Me.BtnClose.Image = Global.GymPaymentControl.My.Resources.Resources.ic_close_22x22
        Me.BtnClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnClose.Location = New System.Drawing.Point(1137, 607)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(0, 56, 0, 0)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnClose.Size = New System.Drawing.Size(140, 64)
        Me.BtnClose.TabIndex = 6
        Me.BtnClose.Text = "  &Cerrar ventana"
        Me.BtnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnClose.UseVisualStyleBackColor = False
        '
        'PnlSearch
        '
        Me.PnlSearch.AutoSize = True
        Me.PnlSearch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlSearch.Controls.Add(Me.CmbFilter)
        Me.PnlSearch.Controls.Add(Me.BtnClean)
        Me.PnlSearch.Controls.Add(Me.TxtSearch)
        Me.PnlSearch.Controls.Add(Me.LblFiltrar)
        Me.PnlSearch.Location = New System.Drawing.Point(118, 60)
        Me.PnlSearch.Margin = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.PnlSearch.Name = "PnlSearch"
        Me.PnlSearch.Size = New System.Drawing.Size(985, 42)
        Me.PnlSearch.TabIndex = 0
        '
        'CmbFilter
        '
        Me.CmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbFilter.ForeColor = System.Drawing.Color.MediumBlue
        Me.CmbFilter.FormattingEnabled = True
        Me.CmbFilter.IntegralHeight = False
        Me.CmbFilter.Items.AddRange(New Object() {"   NOMBRE", "   APELLIDO"})
        Me.CmbFilter.Location = New System.Drawing.Point(100, 7)
        Me.CmbFilter.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.CmbFilter.Name = "CmbFilter"
        Me.CmbFilter.Size = New System.Drawing.Size(150, 24)
        Me.CmbFilter.TabIndex = 1
        '
        'BtnClean
        '
        Me.BtnClean.AutoSize = True
        Me.BtnClean.FlatAppearance.BorderSize = 0
        Me.BtnClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClean.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BtnClean.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_c_28x28
        Me.BtnClean.Location = New System.Drawing.Point(940, -1)
        Me.BtnClean.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.BtnClean.Name = "BtnClean"
        Me.BtnClean.Size = New System.Drawing.Size(37, 37)
        Me.BtnClean.TabIndex = 2
        Me.BtnClean.UseVisualStyleBackColor = True
        '
        'TxtSearch
        '
        Me.TxtSearch.BackColor = System.Drawing.Color.Snow
        Me.TxtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearch.Font = New System.Drawing.Font("Linux Libertine Display G", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtSearch.Location = New System.Drawing.Point(259, 6)
        Me.TxtSearch.Margin = New System.Windows.Forms.Padding(8, 6, 4, 6)
        Me.TxtSearch.MaxLength = 30
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(677, 25)
        Me.TxtSearch.TabIndex = 0
        Me.TxtSearch.WordWrap = False
        '
        'LblFiltrar
        '
        Me.LblFiltrar.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LblFiltrar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFiltrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFiltrar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFiltrar.Location = New System.Drawing.Point(8, 6)
        Me.LblFiltrar.Margin = New System.Windows.Forms.Padding(8, 6, 0, 6)
        Me.LblFiltrar.Name = "LblFiltrar"
        Me.LblFiltrar.Size = New System.Drawing.Size(243, 26)
        Me.LblFiltrar.TabIndex = 3
        Me.LblFiltrar.Text = "  Filtrar por"
        Me.LblFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'LblErrorProvider
        '
        Me.LblErrorProvider.AutoSize = True
        Me.LblErrorProvider.Location = New System.Drawing.Point(1106, 200)
        Me.LblErrorProvider.Name = "LblErrorProvider"
        Me.LblErrorProvider.Size = New System.Drawing.Size(167, 13)
        Me.LblErrorProvider.TabIndex = 87
        Me.LblErrorProvider.Text = "1078; 536 Location Lbl 1106; 200"
        '
        'RbPayIndividual
        '
        Me.RbPayIndividual.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbPayIndividual.Checked = True
        Me.RbPayIndividual.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbPayIndividual.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbPayIndividual.Location = New System.Drawing.Point(0, 0)
        Me.RbPayIndividual.Margin = New System.Windows.Forms.Padding(0)
        Me.RbPayIndividual.Name = "RbPayIndividual"
        Me.RbPayIndividual.Size = New System.Drawing.Size(550, 32)
        Me.RbPayIndividual.TabIndex = 0
        Me.RbPayIndividual.TabStop = True
        Me.RbPayIndividual.Text = "Pagos individuales"
        Me.RbPayIndividual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RbPayIndividual.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.RbPayIndividual)
        Me.Panel1.Controls.Add(Me.RbPayGroup)
        Me.Panel1.Location = New System.Drawing.Point(25, 118)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(16, 16, 0, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1078, 36)
        Me.Panel1.TabIndex = 1
        '
        'RbPayGroup
        '
        Me.RbPayGroup.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbPayGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbPayGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbPayGroup.Location = New System.Drawing.Point(524, 0)
        Me.RbPayGroup.Margin = New System.Windows.Forms.Padding(0)
        Me.RbPayGroup.Name = "RbPayGroup"
        Me.RbPayGroup.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbPayGroup.Size = New System.Drawing.Size(550, 32)
        Me.RbPayGroup.TabIndex = 1
        Me.RbPayGroup.Text = "Pagos grupales"
        Me.RbPayGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RbPayGroup.UseVisualStyleBackColor = True
        '
        'DgvFamilyGroup
        '
        Me.DgvFamilyGroup.AllowUserToAddRows = False
        Me.DgvFamilyGroup.AllowUserToDeleteRows = False
        Me.DgvFamilyGroup.AllowUserToResizeColumns = False
        Me.DgvFamilyGroup.AllowUserToResizeRows = False
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Linux Libertine Display G", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvFamilyGroup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DgvFamilyGroup.ColumnHeadersHeight = 32
        Me.DgvFamilyGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvFamilyGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.members, Me.nom_grp, Me.fdi_pgs_gf, Me.PrcPgsGf, Me.DscPgsGf, Me.TtlPgsGf, Me.daysOfMonthGrp, Me.APgrGf, Me.empty})
        DataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle18.BackColor = System.Drawing.Color.MistyRose
        DataGridViewCellStyle18.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle18.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.HotTrack
        DataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvFamilyGroup.DefaultCellStyle = DataGridViewCellStyle18
        Me.DgvFamilyGroup.Location = New System.Drawing.Point(25, 156)
        Me.DgvFamilyGroup.Margin = New System.Windows.Forms.Padding(16, 4, 0, 16)
        Me.DgvFamilyGroup.MultiSelect = False
        Me.DgvFamilyGroup.Name = "DgvFamilyGroup"
        Me.DgvFamilyGroup.ReadOnly = True
        DataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvFamilyGroup.RowHeadersDefaultCellStyle = DataGridViewCellStyle19
        Me.DgvFamilyGroup.RowHeadersWidth = 4
        Me.DgvFamilyGroup.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvFamilyGroup.RowTemplate.Height = 24
        Me.DgvFamilyGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvFamilyGroup.Size = New System.Drawing.Size(1078, 536)
        Me.DgvFamilyGroup.TabIndex = 3
        '
        'DgvIndividual
        '
        Me.DgvIndividual.AllowUserToAddRows = False
        Me.DgvIndividual.AllowUserToDeleteRows = False
        Me.DgvIndividual.AllowUserToResizeColumns = False
        Me.DgvIndividual.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Linux Libertine Display G", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvIndividual.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvIndividual.ColumnHeadersHeight = 32
        Me.DgvIndividual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvIndividual.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre, Me.Apellido, Me.TextoEdad, Me.MesAnio, Me.PrcPgs, Me.DscPgs, Me.Total, Me.daysOfMonthInv, Me.APagar, Me.Column})
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.MistyRose
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle9.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.HotTrack
        DataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvIndividual.DefaultCellStyle = DataGridViewCellStyle9
        Me.DgvIndividual.Location = New System.Drawing.Point(25, 156)
        Me.DgvIndividual.Margin = New System.Windows.Forms.Padding(16, 4, 0, 16)
        Me.DgvIndividual.MultiSelect = False
        Me.DgvIndividual.Name = "DgvIndividual"
        Me.DgvIndividual.ReadOnly = True
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvIndividual.RowHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DgvIndividual.RowHeadersWidth = 4
        Me.DgvIndividual.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvIndividual.RowTemplate.Height = 24
        Me.DgvIndividual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvIndividual.Size = New System.Drawing.Size(1078, 536)
        Me.DgvIndividual.TabIndex = 2
        '
        'PbLogo
        '
        Me.PbLogo.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cash_register_60x60
        Me.PbLogo.Location = New System.Drawing.Point(25, 25)
        Me.PbLogo.Margin = New System.Windows.Forms.Padding(16, 16, 0, 0)
        Me.PbLogo.Name = "PbLogo"
        Me.PbLogo.Size = New System.Drawing.Size(77, 77)
        Me.PbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PbLogo.TabIndex = 68
        Me.PbLogo.TabStop = False
        '
        'BtnPayMonth
        '
        Me.BtnPayMonth.Enabled = False
        Me.BtnPayMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnPayMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPayMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPayMonth.ForeColor = System.Drawing.Color.DarkGreen
        Me.BtnPayMonth.Image = Global.GymPaymentControl.My.Resources.Resources.ic_pay_month_28x32
        Me.BtnPayMonth.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPayMonth.Location = New System.Drawing.Point(1127, 189)
        Me.BtnPayMonth.Margin = New System.Windows.Forms.Padding(24, 0, 24, 0)
        Me.BtnPayMonth.Name = "BtnPayMonth"
        Me.BtnPayMonth.Size = New System.Drawing.Size(140, 64)
        Me.BtnPayMonth.TabIndex = 4
        Me.BtnPayMonth.Text = "&Pagar mes"
        Me.BtnPayMonth.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPayMonth.UseVisualStyleBackColor = True
        '
        'BtnPaymentGenerator
        '
        Me.BtnPaymentGenerator.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnPaymentGenerator.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPaymentGenerator.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPaymentGenerator.ForeColor = System.Drawing.Color.DarkGreen
        Me.BtnPaymentGenerator.Image = Global.GymPaymentControl.My.Resources.Resources.ic_pay_month_28x32
        Me.BtnPaymentGenerator.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPaymentGenerator.Location = New System.Drawing.Point(1127, 60)
        Me.BtnPaymentGenerator.Margin = New System.Windows.Forms.Padding(24, 0, 24, 0)
        Me.BtnPaymentGenerator.Name = "BtnPaymentGenerator"
        Me.BtnPaymentGenerator.Size = New System.Drawing.Size(140, 92)
        Me.BtnPaymentGenerator.TabIndex = 5
        Me.BtnPaymentGenerator.Text = "&Nuevos pagos mensuales"
        Me.BtnPaymentGenerator.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPaymentGenerator.UseVisualStyleBackColor = True
        '
        'LblSolitos
        '
        Me.LblSolitos.AutoSize = True
        Me.LblSolitos.Location = New System.Drawing.Point(24, 708)
        Me.LblSolitos.Name = "LblSolitos"
        Me.LblSolitos.Size = New System.Drawing.Size(52, 13)
        Me.LblSolitos.TabIndex = 94
        Me.LblSolitos.Text = "LblSolitos"
        '
        'LblToditos
        '
        Me.LblToditos.AutoSize = True
        Me.LblToditos.Location = New System.Drawing.Point(24, 730)
        Me.LblToditos.Name = "LblToditos"
        Me.LblToditos.Size = New System.Drawing.Size(56, 13)
        Me.LblToditos.TabIndex = 95
        Me.LblToditos.Text = "LblToditos"
        '
        'members
        '
        Me.members.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.members.DataPropertyName = "Members"
        Me.members.HeaderText = "INTEGRANTES"
        Me.members.Name = "members"
        Me.members.ReadOnly = True
        Me.members.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.members.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.members.Width = 235
        '
        'nom_grp
        '
        Me.nom_grp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.nom_grp.DataPropertyName = "GroupName"
        Me.nom_grp.HeaderText = "NOMBRE DEL GRUPO"
        Me.nom_grp.Name = "nom_grp"
        Me.nom_grp.ReadOnly = True
        Me.nom_grp.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.nom_grp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.nom_grp.Width = 230
        '
        'fdi_pgs_gf
        '
        Me.fdi_pgs_gf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.fdi_pgs_gf.DataPropertyName = "LongDate"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.fdi_pgs_gf.DefaultCellStyle = DataGridViewCellStyle12
        Me.fdi_pgs_gf.HeaderText = "MES Y AÑO"
        Me.fdi_pgs_gf.Name = "fdi_pgs_gf"
        Me.fdi_pgs_gf.ReadOnly = True
        Me.fdi_pgs_gf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.fdi_pgs_gf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fdi_pgs_gf.Width = 135
        '
        'PrcPgsGf
        '
        Me.PrcPgsGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.PrcPgsGf.DataPropertyName = "PrcPgs"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrcPgsGf.DefaultCellStyle = DataGridViewCellStyle13
        Me.PrcPgsGf.HeaderText = "PRECIO"
        Me.PrcPgsGf.Name = "PrcPgsGf"
        Me.PrcPgsGf.ReadOnly = True
        Me.PrcPgsGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PrcPgsGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PrcPgsGf.Width = 90
        '
        'DscPgsGf
        '
        Me.DscPgsGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DscPgsGf.DataPropertyName = "DscPgs"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DscPgsGf.DefaultCellStyle = DataGridViewCellStyle14
        Me.DscPgsGf.HeaderText = "DSCNTO"
        Me.DscPgsGf.Name = "DscPgsGf"
        Me.DscPgsGf.ReadOnly = True
        Me.DscPgsGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DscPgsGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DscPgsGf.Width = 90
        '
        'TtlPgsGf
        '
        Me.TtlPgsGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.TtlPgsGf.DataPropertyName = "Total"
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.TtlPgsGf.DefaultCellStyle = DataGridViewCellStyle15
        Me.TtlPgsGf.HeaderText = "TOTAL"
        Me.TtlPgsGf.Name = "TtlPgsGf"
        Me.TtlPgsGf.ReadOnly = True
        Me.TtlPgsGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TtlPgsGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TtlPgsGf.Width = 90
        '
        'daysOfMonthGrp
        '
        Me.daysOfMonthGrp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.daysOfMonthGrp.DataPropertyName = "DaysOfMonth"
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.daysOfMonthGrp.DefaultCellStyle = DataGridViewCellStyle16
        Me.daysOfMonthGrp.HeaderText = "Nº DE DIAS"
        Me.daysOfMonthGrp.Name = "daysOfMonthGrp"
        Me.daysOfMonthGrp.ReadOnly = True
        Me.daysOfMonthGrp.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.daysOfMonthGrp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.daysOfMonthGrp.Width = 90
        '
        'APgrGf
        '
        Me.APgrGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.APgrGf.DataPropertyName = "TotalToPay"
        DataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.APgrGf.DefaultCellStyle = DataGridViewCellStyle17
        Me.APgrGf.HeaderText = "A PAGAR"
        Me.APgrGf.Name = "APgrGf"
        Me.APgrGf.ReadOnly = True
        Me.APgrGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.APgrGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.APgrGf.Width = 90
        '
        'empty
        '
        Me.empty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.empty.HeaderText = ""
        Me.empty.MinimumWidth = 4
        Me.empty.Name = "empty"
        Me.empty.ReadOnly = True
        Me.empty.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.empty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.empty.Width = 4
        '
        'Nombre
        '
        Me.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Nombre.DataPropertyName = "Name"
        Me.Nombre.HeaderText = "NOMBRE"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        Me.Nombre.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nombre.Width = 185
        '
        'Apellido
        '
        Me.Apellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Apellido.DataPropertyName = "LastName"
        Me.Apellido.HeaderText = "APELLIDO"
        Me.Apellido.Name = "Apellido"
        Me.Apellido.ReadOnly = True
        Me.Apellido.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Apellido.Width = 185
        '
        'TextoEdad
        '
        Me.TextoEdad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.TextoEdad.DataPropertyName = "Age"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.TextoEdad.DefaultCellStyle = DataGridViewCellStyle2
        Me.TextoEdad.HeaderText = "EDAD"
        Me.TextoEdad.Name = "TextoEdad"
        Me.TextoEdad.ReadOnly = True
        Me.TextoEdad.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TextoEdad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TextoEdad.Width = 95
        '
        'MesAnio
        '
        Me.MesAnio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.MesAnio.DataPropertyName = "LongDate"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.MesAnio.DefaultCellStyle = DataGridViewCellStyle3
        Me.MesAnio.HeaderText = "MES Y AÑO"
        Me.MesAnio.Name = "MesAnio"
        Me.MesAnio.ReadOnly = True
        Me.MesAnio.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MesAnio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MesAnio.Width = 135
        '
        'PrcPgs
        '
        Me.PrcPgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.PrcPgs.DataPropertyName = "PrcPgs"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrcPgs.DefaultCellStyle = DataGridViewCellStyle4
        Me.PrcPgs.HeaderText = "PRECIO"
        Me.PrcPgs.Name = "PrcPgs"
        Me.PrcPgs.ReadOnly = True
        Me.PrcPgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PrcPgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PrcPgs.Width = 90
        '
        'DscPgs
        '
        Me.DscPgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DscPgs.DataPropertyName = "DscPgs"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DscPgs.DefaultCellStyle = DataGridViewCellStyle5
        Me.DscPgs.HeaderText = "DSCNTO"
        Me.DscPgs.Name = "DscPgs"
        Me.DscPgs.ReadOnly = True
        Me.DscPgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DscPgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DscPgs.Width = 90
        '
        'Total
        '
        Me.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Total.DataPropertyName = "Total"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Total.DefaultCellStyle = DataGridViewCellStyle6
        Me.Total.HeaderText = "TOTAL"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        Me.Total.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Total.Width = 90
        '
        'daysOfMonthInv
        '
        Me.daysOfMonthInv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.daysOfMonthInv.DataPropertyName = "DaysOfMonth"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.daysOfMonthInv.DefaultCellStyle = DataGridViewCellStyle7
        Me.daysOfMonthInv.HeaderText = "Nº DE DIAS"
        Me.daysOfMonthInv.Name = "daysOfMonthInv"
        Me.daysOfMonthInv.ReadOnly = True
        Me.daysOfMonthInv.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.daysOfMonthInv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.daysOfMonthInv.Width = 90
        '
        'APagar
        '
        Me.APagar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.APagar.DataPropertyName = "TotalToPay"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.APagar.DefaultCellStyle = DataGridViewCellStyle8
        Me.APagar.HeaderText = "A PAGAR"
        Me.APagar.Name = "APagar"
        Me.APagar.ReadOnly = True
        Me.APagar.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.APagar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.APagar.Width = 90
        '
        'Column
        '
        Me.Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Column.HeaderText = ""
        Me.Column.MinimumWidth = 4
        Me.Column.Name = "Column"
        Me.Column.ReadOnly = True
        Me.Column.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Column.Width = 4
        '
        'FrmListDebtors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1310, 855)
        Me.Controls.Add(Me.BtnPayMonth)
        Me.Controls.Add(Me.LblToditos)
        Me.Controls.Add(Me.LblSolitos)
        Me.Controls.Add(Me.BtnPaymentGenerator)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LblErrorProvider)
        Me.Controls.Add(Me.PnlSearch)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.LblInformacion)
        Me.Controls.Add(Me.PbLogo)
        Me.Controls.Add(Me.StsStatusBar)
        Me.Controls.Add(Me.DgvIndividual)
        Me.Controls.Add(Me.DgvFamilyGroup)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Location = New System.Drawing.Point(20, 20)
        Me.MaximizeBox = False
        Me.Name = "FrmListDebtors"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Lista de pagos pendientes"
        Me.StsStatusBar.ResumeLayout(False)
        Me.StsStatusBar.PerformLayout()
        Me.PnlSearch.ResumeLayout(False)
        Me.PnlSearch.PerformLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.DgvFamilyGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DgvIndividual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PbLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnClose As Button
    Friend WithEvents PbLogo As PictureBox
    Friend WithEvents LblInformacion As Label
    Friend WithEvents BtnPayMonth As Button
    Friend WithEvents StsStatusBar As StatusStrip
    Friend WithEvents SlblTitle As ToolStripStatusLabel
    Friend WithEvents SlblMessage As ToolStripStatusLabel
    Friend WithEvents PnlSearch As Panel
    Friend WithEvents CmbFilter As ComboBox
    Friend WithEvents BtnClean As Button
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents LblFiltrar As Label
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents LblErrorProvider As Label
    Friend WithEvents RbPayIndividual As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RbPayGroup As RadioButton
    Friend WithEvents DgvFamilyGroup As DataGridView
    Friend WithEvents DgvIndividual As DataGridView
    Friend WithEvents BtnPaymentGenerator As Button
    Friend WithEvents LblToditos As Label
    Friend WithEvents LblSolitos As Label
    Friend WithEvents members As DataGridViewTextBoxColumn
    Friend WithEvents nom_grp As DataGridViewTextBoxColumn
    Friend WithEvents fdi_pgs_gf As DataGridViewTextBoxColumn
    Friend WithEvents PrcPgsGf As DataGridViewTextBoxColumn
    Friend WithEvents DscPgsGf As DataGridViewTextBoxColumn
    Friend WithEvents TtlPgsGf As DataGridViewTextBoxColumn
    Friend WithEvents daysOfMonthGrp As DataGridViewTextBoxColumn
    Friend WithEvents APgrGf As DataGridViewTextBoxColumn
    Friend WithEvents empty As DataGridViewTextBoxColumn
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Apellido As DataGridViewTextBoxColumn
    Friend WithEvents TextoEdad As DataGridViewTextBoxColumn
    Friend WithEvents MesAnio As DataGridViewTextBoxColumn
    Friend WithEvents PrcPgs As DataGridViewTextBoxColumn
    Friend WithEvents DscPgs As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents daysOfMonthInv As DataGridViewTextBoxColumn
    Friend WithEvents APagar As DataGridViewTextBoxColumn
    Friend WithEvents Column As DataGridViewTextBoxColumn
End Class
