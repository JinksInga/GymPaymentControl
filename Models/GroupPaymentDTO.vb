Imports GymPaymentControl.Interfaces

Public Class GroupPaymentDTO

    Implements ISelectableRow
    Implements IPaymentSummary

    Public Property IdPgs As Integer
    Public Property IdGrp As Integer
    Public Property GroupName As String
    Public Property Members As String
    Public Property PrcPgs As Decimal
    Public Property DscPgs As Decimal
    Public Property Total As Decimal
    Public Property TotalToPay As Decimal
    Public Property FdiPgs As DateTime
    Public Property LongDate As String
    Public Property IsSummaryRow As Boolean Implements IPaymentSummary.IsSummaryRow
    Public Property DaysOfMonth As Integer
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