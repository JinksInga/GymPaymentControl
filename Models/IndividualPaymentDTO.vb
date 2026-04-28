Imports GymPaymentControl.Interfaces

Namespace Models

    ' DTO que representa un pago individual pendiente de un cliente.
    ' Se utiliza tanto para filas de detalle como para filas de resumen
    ' dentro de los DataGridView.
    Public Class IndividualPaymentDTO

        ' ======================
        ' Datos del cliente - Heredamos todas las propiedades del DTO
        ' ======================
        Inherits ClientPaymentDTO

        ' Implementa:
        ' - ISelectableRow → permite identificar si la fila es seleccionable en el Grid
        ' - IPaymentSummary → indica si la fila es una fila de resumen
        ' - IPaymentCalculable → permite aplicar cálculos financieros genéricos
        Implements ISelectableRow
        Implements IPaymentSummary
        Implements IPaymentCalculable

        ' ======================
        ' Creamos una copia superficial del objeto actual
        ' ======================
        Public Function Clone() As IPaymentCalculable Implements IPaymentCalculable.Clone
            Return DirectCast(Me.MemberwiseClone(), IndividualPaymentDTO)
        End Function

        ' ======================
        ' Identificadores
        ' ======================
        Public Property IdPgs As Integer Implements IPaymentCalculable.IdPgs ' Id del pago
        Public Property IdCli As Integer ' Id del cliente
        'Public Property IdUser As Integer? ' Permite saber quién cobró la mensualidad (puede ser Null)
        Public Property NomUser As String   ' El nombre que viene al hacer LEFT JOIN

        ' ======================
        ' Datos del pago
        ' ======================
        Public Property MtdPgs As String Implements IPaymentCalculable.MtdPgs ' Método de pago (DIARIO o MENSUAL)
        Public Property FrmPgs As String ' Forma de pago (Efectivo, tarjeta, etc.)
        Public Property FdiPgs As Date Implements IPaymentCalculable.FdiPgs ' Fecha de inicio
        Public Property LongFdiPgs As String ' Reemplaza a LongDate (Fecha Inicio)
        Public Property FdpPgs As Date Implements IPaymentCalculable.FdpPgs ' Fecha de pago
        Public Property LongFdpPgs As String ' Reemplaza a LongDate (Fecha de Pago)

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
        Public Property NumberMonths As Integer ' Nº de meses agrupados en la fila resumen

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
                Return $"{Me.FirstName} {Me.LastName} - {Me.AgeText}"
            End Get
        End Property

        ' Devolvemos el nombre del cliente
        Public ReadOnly Property Members As String Implements IPaymentCalculable.Members
            Get
                ' Si el cliente pertenece a un grupo y hemos cargado los integrantes...
                If Not String.IsNullOrEmpty(Me.GroupMembers) Then
                    Return Me.GroupMembers
                Else
                    ' Si es un pago individual puro, mostramos solo al cliente
                    Return $"{Me.FirstName} {Me.LastName}"
                End If
            End Get
        End Property
        ''
        ''
    End Class
End Namespace