<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmClientsPayments
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle15 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle16 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle17 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmClientsPayments))
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.LblResult = New System.Windows.Forms.Label()
        Me.LblDirCli = New System.Windows.Forms.Label()
        Me.LblNomCli = New System.Windows.Forms.Label()
        Me.LblEstCli = New System.Windows.Forms.Label()
        Me.LblFdiCli = New System.Windows.Forms.Label()
        Me.LblGrpFamCli = New System.Windows.Forms.Label()
        Me.LblEmlCli = New System.Windows.Forms.Label()
        Me.LblNombre = New System.Windows.Forms.Label()
        Me.LblMtdPgoCli = New System.Windows.Forms.Label()
        Me.LblFdnCli = New System.Windows.Forms.Label()
        Me.LblTlfCli = New System.Windows.Forms.Label()
        Me.LblEdadCli = New System.Windows.Forms.Label()
        Me.gb2 = New System.Windows.Forms.GroupBox()
        Me.gb1 = New System.Windows.Forms.GroupBox()
        Me.PnlDataClient = New System.Windows.Forms.Panel()
        Me.LblApeCli = New System.Windows.Forms.Label()
        Me.LblTelefono = New System.Windows.Forms.Label()
        Me.LblEmail = New System.Windows.Forms.Label()
        Me.LblDireccion = New System.Windows.Forms.Label()
        Me.LblEdad = New System.Windows.Forms.Label()
        Me.LblGrupoFamiliar = New System.Windows.Forms.Label()
        Me.LblEstado = New System.Windows.Forms.Label()
        Me.LblFechaInscripcion = New System.Windows.Forms.Label()
        Me.LblMetodoPago = New System.Windows.Forms.Label()
        Me.LblFechaNacimiento = New System.Windows.Forms.Label()
        Me.LblApellido = New System.Windows.Forms.Label()
        Me.DgvClientList = New System.Windows.Forms.DataGridView()
        Me.NomCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ApeCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FdnCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TlfCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmlCli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GbState = New System.Windows.Forms.GroupBox()
        Me.RbActive = New System.Windows.Forms.RadioButton()
        Me.RbInactive = New System.Windows.Forms.RadioButton()
        Me.CmbFilter = New System.Windows.Forms.ComboBox()
        Me.TxtSearch = New System.Windows.Forms.TextBox()
        Me.LblFilter = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PnlBuscar = New System.Windows.Forms.Panel()
        Me.BtnSelect = New System.Windows.Forms.Button()
        Me.GbDataClient = New System.Windows.Forms.GroupBox()
        Me.BtnFindClient = New System.Windows.Forms.Button()
        Me.BtnCancelSearch = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.LblLetrero = New System.Windows.Forms.Label()
        Me.PnlBotonera = New System.Windows.Forms.Panel()
        Me.BtnCloseWindow = New System.Windows.Forms.Button()
        Me.BtnDeleteClient = New System.Windows.Forms.Button()
        Me.BtnCollectMonth = New System.Windows.Forms.Button()
        Me.BtnNewPayment = New System.Windows.Forms.Button()
        Me.BtnModifyData = New System.Windows.Forms.Button()
        Me.BtnNewClient = New System.Windows.Forms.Button()
        Me.GbPaymentList = New System.Windows.Forms.GroupBox()
        Me.DgvPaymentList = New System.Windows.Forms.DataGridView()
        Me.PictureBox = New System.Windows.Forms.PictureBox()
        Me.fdi_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fdp_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.frm_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prc_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dsc_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ttl_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ndd_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tap_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.nom_user = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PnlDataClient.SuspendLayout()
        CType(Me.DgvClientList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbState.SuspendLayout()
        Me.PnlBuscar.SuspendLayout()
        Me.GbDataClient.SuspendLayout()
        Me.PnlBotonera.SuspendLayout()
        CType(Me.DgvPaymentList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblResult
        '
        Me.LblResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblResult.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LblResult.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblResult.ForeColor = System.Drawing.Color.Gray
        Me.LblResult.Location = New System.Drawing.Point(0, 254)
        Me.LblResult.Name = "LblResult"
        Me.LblResult.Padding = New System.Windows.Forms.Padding(24, 0, 0, 0)
        Me.LblResult.Size = New System.Drawing.Size(889, 28)
        Me.LblResult.TabIndex = 88
        Me.LblResult.Text = "NUMERO DE CLIENTES QUE COINCIDEN CON LA BUSQUEDA"
        Me.LblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblResult.Visible = False
        '
        'LblDirCli
        '
        Me.LblDirCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblDirCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblDirCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblDirCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblDirCli.Location = New System.Drawing.Point(580, 111)
        Me.LblDirCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblDirCli.Name = "LblDirCli"
        Me.LblDirCli.Padding = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.LblDirCli.Size = New System.Drawing.Size(284, 62)
        Me.LblDirCli.TabIndex = 6
        Me.LblDirCli.Text = "LblDirCli"
        '
        'LblNomCli
        '
        Me.LblNomCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblNomCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNomCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblNomCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblNomCli.Location = New System.Drawing.Point(147, 17)
        Me.LblNomCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblNomCli.Name = "LblNomCli"
        Me.LblNomCli.Size = New System.Drawing.Size(284, 26)
        Me.LblNomCli.TabIndex = 0
        Me.LblNomCli.Text = "LblNomCli"
        Me.LblNomCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblEstCli
        '
        Me.LblEstCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblEstCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblEstCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblEstCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblEstCli.Location = New System.Drawing.Point(581, 241)
        Me.LblEstCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblEstCli.Name = "LblEstCli"
        Me.LblEstCli.Size = New System.Drawing.Size(142, 26)
        Me.LblEstCli.TabIndex = 10
        Me.LblEstCli.Text = "LblEstCli"
        Me.LblEstCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblFdiCli
        '
        Me.LblFdiCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblFdiCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblFdiCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblFdiCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblFdiCli.Location = New System.Drawing.Point(580, 205)
        Me.LblFdiCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblFdiCli.Name = "LblFdiCli"
        Me.LblFdiCli.Size = New System.Drawing.Size(284, 26)
        Me.LblFdiCli.TabIndex = 9
        Me.LblFdiCli.Text = "LblFdiCli"
        Me.LblFdiCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblGrpFamCli
        '
        Me.LblGrpFamCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblGrpFamCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblGrpFamCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblGrpFamCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblGrpFamCli.Location = New System.Drawing.Point(147, 241)
        Me.LblGrpFamCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblGrpFamCli.Name = "LblGrpFamCli"
        Me.LblGrpFamCli.Size = New System.Drawing.Size(284, 26)
        Me.LblGrpFamCli.TabIndex = 8
        Me.LblGrpFamCli.Text = "LblGrpFamCli"
        Me.LblGrpFamCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblEmlCli
        '
        Me.LblEmlCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblEmlCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblEmlCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblEmlCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblEmlCli.Location = New System.Drawing.Point(147, 147)
        Me.LblEmlCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblEmlCli.Name = "LblEmlCli"
        Me.LblEmlCli.Size = New System.Drawing.Size(284, 26)
        Me.LblEmlCli.TabIndex = 5
        Me.LblEmlCli.Text = "LblEmlCli"
        Me.LblEmlCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblNombre
        '
        Me.LblNombre.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNombre.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblNombre.Location = New System.Drawing.Point(24, 16)
        Me.LblNombre.Margin = New System.Windows.Forms.Padding(24, 16, 0, 0)
        Me.LblNombre.Name = "LblNombre"
        Me.LblNombre.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblNombre.Size = New System.Drawing.Size(408, 28)
        Me.LblNombre.TabIndex = 15
        Me.LblNombre.Text = "Nombre"
        '
        'LblMtdPgoCli
        '
        Me.LblMtdPgoCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblMtdPgoCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblMtdPgoCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblMtdPgoCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblMtdPgoCli.Location = New System.Drawing.Point(147, 205)
        Me.LblMtdPgoCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMtdPgoCli.Name = "LblMtdPgoCli"
        Me.LblMtdPgoCli.Size = New System.Drawing.Size(284, 26)
        Me.LblMtdPgoCli.TabIndex = 7
        Me.LblMtdPgoCli.Text = "LblMtdPgoCli"
        Me.LblMtdPgoCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblFdnCli
        '
        Me.LblFdnCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblFdnCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblFdnCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblFdnCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblFdnCli.Location = New System.Drawing.Point(580, 17)
        Me.LblFdnCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblFdnCli.Name = "LblFdnCli"
        Me.LblFdnCli.Size = New System.Drawing.Size(284, 26)
        Me.LblFdnCli.TabIndex = 2
        Me.LblFdnCli.Text = "LblFdnCli"
        Me.LblFdnCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTlfCli
        '
        Me.LblTlfCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblTlfCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTlfCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblTlfCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblTlfCli.Location = New System.Drawing.Point(147, 111)
        Me.LblTlfCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTlfCli.Name = "LblTlfCli"
        Me.LblTlfCli.Size = New System.Drawing.Size(284, 26)
        Me.LblTlfCli.TabIndex = 4
        Me.LblTlfCli.Text = "LblTlfCli"
        Me.LblTlfCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblEdadCli
        '
        Me.LblEdadCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblEdadCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblEdadCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblEdadCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblEdadCli.Location = New System.Drawing.Point(581, 53)
        Me.LblEdadCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblEdadCli.Name = "LblEdadCli"
        Me.LblEdadCli.Size = New System.Drawing.Size(142, 26)
        Me.LblEdadCli.TabIndex = 3
        Me.LblEdadCli.Text = "LblEdadCli"
        Me.LblEdadCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'gb2
        '
        Me.gb2.Location = New System.Drawing.Point(8, 186)
        Me.gb2.Margin = New System.Windows.Forms.Padding(0)
        Me.gb2.Name = "gb2"
        Me.gb2.Padding = New System.Windows.Forms.Padding(0)
        Me.gb2.Size = New System.Drawing.Size(874, 2)
        Me.gb2.TabIndex = 55
        Me.gb2.TabStop = False
        '
        'gb1
        '
        Me.gb1.Location = New System.Drawing.Point(8, 92)
        Me.gb1.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.gb1.Name = "gb1"
        Me.gb1.Padding = New System.Windows.Forms.Padding(0)
        Me.gb1.Size = New System.Drawing.Size(874, 2)
        Me.gb1.TabIndex = 54
        Me.gb1.TabStop = False
        '
        'PnlDataClient
        '
        Me.PnlDataClient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlDataClient.Controls.Add(Me.LblResult)
        Me.PnlDataClient.Controls.Add(Me.LblDirCli)
        Me.PnlDataClient.Controls.Add(Me.LblNomCli)
        Me.PnlDataClient.Controls.Add(Me.LblEstCli)
        Me.PnlDataClient.Controls.Add(Me.LblFdiCli)
        Me.PnlDataClient.Controls.Add(Me.LblGrpFamCli)
        Me.PnlDataClient.Controls.Add(Me.LblEmlCli)
        Me.PnlDataClient.Controls.Add(Me.LblNombre)
        Me.PnlDataClient.Controls.Add(Me.LblMtdPgoCli)
        Me.PnlDataClient.Controls.Add(Me.LblFdnCli)
        Me.PnlDataClient.Controls.Add(Me.LblTlfCli)
        Me.PnlDataClient.Controls.Add(Me.LblEdadCli)
        Me.PnlDataClient.Controls.Add(Me.gb2)
        Me.PnlDataClient.Controls.Add(Me.gb1)
        Me.PnlDataClient.Controls.Add(Me.LblApeCli)
        Me.PnlDataClient.Controls.Add(Me.LblTelefono)
        Me.PnlDataClient.Controls.Add(Me.LblEmail)
        Me.PnlDataClient.Controls.Add(Me.LblDireccion)
        Me.PnlDataClient.Controls.Add(Me.LblEdad)
        Me.PnlDataClient.Controls.Add(Me.LblGrupoFamiliar)
        Me.PnlDataClient.Controls.Add(Me.LblEstado)
        Me.PnlDataClient.Controls.Add(Me.LblFechaInscripcion)
        Me.PnlDataClient.Controls.Add(Me.LblMetodoPago)
        Me.PnlDataClient.Controls.Add(Me.LblFechaNacimiento)
        Me.PnlDataClient.Controls.Add(Me.LblApellido)
        Me.PnlDataClient.Controls.Add(Me.DgvClientList)
        Me.PnlDataClient.Location = New System.Drawing.Point(19, 75)
        Me.PnlDataClient.Margin = New System.Windows.Forms.Padding(0, 8, 0, 16)
        Me.PnlDataClient.Name = "PnlDataClient"
        Me.PnlDataClient.Size = New System.Drawing.Size(893, 286)
        Me.PnlDataClient.TabIndex = 86
        '
        'LblApeCli
        '
        Me.LblApeCli.BackColor = System.Drawing.Color.Gainsboro
        Me.LblApeCli.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblApeCli.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.LblApeCli.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblApeCli.Location = New System.Drawing.Point(147, 53)
        Me.LblApeCli.Margin = New System.Windows.Forms.Padding(0)
        Me.LblApeCli.Name = "LblApeCli"
        Me.LblApeCli.Size = New System.Drawing.Size(284, 26)
        Me.LblApeCli.TabIndex = 1
        Me.LblApeCli.Text = "LblApeCli"
        Me.LblApeCli.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTelefono
        '
        Me.LblTelefono.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTelefono.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblTelefono.Location = New System.Drawing.Point(24, 110)
        Me.LblTelefono.Margin = New System.Windows.Forms.Padding(24, 16, 0, 0)
        Me.LblTelefono.Name = "LblTelefono"
        Me.LblTelefono.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblTelefono.Size = New System.Drawing.Size(408, 28)
        Me.LblTelefono.TabIndex = 19
        Me.LblTelefono.Text = "Teléfono"
        '
        'LblEmail
        '
        Me.LblEmail.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblEmail.Location = New System.Drawing.Point(24, 146)
        Me.LblEmail.Margin = New System.Windows.Forms.Padding(24, 8, 8, 0)
        Me.LblEmail.Name = "LblEmail"
        Me.LblEmail.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblEmail.Size = New System.Drawing.Size(408, 28)
        Me.LblEmail.TabIndex = 20
        Me.LblEmail.Text = "E-Mail"
        '
        'LblDireccion
        '
        Me.LblDireccion.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDireccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblDireccion.Location = New System.Drawing.Point(457, 110)
        Me.LblDireccion.Margin = New System.Windows.Forms.Padding(0, 16, 24, 0)
        Me.LblDireccion.Name = "LblDireccion"
        Me.LblDireccion.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblDireccion.Size = New System.Drawing.Size(408, 64)
        Me.LblDireccion.TabIndex = 21
        Me.LblDireccion.Text = "Dirección"
        '
        'LblEdad
        '
        Me.LblEdad.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblEdad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblEdad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEdad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblEdad.Location = New System.Drawing.Point(457, 52)
        Me.LblEdad.Margin = New System.Windows.Forms.Padding(0, 8, 24, 0)
        Me.LblEdad.Name = "LblEdad"
        Me.LblEdad.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblEdad.Size = New System.Drawing.Size(267, 28)
        Me.LblEdad.TabIndex = 18
        Me.LblEdad.Text = "Edad"
        '
        'LblGrupoFamiliar
        '
        Me.LblGrupoFamiliar.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblGrupoFamiliar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblGrupoFamiliar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGrupoFamiliar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblGrupoFamiliar.Location = New System.Drawing.Point(24, 240)
        Me.LblGrupoFamiliar.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.LblGrupoFamiliar.Name = "LblGrupoFamiliar"
        Me.LblGrupoFamiliar.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblGrupoFamiliar.Size = New System.Drawing.Size(408, 28)
        Me.LblGrupoFamiliar.TabIndex = 24
        Me.LblGrupoFamiliar.Text = "Grupo familiar"
        '
        'LblEstado
        '
        Me.LblEstado.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblEstado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblEstado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblEstado.Location = New System.Drawing.Point(457, 240)
        Me.LblEstado.Margin = New System.Windows.Forms.Padding(0, 8, 24, 0)
        Me.LblEstado.Name = "LblEstado"
        Me.LblEstado.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblEstado.Size = New System.Drawing.Size(267, 28)
        Me.LblEstado.TabIndex = 25
        Me.LblEstado.Text = "Estado"
        '
        'LblFechaInscripcion
        '
        Me.LblFechaInscripcion.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblFechaInscripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFechaInscripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFechaInscripcion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblFechaInscripcion.Location = New System.Drawing.Point(457, 204)
        Me.LblFechaInscripcion.Margin = New System.Windows.Forms.Padding(0, 16, 24, 0)
        Me.LblFechaInscripcion.Name = "LblFechaInscripcion"
        Me.LblFechaInscripcion.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblFechaInscripcion.Size = New System.Drawing.Size(408, 28)
        Me.LblFechaInscripcion.TabIndex = 23
        Me.LblFechaInscripcion.Text = "F. de Inscripción"
        '
        'LblMetodoPago
        '
        Me.LblMetodoPago.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblMetodoPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblMetodoPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMetodoPago.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblMetodoPago.Location = New System.Drawing.Point(24, 204)
        Me.LblMetodoPago.Margin = New System.Windows.Forms.Padding(24, 16, 0, 0)
        Me.LblMetodoPago.Name = "LblMetodoPago"
        Me.LblMetodoPago.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblMetodoPago.Size = New System.Drawing.Size(408, 28)
        Me.LblMetodoPago.TabIndex = 22
        Me.LblMetodoPago.Text = "Método de pago"
        '
        'LblFechaNacimiento
        '
        Me.LblFechaNacimiento.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblFechaNacimiento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFechaNacimiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblFechaNacimiento.Location = New System.Drawing.Point(457, 16)
        Me.LblFechaNacimiento.Margin = New System.Windows.Forms.Padding(0, 16, 24, 0)
        Me.LblFechaNacimiento.Name = "LblFechaNacimiento"
        Me.LblFechaNacimiento.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblFechaNacimiento.Size = New System.Drawing.Size(408, 28)
        Me.LblFechaNacimiento.TabIndex = 17
        Me.LblFechaNacimiento.Text = "F. de Nacimiento"
        '
        'LblApellido
        '
        Me.LblApellido.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblApellido.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblApellido.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LblApellido.Location = New System.Drawing.Point(24, 52)
        Me.LblApellido.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.LblApellido.Name = "LblApellido"
        Me.LblApellido.Padding = New System.Windows.Forms.Padding(4, 4, 0, 0)
        Me.LblApellido.Size = New System.Drawing.Size(408, 28)
        Me.LblApellido.TabIndex = 16
        Me.LblApellido.Text = "Apellido"
        '
        'DgvClientList
        '
        Me.DgvClientList.AllowUserToAddRows = False
        Me.DgvClientList.AllowUserToDeleteRows = False
        Me.DgvClientList.AllowUserToResizeColumns = False
        Me.DgvClientList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvClientList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvClientList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvClientList.ColumnHeadersHeight = 32
        Me.DgvClientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvClientList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NomCli, Me.ApeCli, Me.FdnCli, Me.TlfCli, Me.EmlCli})
        Me.DgvClientList.Location = New System.Drawing.Point(0, 0)
        Me.DgvClientList.Margin = New System.Windows.Forms.Padding(0)
        Me.DgvClientList.MultiSelect = False
        Me.DgvClientList.Name = "DgvClientList"
        Me.DgvClientList.ReadOnly = True
        Me.DgvClientList.RowHeadersWidth = 4
        Me.DgvClientList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvClientList.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvClientList.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DgvClientList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvClientList.RowTemplate.Height = 27
        Me.DgvClientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvClientList.Size = New System.Drawing.Size(889, 254)
        Me.DgvClientList.TabIndex = 84
        Me.ToolTip.SetToolTip(Me.DgvClientList, "DOBLE CLIC PARA SELECCIONAR UN CLIENTE")
        Me.DgvClientList.Visible = False
        '
        'NomCli
        '
        Me.NomCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.NomCli.DataPropertyName = "Name"
        Me.NomCli.HeaderText = "Nombre"
        Me.NomCli.MinimumWidth = 6
        Me.NomCli.Name = "NomCli"
        Me.NomCli.ReadOnly = True
        Me.NomCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.NomCli.Width = 192
        '
        'ApeCli
        '
        Me.ApeCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ApeCli.DataPropertyName = "LastName"
        Me.ApeCli.HeaderText = "Apellido"
        Me.ApeCli.MinimumWidth = 6
        Me.ApeCli.Name = "ApeCli"
        Me.ApeCli.ReadOnly = True
        Me.ApeCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ApeCli.Width = 192
        '
        'FdnCli
        '
        Me.FdnCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.FdnCli.DataPropertyName = "AgeText"
        Me.FdnCli.HeaderText = "Edad"
        Me.FdnCli.MinimumWidth = 6
        Me.FdnCli.Name = "FdnCli"
        Me.FdnCli.ReadOnly = True
        Me.FdnCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.FdnCli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.FdnCli.Width = 120
        '
        'TlfCli
        '
        Me.TlfCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.TlfCli.DataPropertyName = "Phone"
        Me.TlfCli.HeaderText = "Telefóno"
        Me.TlfCli.MinimumWidth = 6
        Me.TlfCli.Name = "TlfCli"
        Me.TlfCli.ReadOnly = True
        Me.TlfCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TlfCli.Width = 144
        '
        'EmlCli
        '
        Me.EmlCli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.EmlCli.DataPropertyName = "Email"
        Me.EmlCli.HeaderText = "E-mail"
        Me.EmlCli.MinimumWidth = 6
        Me.EmlCli.Name = "EmlCli"
        Me.EmlCli.ReadOnly = True
        Me.EmlCli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.EmlCli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.EmlCli.Width = 216
        '
        'GbState
        '
        Me.GbState.Controls.Add(Me.RbActive)
        Me.GbState.Controls.Add(Me.RbInactive)
        Me.GbState.Enabled = False
        Me.GbState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbState.Location = New System.Drawing.Point(920, 73)
        Me.GbState.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.GbState.Name = "GbState"
        Me.GbState.Padding = New System.Windows.Forms.Padding(0)
        Me.GbState.Size = New System.Drawing.Size(151, 288)
        Me.GbState.TabIndex = 83
        Me.GbState.TabStop = False
        Me.GbState.Text = " Estado "
        '
        'RbActive
        '
        Me.RbActive.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbActive.BackColor = System.Drawing.SystemColors.Control
        Me.RbActive.FlatAppearance.BorderSize = 4
        Me.RbActive.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbActive.Image = Global.GymPaymentControl.My.Resources.Resources.ic_in_activity_50x50
        Me.RbActive.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RbActive.Location = New System.Drawing.Point(24, 27)
        Me.RbActive.Margin = New System.Windows.Forms.Padding(32, 12, 32, 0)
        Me.RbActive.Name = "RbActive"
        Me.RbActive.Padding = New System.Windows.Forms.Padding(0, 6, 0, 4)
        Me.RbActive.Size = New System.Drawing.Size(104, 112)
        Me.RbActive.TabIndex = 0
        Me.RbActive.Text = "Clientes en actividad"
        Me.RbActive.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RbActive.UseVisualStyleBackColor = False
        '
        'RbInactive
        '
        Me.RbInactive.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbInactive.BackColor = System.Drawing.SystemColors.Control
        Me.RbInactive.FlatAppearance.BorderSize = 4
        Me.RbInactive.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.RbInactive.Image = Global.GymPaymentControl.My.Resources.Resources.ic_inactive_50x50
        Me.RbInactive.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RbInactive.Location = New System.Drawing.Point(23, 160)
        Me.RbInactive.Margin = New System.Windows.Forms.Padding(0, 0, 0, 16)
        Me.RbInactive.Name = "RbInactive"
        Me.RbInactive.Padding = New System.Windows.Forms.Padding(0, 7, 0, 4)
        Me.RbInactive.Size = New System.Drawing.Size(104, 112)
        Me.RbInactive.TabIndex = 1
        Me.RbInactive.Text = "Clientes inactivos"
        Me.RbInactive.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RbInactive.UseVisualStyleBackColor = False
        '
        'CmbFilter
        '
        Me.CmbFilter.BackColor = System.Drawing.SystemColors.Window
        Me.CmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbFilter.ForeColor = System.Drawing.Color.MediumBlue
        Me.CmbFilter.FormattingEnabled = True
        Me.CmbFilter.IntegralHeight = False
        Me.CmbFilter.Items.AddRange(New Object() {"", "   NOMBRE", "   APELLIDO", "   TELEFONO"})
        Me.CmbFilter.Location = New System.Drawing.Point(100, 7)
        Me.CmbFilter.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.CmbFilter.Name = "CmbFilter"
        Me.CmbFilter.Size = New System.Drawing.Size(136, 24)
        Me.CmbFilter.TabIndex = 82
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
        Me.TxtSearch.Size = New System.Drawing.Size(603, 25)
        Me.TxtSearch.TabIndex = 84
        Me.TxtSearch.WordWrap = False
        '
        'LblFilter
        '
        Me.LblFilter.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.LblFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFilter.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFilter.Location = New System.Drawing.Point(8, 6)
        Me.LblFilter.Margin = New System.Windows.Forms.Padding(8, 6, 0, 6)
        Me.LblFilter.Name = "LblFilter"
        Me.LblFilter.Size = New System.Drawing.Size(229, 26)
        Me.LblFilter.TabIndex = 88
        Me.LblFilter.Text = "  Filtrar por"
        Me.LblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(445, 467)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(10, 26)
        Me.Panel2.TabIndex = 85
        '
        'PnlBuscar
        '
        Me.PnlBuscar.AutoSize = True
        Me.PnlBuscar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlBuscar.Controls.Add(Me.CmbFilter)
        Me.PnlBuscar.Controls.Add(Me.BtnSelect)
        Me.PnlBuscar.Controls.Add(Me.TxtSearch)
        Me.PnlBuscar.Controls.Add(Me.LblFilter)
        Me.PnlBuscar.Enabled = False
        Me.PnlBuscar.Location = New System.Drawing.Point(19, 26)
        Me.PnlBuscar.Margin = New System.Windows.Forms.Padding(16, 8, 8, 0)
        Me.PnlBuscar.Name = "PnlBuscar"
        Me.PnlBuscar.Size = New System.Drawing.Size(893, 45)
        Me.PnlBuscar.TabIndex = 84
        '
        'BtnSelect
        '
        Me.BtnSelect.AutoSize = True
        Me.BtnSelect.FlatAppearance.BorderSize = 0
        Me.BtnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BtnSelect.Image = Global.GymPaymentControl.My.Resources.Resources.ic_okay_round_28x28
        Me.BtnSelect.Location = New System.Drawing.Point(848, 0)
        Me.BtnSelect.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.BtnSelect.Name = "BtnSelect"
        Me.BtnSelect.Size = New System.Drawing.Size(37, 37)
        Me.BtnSelect.TabIndex = 85
        Me.ToolTip.SetToolTip(Me.BtnSelect, "CONFIRMA BUSQUEDA, SELECCIONAR CLIENTE")
        Me.BtnSelect.UseVisualStyleBackColor = True
        '
        'GbDataClient
        '
        Me.GbDataClient.Controls.Add(Me.PnlDataClient)
        Me.GbDataClient.Controls.Add(Me.Panel2)
        Me.GbDataClient.Controls.Add(Me.PnlBuscar)
        Me.GbDataClient.Controls.Add(Me.GbState)
        Me.GbDataClient.Controls.Add(Me.BtnFindClient)
        Me.GbDataClient.Controls.Add(Me.BtnCancelSearch)
        Me.GbDataClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbDataClient.Location = New System.Drawing.Point(25, 73)
        Me.GbDataClient.Margin = New System.Windows.Forms.Padding(16, 8, 0, 0)
        Me.GbDataClient.Name = "GbDataClient"
        Me.GbDataClient.Size = New System.Drawing.Size(1082, 378)
        Me.GbDataClient.TabIndex = 93
        Me.GbDataClient.TabStop = False
        Me.GbDataClient.Text = "Datos del cliente :"
        '
        'BtnFindClient
        '
        Me.BtnFindClient.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnFindClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFindClient.Image = Global.GymPaymentControl.My.Resources.Resources.ic_search_28x32
        Me.BtnFindClient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnFindClient.Location = New System.Drawing.Point(920, 25)
        Me.BtnFindClient.Margin = New System.Windows.Forms.Padding(0, 0, 16, 0)
        Me.BtnFindClient.Name = "BtnFindClient"
        Me.BtnFindClient.Padding = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.BtnFindClient.Size = New System.Drawing.Size(151, 40)
        Me.BtnFindClient.TabIndex = 0
        Me.BtnFindClient.Text = "&Buscar cliente"
        Me.BtnFindClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFindClient.UseVisualStyleBackColor = False
        '
        'BtnCancelSearch
        '
        Me.BtnCancelSearch.FlatAppearance.BorderSize = 0
        Me.BtnCancelSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BtnCancelSearch.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_search_30x30
        Me.BtnCancelSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCancelSearch.Location = New System.Drawing.Point(920, 25)
        Me.BtnCancelSearch.Margin = New System.Windows.Forms.Padding(4, 12, 4, 0)
        Me.BtnCancelSearch.Name = "BtnCancelSearch"
        Me.BtnCancelSearch.Padding = New System.Windows.Forms.Padding(16, 0, 16, 0)
        Me.BtnCancelSearch.Size = New System.Drawing.Size(151, 40)
        Me.BtnCancelSearch.TabIndex = 0
        Me.BtnCancelSearch.Text = "&Cancelar"
        Me.BtnCancelSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip.SetToolTip(Me.BtnCancelSearch, "CANCELAR BUSQUEDA")
        Me.BtnCancelSearch.UseVisualStyleBackColor = True
        Me.BtnCancelSearch.Visible = False
        '
        'LblLetrero
        '
        Me.LblLetrero.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLetrero.ForeColor = System.Drawing.Color.Green
        Me.LblLetrero.Location = New System.Drawing.Point(89, 17)
        Me.LblLetrero.Margin = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.LblLetrero.Name = "LblLetrero"
        Me.LblLetrero.Size = New System.Drawing.Size(787, 48)
        Me.LblLetrero.TabIndex = 88
        Me.LblLetrero.Text = "Para ver los datos de un cliente, actualizar o eliminar haz clic en buscar y sele" &
    "cciona un registro de la lista. Podrás cobrar mensualidades y ver el historial d" &
    "e pagos del cliente seleccionado."
        '
        'PnlBotonera
        '
        Me.PnlBotonera.AutoSize = True
        Me.PnlBotonera.BackColor = System.Drawing.Color.Silver
        Me.PnlBotonera.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlBotonera.Controls.Add(Me.BtnCloseWindow)
        Me.PnlBotonera.Controls.Add(Me.BtnDeleteClient)
        Me.PnlBotonera.Controls.Add(Me.BtnCollectMonth)
        Me.PnlBotonera.Controls.Add(Me.BtnNewPayment)
        Me.PnlBotonera.Controls.Add(Me.BtnModifyData)
        Me.PnlBotonera.Controls.Add(Me.BtnNewClient)
        Me.PnlBotonera.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlBotonera.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlBotonera.Location = New System.Drawing.Point(1131, 0)
        Me.PnlBotonera.Margin = New System.Windows.Forms.Padding(0)
        Me.PnlBotonera.Name = "PnlBotonera"
        Me.PnlBotonera.Size = New System.Drawing.Size(182, 805)
        Me.PnlBotonera.TabIndex = 89
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
        Me.BtnCloseWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCloseWindow.Location = New System.Drawing.Point(4, 723)
        Me.BtnCloseWindow.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCloseWindow.Name = "BtnCloseWindow"
        Me.BtnCloseWindow.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.BtnCloseWindow.Size = New System.Drawing.Size(170, 44)
        Me.BtnCloseWindow.TabIndex = 6
        Me.BtnCloseWindow.Text = "Cerrar &ventana"
        Me.BtnCloseWindow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCloseWindow.UseVisualStyleBackColor = False
        '
        'BtnDeleteClient
        '
        Me.BtnDeleteClient.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnDeleteClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteClient.Image = Global.GymPaymentControl.My.Resources.Resources.ic_delete_28x32
        Me.BtnDeleteClient.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteClient.Location = New System.Drawing.Point(4, 284)
        Me.BtnDeleteClient.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnDeleteClient.Name = "BtnDeleteClient"
        Me.BtnDeleteClient.Padding = New System.Windows.Forms.Padding(0, 6, 0, 4)
        Me.BtnDeleteClient.Size = New System.Drawing.Size(170, 70)
        Me.BtnDeleteClient.TabIndex = 3
        Me.BtnDeleteClient.Text = "&Eliminar cliente"
        Me.BtnDeleteClient.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDeleteClient.UseVisualStyleBackColor = False
        '
        'BtnCollectMonth
        '
        Me.BtnCollectMonth.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnCollectMonth.FlatAppearance.BorderColor = System.Drawing.Color.Green
        Me.BtnCollectMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCollectMonth.ForeColor = System.Drawing.Color.DarkGreen
        Me.BtnCollectMonth.Image = Global.GymPaymentControl.My.Resources.Resources.ic_pay_month_28x32
        Me.BtnCollectMonth.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCollectMonth.Location = New System.Drawing.Point(4, 491)
        Me.BtnCollectMonth.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCollectMonth.Name = "BtnCollectMonth"
        Me.BtnCollectMonth.Padding = New System.Windows.Forms.Padding(0, 6, 0, 4)
        Me.BtnCollectMonth.Size = New System.Drawing.Size(170, 70)
        Me.BtnCollectMonth.TabIndex = 4
        Me.BtnCollectMonth.Text = "&Cobrar mes"
        Me.BtnCollectMonth.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCollectMonth.UseVisualStyleBackColor = False
        '
        'BtnNewPayment
        '
        Me.BtnNewPayment.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnNewPayment.FlatAppearance.BorderColor = System.Drawing.Color.Green
        Me.BtnNewPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewPayment.ForeColor = System.Drawing.Color.DarkGreen
        Me.BtnNewPayment.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_payment_28x32
        Me.BtnNewPayment.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewPayment.Location = New System.Drawing.Point(4, 573)
        Me.BtnNewPayment.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnNewPayment.Name = "BtnNewPayment"
        Me.BtnNewPayment.Padding = New System.Windows.Forms.Padding(0, 5, 0, 4)
        Me.BtnNewPayment.Size = New System.Drawing.Size(170, 70)
        Me.BtnNewPayment.TabIndex = 5
        Me.BtnNewPayment.Text = "N&uevo pago"
        Me.BtnNewPayment.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewPayment.UseVisualStyleBackColor = False
        '
        'BtnModifyData
        '
        Me.BtnModifyData.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnModifyData.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnModifyData.Image = Global.GymPaymentControl.My.Resources.Resources.ic_modify_28x32
        Me.BtnModifyData.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnModifyData.Location = New System.Drawing.Point(4, 202)
        Me.BtnModifyData.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnModifyData.Name = "BtnModifyData"
        Me.BtnModifyData.Padding = New System.Windows.Forms.Padding(0, 6, 0, 4)
        Me.BtnModifyData.Size = New System.Drawing.Size(170, 70)
        Me.BtnModifyData.TabIndex = 2
        Me.BtnModifyData.Text = "&Modificar datos"
        Me.BtnModifyData.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnModifyData.UseVisualStyleBackColor = False
        '
        'BtnNewClient
        '
        Me.BtnNewClient.BackColor = System.Drawing.SystemColors.ControlLight
        Me.BtnNewClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewClient.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_24x32
        Me.BtnNewClient.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewClient.Location = New System.Drawing.Point(4, 120)
        Me.BtnNewClient.Margin = New System.Windows.Forms.Padding(4, 12, 4, 0)
        Me.BtnNewClient.Name = "BtnNewClient"
        Me.BtnNewClient.Padding = New System.Windows.Forms.Padding(0, 5, 0, 4)
        Me.BtnNewClient.Size = New System.Drawing.Size(170, 70)
        Me.BtnNewClient.TabIndex = 1
        Me.BtnNewClient.Text = "&Nuevo cliente"
        Me.BtnNewClient.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewClient.UseVisualStyleBackColor = False
        '
        'GbPaymentList
        '
        Me.GbPaymentList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbPaymentList.Location = New System.Drawing.Point(25, 467)
        Me.GbPaymentList.Margin = New System.Windows.Forms.Padding(0, 16, 0, 16)
        Me.GbPaymentList.Name = "GbPaymentList"
        Me.GbPaymentList.Padding = New System.Windows.Forms.Padding(0)
        Me.GbPaymentList.Size = New System.Drawing.Size(1082, 313)
        Me.GbPaymentList.TabIndex = 90
        Me.GbPaymentList.TabStop = False
        Me.GbPaymentList.Text = "Historial de pagos :"
        '
        'DgvPaymentList
        '
        Me.DgvPaymentList.AllowUserToAddRows = False
        Me.DgvPaymentList.AllowUserToDeleteRows = False
        Me.DgvPaymentList.AllowUserToResizeColumns = False
        Me.DgvPaymentList.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPaymentList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Linux Libertine Display G", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvPaymentList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvPaymentList.ColumnHeadersHeight = 32
        Me.DgvPaymentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvPaymentList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.fdi_pgs, Me.fdp_pgs, Me.frm_pgs, Me.prc_pgs, Me.dsc_pgs, Me.ttl_pgs, Me.ndd_pgs, Me.tap_pgs, Me.nom_user})
        DataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvPaymentList.DefaultCellStyle = DataGridViewCellStyle15
        Me.DgvPaymentList.Location = New System.Drawing.Point(36, 493)
        Me.DgvPaymentList.MultiSelect = False
        Me.DgvPaymentList.Name = "DgvPaymentList"
        Me.DgvPaymentList.ReadOnly = True
        DataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvPaymentList.RowHeadersDefaultCellStyle = DataGridViewCellStyle16
        Me.DgvPaymentList.RowHeadersWidth = 4
        Me.DgvPaymentList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle17.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle17.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.White
        Me.DgvPaymentList.RowsDefaultCellStyle = DataGridViewCellStyle17
        Me.DgvPaymentList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPaymentList.RowTemplate.DividerHeight = 1
        Me.DgvPaymentList.RowTemplate.Height = 30
        Me.DgvPaymentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvPaymentList.Size = New System.Drawing.Size(1060, 276)
        Me.DgvPaymentList.TabIndex = 92
        '
        'PictureBox
        '
        Me.PictureBox.Image = CType(resources.GetObject("PictureBox.Image"), System.Drawing.Image)
        Me.PictureBox.Location = New System.Drawing.Point(25, 17)
        Me.PictureBox.Margin = New System.Windows.Forms.Padding(16, 8, 0, 0)
        Me.PictureBox.Name = "PictureBox"
        Me.PictureBox.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox.TabIndex = 91
        Me.PictureBox.TabStop = False
        '
        'fdi_pgs
        '
        Me.fdi_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.fdi_pgs.DataPropertyName = "LongFdiPgs"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.fdi_pgs.DefaultCellStyle = DataGridViewCellStyle6
        Me.fdi_pgs.HeaderText = "FECHA DE INICIO"
        Me.fdi_pgs.MinimumWidth = 6
        Me.fdi_pgs.Name = "fdi_pgs"
        Me.fdi_pgs.ReadOnly = True
        Me.fdi_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.fdi_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fdi_pgs.Width = 155
        '
        'fdp_pgs
        '
        Me.fdp_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.fdp_pgs.DataPropertyName = "LongFdpPgs"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.fdp_pgs.DefaultCellStyle = DataGridViewCellStyle7
        Me.fdp_pgs.HeaderText = "FECHA DE PAGO"
        Me.fdp_pgs.MinimumWidth = 6
        Me.fdp_pgs.Name = "fdp_pgs"
        Me.fdp_pgs.ReadOnly = True
        Me.fdp_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.fdp_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fdp_pgs.Width = 155
        '
        'frm_pgs
        '
        Me.frm_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.frm_pgs.DataPropertyName = "FrmPgs"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.frm_pgs.DefaultCellStyle = DataGridViewCellStyle8
        Me.frm_pgs.HeaderText = "FORMA DE PAGO"
        Me.frm_pgs.MinimumWidth = 6
        Me.frm_pgs.Name = "frm_pgs"
        Me.frm_pgs.ReadOnly = True
        Me.frm_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.frm_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.frm_pgs.Width = 145
        '
        'prc_pgs
        '
        Me.prc_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.prc_pgs.DataPropertyName = "PrcPgs"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.prc_pgs.DefaultCellStyle = DataGridViewCellStyle9
        Me.prc_pgs.HeaderText = "PRECIO"
        Me.prc_pgs.MinimumWidth = 6
        Me.prc_pgs.Name = "prc_pgs"
        Me.prc_pgs.ReadOnly = True
        Me.prc_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.prc_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.prc_pgs.Width = 90
        '
        'dsc_pgs
        '
        Me.dsc_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.dsc_pgs.DataPropertyName = "DscPgs"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dsc_pgs.DefaultCellStyle = DataGridViewCellStyle10
        Me.dsc_pgs.HeaderText = "DESCT"
        Me.dsc_pgs.MinimumWidth = 6
        Me.dsc_pgs.Name = "dsc_pgs"
        Me.dsc_pgs.ReadOnly = True
        Me.dsc_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dsc_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dsc_pgs.Width = 90
        '
        'ttl_pgs
        '
        Me.ttl_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ttl_pgs.DataPropertyName = "Total"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ttl_pgs.DefaultCellStyle = DataGridViewCellStyle11
        Me.ttl_pgs.HeaderText = "TOTAL"
        Me.ttl_pgs.MinimumWidth = 6
        Me.ttl_pgs.Name = "ttl_pgs"
        Me.ttl_pgs.ReadOnly = True
        Me.ttl_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ttl_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ttl_pgs.Width = 90
        '
        'ndd_pgs
        '
        Me.ndd_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ndd_pgs.DataPropertyName = "DaysOfMonth"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ndd_pgs.DefaultCellStyle = DataGridViewCellStyle12
        Me.ndd_pgs.HeaderText = "Nº DE DIAS"
        Me.ndd_pgs.MinimumWidth = 6
        Me.ndd_pgs.Name = "ndd_pgs"
        Me.ndd_pgs.ReadOnly = True
        Me.ndd_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ndd_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ndd_pgs.Width = 90
        '
        'tap_pgs
        '
        Me.tap_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.tap_pgs.DataPropertyName = "TotalToPay"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.tap_pgs.DefaultCellStyle = DataGridViewCellStyle13
        Me.tap_pgs.HeaderText = "A PAGAR"
        Me.tap_pgs.MinimumWidth = 6
        Me.tap_pgs.Name = "tap_pgs"
        Me.tap_pgs.ReadOnly = True
        Me.tap_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.tap_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.tap_pgs.Width = 90
        '
        'nom_user
        '
        Me.nom_user.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.nom_user.DataPropertyName = "NomUser"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.nom_user.DefaultCellStyle = DataGridViewCellStyle14
        Me.nom_user.HeaderText = "USUARIO"
        Me.nom_user.MinimumWidth = 6
        Me.nom_user.Name = "nom_user"
        Me.nom_user.ReadOnly = True
        Me.nom_user.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.nom_user.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.nom_user.Width = 125
        '
        'FrmClientsPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1313, 805)
        Me.Controls.Add(Me.PictureBox)
        Me.Controls.Add(Me.GbDataClient)
        Me.Controls.Add(Me.LblLetrero)
        Me.Controls.Add(Me.PnlBotonera)
        Me.Controls.Add(Me.DgvPaymentList)
        Me.Controls.Add(Me.GbPaymentList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmClientsPayments"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CLIENTES REGISTRADOS - HISTORIAL DE PAGOS"
        Me.PnlDataClient.ResumeLayout(False)
        CType(Me.DgvClientList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbState.ResumeLayout(False)
        Me.PnlBuscar.ResumeLayout(False)
        Me.PnlBuscar.PerformLayout()
        Me.GbDataClient.ResumeLayout(False)
        Me.GbDataClient.PerformLayout()
        Me.PnlBotonera.ResumeLayout(False)
        CType(Me.DgvPaymentList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblResult As Label
    Friend WithEvents LblDirCli As Label
    Friend WithEvents LblNomCli As Label
    Friend WithEvents LblEstCli As Label
    Friend WithEvents LblFdiCli As Label
    Friend WithEvents LblGrpFamCli As Label
    Friend WithEvents LblEmlCli As Label
    Friend WithEvents LblNombre As Label
    Friend WithEvents LblMtdPgoCli As Label
    Friend WithEvents LblFdnCli As Label
    Friend WithEvents LblTlfCli As Label
    Friend WithEvents LblEdadCli As Label
    Friend WithEvents gb2 As GroupBox
    Friend WithEvents gb1 As GroupBox
    Friend WithEvents PictureBox As PictureBox
    Friend WithEvents PnlDataClient As Panel
    Friend WithEvents LblApeCli As Label
    Friend WithEvents LblTelefono As Label
    Friend WithEvents LblEmail As Label
    Friend WithEvents LblDireccion As Label
    Friend WithEvents LblEdad As Label
    Friend WithEvents LblGrupoFamiliar As Label
    Friend WithEvents LblEstado As Label
    Friend WithEvents LblFechaInscripcion As Label
    Friend WithEvents LblMetodoPago As Label
    Friend WithEvents LblFechaNacimiento As Label
    Friend WithEvents LblApellido As Label
    Friend WithEvents DgvClientList As DataGridView
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents BtnFindClient As Button
    Friend WithEvents RbActive As RadioButton
    Friend WithEvents GbState As GroupBox
    Friend WithEvents RbInactive As RadioButton
    Friend WithEvents CmbFilter As ComboBox
    Friend WithEvents TxtSearch As TextBox
    Friend WithEvents LblFilter As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PnlBuscar As Panel
    Friend WithEvents BtnSelect As Button
    Friend WithEvents GbDataClient As GroupBox
    Friend WithEvents BtnCancelSearch As Button
    Friend WithEvents LblLetrero As Label
    Friend WithEvents BtnCloseWindow As Button
    Friend WithEvents BtnDeleteClient As Button
    Friend WithEvents BtnCollectMonth As Button
    Friend WithEvents BtnNewPayment As Button
    Friend WithEvents BtnModifyData As Button
    Friend WithEvents BtnNewClient As Button
    Friend WithEvents PnlBotonera As Panel
    Friend WithEvents GbPaymentList As GroupBox
    Friend WithEvents DgvPaymentList As DataGridView
    Friend WithEvents NomCli As DataGridViewTextBoxColumn
    Friend WithEvents ApeCli As DataGridViewTextBoxColumn
    Friend WithEvents FdnCli As DataGridViewTextBoxColumn
    Friend WithEvents TlfCli As DataGridViewTextBoxColumn
    Friend WithEvents EmlCli As DataGridViewTextBoxColumn
    Friend WithEvents fdi_pgs As DataGridViewTextBoxColumn
    Friend WithEvents fdp_pgs As DataGridViewTextBoxColumn
    Friend WithEvents frm_pgs As DataGridViewTextBoxColumn
    Friend WithEvents prc_pgs As DataGridViewTextBoxColumn
    Friend WithEvents dsc_pgs As DataGridViewTextBoxColumn
    Friend WithEvents ttl_pgs As DataGridViewTextBoxColumn
    Friend WithEvents ndd_pgs As DataGridViewTextBoxColumn
    Friend WithEvents tap_pgs As DataGridViewTextBoxColumn
    Friend WithEvents nom_user As DataGridViewTextBoxColumn
End Class
