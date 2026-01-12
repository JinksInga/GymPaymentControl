Namespace Models
    Public Class PaymentDTO
        Public Property IdPgs As Integer
        Public Property IdCli As Integer '?
        Public Property Nombre As String
        Public Property Apellido As String
        Public Property MtdPgs As String
        Public Property FdiPgs As Date 'DateTime'
        Public Property MesAnio As String ' Ejemplo: "Enero 2026"
        Public Property PrcPgs As Decimal
        Public Property DscPgs As Decimal
        Public Property Total As Decimal
        Public Property DiasMes As Integer
        Public Property APagar As Decimal
        Public Property EsFilaResumen As Boolean '= False
        Public Property SumaTotalDeuda As Decimal
        Public Property CantidadMeses As Integer


        Public Property IdGrp As Integer? ' Usamos ? porque puede ser NULL si es individual
        Public Property NombreGrupo As String

        ' Para mostrar la lista de morosos


        Public Property Edad As String ' Ejemplo: "25 años"

        ' Para mostrar los clientes pertenecientes a un grupo
        Public Property Integrantes As String

        Public Property MontoResumen As Decimal ' Para el total "82.74€"



    End Class

End Namespace