Namespace Interfaces
    Public Interface ISelectableRow
        '(ReadOnly Property IsSummaryRow As Boolean) lo reemplazamos
        'por (Inherits IPaymentSummary) usando herencia de interfaces.
        Inherits IPaymentSummary
        ReadOnly Property IdPayment As Integer

    End Interface

End Namespace
