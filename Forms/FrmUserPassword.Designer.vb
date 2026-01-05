<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserPassword
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
        Me.TxtPassword2 = New System.Windows.Forms.TextBox()
        Me.LblRepite = New System.Windows.Forms.Label()
        Me.TxtPassword1 = New System.Windows.Forms.TextBox()
        Me.LblUser2 = New System.Windows.Forms.Label()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnStart = New System.Windows.Forms.Button()
        Me.GbSavePassword = New System.Windows.Forms.GroupBox()
        Me.LblIngresa = New System.Windows.Forms.Label()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.LblUsuario = New System.Windows.Forms.Label()
        Me.TxtUser = New System.Windows.Forms.TextBox()
        Me.LblContrasena = New System.Windows.Forms.Label()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GbUserPassword = New System.Windows.Forms.GroupBox()
        Me.PictureBox = New System.Windows.Forms.PictureBox()
        Me.Panel2.SuspendLayout()
        Me.GbSavePassword.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GbUserPassword.SuspendLayout()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtPassword2
        '
        Me.TxtPassword2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword2.Location = New System.Drawing.Point(23, 149)
        Me.TxtPassword2.Margin = New System.Windows.Forms.Padding(30)
        Me.TxtPassword2.Name = "TxtPassword2"
        Me.TxtPassword2.Size = New System.Drawing.Size(304, 26)
        Me.TxtPassword2.TabIndex = 1
        Me.TxtPassword2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblRepite
        '
        Me.LblRepite.AutoSize = True
        Me.LblRepite.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRepite.Location = New System.Drawing.Point(20, 123)
        Me.LblRepite.Name = "LblRepite"
        Me.LblRepite.Size = New System.Drawing.Size(148, 18)
        Me.LblRepite.TabIndex = 3
        Me.LblRepite.Text = "&Repite Contraseña"
        '
        'TxtPassword1
        '
        Me.TxtPassword1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword1.Location = New System.Drawing.Point(23, 86)
        Me.TxtPassword1.Margin = New System.Windows.Forms.Padding(30)
        Me.TxtPassword1.Name = "TxtPassword1"
        Me.TxtPassword1.Size = New System.Drawing.Size(304, 26)
        Me.TxtPassword1.TabIndex = 0
        Me.TxtPassword1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LblUser2
        '
        Me.LblUser2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUser2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblUser2.Location = New System.Drawing.Point(3, 25)
        Me.LblUser2.Name = "LblUser2"
        Me.LblUser2.Size = New System.Drawing.Size(344, 18)
        Me.LblUser2.TabIndex = 0
        Me.LblUser2.Text = "LblUsuario2"
        Me.LblUser2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.ForeColor = System.Drawing.Color.Brown
        Me.BtnCancel.Location = New System.Drawing.Point(207, 23)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(120, 30)
        Me.BtnCancel.TabIndex = 3
        Me.BtnCancel.Text = "C&ancelar"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.Green
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnSave.Location = New System.Drawing.Point(23, 23)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(170, 30)
        Me.BtnSave.TabIndex = 2
        Me.BtnSave.Text = "&Guardar contraseña"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel2.Controls.Add(Me.BtnCancel)
        Me.Panel2.Controls.Add(Me.BtnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 201)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel2.Size = New System.Drawing.Size(350, 76)
        Me.Panel2.TabIndex = 7
        '
        'BtnStart
        '
        Me.BtnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.BtnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.BtnStart.Location = New System.Drawing.Point(23, 23)
        Me.BtnStart.Name = "BtnStart"
        Me.BtnStart.Size = New System.Drawing.Size(130, 30)
        Me.BtnStart.TabIndex = 2
        Me.BtnStart.Text = "&Iniciar sesión"
        Me.BtnStart.UseVisualStyleBackColor = True
        '
        'GbSavePassword
        '
        Me.GbSavePassword.Controls.Add(Me.Panel2)
        Me.GbSavePassword.Controls.Add(Me.TxtPassword2)
        Me.GbSavePassword.Controls.Add(Me.LblRepite)
        Me.GbSavePassword.Controls.Add(Me.TxtPassword1)
        Me.GbSavePassword.Controls.Add(Me.LblIngresa)
        Me.GbSavePassword.Controls.Add(Me.LblUser2)
        Me.GbSavePassword.Location = New System.Drawing.Point(272, 34)
        Me.GbSavePassword.Margin = New System.Windows.Forms.Padding(16, 32, 32, 32)
        Me.GbSavePassword.Name = "GbSavePassword"
        Me.GbSavePassword.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.GbSavePassword.Size = New System.Drawing.Size(350, 277)
        Me.GbSavePassword.TabIndex = 1
        Me.GbSavePassword.TabStop = False
        Me.GbSavePassword.Visible = False
        '
        'LblIngresa
        '
        Me.LblIngresa.AutoSize = True
        Me.LblIngresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIngresa.Location = New System.Drawing.Point(20, 60)
        Me.LblIngresa.Name = "LblIngresa"
        Me.LblIngresa.Size = New System.Drawing.Size(152, 18)
        Me.LblIngresa.TabIndex = 1
        Me.LblIngresa.Text = "&Ingresa contraseña"
        '
        'BtnClose
        '
        Me.BtnClose.FlatAppearance.BorderColor = System.Drawing.Color.Red
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.ForeColor = System.Drawing.Color.Brown
        Me.BtnClose.Location = New System.Drawing.Point(197, 23)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(130, 30)
        Me.BtnClose.TabIndex = 3
        Me.BtnClose.Text = "Cerrar &App"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'LblUsuario
        '
        Me.LblUsuario.AutoSize = True
        Me.LblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUsuario.Location = New System.Drawing.Point(19, 33)
        Me.LblUsuario.Name = "LblUsuario"
        Me.LblUsuario.Size = New System.Drawing.Size(152, 18)
        Me.LblUsuario.TabIndex = 0
        Me.LblUsuario.Text = "&Nombre de usuario"
        '
        'TxtUser
        '
        Me.TxtUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtUser.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUser.Location = New System.Drawing.Point(23, 65)
        Me.TxtUser.MaxLength = 50
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.Size = New System.Drawing.Size(304, 26)
        Me.TxtUser.TabIndex = 0
        Me.TxtUser.Text = "ADMIN"
        '
        'LblContrasena
        '
        Me.LblContrasena.AutoSize = True
        Me.LblContrasena.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblContrasena.Location = New System.Drawing.Point(19, 117)
        Me.LblContrasena.Name = "LblContrasena"
        Me.LblContrasena.Size = New System.Drawing.Size(95, 18)
        Me.LblContrasena.TabIndex = 2
        Me.LblContrasena.Text = "&Contraseña"
        '
        'TxtPassword
        '
        Me.TxtPassword.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword.Location = New System.Drawing.Point(23, 149)
        Me.TxtPassword.MaxLength = 50
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(172)
        Me.TxtPassword.Size = New System.Drawing.Size(304, 26)
        Me.TxtPassword.TabIndex = 1
        Me.TxtPassword.Text = "Admin"
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Controls.Add(Me.BtnStart)
        Me.Panel1.Controls.Add(Me.BtnClose)
        Me.Panel1.Location = New System.Drawing.Point(0, 201)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(20)
        Me.Panel1.Size = New System.Drawing.Size(350, 76)
        Me.Panel1.TabIndex = 6
        '
        'GbUserPassword
        '
        Me.GbUserPassword.Controls.Add(Me.LblUsuario)
        Me.GbUserPassword.Controls.Add(Me.TxtUser)
        Me.GbUserPassword.Controls.Add(Me.LblContrasena)
        Me.GbUserPassword.Controls.Add(Me.TxtPassword)
        Me.GbUserPassword.Controls.Add(Me.Panel1)
        Me.GbUserPassword.Location = New System.Drawing.Point(272, 34)
        Me.GbUserPassword.Margin = New System.Windows.Forms.Padding(16, 32, 32, 32)
        Me.GbUserPassword.Name = "GbUserPassword"
        Me.GbUserPassword.Padding = New System.Windows.Forms.Padding(0, 20, 0, 0)
        Me.GbUserPassword.Size = New System.Drawing.Size(350, 277)
        Me.GbUserPassword.TabIndex = 0
        Me.GbUserPassword.TabStop = False
        '
        'PictureBox
        '
        Me.PictureBox.Image = Global.GymPaymentControl.My.Resources.Resources.img_user_password
        Me.PictureBox.Location = New System.Drawing.Point(41, 41)
        Me.PictureBox.Margin = New System.Windows.Forms.Padding(32, 32, 0, 32)
        Me.PictureBox.Name = "PictureBox"
        Me.PictureBox.Size = New System.Drawing.Size(215, 270)
        Me.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox.TabIndex = 10
        Me.PictureBox.TabStop = False
        '
        'FrmUserPassword
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 352)
        Me.Controls.Add(Me.PictureBox)
        Me.Controls.Add(Me.GbUserPassword)
        Me.Controls.Add(Me.GbSavePassword)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmUserPassword"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CONTROL DE ACCESO"
        Me.Panel2.ResumeLayout(False)
        Me.GbSavePassword.ResumeLayout(False)
        Me.GbSavePassword.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.GbUserPassword.ResumeLayout(False)
        Me.GbUserPassword.PerformLayout()
        CType(Me.PictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TxtPassword2 As TextBox
    Friend WithEvents LblRepite As Label
    Friend WithEvents TxtPassword1 As TextBox
    Friend WithEvents LblUser2 As Label
    Friend WithEvents BtnCancel As Button
    Friend WithEvents BtnSave As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents BtnStart As Button
    Friend WithEvents GbSavePassword As GroupBox
    Friend WithEvents LblIngresa As Label
    Friend WithEvents BtnClose As Button
    Friend WithEvents LblUsuario As Label
    Friend WithEvents TxtUser As TextBox
    Friend WithEvents LblContrasena As Label
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GbUserPassword As GroupBox
    Friend WithEvents PictureBox As PictureBox
End Class
