Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.UIHelpers

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

                    ' Construimos el mensaje
                    Dim result = MessageBox.Show(UnsavedChangesWarning("guardados", "guardar"), "Registro pendiente",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.No Then
                        ' Destruir el formulario para que no quede "flotando"
                        ' En lugar de form.Close(), usamos el cierre forzado
                        form.ForceClose()
                        Return
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
            form.Activate()

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

                    ' Construimos el mensaje
                    Dim result = MessageBox.Show(UnsavedChangesWarning("actualizados", "actualizar"), "Cambios pendientes",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        ' CASO SI : Activamos y mostramos el formulario.
                        form.Activate()
                        Return
                    Else
                        ' CASO NO : Destruir el formulario para que no quede "flotando"
                        ' En lugar de form.Close(), usamos el cierre forzado
                        form.ForceClose()
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