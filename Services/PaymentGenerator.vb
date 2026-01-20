Imports System.Configuration
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services

    ''' <summary>
    ''' Servicio encargado de generar los pagos mensuales de forma automática
    ''' tanto para clientes individuales como para grupos familiares.
    ''' 
    ''' Este proceso se ejecuta normalmente una vez al mes y:
    ''' - Evita duplicar pagos del mismo mes
    ''' - Calcula tarifas y descuentos según reglas del negocio
    ''' - Ejecuta todo dentro de una transacción
    ''' </summary>
    Public Class PaymentGenerator

        ' Cadena de conexión a la base de datos
        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

        ''' <summary>
        ''' Genera los pagos correspondientes al mes actual para:
        ''' - Clientes individuales activos con pago mensual
        ''' - Grupos familiares
        ''' 
        ''' Todo el proceso se ejecuta dentro de una transacción.
        ''' </summary>
        ''' <param name="idUser">Usuario que ejecuta el proceso (auditoría)</param>
        Public Sub GenerateNewMonthPayments(idUser As Integer)

            ' Primer día del mes actual (clave para evitar duplicados)
            Dim firstDayOfMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            Using connection As New MySqlConnection(_connectionString)

                connection.Open()

                ' Iniciamos transacción para asegurar consistencia
                Using transaction As MySqlTransaction = connection.BeginTransaction()
                    Try
                        ' ==========================================================
                        ' PAGOS INDIVIDUALES
                        ' ==========================================================
                        ' Clientes Activos, Mensuales y que no están en un grupo
                        Dim sqlIndividual As String = "SELECT id_cli, fdn_cli FROM clientes
                                                    WHERE std_cli = 'ACTIVO'
                                                    AND mpg_cli = 'MENSUAL'
                                                    AND (id_grp IS NULL OR id_grp = 0)"

                        Dim dtIndividual As DataTable = GetDataTable(connection, transaction, sqlIndividual)

                        For Each row As DataRow In dtIndividual.Rows

                            Dim idCli As Integer = Convert.ToInt32(row("id_cli"))

                            ' Evitamos crear pagos duplicados para el mismo mes
                            If Not PaymentExists(connection, transaction, firstDayOfMonth, idCli:=idCli) Then

                                ' Calculamos la edad del cliente
                                Dim ageClient As Integer = CalculateClientAge(Convert.ToDateTime(row("fdn_cli")))

                                ' Obtenemos la tarifa correspondiente al cliente
                                Dim individualRate = GetIndividualRate(connection, transaction, ageClient)

                                If individualRate IsNot Nothing Then
                                    ' Pasamos los valores tal cual están en la tabla.
                                    InsertPaymentRecord(connection, transaction, firstDayOfMonth,
                                                         Convert.ToDecimal(individualRate("prcio_trfa")),
                                                         Convert.ToDecimal(individualRate("dscto_trfa")),
                                                         idUser, idCli:=idCli, mtdPgs:="MENSUAL")
                                End If
                            End If
                        Next

                        ' ==========================================================
                        ' PAGOS GRUPALES
                        ' ==========================================================

                        ' Obtenemos los grupos familiares existentes
                        Dim sqlFamilyGroup As String = "SELECT id_grp, num_intgrntes_grp FROM grp_familiar"
                        Dim dtFamilyGroup As DataTable = GetDataTable(connection, transaction, sqlFamilyGroup)

                        For Each row As DataRow In dtFamilyGroup.Rows
                            Dim idGrp As Integer = Convert.ToInt32(row("id_grp"))
                            Dim numberMembers As Integer = Convert.ToInt32(row("num_intgrntes_grp"))

                            ' Comprobar si ya existe el registro de este mes. Evitamos pagos duplicados del mismo mes.
                            If Not PaymentExists(connection, transaction, firstDayOfMonth, idGrp:=idGrp) Then

                                ' Obtenemos la tarifa correspondiente al tamaño del grupo
                                Dim groupRate = GetGroupRate(connection, transaction, numberMembers)

                                If groupRate IsNot Nothing Then

                                    ' Pasamos los valores tal cual están en la tabla.
                                    InsertPaymentRecord(connection, transaction, firstDayOfMonth,
                                                        Convert.ToDecimal(groupRate("prcio_trfa")) * numberMembers,
                                                        Convert.ToDecimal(groupRate("dscto_trfa")),
                                                        idUser, idGrp:=idGrp, mtdPgs:="GRUPAL")
                                End If
                            End If
                        Next

                        ' Si todo salió bien, confirmamos la transacción
                        transaction.Commit()

                    Catch ex As Exception
                        ' Ante cualquier error, deshacemos todos los cambios
                        transaction.Rollback()
                        Throw
                    End Try

                End Using
            End Using

        End Sub

        ' ==========================================================
        ' FUNCIONES DE APOYO
        ' ==========================================================

        ''' <summary>
        ''' Verifica si ya existe un pago para un cliente o grupo
        ''' en una fecha determinada (normalmente el primer día del mes).
        ''' </summary>
        Private Function PaymentExists(connection As MySqlConnection, transaction As MySqlTransaction, dateTime As DateTime,
                                       Optional idCli As Integer? = Nothing,
                                       Optional idGrp As Integer? = Nothing) As Boolean

            Dim sqlQuery As String = "SELECT COUNT(*) FROM pagos WHERE fdi_pgs = @fdi AND "
            If idCli.HasValue Then sqlQuery &= "id_cli = @id" Else sqlQuery &= "id_grp = @id"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                command.Parameters.AddWithValue("@fdi", dateTime.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@id", If(idCli, idGrp))
                Return Convert.ToInt32(command.ExecuteScalar()) > 0

            End Using

        End Function

        ''' <summary>
        ''' Obtiene la tarifa individual según la edad del cliente.
        ''' Si no existe tarifa por rango de edad, se usa la tarifa mensual general.
        ''' </summary>
        Private Function GetIndividualRate(connection As MySqlConnection, transaction As MySqlTransaction,
                                           ageClient As Integer) As DataRow

            ' Intentamos primero por Rango de Edad
            Dim sqlQuery As String = "SELECT prcio_trfa, dscto_trfa
                                      FROM trfa_dscto
                                      WHERE @edad
                                      BETWEEN emin_trfa AND emax_trfa
                                      AND nperson_trfa = 1 LIMIT 1"

            Dim dataTable As DataTable = GetDataTableWithParameters(connection, transaction, New MySqlParameter("@edad", ageClient), sqlQuery)

            ' Si no encuentra por edad, buscamos la tarifa general "MENSUAL"
            If dataTable.Rows.Count = 0 Then
                Dim sqlFallback As String = "SELECT prcio_trfa, dscto_trfa
                                             FROM trfa_dscto
                                             WHERE tipo_trfa = 'MENSUAL' LIMIT 1"

                dataTable = GetDataTable(connection, transaction, sqlFallback)
            End If

            Return dataTable.Rows.Cast(Of DataRow).FirstOrDefault()

        End Function

        ''' <summary>
        ''' Obtiene la tarifa grupal según el número de integrantes.
        ''' </summary>
        Private Function GetGroupRate(connection As MySqlConnection, transaction As MySqlTransaction,
                                      numberPeople As Integer) As DataRow

            Dim sqlQuery As String = "SELECT prcio_trfa, dscto_trfa
                                      FROM trfa_dscto
                                      WHERE nperson_trfa = @num"

            Return GetDataTableWithParameters(connection, transaction, New MySqlParameter("@num", numberPeople),
                                                 sqlQuery).Rows.Cast(Of DataRow).FirstOrDefault()

        End Function

        ''' <summary>
        ''' Inserta un nuevo registro de pago en la base de datos.
        ''' Soporta pagos individuales o grupales.
        ''' </summary>
        Private Sub InsertPaymentRecord(connection As MySqlConnection, transaction As MySqlTransaction,
                                        dateTime As DateTime,
                                        price As Decimal, discount As Decimal, idUser As Integer,
                                        Optional idCli As Integer? = Nothing,
                                        Optional idGrp As Integer? = Nothing,
                                        Optional mtdPgs As String = "")

            Dim sqlQuery As String = "INSERT INTO pagos (fdi_pgs, frm_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user)
                                      VALUES (@fdi, '', @metodo, @prc, @dsc, @idCli, @idGrp, @idUser)"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                command.Parameters.AddWithValue("@fdi", dateTime.ToString("yyyy-MM-dd"))
                command.Parameters.AddWithValue("@metodo", mtdPgs)
                command.Parameters.AddWithValue("@prc", price)
                command.Parameters.AddWithValue("@dsc", discount)
                command.Parameters.AddWithValue("@idUser", idUser)

                ' Manejo correcto de nulos para las llaves foráneas
                command.Parameters.AddWithValue("@idCli", If(idCli.HasValue, idCli.Value, DBNull.Value))
                command.Parameters.AddWithValue("@idGrp", If(idGrp.HasValue, idGrp.Value, DBNull.Value))

                command.ExecuteNonQuery()
            End Using
        End Sub

        ''' <summary>
        ''' Ejecuta una consulta SQL y devuelve los resultados en un DataTable.
        ''' </summary>
        Private Function GetDataTable(connection As MySqlConnection, transaction As MySqlTransaction,
                                      sqlQuery As String) As DataTable

            Dim dataTable As New DataTable()

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                Using adapter As New MySqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using
            End Using

            Return dataTable

        End Function

        ''' <summary>
        ''' Ejecuta una consulta SQL con parámetros y devuelve un DataTable.
        ''' </summary>
        Private Function GetDataTableWithParameters(connection As MySqlConnection, transaction As MySqlTransaction,
                                                    parameter As MySqlParameter, sqlQuery As String) As DataTable

            Dim dataTable As New DataTable()

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                command.Parameters.Add(parameter)

                Using adapter As New MySqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using
            End Using

            Return dataTable

        End Function

    End Class
End Namespace