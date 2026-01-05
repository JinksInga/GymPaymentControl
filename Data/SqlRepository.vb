Imports System.Configuration
Imports GymPaymentControl.SqlRepository
Imports MySql.Data.MySqlClient

Public Module SqlRepository

    Private ReadOnly ConnectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

    '|----------------------------------------------------
    '| DEVUELVE TRUE SI LA CONSULTA TIENE REGISTROS
    '|----------------------------------------------------
    Public Function Exists(sqlQuery As String) As Boolean

        Using conString As New MySqlConnection(ConnectionString)

            Using sqlCommand As New MySqlCommand(sqlQuery, conString)

                conString.Open()

                Using exeReader = sqlCommand.ExecuteReader()
                    Return exeReader.HasRows
                End Using

            End Using

        End Using

    End Function

    '|----------------------------------------------------
    '| DEVUELVE UN VALOR ESCALAR (ID, TOTAL, ETC)
    '|----------------------------------------------------
    Public Function ExecuteScalar(Of T)(sqlQuery As String, Optional existParameterList As List(Of MySqlParameter) = Nothing) As T

        Using conString As New MySqlConnection(ConnectionString)

            Using sqlCommand As New MySqlCommand(sqlQuery, conString)

                ' Si se pasan parámetros, agregarlos al comando
                If existParameterList IsNot Nothing Then
                    sqlCommand.Parameters.AddRange(existParameterList.ToArray())
                End If

                conString.Open()

                Dim result As Object = sqlCommand.ExecuteScalar()

                ' Si la consulta no devuelve nada
                If result Is Nothing OrElse result Is DBNull.Value Then
                    Return Nothing
                End If

                Return CType(result, T)
            End Using

        End Using

    End Function

    ' Añade estas versiones que aceptan Transaction al SqlRepository
    Public Function ExecuteScalar(sqlQuery As String, parameterList As List(Of MySqlParameter),
                                  sqlConnection As MySqlConnection, sqlTransaction As MySqlTransaction) As Object

        Using sqlCommand As New MySqlCommand(sqlQuery, sqlConnection)

            sqlCommand.Transaction = sqlTransaction

            If parameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(parameterList.ToArray())

            Return sqlCommand.ExecuteScalar()

        End Using

    End Function

    '|----------------------------------------------------
    '| DEVUELVE UN DataTable (PARA GRIDS)
    '|----------------------------------------------------
    Public Function GetDataTable(sqlQuery As String) As DataTable

        Using conString As New MySqlConnection(ConnectionString)

            Using sqlCommand As New MySqlCommand(sqlQuery, conString)

                Using dtAdapter As New MySqlDataAdapter(sqlCommand)

                    Dim dtTable As New DataTable()

                    dtAdapter.Fill(dtTable)
                    Return dtTable

                End Using

            End Using

        End Using

    End Function

    '|-------------------------------------------------------------------------
    '| Ejecuta una instrucción SQL que NO devuelve filas, NO se usa para SELECT
    '|-------------------------------------------------------------------------
    Public Sub ExecuteNonQuery(sqlQuery As String,
                               parameterList As List(Of MySqlParameter),
                               sqlConnection As MySqlConnection,
                               sqlTransaction As MySqlTransaction)

        Using sqlCommand As New MySqlCommand(sqlQuery, sqlConnection, sqlTransaction)

            If parameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(parameterList.ToArray()) 'End If

            sqlCommand.ExecuteNonQuery()

        End Using

    End Sub

    Public Sub ExecuteNonQuery(sqlQuery As String,
                               Optional optParameterList As List(Of MySqlParameter) = Nothing)

        Using connectionString As New MySqlConnection(SqlRepository.ConnectionString)

            Using sqlCommand As New MySqlCommand(sqlQuery, connectionString)

                If optParameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(optParameterList.ToArray()) 'End If

                connectionString.Open()
                sqlCommand.ExecuteNonQuery()

            End Using

        End Using

    End Sub
    ' Añade estas versiones que aceptan Transaction al SqlRepository
    'Public Function ExecuteNonQuery(sqlQuery As String,
    '                                parameterList As List(Of MySqlParameter),
    '                                sqlConnection As MySqlConnection,
    '                                sqlTransaction As MySqlTransaction) As Integer

    '    Using sqlCommand As New MySqlCommand(sqlQuery, sqlConnection)

    '        sqlCommand.Transaction = sqlTransaction

    '        If parameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(parameterList.ToArray())

    '        Return sqlCommand.ExecuteNonQuery()

    '    End Using

    'End Function

    '|--------------------------------------------------------------------------------
    '| INSERTAR UN REGISTRO A LA TABLA CLIENTES Y OBTENER EL ID DEL CLIENTE REGISTRADO
    '|--------------------------------------------------------------------------------
    '
    ' Versión optimizada de tu función de repositorio
    'Public Function ExecuteInsertAndGetId(sqlQuery As String, parameterList As List(Of MySqlParameter)) As Integer

    '    Using conString As New MySqlConnection(ConnectionString)

    '        ' Añadimos el SELECT al final del string SQL original
    '        Using sqlCommand As New MySqlCommand(sqlQuery & "; SELECT LAST_INSERT_ID();", conString)

    '            sqlCommand.Parameters.AddRange(parameterList.ToArray())
    '            conString.Open()

    '            ' ExecuteScalar ejecutará el INSERT y devolverá el resultado del SELECT LAST_INSERT_ID()
    '            Return Convert.ToInt32(sqlCommand.ExecuteScalar())

    '        End Using

    '    End Using

    'End Function
    Public Function ExecuteInsertAndGetId(sqlQuery As String,
                                          parameterList As List(Of MySqlParameter),
                                          sqlConnection As MySqlConnection,
                                          sqlTransaction As MySqlTransaction) As Integer

        Using sqlCommand As New MySqlCommand(sqlQuery & "; SELECT LAST_INSERT_ID();", sqlConnection, sqlTransaction)

            If parameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(parameterList.ToArray()) 'End If

            Return Convert.ToInt32(sqlCommand.ExecuteScalar())

        End Using

    End Function

    '|-------------------------------------------------------------------------
    '| OBTENER LA TARIFA CORRESPONDIENTE DEL NUEVO CLIENTE O DEL GRUPO FAMILIAR
    '|-------------------------------------------------------------------------
    'Public Function GetRate(sqlQuery As String, parameterList As List(Of MySqlParameter)) As RateResult

    '    Dim resultado As New RateResult With {.Exists = False, .Price = 0, .Discount = 0}

    '    Using conString As New MySqlConnection(ConnectionString)

    '        Using sqlCommand As New MySqlCommand(sqlQuery, conString)

    '            If parameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(parameterList.ToArray())

    '            conString.Open()

    '            Using exeReader = sqlCommand.ExecuteReader()

    '                If exeReader.Read() Then
    '                    resultado.Exists = True
    '                    resultado.Price = If(exeReader.IsDBNull(0), 0, exeReader.GetDecimal(0))
    '                    resultado.Discount = If(exeReader.IsDBNull(1), 0, exeReader.GetDecimal(1))
    '                End If

    '            End Using

    '        End Using

    '    End Using

    '    Return resultado

    'End Function
    Public Function GetRate(sqlQuery As String,
                            parameterList As List(Of MySqlParameter),
                            sqlConnection As MySqlConnection,
                            sqlTransaction As MySqlTransaction) As RateResult

        Dim result As New RateResult With {.Exists = False}

        Using sqlCommand As New MySqlCommand(sqlQuery, sqlConnection, sqlTransaction)

            If parameterList IsNot Nothing Then sqlCommand.Parameters.AddRange(parameterList.ToArray()) 'End If

            Using dr = sqlCommand.ExecuteReader()

                If dr.Read() Then
                    result.Exists = True
                    result.Exists = If(dr.IsDBNull(0), 0D, dr.GetDecimal(0))
                    result.Discount = If(dr.IsDBNull(1), 0D, dr.GetDecimal(1))
                End If

            End Using

        End Using

        Return result

    End Function

    '|-------------------------------------
    '| ESTRUCTURA PARA LA FUNCION GetRate
    '|-------------------------------------
    Public Structure RateResult
        Public Exists As Boolean
        Public Price As Decimal
        Public Discount As Decimal
    End Structure

    '|----------
    '| en principio sirve para mostrar los pagos diarios clases suelas, tambien nos muestra los datos de la tabla grupo
    '|----------
    '|
    Public Function ExecuteDataTable(sqlQuery As String, parameterList As List(Of MySqlParameter)) As DataTable

        Dim dtTable As New DataTable()

        Using conString As New MySqlConnection(ConnectionString)

            Using sqlCommand As New MySqlCommand(sqlQuery, conString)

                If parameterList IsNot Nothing AndAlso parameterList.Count > 0 Then
                    sqlCommand.Parameters.AddRange(parameterList.ToArray())
                End If

                Using dtAdapter As New MySqlDataAdapter(sqlCommand)
                    dtAdapter.Fill(dtTable)
                End Using

            End Using

        End Using

        Return dtTable

    End Function

End Module