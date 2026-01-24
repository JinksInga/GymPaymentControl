<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCollectMembership
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ChkFdpPgs = New System.Windows.Forms.CheckBox()
        Me.ChkMtdPgs = New System.Windows.Forms.CheckBox()
        Me.TxtDetailMethod = New System.Windows.Forms.TextBox()
        Me.LblFdpPgs = New System.Windows.Forms.Label()
        Me.LblFrmPgs = New System.Windows.Forms.Label()
        Me.LblMtdPgs = New System.Windows.Forms.Label()
        Me.DtpFdpPgs = New System.Windows.Forms.DateTimePicker()
        Me.CmbFrmPgs = New System.Windows.Forms.ComboBox()
        Me.CmbMtdPgs = New System.Windows.Forms.ComboBox()
        Me.LblFdiPgs = New System.Windows.Forms.Label()
        Me.LblNumber_Of_Days = New System.Windows.Forms.Label()
        Me.LblPrice_Day = New System.Windows.Forms.Label()
        Me.DtpFdiPgs = New System.Windows.Forms.DateTimePicker()
        Me.LblTotal_ = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.LblPrcPgs = New System.Windows.Forms.Label()
        Me.LblTotal_To_Pay = New System.Windows.Forms.Label()
        Me.LblDscPgs = New System.Windows.Forms.Label()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.GroupBox = New System.Windows.Forms.GroupBox()
        Me.ChkFdiPgs = New System.Windows.Forms.CheckBox()
        Me.LblPriceDay = New System.Windows.Forms.Label()
        Me.TxtDscPgs = New System.Windows.Forms.TextBox()
        Me.LblNumberOfDays = New System.Windows.Forms.Label()
        Me.LblTotalToPay = New System.Windows.Forms.Label()
        Me.TxtPrcPgs = New System.Windows.Forms.TextBox()
        Me.LblDisplayName = New System.Windows.Forms.Label()
        Me.BtnCancelPayment = New System.Windows.Forms.Button()
        Me.BtnPayMonth = New System.Windows.Forms.Button()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.Panel.SuspendLayout()
        Me.GroupBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ChkFdpPgs)
        Me.GroupBox1.Controls.Add(Me.ChkMtdPgs)
        Me.GroupBox1.Controls.Add(Me.TxtDetailMethod)
        Me.GroupBox1.Controls.Add(Me.LblFdpPgs)
        Me.GroupBox1.Controls.Add(Me.LblFrmPgs)
        Me.GroupBox1.Controls.Add(Me.LblMtdPgs)
        Me.GroupBox1.Controls.Add(Me.DtpFdpPgs)
        Me.GroupBox1.Controls.Add(Me.CmbFrmPgs)
        Me.GroupBox1.Controls.Add(Me.CmbMtdPgs)
        Me.GroupBox1.Location = New System.Drawing.Point(333, 16)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(24, 16, 24, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.Size = New System.Drawing.Size(287, 285)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'ChkFdpPgs
        '
        Me.ChkFdpPgs.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkFdpPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFdpPgs.Location = New System.Drawing.Point(249, 45)
        Me.ChkFdpPgs.Margin = New System.Windows.Forms.Padding(4, 0, 12, 0)
        Me.ChkFdpPgs.Name = "ChkFdpPgs"
        Me.ChkFdpPgs.Size = New System.Drawing.Size(26, 26)
        Me.ChkFdpPgs.TabIndex = 3
        Me.ChkFdpPgs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkFdpPgs.UseVisualStyleBackColor = True
        '
        'ChkMtdPgs
        '
        Me.ChkMtdPgs.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkMtdPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkMtdPgs.Location = New System.Drawing.Point(249, 177)
        Me.ChkMtdPgs.Margin = New System.Windows.Forms.Padding(4, 0, 12, 0)
        Me.ChkMtdPgs.Name = "ChkMtdPgs"
        Me.ChkMtdPgs.Size = New System.Drawing.Size(26, 26)
        Me.ChkMtdPgs.TabIndex = 4
        Me.ChkMtdPgs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkMtdPgs.UseVisualStyleBackColor = True
        '
        'TxtDetailMethod
        '
        Me.TxtDetailMethod.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDetailMethod.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDetailMethod.Location = New System.Drawing.Point(21, 205)
        Me.TxtDetailMethod.Margin = New System.Windows.Forms.Padding(0, 0, 0, 16)
        Me.TxtDetailMethod.Multiline = True
        Me.TxtDetailMethod.Name = "TxtDetailMethod"
        Me.TxtDetailMethod.ReadOnly = True
        Me.TxtDetailMethod.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtDetailMethod.Size = New System.Drawing.Size(224, 64)
        Me.TxtDetailMethod.TabIndex = 3
        '
        'LblFdpPgs
        '
        Me.LblFdpPgs.AutoSize = True
        Me.LblFdpPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFdpPgs.Location = New System.Drawing.Point(18, 21)
        Me.LblFdpPgs.Margin = New System.Windows.Forms.Padding(16, 8, 0, 0)
        Me.LblFdpPgs.Name = "LblFdpPgs"
        Me.LblFdpPgs.Size = New System.Drawing.Size(99, 16)
        Me.LblFdpPgs.TabIndex = 4
        Me.LblFdpPgs.Text = "Fecha de pago"
        Me.LblFdpPgs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblFrmPgs
        '
        Me.LblFrmPgs.AutoSize = True
        Me.LblFrmPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFrmPgs.Location = New System.Drawing.Point(18, 87)
        Me.LblFrmPgs.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblFrmPgs.Name = "LblFrmPgs"
        Me.LblFrmPgs.Size = New System.Drawing.Size(100, 16)
        Me.LblFrmPgs.TabIndex = 5
        Me.LblFrmPgs.Text = "Forma de pago"
        Me.LblFrmPgs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblMtdPgs
        '
        Me.LblMtdPgs.AutoSize = True
        Me.LblMtdPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMtdPgs.Location = New System.Drawing.Point(18, 153)
        Me.LblMtdPgs.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblMtdPgs.Name = "LblMtdPgs"
        Me.LblMtdPgs.Size = New System.Drawing.Size(107, 16)
        Me.LblMtdPgs.TabIndex = 6
        Me.LblMtdPgs.Text = "Método de pago"
        Me.LblMtdPgs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtpFdpPgs
        '
        Me.DtpFdpPgs.CustomFormat = "dd 'de' MMMM 'de' yyyy"
        Me.DtpFdpPgs.Enabled = False
        Me.DtpFdpPgs.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.DtpFdpPgs.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpFdpPgs.Location = New System.Drawing.Point(21, 45)
        Me.DtpFdpPgs.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.DtpFdpPgs.Name = "DtpFdpPgs"
        Me.DtpFdpPgs.Size = New System.Drawing.Size(224, 26)
        Me.DtpFdpPgs.TabIndex = 0
        '
        'CmbFrmPgs
        '
        Me.CmbFrmPgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbFrmPgs.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.CmbFrmPgs.ForeColor = System.Drawing.Color.MediumBlue
        Me.CmbFrmPgs.FormattingEnabled = True
        Me.CmbFrmPgs.ItemHeight = 18
        Me.CmbFrmPgs.Items.AddRange(New Object() {"EFECTIVO", "BIZUM", "TARJETA", "TRANSFERENCIA"})
        Me.CmbFrmPgs.Location = New System.Drawing.Point(21, 111)
        Me.CmbFrmPgs.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.CmbFrmPgs.Name = "CmbFrmPgs"
        Me.CmbFrmPgs.Size = New System.Drawing.Size(224, 26)
        Me.CmbFrmPgs.TabIndex = 1
        '
        'CmbMtdPgs
        '
        Me.CmbMtdPgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbMtdPgs.Enabled = False
        Me.CmbMtdPgs.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.CmbMtdPgs.ForeColor = System.Drawing.Color.MediumBlue
        Me.CmbMtdPgs.FormattingEnabled = True
        Me.CmbMtdPgs.IntegralHeight = False
        Me.CmbMtdPgs.Items.AddRange(New Object() {"BONO", "DIARIO", "MENSUAL", "GRUPO FAMILIAR"})
        Me.CmbMtdPgs.Location = New System.Drawing.Point(21, 177)
        Me.CmbMtdPgs.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.CmbMtdPgs.Name = "CmbMtdPgs"
        Me.CmbMtdPgs.Size = New System.Drawing.Size(224, 26)
        Me.CmbMtdPgs.TabIndex = 2
        '
        'LblFdiPgs
        '
        Me.LblFdiPgs.AutoSize = True
        Me.LblFdiPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFdiPgs.Location = New System.Drawing.Point(16, 21)
        Me.LblFdiPgs.Margin = New System.Windows.Forms.Padding(16, 8, 0, 0)
        Me.LblFdiPgs.Name = "LblFdiPgs"
        Me.LblFdiPgs.Size = New System.Drawing.Size(130, 16)
        Me.LblFdiPgs.TabIndex = 7
        Me.LblFdiPgs.Text = "Fecha inicio  de mes"
        Me.LblFdiPgs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblNumber_Of_Days
        '
        Me.LblNumber_Of_Days.AutoSize = True
        Me.LblNumber_Of_Days.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumber_Of_Days.Location = New System.Drawing.Point(16, 219)
        Me.LblNumber_Of_Days.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblNumber_Of_Days.Name = "LblNumber_Of_Days"
        Me.LblNumber_Of_Days.Size = New System.Drawing.Size(82, 16)
        Me.LblNumber_Of_Days.TabIndex = 12
        Me.LblNumber_Of_Days.Text = "Cant de días"
        Me.LblNumber_Of_Days.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblPrice_Day
        '
        Me.LblPrice_Day.AutoSize = True
        Me.LblPrice_Day.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPrice_Day.Location = New System.Drawing.Point(136, 153)
        Me.LblPrice_Day.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblPrice_Day.Name = "LblPrice_Day"
        Me.LblPrice_Day.Size = New System.Drawing.Size(91, 16)
        Me.LblPrice_Day.TabIndex = 11
        Me.LblPrice_Day.Text = "Precio por día"
        Me.LblPrice_Day.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DtpFdiPgs
        '
        Me.DtpFdiPgs.CustomFormat = "dd 'de' MMMM 'de' yyyy"
        Me.DtpFdiPgs.Enabled = False
        Me.DtpFdiPgs.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!)
        Me.DtpFdiPgs.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpFdiPgs.Location = New System.Drawing.Point(19, 45)
        Me.DtpFdiPgs.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.DtpFdiPgs.Name = "DtpFdiPgs"
        Me.DtpFdiPgs.Size = New System.Drawing.Size(224, 26)
        Me.DtpFdiPgs.TabIndex = 0
        '
        'LblTotal_
        '
        Me.LblTotal_.AutoSize = True
        Me.LblTotal_.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal_.Location = New System.Drawing.Point(16, 153)
        Me.LblTotal_.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblTotal_.Name = "LblTotal_"
        Me.LblTotal_.Size = New System.Drawing.Size(38, 16)
        Me.LblTotal_.TabIndex = 10
        Me.LblTotal_.Text = "Total"
        Me.LblTotal_.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotal
        '
        Me.LblTotal.BackColor = System.Drawing.SystemColors.Window
        Me.LblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTotal.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.Location = New System.Drawing.Point(19, 177)
        Me.LblTotal.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.Size = New System.Drawing.Size(104, 26)
        Me.LblTotal.TabIndex = 3
        Me.LblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblPrcPgs
        '
        Me.LblPrcPgs.AutoSize = True
        Me.LblPrcPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPrcPgs.Location = New System.Drawing.Point(16, 87)
        Me.LblPrcPgs.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblPrcPgs.Name = "LblPrcPgs"
        Me.LblPrcPgs.Size = New System.Drawing.Size(46, 16)
        Me.LblPrcPgs.TabIndex = 8
        Me.LblPrcPgs.Text = "Precio"
        Me.LblPrcPgs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotal_To_Pay
        '
        Me.LblTotal_To_Pay.AutoSize = True
        Me.LblTotal_To_Pay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal_To_Pay.Location = New System.Drawing.Point(136, 219)
        Me.LblTotal_To_Pay.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblTotal_To_Pay.Name = "LblTotal_To_Pay"
        Me.LblTotal_To_Pay.Size = New System.Drawing.Size(88, 16)
        Me.LblTotal_To_Pay.TabIndex = 13
        Me.LblTotal_To_Pay.Text = "Total a pagar"
        Me.LblTotal_To_Pay.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblDscPgs
        '
        Me.LblDscPgs.AutoSize = True
        Me.LblDscPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDscPgs.Location = New System.Drawing.Point(136, 87)
        Me.LblDscPgs.Margin = New System.Windows.Forms.Padding(0, 16, 0, 0)
        Me.LblDscPgs.Name = "LblDscPgs"
        Me.LblDscPgs.Size = New System.Drawing.Size(72, 16)
        Me.LblDscPgs.TabIndex = 9
        Me.LblDscPgs.Text = "Descuento"
        Me.LblDscPgs.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel
        '
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel.Controls.Add(Me.GroupBox1)
        Me.Panel.Controls.Add(Me.GroupBox)
        Me.Panel.Location = New System.Drawing.Point(33, 59)
        Me.Panel.Margin = New System.Windows.Forms.Padding(24, 20, 16, 32)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(648, 325)
        Me.Panel.TabIndex = 0
        '
        'GroupBox
        '
        Me.GroupBox.Controls.Add(Me.ChkFdiPgs)
        Me.GroupBox.Controls.Add(Me.LblFdiPgs)
        Me.GroupBox.Controls.Add(Me.LblNumber_Of_Days)
        Me.GroupBox.Controls.Add(Me.LblPrice_Day)
        Me.GroupBox.Controls.Add(Me.DtpFdiPgs)
        Me.GroupBox.Controls.Add(Me.LblTotal_)
        Me.GroupBox.Controls.Add(Me.LblTotal)
        Me.GroupBox.Controls.Add(Me.LblPrcPgs)
        Me.GroupBox.Controls.Add(Me.LblTotal_To_Pay)
        Me.GroupBox.Controls.Add(Me.LblDscPgs)
        Me.GroupBox.Controls.Add(Me.LblPriceDay)
        Me.GroupBox.Controls.Add(Me.TxtDscPgs)
        Me.GroupBox.Controls.Add(Me.LblNumberOfDays)
        Me.GroupBox.Controls.Add(Me.LblTotalToPay)
        Me.GroupBox.Controls.Add(Me.TxtPrcPgs)
        Me.GroupBox.Location = New System.Drawing.Point(24, 16)
        Me.GroupBox.Margin = New System.Windows.Forms.Padding(24, 16, 0, 20)
        Me.GroupBox.Name = "GroupBox"
        Me.GroupBox.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox.Size = New System.Drawing.Size(285, 285)
        Me.GroupBox.TabIndex = 0
        Me.GroupBox.TabStop = False
        '
        'ChkFdiPgs
        '
        Me.ChkFdiPgs.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkFdiPgs.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkFdiPgs.Location = New System.Drawing.Point(247, 45)
        Me.ChkFdiPgs.Margin = New System.Windows.Forms.Padding(4, 0, 12, 0)
        Me.ChkFdiPgs.Name = "ChkFdiPgs"
        Me.ChkFdiPgs.Size = New System.Drawing.Size(26, 26)
        Me.ChkFdiPgs.TabIndex = 3
        Me.ChkFdiPgs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ChkFdiPgs.UseVisualStyleBackColor = True
        '
        'LblPriceDay
        '
        Me.LblPriceDay.BackColor = System.Drawing.SystemColors.Window
        Me.LblPriceDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblPriceDay.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPriceDay.Location = New System.Drawing.Point(139, 177)
        Me.LblPriceDay.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.LblPriceDay.Name = "LblPriceDay"
        Me.LblPriceDay.Size = New System.Drawing.Size(104, 26)
        Me.LblPriceDay.TabIndex = 4
        Me.LblPriceDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtDscPgs
        '
        Me.TxtDscPgs.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDscPgs.Location = New System.Drawing.Point(139, 111)
        Me.TxtDscPgs.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtDscPgs.MaxLength = 10
        Me.TxtDscPgs.Name = "TxtDscPgs"
        Me.TxtDscPgs.Size = New System.Drawing.Size(104, 26)
        Me.TxtDscPgs.TabIndex = 2
        Me.TxtDscPgs.Text = "0"
        Me.TxtDscPgs.WordWrap = False
        '
        'LblNumberOfDays
        '
        Me.LblNumberOfDays.BackColor = System.Drawing.SystemColors.Window
        Me.LblNumberOfDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblNumberOfDays.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumberOfDays.ForeColor = System.Drawing.Color.Green
        Me.LblNumberOfDays.Location = New System.Drawing.Point(19, 243)
        Me.LblNumberOfDays.Margin = New System.Windows.Forms.Padding(0, 8, 0, 16)
        Me.LblNumberOfDays.Name = "LblNumberOfDays"
        Me.LblNumberOfDays.Size = New System.Drawing.Size(104, 26)
        Me.LblNumberOfDays.TabIndex = 5
        Me.LblNumberOfDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalToPay
        '
        Me.LblTotalToPay.BackColor = System.Drawing.SystemColors.Window
        Me.LblTotalToPay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblTotalToPay.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalToPay.Location = New System.Drawing.Point(139, 243)
        Me.LblTotalToPay.Margin = New System.Windows.Forms.Padding(0, 0, 0, 16)
        Me.LblTotalToPay.Name = "LblTotalToPay"
        Me.LblTotalToPay.Size = New System.Drawing.Size(104, 26)
        Me.LblTotalToPay.TabIndex = 6
        Me.LblTotalToPay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtPrcPgs
        '
        Me.TxtPrcPgs.Font = New System.Drawing.Font("Linux Libertine Display G", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrcPgs.Location = New System.Drawing.Point(19, 111)
        Me.TxtPrcPgs.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.TxtPrcPgs.MaxLength = 10
        Me.TxtPrcPgs.Name = "TxtPrcPgs"
        Me.TxtPrcPgs.Size = New System.Drawing.Size(104, 26)
        Me.TxtPrcPgs.TabIndex = 1
        Me.TxtPrcPgs.Text = "0"
        Me.TxtPrcPgs.WordWrap = False
        '
        'LblDisplayName
        '
        Me.LblDisplayName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDisplayName.Location = New System.Drawing.Point(33, 21)
        Me.LblDisplayName.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.LblDisplayName.Name = "LblDisplayName"
        Me.LblDisplayName.Size = New System.Drawing.Size(648, 18)
        Me.LblDisplayName.TabIndex = 0
        Me.LblDisplayName.Text = "NOMBRE DEL GRUPO E INTEGRANTES O DEL CLIENTE Y SU EDAD"
        Me.LblDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCancelPayment
        '
        Me.BtnCancelPayment.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCancelPayment.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancelPayment.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnCancelPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnCancelPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnCancelPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancelPayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelPayment.ForeColor = System.Drawing.Color.Brown
        Me.BtnCancelPayment.Image = Global.GymPaymentControl.My.Resources.Resources.ic_cancel_pay_34x28
        Me.BtnCancelPayment.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCancelPayment.Location = New System.Drawing.Point(705, 283)
        Me.BtnCancelPayment.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCancelPayment.Name = "BtnCancelPayment"
        Me.BtnCancelPayment.Padding = New System.Windows.Forms.Padding(0, 4, 0, 1)
        Me.BtnCancelPayment.Size = New System.Drawing.Size(136, 64)
        Me.BtnCancelPayment.TabIndex = 3
        Me.BtnCancelPayment.Text = "C&ancelar pago"
        Me.BtnCancelPayment.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnCancelPayment.UseVisualStyleBackColor = False
        '
        'BtnPayMonth
        '
        Me.BtnPayMonth.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnPayMonth.FlatAppearance.MouseDownBackColor = System.Drawing.Color.AliceBlue
        Me.BtnPayMonth.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue
        Me.BtnPayMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPayMonth.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPayMonth.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnPayMonth.Image = Global.GymPaymentControl.My.Resources.Resources.ic_pay_month_28x32
        Me.BtnPayMonth.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPayMonth.Location = New System.Drawing.Point(705, 99)
        Me.BtnPayMonth.Margin = New System.Windows.Forms.Padding(8, 0, 20, 0)
        Me.BtnPayMonth.Name = "BtnPayMonth"
        Me.BtnPayMonth.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Me.BtnPayMonth.Size = New System.Drawing.Size(136, 64)
        Me.BtnPayMonth.TabIndex = 2
        Me.BtnPayMonth.Text = "&Pagar mes"
        Me.BtnPayMonth.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPayMonth.UseVisualStyleBackColor = True
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 5000
        Me.ToolTip.BackColor = System.Drawing.Color.PaleTurquoise
        Me.ToolTip.InitialDelay = 300
        Me.ToolTip.ReshowDelay = 100
        '
        'FrmCollectMembership
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 425)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.LblDisplayName)
        Me.Controls.Add(Me.BtnCancelPayment)
        Me.Controls.Add(Me.BtnPayMonth)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmCollectMembership"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PAGO - CUOTA MENSUAL"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel.ResumeLayout(False)
        Me.GroupBox.ResumeLayout(False)
        Me.GroupBox.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TxtDetailMethod As TextBox
    Friend WithEvents LblFdpPgs As Label
    Friend WithEvents LblFrmPgs As Label
    Friend WithEvents LblMtdPgs As Label
    Friend WithEvents DtpFdpPgs As DateTimePicker
    Friend WithEvents CmbFrmPgs As ComboBox
    Friend WithEvents CmbMtdPgs As ComboBox
    Friend WithEvents LblFdiPgs As Label
    Friend WithEvents LblNumber_Of_Days As Label
    Friend WithEvents LblPrice_Day As Label
    Friend WithEvents DtpFdiPgs As DateTimePicker
    Friend WithEvents LblTotal_ As Label
    Friend WithEvents LblTotal As Label
    Friend WithEvents LblPrcPgs As Label
    Friend WithEvents LblTotal_To_Pay As Label
    Friend WithEvents LblDscPgs As Label
    Friend WithEvents Panel As Panel
    Friend WithEvents GroupBox As GroupBox
    Friend WithEvents LblPriceDay As Label
    Friend WithEvents TxtDscPgs As TextBox
    Friend WithEvents LblNumberOfDays As Label
    Friend WithEvents LblTotalToPay As Label
    Friend WithEvents TxtPrcPgs As TextBox
    Friend WithEvents BtnCancelPayment As Button
    Friend WithEvents BtnPayMonth As Button
    Friend WithEvents LblDisplayName As Label
    Friend WithEvents ChkFdiPgs As CheckBox
    Friend WithEvents ChkFdpPgs As CheckBox
    Friend WithEvents ChkMtdPgs As CheckBox
    Friend WithEvents ToolTip As ToolTip
End Class
