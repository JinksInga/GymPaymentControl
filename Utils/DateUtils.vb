Namespace Utils
    Public Module DateUtils

        ''' <summary>
        ''' Calcula la edad actual del cliente a partir de su fecha de nacimiento.
        ''' </summary>
        ''' <param name="dateOfBirth">
        ''' Fecha de nacimiento del cliente.
        ''' </param>
        ''' <returns>
        ''' Edad cumplida en años.
        ''' </returns>
        Public Function CalculateClientAge(dateOfBirth As Date) As Integer

            Dim today As Date = Date.Today
            Dim age As Integer = today.Year - dateOfBirth.Year

            If dateOfBirth.Date > today.AddYears(-age).Date Then age -= 1

            Return age

        End Function


        ''' <summary>
        ''' Formatea una fecha corta 05/11/1979 a
        ''' una fecha larga 05 de Noviembre de 1979
        ''' </summary>
        ''' <param name="shortDate">
        ''' Fecha a formatear.
        ''' </param>
        Public Function FormatLongDate(shortDate As Date) As String

            Return shortDate.ToString("dd 'de' MMMM 'de' yyyy")

        End Function


        ''' <summary>
        ''' Formatea una fecha utilizando el formato largo en español
        ''' y la convierte a mayúsculas.
        ''' </summary>
        ''' <param name="longDate">
        ''' Fecha a formatear.
        ''' </param>
        Public Function FormatDateUppercase(longDate As DateTime) As String

            Dim cultureEs = New Globalization.CultureInfo("es-ES")

            Return longDate.ToString("d MMMM yyyy", cultureEs).ToUpper()

        End Function


        ''' <summary>
        ''' Determina si una fecha representa un valor no asignado.
        ''' </summary>
        ''' <param name="dateValue">
        ''' Fecha a evaluar.
        ''' </param>
        ''' <returns>
        ''' <c>True</c> si la fecha corresponde a un valor no asignado;
        ''' en caso contrario, <c>False</c>.
        ''' </returns>
        ''' <remarks>
        ''' MySQL puede almacenar fechas no asignadas con el valor
        ''' <c>0000-00-00</c>. Al recuperarlas desde VB.NET mediante un
        ''' DataReader, ese valor se convierte automáticamente en
        ''' <see cref="DateTime.MinValue"/> (01/01/0001). Esta función
        ''' permite detectar dicha situación de forma explícita.
        ''' </remarks>
        Public Function IsDateNotAssigned(dateValue As DateTime) As Boolean

            Return dateValue = DateTime.MinValue

        End Function


    End Module

End Namespace