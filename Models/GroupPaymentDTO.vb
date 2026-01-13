Public Class GroupPaymentDTO
    Public Property IdPgs As Integer
    Public Property IdGrp As Integer
    Public Property NomGrp As String
    Public Property Integrantes As String
    'Public Property FdiPgs As DateTime
    Public Property FechaLarga As String ' Para el formato "4 ENERO 2026"
    Public Property PrcPgs As Decimal
    Public Property DscPgs As Decimal
    ' --- NUEVAS PROPIEDADES ---
    Public Property TotalSinProrrateo As Decimal
    Public Property DiasDelMes As Integer
    Public Property APagarProrrateo As Decimal ' El cálculo de los 108.38€
    Public Property EsFilaResumen As Boolean = False
    Public Property TextoColTotal As String = ""
    Public Property TextoColDias As String = ""
End Class