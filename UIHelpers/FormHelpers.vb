Imports GymPaymentControl.Utils

Namespace UIHelpers
    ''' <summary>
    ''' Proporciona métodos auxiliares para manipular controles de formularios (UI),
    ''' como activación/desactivación de controles y validación visual de datos.
    ''' Estos métodos combinan lógica de negocio básica con representación visual.
    ''' </summary>
    Public Module FormHelpers


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
        ''' Valida el nombre del cliente utilizando la lógica de negocio
        ''' y actualiza la interfaz de usuario en consecuencia.
        ''' - Muestra un error si el campo está vacío o no es válido.
        ''' - Normaliza el texto si es correcto.
        ''' - Cambia el color del control según el resultado.
        ''' </summary>
        ''' <param name="textBox">Control que contiene el nombre del cliente.</param>
        ''' <param name="errorProvider">Componente usado para mostrar errores en la UI.</param>
        Public Sub ValidateCustomerDataUI(textBox As TextBox, errorProvider As ErrorProvider)

            errorProvider.Clear()

            If Not Validations.IsCustomerNameValid(textBox.Text) Then

                errorProvider.SetError(textBox, "El campo no puede estar vacío.")
                textBox.BackColor = Color.MistyRose

            Else

                textBox.Text = Validations.NormalizeName(textBox.Text)
                textBox.BackColor = Color.Azure

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
        ''' <remarks>
        ''' Colores usados:
        ''' - MistyRose: indica error
        ''' - Azure: indica estado válido
        ''' </remarks>
        Public Sub ValidateCustomerAgeUI(control As Control, errorProvider As ErrorProvider)

            errorProvider.Clear()

            If Not Validations.IsCustomerAgeValid(control.Text) Then

                errorProvider.SetError(control, "Verifica la edad del cliente.")
                control.BackColor = Color.MistyRose

            Else

                control.BackColor = Color.Azure

            End If

        End Sub


    End Module
End Namespace