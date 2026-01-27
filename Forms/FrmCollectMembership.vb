Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.Utils.PaymentCalculator
Imports GymPaymentControl.Utils.Validations
Imports MySql.Data.MySqlClient

Public Class FrmCollectMembership
    '
    Private ReadOnly _paymentManager As New PaymentManager()

    ' Propiedad privada del objeto completo
    Private _selectedPayment As IPaymentCalculable

    ' Variable para guardar el modo actual
    Private _currentMode As TransactionMode

    ' Definimos los dos estados posibles
    Public Enum TransactionMode
        NewPayment
        UpdatePayment
    End Enum

    Private _isUpdatedText As Boolean = False
    Private _isLoading As Boolean = False

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
        Me.Close()

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

        'Dim strDetail As String

        'Select Case CmbMtdPgs.SelectedIndex
        '    Case 0 'BONO
        '        strDetail = "Cuota mensual +" & vbCrLf & "guantes +" & vbCrLf & "vendas"

        '    Case 1 'DIARIO
        '        strDetail = "Pago diario, " & TxtPrcPgs.Text & " por cada clase suelta."

        '    Case 2 'MENSAL
        '        strDetail = "Con descuento de " & TxtDscPgs.Text & " por edad."

        '    Case Else 'GRUPO FAMILIAR
        '        strDetail = "Jinkis" & vbCrLf & "Sarita" & vbCrLf & "Marjorie"

        'End Select

        'TxtDetailMethod.Text = strDetail

    End Sub
    ''
    ''
    Private Sub BtnPayMonth_Click(sender As Object, e As EventArgs) Handles BtnConfirmPayment.Click
        '
        ' 1. Sincronización final: Aseguramos que el clon tenga los valores de la UI
        _selectedPayment.PrcPgs = ParseMoney(TxtPrcPgs.Text)
        _selectedPayment.DscPgs = ParseMoney(TxtDscPgs.Text)
        _selectedPayment.FdiPgs = DtpFdiPgs.Value
        _selectedPayment.FdpPgs = DtpFdpPgs.Value ' <--- Respetamos tu DTP manual

        ' 2. Validación de existencia (Solo para nuevos pagos)
        If _currentMode = TransactionMode.NewPayment Then

            Dim idClient As Integer? = If(TypeOf _selectedPayment Is IndividualPaymentDTO,
                DirectCast(_selectedPayment, IndividualPaymentDTO).IdCli, CType(Nothing, Integer?))

            Dim idGroup As Integer? = If(TypeOf _selectedPayment Is GroupPaymentDTO,
                DirectCast(_selectedPayment, GroupPaymentDTO).IdGrp, CType(Nothing, Integer?))

            Using connection As New MySqlConnection(_paymentManager.ConnectionString)

                connection.Open()

                Dim generator As New PaymentGenerator()
                If generator.PaymentExists(connection, Nothing, DtpFdiPgs.Value, idClient, idGroup) Then
                    MessageBox.Show("Ya existe un pago registrado para este periodo (Mes/Año).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

            End Using
        End If

        ' 3. Recalcular totales finales antes de guardar para asegurar coherencia
        'PaymentCalculator.CalculateProratedPayment(_selectedPayment)

        ' 4. Guardar usando tu PaymentManager
        Dim monthPaid = _paymentManager.SaveTransaction(_selectedPayment, _currentMode,
                                                        UserSession.IdUser, CmbFrmPgs.Text)

        If monthPaid Then
            MessageBox.Show("Transacción realizada con éxito", "Pago realizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ' Al cerrar con OK, el formulario padre refrescará la lista desde la BD
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub
    ''
    ''
    Private Sub BtnCancelPayment_Click(sender As Object, e As EventArgs) Handles BtnCancelPayment.Click
        ' Forzamos el resultado a Cancel y cerramos
        Me.DialogResult = DialogResult.Cancel
        Me.Close() 'CERRAR FORM

    End Sub
    ''
    ''
    'Creamos un método para "Cargar" el formulario con los datos
    ' Cambiamos el parámetro a la Interfaz
    Public Sub PreparePayment(payment As IPaymentCalculable, mode As TransactionMode)

        _isLoading = True ' <-- BLOQUEO

        _selectedPayment = payment ' _selectedPayment debe ser de tipo IPaymentCalculable
        _currentMode = mode

        ' Configuramos la UI según el modo
        'If _currentMode = TransactionMode.NewPayment Then
        '    Me.Text = "NUEVO PAGO MENSUAL"
        '    BtnConfirmPayment.Text = "Registrar Pago"
        'Else
        '    Me.Text = "COBRAR MES PENDIENTE"
        '    BtnConfirmPayment.Text = "Confirmar Cobro"
        'End If

        'With _selectedPayment
        ' Usamos la propiedad DisplayName que definiremos en los DTOs
        LblDisplayName.Text = _selectedPayment.DisplayName
        DtpFdiPgs.Value = _selectedPayment.FdiPgs

        ' Usamos los valores numéricos; el TextChanged se encargará del " €"
        TxtPrcPgs.Text = $"{ _selectedPayment.PrcPgs} €"
        TxtDscPgs.Text = $"{ _selectedPayment.DscPgs} €"

        Dim strMtdPgs As String = _selectedPayment.MtdPgs 'Método de pago (DIARIO / MENSUAL / GRUPAL)

        Select Case True
            Case strMtdPgs.Contains("DIARIO")
                CmbMtdPgs.Text = "DIARIO"
                TxtDetailMethod.Text = "CLASES SUELTAS : Pago por jornada individual."
                TxtDetailMethod.ForeColor = Color.DarkOrange

            Case strMtdPgs.Contains("MENSUAL")
                CmbMtdPgs.Text = "MENSUAL"

                If payment.DscPgs = 0 Then
                    TxtDetailMethod.Text = "TARIFA INDIVIDUAL : Sin Descuento."
                Else
                    TxtDetailMethod.Text = $"TARIFA INDIVIDUAL : Con descuento aplicado por edad {payment.DscPgs:C2}"
                End If
                TxtDetailMethod.ForeColor = Color.RoyalBlue

            Case strMtdPgs.Contains("GRUPAL")
                CmbMtdPgs.Text = "GRUPO FAMILIAR"

                If TypeOf payment Is GroupPaymentDTO Then
                    TxtDetailMethod.Text = "INTEGRANTES : " & DirectCast(payment, GroupPaymentDTO).Members
                End If
                TxtDetailMethod.ForeColor = Color.Indigo

                'Case strMtdPgs.Contains("BONO")
                '    CmbMtdPgs.Text = "BONO"

            Case Else
                CmbMtdPgs.SelectedIndex = -1

        End Select

        _isLoading = False ' <-- LIBERACIÓN

        ' Llamamos a tu lógica de prorrateo
        ''CalculateProratedPayment(_selectedPayment)
        ' Ahora que todo está cargado, forzamos un único cálculo real
        CalculatePrice()

    End Sub

    Private Sub ChangeFontError()
        '
        LblTotal.Text = "ERROR"
        LblTotal.ForeColor = Color.Red
        LblPriceDay.Text = "ERROR"
        LblPriceDay.ForeColor = Color.Red
        LblTotalToPay.Text = "ERROR"
        LblTotalToPay.ForeColor = Color.Red

    End Sub

    Private Sub ChangeFontOk()
        '
        LblTotal.ForeColor = Color.Green
        LblPriceDay.ForeColor = Color.Green
        LblTotalToPay.ForeColor = Color.Black

    End Sub

    Private Sub CalculatePrice()

        'If _selectedPayment Is Nothing Then Exit Sub
        ' Si estamos cargando datos o no hay objeto, NO calcular nada
        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

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

    Private Sub ChkFdiPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFdiPgs.CheckedChanged

        '
        OnOffCheckBox(DtpFdiPgs, ChkFdiPgs, "Desactiva la fecha de inicio del mes.", "Activa la fecha de inicio del mes.")

    End Sub

    Private Sub ChkFdpPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFdpPgs.CheckedChanged

        '
        OnOffCheckBox(DtpFdpPgs, ChkFdpPgs, "Desactiva la fecha de pago.", "Activa la fecha de pago.")

    End Sub

    Private Sub ChkMtdPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMtdPgs.CheckedChanged

        '
        OnOffCheckBox(CmbMtdPgs, ChkMtdPgs, "Desactiva el método de pago", "Activa el método de pago")

    End Sub

    Private Sub OnOffCheckBox(control As Control, checkBox As CheckBox,
                              strDeactivate As String, strActivate As String)

        control.Enabled = checkBox.Checked

        If checkBox.Checked Then

            control.Focus()
            ToolTip.SetToolTip(checkBox, strDeactivate)
        Else
            ToolTip.SetToolTip(checkBox, strActivate)
        End If

    End Sub
    ''
    ''
End Class