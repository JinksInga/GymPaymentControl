Namespace Utils
    Public Module DateUtils

        Function CalculateClientAge(ByVal dtDateOfBirth As Date) As Integer

            '|-------------------------------------------------------------
            '| CALCULAR EDAD DEL CLIENTE
            '|--------------------------
            '| * Guardamos la fecha actual en la variable 'dtToday'.
            '| * Restamos el año actual y el año de nacimiento el resultado
            '|   lo guardamos en la variable 'intAge'.
            '| IF : Comprobamos si la fecha de nacimiento es mayor a la fecha
            '|      de hoy (A la fecha de hoy le restamos el valor de la
            '|      variable 'intAge').
            '|      * Si se cumple la condición significa que aún no se ha
            '|        cumplido años y restamos un año a la variable 'intAge'.
            '| * Retornamos la variable 'intAge' con la edad del cliente.

            Dim dtToday As Date = Date.Today
            Dim intAge As Integer = dtToday.Year - dtDateOfBirth.Year

            If dtDateOfBirth.Date > dtToday.AddYears(-intAge).Date Then intAge -= 1

            Return intAge

        End Function
        'Private Function CalcularEdad(fechaNacimiento As DateTime) As Integer

        '    Dim edad = Date.Now.Year - fechaNacimiento.Year
        '    If Date.Now < fechaNacimiento.AddYears(edad) Then edad -= 1
        '    Return edad

        'End Function
        Function ConvertVeryLongDate(ByVal shortDate As Date) As String

            '|---------------------------------------------------------
            '| CONVERTIR FECHA CORTA A FECHA LARGA
            '|------------------------------------
            '| * Le pasamos a la variable 'strDateFormat' el formato de
            '|   la fecha "dd 'de' MMMM 'de' yyyy".
            '| * Convertimos la fecha corta (05/11/1979) al formato largo
            '|   (05 de Noviembre de 1979).
            '| * Descripción del formato "dd de MMMM de yyyy" :
            '|      dd: Día con dos dígitos (05)
            '|      MMMM: Nombre completo del mes (Noviembre)
            '|      yyyy: Año con cuatro dígitos (1979)
            '| * Devolvemos la fecha con el formato largo.
            'Return shortDate.ToString("dd 'de' MMMM 'de' yyyy")

            Dim strDateFormat As String = "dd 'de' MMMM 'de' yyyy"

            Return shortDate.ToString(strDateFormat)

        End Function

        Function ConvertLongDate(longDate As DateTime) As String

            Dim cultureEs = New Globalization.CultureInfo("es-ES")

            Return longDate.ToString("d MMMM yyyy", cultureEs).ToUpper()

        End Function
        ''
        ''
        ''
    End Module
End Namespace