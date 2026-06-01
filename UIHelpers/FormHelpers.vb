Imports GymPaymentControl.Utils.Validations

Namespace UIHelpers

    ''' <summary>
    ''' Proporciona métodos auxiliares para manipular controles de formularios (UI),
    ''' como activación/desactivación de controles y validación visual de datos.
    ''' Estos métodos combinan lógica de negocio básica con representación visual.
    ''' </summary>
    Public Module FormHelpers

        Public Function EvaluateNumericRangeLimits(textBox As TextBox, valor As Decimal,
                                                   minimo As Decimal, maximo As Decimal) As Boolean

            Dim isFormatValid As Boolean = True

            ' (Tu lógica de la coma y los decimales...)
            If textBox.Text.Contains(",") Then
                Dim intDec() As String = textBox.Text.Split(","c)
                If intDec(1).Length = 2 OrElse intDec(1).Length > 4 Then
                    isFormatValid = False
                End If
            End If

            ' Condición de rango
            If Not isFormatValid OrElse (valor < minimo OrElse valor > maximo) Then

                textBox.ForeColor = Color.Red
                textBox.Font = New System.Drawing.Font(textBox.Font, FontStyle.Bold)

                Return False

            Else
                textBox.ForeColor = Color.Green
                textBox.Font = New System.Drawing.Font(textBox.Font, textBox.Font.Style And Not FontStyle.Bold)

                Return True

            End If

        End Function


        ''' <summary>
        ''' Aplica el formato visual de moneda (€) al TextBox
        ''' manteniendo la posición del cursor.
        ''' </summary>
        Public Sub ApplyMoneyTextboxFormat(textBox As TextBox)

            Dim cursorPos As Integer = textBox.SelectionStart

            Dim priceWithoutFormat As String = NormalizeMoneyText(textBox.Text)

            textBox.Text = $"{priceWithoutFormat} €"
            textBox.SelectionStart = Math.Min(cursorPos, textBox.Text.Length)

        End Sub


        ''' <summary>
        ''' Actualiza el color y, opcionalmente, el texto de una colección de Labels.
        ''' </summary>
        ''' <param name="labels">
        ''' Colección de controles Label que serán modificados.
        ''' </param>
        ''' <param name="foreColor">
        ''' Color que se aplicará al texto de los Labels.
        ''' </param>
        ''' <param name="text">
        ''' Texto opcional que se asignará a todos los Labels.
        ''' Si es Nothing, el texto actual no se modifica.
        ''' </param>
        ''' <remarks>
        ''' Esta función permite centralizar cambios visuales repetitivos
        ''' en múltiples Labels, evitando duplicación de código en formularios.
        ''' 
        ''' Casos de uso:
        ''' - Mostrar estados de ERROR.
        ''' - Restaurar colores por defecto.
        ''' - Actualizar mensajes visuales masivos.
        ''' </remarks>
        Public Sub UpdateLabelsState(labels As IEnumerable(Of Label),
                                     foreColor As Color,
                                     Optional text As String = Nothing)

            For Each label In labels

                label.ForeColor = foreColor

                If text IsNot Nothing Then label.Text = text

            Next

        End Sub


        ''' <summary>
        ''' Recorre recursivamente todos los controles hijos de un contenedor y ejecuta una acción
        ''' sobre los Label cuyo nombre contiene el filtro especificado.
        ''' </summary>
        ''' <param name="parent">
        ''' Contenedor principal desde donde comenzará la búsqueda recursiva.
        ''' </param>
        ''' <param name="filter">
        ''' Texto que debe contener el nombre del Label para ser modificado.
        ''' </param>
        ''' <param name="action">
        ''' Acción que se ejecutará sobre cada Label que cumpla el filtro indicado.
        ''' </param>
        Public Sub ProcessLabelsRecursive(parent As Control,
                                          filter As String,
                                          action As Action(Of Label))

            For Each ctrl As Control In parent.Controls

                If TypeOf ctrl Is Label AndAlso ctrl.Name.Contains(filter) Then
                    action(DirectCast(ctrl, Label))
                End If

                If ctrl.HasChildren Then
                    ProcessLabelsRecursive(ctrl, filter, action)
                End If

            Next

        End Sub
        '|* HEMOS DEJADO COMENTADO PARA QUE NOS SIRVA DE EJEMPLO POR SI HACE FALTA PARA OTROS CONTROLES *|'
        'Public Sub UpdateLabelColorsRecursive(parent As Control, targetColor As Color, filter As String)
        '    For Each ctrl As Control In parent.Controls
        '        If TypeOf ctrl Is Label AndAlso ctrl.Name.Contains(filter) Then
        '            ctrl.ForeColor = targetColor
        '        End If
        '        If ctrl.HasChildren Then
        '            UpdateLabelColorsRecursive(ctrl, targetColor, filter)
        '        End If
        '    Next
        'End Sub


        ''' <summary>
        ''' Activa o desactiva un control en función del estado de un CheckBox
        ''' y actualiza el ToolTip asociado.
        ''' Si el CheckBox está marcado, el control se habilita y recibe el foco.
        ''' </summary>
        ''' <param name="control">Control que se desea activar o desactivar.</param>
        ''' <param name="checkBox">CheckBox que determina el estado.</param>
        ''' <param name="toolTip">ToolTip asociado al CheckBox.</param>
        ''' <param name="strDeactivate">Texto del ToolTip cuando el control está activo.</param>
        ''' <param name="strActivate">Texto del ToolTip cuando el control está inactivo.</param>
        Public Sub ToggleControl(control As Control,
                                 checkBox As CheckBox,
                                 toolTip As ToolTip,
                                 strDeactivate As String,
                                 strActivate As String)

            control.Enabled = checkBox.Checked

            If checkBox.Checked Then
                control.Focus()
                toolTip.SetToolTip(checkBox, strDeactivate)
            Else
                toolTip.SetToolTip(checkBox, strActivate)
            End If

        End Sub


        ''' <summary>
        ''' Valida los datos del cliente en los campos obligatorios utilizando la
        ''' lógica de negocio y actualiza la interfaz de usuario en consecuencia.
        ''' - Muestra un error si el campo está vacío o no es válido.
        ''' - Normaliza el texto si es correcto.
        ''' - Cambia el color del control según el resultado.
        ''' </summary>
        ''' <param name="textBox">Control que contiene el nombre del cliente.</param>
        ''' <param name="errorProvider">Componente usado para mostrar errores en la UI.</param>
        Public Sub ValidateRequiredFieldUI(textBox As TextBox, errorProvider As ErrorProvider)

            errorProvider.Clear()

            If Not IsCustomerNameValid(textBox.Text) Then
                errorProvider.SetError(textBox, EmptyField)
                textBox.BackColor = Color.MistyRose
            Else
                textBox.Text = NormalizeName(textBox.Text)
                textBox.BackColor = Color.Azure
            End If

        End Sub


        ''' <summary>
        ''' Valida que un TextBox contenga información.
        ''' </summary>
        Public Function ValidateRequiredField(fieldName As String,
                                              titleAction As String,
                                              textBox As TextBox) As Boolean

            If String.IsNullOrWhiteSpace(textBox.Text) Then

                ShowValidationError($"  Verifica la información del cliente.{vbCrLf}{vbCrLf}" &
                                    $"  El campo {fieldName.ToUpper()} no puede estar vacío.",
                                    $"Error al {titleAction.ToLower()}",
                                    textBox)
                Return False
            End If

            Return True
        End Function


        ''' <summary>
        ''' Valida los datos del cliente en los campos opcionales utilizando la
        ''' lógica de negocio y actualiza la interfaz de usuario en consecuencia.
        ''' - Cambia el color que indica OK.
        ''' - Normaliza el texto si es correcto.
        ''' </summary>
        ''' <param name="textBox">Control que contiene el nombre del cliente.</param>
        ''' <param name="errorProvider">Componente usado para mostrar errores en la UI.</param>
        Public Sub ValidateOptionalFieldUI(textBox As TextBox, errorProvider As ErrorProvider)

            errorProvider.Clear()
            textBox.BackColor = Color.Azure

            If IsCustomerNameValid(textBox.Text) Then
                textBox.Text = NormalizeName(textBox.Text)
            End If

        End Sub


        ''' <summary>
        ''' Valida la edad del cliente utilizando la lógica de negocio
        ''' y refleja el resultado en la interfaz de usuario.
        ''' - Muestra un error si la edad no es válida.
        ''' - Cambia el color del control según el resultado.
        ''' </summary>
        ''' <param name="control">Control que contiene la edad del cliente.</param>
        ''' <param name="errorProvider">Componente usado para mostrar errores en la UI.</param>
        ''' <param name="color">Color que indica si estamos cambiando o se confirma la edad.</param>
        ''' <remarks>
        ''' Colores usados:
        ''' - MistyRose: indica error
        ''' - Azure: indica estado válido
        ''' </remarks>
        Public Sub ValidateCustomerAgeUI(control As Control, errorProvider As ErrorProvider, color As Color)

            errorProvider.Clear()

            If Not IsCustomerAgeValid(control.Text) Then
                errorProvider.SetError(control, WrongAge)
                control.BackColor = Color.MistyRose
            Else
                control.BackColor = color
            End If

        End Sub


        ''' <summary>
        ''' Valida que la edad del cliente sea válida y mayor o igual a 5 años.
        ''' </summary>
        Public Function ValidateCustomerAge(titleAction As String,
                                            label As Label,
                                            datePicker As DateTimePicker) As Boolean

            If String.IsNullOrWhiteSpace(label.Text) OrElse
               CInt(Val(label.Text)) < 5 Then

                ShowValidationError($"  Verifica la edad del cliente:{vbCrLf}{vbCrLf}" &
                                    $"  1. El campo no puede estar vacío.{vbCrLf}" &
                                    $"  2. No puede ser menor de 5 años.",
                                    $"Error al {titleAction.ToLower()}",
                                    datePicker)

                Return False
            End If

            Return True
        End Function


        ''' <summary>
        ''' Valida visualmente el contenido de un TextBox como dirección de correo electrónico.
        ''' Actualiza el ErrorProvider y el color del control según el resultado.
        ''' </summary>
        ''' <param name="textBox">Control que contiene el correo electrónico.</param>
        ''' <param name="errorProvider">Componente usado para mostrar errores en la UI.</param>
        Public Sub ValidateEmailUI(textBox As TextBox,
                                   errorProvider As ErrorProvider,
                                   Optional isInside As Boolean = True)

            errorProvider.Clear()

            If String.IsNullOrWhiteSpace(textBox.Text) Then
                textBox.BackColor = If(isInside, Color.Beige, Color.Azure)

            ElseIf Not IsValidEmail(textBox.Text) Then
                errorProvider.SetError(textBox, InvalidEmailMessage)
                textBox.BackColor = Color.MistyRose
            Else
                errorProvider.Clear()
                textBox.BackColor = If(isInside, Color.Beige, Color.Azure)
            End If

        End Sub


        ''' <summary>
        ''' Valida el formato correcto del Email del cliente .
        ''' </summary>
        Public Function ValidateEmail(titleAction As String,
                                      textBox As TextBox) As Boolean

            If String.IsNullOrWhiteSpace(textBox.Text) Then Return True

            If IsValidEmail(textBox.Text) Then Return True

            textBox.BackColor = Color.MistyRose

            ShowValidationError(InvalidEmailMessage, $"Error al {titleAction.ToLower()}", textBox)

            Return False

        End Function


        ''' <summary>
        ''' FUNCIÓN REUTILIZABLE CENTRAL PARA LOS MSGBOX
        ''' Muestra un mensaje de error estandarizado y opcionalmente
        ''' establece el foco sobre un control.
        ''' </summary>
        ''' <param name="message">Mensaje principal del error.</param>
        ''' <param name="title">Título de la ventana.</param>
        ''' <param name="control">Control que recibirá el foco (opcional).</param>
        Public Sub ShowValidationError(message As String,
                                       title As String,
                                       Optional control As Control = Nothing)

            MsgBox(message, vbCritical, title)

            If control IsNot Nothing Then
                control.Focus()
            End If

        End Sub


        ''' <summary>
        ''' Valida que al menos un método de pago esté seleccionado.
        ''' </summary>
        Public Function ValidatePaymentMethod(titleAction As String,
                                              ParamArray radioButtons() As RadioButton) As Boolean

            If Not radioButtons.Any(Function(rb) rb.Checked) Then

                ShowValidationError("  Selecciona un MÉTODO de pago.",
                                    $"Error al {titleAction.ToLower()}")
                Return False
            End If

            Return True
        End Function


        ''' <summary>
        ''' Valida que exista un elemento seleccionado cuando un RadioButton está activo.
        ''' </summary>
        Public Function ValidateRequiredSelection(selectionType As String,
                                                  titleAction As String,
                                                  textBox As TextBox,
                                                  radioButton As RadioButton) As Boolean

            If radioButton.Checked AndAlso String.IsNullOrWhiteSpace(textBox.Text) Then

                If selectionType.ToUpper() = "DIARIO" Then
                    selectionType = "pago diario"
                End If

                ShowValidationError($"  Verifica la información del cliente.{vbCrLf}{vbCrLf}" &
                                    $"  Selecciona un {selectionType} de la lista.",
                                    $"Error al {titleAction.ToLower()}",
                                    textBox)
                Return False
            End If

            Return True
        End Function


        ''' <summary>
        ''' Valida si un grupo familiar admite nuevos integrantes.
        ''' </summary>
        Public Function ValidateGroupCapacity(titleAction As String,
                                              button As Button) As Boolean

            If button.Enabled Then

                ShowValidationError("No se puede registrar un cliente en un GRUPO FAMILIAR completo." & vbCrLf & vbCrLf &
                                    "Haz clic en el botón [Ampliar cupo] para admitir a un nuevo integrante.",
                                    $"Error al {titleAction.ToLower()}",
                                    button)
                Return False
            End If

            Return True
        End Function

    End Module
End Namespace