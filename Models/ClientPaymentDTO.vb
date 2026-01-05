Namespace Models
    Public Class ClientPaymentDTO

        ' ======================
        ' DATOS DEL CLIENTE
        ' ======================
        Public Property Nombre As String
        Public Property Apellido As String
        Public Property FechaNacimiento As Date
        Public Property Edad As Integer
        Public Property Telefono As String
        Public Property Email As String
        Public Property Direccion As String
        Public Property MetodoPago As String
        Public Property FechaInscripcion As Date
        Public Property Estado As String

        ' ======================
        ' GRUPO FAMILIAR
        ' ======================
        Public Property IsGroup As Boolean
        Public Property IdGrupo As Integer?
        Public Property IntegrantesGrupo As Integer
        Public Property TipoActualizacionGrupo As String

        ' ======================
        ' USUARIO / SISTEMA
        ' ======================
        Public Property IdUsuario As Integer

        ' ======================
        ' RESULTADO
        ' ======================
        Public Property IdClienteCreado As Integer

    End Class

End Namespace