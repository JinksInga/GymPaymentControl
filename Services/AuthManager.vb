Imports System.Security.Cryptography
Imports System.Text
Imports GymPaymentControl.Data
Imports GymPaymentControl.Models
Imports MySql.Data.MySqlClient

Namespace Services
    Public Class AuthManager

        ' Al heredar, obtenemos el motor de conexión.
        Inherits BaseRepository

        ' Busca un usuario y devuelve sus datos si existe
        Public Function GetUser(username As String) As UserDTO

            Using connection = GetConnection()

                Dim sql = "SELECT id_user, nom_user, pwd_user, crg_user FROM usuarios WHERE nom_user = @user"
                Dim cmd As New MySqlCommand(sql, connection)

                cmd.Parameters.AddWithValue("@user", username)
                connection.Open()

                Using reader = cmd.ExecuteReader()

                    If reader.Read() Then
                        ' Verificamos si tiene contraseña (lógica de primer ingreso)
                        Dim pwdInDB = reader.GetString("pwd_user")

                        Return New UserDTO With
                            {
                                .Id = reader.GetInt32("id_user"),
                                .Username = reader.GetString("nom_user"),
                                .Role = reader.GetString("crg_user")
                            }
                    End If

                End Using

            End Using

            Return Nothing

        End Function

        ' Valida Login completo
        Public Function ValidateLogin(username As String, password As String) As UserDTO

            Using connection = GetConnection()

                Dim sql = "SELECT * FROM usuarios WHERE nom_user = @user AND pwd_user = @pwd"
                Dim cmd As New MySqlCommand(sql, connection)
                Dim validateKey As String = EncryptKey(password)

                cmd.Parameters.AddWithValue("@user", username)
                cmd.Parameters.AddWithValue("@pwd", validateKey)
                connection.Open()

                Using reader = cmd.ExecuteReader()

                    If reader.Read() Then
                        Return New UserDTO With
                            {
                                .Id = reader.GetInt32("id_user"),
                                .Username = reader.GetString("nom_user"),
                                .Role = reader.GetString("crg_user")
                            }
                    End If

                End Using

            End Using

            Return Nothing

        End Function

        ' Actualiza la contraseña
        Public Sub UpdatePassword(userId As Integer, newPassword As String)

            Using connection = GetConnection()

                Dim sql As String = "UPDATE usuarios SET pwd_user = @pwd WHERE id_user = @id"

                Using cmd As New MySqlCommand(sql, connection)

                    cmd.Parameters.AddWithValue("@pwd", EncryptKey(newPassword))
                    cmd.Parameters.AddWithValue("@id", userId)
                    connection.Open()

                    Dim filasAfectadas As Integer = cmd.ExecuteNonQuery()

                    ' Si filasAfectadas es 0, es que el ID no existe en la tabla
                    If filasAfectadas = 0 Then
                        Throw New Exception("No se encontró el usuario con ID: " & userId)
                    End If

                End Using

            End Using

        End Sub

        Private Function EncryptKey(password As String) As String

            Using sha256 As SHA256 = SHA256.Create()
                ' Convertimos la contraseña en un array de bytes
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
                ' Computamos el Hash
                Dim hash As Byte() = sha256.ComputeHash(bytes)

                ' Lo convertimos a una cadena hexadecimal para guardarlo en la BD
                Dim sb As New StringBuilder()

                For i As Integer = 0 To hash.Length - 1
                    sb.Append(hash(i).ToString("x2"))
                Next

                Return sb.ToString()

            End Using

        End Function

        Public Sub RegisterSession()

            Using connection = GetConnection() 'As New MySqlConnection(_connectionString)
                ' Usamos NOW() de MySQL para que la hora la ponga el servidor de base de datos
                Dim sql As String = "INSERT INTO sesion_user (id_user, fh_entrada) VALUES (@id, NOW())"

                Using cmd As New MySqlCommand(sql, connection)

                    cmd.Parameters.AddWithValue("@id", UserSession.IdUser)
                    connection.Open()
                    cmd.ExecuteNonQuery()

                End Using

            End Using

        End Sub

    End Class

End Namespace