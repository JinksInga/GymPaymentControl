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
            Dim dtoClone = DirectCast(dto.CloneInterface(), IPaymentCalculable)

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
        ''' Abre el formulario para modificar datos cliente y ejecuta una acción al finalizar con éxito.
        ''' </summary>
        Public Sub OpenFrmModifyClient(clientData As ClientPaymentDTO, refreshAction As Action(Of Integer))

            ' 1. Buscamos si el formulario ya está abierto
            Dim form = FrmMdiMain.MdiChildren.OfType(Of FrmNewModifyClient)().FirstOrDefault()

            If form IsNot Nothing Then
                ' 2. Si ya está abierto, comprobamos si tiene cambios pendientes
                If form.HasUnsavedChanges() Then
                    Dim strMsgbox As String = "                ¡ ¡ ¡  ATENCIÓN  ! ! !" & Environment.NewLine & Environment.NewLine &
                        "     Ya tienes una edición abierta con cambios sin guardar." & Environment.NewLine &
                        "     ___________________________________________________________" & Environment.NewLine & Environment.NewLine &
                        "     ¿Deseas descartar esos cambios y cargar el nuevo cliente?" & Environment.NewLine & Environment.NewLine &
                        "            SÍ : Carga al nuevo cliente (se pierden los cambios actuales)." & Environment.NewLine &
                        "            NO : Seguir editando el cliente actual."

                    Dim result = MessageBox.Show(strMsgbox, "Cambios pendientes",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning,
                                                 MessageBoxDefaultButton.Button2)

                    If result = DialogResult.No Then
                        ' El usuario quiere terminar lo que estaba haciendo
                        form.Activate()
                        Return
                    End If
                End If
                ' Si pulsa SÍ o no había cambios, el código sigue hacia abajo y actualiza los datos
            Else
                ' 3. Si no existe, creamos la instancia nueva
                form = New FrmNewModifyClient()
                form.MdiParent = FrmMdiMain
            End If

            ' 4. Carga de datos y visualización (común para nuevo o reutilizado)
            form.SetRefreshAction(refreshAction)
            form.PrepareToModifyClient(clientData) ' Aquí se toma la nueva "foto" original

            form.Show()
            form.BringToFront()
            form.Activate()
        End Sub


    End Module
End Namespace