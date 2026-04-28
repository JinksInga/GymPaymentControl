Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models

Namespace Utils
    Public Module NavigateToForm

        ''' <summary>
        ''' Abre el formulario de cobro de forma genérica y ejecuta una acción al finalizar con éxito.
        ''' </summary>
        Public Sub OpenFrmCollectMembership(dto As IPaymentCalculable, refreshAction As Action)
            ' Centralizamos el clonado para proteger los datos originales
            Dim dtoClone = DirectCast(dto.Clone(), IPaymentCalculable)

            Using form As New FrmCollectMembership()
                form.PreparePayment(dtoClone, TransactionMode.UpdatePayment)

                If form.ShowDialog() = DialogResult.OK Then
                    ' El operador ?. asegura que si no pasamos acción, no explote
                    refreshAction?.Invoke()
                End If
            End Using
        End Sub

        ''' <summary>
        ''' Abre el formulario para registrar un nuevo cliente y ejecuta una acción al finalizar con éxito.
        ''' </summary>
        Public Sub OpenFrmNewClient(refreshAction As Action(Of Integer))
            ' 1. Buscamos si ya existe la ventana
            Dim form = FrmMdiMain.MdiChildren.OfType(Of FrmNewModifyClient)().FirstOrDefault()

            If form IsNot Nothing Then
                ' 2. Si existe y tiene datos, preguntamos
                If form.HasUnsavedChanges() Then

                    ' Construimos el cuerpo del mensaje tal cual tu imagen
                    Dim strMsgbox As String = "                                   ¡ ¡ ¡  ATENCIÓN  ! ! !" & Environment.NewLine & Environment.NewLine &
                        "     Tienes un formulario abierto con información del cliente." & Environment.NewLine &
                        "     ___________________________________________________________" & Environment.NewLine & Environment.NewLine &
                        "     ¿Deseas continuar con el proceso?" & Environment.NewLine & Environment.NewLine &
                        "           SI : Muéstrame el formulario con la información." & Environment.NewLine & Environment.NewLine &
                        "           NO : Quiero empezar un nuevo registro." & Environment.NewLine & Environment.NewLine &
                        "           CANCELAR : Cierra la ventana y termina con el proceso."

                    Dim result = MessageBox.Show(strMsgbox, "Registro pendiente",
                                                 MessageBoxButtons.YesNoCancel,
                                                 MessageBoxIcon.Question)

                    If result = DialogResult.No Then
                        ' CASO NO: Limpiar y mostrar vacío
                        form.PrepareForNewClient()

                    ElseIf result = DialogResult.Cancel Then
                        ' CASO CANCELAR: Destruir el formulario para que no quede "flotando"
                        form.Close()
                        Return ' Salimos de la función por completo

                    End If
                End If
            Else
                ' 3. Si no existe, creación normal
                form = New FrmNewModifyClient() With
                    {.MdiParent = FrmMdiMain}
                form.PrepareForNewClient()
            End If

            ' Entregamos la acción de refresco y mostramos
            form.SetRefreshAction(refreshAction)
            form.Show()
            form.BringToFront()
        End Sub


        ''' <summary>
        ''' Abre el formulario para registrar un nuevo cliente y ejecuta una acción al finalizar con éxito.
        ''' </summary>
        Public Sub OpenFrmModifyClient(clientData As ClientPaymentDTO, refreshAction As Action(Of Integer))
            ' Buscamos si ya está abierto (reutilizamos la lógica del anterior)
            Dim form = FrmMdiMain.MdiChildren.OfType(Of FrmNewModifyClient)().FirstOrDefault()

            If form Is Nothing Then
                form = New FrmNewModifyClient()
                form.MdiParent = FrmMdiMain
            End If

            ' Pasamos la acción de refresco y los datos del cliente
            form.SetRefreshAction(refreshAction)
            form.PrepareToModifyClient(clientData)

            form.Show()
            form.BringToFront()
        End Sub
        ''
        ''
    End Module
End Namespace