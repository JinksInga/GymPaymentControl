Imports GymPaymentControl.Data
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services

    Public Class TariffManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository


        ''' <summary>
        ''' Consulta de forma rápida y eficiente en la base de datos si el gimnasio cuenta con tarifas registradas.
        ''' </summary>
        ''' <returns>True si existe al menos una tarifa en el sistema; de lo contrario, False.</returns>
        Public Function CheckIfTariffsExist() As Boolean

            Dim sqlQuery As String = "SELECT COUNT(*) FROM trfa_dscto"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    Try
                        connection.Open()
                        Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                        Return count > 0

                    Catch ex As Exception

                        Return False

                    End Try

                End Using
            End Using

        End Function


        ''' <summary>
        ''' Recupera todas las tarifas y descuentos de la base de datos ordenadas por tipo.
        ''' </summary>
        Public Function FetchAllTariffs() As List(Of TariffDTO)

            Dim tariffsList As New List(Of TariffDTO)()

            Dim sqlQuery As String = "SELECT * FROM trfa_dscto ORDER BY tipo_trfa"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using reader As MySqlDataReader = command.ExecuteReader()

                        If reader.HasRows Then

                            While reader.Read()

                                Dim tariff As New TariffDTO() With
                                    {
                                        .IdTariff = reader.GetInt16("id_trfa"),
                                        .PaymentMethod = reader.GetString("tipo_trfa"),
                                        .Price = reader.GetDecimal("prcio_trfa"),
                                        .MinimumAge = reader.GetInt16("emin_trfa"),
                                        .MaximumAge = reader.GetInt16("emax_trfa"),
                                        .NumberMembers = reader.GetInt16("nperson_trfa"),
                                        .Discount = reader.GetDecimal("dscto_trfa")
                                    }
                                tariffsList.Add(tariff)

                            End While
                        End If

                    End Using
                End Using
            End Using

            Return tariffsList

        End Function


        ''' <summary>
        ''' Guarda de forma unificada la tarifa en la base de datos, decidiendo automáticamente si debe 
        ''' registrar una nueva (Insert) o actualizar una existente (Update) basándose en el ID de la tarifa.
        ''' </summary>
        ''' <param name="tariffDto">El objeto de transferencia de datos (DTO) que contiene toda la información de la tarifa.</param>
        ''' <returns>
        ''' El identificador único (<italic>id_trfa</italic>) de la tarifa. 
        ''' Si fue una inserción, devuelve el ID autogenerado por MySQL; si fue una actualización, devuelve el mismo ID de entrada.
        ''' </returns>
        ''' <remarks>
        ''' Patrón <bold>Upsert</bold>: Si el campo <italic>IdTariff</italic> es igual a cero (0), la función asume que es una 
        ''' tarifa nueva y ejecuta un INSERT recuperando el último ID con <italic>LAST_INSERT_ID()</italic>. 
        ''' Si el ID es mayor a cero, ejecuta un UPDATE sobre el registro correspondiente.
        ''' </remarks>
        Public Function UpsertTariff(tariffDto As TariffDTO) As Integer

            Dim sqlQuery As String
            Dim insertedId As Integer = tariffDto.IdTariff

            If tariffDto.IdTariff = 0 Then

                sqlQuery = "INSERT INTO trfa_dscto (tipo_trfa, prcio_trfa, emin_trfa, emax_trfa, nperson_trfa, dscto_trfa) " &
                           "VALUES (@tipo_trfa, @prcio_trfa, @emin_trfa, @emax_trfa, @nperson_trfa, @dscto_trfa); " &
                           "SELECT LAST_INSERT_ID();"
            Else

                sqlQuery = "UPDATE trfa_dscto SET " &
                           "tipo_trfa = @tipo_trfa, prcio_trfa = @prcio_trfa, emin_trfa = @emin_trfa, " &
                           "emax_trfa = @emax_trfa, nperson_trfa = @nperson_trfa, dscto_trfa = @dscto_trfa " &
                           "WHERE id_trfa = @id_trfa"
            End If

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.AddWithValue("@tipo_trfa", tariffDto.PaymentMethod)
                    command.Parameters.AddWithValue("@prcio_trfa", tariffDto.Price)
                    command.Parameters.AddWithValue("@emin_trfa", tariffDto.MinimumAge)
                    command.Parameters.AddWithValue("@emax_trfa", tariffDto.MaximumAge)
                    command.Parameters.AddWithValue("@nperson_trfa", tariffDto.NumberMembers)
                    command.Parameters.AddWithValue("@dscto_trfa", tariffDto.Discount)

                    If tariffDto.IdTariff > 0 Then command.Parameters.AddWithValue("@id_trfa", tariffDto.IdTariff)

                    connection.Open()

                    If tariffDto.IdTariff = 0 Then

                        insertedId = Convert.ToInt32(command.ExecuteScalar())
                    Else
                        command.ExecuteNonQuery()

                    End If

                End Using

            End Using

            Return insertedId

        End Function


        ''' <summary>
        ''' Actualiza en cascada el precio base de todas las tarifas derivadas por edad o grupo familiar.
        ''' </summary>
        ''' <param name="newPrice">El nuevo precio base establecido en la tarifa mensual.</param>
        ''' <returns>True si se actualizó al menos una tarifa derivada; de lo contrario, False.</returns>
        Public Function UpdateDerivedTariffsPrice(newPrice As Decimal) As Boolean

            Dim sqlQuery As String = "UPDATE trfa_dscto SET prcio_trfa = @NewPrice
                                        WHERE tipo_trfa LIKE 'DSCTO EDAD%' OR tipo_trfa LIKE 'GRUPO FAM%'"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.AddWithValue("@NewPrice", newPrice)

                    Try
                        connection.Open()

                        ' ExecuteNonQuery devuelve el número de filas afectadas en la BBDD
                        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                        Return rowsAffected > 0

                    Catch ex As MySqlException

                        Throw New Exception($"ERROR DE MySQL : {vbCrLf}{ex.Message}")

                    Catch ex As Exception
                        Throw New Exception($"ERROR GENERAL : {vbCrLf}{ex.Message}")

                    End Try

                End Using
            End Using

        End Function


        ''' <summary>
        ''' Elimina de forma permanente una tarifa de la base de datos a partir de su ID único.
        ''' </summary>
        ''' <param name="tariffId">El ID de la tarifa que se desea eliminar.</param>
        ''' <returns>True si el registro se eliminó correctamente; False si no se encontró o no se pudo borrar.</returns>
        Public Function DeleteTariff(tariffId As Integer) As Boolean

            ' ID 1 (tarifa mensual base) NO se borra, por si falla la validación de la interfaz.
            If tariffId = 1 Then Return False

            Dim sqlQuery As String = "DELETE FROM trfa_dscto WHERE id_trfa = @idTariff"

            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    command.Parameters.AddWithValue("@idTariff", tariffId)

                    Try
                        connection.Open()

                        ' ExecuteNonQuery devuelve el número de filas afectadas en la BBDD
                        Dim rowsAffected As Integer = command.ExecuteNonQuery()

                        Return rowsAffected > 0

                    Catch ex As MySqlException

                        Throw New Exception(ex.Message)

                    Catch ex As Exception
                        Throw New Exception(ex.Message)

                    End Try

                End Using
            End Using

        End Function


    End Class

End Namespace