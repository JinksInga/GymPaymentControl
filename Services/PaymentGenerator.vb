Imports GymPaymentControl.Constants
Imports GymPaymentControl.Data
Imports GymPaymentControl.Enums
Imports GymPaymentControl.FrmCollectMembership
Imports GymPaymentControl.Models
Imports GymPaymentControl.Utils
Imports MySql.Data.MySqlClient

Namespace Services

    ''' <summary>
    ''' Servicio encargado de generar de forma masiva y automatizada los cargos
    ''' de pago mensuales para clientes individuales y grupos familiares.
    ''' </summary>
    ''' <remarks>
    ''' Este proceso se ejecuta al inicio del ciclo contable y garantiza:
    ''' <list type="bullet">
    ''' <item><description>Idempotencia (evita duplicar cobros dentro del mismo periodo).</description></item>
    ''' <item><description>Cálculo dinámico de tarifas y descuentos vigentes.</description></item>
    ''' <item><description>Atomicidad mediante transacciones de base de datos.</description></item>
    ''' </list>
    ''' </remarks>
    Public Class PaymentGenerator

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ' Declaramos el Manager de Pagos para poder usar el método maestro
        Private ReadOnly _paymentManager As New PaymentManager()


        ''' <summary>
        ''' Genera de forma masiva y en una sola transacción los cargos de pago
        ''' correspondientes al primer día del mes actual para:
        ''' - Clientes individuales activos con pago mensual.
        ''' - Grupos familiares activos.
        ''' </summary>
        ''' <returns>Número total de registros de deuda insertados en la base de datos.</returns>
        Public Function GenerateNewMonthPayments() As Integer

            Dim filasInsertadas As Integer = 0
            Dim firstDayOfMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            Using connection = GetConnection()

                connection.Open()

                Using transaction As MySqlTransaction = connection.BeginTransaction()

                    Try
                        ' ======================
                        ' | PAGOS INDIVIDUALES |
                        ' ======================

                        Dim sqlIndividual As String = "SELECT id_cli, fdn_cli " &
                                                      "FROM clientes " &
                                                      "WHERE std_cli = @status " &
                                                      "AND mpg_cli = @paymentMethod " &
                                                      "AND (id_grp IS NULL OR id_grp = 0)"

                        Dim dtIndividual As DataTable = GetDataTable(connection, transaction, sqlIndividual,
                                                                     New MySqlParameter("@status", CByte(EntityStatus.Active)),
                                                                     New MySqlParameter("@paymentMethod", PaymentMethods.Monthly))

                        For Each row As DataRow In dtIndividual.Rows

                            Dim idCli As Integer = Convert.ToInt32(row("id_cli"))

                            If Not PaymentExists(connection, transaction, firstDayOfMonth, idCli:=idCli) Then

                                Dim ageClient As Integer = CalculateClientAge(Convert.ToDateTime(row("fdn_cli")))
                                Dim individualRate = GetIndividualRate(connection, transaction, ageClient)

                                If individualRate IsNot Nothing Then

                                    Dim paymentDto As New IndividualPaymentDTO With
                                        {
                                            .IdCli = idCli,
                                            .FdiPgs = firstDayOfMonth,
                                            .FdpPgs = Nothing, 'Deuda generada, pendiente de cobrar
                                            .MtdPgs = PaymentMethods.Monthly,
                                            .PrcPgs = Convert.ToDecimal(individualRate("prcio_trfa")),
                                            .DscPgs = Convert.ToDecimal(individualRate("dscto_trfa"))
                                        }

                                    '| * LLAMADA AL MÉTODO MAESTRO (Pasamos la conexión y transacción masiva)
                                    _paymentManager.SavePaymentTransaction(paymentDto, TransactionMode.NewPayment,
                                                                           UserSession.IdUser, Nothing, connection, transaction)
                                    filasInsertadas += 1
                                End If
                            End If

                        Next

                        ' ==================
                        ' | PAGOS GRUPALES |
                        ' ==================

                        Dim sqlFamilyGroup As String = "SELECT id_grp, num_intgrntes_grp " &
                                                       "FROM grp_familiar " &
                                                       "WHERE std_grp = @status"

                        Dim dtFamilyGroup As DataTable = GetDataTable(connection, transaction, sqlFamilyGroup,
                                                                      New MySqlParameter("@status", CByte(EntityStatus.Active)))

                        For Each row As DataRow In dtFamilyGroup.Rows

                            Dim idGrp As Integer = Convert.ToInt32(row("id_grp"))
                            Dim numberMembers As Integer = Convert.ToInt32(row("num_intgrntes_grp"))

                            If Not PaymentExists(connection, transaction, firstDayOfMonth, idGrp:=idGrp) Then

                                Dim groupRate = GetGroupRate(connection, transaction, numberMembers)

                                If groupRate IsNot Nothing Then

                                    Dim groupPaymentDto As New GroupPaymentDTO With
                                        {
                                            .IdGrp = idGrp,
                                            .FdiPgs = firstDayOfMonth,
                                            .FdpPgs = Nothing, ' Es deuda automática
                                            .MtdPgs = PaymentMethods.Grupal,
                                            .PrcPgs = Convert.ToDecimal(groupRate("prcio_trfa")) * numberMembers,
                                            .DscPgs = Convert.ToDecimal(groupRate("dscto_trfa"))
                                        }

                                    '| * LLAMADA AL MÉTODO MAESTRO (Pasamos la conexión y transacción masiva)
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
        ''' Verifica si ya existe un registro de pago generado para un cliente o grupo en una fecha determinada.
        ''' </summary>
        ''' <param name="connection">Conexión activa a la base de datos.</param>
        ''' <param name="transaction">Transacción activa en curso.</param>
        ''' <param name="dateTime">Fecha a evaluar (generalmente el primer día del mes o la fecha actual).</param>
        ''' <param name="idCli">Identificador único del cliente individual (Opcional).</param>
        ''' <param name="idGrp">Identificador único del grupo familiar (Opcional).</param>
        ''' <param name="isDaily">Indica si la validación es para un pago del día exacto (<c>True</c>) o mensual (<c>False</c>).</param>
        ''' <returns><c>True</c> si ya existe al menos un pago registrado; de lo contrario, <c>False</c>.</returns>
        Public Function PaymentExists(connection As MySqlConnection, transaction As MySqlTransaction, dateTime As DateTime,
                                      Optional idCli As Integer? = Nothing, Optional idGrp As Integer? = Nothing,
                                      Optional isDaily As Boolean = False) As Boolean

            Dim sqlQuery As String

            ' 1. Decisión inteligente de la Query según el tipo de pago
            If isDaily Then 'CASO DIARIO

                sqlQuery = "SELECT COUNT(*) FROM pagos " &
                           "WHERE fdi_pgs = @fullDate AND "

            Else 'CASO MENSUAL / GRUPAL

                sqlQuery = "SELECT COUNT(*) FROM pagos " &
                           "WHERE MONTH(fdi_pgs) = @month " &
                           "AND YEAR(fdi_pgs) = @year AND "
            End If

            ' 2. Añadimos el filtro por Cliente o por Grupo
            sqlQuery &= If(idCli.HasValue, "id_cli = @id", "id_grp = @id")

            ' 3. Ejecución de la consulta y mapeo de parámetros
            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                If isDaily Then 'Parámetro para el día exacto.
                    command.Parameters.Add("@fullDate", MySqlDbType.Date).Value = dateTime.Date
                Else 'Parámetros tradicionales de mes y año
                    command.Parameters.Add("@month", MySqlDbType.Int32).Value = dateTime.Month
                    command.Parameters.Add("@year", MySqlDbType.Int32).Value = dateTime.Year
                End If

                command.Parameters.AddWithValue("@id", If(idCli, idGrp))

                Return Convert.ToInt32(command.ExecuteScalar()) > 0

            End Using

        End Function


        ''' <summary>
        ''' Obtiene la tarifa y descuento individual correspondiente según el rango de edad del cliente.
        ''' Si no se encuentra una coincidencia por edad, recurre a la tarifa mensual base.
        ''' </summary>
        ''' <param name="connection">Conexión activa a la base de datos.</param>
        ''' <param name="transaction">Transacción activa en curso.</param>
        ''' <param name="ageClient">Edad calculada del cliente.</param>
        ''' <returns>
        ''' Un <see cref="DataRow"/> con el precio y descuento, o <c>Nothing</c> si no existe tarifa aplicable.
        ''' </returns>
        Private Function GetIndividualRate(connection As MySqlConnection, transaction As MySqlTransaction,
                                           ageClient As Integer) As DataRow

            ' Intentamos primero por Rango de Edad
            Dim sqlQuery As String = "SELECT prcio_trfa, dscto_trfa " &
                                     "FROM trfa_dscto " &
                                     "WHERE @edad " &
                                     "BETWEEN emin_trfa AND emax_trfa " &
                                     "AND nperson_trfa = 1 LIMIT 1"

            Dim dataTable As DataTable = GetDataTable(connection, transaction, sqlQuery,
                                                      New MySqlParameter("@edad", ageClient))

            ' Si no encuentra por edad, buscamos la tarifa general "MENSUAL"
            If dataTable.Rows.Count = 0 Then

                Dim sqlFallback As String = "SELECT prcio_trfa, dscto_trfa " &
                                            "FROM trfa_dscto " &
                                            "WHERE tipo_trfa = @paymentMethod LIMIT 1"

                dataTable = GetDataTable(connection, transaction, sqlFallback,
                                         New MySqlParameter("@paymentMethod", PaymentMethods.Monthly))
            End If

            Return dataTable.Rows.Cast(Of DataRow).FirstOrDefault()

        End Function


        ''' <summary>
        ''' Obtiene la tarifa y descuento aplicable para un grupo familiar 
        ''' en función del número de integrantes.
        ''' </summary>
        ''' <param name="connection">Conexión activa a la base de datos.</param>
        ''' <param name="transaction">Transacción activa en curso.</param>
        ''' <param name="numberPeople">Número de personas/integrantes en el grupo.</param>
        ''' <returns>
        ''' Un <see cref="DataRow"/> con el precio y descuento base, 
        ''' o <c>Nothing</c> si no existe una tarifa configurada para esa cantidad de miembros.
        ''' </returns>
        Friend Function GetGroupRate(connection As MySqlConnection, transaction As MySqlTransaction,
                                     numberPeople As Integer) As DataRow

            Dim sqlQuery As String = "SELECT prcio_trfa, dscto_trfa " &
                                     "FROM trfa_dscto " &
                                     "WHERE nperson_trfa = @num"

            Dim dataTable As DataTable = GetDataTable(connection, transaction, sqlQuery,
                                                      New MySqlParameter("@num", numberPeople))

            Return dataTable.Rows.Cast(Of DataRow)().FirstOrDefault()

        End Function


        ''' <summary>
        ''' Ejecuta una consulta SQL dentro de una conexión y transacción activas
        ''' y retorna el resultado mapeado en un <see cref="DataTable"/>.
        ''' </summary>
        ''' <param name="connection">Conexión activa a la base de datos.</param>
        ''' <param name="transaction">Transacción activa en curso.</param>
        ''' <param name="sqlQuery">Consulta SQL parametrizada.</param>
        ''' <param name="parameters">Lista opcional de parámetros MySQL.</param>
        ''' <returns>Un objeto DataTable cargado con los registros devueltos por la consulta.</returns>
        Private Function GetDataTable(connection As MySqlConnection, transaction As MySqlTransaction,
                                      sqlQuery As String,
                                      ParamArray parameters() As MySqlParameter) As DataTable

            Dim dataTable As New DataTable()

            Using command As New MySqlCommand(sqlQuery, connection, transaction)

                ' Si se pasaron parámetros a la función, los adjuntamos al comando
                If parameters IsNot Nothing AndAlso parameters.Length > 0 Then
                    command.Parameters.AddRange(parameters)
                End If

                Using adapter As New MySqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using

            End Using

            Return dataTable

        End Function


        ''' <summary>
        ''' Comprueba si existen clientes o grupos familiares activos
        ''' que aún no tengan generado su pago del mes en curso.
        ''' </summary>
        ''' <returns><c>True</c> si hay al menos un registro pendiente por procesar;
        ''' de lo contrario, <c>False</c>.</returns>
        Public Function HasPendingMassivePayments() As Boolean

            Dim firstDayOfMonth As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            Using connection = GetConnection()

                connection.Open()

                ' 1. Buscamos si hay al menos UN cliente individual que falte por registrar
                Dim sqlInd As String = "SELECT COUNT(*) FROM clientes c " &
                                       "WHERE std_cli = @status " &
                                       "AND mpg_cli = @paymentMethod " &
                                       "AND (id_grp IS NULL OR id_grp = 0) " &
                                       "AND NOT EXISTS (SELECT 1 FROM pagos p " &
                                                       "WHERE p.id_cli = c.id_cli " &
                                                       "AND p.fdi_pgs = @fdi)"

                Using cmd As New MySqlCommand(sqlInd, connection)

                    cmd.Parameters.Add("@status", MySqlDbType.Byte).Value = CByte(EntityStatus.Active)
                    cmd.Parameters.Add("@paymentMethod", MySqlDbType.VarChar).Value = PaymentMethods.Monthly
                    cmd.Parameters.Add("@fdi", MySqlDbType.Date).Value = firstDayOfMonth

                    If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then Return True

                End Using

                ' 2. Buscamos si hay al menos UN grupo familiar que falte por registrar
                Dim sqlGrp As String = "SELECT COUNT(*) FROM grp_familiar grp " &
                                       "WHERE std_grp = @status " &
                                       "AND NOT EXISTS (SELECT 1 FROM pagos p " &
                                                       "WHERE p.id_grp = grp.id_grp " &
                                                       "AND p.fdi_pgs = @fdi)"

                Using cmd As New MySqlCommand(sqlGrp, connection)

                    cmd.Parameters.Add("@status", MySqlDbType.Byte).Value = CByte(EntityStatus.Active)
                    cmd.Parameters.Add("@fdi", MySqlDbType.Date).Value = firstDayOfMonth

                    If Convert.ToInt32(cmd.ExecuteScalar()) > 0 Then Return True

                End Using

            End Using

            Return False ' Si llegamos aquí, es que todo está ya creado

        End Function


    End Class

End Namespace