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
        Dim DataGridViewCellStyle20 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle27 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle28 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle21 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle22 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle23 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle24 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle25 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle26 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle29 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle37 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle38 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle30 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle31 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle32 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle33 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle34 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle35 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle36 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.Label = New System.Windows.Forms.Label()
        Me.RbPayIndividual = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RbPayGroup = New System.Windows.Forms.RadioButton()
        Me.DgvFamilyGroup = New System.Windows.Forms.DataGridView()
        Me.nom_cli_gf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nom_grp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fdi_pgs_gf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrcPgsGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DscPgsGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TtlPgsGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumDiasGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.APgrGf = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.empty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvIndividual = New System.Windows.Forms.DataGridView()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Apellido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TextoEdad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MesAnio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrcPgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DscPgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Total = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DiasMes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.APagar = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fdi_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.id_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.cliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PbLogo = New System.Windows.Forms.PictureBox()
        Me.BtnPayMonth = New System.Windows.Forms.Button()
        Me.BtnPaymentGenerator = New System.Windows.Forms.Button()
        Me.LblSolitos = New System.Windows.Forms.Label()
        Me.LblToditos = New System.Windows.Forms.Label()
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
        Me.StsStatusBar.Location = New System.Drawing.Point(0, 713)
        Me.StsStatusBar.Name = "StsStatusBar"
        Me.StsStatusBar.Size = New System.Drawing.Size(1252, 46)
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
        Me.BtnClose.Location = New System.Drawing.Point(1103, 133)
        Me.BtnClose.Margin = New System.Windows.Forms.Padding(0, 56, 0, 0)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnClose.Size = New System.Drawing.Size(140, 64)
        Me.BtnClose.TabIndex = 3
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
        Me.PnlSearch.Size = New System.Drawing.Size(959, 42)
        Me.PnlSearch.TabIndex = 85
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
        Me.CmbFilter.Size = New System.Drawing.Size(136, 24)
        Me.CmbFilter.TabIndex = 82
        '
        'BtnClean
        '
        Me.BtnClean.AutoSize = True
        Me.BtnClean.FlatAppearance.BorderSize = 0
        Me.BtnClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClean.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BtnClean.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_c_28x28
        Me.BtnClean.Location = New System.Drawing.Point(914, 0)
        Me.BtnClean.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.BtnClean.Name = "BtnClean"
        Me.BtnClean.Size = New System.Drawing.Size(37, 37)
        Me.BtnClean.TabIndex = 85
        Me.BtnClean.UseVisualStyleBackColor = True
        '
        'TxtSearch
        '
        Me.TxtSearch.BackColor = System.Drawing.Color.Snow
        Me.TxtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtSearch.Font = New System.Drawing.Font("Linux Libertine Display G", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSearch.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtSearch.Location = New System.Drawing.Point(241, 6)
        Me.TxtSearch.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.TxtSearch.MaxLength = 30
        Me.TxtSearch.Name = "TxtSearch"
        Me.TxtSearch.Size = New System.Drawing.Size(669, 25)
        Me.TxtSearch.TabIndex = 84
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
        Me.LblFiltrar.Size = New System.Drawing.Size(229, 26)
        Me.LblFiltrar.TabIndex = 88
        Me.LblFiltrar.Text = "  Filtrar por"
        Me.LblFiltrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Location = New System.Drawing.Point(1022, 9)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(55, 13)
        Me.Label.TabIndex = 87
        Me.Label.Text = "1052; 536"
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
        Me.RbPayIndividual.Size = New System.Drawing.Size(544, 32)
        Me.RbPayIndividual.TabIndex = 88
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
        Me.Panel1.Size = New System.Drawing.Size(1054, 36)
        Me.Panel1.TabIndex = 90
        '
        'RbPayGroup
        '
        Me.RbPayGroup.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbPayGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbPayGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbPayGroup.Location = New System.Drawing.Point(506, 0)
        Me.RbPayGroup.Margin = New System.Windows.Forms.Padding(0)
        Me.RbPayGroup.Name = "RbPayGroup"
        Me.RbPayGroup.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbPayGroup.Size = New System.Drawing.Size(544, 32)
        Me.RbPayGroup.TabIndex = 91
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
        DataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle20.Font = New System.Drawing.Font("Linux Libertine Display G", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvFamilyGroup.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle20
        Me.DgvFamilyGroup.ColumnHeadersHeight = 32
        Me.DgvFamilyGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvFamilyGroup.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.nom_cli_gf, Me.nom_grp, Me.fdi_pgs_gf, Me.PrcPgsGf, Me.DscPgsGf, Me.TtlPgsGf, Me.NumDiasGf, Me.APgrGf, Me.empty})
        DataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle27.BackColor = System.Drawing.Color.MistyRose
        DataGridViewCellStyle27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle27.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.HotTrack
        DataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvFamilyGroup.DefaultCellStyle = DataGridViewCellStyle27
        Me.DgvFamilyGroup.Location = New System.Drawing.Point(25, 436)
        Me.DgvFamilyGroup.Margin = New System.Windows.Forms.Padding(16, 0, 0, 16)
        Me.DgvFamilyGroup.MultiSelect = False
        Me.DgvFamilyGroup.Name = "DgvFamilyGroup"
        Me.DgvFamilyGroup.ReadOnly = True
        DataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle28.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvFamilyGroup.RowHeadersDefaultCellStyle = DataGridViewCellStyle28
        Me.DgvFamilyGroup.RowHeadersWidth = 4
        Me.DgvFamilyGroup.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvFamilyGroup.RowTemplate.Height = 24
        Me.DgvFamilyGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvFamilyGroup.Size = New System.Drawing.Size(1052, 250)
        Me.DgvFamilyGroup.TabIndex = 91
        '
        'nom_cli_gf
        '
        Me.nom_cli_gf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.nom_cli_gf.DataPropertyName = "Integrantes"
        Me.nom_cli_gf.HeaderText = "INTEGRANTES"
        Me.nom_cli_gf.Name = "nom_cli_gf"
        Me.nom_cli_gf.ReadOnly = True
        Me.nom_cli_gf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.nom_cli_gf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.nom_cli_gf.Width = 184
        '
        'nom_grp
        '
        Me.nom_grp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.nom_grp.DataPropertyName = "NombreGrupo"
        Me.nom_grp.HeaderText = "NOMBRE DEL GRUPO"
        Me.nom_grp.Name = "nom_grp"
        Me.nom_grp.ReadOnly = True
        Me.nom_grp.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.nom_grp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.nom_grp.Width = 272
        '
        'fdi_pgs_gf
        '
        Me.fdi_pgs_gf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.fdi_pgs_gf.DataPropertyName = "MesAnio"
        DataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.fdi_pgs_gf.DefaultCellStyle = DataGridViewCellStyle21
        Me.fdi_pgs_gf.HeaderText = "MES Y AÑO"
        Me.fdi_pgs_gf.Name = "fdi_pgs_gf"
        Me.fdi_pgs_gf.ReadOnly = True
        Me.fdi_pgs_gf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.fdi_pgs_gf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fdi_pgs_gf.Width = 128
        '
        'PrcPgsGf
        '
        Me.PrcPgsGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.PrcPgsGf.DataPropertyName = "PrcPgs"
        DataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrcPgsGf.DefaultCellStyle = DataGridViewCellStyle22
        Me.PrcPgsGf.HeaderText = "PRECIO"
        Me.PrcPgsGf.Name = "PrcPgsGf"
        Me.PrcPgsGf.ReadOnly = True
        Me.PrcPgsGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PrcPgsGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PrcPgsGf.Width = 88
        '
        'DscPgsGf
        '
        Me.DscPgsGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DscPgsGf.DataPropertyName = "DscPgs"
        DataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DscPgsGf.DefaultCellStyle = DataGridViewCellStyle23
        Me.DscPgsGf.HeaderText = "DSCNTO"
        Me.DscPgsGf.Name = "DscPgsGf"
        Me.DscPgsGf.ReadOnly = True
        Me.DscPgsGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DscPgsGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DscPgsGf.Width = 88
        '
        'TtlPgsGf
        '
        Me.TtlPgsGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.TtlPgsGf.DataPropertyName = "Total"
        DataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.TtlPgsGf.DefaultCellStyle = DataGridViewCellStyle24
        Me.TtlPgsGf.HeaderText = "TOTAL"
        Me.TtlPgsGf.Name = "TtlPgsGf"
        Me.TtlPgsGf.ReadOnly = True
        Me.TtlPgsGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TtlPgsGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TtlPgsGf.Width = 88
        '
        'NumDiasGf
        '
        Me.NumDiasGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.NumDiasGf.DataPropertyName = "DiasMes"
        DataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.NumDiasGf.DefaultCellStyle = DataGridViewCellStyle25
        Me.NumDiasGf.HeaderText = "Nº DE DIAS"
        Me.NumDiasGf.Name = "NumDiasGf"
        Me.NumDiasGf.ReadOnly = True
        Me.NumDiasGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.NumDiasGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.NumDiasGf.Width = 88
        '
        'APgrGf
        '
        Me.APgrGf.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.APgrGf.DataPropertyName = "APagar"
        DataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.APgrGf.DefaultCellStyle = DataGridViewCellStyle26
        Me.APgrGf.HeaderText = "A PAGAR"
        Me.APgrGf.Name = "APgrGf"
        Me.APgrGf.ReadOnly = True
        Me.APgrGf.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.APgrGf.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.APgrGf.Width = 88
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
        'DgvIndividual
        '
        Me.DgvIndividual.AllowUserToAddRows = False
        Me.DgvIndividual.AllowUserToDeleteRows = False
        Me.DgvIndividual.AllowUserToResizeColumns = False
        Me.DgvIndividual.AllowUserToResizeRows = False
        DataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle29.Font = New System.Drawing.Font("Linux Libertine Display G", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvIndividual.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle29
        Me.DgvIndividual.ColumnHeadersHeight = 32
        Me.DgvIndividual.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvIndividual.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Nombre, Me.Apellido, Me.TextoEdad, Me.MesAnio, Me.PrcPgs, Me.DscPgs, Me.Total, Me.DiasMes, Me.APagar, Me.Column, Me.fdi_pgs, Me.id_pgs, Me.cliente})
        DataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle37.BackColor = System.Drawing.Color.MistyRose
        DataGridViewCellStyle37.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle37.ForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle37.SelectionBackColor = System.Drawing.SystemColors.HotTrack
        DataGridViewCellStyle37.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvIndividual.DefaultCellStyle = DataGridViewCellStyle37
        Me.DgvIndividual.Location = New System.Drawing.Point(27, 170)
        Me.DgvIndividual.Margin = New System.Windows.Forms.Padding(16, 0, 0, 16)
        Me.DgvIndividual.MultiSelect = False
        Me.DgvIndividual.Name = "DgvIndividual"
        Me.DgvIndividual.ReadOnly = True
        DataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle38.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        DataGridViewCellStyle38.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption
        DataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.Navy
        DataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvIndividual.RowHeadersDefaultCellStyle = DataGridViewCellStyle38
        Me.DgvIndividual.RowHeadersWidth = 4
        Me.DgvIndividual.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvIndividual.RowTemplate.Height = 24
        Me.DgvIndividual.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvIndividual.Size = New System.Drawing.Size(1052, 250)
        Me.DgvIndividual.TabIndex = 2
        '
        'Nombre
        '
        Me.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Nombre.DataPropertyName = "Nombre"
        Me.Nombre.HeaderText = "NOMBRE"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        Me.Nombre.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Nombre.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Nombre.Width = 184
        '
        'Apellido
        '
        Me.Apellido.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Apellido.DataPropertyName = "Apellido"
        Me.Apellido.HeaderText = "APELLIDO"
        Me.Apellido.Name = "Apellido"
        Me.Apellido.ReadOnly = True
        Me.Apellido.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Apellido.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Apellido.Width = 184
        '
        'TextoEdad
        '
        Me.TextoEdad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.TextoEdad.DataPropertyName = "Edad"
        DataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.TextoEdad.DefaultCellStyle = DataGridViewCellStyle30
        Me.TextoEdad.HeaderText = "EDAD"
        Me.TextoEdad.Name = "TextoEdad"
        Me.TextoEdad.ReadOnly = True
        Me.TextoEdad.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TextoEdad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TextoEdad.Width = 88
        '
        'MesAnio
        '
        Me.MesAnio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.MesAnio.DataPropertyName = "MesAnio"
        DataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.MesAnio.DefaultCellStyle = DataGridViewCellStyle31
        Me.MesAnio.HeaderText = "MES Y AÑO"
        Me.MesAnio.Name = "MesAnio"
        Me.MesAnio.ReadOnly = True
        Me.MesAnio.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.MesAnio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.MesAnio.Width = 128
        '
        'PrcPgs
        '
        Me.PrcPgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.PrcPgs.DataPropertyName = "PrcPgs"
        DataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.PrcPgs.DefaultCellStyle = DataGridViewCellStyle32
        Me.PrcPgs.HeaderText = "PRECIO"
        Me.PrcPgs.Name = "PrcPgs"
        Me.PrcPgs.ReadOnly = True
        Me.PrcPgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PrcPgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.PrcPgs.Width = 88
        '
        'DscPgs
        '
        Me.DscPgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DscPgs.DataPropertyName = "DscPgs"
        DataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.DscPgs.DefaultCellStyle = DataGridViewCellStyle33
        Me.DscPgs.HeaderText = "DSCNTO"
        Me.DscPgs.Name = "DscPgs"
        Me.DscPgs.ReadOnly = True
        Me.DscPgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DscPgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DscPgs.Width = 88
        '
        'Total
        '
        Me.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Total.DataPropertyName = "Total"
        DataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.Total.DefaultCellStyle = DataGridViewCellStyle34
        Me.Total.HeaderText = "TOTAL"
        Me.Total.Name = "Total"
        Me.Total.ReadOnly = True
        Me.Total.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Total.Width = 88
        '
        'DiasMes
        '
        Me.DiasMes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DiasMes.DataPropertyName = "DiasMes"
        DataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DiasMes.DefaultCellStyle = DataGridViewCellStyle35
        Me.DiasMes.HeaderText = "Nº DE DIAS"
        Me.DiasMes.Name = "DiasMes"
        Me.DiasMes.ReadOnly = True
        Me.DiasMes.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DiasMes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DiasMes.Width = 88
        '
        'APagar
        '
        Me.APagar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.APagar.DataPropertyName = "APagar"
        DataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.APagar.DefaultCellStyle = DataGridViewCellStyle36
        Me.APagar.HeaderText = "A PAGAR"
        Me.APagar.Name = "APagar"
        Me.APagar.ReadOnly = True
        Me.APagar.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.APagar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.APagar.Width = 88
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
        'fdi_pgs
        '
        Me.fdi_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.fdi_pgs.HeaderText = "fdi_pgs"
        Me.fdi_pgs.Name = "fdi_pgs"
        Me.fdi_pgs.ReadOnly = True
        Me.fdi_pgs.Visible = False
        Me.fdi_pgs.Width = 10
        '
        'id_pgs
        '
        Me.id_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.id_pgs.HeaderText = "id_pgs"
        Me.id_pgs.Name = "id_pgs"
        Me.id_pgs.ReadOnly = True
        Me.id_pgs.Visible = False
        Me.id_pgs.Width = 10
        '
        'cliente
        '
        Me.cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.cliente.HeaderText = "cliente"
        Me.cliente.Name = "cliente"
        Me.cliente.ReadOnly = True
        Me.cliente.Visible = False
        Me.cliente.Width = 10
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
        Me.BtnPayMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnPayMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPayMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPayMonth.ForeColor = System.Drawing.Color.DarkGreen
        Me.BtnPayMonth.Image = Global.GymPaymentControl.My.Resources.Resources.ic_pay_month_28x32
        Me.BtnPayMonth.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPayMonth.Location = New System.Drawing.Point(1103, 60)
        Me.BtnPayMonth.Margin = New System.Windows.Forms.Padding(24, 0, 24, 0)
        Me.BtnPayMonth.Name = "BtnPayMonth"
        Me.BtnPayMonth.Size = New System.Drawing.Size(140, 64)
        Me.BtnPayMonth.TabIndex = 1
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
        Me.BtnPaymentGenerator.Location = New System.Drawing.Point(1103, 204)
        Me.BtnPaymentGenerator.Margin = New System.Windows.Forms.Padding(24, 0, 24, 0)
        Me.BtnPaymentGenerator.Name = "BtnPaymentGenerator"
        Me.BtnPaymentGenerator.Size = New System.Drawing.Size(140, 64)
        Me.BtnPaymentGenerator.TabIndex = 93
        Me.BtnPaymentGenerator.Text = "&Nuevos pagos mensuales"
        Me.BtnPaymentGenerator.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPaymentGenerator.UseVisualStyleBackColor = True
        '
        'LblSolitos
        '
        Me.LblSolitos.AutoSize = True
        Me.LblSolitos.Location = New System.Drawing.Point(26, 152)
        Me.LblSolitos.Name = "LblSolitos"
        Me.LblSolitos.Size = New System.Drawing.Size(52, 13)
        Me.LblSolitos.TabIndex = 94
        Me.LblSolitos.Text = "LblSolitos"
        '
        'LblToditos
        '
        Me.LblToditos.AutoSize = True
        Me.LblToditos.Location = New System.Drawing.Point(22, 700)
        Me.LblToditos.Name = "LblToditos"
        Me.LblToditos.Size = New System.Drawing.Size(56, 13)
        Me.LblToditos.TabIndex = 95
        Me.LblToditos.Text = "LblToditos"
        '
        'FrmListDebtors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1252, 759)
        Me.Controls.Add(Me.LblToditos)
        Me.Controls.Add(Me.LblSolitos)
        Me.Controls.Add(Me.BtnPaymentGenerator)
        Me.Controls.Add(Me.DgvFamilyGroup)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label)
        Me.Controls.Add(Me.PnlSearch)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.DgvIndividual)
        Me.Controls.Add(Me.LblInformacion)
        Me.Controls.Add(Me.PbLogo)
        Me.Controls.Add(Me.BtnPayMonth)
        Me.Controls.Add(Me.StsStatusBar)
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
    Friend WithEvents Label As Label
    Friend WithEvents RbPayIndividual As RadioButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RbPayGroup As RadioButton
    Friend WithEvents DgvFamilyGroup As DataGridView
    Friend WithEvents DgvIndividual As DataGridView
    Friend WithEvents BtnPaymentGenerator As Button
    Friend WithEvents LblToditos As Label
    Friend WithEvents LblSolitos As Label
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Apellido As DataGridViewTextBoxColumn
    Friend WithEvents TextoEdad As DataGridViewTextBoxColumn
    Friend WithEvents MesAnio As DataGridViewTextBoxColumn
    Friend WithEvents PrcPgs As DataGridViewTextBoxColumn
    Friend WithEvents DscPgs As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents DiasMes As DataGridViewTextBoxColumn
    Friend WithEvents APagar As DataGridViewTextBoxColumn
    Friend WithEvents Column As DataGridViewTextBoxColumn
    Friend WithEvents fdi_pgs As DataGridViewTextBoxColumn
    Friend WithEvents id_pgs As DataGridViewTextBoxColumn
    Friend WithEvents cliente As DataGridViewTextBoxColumn
    Friend WithEvents nom_cli_gf As DataGridViewTextBoxColumn
    Friend WithEvents nom_grp As DataGridViewTextBoxColumn
    Friend WithEvents fdi_pgs_gf As DataGridViewTextBoxColumn
    Friend WithEvents PrcPgsGf As DataGridViewTextBoxColumn
    Friend WithEvents DscPgsGf As DataGridViewTextBoxColumn
    Friend WithEvents TtlPgsGf As DataGridViewTextBoxColumn
    Friend WithEvents NumDiasGf As DataGridViewTextBoxColumn
    Friend WithEvents APgrGf As DataGridViewTextBoxColumn
    Friend WithEvents empty As DataGridViewTextBoxColumn
End Class
