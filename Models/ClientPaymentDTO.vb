Namespace Models
    Public Class ClientPaymentDTO

        ' ======================
        ' DATOS DEL CLIENTE
        ' ======================
        Public Property Name As String
        Public Property LastName As String
        Public Property DateBirth As Date
        Public Property Age As Integer
        Public Property Phone As String
        Public Property Email As String
        Public Property Address As String
        Public Property PaymentMethod As String
        Public Property RegistrationDate As Date
        Public Property State As String

        ' ======================
        ' GRUPO FAMILIAR
        ' ======================
        Public Property IsGroup As Boolean
        Public Property IdGroup As Integer?
        Public Property GroupName As String
        Public Property GroupMembers As Integer

        ' ======================
        ' RESULTADO
        ' ======================
        Public Property IdNewClient As Integer

    End Class

End Namespace