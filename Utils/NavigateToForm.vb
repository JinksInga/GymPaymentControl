Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Interfaces

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

    End Module
End Namespace