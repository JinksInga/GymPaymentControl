Imports System.Text.RegularExpressions

Namespace Utils

    Public Module Validations

        Public Sub ValidateOnlyLetters(strAllowKey As String, strLockKey As String, e As KeyPressEventArgs)

            '| IF : Comprobamos si una de las condiciones se cumplem.
            '|      * Not Char.IsLetter(e.KeyChar) - Permite letras mayúsculas y minúsculas.
            '|      * Not Char.IsControl(e.KeyChar) - Permite teclas de control por ejemplo Backspace.
            '|      * Not Allow_Key.Contains(e.KeyChar)) - Permite los caracteres que están almacenadas en la variable.
            '|      * Lock_Key.Contains(e.KeyChar)) - Bloquea los caracteres que están almacenadas en la variable.
            '| * Si la tecla no cumple NINGUNA de las condiciones anteriores la bloqueamos - e.Handled = True

            ' Combinamos las condiciones en una sola evaluación lógica clara
            Dim isLetter As Boolean = Char.IsLetter(e.KeyChar)
            Dim isControl As Boolean = Char.IsControl(e.KeyChar)
            Dim isAllowed As Boolean = strAllowKey.Contains(e.KeyChar)
            Dim isLocked As Boolean = strLockKey.Contains(e.KeyChar)

            If (Not isLetter AndAlso Not isControl AndAlso Not isAllowed) OrElse isLocked Then e.Handled = True

        End Sub

        Public Sub ValidateIntegerNumbers(strAllowKey As String, e As KeyPressEventArgs)

            '| * Almacenamos en la variable Allow_Key los caracteres PERMITIDOS.
            '| IF : Comprobamos si una de las condiciones se cumplem.
            '|      * Not Char.IsDigit(e.KeyChar) - Permite dígitos.
            '|      * Not Char.IsControl(e.KeyChar) - Permite teclas de control _
            '|        _ por ejemplo Backspace.
            '|      * Not Allow_Key.Contains(e.KeyChar)) - Permite los caracteres _
            '|        _ que están almacenadas en la variable.
            '| * Si la tecla no cumple NINGUNA de las condiciones anteriores la _
            '|   _ bloqueamos - e.Handled = True.

            Dim isDigit As Boolean = Char.IsDigit(e.KeyChar)
            Dim isControl As Boolean = Char.IsControl(e.KeyChar)
            Dim isAllowed As Boolean = strAllowKey.Contains(e.KeyChar)

            If Not isDigit AndAlso Not isControl AndAlso Not isAllowed Then e.Handled = True

        End Sub

        Public Sub ValidateNumbersAndLetters(strAllowKey As String, e As KeyPressEventArgs)

            '| IF : Comprobamos si una de las condiciones se cumplem.
            '|      * Not Char.IsLetterOrDigit(e.KeyChar) - Si el carácter es una letra o un dígito, incluye los _
            '|        _ caracteres acentuados y la ñ, haciendo que la validación sea más robusta.
            '|      * Not Char.IsControl(e.KeyChar) - Permite teclas de control por ejemplo Backspace.
            '|      * Not Allow_Key.Contains(e.KeyChar)) - Permite los caracteres que están almacenadas en la variable.
            '| * Si la tecla no cumple NINGUNA de las condiciones anteriores la bloqueamos - e.Handled = True

            Dim isLetterOrDigit As Boolean = Char.IsLetterOrDigit(e.KeyChar)
            Dim isControl As Boolean = Char.IsControl(e.KeyChar)
            Dim isAllowed As Boolean = strAllowKey.Contains(e.KeyChar)

            If Not isLetterOrDigit AndAlso Not isControl AndAlso Not isAllowed Then e.Handled = True

        End Sub

        Public Sub ValidateDecimalNumbers(textBox As TextBox, e As KeyPressEventArgs)

            '| IF : Comprobamos si una de las condiciones se cumplem.
            '|      * Not Char.IsDigit(e.KeyChar) - Permite dígitos.
            '|      * Not Char.IsControl(e.KeyChar) - Permite teclas de control por ejemplo Backspace.
            '|      IF : Comprobamos si es el punto decimal y cambiamos por la coma.
            '|          * e.Handled = False - Permite ingresar solo un punto.
            '| * Si la tecla no cumple NINGUNA de las condiciones anteriores la bloqueamos - e.Handled = True

            Dim isDigit As Boolean = Char.IsDigit(e.KeyChar)
            Dim isControl As Boolean = Char.IsControl(e.KeyChar)

            If Not isDigit AndAlso Not isControl Then

                If e.KeyChar = "."c AndAlso Not textBox.Text.Contains(",") Then
                    e.Handled = False
                Else
                    e.Handled = True
                End If

            End If

        End Sub

        Public Function IsValidEmail(eMail As String) As Boolean

            '| * Almacenamos en la variable strRegex un patrón de Regex estándar para validar _
            '|   _ correos electrónicos, cubre letras, números, guiones, puntos, guiones _
            '|   _ bajos, el signo de más y asegura la estructura parte_local@dominio.tld.
            '| * Regex.IsMatch comprueba si la cadena cumple con el patrón y devuelve si es _
            '|   _ verdadero o falso.
            'If String.IsNullOrWhiteSpace(eMail) Then Return False
            Dim strRegex As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
            Return Regex.IsMatch(eMail, strRegex)

        End Function

        Public Sub ValidateDecimalInput(sender As Object, e As KeyPressEventArgs)

            '|
            '|
            '|

            Dim textBox = TryCast(sender, TextBox)
            If textBox Is Nothing Then Return

            ' Obtenemos el separador del sistema actual (, o .)
            Dim separator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator

            If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
                ' Permitir el punto o coma, pero solo una vez
                If (e.KeyChar = "."c OrElse e.KeyChar = ","c) Then
                    If textBox.Text.Contains(separator) Then
                        e.Handled = True ' Ya existe un separador
                    Else
                        ' Normalizamos la tecla a lo que el sistema espera
                        e.KeyChar = CChar(separator)
                        e.Handled = False
                    End If
                Else
                    e.Handled = True

                End If
            End If

        End Sub

        Public Sub ValidateCustomerData(textBox As TextBox, errorProvider As ErrorProvider)

            '| --------------------------------------------------------------------------------
            '| * Limpiamos cualquier error previo que se haya establecido en cualquier control.
            '|
            '| IF : Comprueba si el contenido del TextBox es NULO, VACÍO (""), o si solo contiene
            '|      ESPACIOS EN BLANCO (incluyendo tabs o saltos de línea):
            '|      * Si la validación falla activamos el ErrorProvider y cambiamos el color de
            '|        fondo del textbox que nos indica un error y requiere atención.
            '|
            '| ELSE : Si el campo tiene caracteres:
            '|      * Quitamos los espacios en blanco iniciales y finales de la cadena.
            '|
            '|      WHILE : Comienza un ciclo para eliminar múltiples espacios internos. Se ejecuta
            '|              MIENTRAS la cadena contenga la secuencia "  " (dos o más espacios).
            '|              * Reemplaza los DOS espacios consecutivos con UN solo espacio. Esto se
            '|                repite hasta que no queden más espacios dobles, asegurando un solo
            '|                espacio entre palabras.
            '|      ** Para la limpieza de espacios en blanco tambien podemos usar TRIM y luego
            '|         REGEX [Regex.Replace(cleanText, "\s+", " ")]. Para nuestro caso no sirve
            '|         porque borra los saltos de línea y concatena la dirección.**
            '|
            '|      * Cambiamos el color de fondo del TextBox que indica que el valor es correcto.

            errorProvider.Clear()
            If String.IsNullOrWhiteSpace(textBox.Text) Then
                errorProvider.SetError(textBox, "El campo no puede estar vacío.")
                textBox.BackColor = Color.MistyRose

            Else
                'textBox.Text = Regex.Replace(textBox.Text.Trim(), "\s+", " ")
                textBox.Text = Trim(textBox.Text)
                While textBox.Text.Contains("  ")
                    textBox.Text = textBox.Text.Replace("  ", " ")
                End While
                textBox.BackColor = Color.Azure

            End If

        End Sub

        Sub ValidateCustomerAge(lblLabel As Label, errorProvider As ErrorProvider)

            '| ------------------------------------------------------------------------
            '| * Limpiamos cualquier error previo.
            '|
            '| IF : Si el label está vacio
            '|      * Activamos el ErrorProvider y cambiamos el color del label que nos
            '|        indica error.
            '| ELSE :
            '|      * Cambiamos el color del label que indica que el valor es correcto.

            errorProvider.Clear()

            If String.IsNullOrWhiteSpace(lblLabel.Text) Or CInt(Val(lblLabel.Text)) < 5 Then
                errorProvider.SetError(lblLabel, "Verifica la edad del cliente.")
                lblLabel.BackColor = Color.MistyRose
            Else
                lblLabel.BackColor = Color.Azure
            End If

        End Sub

    End Module
End Namespace