Imports System.Text.RegularExpressions

Namespace Utils

    ''' <summary>
    ''' Proporciona métodos de validación y normalización de datos
    ''' independientes de la interfaz de usuario.
    ''' Estas funciones contienen lógica de negocio reutilizable
    ''' para garantizar la integridad y formato correcto de los datos.
    ''' </summary>
    ''' <remarks>
    ''' Este módulo no depende de controles de UI (TextBox, Label, etc.),
    ''' lo que permite reutilizar estas validaciones en cualquier capa de la aplicación.
    ''' </remarks>
    Public Module Validations


        ''' <summary>
        ''' Devuelve una cadena segura eliminando espacios al inicio y al final.
        ''' Si el valor es Nothing, retorna una cadena vacía.
        ''' Útil para comparaciones y para evitar errores por referencias nulas.
        ''' </summary>
        ''' <param name="value">Cadena de entrada.</param>
        ''' <returns>Cadena sin espacios laterales y nunca nula.</returns>
        Public Function SafeTrim(value As String) As String

            Return If(value, String.Empty).Trim()

        End Function


        ''' <summary>
        ''' Indica si un nombre de cliente es válido.
        ''' Un nombre se considera válido si no es nulo, vacío
        ''' ni contiene únicamente espacios en blanco.
        ''' </summary>
        ''' <param name="text">Texto a validar.</param>
        ''' <returns>True si el nombre es válido; False en caso contrario.</returns>
        Public Function IsCustomerNameValid(text As String) As Boolean

            Return Not String.IsNullOrWhiteSpace(text)

        End Function


        ''' <summary>
        ''' Normaliza un nombre eliminando espacios innecesarios.
        ''' - Quita espacios al inicio y al final.
        ''' - Reduce múltiples espacios internos a un solo espacio.
        ''' </summary>
        ''' <param name="text">Texto a normalizar.</param>
        ''' <returns>Cadena normalizada.</returns>
        Public Function NormalizeName(text As String) As String

            Dim result As String = text.Trim()

            ' Reemplazar múltiples espacios por uno solo
            While result.Contains("  ")
                result = result.Replace("  ", " ")
            End While

            Return result

        End Function


        ''' <summary>
        ''' Valida si la edad del cliente es correcta.
        ''' - Debe ser un valor numérico entero.
        ''' - Debe ser mayor o igual a 5.
        ''' </summary>
        ''' <param name="ageText">Texto que representa la edad.</param>
        ''' <returns>True si la edad es válida; False en caso contrario.</returns>
        Public Function IsCustomerAgeValid(ageText As String) As Boolean

            If String.IsNullOrWhiteSpace(ageText) Then Return False

            Dim age As Integer

            If Not Integer.TryParse(ageText, age) Then Return False

            Return age >= 5

        End Function


        ''' <summary>
        ''' Valida si una dirección de correo electrónico tiene un formato válido.
        ''' Utiliza una expresión regular para comprobar la estructura básica:
        ''' parte_local@dominio.extensión
        ''' </summary>
        ''' <param name="eMail">Correo electrónico a validar.</param>
        ''' <returns>True si el formato es válido; False en caso contrario.</returns>
        Public Function IsValidEmail(eMail As String) As Boolean

            If String.IsNullOrWhiteSpace(eMail) Then Return False

            Dim strRegex As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"

            Return Regex.IsMatch(eMail, strRegex)

        End Function

    End Module
End Namespace