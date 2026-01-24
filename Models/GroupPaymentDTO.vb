Imports GymPaymentControl.Interfaces

Namespace Models

    ' DTO que representa un pago grupal (familia).
    ' Se utiliza tanto para filas de detalle como para filas
    ' de resumen dentro del DataGridView.
    Public Class GroupPaymentDTO

        ' Implementa las mismas interfaces que el DTO individual
        ' para permitir reutilización de lógica y UI común
        Implements ISelectableRow
        Implements IPaymentSummary
        Implements IPaymentCalculable

        ' ======================
        ' Creamos una copia superficial del objeto actual
        ' ======================
        Public Function Clone() As IPaymentCalculable Implements IPaymentCalculable.Clone
            Return DirectCast(Me.MemberwiseClone(), GroupPaymentDTO)
        End Function

        ' ======================
        ' Identificadores
        ' ======================
        Public Property IdPgs As Integer Implements IPaymentCalculable.IdPgs ' Id del pago
        Public Property IdGrp As Integer            ' Id del grupo familiar

        ' ======================
        ' Datos del grupo
        ' ======================
        Public Property GroupName As String
        Public Property Members As String           ' Lista concatenada de integrantes

        ' ======================
        ' Datos del pago
        ' ======================
        Public Property MtdPgs As String Implements IPaymentCalculable.MtdPgs ' Método de pago (GRUPAL)
        Public Property FdiPgs As Date Implements IPaymentCalculable.FdiPgs ' Fecha de inicio
        Public Property FdpPgs As Date Implements IPaymentCalculable.FdpPgs ' Fecha de pago
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
        Public Property NumberMonths As Integer            ' Nº de meses agrupados en la fila resumen

        ' ======================
        ' Propiedades calculadas
        ' ======================
        ' Devuelve el identificador de pago de forma genérica
        ' para selección de filas en DataGridView
        Public ReadOnly Property IdPayment As Integer Implements ISelectableRow.IdPayment
            Get
                Return IdPgs
            End Get
        End Property

        ' Devuelve el nombre del grupo
        Public ReadOnly Property DisplayName As String Implements IPaymentCalculable.DisplayName
            Get
                Return $"GRUPO: {Me.GroupName}"
            End Get
        End Property

        ''
        ''
    End Class
End Namespace