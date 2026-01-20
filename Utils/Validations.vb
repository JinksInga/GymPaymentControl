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
        ''
        ''
        ''
        Public Sub ValidateDecimalInput(sender As Object, e As KeyPressEventArgs)

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
        ''
        ''
        ''
    End Module
End Namespace