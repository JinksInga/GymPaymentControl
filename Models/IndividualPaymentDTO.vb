Imports GymPaymentControl.Interfaces

Namespace Models

    ' DTO que representa un pago individual pendiente de un cliente.
    ' Se utiliza tanto para filas de detalle como para filas de resumen
    ' dentro de los DataGridView.
    Public Class IndividualPaymentDTO

        ' Implementa:
        ' - ISelectableRow → permite identificar si la fila es seleccionable en el Grid
        ' - IPaymentSummary → indica si la fila es una fila de resumen
        ' - IPaymentCalculable → permite aplicar cálculos financieros genéricos
        Implements ISelectableRow
        Implements IPaymentSummary
        Implements IPaymentCalculable

        ' ======================
        ' Identificadores
        ' ======================
        Public Property IdPgs As Integer Implements IPaymentCalculable.IdPgs ' Id del pago
        Public Property IdCli As Integer          ' Id del cliente

        ' ======================
        ' Datos del cliente
        ' ======================
        Public Property Name As String
        Public Property LastName As String

        ' Edad calculada (en años) como texto base
        ' El formato final se expone mediante AgeText
        Public Property Age As String

        ' ======================
        ' Datos del pago
        ' ======================
        Public Property MtdPgs As String          ' Método de pago (MENSUAL / DIARIO)
        Public Property FdiPgs As Date Implements IPaymentCalculable.FdiPgs
        Public Property LongDate As String        ' Fecha formateada para UI

        ' ======================
        ' Valores económicos
        ' ======================
        Public Property PrcPgs As Decimal Implements IPaymentCalculable.PrcPgs
        Public Property DscPgs As Decimal Implements IPaymentCalculable.DscPgs
        Public Property Total As Decimal Implements IPaymentCalculable.Total
        Public Property DaysOfMonth As Integer Implements IPaymentCalculable.DaysOfMonth
        Public Property TotalToPay As Decimal Implements IPaymentCalculable.TotalToPay

        ' ======================
        ' Control de resumen
        ' ======================
        Public Property IsSummaryRow As Boolean Implements IPaymentSummary.IsSummaryRow
        Public Property NumberMonths As Integer   ' Nº de meses agrupados en la fila resumen

        ' ======================
        ' Propiedades calculadas
        ' ======================

        ' Devuelve la edad con formato legible para UI (ej: "46 años")
        Public ReadOnly Property AgeText As String
            Get
                Return $"{Age} años"
            End Get
        End Property

        ' Devuelve el identificador de pago de forma genérica
        ' para selección de filas en DataGridView
        Public ReadOnly Property IdPayment As Integer Implements ISelectableRow.IdPayment
            Get
                Return IdPgs
            End Get
        End Property

        ' Devuelve el nombre completo del cliente y la edad.
        Public ReadOnly Property DisplayName As String Implements IPaymentCalculable.DisplayName
            Get
                Return $"{Me.Name} {Me.LastName} - {Me.AgeText}"
            End Get
        End Property
    End Class

End Namespace