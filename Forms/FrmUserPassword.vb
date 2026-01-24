Imports GymPaymentControl.Models
Imports GymPaymentControl.Services

Public Class FrmUserPassword

    '|-------------------------------------------------------------------------------------
    '| DECLARAR VARIABLES GLOBALES
    '|----------------------------
    '|
    '| * Variable temporal que almacena el ID del usuario mientras crea su clave de acceso.
    Private idUserTemp As Integer = 0
    ''
    ''
    ''
    Private Sub FrmUserPassword_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub
    ''
    ''
    ''
    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click

        '|-----------------------------------------------------------------------------
        '| VALIDAR USUARIO Y CONTRASEÑA DEL USUARIO PARA ACCEDER AL SISTEMA
        '|-----------------------------------------------------------------
        '|
        '| * Creamos una instancia del administrador de autenticación 'AuthManager'.
        '| * Variable 'userDto' para almacenar la información del usuario.
        '|
        '| IF : Comprobamos si 'TxtPassword' está vacio, se asume que el usuario va a
        '|      crear su contraseña.
        '|      * Almacenamos el nombre del usuario en 'userDto'
        '|
        '|      IF : Verificamos si el usuario NO está registrado en la BBDD.
        '|          * Mostramos un mensaje de error y salimos del procedimiento.
        '|
        '|      * Preparar la ventana para crear la contraseña, almacenamos el id del
        '|        usuario en ‘idUserTemp’, mostramos el nombre del usuario en ‘LblUser2’,
        '|        ocultamos y mostramos los groupbox ‘GbUserPassword’ y ‘GbSavePassword’.
        '|
        '| * Validamos el inicio de sesión llamando a la función 'ValidateLogin' del
        '|   'AuthManager'le pasamos como parametros el usuario 'TxtUser' y la contraseña
        '|   'TxtPassword'.
        '|
        '| IF : Verificamos si las credenciales son incorrectas.
        '|      * Mostramos un mensaje de error y salimos del procedimiento.
        '|
        '| * Se cargan los datos del usuario (Id, Username y Role) en la sesión 'UserSession'.
        '| * Registramos la sesión del usuario llamando a la función 'RegisterSession'.
        '| * Limpiamos la variable temporal 'idUserTemp'.
        '| * Abrimos el formulario principal 'FrmMdiMain'.
        '| * Cerramos el formulario login 'FrmUserPassword'.

        Dim authManager As New AuthManager()
        Dim userDto As UserDTO

        If String.IsNullOrWhiteSpace(TxtPassword.Text) Then

            userDto = authManager.GetUser(TxtUser.Text.Trim())

            If userDto Is Nothing Then
                MsgBox("El usuario no está registrado.", vbCritical, "Control de acceso")
                Exit Sub
            End If

            idUserTemp = userDto.Id
            LblUser2.Text = "Ingresa una contraseña para: " & userDto.Username
            GbUserPassword.Visible = False
            GbSavePassword.Visible = True
            Exit Sub

        End If

        userDto = authManager.ValidateLogin(TxtUser.Text.Trim(), TxtPassword.Text)

        If userDto Is Nothing Then
            MsgBox("El usuario o la contraseña son incorrectos.", vbCritical, "Control de acceso")
            Exit Sub
        End If

        UserSession.IdUser = userDto.Id
        UserSession.UserName = userDto.Username
        UserSession.Role = userDto.Role

        authManager.RegisterSession()

        idUserTemp = 0

        FrmMdiMain.Show()
        Me.Close()

    End Sub
    ''
    ''
    ''
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click

        '|---------------------------------------------------------------------------
        '| VALIDAR Y GUARDAR CONTRASEÑA DEL USUARIO
        '|-----------------------------------------
        '|
        '| IF : Comprobamos si los cuadros de texto 'TxtPassword1' y 'TxtPassword2'
        '|      están vacíos. 
        '|      * Mostramos al usuario un mensaje de error.
        '|      * Return = Indica que se está saliendo del procedimiento.
        '|
        '| IF : Comprobamos si las contraseñas no son iguales.
        '|      * Mostramos al usuario un mensaje de advertencia.
        '|      * Return = Indica que se está saliendo del procedimiento.
        '|
        '| TRY : Para controlar posibles errores.
        '|      * Instanciamos el servicio AuthManager.
        '|      * Llamamos a la función publica ‘UpdatePassword’ y le pasamos como
        '|        parametro el id ‘idUserTemp’ y la contraseña ‘TxtPassword1.Text’
        '|        del usuario.
        '|      * Mostramos al usuario un mensaje de información exitosa.
        '|      * Llamamos a la subrutina que se encarga del comportamiento de los
        '|        controles para hacer el login y acceder al sistema de control.
        '|
        '| CATCH : Mostrar un mensaje si ha ocurrido un error al momento de guardar
        '|         la contraseña

        If String.IsNullOrWhiteSpace(TxtPassword1.Text) Or String.IsNullOrWhiteSpace(TxtPassword2.Text) Then
            MessageBox.Show("La contraseña no puede estar vacía.", "Error campos vacios", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If TxtPassword1.Text <> TxtPassword2.Text Then
            MessageBox.Show("Las contraseñas no coinciden.", "Verificar contraseña", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            Dim authManager As New AuthManager()

            authManager.UpdatePassword(idUserTemp, TxtPassword1.Text.Trim())

            MessageBox.Show("Contraseña guardada correctamente. Puedes iniciar sesión.", "Enhorabuena", MessageBoxButtons.OK, MessageBoxIcon.Information)

            SaveOrCancel()

        Catch ex As Exception
            MessageBox.Show("Error : " & ex.Message)
        End Try

    End Sub
    ''
    ''
    ''
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

        '|-------------------------------------------------------------------------------
        '| CANCELAR ANTES DE CREAR UNA CONTRASEÑA PARA EL USUARIO
        '|-------------------------------------------------------
        '|
        '| * Llamamos a la subrutina que se encarga del comportamiento de los controles
        '|   para hacer el login y acceder al sistema de control.

        SaveOrCancel()

    End Sub
    ''
    ''
    ''
    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click

        '|-----------------------------------------------------------------------------
        '| CERRAR Y TERMINAR EL PROGRAMA
        '|------------------------------
        '|
        '| * Usamos ' Me.Close' para cerrar el formulario actual.
        '| * Para cerrar toda la aplicación de forma ordenada y cerrar todos los hilos
        '|   de forma segura usamos Application.Exit.

        Me.Close()
        Application.Exit()

    End Sub
    ''
    ''
    ''
    Private Sub SaveOrCancel()

        '|-------------------------------------------------------------------------------
        '| MOSTRAR, OCULTAR, LIMPIAR Y ENVIAR ENFOQUE
        '|-------------------------------------------
        '|
        '| * Visible = Muestra y oculta los groupbox 'GbUserPassword' y 'GbSavePassword'.
        '| * Clear = Limpia los cuadros de texto 'TxtPassword1' y 'TxtPassword2'.
        '| * Focus = Envia el enfoque al cuadro de texto 'TxtPassword'.
        '| * Limpiamos la variable 'idUserTemp' por seguridad.

        GbUserPassword.Visible = True
        GbSavePassword.Visible = False
        TxtPassword1.Clear()
        TxtPassword2.Clear()
        TxtPassword.Focus()
        idUserTemp = 0

    End Sub

End Class