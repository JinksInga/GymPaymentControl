Imports GymPaymentControl.Data
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Ocsp

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
                                        .Id = reader.GetInt16("id_trfa"),
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

    End Class

End Namespace