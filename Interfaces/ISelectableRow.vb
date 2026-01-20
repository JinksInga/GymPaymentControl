Namespace Interfaces

    ' Define el contrato para filas que pueden ser seleccionadas
    ' en un DataGridView.
    '
    ' Hereda de IPaymentSummary para poder distinguir automáticamente
    ' entre filas seleccionables y filas de resumen.
    Public Interface ISelectableRow

        Inherits IPaymentSummary

        ' Identificador del pago asociado a la fila.
        ' Permite trabajar de forma genérica con pagos
        ' individuales o grupales.
        ReadOnly Property IdPayment As Integer

    End Interface

End Namespace