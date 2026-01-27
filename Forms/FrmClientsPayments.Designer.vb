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
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle54 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle55 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle56 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle57 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle58 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle59 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle60 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle61 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle62 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle63 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle64 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle65 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle66 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle67 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle68 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmClientsPayments))
        Me.FregistroCorto = New System.Windows.Forms.Label()
        Me.FnacimientoCorto = New System.Windows.Forms.Label()
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
        Me.PnlDatosCliente = New System.Windows.Forms.Panel()
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
        Me.DgvClientes = New System.Windows.Forms.DataGridView()
        Me.Colidcli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colnomcli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colapecli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colnacimientocorto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colnacimientolargo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Coledadcliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Coltlfcli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colmailcli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Coldircli = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colmetodopago = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colinscripcioncorto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colinscripcionlargo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colestadocliente = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Colidgrupo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GbEstado = New System.Windows.Forms.GroupBox()
        Me.RbActivo = New System.Windows.Forms.RadioButton()
        Me.RbInactivo = New System.Windows.Forms.RadioButton()
        Me.CmbFiltrar = New System.Windows.Forms.ComboBox()
        Me.TxtBuscar = New System.Windows.Forms.TextBox()
        Me.LblFiltrar = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PnlBuscar = New System.Windows.Forms.Panel()
        Me.BtnSeleccionar = New System.Windows.Forms.Button()
        Me.GbDatosCliente = New System.Windows.Forms.GroupBox()
        Me.BtnFindClient = New System.Windows.Forms.Button()
        Me.BtnCancelSearch = New System.Windows.Forms.Button()
        Me.id_user = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.LblLetrero = New System.Windows.Forms.Label()
        Me.BtnCloseWindow = New System.Windows.Forms.Button()
        Me.PnlBotonera = New System.Windows.Forms.Panel()
        Me.BtnDeleteClient = New System.Windows.Forms.Button()
        Me.BtnCollectMonth = New System.Windows.Forms.Button()
        Me.BtnNewPayment = New System.Windows.Forms.Button()
        Me.BtnModifyData = New System.Windows.Forms.Button()
        Me.BtnNewClient = New System.Windows.Forms.Button()
        Me.GbListaPagos = New System.Windows.Forms.GroupBox()
        Me.tap_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ndd_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ttl_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dsc_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.prc_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mtd_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.frm_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fdp_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.fdi_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DgvPaymentList = New System.Windows.Forms.DataGridView()
        Me.id_pgs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PictureBox = New System.Windows.Forms.PictureBox()
        Me.PnlDatosCliente.SuspendLayout()
        CType(Me.DgvClientes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbEstado.SuspendLayout()
        Me.PnlBuscar.SuspendLayout()
        Me.GbDatosCliente.SuspendLayout()
        Me.PnlBotonera.SuspendLayout()
        CType(Me.DgvPaymentList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'FregistroCorto
        '
        Me.FregistroCorto.AutoSize = True
        Me.FregistroCorto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FregistroCorto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FregistroCorto.Location = New System.Drawing.Point(652, 212)
        Me.FregistroCorto.Margin = New System.Windows.Forms.Padding(0)
        Me.FregistroCorto.Name = "FregistroCorto"
        Me.FregistroCorto.Size = New System.Drawing.Size(72, 13)
        Me.FregistroCorto.TabIndex = 13
        Me.FregistroCorto.Text = "FregistroCorto"
        Me.FregistroCorto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.FregistroCorto.Visible = False
        '
        'FnacimientoCorto
        '
        Me.FnacimientoCorto.AutoSize = True
        Me.FnacimientoCorto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.FnacimientoCorto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FnacimientoCorto.Location = New System.Drawing.Point(652, 24)
        Me.FnacimientoCorto.Margin = New System.Windows.Forms.Padding(0)
        Me.FnacimientoCorto.Name = "FnacimientoCorto"
        Me.FnacimientoCorto.Size = New System.Drawing.Size(89, 13)
        Me.FnacimientoCorto.TabIndex = 11
        Me.FnacimientoCorto.Text = "FnacimientoCorto"
        Me.FnacimientoCorto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.FnacimientoCorto.Visible = False
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
        'PnlDatosCliente
        '
        Me.PnlDatosCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlDatosCliente.Controls.Add(Me.FregistroCorto)
        Me.PnlDatosCliente.Controls.Add(Me.FnacimientoCorto)
        Me.PnlDatosCliente.Controls.Add(Me.LblResult)
        Me.PnlDatosCliente.Controls.Add(Me.LblDirCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblNomCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblEstCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblFdiCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblGrpFamCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblEmlCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblNombre)
        Me.PnlDatosCliente.Controls.Add(Me.LblMtdPgoCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblFdnCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblTlfCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblEdadCli)
        Me.PnlDatosCliente.Controls.Add(Me.gb2)
        Me.PnlDatosCliente.Controls.Add(Me.gb1)
        Me.PnlDatosCliente.Controls.Add(Me.LblApeCli)
        Me.PnlDatosCliente.Controls.Add(Me.LblTelefono)
        Me.PnlDatosCliente.Controls.Add(Me.LblEmail)
        Me.PnlDatosCliente.Controls.Add(Me.LblDireccion)
        Me.PnlDatosCliente.Controls.Add(Me.LblEdad)
        Me.PnlDatosCliente.Controls.Add(Me.LblGrupoFamiliar)
        Me.PnlDatosCliente.Controls.Add(Me.LblEstado)
        Me.PnlDatosCliente.Controls.Add(Me.LblFechaInscripcion)
        Me.PnlDatosCliente.Controls.Add(Me.LblMetodoPago)
        Me.PnlDatosCliente.Controls.Add(Me.LblFechaNacimiento)
        Me.PnlDatosCliente.Controls.Add(Me.LblApellido)
        Me.PnlDatosCliente.Controls.Add(Me.DgvClientes)
        Me.PnlDatosCliente.Location = New System.Drawing.Point(19, 75)
        Me.PnlDatosCliente.Margin = New System.Windows.Forms.Padding(0, 8, 0, 16)
        Me.PnlDatosCliente.Name = "PnlDatosCliente"
        Me.PnlDatosCliente.Size = New System.Drawing.Size(893, 286)
        Me.PnlDatosCliente.TabIndex = 86
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
        'DgvClientes
        '
        Me.DgvClientes.AllowUserToAddRows = False
        Me.DgvClientes.AllowUserToDeleteRows = False
        Me.DgvClientes.AllowUserToResizeColumns = False
        Me.DgvClientes.AllowUserToResizeRows = False
        DataGridViewCellStyle52.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle52.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvClientes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle52
        DataGridViewCellStyle53.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle53.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle53.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle53.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle53.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle53.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvClientes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle53
        Me.DgvClientes.ColumnHeadersHeight = 32
        Me.DgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvClientes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Colidcli, Me.Colnomcli, Me.Colapecli, Me.Colnacimientocorto, Me.Colnacimientolargo, Me.Coledadcliente, Me.Coltlfcli, Me.Colmailcli, Me.Coldircli, Me.Colmetodopago, Me.Colinscripcioncorto, Me.Colinscripcionlargo, Me.Colestadocliente, Me.Colidgrupo})
        Me.DgvClientes.Location = New System.Drawing.Point(0, 0)
        Me.DgvClientes.Margin = New System.Windows.Forms.Padding(0)
        Me.DgvClientes.MultiSelect = False
        Me.DgvClientes.Name = "DgvClientes"
        Me.DgvClientes.ReadOnly = True
        Me.DgvClientes.RowHeadersWidth = 4
        Me.DgvClientes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle54.BackColor = System.Drawing.Color.Lavender
        DataGridViewCellStyle54.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvClientes.RowsDefaultCellStyle = DataGridViewCellStyle54
        Me.DgvClientes.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.DgvClientes.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvClientes.RowTemplate.Height = 27
        Me.DgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvClientes.Size = New System.Drawing.Size(889, 254)
        Me.DgvClientes.TabIndex = 84
        Me.ToolTip.SetToolTip(Me.DgvClientes, "DOBLE CLIC PARA SELECCIONAR UN CLIENTE")
        Me.DgvClientes.Visible = False
        '
        'Colidcli
        '
        Me.Colidcli.HeaderText = "Colidcli"
        Me.Colidcli.Name = "Colidcli"
        Me.Colidcli.ReadOnly = True
        Me.Colidcli.Visible = False
        '
        'Colnomcli
        '
        Me.Colnomcli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Colnomcli.HeaderText = "Nombre"
        Me.Colnomcli.Name = "Colnomcli"
        Me.Colnomcli.ReadOnly = True
        Me.Colnomcli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Colnomcli.Width = 192
        '
        'Colapecli
        '
        Me.Colapecli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Colapecli.HeaderText = "Apellido"
        Me.Colapecli.Name = "Colapecli"
        Me.Colapecli.ReadOnly = True
        Me.Colapecli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Colapecli.Width = 192
        '
        'Colnacimientocorto
        '
        Me.Colnacimientocorto.HeaderText = "Colnacimientocorto"
        Me.Colnacimientocorto.Name = "Colnacimientocorto"
        Me.Colnacimientocorto.ReadOnly = True
        Me.Colnacimientocorto.Visible = False
        '
        'Colnacimientolargo
        '
        Me.Colnacimientolargo.HeaderText = "Colnacimientolargo"
        Me.Colnacimientolargo.Name = "Colnacimientolargo"
        Me.Colnacimientolargo.ReadOnly = True
        Me.Colnacimientolargo.Visible = False
        '
        'Coledadcliente
        '
        Me.Coledadcliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Coledadcliente.HeaderText = "Edad"
        Me.Coledadcliente.Name = "Coledadcliente"
        Me.Coledadcliente.ReadOnly = True
        Me.Coledadcliente.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Coledadcliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Coledadcliente.Width = 120
        '
        'Coltlfcli
        '
        Me.Coltlfcli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Coltlfcli.HeaderText = "Telefóno"
        Me.Coltlfcli.Name = "Coltlfcli"
        Me.Coltlfcli.ReadOnly = True
        Me.Coltlfcli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Coltlfcli.Width = 144
        '
        'Colmailcli
        '
        Me.Colmailcli.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Colmailcli.HeaderText = "E-mail"
        Me.Colmailcli.Name = "Colmailcli"
        Me.Colmailcli.ReadOnly = True
        Me.Colmailcli.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Colmailcli.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Colmailcli.Width = 216
        '
        'Coldircli
        '
        Me.Coldircli.HeaderText = "Coldircli"
        Me.Coldircli.Name = "Coldircli"
        Me.Coldircli.ReadOnly = True
        Me.Coldircli.Visible = False
        '
        'Colmetodopago
        '
        Me.Colmetodopago.HeaderText = "Colmetodopago"
        Me.Colmetodopago.Name = "Colmetodopago"
        Me.Colmetodopago.ReadOnly = True
        Me.Colmetodopago.Visible = False
        '
        'Colinscripcioncorto
        '
        Me.Colinscripcioncorto.HeaderText = "Colinscripcioncorto"
        Me.Colinscripcioncorto.Name = "Colinscripcioncorto"
        Me.Colinscripcioncorto.ReadOnly = True
        Me.Colinscripcioncorto.Visible = False
        '
        'Colinscripcionlargo
        '
        Me.Colinscripcionlargo.HeaderText = "Colinscripcionlargo"
        Me.Colinscripcionlargo.Name = "Colinscripcionlargo"
        Me.Colinscripcionlargo.ReadOnly = True
        Me.Colinscripcionlargo.Visible = False
        '
        'Colestadocliente
        '
        Me.Colestadocliente.HeaderText = "Colestadocliente"
        Me.Colestadocliente.Name = "Colestadocliente"
        Me.Colestadocliente.ReadOnly = True
        Me.Colestadocliente.Visible = False
        '
        'Colidgrupo
        '
        Me.Colidgrupo.HeaderText = "Colidgrupo"
        Me.Colidgrupo.Name = "Colidgrupo"
        Me.Colidgrupo.ReadOnly = True
        Me.Colidgrupo.Visible = False
        '
        'GbEstado
        '
        Me.GbEstado.Controls.Add(Me.RbActivo)
        Me.GbEstado.Controls.Add(Me.RbInactivo)
        Me.GbEstado.Enabled = False
        Me.GbEstado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbEstado.Location = New System.Drawing.Point(920, 73)
        Me.GbEstado.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.GbEstado.Name = "GbEstado"
        Me.GbEstado.Padding = New System.Windows.Forms.Padding(0)
        Me.GbEstado.Size = New System.Drawing.Size(151, 288)
        Me.GbEstado.TabIndex = 83
        Me.GbEstado.TabStop = False
        Me.GbEstado.Text = " Estado "
        '
        'RbActivo
        '
        Me.RbActivo.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbActivo.BackColor = System.Drawing.SystemColors.Control
        Me.RbActivo.FlatAppearance.BorderSize = 4
        Me.RbActivo.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbActivo.Image = Global.GymPaymentControl.My.Resources.Resources.ic_in_activity_50x50
        Me.RbActivo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RbActivo.Location = New System.Drawing.Point(24, 27)
        Me.RbActivo.Margin = New System.Windows.Forms.Padding(32, 12, 32, 0)
        Me.RbActivo.Name = "RbActivo"
        Me.RbActivo.Padding = New System.Windows.Forms.Padding(0, 6, 0, 4)
        Me.RbActivo.Size = New System.Drawing.Size(104, 112)
        Me.RbActivo.TabIndex = 0
        Me.RbActivo.Text = "Clientes en actividad"
        Me.RbActivo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RbActivo.UseVisualStyleBackColor = False
        '
        'RbInactivo
        '
        Me.RbInactivo.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbInactivo.BackColor = System.Drawing.SystemColors.Control
        Me.RbInactivo.FlatAppearance.BorderSize = 4
        Me.RbInactivo.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.RbInactivo.Image = Global.GymPaymentControl.My.Resources.Resources.ic_inactive_50x50
        Me.RbInactivo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RbInactivo.Location = New System.Drawing.Point(23, 160)
        Me.RbInactivo.Margin = New System.Windows.Forms.Padding(0, 0, 0, 16)
        Me.RbInactivo.Name = "RbInactivo"
        Me.RbInactivo.Padding = New System.Windows.Forms.Padding(0, 7, 0, 4)
        Me.RbInactivo.Size = New System.Drawing.Size(104, 112)
        Me.RbInactivo.TabIndex = 1
        Me.RbInactivo.Text = "Clientes inactivos"
        Me.RbInactivo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.RbInactivo.UseVisualStyleBackColor = False
        '
        'CmbFiltrar
        '
        Me.CmbFiltrar.BackColor = System.Drawing.SystemColors.Window
        Me.CmbFiltrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFiltrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbFiltrar.ForeColor = System.Drawing.Color.MediumBlue
        Me.CmbFiltrar.FormattingEnabled = True
        Me.CmbFiltrar.IntegralHeight = False
        Me.CmbFiltrar.Items.AddRange(New Object() {"", "   NOMBRE", "   APELLIDO", "   TELEFONO"})
        Me.CmbFiltrar.Location = New System.Drawing.Point(100, 7)
        Me.CmbFiltrar.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.CmbFiltrar.Name = "CmbFiltrar"
        Me.CmbFiltrar.Size = New System.Drawing.Size(136, 24)
        Me.CmbFiltrar.TabIndex = 82
        '
        'TxtBuscar
        '
        Me.TxtBuscar.BackColor = System.Drawing.Color.Snow
        Me.TxtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtBuscar.Font = New System.Drawing.Font("Linux Libertine Display G", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBuscar.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtBuscar.Location = New System.Drawing.Point(241, 6)
        Me.TxtBuscar.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.TxtBuscar.MaxLength = 30
        Me.TxtBuscar.Name = "TxtBuscar"
        Me.TxtBuscar.Size = New System.Drawing.Size(603, 25)
        Me.TxtBuscar.TabIndex = 84
        Me.TxtBuscar.WordWrap = False
        '
        'LblFiltrar
        '
        Me.LblFiltrar.BackColor = System.Drawing.SystemColors.ButtonFace
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
        Me.PnlBuscar.Controls.Add(Me.CmbFiltrar)
        Me.PnlBuscar.Controls.Add(Me.BtnSeleccionar)
        Me.PnlBuscar.Controls.Add(Me.TxtBuscar)
        Me.PnlBuscar.Controls.Add(Me.LblFiltrar)
        Me.PnlBuscar.Enabled = False
        Me.PnlBuscar.Location = New System.Drawing.Point(19, 26)
        Me.PnlBuscar.Margin = New System.Windows.Forms.Padding(16, 8, 8, 0)
        Me.PnlBuscar.Name = "PnlBuscar"
        Me.PnlBuscar.Size = New System.Drawing.Size(893, 42)
        Me.PnlBuscar.TabIndex = 84
        '
        'BtnSeleccionar
        '
        Me.BtnSeleccionar.AutoSize = True
        Me.BtnSeleccionar.FlatAppearance.BorderSize = 0
        Me.BtnSeleccionar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSeleccionar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold)
        Me.BtnSeleccionar.Image = Global.GymPaymentControl.My.Resources.Resources.ic_okay_round_28x28
        Me.BtnSeleccionar.Location = New System.Drawing.Point(848, 0)
        Me.BtnSeleccionar.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.BtnSeleccionar.Name = "BtnSeleccionar"
        Me.BtnSeleccionar.Size = New System.Drawing.Size(37, 37)
        Me.BtnSeleccionar.TabIndex = 85
        Me.ToolTip.SetToolTip(Me.BtnSeleccionar, "CONFIRMA BUSQUEDA, SELECCIONAR CLIENTE")
        Me.BtnSeleccionar.UseVisualStyleBackColor = True
        '
        'GbDatosCliente
        '
        Me.GbDatosCliente.Controls.Add(Me.PnlDatosCliente)
        Me.GbDatosCliente.Controls.Add(Me.Panel2)
        Me.GbDatosCliente.Controls.Add(Me.PnlBuscar)
        Me.GbDatosCliente.Controls.Add(Me.GbEstado)
        Me.GbDatosCliente.Controls.Add(Me.BtnFindClient)
        Me.GbDatosCliente.Controls.Add(Me.BtnCancelSearch)
        Me.GbDatosCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbDatosCliente.Location = New System.Drawing.Point(25, 73)
        Me.GbDatosCliente.Margin = New System.Windows.Forms.Padding(16, 8, 0, 0)
        Me.GbDatosCliente.Name = "GbDatosCliente"
        Me.GbDatosCliente.Size = New System.Drawing.Size(1082, 378)
        Me.GbDatosCliente.TabIndex = 93
        Me.GbDatosCliente.TabStop = False
        Me.GbDatosCliente.Text = "Datos del cliente :"
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
        'id_user
        '
        Me.id_user.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle55.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.id_user.DefaultCellStyle = DataGridViewCellStyle55
        Me.id_user.HeaderText = "USUARIO"
        Me.id_user.Name = "id_user"
        Me.id_user.ReadOnly = True
        Me.id_user.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.id_user.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
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
        'GbListaPagos
        '
        Me.GbListaPagos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbListaPagos.Location = New System.Drawing.Point(25, 467)
        Me.GbListaPagos.Margin = New System.Windows.Forms.Padding(0, 16, 0, 16)
        Me.GbListaPagos.Name = "GbListaPagos"
        Me.GbListaPagos.Padding = New System.Windows.Forms.Padding(0)
        Me.GbListaPagos.Size = New System.Drawing.Size(1082, 313)
        Me.GbListaPagos.TabIndex = 90
        Me.GbListaPagos.TabStop = False
        Me.GbListaPagos.Text = "Historial de pagos :"
        '
        'tap_pgs
        '
        Me.tap_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle56.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.tap_pgs.DefaultCellStyle = DataGridViewCellStyle56
        Me.tap_pgs.HeaderText = "A PAGAR"
        Me.tap_pgs.Name = "tap_pgs"
        Me.tap_pgs.ReadOnly = True
        Me.tap_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.tap_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.tap_pgs.Width = 90
        '
        'ndd_pgs
        '
        Me.ndd_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle57.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ndd_pgs.DefaultCellStyle = DataGridViewCellStyle57
        Me.ndd_pgs.HeaderText = "Nº DE DIAS"
        Me.ndd_pgs.Name = "ndd_pgs"
        Me.ndd_pgs.ReadOnly = True
        Me.ndd_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ndd_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ndd_pgs.Width = 90
        '
        'ttl_pgs
        '
        Me.ttl_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle58.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ttl_pgs.DefaultCellStyle = DataGridViewCellStyle58
        Me.ttl_pgs.HeaderText = "TOTAL"
        Me.ttl_pgs.Name = "ttl_pgs"
        Me.ttl_pgs.ReadOnly = True
        Me.ttl_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ttl_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ttl_pgs.Width = 90
        '
        'dsc_pgs
        '
        Me.dsc_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle59.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.dsc_pgs.DefaultCellStyle = DataGridViewCellStyle59
        Me.dsc_pgs.HeaderText = "DESCT"
        Me.dsc_pgs.Name = "dsc_pgs"
        Me.dsc_pgs.ReadOnly = True
        Me.dsc_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dsc_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.dsc_pgs.Width = 90
        '
        'prc_pgs
        '
        Me.prc_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle60.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.prc_pgs.DefaultCellStyle = DataGridViewCellStyle60
        Me.prc_pgs.HeaderText = "PRECIO"
        Me.prc_pgs.Name = "prc_pgs"
        Me.prc_pgs.ReadOnly = True
        Me.prc_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.prc_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.prc_pgs.Width = 90
        '
        'mtd_pgs
        '
        Me.mtd_pgs.HeaderText = "mtd_pgs"
        Me.mtd_pgs.Name = "mtd_pgs"
        Me.mtd_pgs.ReadOnly = True
        Me.mtd_pgs.Visible = False
        '
        'frm_pgs
        '
        Me.frm_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle61.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.frm_pgs.DefaultCellStyle = DataGridViewCellStyle61
        Me.frm_pgs.HeaderText = "FORMA DE PAGO"
        Me.frm_pgs.Name = "frm_pgs"
        Me.frm_pgs.ReadOnly = True
        Me.frm_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.frm_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.frm_pgs.Width = 140
        '
        'fdp_pgs
        '
        Me.fdp_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle62.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.fdp_pgs.DefaultCellStyle = DataGridViewCellStyle62
        Me.fdp_pgs.HeaderText = "FECHA DE PAGO"
        Me.fdp_pgs.Name = "fdp_pgs"
        Me.fdp_pgs.ReadOnly = True
        Me.fdp_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.fdp_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fdp_pgs.Width = 170
        '
        'fdi_pgs
        '
        Me.fdi_pgs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        DataGridViewCellStyle63.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.fdi_pgs.DefaultCellStyle = DataGridViewCellStyle63
        Me.fdi_pgs.HeaderText = "FECHA DE INICIO"
        Me.fdi_pgs.Name = "fdi_pgs"
        Me.fdi_pgs.ReadOnly = True
        Me.fdi_pgs.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.fdi_pgs.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.fdi_pgs.Width = 170
        '
        'DgvPaymentList
        '
        Me.DgvPaymentList.AllowUserToAddRows = False
        Me.DgvPaymentList.AllowUserToDeleteRows = False
        Me.DgvPaymentList.AllowUserToResizeColumns = False
        Me.DgvPaymentList.AllowUserToResizeRows = False
        DataGridViewCellStyle64.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle64.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPaymentList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle64
        DataGridViewCellStyle65.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle65.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle65.Font = New System.Drawing.Font("Linux Libertine Display G", 9.75!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle65.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle65.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle65.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle65.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvPaymentList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle65
        Me.DgvPaymentList.ColumnHeadersHeight = 32
        Me.DgvPaymentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvPaymentList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.id_pgs, Me.fdi_pgs, Me.fdp_pgs, Me.frm_pgs, Me.mtd_pgs, Me.prc_pgs, Me.dsc_pgs, Me.ttl_pgs, Me.ndd_pgs, Me.tap_pgs, Me.id_user})
        DataGridViewCellStyle66.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle66.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle66.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle66.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle66.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle66.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle66.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvPaymentList.DefaultCellStyle = DataGridViewCellStyle66
        Me.DgvPaymentList.Location = New System.Drawing.Point(36, 493)
        Me.DgvPaymentList.MultiSelect = False
        Me.DgvPaymentList.Name = "DgvPaymentList"
        Me.DgvPaymentList.ReadOnly = True
        DataGridViewCellStyle67.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle67.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle67.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle67.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle67.SelectionBackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle67.SelectionForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle67.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvPaymentList.RowHeadersDefaultCellStyle = DataGridViewCellStyle67
        Me.DgvPaymentList.RowHeadersWidth = 4
        Me.DgvPaymentList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle68.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle68.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle68.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle68.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle68.SelectionForeColor = System.Drawing.Color.White
        Me.DgvPaymentList.RowsDefaultCellStyle = DataGridViewCellStyle68
        Me.DgvPaymentList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPaymentList.RowTemplate.DividerHeight = 1
        Me.DgvPaymentList.RowTemplate.Height = 30
        Me.DgvPaymentList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvPaymentList.Size = New System.Drawing.Size(1060, 276)
        Me.DgvPaymentList.TabIndex = 92
        '
        'id_pgs
        '
        Me.id_pgs.HeaderText = "id_pgs"
        Me.id_pgs.Name = "id_pgs"
        Me.id_pgs.ReadOnly = True
        Me.id_pgs.Visible = False
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
        'FrmClientsPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1313, 805)
        Me.Controls.Add(Me.PictureBox)
        Me.Controls.Add(Me.GbDatosCliente)
        Me.Controls.Add(Me.LblLetrero)
        Me.Controls.Add(Me.PnlBotonera)
        Me.Controls.Add(Me.DgvPaymentList)
        Me.Controls.Add(Me.GbListaPagos)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmClientsPayments"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CLIENTES REGISTRADOS - HISTORIAL DE PAGOS"
        Me.PnlDatosCliente.ResumeLayout(False)
        Me.PnlDatosCliente.PerformLayout()
        CType(Me.DgvClientes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbEstado.ResumeLayout(False)
        Me.PnlBuscar.ResumeLayout(False)
        Me.PnlBuscar.PerformLayout()
        Me.GbDatosCliente.ResumeLayout(False)
        Me.GbDatosCliente.PerformLayout()
        Me.PnlBotonera.ResumeLayout(False)
        CType(Me.DgvPaymentList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FregistroCorto As Label
    Friend WithEvents FnacimientoCorto As Label
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
    Friend WithEvents PnlDatosCliente As Panel
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
    Friend WithEvents DgvClientes As DataGridView
    Friend WithEvents Colidcli As DataGridViewTextBoxColumn
    Friend WithEvents Colnomcli As DataGridViewTextBoxColumn
    Friend WithEvents Colapecli As DataGridViewTextBoxColumn
    Friend WithEvents Colnacimientocorto As DataGridViewTextBoxColumn
    Friend WithEvents Colnacimientolargo As DataGridViewTextBoxColumn
    Friend WithEvents Coledadcliente As DataGridViewTextBoxColumn
    Friend WithEvents Coltlfcli As DataGridViewTextBoxColumn
    Friend WithEvents Colmailcli As DataGridViewTextBoxColumn
    Friend WithEvents Coldircli As DataGridViewTextBoxColumn
    Friend WithEvents Colmetodopago As DataGridViewTextBoxColumn
    Friend WithEvents Colinscripcioncorto As DataGridViewTextBoxColumn
    Friend WithEvents Colinscripcionlargo As DataGridViewTextBoxColumn
    Friend WithEvents Colestadocliente As DataGridViewTextBoxColumn
    Friend WithEvents Colidgrupo As DataGridViewTextBoxColumn
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents BtnFindClient As Button
    Friend WithEvents RbActivo As RadioButton
    Friend WithEvents GbEstado As GroupBox
    Friend WithEvents RbInactivo As RadioButton
    Friend WithEvents CmbFiltrar As ComboBox
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents LblFiltrar As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PnlBuscar As Panel
    Friend WithEvents BtnSeleccionar As Button
    Friend WithEvents GbDatosCliente As GroupBox
    Friend WithEvents BtnCancelSearch As Button
    Friend WithEvents id_user As DataGridViewTextBoxColumn
    Friend WithEvents LblLetrero As Label
    Friend WithEvents BtnCloseWindow As Button
    Friend WithEvents BtnDeleteClient As Button
    Friend WithEvents BtnCollectMonth As Button
    Friend WithEvents BtnNewPayment As Button
    Friend WithEvents BtnModifyData As Button
    Friend WithEvents BtnNewClient As Button
    Friend WithEvents PnlBotonera As Panel
    Friend WithEvents GbListaPagos As GroupBox
    Friend WithEvents tap_pgs As DataGridViewTextBoxColumn
    Friend WithEvents ndd_pgs As DataGridViewTextBoxColumn
    Friend WithEvents ttl_pgs As DataGridViewTextBoxColumn
    Friend WithEvents dsc_pgs As DataGridViewTextBoxColumn
    Friend WithEvents prc_pgs As DataGridViewTextBoxColumn
    Friend WithEvents mtd_pgs As DataGridViewTextBoxColumn
    Friend WithEvents frm_pgs As DataGridViewTextBoxColumn
    Friend WithEvents fdp_pgs As DataGridViewTextBoxColumn
    Friend WithEvents fdi_pgs As DataGridViewTextBoxColumn
    Friend WithEvents DgvPaymentList As DataGridView
    Friend WithEvents id_pgs As DataGridViewTextBoxColumn
End Class
