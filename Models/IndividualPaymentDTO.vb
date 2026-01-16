Imports GymPaymentControl.Interfaces

Namespace Models
    Public Class IndividualPaymentDTO

        Implements ISelectableRow
        Implements IPaymentSummary

        Public Property IdPgs As Integer
        Public Property IdCli As Integer
        Public Property Name As String
        Public Property LastName As String
        Public Property Age As String
        Public Property MtdPgs As String
        Public Property FdiPgs As Date
        Public Property LongDate As String
        Public Property PrcPgs As Decimal
        Public Property DscPgs As Decimal
        Public Property Total As Decimal
        Public Property DaysOfMonth As Integer
        Public Property TotalToPay As Decimal
        Public Property IsSummaryRow As Boolean Implements IPaymentSummary.IsSummaryRow
        Public Property TotalAmountDebt As Decimal
        Public Property NumberMonths As Integer

        Public ReadOnly Property IdPayment As Integer _
        Implements ISelectableRow.IdPayment
            Get
                Return IdPgs
            End Get
        End Property

        Public Property EsFilaResumen As Boolean _
        Implements ISelectableRow.IsSummaryRow

    End Class

End Namespace