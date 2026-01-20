Imports GymPaymentControl.Interfaces

Namespace Utils
    Public Module PaymentCalculator

        Public Sub CalculateProratedPayment(item As IPaymentCalculable)

            item.Total = item.PrcPgs - item.DscPgs

            Dim daysInMonth = DateTime.DaysInMonth(item.FdiPgs.Year, item.FdiPgs.Month)

            item.DaysOfMonth = (daysInMonth - item.FdiPgs.Day) + 1
            item.TotalToPay = (item.Total / daysInMonth) * item.DaysOfMonth

        End Sub

    End Module
End Namespace