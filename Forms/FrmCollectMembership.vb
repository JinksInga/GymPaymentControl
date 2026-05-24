Imports GymPaymentControl.Constants
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports MySql.Data.MySqlClient

Public Class FrmCollectMembership
    '
    Private ReadOnly _paymentManager As New PaymentManager()

    ' Propiedad privada del objeto completo
    Private _selectedPayment As IPaymentCalculable

    Private _isUpdatedText As Boolean = False
    Private _isLoading As Boolean = False

    ' Variable para guardar el modo actual
    Private _currentMode As TransactionMode

    ' Definimos los dos estados posibles
    Public Enum TransactionMode
        NewPayment
        UpdatePayment
    End Enum

    '
    Private _originalPrice As Decimal
    Private _originalDiscount As Decimal

    Private Const MARGIN_DAILY As Decimal = 2D
    Private Const MARGIN_MONTHLY_GRUPAL As Decimal = 5D


    Private Sub FrmCollectMembership_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CmbFrmPgs.Text = PaymentForms.Cash
    End Sub
    Private Sub FrmCollectMembership_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub


    Private Sub DtpFdiPgs_ValueChanged(sender As Object, e As EventArgs) Handles DtpFdiPgs.ValueChanged

        ' Filtro de seguridad: si estamos cargando el form o no hay objeto, no hacemos nada
        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

        ' 1. Sincronizamos la nueva fecha del calendario al objeto de negocio
        _selectedPayment.FdiPgs = DtpFdiPgs.Value

        ' Forzamos el recálculo
        ' Esto llamará a tu módulo, recalculará los días restantes del mes, el total prorrateado
        ' y refrescará todas las etiquetas de la pantalla al instante.
        CalculatePrice()

    End Sub


    Private Sub TxtPrcPgs_TextChanged(sender As Object, e As EventArgs) Handles TxtPrcPgs.TextChanged
    End Sub
    Private Sub TxtPrcPgs_GotFocus(sender As Object, e As EventArgs) Handles TxtPrcPgs.GotFocus
        TxtPrcPgs.SelectAll()
    End Sub
    Private Sub TxtPrcPgs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPrcPgs.KeyPress
        AllowDecimalInput(TxtPrcPgs.Text, e)
    End Sub


    Private Sub TxtDscPgs_TextChanged(sender As Object, e As EventArgs) Handles TxtDscPgs.TextChanged
    End Sub
    Private Sub TxtDscPgs_GotFocus(sender As Object, e As EventArgs) Handles TxtDscPgs.GotFocus
        TxtDscPgs.SelectAll()
    End Sub
    Private Sub TxtDscPgs_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDscPgs.KeyPress
        AllowDecimalInput(TxtDscPgs.Text, e)
    End Sub


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
    End Sub
    ''
    ''


    Private Sub ChkFdiPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFdiPgs.CheckedChanged

        ToggleControl(DtpFdiPgs, ChkFdiPgs, ToolTip, "Desactiva la fecha de inicio del mes.", "Activa la fecha de inicio del mes.")

    End Sub
    Private Sub ChkFdpPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFdpPgs.CheckedChanged

        ToggleControl(DtpFdpPgs, ChkFdpPgs, ToolTip, "Desactiva la fecha de pago.", "Activa la fecha de pago.")

    End Sub
    Private Sub ChkMtdPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMtdPgs.CheckedChanged

        ToggleControl(CmbMtdPgs, ChkMtdPgs, ToolTip, "Desactiva el método de pago.", "Activa el método de pago.")

    End Sub


    Private Sub BtnPayMonth_Click(sender As Object, e As EventArgs) Handles BtnConfirmPayment.Click

        ' paymentMethod (BONO, DIARIO, MENSUAL, GRUPO FAMILIAR)
        Dim paymentMethod As String = CmbMtdPgs.Text
        If CmbMtdPgs.Text = PaymentMethods.Daily Then paymentMethod = $"{PaymentMethods.Daily} {CInt(Val(TxtPrcPgs.Text))}"
        If CmbMtdPgs.Text = PaymentMethods.FamilyGroup Then paymentMethod = PaymentMethods.Grupal

        ' 1. Sincronización final: Aseguramos que el clon tenga los valores de la UI
        _selectedPayment.PrcPgs = ParseMoney(TxtPrcPgs.Text) 'Precio
        _selectedPayment.DscPgs = ParseMoney(TxtDscPgs.Text) 'Descuento
        _selectedPayment.FdiPgs = DtpFdiPgs.Value ' Fecha de inicio de mes
        _selectedPayment.FdpPgs = DtpFdpPgs.Value ' Fecha de pago
        _selectedPayment.MtdPgs = paymentMethod ' Método de pago

        ' 2. Validación de existencia (Solo para nuevos pagos)
        If _currentMode = TransactionMode.NewPayment Then

            Dim idClient As Integer? = If(TypeOf _selectedPayment Is IndividualPaymentDTO,
                DirectCast(_selectedPayment, IndividualPaymentDTO).IdCli, CType(Nothing, Integer?))

            Dim idGroup As Integer? = If(TypeOf _selectedPayment Is GroupPaymentDTO,
                DirectCast(_selectedPayment, GroupPaymentDTO).IdGrp, CType(Nothing, Integer?))

            ' 1. Capturamos el método de pago del DTO actual
            Dim metodoPago As String = _selectedPayment.MtdPgs.ToUpper()
            Dim esDaily As Boolean = metodoPago.Contains(PaymentMethods.Daily)
            Dim yaExistePago As Boolean '= False

            Using connection As New MySqlConnection(_paymentManager.ConnectionString)
                connection.Open()
                Dim generator As New PaymentGenerator()

                ' 2. Consultamos si ya existe el pago en la Base de Datos
                yaExistePago = generator.PaymentExists(connection, Nothing, DtpFdiPgs.Value, idClient, idGroup, esDaily)

                ' 3. Si el generador dice que ya existe, preparamos el mensaje según el tipo de pago
                If yaExistePago Then
                    Dim mensajeError As String = If(esDaily,
                        $"Ya existe un pago diario registrado para este cliente el día {DtpFdiPgs.Value.ToShortDateString()}.",
                        "Ya existe un pago registrado para este periodo (Mes/Año).")

                    MessageBox.Show(mensajeError, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If

            End Using
        End If

        ' 4. Guardar usando tu PaymentManager
        Dim monthPaid = _paymentManager.SavePaymentTransaction(_selectedPayment, _currentMode, UserSession.IdUser, CmbFrmPgs.Text)

        If monthPaid Then
            MessageBox.Show("Transacción realizada con éxito", "Pago realizado", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If

    End Sub


    Private Sub BtnCancelPayment_Click(sender As Object, e As EventArgs) Handles BtnCancelPayment.Click
        ' Forzamos el resultado a Cancel y cerramos
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub


    'Creamos un método para "Cargar" el formulario con los datos
    ' Cambiamos el parámetro a la Interfaz
    Public Sub PreparePayment(payment As IPaymentCalculable, mode As TransactionMode)

        _isLoading = True

        _currentMode = mode
        _selectedPayment = payment

        ' Si es el DTO de Grupo (deudores), priorizamos el nombre del grupo.
        If TypeOf _selectedPayment Is GroupPaymentDTO Then
            Dim grpName As String = DirectCast(_selectedPayment, GroupPaymentDTO).GroupName
            LblDisplayName.Text = "GRUPO: " & If(Not String.IsNullOrEmpty(grpName), grpName, "FAMILIAR")
        Else
            ' Si es Individual, Mensual o Diario, nombre del cliente normal.
            LblDisplayName.Text = _selectedPayment.DisplayName
        End If

        ' --- Asignación de Controles ---
        DtpFdiPgs.Value = _selectedPayment.FdiPgs
        TxtPrcPgs.Text = $"{_selectedPayment.PrcPgs} €"
        TxtDscPgs.Text = $"{_selectedPayment.DscPgs} €"

        ' Al cargar los datos del pago en la pantalla:
        _originalPrice = _selectedPayment.PrcPgs
        _originalDiscount = _selectedPayment.DscPgs

        Dim paymentMethod As String = _selectedPayment.MtdPgs

        Select Case True
            Case paymentMethod.Contains(PaymentMethods.Daily)

                CmbMtdPgs.Text = PaymentMethods.Daily
                TxtDetailMethod.Text = "CLASES SUELTAS : Pago por jornada individual."
                TxtDetailMethod.ForeColor = Color.DarkOrange

            Case paymentMethod.Contains(PaymentMethods.Monthly)

                CmbMtdPgs.Text = PaymentMethods.Monthly
                If payment.DscPgs = 0 Then
                    TxtDetailMethod.Text = "TARIFA INDIVIDUAL : Sin Descuento."
                Else
                    TxtDetailMethod.Text = $"TARIFA INDIVIDUAL : Con descuento aplicado por edad {payment.DscPgs:C2}"
                End If
                TxtDetailMethod.ForeColor = Color.RoyalBlue

            Case paymentMethod.Contains(PaymentMethods.Grupal)

                CmbMtdPgs.Text = PaymentMethods.FamilyGroup
                If TypeOf _selectedPayment Is IndividualPaymentDTO Then
                    Dim ind = DirectCast(_selectedPayment, IndividualPaymentDTO)
                    Dim nGrupo = If(Not String.IsNullOrEmpty(ind.GroupName), ind.GroupName, "un grupo familiar")
                    TxtDetailMethod.Text = $"NOTA: Este cobro aplica tarifa reducida por pertenecer a: {nGrupo}."
                Else
                    TxtDetailMethod.Text = "INTEGRANTES : " & _selectedPayment.Members
                End If

                TxtDetailMethod.ForeColor = Color.Indigo

            Case Else
                CmbMtdPgs.SelectedIndex = -1

        End Select

        _isLoading = False

        CalculatePrice()

    End Sub


    Private Sub CalculatePrice()

        ' Si alguna de las cajas está en rojo por error de rango,
        ' abortamos el cálculo para no mostrar datos inconsistentes.
        If TxtPrcPgs.ForeColor = Color.Red OrElse TxtDscPgs.ForeColor = Color.Red Then Exit Sub

        ' Si estamos cargando datos o no hay objeto, NO calcular nada
        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

        ' 1. Sincronizamos los cambios manuales de la UI al objeto
        ' (Quitamos el " €" y convertimos a decimal)
        With _selectedPayment

            .PrcPgs = ParseMoney(TxtPrcPgs.Text)
            .DscPgs = ParseMoney(TxtDscPgs.Text)

            ' Así el objeto siempre tiene la fecha fresca del calendario
            .FdiPgs = DtpFdiPgs.Value

            ' 2. Usamos tu nuevo módulo de utilidad
            CalculatePaymentAmount(_selectedPayment)

            ' 3. Mostramos los resultados calculados en las etiquetas
            LblTotal.Text = .Total.ToString("C2")
            LblNumberOfDays.Text = .DaysOfMonth.ToString()
            LblTotalToPay.Text = .TotalToPay.ToString("C2")

            ' El precio por día lo calculamos al vuelo para la etiqueta si lo necesitas
            Dim priceDay = .Total / DateTime.DaysInMonth(.FdiPgs.Year, .FdiPgs.Month)
            LblPriceDay.Text = priceDay.ToString("C2")
        End With

    End Sub


    Private Sub TxtMoney_TextChanged(sender As Object, e As EventArgs) _
        Handles TxtPrcPgs.TextChanged, TxtDscPgs.TextChanged

        '  Filtro de seguridad
        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

        If sender Is TxtPrcPgs Then
            ' 1. Configuración de márgenes para el PRECIO
            Dim margin As Decimal = MARGIN_MONTHLY_GRUPAL
            'If _selectedPayment.MtdPgs = PaymentMethods.Daily Then
            If _selectedPayment.MtdPgs.Contains(PaymentMethods.Daily) Then
                margin = MARGIN_DAILY
            End If

            ' 2. Usamos la variable fija e inmutable 🎯
            Dim minAllowed As Decimal = Math.Max(0D, _originalPrice - margin)
            Dim maxAllowed As Decimal = _originalPrice + margin

            FormatMoneyTextChanged(TxtPrcPgs, minAllowed, maxAllowed, Color.DarkOrange)

        ElseIf sender Is TxtDscPgs Then
            ' 3. Configuración de márgenes para el DESCUENTO usando su variable fija 🎯
            ' Por ejemplo, permitimos que el descuento varíe un máximo de 5€ arriba o abajo del original, sin bajar de 0
            Dim marginDsc As Decimal = 5D
            Dim minAllowedDsc As Decimal = Math.Max(0D, _originalDiscount - marginDsc)
            Dim maxAllowedDsc As Decimal = _originalDiscount + marginDsc

            FormatMoneyTextChanged(TxtDscPgs, minAllowedDsc, maxAllowedDsc, Color.DarkOrange)
        End If

    End Sub


    Private Sub FormatMoneyTextChanged(textBox As TextBox, minValue As Decimal, maxValue As Decimal, zeroColor As Color)

        ' Desactivamos temporalmente el evento para evitar bucles infinitos al formatear el texto
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
            UpdateLabelsState({LblTotal, LblPriceDay, LblTotalToPay}, Color.Red, "ERROR")
        Else

            If value = 0D Then
                textBox.ForeColor = zeroColor
            Else
                textBox.ForeColor = Color.Green
            End If

            UpdateLabelsState({LblTotal, LblPriceDay}, Color.Green)
            UpdateLabelsState({LblTotalToPay}, Color.Black)

            CalculatePrice()

        End If

        AddHandler textBox.TextChanged, AddressOf TxtMoney_TextChanged

    End Sub


End Class