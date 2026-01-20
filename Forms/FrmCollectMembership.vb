Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Utils.PaymentCalculator
Imports GymPaymentControl.Utils.Validations

Public Class FrmCollectMembership
    '
    'Public strIdCli, strIdPgs As String
    ' Propiedad privada del objeto completo
    Private _selectedPayment As IPaymentCalculable

    Private _isUpdatedText As Boolean = False

    ' Variable para guardar el modo actual
    Private _currentMode As TransactionMode
    ' Definimos los dos estados posibles
    Public Enum TransactionMode
        NewPayment
        UpdatePayment
    End Enum
    ''
    ''
    Private Sub FrmCollectMembership_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SELECCIONA LA PRIMA OPCIÓN DEL COMBOBOX
        CmbFrmPgs.SelectedIndex = 0
    End Sub
    Private Sub FrmCollectMembership_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate

        '| --------------------------------------------------------------------------------------------
        '| CERRAMOS LA VENTANA AL DESACTIVAR EL FORMULARIO 
        '| ------------------------------------------------
        '| * Si se desactiva el Form o se hace clic fuera del Form cerramos el FrmCollectMembership para
        '|   evitar hacer otras acciones con el form ejecutado (no visible).
        Close()

    End Sub
    ''
    ''
    Private Sub DtpFdiPgs_ValueChanged(sender As Object, e As EventArgs) Handles DtpFdiPgs.ValueChanged
        '
    End Sub
    ''
    ''
    Private Sub TxtPrcPgs_TextChanged(sender As Object, e As EventArgs) Handles TxtPrcPgs.TextChanged
    End Sub
    Private Sub TxtPrcPgs_GotFocus(sender As Object, e As EventArgs) Handles TxtPrcPgs.GotFocus
        'SELECCIONA TODO EL TEXTO
        TxtPrcPgs.SelectAll()
    End Sub
    Private Sub TxtPrcPgs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPrcPgs.KeyPress

        '| --------------------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE NÚMEROS DECIMALES CON FORMATO MONEDA
        '| ----------------------------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Llamamos a la subrutina Sub_Only_Numbers y le pasamos la variable como parámetro.
        'Dim strAllowKey As String = "(-) "
        'Sub_Only_Numbers(strAllowKey, e
        ValidateDecimalInput(TxtPrcPgs.Text, e)

    End Sub
    ''
    ''
    Private Sub TxtDscPgs_TextChanged(sender As Object, e As EventArgs) Handles TxtDscPgs.TextChanged
    End Sub
    Private Sub TxtDscPgs_GotFocus(sender As Object, e As EventArgs) Handles TxtDscPgs.GotFocus
        'SELECCIONA TODO EL TEXTO
        TxtDscPgs.SelectAll()
    End Sub
    Private Sub TxtDscPgs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDscPgs.KeyPress

        '| --------------------------------------------------------------------------------------------
        '| VALIDAR EL INGRESO DE NÚMEROS DECIMALES CON FORMATO MONEDA
        '| ----------------------------------------------------------
        '| * Almacenamos en la variable strAllowKey los caracteres que queremos PERMITIR.
        '| * Llamamos a la subrutina Sub_Only_Numbers y le pasamos la variable como parámetro.
        'Dim strAllowKey As String = "(-) "
        'Sub_Only_Numbers(strAllowKey, e
        ValidateDecimalInput(TxtDscPgs.Text, e)

    End Sub
    ''
    ''
    Private Sub DtpFdpPgs_ValueChanged(sender As Object, e As EventArgs) Handles DtpFdpPgs.ValueChanged
        '
    End Sub
    ''
    ''
    Private Sub CmbFrmPgs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFrmPgs.SelectedIndexChanged
        '
    End Sub
    ''
    ''
    Private Sub CmbMtdPgs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbMtdPgs.SelectedIndexChanged

        '
        '|
        '|

        Dim strDetail As String

        Select Case CmbMtdPgs.SelectedIndex
            Case 0 'BONO
                strDetail = "Cuota mensual +" & vbCrLf & "guantes +" & vbCrLf & "vendas"

            Case 1 'DIARIO
                strDetail = "Pago diario, " & TxtPrcPgs.Text & " por cada clase suelta."

            Case 2 'MENSAL
                strDetail = "Con descuento de " & TxtDscPgs.Text & " por edad."

            Case Else 'GRUPO FAMILIAR
                strDetail = "Jinkis" & vbCrLf & "Sarita" & vbCrLf & "Marjorie"

        End Select

        TxtDtlleMtdo.Text = strDetail

    End Sub
    ''
    ''
    Private Sub BtnPayMonth_Click(sender As Object, e As EventArgs) Handles BtnPayMonth.Click
        '
    End Sub
    ''
    ''
    Private Sub BtnCancelPayment_Click(sender As Object, e As EventArgs) Handles BtnCancelPayment.Click
        Close() 'CERRAR FORM
    End Sub
    ''
    ''
    'Creamos un método para "Cargar" el formulario con los datos
    ' Cambiamos el parámetro a la Interfaz
    Public Sub PreparePayment(payment As IPaymentCalculable, mode As TransactionMode)

        _selectedPayment = payment ' _selectedPayment debe ser de tipo IPaymentCalculable
        _currentMode = mode

        ' Configuramos la UI según el modo
        If _currentMode = TransactionMode.NewPayment Then
            Me.Text = "Nuevo pago mensual"
            BtnPayMonth.Text = "Registrar Pago"
        Else
            Me.Text = "Cobrar mes pendiente"
            BtnPayMonth.Text = "Confirmar Cobro"
        End If

        With _selectedPayment
            ' Usamos la propiedad DisplayName que definiremos en los DTOs
            LblCliente.Text = .DisplayName
            DtpFdiPgs.Value = .FdiPgs

            ' Usamos los valores numéricos; el TextChanged se encargará del " €"
            TxtPrcPgs.Text = $"{ .PrcPgs} €" '.PrcPgs.ToString()
            TxtDscPgs.Text = $"{ .DscPgs} €" '.DscPgs.ToString()
        End With

        ' Llamamos a tu lógica de prorrateo
        CalculateProratedPayment(_selectedPayment)
    End Sub

    Private Sub ChangeFontError()
        '
        LblTotal.Text = "ERROR"
        LblTotal.ForeColor = Color.Red
        LblPriceDay.Text = "ERROR"
        LblPriceDay.ForeColor = Color.Red
        LblTotalToPay.Text = "ERROR"
        LblTotalToPay.ForeColor = Color.Red
        'LblTtlPgs.Font = New System.Drawing.Font(LblTtlPgs.Font, FontStyle.Bold)
        'LblPrcDisPgs.Font = New System.Drawing.Font(LblPrcDisPgs.Font, FontStyle.Bold)
        'LblPagarPgs.Font = New System.Drawing.Font(LblPagarPgs.Font, FontStyle.Bold)
    End Sub

    Private Sub ChangeFontOk()
        '
        LblTotal.ForeColor = Color.Green
        LblPriceDay.ForeColor = Color.Green
        LblTotalToPay.ForeColor = Color.Black
        'LblTtlPgs.Font = New System.Drawing.Font(LblPrcPgs.Font, LblTtlPgs.Font.Style And Not FontStyle.Bold)
        'LblPrcDisPgs.Font = New System.Drawing.Font(LblPrcDisPgs.Font, LblPrcPgs.Font.Style And Not FontStyle.Bold)
        'LblPagarPgs.Font = New System.Drawing.Font(LblPagarPgs.Font, LblPrcPgs.Font.Style And Not FontStyle.Bold)
    End Sub

    Private Sub CalculatePrice()

        If _selectedPayment Is Nothing Then Exit Sub

        'Try
        ' 1. Sincronizamos los cambios manuales de la UI al objeto
        ' (Quitamos el " €" y convertimos a decimal)
        With _selectedPayment

            .PrcPgs = ParseMoney(TxtPrcPgs.Text)
            .DscPgs = ParseMoney(TxtDscPgs.Text)

            ' 2. Usamos tu nuevo módulo de utilidad
            CalculateProratedPayment(_selectedPayment)

            ' 3. Mostramos los resultados calculados en las etiquetas
            LblTotal.Text = .Total.ToString("C2")
            LblNumberOfDays.Text = .DaysOfMonth.ToString()
            LblTotalToPay.Text = .TotalToPay.ToString("C2")

            ' El precio por día lo calculamos al vuelo para la etiqueta si lo necesitas
            Dim priceDay = .Total / DateTime.DaysInMonth(.FdiPgs.Year, .FdiPgs.Month)
            LblPriceDay.Text = priceDay.ToString("C2")
        End With

    End Sub

    Private Function ParseMoney(text As String) As Decimal
        Dim clean = text.Replace("€", "").Trim()

        Dim value As Decimal
        If Decimal.TryParse(clean, value) Then
            Return value
        End If

        Return 0D
    End Function

    Private Sub FormatMoneyTextChanged(textBox As TextBox, minValue As Decimal, maxValue As Decimal, zeroColor As Color)

        RemoveHandler textBox.TextChanged, AddressOf TxtMoney_TextChanged

        Dim cursorPos = textBox.SelectionStart
        Dim raw = textBox.Text.Replace("€", "").Trim()
        raw = raw.Replace(".", ",")

        Dim value As Decimal
        Dim isValid = Decimal.TryParse(raw, value)

        textBox.Text = $"{raw} €"
        textBox.SelectionStart = Math.Min(cursorPos, textBox.Text.Length)

        If Not isValid OrElse value < minValue OrElse value > maxValue Then
            textBox.ForeColor = Color.Red
            ChangeFontError()
        Else

            If value = 0D Then
                textBox.ForeColor = zeroColor
            Else
                textBox.ForeColor = Color.Green
            End If

            ChangeFontOk()
            CalculatePrice()

        End If

        AddHandler textBox.TextChanged, AddressOf TxtMoney_TextChanged

    End Sub


    Private Sub TxtMoney_TextChanged(sender As Object, e As EventArgs) _
        Handles TxtPrcPgs.TextChanged, TxtDscPgs.TextChanged

        If sender Is TxtPrcPgs Then
            FormatMoneyTextChanged(TxtPrcPgs, 30D, 90D, Color.DarkOrange)

        ElseIf sender Is TxtDscPgs Then
            FormatMoneyTextChanged(TxtDscPgs, 0D, 25D, Color.DarkOrange)

        End If

    End Sub
    ''
    ''
End Class