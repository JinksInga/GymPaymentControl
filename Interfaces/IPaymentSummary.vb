Namespace Interfaces

    ' Marca una entidad como fila de resumen dentro de una colección
    ' de pagos.
    '
    ' Se utiliza principalmente en la capa de presentación
    ' (DataGridView) para:
    ' - aplicar estilos visuales distintos
    ' - evitar selecciones inválidas
    Public Interface IPaymentSummary

        ' Indica si la fila representa un resumen
        ' (true = fila de resumen, false = fila de detalle)
        ReadOnly Property IsSummaryRow As Boolean

    End Interface

End Namespace