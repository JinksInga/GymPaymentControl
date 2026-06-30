Imports System.Configuration
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports MySql.Data.MySqlClient

Public Class FrmMdiMain

    ' Variable especial vinculada a los eventos del formulario de tarifas
    Private WithEvents _frmTariffsEvents As FrmPricesAndDiscounts

    Private Sub FrmMdiMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '|----------------------------------------------------------------------
        '| INICIALIZACIÓN DE SESIÓN Y CONTROL DE BLOQUEO POR TARIFAS FALTANTES |
        '|----------------------------------------------------------------------

        Try
            Me.Text = $"{Me.Text} {UserSession.UserName} - {UserSession.Role}"

            Dim tariffManager As New TariffManager()

            Dim hasTariffs As Boolean = tariffManager.CheckIfTariffsExist()

            If hasTariffs Then

                FrmListDebtors.MdiParent = Me
                FrmListDebtors.Show()
                BtnPriceAndDiscounts.Enabled = (UserSession.Role = "ADMINISTRADOR")

            Else

                BtnClientPayments.Enabled = False
                BtnFamilyGroup.Enabled = False
                BtnOutstandingPayments.Enabled = False

                If UserSession.Role = "ADMINISTRADOR" Then

                    _frmTariffsEvents = FrmPricesAndDiscounts

                    ShowFormChild(FrmPricesAndDiscounts)

                    MessageBox.Show("                     ⚠️ CONFIGURACIÓN REQUERIDA ⚠️" & Environment.NewLine &
                                    " ------------------------------------------------------------------------------" & Environment.NewLine & Environment.NewLine &
                                    "  El sistema se ha iniciado en modo restringido debido a que" & Environment.NewLine &
                                    "  no existen tarifas registradas." & Environment.NewLine & Environment.NewLine &
                                    "  Configure la TARIFA BASE para desbloquear el programa.",
                                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Else

                    BtnPriceAndDiscounts.Enabled = False

                    MessageBox.Show("                          ❌ ACCESO RESTRINGIDO ❌" & Environment.NewLine &
                                    " ------------------------------------------------------------------------------" & Environment.NewLine & Environment.NewLine &
                                    "  El sistema se encuentra bloqueado debido a que no existen" & Environment.NewLine &
                                    "  existen tarifas configuradas." & Environment.NewLine & Environment.NewLine &
                                    "  Solicite a un ADMINISTRADOR que configure los precios.",
                                    "Sistema bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("ERROR AL INICIO DE LA APP : " & ex.Message)
        End Try

    End Sub
    Private Sub FrmMdiMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        '|------------------------------------------------------------
        '| COMPROBAR RESPUESTA Y REGISTRO DE SALIDA EN BASE DE DATOS |
        '|------------------------------------------------------------

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
            MsgBox("ERROR AL REGISTRAR LA SALIDA : " & ex.Message)
        End Try

    End Sub
    Private Sub FrmMdiMain_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ' Cerramos toda la aplicación de forma ordenada. Esto ordena el cierre de todos
        ' los hilos de forma segura, después de ejecutar el código de ‘FormClosing’.
        Application.Exit()
    End Sub


    Private Sub BtnClientPayments_Click(sender As Object, e As EventArgs) Handles BtnClientPayments.Click
        ShowFormChild(FrmClientsPayments)
    End Sub


    Private Sub BtnFamilyGroup_Click(sender As Object, e As EventArgs) Handles BtnFamilyGroup.Click
        ShowFormChild(FrmFamilyGroup)
    End Sub


    Private Sub BtnOutstandingPayments_Click(sender As Object, e As EventArgs) Handles BtnOutstandingPayments.Click
        ShowFormChild(FrmListDebtors)
    End Sub


    Private Sub BtnPriceAndDiscounts_Click(sender As Object, e As EventArgs) Handles BtnPriceAndDiscounts.Click

        ' Enlazamos la instancia global a nuestra variable para escuchar sus eventos.
        _frmTariffsEvents = FrmPricesAndDiscounts

        ShowFormChild(FrmPricesAndDiscounts)
    End Sub


    Private Sub BtnGoOut_Click(sender As Object, e As EventArgs) Handles BtnGoOut.Click
        ' Me.Close() activa automáticamente el FormClosing del formulario principal.
        Me.Close()
    End Sub


    ''' <summary>
    ''' Orquesta la apertura y el posicionamiento de los formularios hijo dentro del contenedor MDI principal, 
    ''' garantizando un comportamiento de instancia única en la interfaz de usuario.
    ''' </summary>
    ''' <param name="formChild">La instancia del formulario secundario que se desea renderizar o traer al frente.</param>
    ''' <remarks>
    ''' El método evalúa el estado actual de la pantalla para optimizar la experiencia de usuario (UX):
    ''' <list type="bullet">
    ''' <item>
    ''' <description><bold>Si el formulario ya está visible:</bold> Comprueba si está minimizado para restaurarlo a su tamaño normal y utiliza <italic>Activate()</italic> para darle el foco visual instantáneo.</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Si el formulario está cerrado u oculto:</bold> Le asigna el <italic>MdiParent</italic> apuntando al contenedor actual (<italic>Me</italic>) y lo inicializa en pantalla con el método <italic>Show()</italic>.</description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ShowFormChild(formChild As Form)

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


    ''' <summary>
    ''' Escucha el evento lanzado por el formulario de tarifas al cerrarse y bloquea o libera 
    ''' la navegación del menú principal según las reglas de negocio.
    ''' </summary>
    Private Sub OnTariffFormClosing(sender As Object, totalRows As Integer) Handles _frmTariffsEvents.TariffClosingValidation

        If totalRows > 0 Then

            BtnClientPayments.Enabled = True
            BtnFamilyGroup.Enabled = True
            BtnOutstandingPayments.Enabled = True
        Else

            BtnClientPayments.Enabled = False
            BtnFamilyGroup.Enabled = False
            BtnOutstandingPayments.Enabled = False

            MessageBox.Show("              ⚠❌ CONTROL DE PAGOS BLOQUEADO ❌⚠️" & Environment.NewLine &
                            " ----------------------------------------------------------------------------" & Environment.NewLine & Environment.NewLine &
                            "  No se puede operar con el PROGRAMA porque no existen" & Environment.NewLine &
                            "  tarifas configuradas." & Environment.NewLine & Environment.NewLine &
                            "  Por favor, cree al menos la MENSUALIDAD GENERAL para" & Environment.NewLine &
                            "  activar el sistema.",
                            "Alerta de configuración", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub


End Class