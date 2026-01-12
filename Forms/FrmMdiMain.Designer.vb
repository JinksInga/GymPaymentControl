<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMdiMain
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
        Me.PnlBotonera = New System.Windows.Forms.Panel()
        Me.BtnFamilyGroup = New System.Windows.Forms.Button()
        Me.BtnClientPayments = New System.Windows.Forms.Button()
        Me.BtnGoOut = New System.Windows.Forms.Button()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.BtnPriceAndDiscounts = New System.Windows.Forms.Button()
        Me.BtnOutstandingPayments = New System.Windows.Forms.Button()
        Me.PnlBotonera.SuspendLayout()
        Me.SuspendLayout()
        '
        'PnlBotonera
        '
        Me.PnlBotonera.AutoSize = True
        Me.PnlBotonera.Controls.Add(Me.BtnFamilyGroup)
        Me.PnlBotonera.Controls.Add(Me.BtnClientPayments)
        Me.PnlBotonera.Controls.Add(Me.BtnGoOut)
        Me.PnlBotonera.Controls.Add(Me.Panel)
        Me.PnlBotonera.Controls.Add(Me.BtnPriceAndDiscounts)
        Me.PnlBotonera.Controls.Add(Me.BtnOutstandingPayments)
        Me.PnlBotonera.Dock = System.Windows.Forms.DockStyle.Left
        Me.PnlBotonera.Location = New System.Drawing.Point(0, 0)
        Me.PnlBotonera.Name = "PnlBotonera"
        Me.PnlBotonera.Size = New System.Drawing.Size(188, 561)
        Me.PnlBotonera.TabIndex = 2
        '
        'BtnFamilyGroup
        '
        Me.BtnFamilyGroup.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFamilyGroup.Image = Global.GymPaymentControl.My.Resources.Resources.ic_family_group_53x30
        Me.BtnFamilyGroup.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFamilyGroup.Location = New System.Drawing.Point(4, 103)
        Me.BtnFamilyGroup.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnFamilyGroup.Name = "BtnFamilyGroup"
        Me.BtnFamilyGroup.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.BtnFamilyGroup.Size = New System.Drawing.Size(180, 74)
        Me.BtnFamilyGroup.TabIndex = 5
        Me.BtnFamilyGroup.Text = "Grupo Familiar"
        Me.BtnFamilyGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnFamilyGroup.UseVisualStyleBackColor = True
        '
        'BtnClientPayments
        '
        Me.BtnClientPayments.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClientPayments.Image = Global.GymPaymentControl.My.Resources.Resources.ic_client_37x35
        Me.BtnClientPayments.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnClientPayments.Location = New System.Drawing.Point(4, 17)
        Me.BtnClientPayments.Margin = New System.Windows.Forms.Padding(4, 8, 4, 0)
        Me.BtnClientPayments.Name = "BtnClientPayments"
        Me.BtnClientPayments.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
        Me.BtnClientPayments.Size = New System.Drawing.Size(180, 74)
        Me.BtnClientPayments.TabIndex = 4
        Me.BtnClientPayments.Text = "Clientes"
        Me.BtnClientPayments.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnClientPayments.UseVisualStyleBackColor = True
        '
        'BtnGoOut
        '
        Me.BtnGoOut.BackColor = System.Drawing.SystemColors.Control
        Me.BtnGoOut.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.BtnGoOut.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnGoOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.BtnGoOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnGoOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGoOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGoOut.Image = Global.GymPaymentControl.My.Resources.Resources.ic_go_out_34x31
        Me.BtnGoOut.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnGoOut.Location = New System.Drawing.Point(0, 463)
        Me.BtnGoOut.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnGoOut.Name = "BtnGoOut"
        Me.BtnGoOut.Padding = New System.Windows.Forms.Padding(0, 6, 0, 2)
        Me.BtnGoOut.Size = New System.Drawing.Size(188, 74)
        Me.BtnGoOut.TabIndex = 3
        Me.BtnGoOut.Text = "CERRAR &APP"
        Me.BtnGoOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnGoOut.UseVisualStyleBackColor = False
        '
        'Panel
        '
        Me.Panel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel.Location = New System.Drawing.Point(0, 537)
        Me.Panel.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(188, 24)
        Me.Panel.TabIndex = 1
        '
        'BtnPriceAndDiscounts
        '
        Me.BtnPriceAndDiscounts.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPriceAndDiscounts.Image = Global.GymPaymentControl.My.Resources.Resources.ic_rate_discount_45x33
        Me.BtnPriceAndDiscounts.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPriceAndDiscounts.Location = New System.Drawing.Point(4, 275)
        Me.BtnPriceAndDiscounts.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnPriceAndDiscounts.Name = "BtnPriceAndDiscounts"
        Me.BtnPriceAndDiscounts.Padding = New System.Windows.Forms.Padding(0, 6, 0, 5)
        Me.BtnPriceAndDiscounts.Size = New System.Drawing.Size(180, 74)
        Me.BtnPriceAndDiscounts.TabIndex = 2
        Me.BtnPriceAndDiscounts.Text = "&Precio y Descuentos"
        Me.BtnPriceAndDiscounts.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPriceAndDiscounts.UseVisualStyleBackColor = True
        '
        'BtnOutstandingPayments
        '
        Me.BtnOutstandingPayments.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOutstandingPayments.Image = Global.GymPaymentControl.My.Resources.Resources.ic_defaulters_35x35
        Me.BtnOutstandingPayments.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnOutstandingPayments.Location = New System.Drawing.Point(4, 189)
        Me.BtnOutstandingPayments.Margin = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.BtnOutstandingPayments.Name = "BtnOutstandingPayments"
        Me.BtnOutstandingPayments.Padding = New System.Windows.Forms.Padding(0, 6, 0, 5)
        Me.BtnOutstandingPayments.Size = New System.Drawing.Size(180, 74)
        Me.BtnOutstandingPayments.TabIndex = 1
        Me.BtnOutstandingPayments.Text = "Pagos pendientes"
        Me.BtnOutstandingPayments.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnOutstandingPayments.UseVisualStyleBackColor = True
        '
        'FrmMdiMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 561)
        Me.Controls.Add(Me.PnlBotonera)
        Me.IsMdiContainer = True
        Me.MinimumSize = New System.Drawing.Size(1200, 600)
        Me.Name = "FrmMdiMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gimnasio Segundos Fuera   -   USUARIO  : :  "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.PnlBotonera.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PnlBotonera As Panel
    Friend WithEvents BtnFamilyGroup As Button
    Friend WithEvents BtnClientPayments As Button
    Friend WithEvents BtnGoOut As Button
    Friend WithEvents Panel As Panel
    Friend WithEvents BtnPriceAndDiscounts As Button
    Friend WithEvents BtnOutstandingPayments As Button
End Class
