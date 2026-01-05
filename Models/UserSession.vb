Namespace Models
    Public NotInheritable Class UserSession
        Private Sub New()
            ' Al ser privada, nadie puede hacer "Dim s As New UserSession"
            ' Obligamos a usar solo las propiedades compartidas.
        End Sub

        Public Shared Property IdUser As Integer
        Public Shared Property UserName As String
        Public Shared Property Role As String

        Public Shared Sub Logout()
            IdUser = 0
            UserName = String.Empty
            Role = String.Empty
        End Sub

    End Class

End Namespace