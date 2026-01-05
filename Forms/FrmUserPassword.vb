Imports GymPaymentControl.Models
Imports GymPaymentControl.Services

Public Class FrmUserPassword

    ' Esta es la variable que "recordará" el ID mientras el usuario crea su clave
    Private _idUserTemp As Integer = 0

    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click

        Dim auth As New AuthManager()
        ' 1. Declaramos la variable una sola vez al inicio del método
        Dim user As UserDTO = Nothing

        If TxtPassword.Text = "" Then
            ' Flujo: Intento de primer ingreso
            ' 2. AQUÍ YA NO USAS "Dim", solo asignas el valor
            user = auth.GetUser(TxtUser.Text.Trim())

            If user IsNot Nothing Then
                ' 1. Guardamos el ID en la variable privada del formulario
                _idUserTemp = user.Id
                'UserSession.IdUser = user.Id
                LblUser2.Text = "Ingresa una contraseña para: " & user.Username
                GbUserPassword.Visible = False
                GbSavePassword.Visible = True
            Else
                MsgBox("El usuario no está registrado.", vbCritical, "Control de acceso")
            End If
        Else
            ' Flujo: Login Normal
            ' 3. AQUÍ TAMBIÉN QUITAS EL "Dim"
            user = auth.ValidateLogin(TxtUser.Text, TxtPassword.Text)

            If user IsNot Nothing Then
                UserSession.IdUser = user.Id
                UserSession.UserName = user.Username
                UserSession.Role = user.Role

                ' 2. Registramos la entrada en la base de datos
                auth.RegisterSession()
                _idUserTemp = 0 ' Limpiamos el ID por seguridad

                FrmMain.Show()
                Me.Close()

            Else
                MsgBox("El usuario o la contraseña son incorreco.", vbCritical, "Control de acceso")
            End If
        End If

    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        End
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        ' 1. Validaciones básicas de interfaz
        If String.IsNullOrWhiteSpace(TxtPassword1.Text) Then
            MessageBox.Show("La contraseña no puede estar vacía.")
            Return
        End If

        If TxtPassword1.Text <> TxtPassword2.Text Then
            MessageBox.Show("Las contraseñas no coinciden.", "Guardar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            ' 2. Instanciamos el servicio
            Dim auth As New AuthManager()

            ' 3. Llamamos a la lógica enviando el ID temporal que capturamos en el inicio
            ' _idUserTemp es la variable que definimos antes a nivel de formulario
            auth.UpdatePassword(_idUserTemp, TxtPassword1.Text.Trim())

            MessageBox.Show("Contraseña guardada correctamente. Puedes iniciar sesión.", "Enhorabuena", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' 4. Limpiamos la interfaz para que haga el login normal
            GbUserPassword.Visible = True
            GbSavePassword.Visible = False
            TxtPassword1.Clear()
            TxtPassword2.Clear()
            TxtPassword.Focus()
            _idUserTemp = 0 ' Limpiamos el ID por seguridad

        Catch ex As Exception
            MessageBox.Show("Error al guardar: " & ex.Message)
        End Try
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        GbUserPassword.Visible = True
        GbSavePassword.Visible = False
        TxtPassword1.Clear()
        TxtPassword2.Clear()
        TxtPassword.Focus()

    End Sub
End Class