Imports GymPaymentControl.Data
Imports MySql.Data.MySqlClient

Namespace Services

    Public Class FamilyGroupManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository


        ''' <summary>
        ''' Busca grupos que coincidan exactamente con el nombre proporcionado (para validar duplicados).
        ''' </summary>
        Public Function GetGroupsByNameMatch(groupName As String) As DataTable

            Dim sqlQuery As String = "SELECT id_grp, nom_grp, num_intgrntes_grp FROM grp_familiar
                                        WHERE nom_grp LIKE @GroupName ORDER BY nom_grp"

            ' Preparamos el parámetro de forma segura para evitar inyección SQL y el fallo de la comilla (')
            Dim parameters As New List(Of MySqlParameter) From
                {
                    New MySqlParameter("@GroupName", $"%{groupName}%")
                }

            ' Delegamos la ejecución completa a la infraestructura del búnker
            Return ExecuteDataTable(sqlQuery, parameters)

        End Function




    End Class
End Namespace
