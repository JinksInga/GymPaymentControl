Imports GymPaymentControl.Interfaces

Namespace Utils
    Public Module PaymentCalculator

        ''' <summary>
        ''' Calcula el importe final basándose en el método de pago (MENSUAL, GRUPAL o DIARIO)
        ''' </summary>
        Public Sub CalculatePaymentAmount(item As IPaymentCalculable)
            ' 1. Cálculo base (Precio - Descuento)
            item.Total = item.PrcPgs - item.DscPgs

            ' 2. Aplicamos lógica según el método
            If item.MtdPgs = PaymentMethods.Monthly OrElse item.MtdPgs = PaymentMethods.Grupal Then
                ' Lógica de Prorrateo
                Dim daysInMonth = DateTime.DaysInMonth(item.FdiPgs.Year, item.FdiPgs.Month)
                item.DaysOfMonth = (daysInMonth - item.FdiPgs.Day) + 1

                ' Calculamos el proporcional
                item.TotalToPay = (item.Total / daysInMonth) * item.DaysOfMonth
            Else
                ' Lógica para pagos DIARIOS u otros
                item.DaysOfMonth = 1
                item.TotalToPay = item.Total ' Aquí ya lleva el descuento restado
            End If

            ' 3. Opcional: Redondear a 2 decimales para evitar problemas de céntimos
            item.TotalToPay = Math.Round(item.TotalToPay, 2)
        End Sub

    End Module

End Namespace