Imports GymPaymentControl.Interfaces

Namespace Models
    Public Class IndividualPaymentDTO

        Implements ISelectableRow
        Implements IPaymentSummary
        Implements IPaymentCalculable

        Public Property IdPgs As Integer
        Public Property IdCli As Integer
        Public Property Name As String
        Public Property LastName As String
        Public Property Age As String
        Public Property MtdPgs As String
        Public Property FdiPgs As Date Implements IPaymentCalculable.FdiPgs
        Public Property LongDate As String
        Public Property PrcPgs As Decimal Implements IPaymentCalculable.PrcPgs
        Public Property DscPgs As Decimal Implements IPaymentCalculable.DscPgs
        Public Property Total As Decimal Implements IPaymentCalculable.Total
        Public Property DaysOfMonth As Integer Implements IPaymentCalculable.DaysOfMonth
        Public Property TotalToPay As Decimal Implements IPaymentCalculable.TotalToPay
        Public Property IsSummaryRow As Boolean Implements IPaymentSummary.IsSummaryRow
        Public Property NumberMonths As Integer
        Public Property TotalAmountDebt As Decimal

        Public ReadOnly Property AgeText As String
            Get
                Return $"{Age} años"
            End Get
        End Property

        Public ReadOnly Property IdPayment As Integer Implements ISelectableRow.IdPayment
            Get
                Return IdPgs
            End Get
        End Property

    End Class

End Namespace