Imports MySql.Data.MySqlClient
Imports System.Configuration

Namespace Data
    Public MustInherit Class BaseRepository

        ' Centralizamos la conexión que ya tenías en el módulo
        Protected ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

        ' Este es el método que usaremos en los Managers para los DTOs
        Protected Function GetConnection() As MySqlConnection

            Return New MySqlConnection(_connectionString)

        End Function

        ' Mantenemos GetDataTable por si algún reporte viejo lo necesita
        Protected Function ExecuteDataTable(sqlQuery As String,
                                            Optional parameterList As List(Of MySqlParameter) = Nothing) As DataTable
            Dim dtTable As New DataTable()

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    If parameterList IsNot Nothing Then command.Parameters.AddRange(parameterList.ToArray())

                    Using dtAdapter As New MySqlDataAdapter(command)
                        dtAdapter.Fill(dtTable)
                    End Using

                End Using

            End Using

            Return dtTable

        End Function
        'Public Function ExecuteDataTable(sqlQuery As String, parameterList As List(Of MySqlParameter)) As DataTable

        '    Dim dtTable As New DataTable()

        '    Using conString As New MySqlConnection(ConnectionString)

        '        Using sqlCommand As New MySqlCommand(sqlQuery, conString)

        '            If parameterList IsNot Nothing AndAlso parameterList.Count > 0 Then
        '                sqlCommand.Parameters.AddRange(parameterList.ToArray())
        '            End If

        '            Using dtAdapter As New MySqlDataAdapter(sqlCommand)
        '                dtAdapter.Fill(dtTable)
        '            End Using

        '        End Using

        '    End Using

        '    Return dtTable

        'End Function
    End Class
End Namespace