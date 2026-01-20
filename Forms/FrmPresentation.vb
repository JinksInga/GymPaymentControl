Imports System.Configuration
Imports MySql.Data.MySqlClient

Public Class FrmPresentation
    ''
    ''
    ''
    ''
    '| Se declara Async para permitir operaciones no bloqueantes (Await)
    Private Async Sub FrmPresentation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '|--------------------------------------------------------------------------------------
        '| COMPROBAR CONEXIÓN, MOSTRAR TEXTO Y BARRA DE PROGRESO
        '|------------------------------------------------------
        '|
        '| * Llamamos al método 'SetProgress' y le pasamos valores para el porcentaje de la barra
        '|   de progreso, el texo de información y el color del texo.
        '| * Hacemos una pausa de 500 milisegundos (0,5 segundos) para que el usuario pueda ver
        '|   el mensaje sin bloquear la aplicación y la interfaz gráfica.
        '|
        '| IF : Comprobamos la conexión llamando al método 'CheckConnection', 'AddressOf' pasa
        '|      la referencia del método y la ejecútala cuando corresponda, mientras se ejecuta
        '|      en segundo plano 'Task', espera sin bloquear la UI 'Await', y si falla la conexión
        '|      'Not', entra al If.
        '|      * Llamamos a la subrutina 'ShowCriticalError' y salimos del procedimiento 'Return'.
        '|
        '| * Volvemos a llamar al método 'SetProgress' y hacemos otra pausa (x2).
        '|
        '| * Creamos una instancia del formulario 'FrmUserPassword'
        '| * Mostramos el formualrio 'FrmUserPassword'
        '| * Ocultamos el formulario 'FrmPresentation'

        SetProgress(33, "Iniciando conexión con la Base de Datos.", Color.Red)
        Await Task.Delay(1) '500

        If Not Await Task.Run(AddressOf CheckConnection) Then
            ShowCriticalError()
            Return
        End If

        SetProgress(66, "Conexión exitosa.", Color.Green)
        Await Task.Delay(1) '600

        SetProgress(100, "Iniciando el programa.", Color.Black)
        Await Task.Delay(1) '600

        Dim login As New FrmUserPassword()
        login.Show()
        Me.Hide()

    End Sub
    ''
    ''
    ''
    Public Function CheckConnection() As Boolean

        '|---------------------------------------------------------------------------------
        '| COMPROBAR CONEXIÓN CON MYSQL
        '|-----------------------------
        '|
        '| * Obtenemos la cadena de conexión desde el archivo de configuración 'App.config'
        '|   y lo guardamos en 'connectionMySQL'.
        '|
        '| IF : Si falla la conexión (no existe la cadena), devolvemos 'False'
        '|
        '| TRY : Se crea y abre la conexión dentro de 'Using' nos garantiza que la conexión
        '|       se cierre correctamente y devolvemos 'True' para informar que la conexión
        '|       es correcta.
        '|
        '| CATCH : Controlar un error específico de MySQL y cualquier otro error no controlado.

        Dim connectionMySQL = ConfigurationManager.ConnectionStrings("MyConnectionMySQL")

        If connectionMySQL Is Nothing Then Return False

        Try
            Using connectionString As New MySqlConnection(connectionMySQL.ConnectionString)

                connectionString.Open()
                Return True

            End Using

        Catch ex As MySqlException
            Return False

        Catch ex As Exception
            Return False

        End Try

    End Function
    ''
    ''
    ''
    Private Sub SetProgress(value As Integer, text As String, color As Color)

        '|-------------------------------------------------------------------
        '| ACTUALIZAR BARRA DE PROGRESO Y TEXTO DE ESTADO
        '|-----------------------------------------------
        '|
        '| * Pinta la barra de progreso hasta el % recibido.
        '| * Actualiza el texto de la etiqueta 'LblBarra'.
        '| * Cambia el color del texto de la etiqueta 'LblBarra'.
        '| * Forzamos al formulario a repintar la etiqueta inmediatamente para
        '|   que se vea el cambio sin retrasos.

        BarraProgreso.Value = value
        LblBarra.Text = text
        LblBarra.ForeColor = color
        LblBarra.Refresh()

    End Sub
    ''
    ''
    ''
    Private Sub ShowCriticalError()

        '|---------------------------------------------------------------------
        '| MOSTRAR MENSAJE DE ERROR
        '|-------------------------
        '|
        '| * Llamamos al método 'SetProgress' y le pasamos CERO para limpiar la
        '|   barra de progreso, un texo de información y el color del texo.
        '| * Mostramos un mensaje error informando que no hay conexión.
        '| * Cerramos el formulario 'FrmPresentation'.

        SetProgress(0, "Error crítico: Problemas con la conexión.", Color.Red)

        MsgBox("   No hay conexión con la Base de Datos." & vbCrLf & vbCrLf &
               "   LLamar a Jinkano.", vbCritical, "Fallo de Conexión")
        Me.Close()

    End Sub

End Class