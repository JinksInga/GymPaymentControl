Imports GymPaymentControl.Data
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Models
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

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ' Declaramos el Manager de Pagos para poder usar el método maestro
        Private ReadOnly _paymentManager As New PaymentManager()

        ''' <summary>
        ''' Genera los pagos correspondientes al mes actual para:
        ''' - Clientes individuales activos con pago mensual
        ''' - Grupos familiares
        ''' 
        ''' Todo el proceso se ejecuta dentro de una transacción.
        ''' </summary>
        Public Function GenerateNewMonthPayments() As Integer
            Dim filasInsertadas As Integer = 0
            Dim firstDayOfMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            Using connection = GetConnection()
                connection.Open()
                Using transaction As MySqlTransaction = connection.BeginTransaction()
                    Try
                        ' =====================
                        ' 1. PAGOS INDIVIDUALES
                        ' =====================
                        Dim sqlIndividual As String = "SELECT id_cli, fdn_cli FROM clientes WHERE std_cli = 'ACTIVO' AND mpg_cli = 'MENSUAL' AND (id_grp IS NULL OR id_grp = 0)"
                        Dim dtIndividual As DataTable = GetDataTable(connection, transaction, sqlIndividual)

                        For Each row As DataRow In dtIndividual.Rows
                            Dim idCli As Integer = Convert.ToInt32(row("id_cli"))

                            If Not PaymentExists(connection, transaction, firstDayOfMonth, idCli:=idCli) Then
                                Dim ageClient As Integer = CalculateClientAge(Convert.ToDateTime(row("fdn_cli")))
                                Dim individualRate = GetIndividualRate(connection, transaction, ageClient)

                                If individualRate IsNot Nothing Then
                                    ' CREAMOS EL DTO AL VUELO PARA EL MAESTRO
                                    Dim paymentDto As New IndividualPaymentDTO With {
                                .IdCli = idCli,
                                .FdiPgs = firstDayOfMonth,
                                .FdpPgs = Nothing, ' Aún no está pagado, es una deuda generada
                                .MtdPgs = PaymentMethods.Monthly,
                                .PrcPgs = Convert.ToDecimal(individualRate("prcio_trfa")),
                                .DscPgs = Convert.ToDecimal(individualRate("dscto_trfa"))
                            }

                                    ' LLAMADA AL MÉTODO MAESTRO (Pasando la conexión y transacción masiva)
                                    ' Pasamos 0 o Nothing en idUser si el sistema automático no requiere un usuario físico,
                                    ' o puedes pasar el ID del sistema/usuario actual. Como forma de pago pasamos cadena vacía o "PENDIENTE".
                                    _paymentManager.SavePaymentTransaction(paymentDto, TransactionMode.NewPayment,
                                                                           UserSession.IdUser, Nothing, connection, transaction)
                                    filasInsertadas += 1
                                End If
                            End If
                        Next

                        ' =================
                        ' 2. PAGOS GRUPALES
                        ' =================
                        Dim sqlFamilyGroup As String = "SELECT id_grp, num_intgrntes_grp FROM grp_familiar"
                        Dim dtFamilyGroup As DataTable = GetDataTable(connection, transaction, sqlFamilyGroup)

                        For Each row As DataRow In dtFamilyGroup.Rows
                            Dim idGrp As Integer = Convert.ToInt32(row("id_grp"))
                            Dim numberMembers As Integer = Convert.ToInt32(row("num_intgrntes_grp"))

                            If Not PaymentExists(connection, transaction, firstDayOfMonth, idGrp:=idGrp) Then
                                Dim groupRate = GetGroupRate(connection, transaction, numberMembers)

                                If groupRate IsNot Nothing Then
                                    ' CREAMOS EL DTO AL VUELO PARA EL GRUPO
                                    Dim groupPaymentDto As New GroupPaymentDTO With {
                                .IdGrp = idGrp,
                                .FdiPgs = firstDayOfMonth,
                                .FdpPgs = Nothing, ' Es deuda automática
                                .MtdPgs = PaymentMethods.Grupal,
                                .PrcPgs = Convert.ToDecimal(groupRate("prcio_trfa")) * numberMembers,
                                .DscPgs = Convert.ToDecimal(groupRate("dscto_trfa"))
                            }

                                    ' LLAMADA AL MÉTODO MAESTRO
                                    _paymentManager.SavePaymentTransaction(groupPaymentDto, TransactionMode.NewPayment,
                                                                           UserSession.IdUser, Nothing, connection, transaction)
                                    filasInsertadas += 1
                                End If
                            End If
                        Next

                        transaction.Commit()
                        Return filasInsertadas

                    Catch ex As Exception
                        transaction.Rollback()
                        Throw
                    End Try
                End Using
            End Using
        End Function


        ''' <summary>
        ''' Verifica si ya existe un pago para un cliente o grupo en una fecha determinada.
        ''' (Normalmente el primer día del mes).
        ''' </summary>
        Public Function PaymentExists(connection As MySqlConnection, transaction As MySqlTransaction, dateTime As DateTime,
                                       Optional idCli As Integer? = Nothing,
                                       Optional idGrp As Integer? = Nothing) As Boolean

            Dim sqlQuery As String = "SELECT COUNT(*) FROM pagos WHERE MONTH(fdi_pgs) = @month AND YEAR(fdi_pgs) = @year AND "
            If idCli.HasValue Then sqlQuery &= "id_cli = @id" Else sqlQuery &= "id_grp = @id"

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                command.Parameters.AddWithValue("@month", dateTime.Month)
                command.Parameters.AddWithValue("@year", dateTime.Year)
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

        '
        '
        '
        Public Function HasPendingMassivePayments() As Boolean

            Dim firstDayOfMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            Using connection = GetConnection()

                connection.Open()

                ' 1. Buscamos si hay al menos UN cliente individual que falte por registrar
                Dim sqlInd As String = "SELECT COUNT(*) FROM clientes c WHERE std_cli = 'ACTIVO' AND mpg_cli = 'MENSUAL'
                                        AND (id_grp Is NULL Or id_grp = 0) AND
                                        NOT EXISTS (SELECT 1 FROM pagos p WHERE p.id_cli = c.id_cli AND p.fdi_pgs = @fdi)"

                Using cmd As New MySqlCommand(sqlInd, connection)
                    cmd.Parameters.AddWithValue("@fdi", firstDayOfMonth)
                    If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then Return True
                End Using

                ' 2. Buscamos si hay al menos UN grupo familiar que falte por registrar
                Dim sqlGrp As String = "SELECT COUNT(*) FROM grp_familiar grp WHERE
                                        NOT EXISTS (SELECT 1 FROM pagos p WHERE p.id_grp = grp.id_grp AND p.fdi_pgs = @fdi)"

                Using cmd As New MySqlCommand(sqlGrp, connection)
                    cmd.Parameters.AddWithValue("@fdi", firstDayOfMonth)
                    If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then Return True
                End Using

            End Using

            Return False ' Si llegamos aquí, es que todo está ya creado

        End Function


    End Class
End Namespace