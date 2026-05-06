Namespace UIHelpers

    ''' <summary>
    ''' Proporciona métodos para validar y restringir la entrada de datos en controles de UI.
    ''' Enfocado principalmente a eventos KeyPress para filtrar caracteres en tiempo real.
    ''' </summary>
    Public Module InputValidators

        ''' <summary>
        ''' Determina si un carácter introducido es válido según las reglas especificadas.
        ''' Permite configurar si se aceptan letras, números, teclas de control,
        ''' caracteres explícitamente permitidos o bloqueados.
        ''' </summary>
        ''' <param name="keyChar">Carácter introducido por el usuario.</param>
        ''' <param name="allowLetters">Indica si se permiten letras.</param>
        ''' <param name="allowDigits">Indica si se permiten números.</param>
        ''' <param name="allowControl">Indica si se permiten teclas de control (Backspace, Delete, etc.).</param>
        ''' <param name="allowedChars">Cadena de caracteres adicionales permitidos.</param>
        ''' <param name="blockedChars">Cadena de caracteres explícitamente bloqueados.</param>
        ''' <returns>True si el carácter es válido; False en caso contrario.</returns>
        Private Function IsAllowedInput(keyChar As Char,
                                        allowLetters As Boolean,
                                        allowDigits As Boolean,
                                        allowControl As Boolean,
                                        allowedChars As String,
                                        blockedChars As String) As Boolean

            If allowControl AndAlso Char.IsControl(keyChar) Then Return True
            If allowLetters AndAlso Char.IsLetter(keyChar) Then Return True
            If allowDigits AndAlso Char.IsDigit(keyChar) Then Return True
            If Not String.IsNullOrEmpty(allowedChars) AndAlso allowedChars.Contains(keyChar) Then Return True
            If Not String.IsNullOrEmpty(blockedChars) AndAlso blockedChars.Contains(keyChar) Then Return False

            Return False

        End Function


        ''' <summary>
        ''' Permite únicamente letras, teclas de control y caracteres adicionales opcionales.
        ''' También permite bloquear caracteres específicos.
        ''' Diseñado para usarse en eventos KeyPress.
        ''' </summary>
        ''' <param name="e">Argumentos del evento KeyPress.</param>
        ''' <param name="allowedChars">Caracteres adicionales permitidos (opcional).</param>
        ''' <param name="blockedChars">Caracteres a bloquear explícitamente (opcional).</param>
        Public Sub AllowOnlyLetters(e As KeyPressEventArgs,
                                    Optional allowedChars As String = "",
                                    Optional blockedChars As String = "")

            If Not IsAllowedInput(e.KeyChar, True, False, True, allowedChars, blockedChars) Then
                e.Handled = True
            End If

        End Sub


        ''' <summary>
        ''' Permite únicamente números enteros (0-9), teclas de control
        ''' y caracteres adicionales opcionales.
        ''' Diseñado para validación en eventos KeyPress.
        ''' </summary>
        ''' <param name="e">Argumentos del evento KeyPress.</param>
        ''' <param name="allowedChars">Caracteres adicionales permitidos (opcional).</param>
        Public Sub AllowOnlyIntegers(e As KeyPressEventArgs,
                                     Optional allowedChars As String = "")

            If Not IsAllowedInput(e.KeyChar, False, True, True, allowedChars, Nothing) Then
                e.Handled = True
            End If

        End Sub


        ''' <summary>
        ''' Permite letras y números (alfanumérico), teclas de control
        ''' y caracteres adicionales definidos por el usuario.
        ''' Diseñado para usarse en eventos KeyPress.
        ''' </summary>
        ''' <param name="e">Argumentos del evento KeyPress.</param>
        ''' <param name="allowedChars">Caracteres adicionales permitidos (opcional).</param>
        Public Sub AllowLettersAndDigits(e As KeyPressEventArgs,
                                         Optional allowedChars As String = "")

            If Not IsAllowedInput(e.KeyChar, True, True, True, allowedChars, Nothing) Then
                e.Handled = True
            End If

        End Sub


        ''' <summary>
        ''' Permite la introducción de números decimales respetando la configuración regional.
        ''' Acepta dígitos, teclas de control y un único separador decimal.
        ''' Convierte automáticamente "." o "," al separador correcto del sistema.
        ''' Diseñado para eventos KeyPress en controles TextBox.
        ''' </summary>
        ''' <param name="sender">Control que dispara el evento (esperado: TextBox).</param>
        ''' <param name="e">Argumentos del evento KeyPress.</param>
        Public Sub AllowDecimalInput(sender As Object,
                                     e As KeyPressEventArgs)

            Dim textBox = TryCast(sender, TextBox)
            If textBox Is Nothing Then Return

            Dim separator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator

            ' Permitir números y control
            If Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar) Then Return

            ' Permitir separador decimal (una sola vez)
            If e.KeyChar = "."c OrElse e.KeyChar = ","c Then

                If textBox.Text.Contains(separator) Then
                    e.Handled = True
                Else
                    e.KeyChar = CChar(separator)
                End If

                Return
            End If

            ' Bloquear todo lo demás
            e.Handled = True

        End Sub


        '''' <summary>
        '''' Permite únicamente letras, teclas de control (Backspace, etc.),
        '''' caracteres explícitamente permitidos y bloquea los definidos en strLockKey.
        '''' </summary>
        'Public Sub ValidateOnlyLetters(strAllowKey As String, strLockKey As String, e As KeyPressEventArgs)
        '    Dim isLetter As Boolean = Char.IsLetter(e.KeyChar)
        '    Dim isControl As Boolean = Char.IsControl(e.KeyChar)
        '    Dim isAllowed As Boolean = strAllowKey.Contains(e.KeyChar)
        '    Dim isLocked As Boolean = strLockKey.Contains(e.KeyChar)
        '    ' Bloquear si no cumple condiciones o si está explícitamente bloqueado
        '    If (Not isLetter AndAlso Not isControl AndAlso Not isAllowed) OrElse isLocked Then
        '        e.Handled = True
        '    End If
        'End Sub
        '''' <summary>
        '''' Permite únicamente números enteros (0-9), teclas de control
        '''' y caracteres opcionalmente permitidos.
        '''' </summary>
        'Public Sub ValidateIntegerNumbers(strAllowKey As String, e As KeyPressEventArgs)
        '    Dim isDigit As Boolean = Char.IsDigit(e.KeyChar)
        '    Dim isControl As Boolean = Char.IsControl(e.KeyChar)
        '    Dim isAllowed As Boolean = strAllowKey.Contains(e.KeyChar)
        '    If Not isDigit AndAlso Not isControl AndAlso Not isAllowed Then
        '        e.Handled = True
        '    End If
        'End Sub
        '''' <summary>
        '''' Permite letras y números (alfanumérico), teclas de control
        '''' y caracteres adicionales definidos por el usuario.
        '''' </summary>
        'Public Sub ValidateNumbersAndLetters(strAllowKey As String, e As KeyPressEventArgs)
        '    Dim isLetterOrDigit As Boolean = Char.IsLetterOrDigit(e.KeyChar)
        '    Dim isControl As Boolean = Char.IsControl(e.KeyChar)
        '    Dim isAllowed As Boolean = strAllowKey.Contains(e.KeyChar)
        '    If Not isLetterOrDigit AndAlso Not isControl AndAlso Not isAllowed Then
        '        e.Handled = True
        '    End If
        'End Sub
        '''' <summary>
        '''' Permite números decimales usando "." como separador.
        '''' Evita múltiples separadores en el mismo TextBox.
        '''' ⚠ Depende de UI (TextBox).
        '''' </summary>
        'Public Sub ValidateDecimalNumbers(textBox As TextBox, e As KeyPressEventArgs)
        '    Dim isDigit As Boolean = Char.IsDigit(e.KeyChar)
        '    Dim isControl As Boolean = Char.IsControl(e.KeyChar)
        '    If Not isDigit AndAlso Not isControl Then
        '        If e.KeyChar = "."c AndAlso Not textBox.Text.Contains(",") Then
        '            e.Handled = False
        '        Else
        '            e.Handled = True
        '        End If
        '    End If
        'End Sub
        '''' <summary>
        '''' Permite introducir números decimales respetando la configuración regional.
        '''' Convierte automáticamente "." o "," al separador correcto del sistema.
        '''' </summary>
        'Public Sub ValidateDecimalInput(sender As Object, e As KeyPressEventArgs)
        '    Dim textBox = TryCast(sender, TextBox)
        '    If textBox Is Nothing Then Return
        '    ' Separador decimal según cultura (ej: "," en ES, "." en US)
        '    Dim separator As String = Application.CurrentCulture.NumberFormat.NumberDecimalSeparator
        '    If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
        '        If (e.KeyChar = "."c OrElse e.KeyChar = ","c) Then
        '            ' Evitar múltiples separadores
        '            If textBox.Text.Contains(separator) Then
        '                e.Handled = True
        '            Else
        '                ' Reemplazar por el separador correcto
        '                e.KeyChar = CChar(separator)
        '                e.Handled = False
        '            End If
        '        Else
        '            e.Handled = True
        '        End If
        '    End If
        'End Sub

    End Module
End Namespace