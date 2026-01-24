Namespace Interfaces

    ' Define el contrato para cualquier entidad que pueda ser
    ' utilizada en cálculos financieros de pagos.
    '
    ' Esta interfaz permite aplicar lógica de cálculo genérica
    ' (por ejemplo, prorrateo mensual) sin conocer el tipo concreto
    ' del objeto (individual, grupal, etc.).
    Public Interface IPaymentCalculable

        ' Mostrar el nombre completo y la edad del cliente o el nombre del grupo familiar
        ReadOnly Property DisplayName As String

        ' Identificador único para poder hacer el UPDATE en la BD
        Property IdPgs As Integer

        ' Precio base del pago
        Property PrcPgs As Decimal

        ' Descuento aplicado al pago
        Property DscPgs As Decimal

        ' Fecha de pago (Fecha de inicio del mes)
        Property FdiPgs As Date

        ' Fecha de Pago (Según el valor del DateTimepicker - manual)
        Property FdpPgs As Date

        ' Total calculado (precio - descuento)
        Property Total As Decimal

        ' Número de días considerados para el cálculo
        Property DaysOfMonth As Integer

        ' Importe final a pagar (prorrateado o total)
        Property TotalToPay As Decimal

        ' Método de pago
        Property MtdPgs As String

        ' Método para crear una copia exacta del objeto
        Function Clone() As IPaymentCalculable

        ''
        ''
    End Interface
End Namespace