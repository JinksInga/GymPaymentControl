<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPricesAndDiscounts
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
        Dim DataGridViewCellStyle40 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle41 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle51 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle42 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle43 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle44 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle45 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle46 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle47 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle48 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.BtnModifyRate = New System.Windows.Forms.Button()
        Me.DgvPriceList = New System.Windows.Forms.DataGridView()
        Me.ColId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPaymentMethod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMinimumAge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColMaximumAge = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNumberMembers = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDiscount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColToPay = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TxtToPay = New System.Windows.Forms.TextBox()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.TxtTotal = New System.Windows.Forms.TextBox()
        Me.LblPrice = New System.Windows.Forms.Label()
        Me.TxtPrice = New System.Windows.Forms.TextBox()
        Me.LblPaymentMethod_ = New System.Windows.Forms.Label()
        Me.CmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.TxtDiscount = New System.Windows.Forms.TextBox()
        Me.LblToPay = New System.Windows.Forms.Label()
        Me.LblDiscount = New System.Windows.Forms.Label()
        Me.NudMaximumAge = New System.Windows.Forms.NumericUpDown()
        Me.LblMaximumAge = New System.Windows.Forms.Label()
        Me.LblNumberOfMembers = New System.Windows.Forms.Label()
        Me.NudMinimumAge = New System.Windows.Forms.NumericUpDown()
        Me.NudNumberMembers = New System.Windows.Forms.NumericUpDown()
        Me.LblMinimumAge = New System.Windows.Forms.Label()
        Me.LblPaymentMethod = New System.Windows.Forms.Label()
        Me.LblNamePay = New System.Windows.Forms.Label()
        Me.BtnCloseWindow = New System.Windows.Forms.Button()
        Me.BtnDeleteRate = New System.Windows.Forms.Button()
        Me.BtnNewRate = New System.Windows.Forms.Button()
        Me.BtnSaveRate = New System.Windows.Forms.Button()
        Me.BtnUpdateRate = New System.Windows.Forms.Button()
        Me.BtnCancelRegistration = New System.Windows.Forms.Button()
        Me.PnlBotonera = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        CType(Me.DgvPriceList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudMaximumAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudMinimumAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlBotonera.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnModifyRate
        '
        Me.BtnModifyRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnModifyRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_modify_28x32
        Me.BtnModifyRate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnModifyRate.Location = New System.Drawing.Point(8, 128)
        Me.BtnModifyRate.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnModifyRate.Name = "BtnModifyRate"
        Me.BtnModifyRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnModifyRate.Size = New System.Drawing.Size(136, 64)
        Me.BtnModifyRate.TabIndex = 3
        Me.BtnModifyRate.Text = "&Modificar"
        Me.BtnModifyRate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnModifyRate.UseVisualStyleBackColor = True
        '
        'DgvPriceList
        '
        Me.DgvPriceList.AllowUserToAddRows = False
        Me.DgvPriceList.AllowUserToDeleteRows = False
        Me.DgvPriceList.AllowUserToResizeColumns = False
        Me.DgvPriceList.AllowUserToResizeRows = False
        DataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle40.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPriceList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle40
        DataGridViewCellStyle41.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle41.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle41.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle41.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle41.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle41.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvPriceList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle41
        Me.DgvPriceList.ColumnHeadersHeight = 35
        Me.DgvPriceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvPriceList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColId, Me.ColPaymentMethod, Me.ColPrice, Me.ColMinimumAge, Me.ColMaximumAge, Me.ColNumberMembers, Me.ColTotal, Me.ColDiscount, Me.ColToPay})
        DataGridViewCellStyle50.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle50.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle50.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle50.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle50.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle50.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle50.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvPriceList.DefaultCellStyle = DataGridViewCellStyle50
        Me.DgvPriceList.Location = New System.Drawing.Point(33, 222)
        Me.DgvPriceList.Margin = New System.Windows.Forms.Padding(24, 12, 24, 24)
        Me.DgvPriceList.MultiSelect = False
        Me.DgvPriceList.Name = "DgvPriceList"
        Me.DgvPriceList.ReadOnly = True
        DataGridViewCellStyle51.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle51.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle51.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle51.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle51.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvPriceList.RowHeadersDefaultCellStyle = DataGridViewCellStyle51
        Me.DgvPriceList.RowHeadersWidth = 35
        Me.DgvPriceList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle52.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPriceList.RowsDefaultCellStyle = DataGridViewCellStyle52
        Me.DgvPriceList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPriceList.RowTemplate.DividerHeight = 2
        Me.DgvPriceList.RowTemplate.Height = 30
        Me.DgvPriceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvPriceList.Size = New System.Drawing.Size(907, 280)
        Me.DgvPriceList.TabIndex = 4
        '
        'ColId
        '
        Me.ColId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColId.DataPropertyName = "IdTariff"
        Me.ColId.HeaderText = "id"
        Me.ColId.Name = "ColId"
        Me.ColId.ReadOnly = True
        Me.ColId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColId.Visible = False
        Me.ColId.Width = 20
        '
        'ColPaymentMethod
        '
        Me.ColPaymentMethod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColPaymentMethod.DataPropertyName = "PaymentMethod"
        DataGridViewCellStyle42.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColPaymentMethod.DefaultCellStyle = DataGridViewCellStyle42
        Me.ColPaymentMethod.HeaderText = "MÉTODO PAGO"
        Me.ColPaymentMethod.Name = "ColPaymentMethod"
        Me.ColPaymentMethod.ReadOnly = True
        Me.ColPaymentMethod.Width = 130
        '
        'ColPrice
        '
        Me.ColPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColPrice.DataPropertyName = "Price"
        DataGridViewCellStyle43.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColPrice.DefaultCellStyle = DataGridViewCellStyle43
        Me.ColPrice.HeaderText = "PRECIO"
        Me.ColPrice.Name = "ColPrice"
        Me.ColPrice.ReadOnly = True
        Me.ColPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColMinimumAge
        '
        Me.ColMinimumAge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColMinimumAge.DataPropertyName = "MinimumAge"
        DataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColMinimumAge.DefaultCellStyle = DataGridViewCellStyle44
        Me.ColMinimumAge.HeaderText = "EDAD MIN"
        Me.ColMinimumAge.Name = "ColMinimumAge"
        Me.ColMinimumAge.ReadOnly = True
        Me.ColMinimumAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColMaximumAge
        '
        Me.ColMaximumAge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColMaximumAge.DataPropertyName = "MaximumAge"
        DataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColMaximumAge.DefaultCellStyle = DataGridViewCellStyle45
        Me.ColMaximumAge.HeaderText = "EDAD MAX"
        Me.ColMaximumAge.Name = "ColMaximumAge"
        Me.ColMaximumAge.ReadOnly = True
        Me.ColMaximumAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColNumberMembers
        '
        Me.ColNumberMembers.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColNumberMembers.DataPropertyName = "NumberMembers"
        DataGridViewCellStyle46.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColNumberMembers.DefaultCellStyle = DataGridViewCellStyle46
        Me.ColNumberMembers.HeaderText = "Nº PERSONAS"
        Me.ColNumberMembers.Name = "ColNumberMembers"
        Me.ColNumberMembers.ReadOnly = True
        Me.ColNumberMembers.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColNumberMembers.Width = 110
        '
        'ColTotal
        '
        Me.ColTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColTotal.DataPropertyName = "Total"
        DataGridViewCellStyle47.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle47
        Me.ColTotal.HeaderText = "TOTAL"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColDiscount
        '
        Me.ColDiscount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColDiscount.DataPropertyName = "Discount"
        DataGridViewCellStyle48.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColDiscount.DefaultCellStyle = DataGridViewCellStyle48
        Me.ColDiscount.HeaderText = "DESCUENTO"
        Me.ColDiscount.Name = "ColDiscount"
        Me.ColDiscount.ReadOnly = True
        Me.ColDiscount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ColDiscount.Width = 110
        '
        'ColToPay
        '
        Me.ColToPay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColToPay.DataPropertyName = "TotalToPay"
        DataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColToPay.DefaultCellStyle = DataGridViewCellStyle49
        Me.ColToPay.HeaderText = "A PAGAR"
        Me.ColToPay.Name = "ColToPay"
        Me.ColToPay.ReadOnly = True
        Me.ColToPay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TxtToPay
        '
        Me.TxtToPay.BackColor = System.Drawing.SystemColors.Window
        Me.TxtToPay.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtToPay.Enabled = False
        Me.TxtToPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToPay.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtToPay.Location = New System.Drawing.Point(312, 59)
        Me.TxtToPay.Margin = New System.Windows.Forms.Padding(16, 8, 20, 0)
        Me.TxtToPay.MaxLength = 10
        Me.TxtToPay.Name = "TxtToPay"
        Me.TxtToPay.Size = New System.Drawing.Size(112, 26)
        Me.TxtToPay.TabIndex = 3
        Me.TxtToPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtToPay.WordWrap = False
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(258, 31)
        Me.LblTotal.Margin = New System.Windows.Forms.Padding(0)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(38, 16)
        Me.LblTotal.TabIndex = 2
        Me.LblTotal.Text = "Total"
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtTotal
        '
        Me.TxtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.TxtTotal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtTotal.Enabled = False
        Me.TxtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotal.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtTotal.Location = New System.Drawing.Point(312, 25)
        Me.TxtTotal.Margin = New System.Windows.Forms.Padding(16, 8, 20, 0)
        Me.TxtTotal.MaxLength = 10
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.Size = New System.Drawing.Size(112, 26)
        Me.TxtTotal.TabIndex = 2
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtTotal.WordWrap = False
        '
        'LblPrice
        '
        Me.LblPrice.AutoSize = True
        Me.LblPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPrice.Location = New System.Drawing.Point(42, 31)
        Me.LblPrice.Margin = New System.Windows.Forms.Padding(0)
        Me.LblPrice.Name = "LblPrice"
        Me.LblPrice.Size = New System.Drawing.Size(46, 16)
        Me.LblPrice.TabIndex = 0
        Me.LblPrice.Text = "Precio"
        Me.LblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtPrice
        '
        Me.TxtPrice.BackColor = System.Drawing.SystemColors.Window
        Me.TxtPrice.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtPrice.Enabled = False
        Me.TxtPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrice.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtPrice.Location = New System.Drawing.Point(104, 25)
        Me.TxtPrice.Margin = New System.Windows.Forms.Padding(16, 8, 16, 0)
        Me.TxtPrice.MaxLength = 10
        Me.TxtPrice.Name = "TxtPrice"
        Me.TxtPrice.Size = New System.Drawing.Size(112, 26)
        Me.TxtPrice.TabIndex = 0
        Me.TxtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtPrice.WordWrap = False
        '
        'LblPaymentMethod_
        '
        Me.LblPaymentMethod_.AutoSize = True
        Me.LblPaymentMethod_.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentMethod_.Location = New System.Drawing.Point(39, 28)
        Me.LblPaymentMethod_.Margin = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.LblPaymentMethod_.Name = "LblPaymentMethod_"
        Me.LblPaymentMethod_.Size = New System.Drawing.Size(89, 16)
        Me.LblPaymentMethod_.TabIndex = 1
        Me.LblPaymentMethod_.Text = "Tipo de pago"
        Me.LblPaymentMethod_.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmbPaymentMethod
        '
        Me.CmbPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbPaymentMethod.Enabled = False
        Me.CmbPaymentMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmbPaymentMethod.ForeColor = System.Drawing.Color.MediumBlue
        Me.CmbPaymentMethod.FormattingEnabled = True
        Me.CmbPaymentMethod.IntegralHeight = False
        Me.CmbPaymentMethod.Items.AddRange(New Object() {"", "   CLASES SUELTAS", "   DESCUENTO POR EDAD", "   GRUPO FAMILIAR", "   MENSUALIDAD + IMPLEMENTOS"})
        Me.CmbPaymentMethod.Location = New System.Drawing.Point(144, 25)
        Me.CmbPaymentMethod.Margin = New System.Windows.Forms.Padding(16, 8, 16, 0)
        Me.CmbPaymentMethod.Name = "CmbPaymentMethod"
        Me.CmbPaymentMethod.Size = New System.Drawing.Size(280, 24)
        Me.CmbPaymentMethod.TabIndex = 0
        '
        'TxtDiscount
        '
        Me.TxtDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDiscount.Enabled = False
        Me.TxtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDiscount.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtDiscount.Location = New System.Drawing.Point(104, 59)
        Me.TxtDiscount.Margin = New System.Windows.Forms.Padding(16, 8, 16, 0)
        Me.TxtDiscount.MaxLength = 10
        Me.TxtDiscount.Name = "TxtDiscount"
        Me.TxtDiscount.Size = New System.Drawing.Size(112, 26)
        Me.TxtDiscount.TabIndex = 1
        Me.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtDiscount.WordWrap = False
        '
        'LblToPay
        '
        Me.LblToPay.AutoSize = True
        Me.LblToPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToPay.Location = New System.Drawing.Point(241, 64)
        Me.LblToPay.Margin = New System.Windows.Forms.Padding(0)
        Me.LblToPay.Name = "LblToPay"
        Me.LblToPay.Size = New System.Drawing.Size(55, 16)
        Me.LblToPay.TabIndex = 3
        Me.LblToPay.Text = "A pagar"
        Me.LblToPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblDiscount
        '
        Me.LblDiscount.AutoSize = True
        Me.LblDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDiscount.Location = New System.Drawing.Point(16, 65)
        Me.LblDiscount.Margin = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.LblDiscount.Name = "LblDiscount"
        Me.LblDiscount.Size = New System.Drawing.Size(72, 16)
        Me.LblDiscount.TabIndex = 1
        Me.LblDiscount.Text = "Descuento"
        Me.LblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NudMaximumAge
        '
        Me.NudMaximumAge.Enabled = False
        Me.NudMaximumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudMaximumAge.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudMaximumAge.Location = New System.Drawing.Point(308, 59)
        Me.NudMaximumAge.Margin = New System.Windows.Forms.Padding(16, 8, 24, 16)
        Me.NudMaximumAge.Maximum = New Decimal(New Integer() {17, 0, 0, 0})
        Me.NudMaximumAge.Name = "NudMaximumAge"
        Me.NudMaximumAge.Size = New System.Drawing.Size(112, 26)
        Me.NudMaximumAge.TabIndex = 2
        Me.NudMaximumAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblMaximumAge
        '
        Me.LblMaximumAge.AutoSize = True
        Me.LblMaximumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaximumAge.Location = New System.Drawing.Point(202, 65)
        Me.LblMaximumAge.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMaximumAge.Name = "LblMaximumAge"
        Me.LblMaximumAge.Size = New System.Drawing.Size(90, 16)
        Me.LblMaximumAge.TabIndex = 2
        Me.LblMaximumAge.Text = "Edad maxima"
        Me.LblMaximumAge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblNumberOfMembers
        '
        Me.LblNumberOfMembers.AutoSize = True
        Me.LblNumberOfMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberOfMembers.Location = New System.Drawing.Point(28, 31)
        Me.LblNumberOfMembers.Margin = New System.Windows.Forms.Padding(28, 0, 0, 0)
        Me.LblNumberOfMembers.Name = "LblNumberOfMembers"
        Me.LblNumberOfMembers.Size = New System.Drawing.Size(95, 16)
        Me.LblNumberOfMembers.TabIndex = 0
        Me.LblNumberOfMembers.Text = "Num personas"
        Me.LblNumberOfMembers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NudMinimumAge
        '
        Me.NudMinimumAge.Enabled = False
        Me.NudMinimumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudMinimumAge.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudMinimumAge.Location = New System.Drawing.Point(308, 25)
        Me.NudMinimumAge.Margin = New System.Windows.Forms.Padding(16, 8, 24, 0)
        Me.NudMinimumAge.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.NudMinimumAge.Name = "NudMinimumAge"
        Me.NudMinimumAge.Size = New System.Drawing.Size(112, 26)
        Me.NudMinimumAge.TabIndex = 1
        Me.NudMinimumAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NudNumberMembers
        '
        Me.NudNumberMembers.Enabled = False
        Me.NudNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudNumberMembers.Location = New System.Drawing.Point(24, 59)
        Me.NudNumberMembers.Margin = New System.Windows.Forms.Padding(24, 16, 16, 16)
        Me.NudNumberMembers.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NudNumberMembers.Name = "NudNumberMembers"
        Me.NudNumberMembers.Size = New System.Drawing.Size(112, 26)
        Me.NudNumberMembers.TabIndex = 0
        Me.NudNumberMembers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblMinimumAge
        '
        Me.LblMinimumAge.AutoSize = True
        Me.LblMinimumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMinimumAge.Location = New System.Drawing.Point(206, 31)
        Me.LblMinimumAge.Margin = New System.Windows.Forms.Padding(0)
        Me.LblMinimumAge.Name = "LblMinimumAge"
        Me.LblMinimumAge.Size = New System.Drawing.Size(86, 16)
        Me.LblMinimumAge.TabIndex = 1
        Me.LblMinimumAge.Text = "Edad minima"
        Me.LblMinimumAge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblPaymentMethod
        '
        Me.LblPaymentMethod.BackColor = System.Drawing.SystemColors.Window
        Me.LblPaymentMethod.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblPaymentMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentMethod.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblPaymentMethod.Location = New System.Drawing.Point(607, 28)
        Me.LblPaymentMethod.Margin = New System.Windows.Forms.Padding(16, 8, 16, 16)
        Me.LblPaymentMethod.Name = "LblPaymentMethod"
        Me.LblPaymentMethod.Size = New System.Drawing.Size(280, 24)
        Me.LblPaymentMethod.TabIndex = 0
        Me.LblPaymentMethod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNamePay
        '
        Me.LblNamePay.AutoSize = True
        Me.LblNamePay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNamePay.Location = New System.Drawing.Point(478, 31)
        Me.LblNamePay.Margin = New System.Windows.Forms.Padding(16, 0, 0, 0)
        Me.LblNamePay.Name = "LblNamePay"
        Me.LblNamePay.Size = New System.Drawing.Size(113, 16)
        Me.LblNamePay.TabIndex = 2
        Me.LblNamePay.Text = "Nombre del pago"
        Me.LblNamePay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'BtnCloseWindow
        '
        Me.BtnCloseWindow.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCloseWindow.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnCloseWindow.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnCloseWindow.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnCloseWindow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCloseWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCloseWindow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCloseWindow.Image = Global.GymPaymentControl.My.Resources.Resources.ic_close_22x22
        Me.BtnCloseWindow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCloseWindow.Location = New System.Drawing.Point(8, 436)
        Me.BtnCloseWindow.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCloseWindow.Name = "BtnCloseWindow"
        Me.BtnCloseWindow.Padding = New System.Windows.Forms.Padding(0, 4, 0, 4)
        Me.BtnCloseWindow.Size = New System.Drawing.Size(136, 64)
        Me.BtnCloseWindow.TabIndex = 6
        Me.BtnCloseWindow.Text = "&Cerrar ventana"
        Me.BtnCloseWindow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCloseWindow.UseVisualStyleBackColor = False
        '
        'BtnDeleteRate
        '
        Me.BtnDeleteRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_delete_28x32
        Me.BtnDeleteRate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnDeleteRate.Location = New System.Drawing.Point(8, 208)
        Me.BtnDeleteRate.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnDeleteRate.Name = "BtnDeleteRate"
        Me.BtnDeleteRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnDeleteRate.Size = New System.Drawing.Size(136, 64)
        Me.BtnDeleteRate.TabIndex = 5
        Me.BtnDeleteRate.Text = "&Eliminar"
        Me.BtnDeleteRate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnDeleteRate.UseVisualStyleBackColor = True
        '
        'BtnNewRate
        '
        Me.BtnNewRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_24x32
        Me.BtnNewRate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnNewRate.Location = New System.Drawing.Point(8, 48)
        Me.BtnNewRate.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
        Me.BtnNewRate.Name = "BtnNewRate"
        Me.BtnNewRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnNewRate.Size = New System.Drawing.Size(136, 64)
        Me.BtnNewRate.TabIndex = 0
        Me.BtnNewRate.Text = "&Nuevo"
        Me.BtnNewRate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnNewRate.UseVisualStyleBackColor = True
        '
        'BtnSaveRate
        '
        Me.BtnSaveRate.Enabled = False
        Me.BtnSaveRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_save_28x28
        Me.BtnSaveRate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnSaveRate.Location = New System.Drawing.Point(8, 48)
        Me.BtnSaveRate.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
        Me.BtnSaveRate.Name = "BtnSaveRate"
        Me.BtnSaveRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnSaveRate.Size = New System.Drawing.Size(136, 64)
        Me.BtnSaveRate.TabIndex = 1
        Me.BtnSaveRate.Text = "&Guardar"
        Me.BtnSaveRate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnSaveRate.UseVisualStyleBackColor = True
        Me.BtnSaveRate.Visible = False
        '
        'BtnUpdateRate
        '
        Me.BtnUpdateRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdateRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_update_28x27
        Me.BtnUpdateRate.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnUpdateRate.Location = New System.Drawing.Point(8, 48)
        Me.BtnUpdateRate.Margin = New System.Windows.Forms.Padding(8, 48, 8, 0)
        Me.BtnUpdateRate.Name = "BtnUpdateRate"
        Me.BtnUpdateRate.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnUpdateRate.Size = New System.Drawing.Size(136, 64)
        Me.BtnUpdateRate.TabIndex = 2
        Me.BtnUpdateRate.Text = "&Actualizar"
        Me.BtnUpdateRate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnUpdateRate.UseVisualStyleBackColor = True
        Me.BtnUpdateRate.Visible = False
        '
        'BtnCancelRegistration
        '
        Me.BtnCancelRegistration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelRegistration.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_28x28
        Me.BtnCancelRegistration.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancelRegistration.Location = New System.Drawing.Point(8, 128)
        Me.BtnCancelRegistration.Margin = New System.Windows.Forms.Padding(8, 16, 8, 0)
        Me.BtnCancelRegistration.Name = "BtnCancelRegistration"
        Me.BtnCancelRegistration.Padding = New System.Windows.Forms.Padding(0, 4, 0, 2)
        Me.BtnCancelRegistration.Size = New System.Drawing.Size(136, 64)
        Me.BtnCancelRegistration.TabIndex = 4
        Me.BtnCancelRegistration.Text = "&Cancelar"
        Me.BtnCancelRegistration.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancelRegistration.UseVisualStyleBackColor = True
        Me.BtnCancelRegistration.Visible = False
        '
        'PnlBotonera
        '
        Me.PnlBotonera.AutoSize = True
        Me.PnlBotonera.BackColor = System.Drawing.Color.Silver
        Me.PnlBotonera.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PnlBotonera.Controls.Add(Me.BtnDeleteRate)
        Me.PnlBotonera.Controls.Add(Me.BtnCloseWindow)
        Me.PnlBotonera.Controls.Add(Me.BtnNewRate)
        Me.PnlBotonera.Controls.Add(Me.BtnModifyRate)
        Me.PnlBotonera.Controls.Add(Me.BtnSaveRate)
        Me.PnlBotonera.Controls.Add(Me.BtnUpdateRate)
        Me.PnlBotonera.Controls.Add(Me.BtnCancelRegistration)
        Me.PnlBotonera.Dock = System.Windows.Forms.DockStyle.Right
        Me.PnlBotonera.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlBotonera.Location = New System.Drawing.Point(972, 0)
        Me.PnlBotonera.Margin = New System.Windows.Forms.Padding(8, 24, 24, 0)
        Me.PnlBotonera.Name = "PnlBotonera"
        Me.PnlBotonera.Size = New System.Drawing.Size(156, 535)
        Me.PnlBotonera.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CmbPaymentMethod)
        Me.GroupBox1.Controls.Add(Me.LblNamePay)
        Me.GroupBox1.Controls.Add(Me.LblPaymentMethod)
        Me.GroupBox1.Controls.Add(Me.LblPaymentMethod_)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(33, 25)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(24, 16, 24, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 73)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Descripción"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.NudNumberMembers)
        Me.GroupBox2.Controls.Add(Me.LblMinimumAge)
        Me.GroupBox2.Controls.Add(Me.NudMinimumAge)
        Me.GroupBox2.Controls.Add(Me.LblNumberOfMembers)
        Me.GroupBox2.Controls.Add(Me.LblMaximumAge)
        Me.GroupBox2.Controls.Add(Me.NudMaximumAge)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(33, 106)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(24, 8, 0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.Size = New System.Drawing.Size(444, 101)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Rango"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TxtTotal)
        Me.GroupBox3.Controls.Add(Me.LblDiscount)
        Me.GroupBox3.Controls.Add(Me.TxtToPay)
        Me.GroupBox3.Controls.Add(Me.LblToPay)
        Me.GroupBox3.Controls.Add(Me.LblTotal)
        Me.GroupBox3.Controls.Add(Me.TxtDiscount)
        Me.GroupBox3.Controls.Add(Me.TxtPrice)
        Me.GroupBox3.Controls.Add(Me.LblPrice)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(496, 106)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(0, 8, 24, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.Size = New System.Drawing.Size(444, 101)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Precio y descuento"
        '
        'FrmPricesAndDiscounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1128, 535)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PnlBotonera)
        Me.Controls.Add(Me.DgvPriceList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmPricesAndDiscounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TABLA DE PRECIOS Y DESCUENTOS"
        CType(Me.DgvPriceList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudMaximumAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudMinimumAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlBotonera.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnModifyRate As Button
    Friend WithEvents DgvPriceList As DataGridView
    Friend WithEvents TxtToPay As TextBox
    Friend WithEvents LblTotal As Label
    Friend WithEvents TxtTotal As TextBox
    Friend WithEvents LblPrice As Label
    Friend WithEvents TxtPrice As TextBox
    Friend WithEvents LblPaymentMethod_ As Label
    Friend WithEvents CmbPaymentMethod As ComboBox
    Friend WithEvents TxtDiscount As TextBox
    Friend WithEvents LblToPay As Label
    Friend WithEvents LblDiscount As Label
    Friend WithEvents NudMaximumAge As NumericUpDown
    Friend WithEvents LblMaximumAge As Label
    Friend WithEvents LblNumberOfMembers As Label
    Friend WithEvents NudMinimumAge As NumericUpDown
    Friend WithEvents NudNumberMembers As NumericUpDown
    Friend WithEvents LblMinimumAge As Label
    Friend WithEvents LblPaymentMethod As Label
    Friend WithEvents LblNamePay As Label
    Friend WithEvents BtnCloseWindow As Button
    Friend WithEvents BtnDeleteRate As Button
    Friend WithEvents BtnNewRate As Button
    Friend WithEvents BtnSaveRate As Button
    Friend WithEvents BtnUpdateRate As Button
    Friend WithEvents BtnCancelRegistration As Button
    Friend WithEvents ColId As DataGridViewTextBoxColumn
    Friend WithEvents ColPaymentMethod As DataGridViewTextBoxColumn
    Friend WithEvents ColPrice As DataGridViewTextBoxColumn
    Friend WithEvents ColMinimumAge As DataGridViewTextBoxColumn
    Friend WithEvents ColMaximumAge As DataGridViewTextBoxColumn
    Friend WithEvents ColNumberMembers As DataGridViewTextBoxColumn
    Friend WithEvents ColTotal As DataGridViewTextBoxColumn
    Friend WithEvents ColDiscount As DataGridViewTextBoxColumn
    Friend WithEvents ColToPay As DataGridViewTextBoxColumn
    Friend WithEvents PnlBotonera As Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents GroupBox3 As GroupBox
End Class
