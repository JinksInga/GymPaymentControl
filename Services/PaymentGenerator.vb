Imports MySql.Data.MySqlClient
Imports System.Configuration

Namespace Services
    Public Class PaymentGenerator
        Private ReadOnly _connectionString As String = ConfigurationManager.ConnectionStrings("MyConnectionMySQL").ConnectionString

        ''' <summary>
        ''' Proceso principal que recorre clientes y grupos para generar sus mensualidades
        ''' </summary>
        Public Sub GenerarPagosDelMes(idUsuarioActivo As Integer)
            ' Punto 5: Forzamos el primer día del mes actual
            Dim fechaFdi As New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)

            Using conn As New MySqlConnection(_connectionString)
                conn.Open()

                Using tx As MySqlTransaction = conn.BeginTransaction()
                    Try
                        ' =========================
                        ' TODO TU CÓDIGO ACTUAL
                        ' =========================

                        ' --- BLOQUE 1: INDIVIDUALES (Punto 2) ---
                        ' Clientes Activos, Mensuales y que no están en un grupo
                        Dim sqlIndiv As String = "SELECT id_cli, fdn_cli FROM clientes WHERE
                                        std_cli = 'ACTIVO' AND mpg_cli = 'MENSUAL'
                                        AND (id_grp IS NULL OR id_grp = 0)"
                        Dim dtIndiv As DataTable = ObtenerDataTable(sqlIndiv, conn, tx)

                        For Each row As DataRow In dtIndiv.Rows
                            Dim idCli As Integer = Convert.ToInt32(row("id_cli"))

                            ' Punto 3: Comprobar si ya existe
                            If Not ExistePago(conn, tx, fechaFdi, idCli:=idCli) Then
                                ' Calculamos edad con tu función (debes tenerla accesible o pegarla aquí)
                                Dim edad As Integer = CalculateClientAge(Convert.ToDateTime(row("fdn_cli")))
                                Dim tarifa = ObtenerTarifaIndividual(conn, tx, edad)

                                If tarifa IsNot Nothing Then
                                    ' Pasamos los valores tal cual están en la tabla (Tarifa 1, 4 o 5)
                                    InsertarRegistroPago(conn, tx, fechaFdi,
                         Convert.ToDecimal(tarifa("prcio_trfa")),
                         Convert.ToDecimal(tarifa("dscto_trfa")),
                         idUsuarioActivo, idCli:=idCli, metodo:="MENSUAL")
                                End If
                            End If
                        Next

                        ' --- DENTRO DE GenerarPagosDelMes ---
                        ' BLOQUE 2: GRUPALES
                        Dim sqlGrupos As String = "SELECT id_grp, num_intgrntes_grp FROM grp_familiar"
                        Dim dtGrupos As DataTable = ObtenerDataTable(sqlGrupos, conn, tx)

                        For Each row As DataRow In dtGrupos.Rows
                            Dim idGrp As Integer = Convert.ToInt32(row("id_grp"))
                            Dim numInt As Integer = Convert.ToInt32(row("num_intgrntes_grp"))

                            ' Comprobar si ya existe el registro este mes (Punto 3)
                            If Not ExistePago(conn,tx, fechaFdi, idGrp:=idGrp) Then
                                ' Buscamos la tarifa que corresponda al número de integrantes (Punto 4)
                                Dim tarifa = ObtenerTarifaGrupal(conn, tx, numInt)

                                If tarifa IsNot Nothing Then
                                    ' Realizamos el cálculo: 45.00 * nº integrantes
                                    Dim pTotal As Decimal = Convert.ToDecimal(tarifa("prcio_trfa")) * numInt
                                    Dim dTotal As Decimal = Convert.ToDecimal(tarifa("dscto_trfa")) ' El descuento ya es total

                                    InsertarRegistroPago(conn, tx, fechaFdi, pTotal, dTotal, idUsuarioActivo, idGrp:=idGrp, metodo:="GRUPAL")
                                End If
                            End If
                        Next
                        ' =========================
                        ' TODO TU CÓDIGO ACTUAL
                        ' =========================

                        tx.Commit()
                    Catch ex As Exception
                        tx.Rollback()
                        Throw ' Re-lanzamos la excepción para que el Form la capture
                    End Try
                End Using

            End Using
        End Sub

        ' --- FUNCIONES DE APOYO PRIVADAS ---

        Private Function ExistePago(conn As MySqlConnection, tx As MySqlTransaction, fecha As DateTime, Optional idCli As Integer? = Nothing, Optional idGrp As Integer? = Nothing) As Boolean
            'Private Function ExistePago(conn As MySqlConnection, fecha As DateTime, Optional idCli As Integer? = Nothing, Optional idGrp As Integer? = Nothing) As Boolean

            Dim sql As String = "SELECT COUNT(*) FROM pagos WHERE fdi_pgs = @fdi AND "
            If idCli.HasValue Then sql &= "id_cli = @id" Else sql &= "id_grp = @id"

            Using cmd As New MySqlCommand(sql, conn, tx)
                'Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@fdi", fecha.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@id", If(idCli, idGrp))
                Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
            End Using

        End Function

        Private Function ObtenerTarifaIndividual(conn As MySqlConnection, tx As MySqlTransaction, edad As Integer) As DataRow
            'Private Function ObtenerTarifaIndividual(conn As MySqlConnection, edad As Integer) As DataRow
            ' Intentamos primero por Rango de Edad
            Dim sql As String = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE @edad BETWEEN emin_trfa AND emax_trfa AND nperson_trfa = 1 LIMIT 1"
            Dim dt As DataTable = ObtenerDataTableConParametros(sql, conn, tx, New MySqlParameter("@edad", edad))

            ' Si no encuentra por edad (como pasará con los de 25 años), buscamos la tarifa general "MENSUAL"
            If dt.Rows.Count = 0 Then
                Dim sqlFallback As String = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE tipo_trfa = 'MENSUAL' LIMIT 1"
                dt = ObtenerDataTable(sqlFallback, conn, tx)
            End If

            Return dt.Rows.Cast(Of DataRow).FirstOrDefault()
        End Function

        Private Function ObtenerTarifaGrupal(conn As MySqlConnection, tx As MySqlTransaction, numPersonas As Integer) As DataRow
            Dim sql As String = "SELECT prcio_trfa, dscto_trfa FROM trfa_dscto WHERE nperson_trfa = @num"
            Return ObtenerDataTableConParametros(sql, conn, tx, New MySqlParameter("@num", numPersonas)).Rows.Cast(Of DataRow).FirstOrDefault()
        End Function

        'End Sub

        Private Sub InsertarRegistroPago(conn As MySqlConnection, tx As MySqlTransaction, fecha As DateTime, precio As Decimal, descuento As Decimal, idUser As Integer, Optional idCli As Integer? = Nothing, Optional idGrp As Integer? = Nothing, Optional metodo As String = "")
            ' fdp_pgs se omite para que MySQL guarde el valor por defecto 0000-00-00
            Dim sql As String = "INSERT INTO pagos (fdi_pgs, frm_pgs, mtd_pgs, prc_pgs, dsc_pgs, id_cli, id_grp, id_user) " &
                        "VALUES (@fdi, '', @metodo, @prc, @dsc, @idCli, @idGrp, @idUser)"

            Using cmd As New MySqlCommand(sql, conn, tx)
                'Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@fdi", fecha.ToString("yyyy-MM-dd"))
                cmd.Parameters.AddWithValue("@metodo", metodo)
                cmd.Parameters.AddWithValue("@prc", precio)
                cmd.Parameters.AddWithValue("@dsc", descuento)
                cmd.Parameters.AddWithValue("@idUser", idUser)

                ' Manejo de nulos para las llaves foráneas
                cmd.Parameters.AddWithValue("@idCli", If(idCli.HasValue, idCli.Value, DBNull.Value))
                cmd.Parameters.AddWithValue("@idGrp", If(idGrp.HasValue, idGrp.Value, DBNull.Value))

                cmd.ExecuteNonQuery()
            End Using
        End Sub

        ' Herramientas de base de datos
        Private Function ObtenerDataTable(sql As String,
                                          conn As MySqlConnection,
                                          tx As MySqlTransaction) As DataTable
            'Private Function ObtenerDataTable(sql As String, conn As MySqlConnection) As DataTable
            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(sql, conn, tx)
                'Using cmd As New MySqlCommand(sql, conn)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            Return dt
        End Function

        Private Function ObtenerDataTableConParametros(sql As String,
                                                       conn As MySqlConnection,
                                                       tx As MySqlTransaction,
                                                       param As MySqlParameter) As DataTable
            'Private Function ObtenerDataTableConParametros(sql As String, conn As MySqlConnection, param As MySqlParameter) As DataTable
            Dim dt As New DataTable()
            Using cmd As New MySqlCommand(sql, conn, tx)
                'Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.Add(param)
                Using adapter As New MySqlDataAdapter(cmd)
                    adapter.Fill(dt)
                End Using
            End Using
            Return dt
        End Function

        ' Tu función de edad (Cópiala aquí dentro también)
        Public Function CalculateClientAge(ByVal dtDateOfBirth As Date) As Integer
            Dim dtToday As Date = Date.Today
            Dim intAge As Integer = dtToday.Year - dtDateOfBirth.Year
            If dtDateOfBirth.Date > dtToday.AddYears(-intAge).Date Then intAge -= 1
            Return intAge
        End Function
    End Class
End Namespace