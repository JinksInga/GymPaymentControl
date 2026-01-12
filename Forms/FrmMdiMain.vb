Imports System.Configuration
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Public Class FrmMdiMain
    ''
    ''
    ''
    Private Sub FrmMdiMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '|---------------------------------------------------------------------------
        '| TEXTO BARRA DE TITULO, CARGAR FORM Y VALIDAR BOTÓN
        '|---------------------------------------------------
        '|
        '| TRY : Controlamos algún error
        '|      * Agregamos el nombre del usuario y su rol en la barra de título.
        '|      * Con 'MdiParent' asignamos al formulario 'FrmMdiMain' como el contenedor.
        '|      * Usamos 'Show' para mostrar el formulario hijo 'FrmListDebtors'.
        '|      * Si el usuario no es el admin desactivamos el 'BtnPriceAndDiscounts'
        '|
        '| CATCH : 
        '|      * Mostramos un mensaje con el error capturado. 

        Try
            Me.Text = $"{Me.Text} {UserSession.UserName} - {UserSession.Role}"

            FrmListDebtors.MdiParent = Me
            FrmListDebtors.Show()

            BtnPriceAndDiscounts.Enabled = (UserSession.Role = "ADMINISTRADOR")

        Catch ex As Exception
            MessageBox.Show("Error al cargar sesión: " & ex.Message)

        End Try

    End Sub
    Private Sub FrmMdiMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        '|------------------------------------------------------------------------------------------------
        '| COMPROBAR RESPUESTA Y REGISTRO DE SALIDA EN BASE DE DATOS
        '|----------------------------------------------------------
        '|
        '| IF : Pregunta al usuario si realmente quiere salir, si la respuesta es NO entramos al bloque If.
        '|      * Cancelamos el evento de cierre 'e.Cancel = True', manteniendo la aplicación abierta.
        '|      * 'Exit Sub' Sale de la función para no ejecutar el resto del código.
        '|
        '| TRY : Controlamos posibles errores.
        '|      * Recupera la cadena de conexión desde el archivo 'App.config'.
        '|
        '|      USING : Asegura que la conexión se cierre y se libere automáticamente de la memoria.
        '|          * Abrimos la conexión  con la base de datos.
        '|          * Consultamos a la BBDD el último registro de la tabla 'sesion_user' para actualizar la
        '|            fecha y la hora de salida del usuario que inició sesión y lo almacenamos en la variable
        '|            'sqlQuery'. Usamos LIMIT 1 y DESC para asegurar que solo tocamos el último registro.
        '|
        '|              USING : Creamos una instancia 'sqlCommand' de 'MySqlCommand' dentro del bloque 'Using'
        '|                      para ejecutar la consulta SQL y liberar automática los recursos.
        '|                      * Creamos un parámetro '@fecha' para la consulta SQL.
        '|                      * Ejecutamos la consulta 'ExecuteNonQuery', solo actualiza fila o el registro.
        '|
        '| CATCH : Capturamos el error, si falla la conexión o no se puede actualizar el registro.
        '|      * Mostramos un mensaje con el error.
        '|

        If MsgBox("¿Está seguro que desea CERRAR la aplicación?", vbQuestion + vbYesNo, "Segundos Fuera") = vbNo Then
            e.Cancel = True
            Exit Sub
        End If

        Try
            Dim connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

            Using sqlConnection As New MySqlConnection(connectionString)

                sqlConnection.Open()
                Dim sqlQuery As String = "UPDATE sesion_user SET fh_salida = @fecha ORDER BY id_reg DESC LIMIT 1"

                Using sqlCommand As New MySqlCommand(sqlQuery, sqlConnection)

                    sqlCommand.Parameters.AddWithValue("@fecha", DateTime.Now)
                    sqlCommand.ExecuteNonQuery()

                End Using

            End Using

        Catch ex As Exception
            MsgBox("Error al registrar salida : " & ex.Message)

        End Try

    End Sub
    Private Sub FrmMdiMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        '|--------------------------------------------------------------------------------
        '| CERRAR TODOS LOS FORMULARIOS
        '|-----------------------------
        '| * Cerramos toda la aplicación de forma ordenada. Esto ordena el cierre de todos
        '|   los hilos de forma segura, después de ejecutar el código de ‘FormClosing’.

        Application.Exit()

    End Sub
    ''
    ''
    ''
    Private Sub BtnClientPayments_Click(sender As Object, e As EventArgs) Handles BtnClientPayments.Click

        '|-----------------------------------------------------------------------
        '| MOSTRAR FORMULARIO HIJO DENTRO CONTENEDOR MDI
        '|----------------------------------------------
        '| * Llamamos a la función 'ShowFormChild' y le pasamos como parámetro el
        '|   formulario hijo 'FrmClientsPayments'

        ShowFormChild(FrmClientsPayments)

    End Sub
    ''
    ''
    ''
    Private Sub BtnFamilyGroup_Click(sender As Object, e As EventArgs) Handles BtnFamilyGroup.Click

        '|-----------------------------------------------------------------------
        '| MOSTRAR FORMULARIO HIJO DENTRO CONTENEDOR MDI
        '|----------------------------------------------
        '| * Llamamos a la función 'ShowFormChild' y le pasamos como parámetro el
        '|   formulario hijo 'FrmFamilyGroup'

        ShowFormChild(FrmFamilyGroup)

    End Sub
    ''
    ''
    ''
    Private Sub BtnOutstandingPayments_Click(sender As Object, e As EventArgs) Handles BtnOutstandingPayments.Click

        '|-----------------------------------------------------------------------------
        '| MOSTRAR FORMULARIO HIJO DENTRO CONTENEDOR MDI
        '|----------------------------------------------
        '| * Llamamos a la función 'ShowFormChild' y le pasamos como parámetro el
        '|   formulario hijo 'FrmListDebtors'

        ShowFormChild(FrmListDebtors)

    End Sub
    ''
    ''
    ''
    Private Sub BtnPriceAndDiscounts_Click(sender As Object, e As EventArgs) Handles BtnPriceAndDiscounts.Click

        '|-----------------------------------------------------------------------------
        '| MOSTRAR FORMULARIO HIJO DENTRO CONTENEDOR MDI
        '|----------------------------------------------
        '| * Llamamos a la función 'ShowFormChild' y le pasamos como parámetro el
        '|   formulario hijo 'FrmDiscountTable'

        ShowFormChild(FrmDiscountTable)

    End Sub
    ''
    ''
    ''
    Private Sub BtnGoOut_Click(sender As Object, e As EventArgs) Handles BtnGoOut.Click

        '|-----------------------------------------------------------------------------
        '| CERRAR FORMULARIO PRINCIPAL
        '|----------------------------
        '| * Me.Close() activa automáticamente el FormClosing del formulario principal.

        Me.Close()

    End Sub
    ''
    ''
    ''
    Private Sub ShowFormChild(formChild As Form)

        '|---------------------------------------------------------------------------------
        '| FUNCION PARA MOSTRAR UN FORMULARIO HIJO DENTRO DEL MDIFORM
        '|-----------------------------------------------------------
        '| IF : Comprobamos si el formulario está abierto.
        '|
        '|      IF : Comprobamos si el formulario esta minimizado.
        '|          * Restauramos el formulario
        '|
        '|      * Si el formulario está oculto lo traemos al frente.
        '|
        '| ELSE : Cargamos el formulario por primera vez.
        '|      * Con 'MdiParent' asignamos al formulario 'FrmMdiMain' como contenedor MDI.
        '|      * Usamos 'Show' para mostrar el formulario hijo, que recibimos por parámetro,
        '|        dentro del contenedor MDI.

        If formChild.Visible Then

            If formChild.WindowState = FormWindowState.Minimized Then
                formChild.WindowState = FormWindowState.Normal
            End If
            formChild.Activate()

        Else
            formChild.MdiParent = Me
            formChild.Show()

        End If

    End Sub

End Class