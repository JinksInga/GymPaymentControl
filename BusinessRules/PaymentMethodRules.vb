Imports GymPaymentControl.Constants

Namespace BusinessRules

    Public Module PaymentMethodRules

        ''' <summary>
        ''' Determina si se permite cambiar el método de pago cuando existe
        ''' una deuda pendiente.
        ''' </summary>
        ''' <param name="currentPaymentMethod">
        ''' Método de pago actual del cliente.
        ''' </param>
        ''' <param name="newPaymentMethod">
        ''' Método de pago que se desea establecer.
        ''' </param>
        ''' <param name="hasPendingDebt">
        ''' Indica si el cliente tiene alguna deuda pendiente.
        ''' </param>
        ''' <returns>
        ''' True si el cambio está permitido; False si debe bloquearse.
        ''' </returns>
        Public Function CanChangePaymentMethod(currentPaymentMethod As String,
                                               newPaymentMethod As String,
                                               hasPendingDebt As Boolean) As Boolean

            If Not hasPendingDebt Then Return True

            ' Los cambios entre métodos individuales están permitidos.
            Dim currentIsIndividual As Boolean = currentPaymentMethod = PaymentMethods.Monthly OrElse
                                                 currentPaymentMethod = PaymentMethods.Daily

            Dim newIsIndividual As Boolean = newPaymentMethod = PaymentMethods.Monthly OrElse
                                             newPaymentMethod = PaymentMethods.Daily

            If currentIsIndividual AndAlso newIsIndividual Then
                Return True
            End If

            ' Cualquier entrada o salida de GRUPAL con deuda
            ' queda bloqueada.
            Return False

        End Function

    End Module

End Namespace