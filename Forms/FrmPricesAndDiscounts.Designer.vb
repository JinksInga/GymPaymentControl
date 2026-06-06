<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPricesAndDiscounts
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
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
        Me.Panel = New System.Windows.Forms.Panel()
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
        CType(Me.DgvPriceList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel.SuspendLayout()
        CType(Me.NudMaximumAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudMinimumAge, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnModifyRate
        '
        Me.BtnModifyRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnModifyRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_modify_28x32
        Me.BtnModifyRate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnModifyRate.Location = New System.Drawing.Point(769, 94)
        Me.BtnModifyRate.Margin = New System.Windows.Forms.Padding(0, 0, 24, 0)
        Me.BtnModifyRate.Name = "BtnModifyRate"
        Me.BtnModifyRate.Padding = New System.Windows.Forms.Padding(25, 0, 20, 0)
        Me.BtnModifyRate.Size = New System.Drawing.Size(170, 46)
        Me.BtnModifyRate.TabIndex = 12
        Me.BtnModifyRate.Text = "&Modificar"
        Me.BtnModifyRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnModifyRate.UseVisualStyleBackColor = True
        '
        'DgvPriceList
        '
        Me.DgvPriceList.AllowUserToAddRows = False
        Me.DgvPriceList.AllowUserToDeleteRows = False
        Me.DgvPriceList.AllowUserToResizeColumns = False
        Me.DgvPriceList.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPriceList.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvPriceList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvPriceList.ColumnHeadersHeight = 35
        Me.DgvPriceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DgvPriceList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColId, Me.ColPaymentMethod, Me.ColPrice, Me.ColMinimumAge, Me.ColMaximumAge, Me.ColNumberMembers, Me.ColTotal, Me.ColDiscount, Me.ColToPay})
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.PaleTurquoise
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvPriceList.DefaultCellStyle = DataGridViewCellStyle11
        Me.DgvPriceList.Location = New System.Drawing.Point(33, 271)
        Me.DgvPriceList.Margin = New System.Windows.Forms.Padding(24, 12, 24, 0)
        Me.DgvPriceList.MultiSelect = False
        Me.DgvPriceList.Name = "DgvPriceList"
        Me.DgvPriceList.ReadOnly = True
        DataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.DgvPriceList.RowHeadersDefaultCellStyle = DataGridViewCellStyle12
        Me.DgvPriceList.RowHeadersWidth = 35
        Me.DgvPriceList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPriceList.RowsDefaultCellStyle = DataGridViewCellStyle13
        Me.DgvPriceList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgvPriceList.RowTemplate.DividerHeight = 2
        Me.DgvPriceList.RowTemplate.Height = 30
        Me.DgvPriceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvPriceList.Size = New System.Drawing.Size(906, 246)
        Me.DgvPriceList.TabIndex = 18
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
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColPaymentMethod.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColPaymentMethod.HeaderText = "MÉTODO PAGO"
        Me.ColPaymentMethod.Name = "ColPaymentMethod"
        Me.ColPaymentMethod.ReadOnly = True
        Me.ColPaymentMethod.Width = 130
        '
        'ColPrice
        '
        Me.ColPrice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColPrice.DataPropertyName = "Price"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColPrice.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColPrice.HeaderText = "PRECIO"
        Me.ColPrice.Name = "ColPrice"
        Me.ColPrice.ReadOnly = True
        Me.ColPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColMinimumAge
        '
        Me.ColMinimumAge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColMinimumAge.DataPropertyName = "MinimumAge"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColMinimumAge.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColMinimumAge.HeaderText = "EDAD MIN"
        Me.ColMinimumAge.Name = "ColMinimumAge"
        Me.ColMinimumAge.ReadOnly = True
        Me.ColMinimumAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColMaximumAge
        '
        Me.ColMaximumAge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColMaximumAge.DataPropertyName = "MaximumAge"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColMaximumAge.DefaultCellStyle = DataGridViewCellStyle6
        Me.ColMaximumAge.HeaderText = "EDAD MAX"
        Me.ColMaximumAge.Name = "ColMaximumAge"
        Me.ColMaximumAge.ReadOnly = True
        Me.ColMaximumAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColNumberMembers
        '
        Me.ColNumberMembers.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColNumberMembers.DataPropertyName = "NumberMembers"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColNumberMembers.DefaultCellStyle = DataGridViewCellStyle7
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
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColTotal.DefaultCellStyle = DataGridViewCellStyle8
        Me.ColTotal.HeaderText = "TOTAL"
        Me.ColTotal.Name = "ColTotal"
        Me.ColTotal.ReadOnly = True
        Me.ColTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'ColDiscount
        '
        Me.ColDiscount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ColDiscount.DataPropertyName = "Discount"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColDiscount.DefaultCellStyle = DataGridViewCellStyle9
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
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColToPay.DefaultCellStyle = DataGridViewCellStyle10
        Me.ColToPay.HeaderText = "A PAGAR"
        Me.ColToPay.Name = "ColToPay"
        Me.ColToPay.ReadOnly = True
        Me.ColToPay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Panel
        '
        Me.Panel.BackColor = System.Drawing.SystemColors.Control
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel.Controls.Add(Me.TxtToPay)
        Me.Panel.Controls.Add(Me.LblTotal)
        Me.Panel.Controls.Add(Me.TxtTotal)
        Me.Panel.Controls.Add(Me.LblPrice)
        Me.Panel.Controls.Add(Me.TxtPrice)
        Me.Panel.Controls.Add(Me.LblPaymentMethod_)
        Me.Panel.Controls.Add(Me.CmbPaymentMethod)
        Me.Panel.Controls.Add(Me.TxtDiscount)
        Me.Panel.Controls.Add(Me.LblToPay)
        Me.Panel.Controls.Add(Me.LblDiscount)
        Me.Panel.Controls.Add(Me.NudMaximumAge)
        Me.Panel.Controls.Add(Me.LblMaximumAge)
        Me.Panel.Controls.Add(Me.LblNumberOfMembers)
        Me.Panel.Controls.Add(Me.NudMinimumAge)
        Me.Panel.Controls.Add(Me.NudNumberMembers)
        Me.Panel.Controls.Add(Me.LblMinimumAge)
        Me.Panel.Controls.Add(Me.LblPaymentMethod)
        Me.Panel.Controls.Add(Me.LblNamePay)
        Me.Panel.Location = New System.Drawing.Point(33, 33)
        Me.Panel.Margin = New System.Windows.Forms.Padding(24, 24, 0, 0)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(677, 226)
        Me.Panel.TabIndex = 17
        '
        'TxtToPay
        '
        Me.TxtToPay.BackColor = System.Drawing.SystemColors.Window
        Me.TxtToPay.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtToPay.Enabled = False
        Me.TxtToPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToPay.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtToPay.Location = New System.Drawing.Point(140, 174)
        Me.TxtToPay.MaxLength = 10
        Me.TxtToPay.Name = "TxtToPay"
        Me.TxtToPay.Size = New System.Drawing.Size(170, 26)
        Me.TxtToPay.TabIndex = 4
        Me.TxtToPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtToPay.WordWrap = False
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(89, 104)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(43, 16)
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
        Me.TxtTotal.Location = New System.Drawing.Point(140, 98)
        Me.TxtTotal.MaxLength = 10
        Me.TxtTotal.Name = "TxtTotal"
        Me.TxtTotal.Size = New System.Drawing.Size(170, 26)
        Me.TxtTotal.TabIndex = 2
        Me.TxtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtTotal.WordWrap = False
        '
        'LblPrice
        '
        Me.LblPrice.AutoSize = True
        Me.LblPrice.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPrice.Location = New System.Drawing.Point(80, 66)
        Me.LblPrice.Name = "LblPrice"
        Me.LblPrice.Size = New System.Drawing.Size(52, 16)
        Me.LblPrice.TabIndex = 1
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
        Me.TxtPrice.Location = New System.Drawing.Point(140, 60)
        Me.TxtPrice.MaxLength = 10
        Me.TxtPrice.Name = "TxtPrice"
        Me.TxtPrice.Size = New System.Drawing.Size(170, 26)
        Me.TxtPrice.TabIndex = 1
        Me.TxtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtPrice.WordWrap = False
        '
        'LblPaymentMethod_
        '
        Me.LblPaymentMethod_.AutoSize = True
        Me.LblPaymentMethod_.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentMethod_.Location = New System.Drawing.Point(31, 27)
        Me.LblPaymentMethod_.Name = "LblPaymentMethod_"
        Me.LblPaymentMethod_.Size = New System.Drawing.Size(101, 16)
        Me.LblPaymentMethod_.TabIndex = 0
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
        Me.CmbPaymentMethod.Location = New System.Drawing.Point(140, 24)
        Me.CmbPaymentMethod.Name = "CmbPaymentMethod"
        Me.CmbPaymentMethod.Size = New System.Drawing.Size(500, 24)
        Me.CmbPaymentMethod.TabIndex = 0
        '
        'TxtDiscount
        '
        Me.TxtDiscount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtDiscount.Enabled = False
        Me.TxtDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDiscount.ForeColor = System.Drawing.Color.MediumBlue
        Me.TxtDiscount.Location = New System.Drawing.Point(140, 136)
        Me.TxtDiscount.MaxLength = 10
        Me.TxtDiscount.Name = "TxtDiscount"
        Me.TxtDiscount.Size = New System.Drawing.Size(170, 26)
        Me.TxtDiscount.TabIndex = 3
        Me.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtDiscount.WordWrap = False
        '
        'LblToPay
        '
        Me.LblToPay.AutoSize = True
        Me.LblToPay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToPay.Location = New System.Drawing.Point(70, 180)
        Me.LblToPay.Name = "LblToPay"
        Me.LblToPay.Size = New System.Drawing.Size(62, 16)
        Me.LblToPay.TabIndex = 4
        Me.LblToPay.Text = "A pagar"
        Me.LblToPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblDiscount
        '
        Me.LblDiscount.AutoSize = True
        Me.LblDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDiscount.Location = New System.Drawing.Point(51, 142)
        Me.LblDiscount.Name = "LblDiscount"
        Me.LblDiscount.Size = New System.Drawing.Size(81, 16)
        Me.LblDiscount.TabIndex = 3
        Me.LblDiscount.Text = "Descuento"
        Me.LblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NudMaximumAge
        '
        Me.NudMaximumAge.Enabled = False
        Me.NudMaximumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudMaximumAge.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudMaximumAge.Location = New System.Drawing.Point(470, 175)
        Me.NudMaximumAge.Maximum = New Decimal(New Integer() {17, 0, 0, 0})
        Me.NudMaximumAge.Name = "NudMaximumAge"
        Me.NudMaximumAge.Size = New System.Drawing.Size(170, 26)
        Me.NudMaximumAge.TabIndex = 7
        Me.NudMaximumAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblMaximumAge
        '
        Me.LblMaximumAge.AutoSize = True
        Me.LblMaximumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaximumAge.Location = New System.Drawing.Point(361, 180)
        Me.LblMaximumAge.Name = "LblMaximumAge"
        Me.LblMaximumAge.Size = New System.Drawing.Size(101, 16)
        Me.LblMaximumAge.TabIndex = 9
        Me.LblMaximumAge.Text = "Edad maxima"
        '
        'LblNumberOfMembers
        '
        Me.LblNumberOfMembers.AutoSize = True
        Me.LblNumberOfMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberOfMembers.Location = New System.Drawing.Point(355, 104)
        Me.LblNumberOfMembers.Name = "LblNumberOfMembers"
        Me.LblNumberOfMembers.Size = New System.Drawing.Size(107, 16)
        Me.LblNumberOfMembers.TabIndex = 7
        Me.LblNumberOfMembers.Text = "Num personas"
        Me.LblNumberOfMembers.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'NudMinimumAge
        '
        Me.NudMinimumAge.Enabled = False
        Me.NudMinimumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudMinimumAge.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudMinimumAge.Location = New System.Drawing.Point(470, 137)
        Me.NudMinimumAge.Maximum = New Decimal(New Integer() {16, 0, 0, 0})
        Me.NudMinimumAge.Name = "NudMinimumAge"
        Me.NudMinimumAge.Size = New System.Drawing.Size(170, 26)
        Me.NudMinimumAge.TabIndex = 6
        Me.NudMinimumAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'NudNumberMembers
        '
        Me.NudNumberMembers.Enabled = False
        Me.NudNumberMembers.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NudNumberMembers.ForeColor = System.Drawing.Color.MediumBlue
        Me.NudNumberMembers.Location = New System.Drawing.Point(470, 99)
        Me.NudNumberMembers.Maximum = New Decimal(New Integer() {9, 0, 0, 0})
        Me.NudNumberMembers.Name = "NudNumberMembers"
        Me.NudNumberMembers.Size = New System.Drawing.Size(170, 26)
        Me.NudNumberMembers.TabIndex = 5
        Me.NudNumberMembers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblMinimumAge
        '
        Me.LblMinimumAge.AutoSize = True
        Me.LblMinimumAge.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMinimumAge.Location = New System.Drawing.Point(365, 142)
        Me.LblMinimumAge.Name = "LblMinimumAge"
        Me.LblMinimumAge.Size = New System.Drawing.Size(97, 16)
        Me.LblMinimumAge.TabIndex = 8
        Me.LblMinimumAge.Text = "Edad minima"
        '
        'LblPaymentMethod
        '
        Me.LblPaymentMethod.BackColor = System.Drawing.Color.WhiteSmoke
        Me.LblPaymentMethod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPaymentMethod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentMethod.ForeColor = System.Drawing.Color.MediumBlue
        Me.LblPaymentMethod.Location = New System.Drawing.Point(470, 60)
        Me.LblPaymentMethod.Name = "LblPaymentMethod"
        Me.LblPaymentMethod.Size = New System.Drawing.Size(170, 26)
        Me.LblPaymentMethod.TabIndex = 6
        Me.LblPaymentMethod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblNamePay
        '
        Me.LblNamePay.AutoSize = True
        Me.LblNamePay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNamePay.Location = New System.Drawing.Point(360, 66)
        Me.LblNamePay.Name = "LblNamePay"
        Me.LblNamePay.Size = New System.Drawing.Size(102, 16)
        Me.LblNamePay.TabIndex = 5
        Me.LblNamePay.Text = "Nombre pago"
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
        Me.BtnCloseWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCloseWindow.Location = New System.Drawing.Point(769, 533)
        Me.BtnCloseWindow.Margin = New System.Windows.Forms.Padding(0, 16, 24, 24)
        Me.BtnCloseWindow.Name = "BtnCloseWindow"
        Me.BtnCloseWindow.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.BtnCloseWindow.Size = New System.Drawing.Size(170, 42)
        Me.BtnCloseWindow.TabIndex = 15
        Me.BtnCloseWindow.Text = "&Cerrar ventana"
        Me.BtnCloseWindow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCloseWindow.UseVisualStyleBackColor = False
        '
        'BtnDeleteRate
        '
        Me.BtnDeleteRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDeleteRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_delete_28x32
        Me.BtnDeleteRate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDeleteRate.Location = New System.Drawing.Point(769, 191)
        Me.BtnDeleteRate.Margin = New System.Windows.Forms.Padding(0, 0, 24, 0)
        Me.BtnDeleteRate.Name = "BtnDeleteRate"
        Me.BtnDeleteRate.Padding = New System.Windows.Forms.Padding(25, 0, 20, 0)
        Me.BtnDeleteRate.Size = New System.Drawing.Size(170, 46)
        Me.BtnDeleteRate.TabIndex = 14
        Me.BtnDeleteRate.Text = "&Eliminar"
        Me.BtnDeleteRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDeleteRate.UseVisualStyleBackColor = True
        '
        'BtnNewRate
        '
        Me.BtnNewRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_new_24x32
        Me.BtnNewRate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnNewRate.Location = New System.Drawing.Point(769, 33)
        Me.BtnNewRate.Margin = New System.Windows.Forms.Padding(0, 24, 24, 0)
        Me.BtnNewRate.Name = "BtnNewRate"
        Me.BtnNewRate.Padding = New System.Windows.Forms.Padding(25, 0, 30, 0)
        Me.BtnNewRate.Size = New System.Drawing.Size(170, 46)
        Me.BtnNewRate.TabIndex = 10
        Me.BtnNewRate.Text = "&Nuevo"
        Me.BtnNewRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnNewRate.UseVisualStyleBackColor = True
        '
        'BtnSaveRate
        '
        Me.BtnSaveRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_save_28x28
        Me.BtnSaveRate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSaveRate.Location = New System.Drawing.Point(769, 33)
        Me.BtnSaveRate.Margin = New System.Windows.Forms.Padding(0, 24, 24, 0)
        Me.BtnSaveRate.Name = "BtnSaveRate"
        Me.BtnSaveRate.Padding = New System.Windows.Forms.Padding(25, 0, 25, 0)
        Me.BtnSaveRate.Size = New System.Drawing.Size(170, 46)
        Me.BtnSaveRate.TabIndex = 16
        Me.BtnSaveRate.Text = "&Guardar"
        Me.BtnSaveRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSaveRate.UseVisualStyleBackColor = True
        Me.BtnSaveRate.Visible = False
        '
        'BtnUpdateRate
        '
        Me.BtnUpdateRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdateRate.Image = Global.GymPaymentControl.My.Resources.Resources.ic_update_28x27
        Me.BtnUpdateRate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUpdateRate.Location = New System.Drawing.Point(769, 33)
        Me.BtnUpdateRate.Margin = New System.Windows.Forms.Padding(0, 24, 24, 0)
        Me.BtnUpdateRate.Name = "BtnUpdateRate"
        Me.BtnUpdateRate.Padding = New System.Windows.Forms.Padding(18, 0, 18, 0)
        Me.BtnUpdateRate.Size = New System.Drawing.Size(170, 46)
        Me.BtnUpdateRate.TabIndex = 11
        Me.BtnUpdateRate.Text = "&Actualizar"
        Me.BtnUpdateRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUpdateRate.UseVisualStyleBackColor = True
        Me.BtnUpdateRate.Visible = False
        '
        'BtnCancelRegistration
        '
        Me.BtnCancelRegistration.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelRegistration.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_28x28
        Me.BtnCancelRegistration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnCancelRegistration.Location = New System.Drawing.Point(769, 119)
        Me.BtnCancelRegistration.Margin = New System.Windows.Forms.Padding(0, 0, 24, 0)
        Me.BtnCancelRegistration.Name = "BtnCancelRegistration"
        Me.BtnCancelRegistration.Padding = New System.Windows.Forms.Padding(25, 0, 20, 0)
        Me.BtnCancelRegistration.Size = New System.Drawing.Size(170, 46)
        Me.BtnCancelRegistration.TabIndex = 13
        Me.BtnCancelRegistration.Text = "&Cancelar"
        Me.BtnCancelRegistration.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnCancelRegistration.UseVisualStyleBackColor = True
        Me.BtnCancelRegistration.Visible = False
        '
        'FrmPricesAndDiscounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(972, 608)
        Me.Controls.Add(Me.DgvPriceList)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.BtnCloseWindow)
        Me.Controls.Add(Me.BtnModifyRate)
        Me.Controls.Add(Me.BtnCancelRegistration)
        Me.Controls.Add(Me.BtnDeleteRate)
        Me.Controls.Add(Me.BtnSaveRate)
        Me.Controls.Add(Me.BtnUpdateRate)
        Me.Controls.Add(Me.BtnNewRate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmPricesAndDiscounts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TABLA DE PRECIOS Y DESCUENTOS"
        CType(Me.DgvPriceList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        CType(Me.NudMaximumAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudMinimumAge, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NudNumberMembers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnModifyRate As Button
    Friend WithEvents DgvPriceList As DataGridView
    Friend WithEvents Panel As Panel
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
End Class
