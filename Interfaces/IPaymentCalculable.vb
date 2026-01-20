Namespace Interfaces

    ' Define el contrato para cualquier entidad que pueda ser
    ' utilizada en cálculos financieros de pagos.
    '
    ' Esta interfaz permite aplicar lógica de cálculo genérica
    ' (por ejemplo, prorrateo mensual) sin conocer el tipo concreto
    ' del objeto (individual, grupal, etc.).
    Public Interface IPaymentCalculable

        ' Identificador único para poder hacer el UPDATE en la BD
        Property IdPgs As Integer

        ' Precio base del pago
        Property PrcPgs As Decimal

        ' Descuento aplicado al pago
        Property DscPgs As Decimal

        ' Fecha de inicio del pago
        Property FdiPgs As Date

        ' Total calculado (precio - descuento)
        Property Total As Decimal

        ' Número de días considerados para el cálculo
        Property DaysOfMonth As Integer

        ' Importe final a pagar (prorrateado o total)
        Property TotalToPay As Decimal

        ' Opcional: Para mostrar el nombre sin hacer Castings complicados
        ReadOnly Property DisplayName As String

    End Interface

End Namespace