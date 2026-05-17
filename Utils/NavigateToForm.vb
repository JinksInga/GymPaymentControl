Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.UIHelpers

Namespace Utils
    Public Module NavigateToForm

        ''' <summary>
        ''' Abre el formulario de cobro de forma genérica y ejecuta una acción al finalizar con éxito.
        ''' </summary>
        Public Sub OpenFrmCollectMembership(dto As IPaymentCalculable,
                                            refreshAction As Action,
                                            Optional mode As TransactionMode = TransactionMode.UpdatePayment)

            ' 1. Buscamos si ya existe una instancia abierta para no duplicar
            Dim frmOpen = Application.OpenForms.OfType(Of FrmCollectMembership)().FirstOrDefault()

            If frmOpen IsNot Nothing Then
                frmOpen.Activate() ' Si ya está abierto, lo traemos al frente
                Return
            End If

            ' 2. Si no está abierto, creamos la instancia
            ' Clonamos el DTO para trabajar sobre una copia segura
            ' Centralizamos el clonado para proteger los datos originales
            Dim dtoClone = DirectCast(dto.CloneInterface(), IPaymentCalculable)
            Dim form As New FrmCollectMembership()

            ' 3. Configuración MDI
            form.MdiParent = FrmMdiMain ' IMPORTANTE: Asignamos el padre
            form.PreparePayment(dtoClone, mode)

            ' 4. Manejo del Refresco
            ' Como ya no es modal, usamos el evento FormClosed para ejecutar el refresco
            If refreshAction IsNot Nothing Then

                ' Solo refrescamos si el formulario se cerró con éxito (puedes añadir lógica aquí)
                AddHandler form.FormClosed, Sub(s, e)
                                                refreshAction.Invoke()
                                            End Sub

            End If

            form.Show() ' Ahora es no bloqueante

            '' Centralizamos el clonado para proteger los datos originales
            'Dim dtoClone = DirectCast(dto.CloneInterface(), IPaymentCalculable)

            'Using form As New FrmCollectMembership()
            '    form.PreparePayment(dtoClone, mode) 'TransactionMode.UpdatePayment)

            '    If form.ShowDialog() = DialogResult.OK Then
            '        ' El operador ?. asegura que si no pasamos acción, no explote
            '        refreshAction?.Invoke()
            '    End If
            'End Using

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
            Dim frmOpen = FrmMdiMain.MdiChildren.OfType(Of FrmNewModifyClient)().FirstOrDefault()

            If frmOpen IsNot Nothing Then
                ' 2. Si ya está abierto, comprobamos si tiene cambios pendientes
                If frmOpen.HasUnsavedChanges() Then

                    ' Construimos el mensaje
                    Dim result = MessageBox.Show(UnsavedChangesWarning("actualizados", "actualizar"), "Cambios pendientes",
                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If result = DialogResult.Yes Then
                        ' CASO SI : Activamos y mostramos el formulario.
                        frmOpen.Activate()
                        Return
                    Else
                        ' CASO NO : Destruir el formulario para que no quede "flotando"
                        ' En lugar de form.Close(), usamos el cierre forzado
                        frmOpen.ForceClose()
                        Return
                    End If
                End If
                ' Si pulsa SÍ o no había cambios, el código sigue hacia abajo y actualiza los datos
            Else
                ' 3. Si no existe, creamos la instancia nueva
                frmOpen = New FrmNewModifyClient()
                frmOpen.MdiParent = FrmMdiMain
            End If

            ' 4. Carga de datos y visualización (común para nuevo o reutilizado)
            frmOpen.SetRefreshAction(refreshAction)
            frmOpen.PrepareToModifyClient(clientData) ' Aquí se toma la nueva "foto" original

            frmOpen.Show()
            frmOpen.BringToFront()
            frmOpen.Activate()
        End Sub


    End Module
End Namespace