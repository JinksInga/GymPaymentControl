Namespace Models

    ''' <summary>
    ''' Representa los datos comerciales de una tarifa o descuento con sus propiedades calculadas.
    ''' </summary>
    Public Class TariffDTO

        Public Property Id As Short
        Public Property PaymentMethod As String
        Public Property Price As Decimal
        Public Property MinimumAge As Short
        Public Property MaximumAge As Short
        Public Property NumberMembers As Short
        Public Property Discount As Decimal

        ' PROPIEDADES CALCULADAS DE FORMA PURA (Sin tocar la UI)
        Public ReadOnly Property Total As Decimal
            Get
                Return Price * NumberMembers
            End Get
        End Property

        Public ReadOnly Property TotalToPay As Decimal
            Get
                Return Total - Discount
            End Get
        End Property

    End Class

End Namespace