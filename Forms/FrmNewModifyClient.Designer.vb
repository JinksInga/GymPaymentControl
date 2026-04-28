<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmNewModifyClient
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.GbOtros = New System.Windows.Forms.GroupBox()
        Me.RbInactiveState = New System.Windows.Forms.RadioButton()
        Me.RbActiveStatus = New System.Windows.Forms.RadioButton()
        Me.LblEstadoCli = New System.Windows.Forms.Label()
        Me.DtpRegistrationDate = New System.Windows.Forms.DateTimePicker()
        Me.LblFinscripcion = New System.Windows.Forms.Label()
        Me.GbMetodoPago = New System.Windows.Forms.GroupBox()
        Me.RbDailyPayment = New System.Windows.Forms.RadioButton()
        Me.RbMonthlyPayment = New System.Windows.Forms.RadioButton()
        Me.RbGroupPayment = New System.Windows.Forms.RadioButton()
        Me.BtnAddGroup = New System.Windows.Forms.Button()
        Me.GbListaGrupoFamiliar = New System.Windows.Forms.GroupBox()
        Me.LblNumberMembers = New System.Windows.Forms.Label()
        Me.TxtListGroupsDailyPayment = New System.Windows.Forms.TextBox()
        Me.DgvListGroupsDailyPayment = New System.Windows.Forms.DataGridView()
        Me.colIdDailyGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNameDailyGroup = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colNumMembers = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colMembersReg = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnCancelRegistration = New System.Windows.Forms.Button()
        Me.GbContacto = New System.Windows.Forms.GroupBox()
        Me.TxtAddress = New System.Windows.Forms.TextBox()
        Me.TxtEmail = New System.Windows.Forms.TextBox()
        Me.TxtPhone = New System.Windows.Forms.TextBox()
        Me.LblDireccion = New System.Windows.Forms.Label()
        Me.LblEmail = New System.Windows.Forms.Label()
        Me.LblTelefono = New System.Windows.Forms.Label()
        Me.GbDatos = New System.Windows.Forms.GroupBox()
        Me.DtpBirthdate = New System.Windows.Forms.DateTimePicker()
        Me.LblEdad = New System.Windows.Forms.Label()
        Me.TxtLastName = New System.Windows.Forms.TextBox()
        Me.TxtFirstName = New System.Windows.Forms.TextBox()
        Me.TxtCustomerAge = New System.Windows.Forms.Label()
        Me.LblFnacimiento = New System.Windows.Forms.Label()
        Me.LblApellido = New System.Windows.Forms.Label()
        Me.LblNombre = New System.Windows.Forms.Label()
        Me.BtnUpdateCustomerData = New System.Windows.Forms.Button()
        Me.BtnSaveCustomerData = New System.Windows.Forms.Button()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.GbOtros.SuspendLayout()
        Me.GbMetodoPago.SuspendLayout()
        Me.GbListaGrupoFamiliar.SuspendLayout()
        CType(Me.DgvListGroupsDailyPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GbContacto.SuspendLayout()
        Me.GbDatos.SuspendLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GbOtros
        '
        Me.GbOtros.Controls.Add(Me.RbInactiveState)
        Me.GbOtros.Controls.Add(Me.RbActiveStatus)
        Me.GbOtros.Controls.Add(Me.LblEstadoCli)
        Me.GbOtros.Controls.Add(Me.DtpRegistrationDate)
        Me.GbOtros.Controls.Add(Me.LblFinscripcion)
        Me.GbOtros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbOtros.Location = New System.Drawing.Point(439, 25)
        Me.GbOtros.Margin = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.GbOtros.Name = "GbOtros"
        Me.GbOtros.Padding = New System.Windows.Forms.Padding(0)
        Me.GbOtros.Size = New System.Drawing.Size(374, 163)
        Me.GbOtros.TabIndex = 2
        Me.GbOtros.TabStop = False
        Me.GbOtros.Text = "Otros datos :"
        '
        'RbInactiveState
        '
        Me.RbInactiveState.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbInactiveState.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbInactiveState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbInactiveState.Location = New System.Drawing.Point(192, 113)
        Me.RbInactiveState.Margin = New System.Windows.Forms.Padding(0)
        Me.RbInactiveState.Name = "RbInactiveState"
        Me.RbInactiveState.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbInactiveState.Size = New System.Drawing.Size(160, 26)
        Me.RbInactiveState.TabIndex = 2
        Me.RbInactiveState.Text = "Inactivo"
        Me.RbInactiveState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RbInactiveState.UseVisualStyleBackColor = True
        '
        'RbActiveStatus
        '
        Me.RbActiveStatus.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbActiveStatus.Checked = True
        Me.RbActiveStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbActiveStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbActiveStatus.Location = New System.Drawing.Point(24, 113)
        Me.RbActiveStatus.Margin = New System.Windows.Forms.Padding(0, 8, 0, 24)
        Me.RbActiveStatus.Name = "RbActiveStatus"
        Me.RbActiveStatus.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbActiveStatus.Size = New System.Drawing.Size(160, 26)
        Me.RbActiveStatus.TabIndex = 1
        Me.RbActiveStatus.TabStop = True
        Me.RbActiveStatus.Text = "Activo"
        Me.RbActiveStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.RbActiveStatus.UseVisualStyleBackColor = True
        '
        'LblEstadoCli
        '
        Me.LblEstadoCli.AutoSize = True
        Me.LblEstadoCli.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEstadoCli.Location = New System.Drawing.Point(24, 89)
        Me.LblEstadoCli.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblEstadoCli.Name = "LblEstadoCli"
        Me.LblEstadoCli.Size = New System.Drawing.Size(114, 16)
        Me.LblEstadoCli.TabIndex = 1
        Me.LblEstadoCli.Text = "Estado del cliente"
        '
        'DtpRegistrationDate
        '
        Me.DtpRegistrationDate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpRegistrationDate.CustomFormat = "' ' dddd ', ' dd ' de ' MMMM ' de ' yyyy"
        Me.DtpRegistrationDate.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.DtpRegistrationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpRegistrationDate.Location = New System.Drawing.Point(24, 51)
        Me.DtpRegistrationDate.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.DtpRegistrationDate.Name = "DtpRegistrationDate"
        Me.DtpRegistrationDate.Size = New System.Drawing.Size(328, 26)
        Me.DtpRegistrationDate.TabIndex = 0
        '
        'LblFinscripcion
        '
        Me.LblFinscripcion.AutoSize = True
        Me.LblFinscripcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFinscripcion.Location = New System.Drawing.Point(24, 27)
        Me.LblFinscripcion.Margin = New System.Windows.Forms.Padding(24, 12, 0, 0)
        Me.LblFinscripcion.Name = "LblFinscripcion"
        Me.LblFinscripcion.Size = New System.Drawing.Size(131, 16)
        Me.LblFinscripcion.TabIndex = 0
        Me.LblFinscripcion.Text = "Fecha de Inscripción"
        '
        'GbMetodoPago
        '
        Me.GbMetodoPago.Controls.Add(Me.RbDailyPayment)
        Me.GbMetodoPago.Controls.Add(Me.RbMonthlyPayment)
        Me.GbMetodoPago.Controls.Add(Me.RbGroupPayment)
        Me.GbMetodoPago.Controls.Add(Me.BtnAddGroup)
        Me.GbMetodoPago.Controls.Add(Me.GbListaGrupoFamiliar)
        Me.GbMetodoPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbMetodoPago.Location = New System.Drawing.Point(439, 204)
        Me.GbMetodoPago.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.GbMetodoPago.Name = "GbMetodoPago"
        Me.GbMetodoPago.Padding = New System.Windows.Forms.Padding(0)
        Me.GbMetodoPago.Size = New System.Drawing.Size(374, 341)
        Me.GbMetodoPago.TabIndex = 3
        Me.GbMetodoPago.TabStop = False
        Me.GbMetodoPago.Text = "Método de pago :"
        '
        'RbDailyPayment
        '
        Me.RbDailyPayment.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbDailyPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbDailyPayment.Image = Global.GymPaymentControl.My.Resources.Resources.ic_daily_27x30
        Me.RbDailyPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbDailyPayment.Location = New System.Drawing.Point(24, 27)
        Me.RbDailyPayment.Margin = New System.Windows.Forms.Padding(24, 12, 0, 0)
        Me.RbDailyPayment.Name = "RbDailyPayment"
        Me.RbDailyPayment.Padding = New System.Windows.Forms.Padding(30, 0, 30, 0)
        Me.RbDailyPayment.Size = New System.Drawing.Size(160, 40)
        Me.RbDailyPayment.TabIndex = 0
        Me.RbDailyPayment.Text = "Diario"
        Me.RbDailyPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RbDailyPayment.UseVisualStyleBackColor = True
        '
        'RbMonthlyPayment
        '
        Me.RbMonthlyPayment.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbMonthlyPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbMonthlyPayment.Image = Global.GymPaymentControl.My.Resources.Resources.ic_monthly_30x30
        Me.RbMonthlyPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RbMonthlyPayment.Location = New System.Drawing.Point(192, 27)
        Me.RbMonthlyPayment.Margin = New System.Windows.Forms.Padding(8, 0, 24, 0)
        Me.RbMonthlyPayment.Name = "RbMonthlyPayment"
        Me.RbMonthlyPayment.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.RbMonthlyPayment.Size = New System.Drawing.Size(160, 40)
        Me.RbMonthlyPayment.TabIndex = 1
        Me.RbMonthlyPayment.Text = "Mensual"
        Me.RbMonthlyPayment.UseVisualStyleBackColor = True
        '
        'RbGroupPayment
        '
        Me.RbGroupPayment.Appearance = System.Windows.Forms.Appearance.Button
        Me.RbGroupPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbGroupPayment.Image = Global.GymPaymentControl.My.Resources.Resources.ic_family_group_53x30
        Me.RbGroupPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RbGroupPayment.Location = New System.Drawing.Point(24, 75)
        Me.RbGroupPayment.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.RbGroupPayment.Name = "RbGroupPayment"
        Me.RbGroupPayment.Padding = New System.Windows.Forms.Padding(25, 0, 25, 0)
        Me.RbGroupPayment.Size = New System.Drawing.Size(230, 40)
        Me.RbGroupPayment.TabIndex = 2
        Me.RbGroupPayment.Text = "Grupo familiar"
        Me.RbGroupPayment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RbGroupPayment.UseVisualStyleBackColor = True
        '
        'BtnAddGroup
        '
        Me.BtnAddGroup.Enabled = False
        Me.BtnAddGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_add_group_37x30
        Me.BtnAddGroup.Location = New System.Drawing.Point(262, 75)
        Me.BtnAddGroup.Margin = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.BtnAddGroup.Name = "BtnAddGroup"
        Me.BtnAddGroup.Size = New System.Drawing.Size(90, 40)
        Me.BtnAddGroup.TabIndex = 3
        Me.BtnAddGroup.UseVisualStyleBackColor = True
        '
        'GbListaGrupoFamiliar
        '
        Me.GbListaGrupoFamiliar.Controls.Add(Me.LblNumberMembers)
        Me.GbListaGrupoFamiliar.Controls.Add(Me.TxtListGroupsDailyPayment)
        Me.GbListaGrupoFamiliar.Controls.Add(Me.DgvListGroupsDailyPayment)
        Me.GbListaGrupoFamiliar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbListaGrupoFamiliar.Location = New System.Drawing.Point(24, 131)
        Me.GbListaGrupoFamiliar.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.GbListaGrupoFamiliar.Name = "GbListaGrupoFamiliar"
        Me.GbListaGrupoFamiliar.Padding = New System.Windows.Forms.Padding(0)
        Me.GbListaGrupoFamiliar.Size = New System.Drawing.Size(328, 186)
        Me.GbListaGrupoFamiliar.TabIndex = 4
        Me.GbListaGrupoFamiliar.TabStop = False
        Me.GbListaGrupoFamiliar.Text = "Lista vacia"
        '
        'LblNumberMembers
        '
        Me.LblNumberMembers.BackColor = System.Drawing.Color.Azure
        Me.LblNumberMembers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblNumberMembers.Location = New System.Drawing.Point(258, 26)
        Me.LblNumberMembers.Margin = New System.Windows.Forms.Padding(0)
        Me.LblNumberMembers.Name = "LblNumberMembers"
        Me.LblNumberMembers.Size = New System.Drawing.Size(70, 26)
        Me.LblNumberMembers.TabIndex = 2
        Me.LblNumberMembers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtListGroupsDailyPayment
        '
        Me.TxtListGroupsDailyPayment.BackColor = System.Drawing.Color.Azure
        Me.TxtListGroupsDailyPayment.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtListGroupsDailyPayment.Enabled = False
        Me.TxtListGroupsDailyPayment.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtListGroupsDailyPayment.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtListGroupsDailyPayment.Location = New System.Drawing.Point(0, 26)
        Me.TxtListGroupsDailyPayment.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtListGroupsDailyPayment.MaxLength = 30
        Me.TxtListGroupsDailyPayment.Name = "TxtListGroupsDailyPayment"
        Me.TxtListGroupsDailyPayment.Size = New System.Drawing.Size(255, 26)
        Me.TxtListGroupsDailyPayment.TabIndex = 0
        Me.TxtListGroupsDailyPayment.WordWrap = False
        '
        'DgvListGroupsDailyPayment
        '
        Me.DgvListGroupsDailyPayment.AllowUserToAddRows = False
        Me.DgvListGroupsDailyPayment.AllowUserToDeleteRows = False
        Me.DgvListGroupsDailyPayment.AllowUserToResizeColumns = False
        Me.DgvListGroupsDailyPayment.AllowUserToResizeRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        Me.DgvListGroupsDailyPayment.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DgvListGroupsDailyPayment.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvListGroupsDailyPayment.ColumnHeadersHeight = 4
        Me.DgvListGroupsDailyPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvListGroupsDailyPayment.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colIdDailyGroup, Me.colNameDailyGroup, Me.colNumMembers, Me.colMembersReg})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvListGroupsDailyPayment.DefaultCellStyle = DataGridViewCellStyle4
        Me.DgvListGroupsDailyPayment.Enabled = False
        Me.DgvListGroupsDailyPayment.Location = New System.Drawing.Point(0, 56)
        Me.DgvListGroupsDailyPayment.Margin = New System.Windows.Forms.Padding(0, 4, 0, 0)
        Me.DgvListGroupsDailyPayment.MultiSelect = False
        Me.DgvListGroupsDailyPayment.Name = "DgvListGroupsDailyPayment"
        Me.DgvListGroupsDailyPayment.ReadOnly = True
        Me.DgvListGroupsDailyPayment.RowHeadersWidth = 4
        Me.DgvListGroupsDailyPayment.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DgvListGroupsDailyPayment.RowTemplate.DividerHeight = 2
        Me.DgvListGroupsDailyPayment.RowTemplate.Height = 25
        Me.DgvListGroupsDailyPayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.DgvListGroupsDailyPayment.Size = New System.Drawing.Size(328, 130)
        Me.DgvListGroupsDailyPayment.TabIndex = 1
        '
        'colIdDailyGroup
        '
        Me.colIdDailyGroup.HeaderText = "colIdDailyGroup"
        Me.colIdDailyGroup.Name = "colIdDailyGroup"
        Me.colIdDailyGroup.ReadOnly = True
        Me.colIdDailyGroup.Visible = False
        '
        'colNameDailyGroup
        '
        Me.colNameDailyGroup.HeaderText = "colNameDailyGroup"
        Me.colNameDailyGroup.Name = "colNameDailyGroup"
        Me.colNameDailyGroup.ReadOnly = True
        Me.colNameDailyGroup.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.colNameDailyGroup.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.colNameDailyGroup.Width = 303
        '
        'colNumMembers
        '
        Me.colNumMembers.HeaderText = "colNumMembers"
        Me.colNumMembers.Name = "colNumMembers"
        Me.colNumMembers.ReadOnly = True
        Me.colNumMembers.Visible = False
        '
        'colMembersReg
        '
        Me.colMembersReg.HeaderText = "colMembersReg"
        Me.colMembersReg.Name = "colMembersReg"
        Me.colMembersReg.ReadOnly = True
        Me.colMembersReg.Visible = False
        '
        'BtnCancelRegistration
        '
        Me.BtnCancelRegistration.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCancelRegistration.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnCancelRegistration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnCancelRegistration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnCancelRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancelRegistration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelRegistration.ForeColor = System.Drawing.Color.Brown
        Me.BtnCancelRegistration.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_28x28
        Me.BtnCancelRegistration.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancelRegistration.Location = New System.Drawing.Point(837, 212)
        Me.BtnCancelRegistration.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCancelRegistration.Name = "BtnCancelRegistration"
        Me.BtnCancelRegistration.Padding = New System.Windows.Forms.Padding(0, 7, 0, 4)
        Me.BtnCancelRegistration.Size = New System.Drawing.Size(135, 75)
        Me.BtnCancelRegistration.TabIndex = 6
        Me.BtnCancelRegistration.Text = "&Cancelar"
        Me.BtnCancelRegistration.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancelRegistration.UseVisualStyleBackColor = False
        '
        'GbContacto
        '
        Me.GbContacto.Controls.Add(Me.TxtAddress)
        Me.GbContacto.Controls.Add(Me.TxtEmail)
        Me.GbContacto.Controls.Add(Me.TxtPhone)
        Me.GbContacto.Controls.Add(Me.LblDireccion)
        Me.GbContacto.Controls.Add(Me.LblEmail)
        Me.GbContacto.Controls.Add(Me.LblTelefono)
        Me.GbContacto.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbContacto.Location = New System.Drawing.Point(25, 266)
        Me.GbContacto.Margin = New System.Windows.Forms.Padding(0, 16, 0, 24)
        Me.GbContacto.Name = "GbContacto"
        Me.GbContacto.Padding = New System.Windows.Forms.Padding(0)
        Me.GbContacto.Size = New System.Drawing.Size(398, 279)
        Me.GbContacto.TabIndex = 1
        Me.GbContacto.TabStop = False
        Me.GbContacto.Text = "Datos de contacto :"
        '
        'TxtAddress
        '
        Me.TxtAddress.BackColor = System.Drawing.Color.Azure
        Me.TxtAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtAddress.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.TxtAddress.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtAddress.Location = New System.Drawing.Point(24, 175)
        Me.TxtAddress.Margin = New System.Windows.Forms.Padding(0, 8, 0, 24)
        Me.TxtAddress.MaxLength = 100
        Me.TxtAddress.Multiline = True
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtAddress.Size = New System.Drawing.Size(350, 80)
        Me.TxtAddress.TabIndex = 2
        '
        'TxtEmail
        '
        Me.TxtEmail.BackColor = System.Drawing.Color.Azure
        Me.TxtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower
        Me.TxtEmail.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.TxtEmail.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtEmail.Location = New System.Drawing.Point(24, 113)
        Me.TxtEmail.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtEmail.MaxLength = 50
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(350, 26)
        Me.TxtEmail.TabIndex = 1
        Me.TxtEmail.WordWrap = False
        '
        'TxtPhone
        '
        Me.TxtPhone.BackColor = System.Drawing.Color.Azure
        Me.TxtPhone.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.TxtPhone.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtPhone.Location = New System.Drawing.Point(24, 51)
        Me.TxtPhone.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtPhone.MaxLength = 15
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(350, 26)
        Me.TxtPhone.TabIndex = 0
        Me.TxtPhone.WordWrap = False
        '
        'LblDireccion
        '
        Me.LblDireccion.AutoSize = True
        Me.LblDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDireccion.Location = New System.Drawing.Point(24, 151)
        Me.LblDireccion.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblDireccion.Name = "LblDireccion"
        Me.LblDireccion.Size = New System.Drawing.Size(64, 16)
        Me.LblDireccion.TabIndex = 2
        Me.LblDireccion.Text = "Dirección"
        '
        'LblEmail
        '
        Me.LblEmail.AutoSize = True
        Me.LblEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEmail.Location = New System.Drawing.Point(21, 89)
        Me.LblEmail.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblEmail.Name = "LblEmail"
        Me.LblEmail.Size = New System.Drawing.Size(45, 16)
        Me.LblEmail.TabIndex = 1
        Me.LblEmail.Text = "E-Mail"
        '
        'LblTelefono
        '
        Me.LblTelefono.AutoSize = True
        Me.LblTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTelefono.Location = New System.Drawing.Point(21, 27)
        Me.LblTelefono.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblTelefono.Name = "LblTelefono"
        Me.LblTelefono.Size = New System.Drawing.Size(61, 16)
        Me.LblTelefono.TabIndex = 0
        Me.LblTelefono.Text = "Teléfono"
        '
        'GbDatos
        '
        Me.GbDatos.Controls.Add(Me.DtpBirthdate)
        Me.GbDatos.Controls.Add(Me.LblEdad)
        Me.GbDatos.Controls.Add(Me.TxtLastName)
        Me.GbDatos.Controls.Add(Me.TxtFirstName)
        Me.GbDatos.Controls.Add(Me.TxtCustomerAge)
        Me.GbDatos.Controls.Add(Me.LblFnacimiento)
        Me.GbDatos.Controls.Add(Me.LblApellido)
        Me.GbDatos.Controls.Add(Me.LblNombre)
        Me.GbDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GbDatos.Location = New System.Drawing.Point(25, 25)
        Me.GbDatos.Margin = New System.Windows.Forms.Padding(16, 16, 0, 0)
        Me.GbDatos.Name = "GbDatos"
        Me.GbDatos.Padding = New System.Windows.Forms.Padding(0)
        Me.GbDatos.Size = New System.Drawing.Size(398, 225)
        Me.GbDatos.TabIndex = 0
        Me.GbDatos.TabStop = False
        Me.GbDatos.Text = "Datos del cliente :"
        '
        'DtpBirthdate
        '
        Me.DtpBirthdate.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpBirthdate.CustomFormat = "' ' dd ' de  ' MMMM ' de  ' yyyy"
        Me.DtpBirthdate.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.DtpBirthdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpBirthdate.Location = New System.Drawing.Point(24, 175)
        Me.DtpBirthdate.Margin = New System.Windows.Forms.Padding(0, 8, 0, 24)
        Me.DtpBirthdate.Name = "DtpBirthdate"
        Me.DtpBirthdate.Size = New System.Drawing.Size(270, 26)
        Me.DtpBirthdate.TabIndex = 2
        '
        'LblEdad
        '
        Me.LblEdad.AutoSize = True
        Me.LblEdad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEdad.Location = New System.Drawing.Point(301, 151)
        Me.LblEdad.Margin = New System.Windows.Forms.Padding(0)
        Me.LblEdad.Name = "LblEdad"
        Me.LblEdad.Size = New System.Drawing.Size(40, 16)
        Me.LblEdad.TabIndex = 3
        Me.LblEdad.Text = "Edad"
        '
        'TxtLastName
        '
        Me.TxtLastName.BackColor = System.Drawing.Color.Azure
        Me.TxtLastName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLastName.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.TxtLastName.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtLastName.Location = New System.Drawing.Point(24, 113)
        Me.TxtLastName.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtLastName.MaxLength = 30
        Me.TxtLastName.Name = "TxtLastName"
        Me.TxtLastName.Size = New System.Drawing.Size(350, 26)
        Me.TxtLastName.TabIndex = 1
        Me.TxtLastName.WordWrap = False
        '
        'TxtFirstName
        '
        Me.TxtFirstName.BackColor = System.Drawing.Color.Azure
        Me.TxtFirstName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtFirstName.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFirstName.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtFirstName.Location = New System.Drawing.Point(24, 51)
        Me.TxtFirstName.Margin = New System.Windows.Forms.Padding(24, 8, 24, 0)
        Me.TxtFirstName.MaxLength = 30
        Me.TxtFirstName.Name = "TxtFirstName"
        Me.TxtFirstName.Size = New System.Drawing.Size(350, 26)
        Me.TxtFirstName.TabIndex = 0
        Me.TxtFirstName.WordWrap = False
        '
        'TxtCustomerAge
        '
        Me.TxtCustomerAge.BackColor = System.Drawing.Color.Azure
        Me.TxtCustomerAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCustomerAge.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.TxtCustomerAge.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtCustomerAge.Location = New System.Drawing.Point(304, 175)
        Me.TxtCustomerAge.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtCustomerAge.Name = "TxtCustomerAge"
        Me.TxtCustomerAge.Size = New System.Drawing.Size(70, 26)
        Me.TxtCustomerAge.TabIndex = 3
        Me.TxtCustomerAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblFnacimiento
        '
        Me.LblFnacimiento.AutoSize = True
        Me.LblFnacimiento.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFnacimiento.Location = New System.Drawing.Point(24, 151)
        Me.LblFnacimiento.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblFnacimiento.Name = "LblFnacimiento"
        Me.LblFnacimiento.Size = New System.Drawing.Size(135, 16)
        Me.LblFnacimiento.TabIndex = 2
        Me.LblFnacimiento.Text = "Fecha de Nacimiento"
        '
        'LblApellido
        '
        Me.LblApellido.AutoSize = True
        Me.LblApellido.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblApellido.Location = New System.Drawing.Point(24, 89)
        Me.LblApellido.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblApellido.Name = "LblApellido"
        Me.LblApellido.Size = New System.Drawing.Size(57, 16)
        Me.LblApellido.TabIndex = 1
        Me.LblApellido.Text = "Apellido"
        '
        'LblNombre
        '
        Me.LblNombre.AutoSize = True
        Me.LblNombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNombre.Location = New System.Drawing.Point(24, 27)
        Me.LblNombre.Margin = New System.Windows.Forms.Padding(24, 12, 0, 0)
        Me.LblNombre.Name = "LblNombre"
        Me.LblNombre.Size = New System.Drawing.Size(56, 16)
        Me.LblNombre.TabIndex = 0
        Me.LblNombre.Text = "Nombre"
        '
        'BtnUpdateCustomerData
        '
        Me.BtnUpdateCustomerData.BackColor = System.Drawing.SystemColors.Control
        Me.BtnUpdateCustomerData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnUpdateCustomerData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUpdateCustomerData.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdateCustomerData.ForeColor = System.Drawing.Color.Green
        Me.BtnUpdateCustomerData.Image = Global.GymPaymentControl.My.Resources.Resources.ic_update_28x27
        Me.BtnUpdateCustomerData.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUpdateCustomerData.Location = New System.Drawing.Point(837, 41)
        Me.BtnUpdateCustomerData.Margin = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.BtnUpdateCustomerData.Name = "BtnUpdateCustomerData"
        Me.BtnUpdateCustomerData.Padding = New System.Windows.Forms.Padding(0, 7, 0, 4)
        Me.BtnUpdateCustomerData.Size = New System.Drawing.Size(135, 75)
        Me.BtnUpdateCustomerData.TabIndex = 5
        Me.BtnUpdateCustomerData.Text = "&Actualizar"
        Me.BtnUpdateCustomerData.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnUpdateCustomerData.UseVisualStyleBackColor = False
        '
        'BtnSaveCustomerData
        '
        Me.BtnSaveCustomerData.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSaveCustomerData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnSaveCustomerData.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveCustomerData.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveCustomerData.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnSaveCustomerData.Image = Global.GymPaymentControl.My.Resources.Resources.ic_save_28x28
        Me.BtnSaveCustomerData.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSaveCustomerData.Location = New System.Drawing.Point(837, 41)
        Me.BtnSaveCustomerData.Margin = New System.Windows.Forms.Padding(24, 32, 16, 0)
        Me.BtnSaveCustomerData.Name = "BtnSaveCustomerData"
        Me.BtnSaveCustomerData.Padding = New System.Windows.Forms.Padding(0, 7, 0, 4)
        Me.BtnSaveCustomerData.Size = New System.Drawing.Size(135, 75)
        Me.BtnSaveCustomerData.TabIndex = 4
        Me.BtnSaveCustomerData.Text = "&Guardar"
        Me.BtnSaveCustomerData.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSaveCustomerData.UseVisualStyleBackColor = False
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'FrmNewModifyClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(997, 578)
        Me.Controls.Add(Me.GbOtros)
        Me.Controls.Add(Me.GbMetodoPago)
        Me.Controls.Add(Me.BtnCancelRegistration)
        Me.Controls.Add(Me.GbContacto)
        Me.Controls.Add(Me.GbDatos)
        Me.Controls.Add(Me.BtnSaveCustomerData)
        Me.Controls.Add(Me.BtnUpdateCustomerData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmNewModifyClient"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "REGISTRO Y ACTUALIZACIÓN DE USUARIOS"
        Me.GbOtros.ResumeLayout(False)
        Me.GbOtros.PerformLayout()
        Me.GbMetodoPago.ResumeLayout(False)
        Me.GbListaGrupoFamiliar.ResumeLayout(False)
        Me.GbListaGrupoFamiliar.PerformLayout()
        CType(Me.DgvListGroupsDailyPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GbContacto.ResumeLayout(False)
        Me.GbContacto.PerformLayout()
        Me.GbDatos.ResumeLayout(False)
        Me.GbDatos.PerformLayout()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As ToolTip
    Friend WithEvents GbOtros As GroupBox
    Friend WithEvents RbInactiveState As RadioButton
    Friend WithEvents RbActiveStatus As RadioButton
    Friend WithEvents LblEstadoCli As Label
    Friend WithEvents DtpRegistrationDate As DateTimePicker
    Friend WithEvents LblFinscripcion As Label
    Friend WithEvents GbMetodoPago As GroupBox
    Friend WithEvents RbDailyPayment As RadioButton
    Friend WithEvents RbMonthlyPayment As RadioButton
    Friend WithEvents RbGroupPayment As RadioButton
    Friend WithEvents BtnAddGroup As Button
    Friend WithEvents GbListaGrupoFamiliar As GroupBox
    Friend WithEvents LblNumberMembers As Label
    Friend WithEvents TxtListGroupsDailyPayment As TextBox
    Friend WithEvents DgvListGroupsDailyPayment As DataGridView
    Friend WithEvents BtnCancelRegistration As Button
    Friend WithEvents GbContacto As GroupBox
    Friend WithEvents TxtAddress As TextBox
    Friend WithEvents TxtEmail As TextBox
    Friend WithEvents TxtPhone As TextBox
    Friend WithEvents LblDireccion As Label
    Friend WithEvents LblEmail As Label
    Friend WithEvents LblTelefono As Label
    Friend WithEvents GbDatos As GroupBox
    Friend WithEvents DtpBirthdate As DateTimePicker
    Friend WithEvents LblEdad As Label
    Friend WithEvents TxtLastName As TextBox
    Friend WithEvents TxtFirstName As TextBox
    Friend WithEvents TxtCustomerAge As Label
    Friend WithEvents LblFnacimiento As Label
    Friend WithEvents LblApellido As Label
    Friend WithEvents LblNombre As Label
    Friend WithEvents BtnUpdateCustomerData As Button
    Friend WithEvents BtnSaveCustomerData As Button
    Friend WithEvents ErrorProvider As ErrorProvider
    Friend WithEvents colIdDailyGroup As DataGridViewTextBoxColumn
    Friend WithEvents colNameDailyGroup As DataGridViewTextBoxColumn
    Friend WithEvents colNumMembers As DataGridViewTextBoxColumn
    Friend WithEvents colMembersReg As DataGridViewTextBoxColumn
End Class
