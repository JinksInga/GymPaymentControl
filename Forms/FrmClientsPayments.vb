Imports System.Data.Common
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports MySql.Data.MySqlClient

Public Class FrmClientsPayments

    ' Instanciamos el manager para acceder a la lógica de negocio
    Private ReadOnly _paymentManager As New PaymentManager()

    ' También es recomendable tener la lista de clientes a nivel de clase
    ' para las búsquedas rápidas que haremos mañana
    Private _clientsList As List(Of IndividualPaymentDTO)

    Private Sub FrmClientsPayments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' 1. Limpieza inicial
            DisableButtons()
            CleanControls()

            ' 2. Verificación inteligente
            ' Usamos el manager que ya tienes instanciado
            BtnFindClient.Enabled = _paymentManager.HasClients()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error de Inicio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub




    '| ---------------------------------------------------- '
    '| ---------->>>>>>>>>> SUBRUTINAS <<<<<<<<<<---------- '
    '| ---------------------------------------------------- '

    Private Sub Sub_Activate_Buttons()

        '| -----------------------------
        '| * ACTIVAMOS LOS BOTONES
        '| -----------------------------

        BtnFindClient.Enabled = True
        BtnModifyData.Enabled = True
        BtnDeleteClient.Enabled = True
        'BtnCollectMonth.Enabled = True
        BtnNewPayment.Enabled = True
        DgvPaymentList.Enabled = True

    End Sub

    Private Sub DisableButtons()

        '| -----------------------------
        '| * DESACTIVAMOS LOS CONTROLES
        '| -----------------------------

        'BtnFindClient.Enabled = False
        BtnModifyData.Enabled = False
        BtnDeleteClient.Enabled = False
        BtnCollectMonth.Enabled = False
        BtnNewPayment.Enabled = False
        DgvPaymentList.Enabled = False

    End Sub

    Private Sub Sub_SelectRecord_CancelSearch()

        '| * Activamos el botón BtnNewClient.
        '| * Mostramos el botón BtnCancelSearch y ocultamos el botón BtnFindClient.
        '| * Desactivamos el contenedor PnlBuscar.
        '| * Limpiamos el CmbFiltrar cambiando el index a cero.
        '| * Desactivamos el RbActivo, para que al momento de hacer clic en BtnBuscar nos muestre todos los clientes.
        '| * Ocultamos el DgvClientes.
        '| * Desactivvamos el GbEstado para no cambiar de valor los RadioButton.

        BtnNewClient.Enabled = True
        BtnFindClient.Visible = True
        BtnCancelSearch.Visible = False
        PnlBuscar.Enabled = False
        CmbFiltrar.SelectedIndex = 0
        RbActivo.Checked = False
        DgvClientes.Visible = False
        LblResult.Visible = False
        GbEstado.Enabled = False

    End Sub

    Private Sub Sub_Search_Record()

        '| * Desactivamos el botón BtnNewClient.
        '| * Ocultamos el botón BtnFindClient y mostramos el botón BtnCancelSearch.
        '| * Activamos los dos contenedores PnlBuscar y GbEstado.
        '| * Cambiamos el texto del PnlBuscar seleccionando el index 1 (NOMBRE).
        '| * Activamos el RbActivo para mostrar y buscar los clientes en actividad.
        '| * Mostramos el DgvClientes y lo ponemos delante de los otros controles usando BringToFront.

        BtnNewClient.Enabled = False
        BtnFindClient.Visible = False
        BtnCancelSearch.Visible = True
        PnlBuscar.Enabled = True
        GbEstado.Enabled = True
        CmbFiltrar.SelectedIndex = 1
        RbActivo.Checked = True
        DgvClientes.Visible = True
        LblResult.Visible = True
        DgvClientes.BringToFront()

    End Sub

    Private Sub CleanControls()

        '| * Recorremos todos los controles que están dentro del contenedor PnlDatosCliente.
        '|    * Comprobamos si los controles son de tipo Label.
        '|       * Comprobamos si el Name del control contiene "Cli".
        '|       * Limpiamos el texto del label.
        ''
        '| * Limpiamos la variable que contiene el id del cliente para hacer comprobaciones _
        '|   _ cuando se hacen cambios en el TxtBuscar.

        For Each control As Control In PnlDatosCliente.Controls
            If TypeOf (control) Is Label Then
                If control.Name.Contains("Cli") Then
                    control.Text = ""
                End If
            End If
        Next
        DgvPaymentList.Rows.Clear()
        'strIdClient = ""
    End Sub

    Private Sub Sub_Fill_Data_DgvClientes()

        '| * Depúes de confirmar la busqueda llenamos los label con información del cliente que
        '|   obtenemos del DgvClientes y lo ocultamos para visualizar el resultado.

        With DgvClientes
            'strIdClient = .CurrentRow.Cells(0).Value
            LblNomCli.Text = .CurrentRow.Cells(1).Value
            LblApeCli.Text = .CurrentRow.Cells(2).Value
            FnacimientoCorto.Text = .CurrentRow.Cells(3).Value
            LblFdnCli.Text = .CurrentRow.Cells(4).Value
            LblEdadCli.Text = .CurrentRow.Cells(5).Value
            LblTlfCli.Text = .CurrentRow.Cells(6).Value
            LblEmlCli.Text = .CurrentRow.Cells(7).Value
            LblDirCli.Text = .CurrentRow.Cells(8).Value
            LblMtdPgoCli.Text = .CurrentRow.Cells(9).Value
            FregistroCorto.Text = .CurrentRow.Cells(10).Value
            LblFdiCli.Text = .CurrentRow.Cells(11).Value
            LblEstCli.Text = .CurrentRow.Cells(12).Value
            'strIdGrpFamily = .CurrentRow.Cells(13).Value
        End With
    End Sub

    Private Sub ChangeBackColorLabel()

        'COMPROBAMOS EL BACKCOLOR
        If LblNomCli.BackColor = Color.MintCream Then
            'RECORREMOS TODOS LOS LABEL QUE CUMPLAN LA CONDIÓN PARA CAMBIAR EL COLOR
            For Each control As Control In GbDatosCliente.Controls
                If TypeOf (control) Is Label Then
                    If control.Name.Contains("Cli") Then
                        control.BackColor = Color.WhiteSmoke
                    End If
                End If
            Next
        Else
            'RECORREMOS TODOS LOS LABEL QUE CUMPLAN LA CONDIÓN PARA CAMBIAR EL COLOR
            For Each control As Control In GbDatosCliente.Controls
                If TypeOf (control) Is Label Then
                    If control.Name.Contains("Cli") Then
                        control.BackColor = Color.MintCream
                    End If
                End If
            Next
        End If

    End Sub
    ''
    ''
    ''
End Class