Imports GymPaymentControl.Data
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services

    Public Class TariffManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ''' <summary>
        ''' Recupera todas las tarifas y descuentos de la base de datos ordenadas por tipo.
        ''' </summary>
        Public Function FetchAllTariffs() As List(Of TariffDTO)

            Dim tariffsList As New List(Of TariffDTO)()

            Dim sqlQuery As String = "SELECT * FROM trfa_dscto ORDER BY tipo_trfa"

            ' Utilizar bloques Using asegura que la conexión y el comando se cierren correctamente pase lo que pase
            Using connection = GetConnection()

                Using command As New MySqlCommand(sqlQuery, connection)

                    connection.Open()

                    Using reader As MySqlDataReader = command.ExecuteReader()

                        If reader.HasRows Then

                            While reader.Read()
                                ' Creamos el DTO y mapeamos los campos de forma segura usando sus tipos correspondientes
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


        '==============================================================================

        Public Function Save(tariffDto As TariffDTO) As Integer

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


    End Class
    End Namespace