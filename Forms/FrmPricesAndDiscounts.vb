Imports System.ComponentModel
Imports GymPaymentControl.Constants
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

Public Class FrmPricesAndDiscounts

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Componentes de Negocio y Reglas Fijas ---
    Private ReadOnly _tariffManager As New TariffManager()

    Private Const MINIMUM_AGE_FOR_DISCOUNT As Decimal = 5D
    Private Const MINIMUM_PRICE_LIMIT As Decimal = 10D
    Private Const MAXIMUM_PRICE_LIMIT As Decimal = 90D

    ' --- Reglas de Porcentajes para Clases Sueltas ---
    Private Const INDIVIDUAL_CLASS_MIN_PCT As Decimal = 0.1D ' 10% del precio base
    Private Const INDIVIDUAL_CLASS_MAX_PCT As Decimal = 0.3D ' 30% del precio base

    ' --- Reglas de Porcentajes para Descuento por Edad ---
    Private Const AGE_DISCOUNT_MIN_PCT As Decimal = 0.1D ' 10% mínimo de descuento
    Private Const AGE_DISCOUNT_MAX_PCT As Decimal = 0.4D ' 40% máximo de descuento

    ' --- Reglas de Porcentajes para Grupo Familiar ---
    Private Const FAMILY_GROUP_MIN_PCT As Decimal = 0.05D ' 5% mínimo por miembro
    Private Const FAMILY_GROUP_MAX_PCT As Decimal = 0.25D ' 25% máximo por miembro

    ' --- Control de Flujo y Modos de Pantalla ---
    Private _currentMode As TransactionMode?
    Private _selectedTariffId As Integer
    Private _currentTariffId As Integer

    Private _isCalculating As Boolean

    ' --- Variables de Validación (Estado del Botón Guardar) ---
    Private _isPriceValid As Boolean
    Private _isDiscountValid As Boolean
    Private _isToPayValid As Boolean
    Private _isNumberMembersValid As Boolean
    Private _isMinimumAgeValid As Boolean
    Private _isMaximumAgeValid As Boolean

    ' --- Valores Económicos de la Tarifa Activa ---
    Private _currentPrice As Decimal
    Private _currentDiscount As Decimal
    Private _currentToPay As Decimal
    Private _fixedMonthlyPrice As Decimal

    ' --- Límites Comerciales Permitidos (Min / Max) ---
    Private _allowedPriceMin As Decimal, _allowedPriceMax As Decimal
    Private _allowedDiscountMin As Decimal, _allowedDiscountMax As Decimal
    Private _allowedToPayMin As Decimal, _allowedToPayMax As Decimal

    ' --- Valores Temporales de Validación (Snapshots) ---
    Private _tempAgeMin As Integer
    Private _tempAgeMax As Integer
    Private _tempDiscount As Decimal

    ' --- Avisamos cuántas tarifas quedan al cerrar ---
    Public Event TariffClosingValidation(sender As Object, totalRows As Integer)

    ' --- Parámetros de Apertura desde Otros Formularios ---
    Public Property IsGroupRateRequest As Boolean
    Public Property SuggestedNumberMembers As Integer?

#End Region

#Region " EVENTOS DEL FORMULARIO (Handlers) "
    ' Los disparadores nativos de los componentes de Windows Forms.

    Private Sub FrmPricesAndDiscounts_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        ' Lanzamos el grito al aire (RaiseEvent) pasando la cantidad de filas actuales
        RaiseEvent TariffClosingValidation(Me, DgvPriceList.RowCount)
    End Sub
    Private Sub FrmPricesAndDiscounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FetchAndRenderTariffsGridUI()

        SetInterfaceVisualState(isEditing:=False)

        ' Quitamos la fila seleccionada al final de la cola de mensajes de Windows.
        ' Se ejecutará de forma invisible e instantánea justo al terminar de pintarse.
        BeginInvoke(Sub()
                        If String.IsNullOrEmpty(LblPaymentMethod.Text.Trim()) Then
                            DgvPriceList.CurrentCell = Nothing
                        End If
                    End Sub)
    End Sub


    Private Sub CmbPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPaymentMethod.SelectedIndexChanged

        ' Reseteo y limpieza base, ante cualquier cambio de selección.
        ClearInputControls()

        ResetStateVariables()

        DisableInputControls()

        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.IndividualClasses
                ConfigureDirectPriceTariffUI(PaymentMethods.Daily, PaymentMethods.IndividualClasses)

            Case PaymentMethods.AgeDiscount
                ConfigureAgeDiscountUI()

            Case PaymentMethods.FamilyGroup
                ConfigureFamilyGroupTariffUI()

            Case PaymentMethods.MonthlyFeeSupplies
                ConfigureDirectPriceTariffUI(PaymentMethods.MonthImp, PaymentMethods.MonthlyFeeSupplies)

            Case Else
                BtnSaveRate.Enabled = False

        End Select

    End Sub


    Private Sub TxtPrice_TextChanged(sender As Object, e As EventArgs) Handles TxtPrice.TextChanged

        ' Private _currentMode As TransactionMode?
        If _currentMode Is Nothing OrElse _isCalculating Then Exit Sub

        ' Encendemos el búnker global (nadie más puede calcular mientras yo trabaje)
        _isCalculating = True

        Try
            Dim cleanText As String = NormalizeMoneyText(TxtPrice.Text)

            ApplyMoneyTextboxFormat(TxtPrice)

            ' En el caso de clases sueltas y mensualidad + implementos, copiamos el precio ingresado.
            TxtTotal.Text = TxtPrice.Text
            TxtToPay.Text = TxtPrice.Text

            ' Parseo numérico seguro
            Dim currentPriceValue As Decimal
            Dim isDecimalValid As Boolean = Decimal.TryParse(cleanText, currentPriceValue)

            ' Calcula los límites dinámicos (_allowedPriceMin y _allowedPriceMax)
            CalculatePriceLimits()

            ' Actualiza los Labels informativos.
            UpdateDynamicTariffLabel()

            ' El descuento solo es válido si es un número real Y ADEMÁS está dentro de los límites.
            _isPriceValid = isDecimalValid AndAlso
                            EvaluateNumericRangeLimits(TxtPrice, currentPriceValue, _allowedPriceMin, _allowedPriceMax)

            ' El botón solo se encenderá si la tarifa es correcta.
            Dim isFormValid As Boolean = IsTariffConfigurationValid()
            BtnSaveRate.Enabled = isFormValid
            BtnUpdateRate.Enabled = isFormValid

            ' Si el descuento es correcto, guardamos el número limpio en la variable de estado.
            If _isPriceValid Then _currentPrice = currentPriceValue

        Finally
            ' Abrimos el cerrojo para el próximo tecleo.
            _isCalculating = False
        End Try

    End Sub
    Private Sub TxtPrice_Enter(sender As Object, e As EventArgs) Handles TxtPrice.Enter
        ' Selecciona automáticamente todo el texto al recibir el enfoque
        TxtPrice.SelectAll()
    End Sub
    Private Sub TxtPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPrice.KeyPress
        ' Restringimos la entrada del teclado utilizando tu helper estándar de números
        AllowDecimalInput(TxtPrice, e)
    End Sub


    Private Sub TxtDiscount_TextChanged(sender As Object, e As EventArgs) Handles TxtDiscount.TextChanged

        ' Private _currentMode As TransactionMode?
        If _currentMode Is Nothing OrElse _isCalculating Then Exit Sub

        _isCalculating = True

        Try
            Dim cleanDiscountText As String = NormalizeMoneyText(TxtDiscount.Text)

            ApplyMoneyTextboxFormat(TxtDiscount)

            ' Parseo numérico seguro
            Dim currentDiscountValue As Decimal
            Dim isDecimalValid As Boolean = Decimal.TryParse(cleanDiscountText, currentDiscountValue)

            ' Calculamos los límites Minimo y Maximo del descuento.
            CalculateDiscountLimits()

            ' Calcula el total a pagar según el tipo de pago
            UpdateDiscountCalculationsAndTotals(currentDiscountValue)

            ' Actualizamos los límites comerciales permitidos para el TOTAL A PAGAR
            CalculateToPayLimits()

            ' El precio solo es válido si es un número real Y ADEMÁS está dentro de los límites.
            _isDiscountValid = isDecimalValid AndAlso
                               EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue, _allowedDiscountMin, _allowedDiscountMax)

            Dim cleanToPayText As String = NormalizeMoneyText(TxtToPay.Text)
            Dim currentToPayValue As Decimal
            Decimal.TryParse(cleanToPayText, currentToPayValue)
            _isToPayValid = EvaluateNumericRangeLimits(TxtToPay, currentToPayValue, _allowedToPayMin, _allowedToPayMax)

            ' El botón solo se encenderá si la tarifa es correcta.
            Dim isFormValid As Boolean = IsTariffConfigurationValid()
            BtnSaveRate.Enabled = isFormValid
            BtnUpdateRate.Enabled = isFormValid

            ' Si el descuento es correcto, guardamos el número limpio en la variable de estado.
            If _isDiscountValid Then _currentDiscount = currentDiscountValue

        Finally
            ' Abrimos el cerrojo para el próximo tecleo.
            _isCalculating = False
        End Try

    End Sub
    Private Sub TxtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDiscount.KeyPress
        AllowDecimalInput(sender, e)
    End Sub
    Private Sub TxtDiscount_Enter(sender As Object, e As EventArgs) Handles TxtDiscount.Enter
        TxtDiscount.SelectAll()
    End Sub


    Private Sub TxtToPay_TextChanged(sender As Object, e As EventArgs) Handles TxtToPay.TextChanged

        ' Private _currentMode As TransactionMode?
        If _currentMode Is Nothing OrElse _isCalculating Then Exit Sub

        _isCalculating = True

        Try
            Dim cleanToPayText As String = NormalizeMoneyText(TxtToPay.Text)

            ApplyMoneyTextboxFormat(TxtToPay)

            ' Parseo numérico seguro
            Dim currentToPayValue As Decimal
            Dim isDecimalValid As Boolean = Decimal.TryParse(cleanToPayText, currentToPayValue)

            ' Calculamos los límites inversos y el descuento resultante
            CalculateToPayLimits()

            ' Calcula el total a pagar según el tipo de pago
            UpdateToPayCalculationsAndDiscounts(currentToPayValue)

            ' Evaluamos los rangos comerciales permitidos para el total a pagar
            _isToPayValid = isDecimalValid AndAlso
                            EvaluateNumericRangeLimits(TxtToPay, currentToPayValue, _allowedToPayMin, _allowedToPayMax)

            Dim cleanDiscountText As String = NormalizeMoneyText(TxtDiscount.Text)
            Dim currentDiscountValue As Decimal
            Decimal.TryParse(cleanDiscountText, currentDiscountValue)
            _isDiscountValid = EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue,
                                                          _allowedDiscountMin, _allowedDiscountMax)

            ' El botón solo se encenderá si la tarifa es correcta.
            Dim isFormValid As Boolean = IsTariffConfigurationValid()
            BtnSaveRate.Enabled = isFormValid
            BtnUpdateRate.Enabled = isFormValid

            ' uardamos el número limpio en la variable de estado
            If _isToPayValid Then _currentToPay = currentToPayValue

        Finally
            ' Abrimos el cerrojo para el próximo tecleo
            _isCalculating = False
        End Try

    End Sub
    Private Sub TxtToPay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtToPay.KeyPress
        AllowDecimalInput(sender, e)
    End Sub
    Private Sub TxtToPay_Enter(sender As Object, e As EventArgs) Handles TxtToPay.Enter
        TxtToPay.SelectAll()
    End Sub


    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged

        ' Private _currentMode As TransactionMode?
        If _currentMode Is Nothing OrElse _isCalculating Then Exit Sub

        _isCalculating = True

        Try
            Dim paymentMethod As String = GetNamePaymentMethod()

            If paymentMethod = PaymentMethods.FamilyGroup Then

                ' Evaluamos el control, pintamos en tiempo real y actualizamos su bandera de estado.
                _isNumberMembersValid = EvaluateNumericRangeLimits(NudNumberMembers, NudNumberMembers.Value,
                                                                   NudNumberMembers.Minimum, NudNumberMembers.Maximum)
                UpdateDynamicTariffLabel()

                Dim cleanText As String = NormalizeMoneyText(TxtDiscount.Text)
                Dim currentDiscountValue As Decimal
                Decimal.TryParse(cleanText, currentDiscountValue)

                CalculateDiscountLimits()
                UpdateDiscountCalculationsAndTotals(currentDiscountValue)
                CalculateToPayLimits()

                ' Comprobamos si el descuento sigue siendo válido con los nuevos límites de personas
                _isDiscountValid = EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue,
                                                              _allowedDiscountMin, _allowedDiscountMax)

                ' El botón solo se encenderá si la tarifa es correcta.
                Dim isFormValid As Boolean = IsTariffConfigurationValid()
                BtnSaveRate.Enabled = isFormValid
                BtnUpdateRate.Enabled = isFormValid

            End If

        Finally
            _isCalculating = False
        End Try

    End Sub
    Private Sub NudNumberMembers_Enter(sender As Object, e As EventArgs) Handles NudNumberMembers.Enter
        NudNumberMembers.Select(0, NudNumberMembers.Text.Length)
    End Sub
    Private Sub NudNumberMembers_KeyUp(sender As Object, e As KeyEventArgs) Handles NudNumberMembers.KeyUp

        Dim typedValue As Decimal

        Decimal.TryParse(NormalizeMoneyText(NudNumberMembers.Text), typedValue)

        _isNumberMembersValid = EvaluateNumericRangeLimits(NudNumberMembers, typedValue,
                                                           NudNumberMembers.Minimum, NudNumberMembers.Maximum)

        If _isNumberMembersValid Then NudNumberMembers.Value = typedValue

        Dim isFormValid As Boolean = IsTariffConfigurationValid()
        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub


    Private Sub NudMinimumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMinimumAge.ValueChanged

        If _currentMode Is Nothing Then Exit Sub

        Dim paymentMethod As String = GetNamePaymentMethod()

        If paymentMethod = PaymentMethods.AgeDiscount Then UpdateDynamicTariffLabel()

        _isMinimumAgeValid = EvaluateNumericRangeLimits(NudMinimumAge, NudMinimumAge.Value,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMinimumAge.Maximum)

        ValidateAgeRangeCoherence()

        Dim isFormValid As Boolean = IsTariffConfigurationValid()
        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub
    Private Sub NudMinimumAge_Enter(sender As Object, e As EventArgs) Handles NudMinimumAge.Enter
        NudMinimumAge.Select(0, NudMinimumAge.Text.Length)
    End Sub
    Private Sub NudMinimumAge_KeyUp(sender As Object, e As KeyEventArgs) Handles NudMinimumAge.KeyUp

        Dim isFormValid As Boolean = IsTariffConfigurationValid()

        If String.IsNullOrWhiteSpace(NudMinimumAge.Text) Then

            NudMinimumAge.ForeColor = Color.Red
            NudMinimumAge.Font = New System.Drawing.Font(NudMinimumAge.Font, FontStyle.Bold)
            _isMinimumAgeValid = False

            BtnSaveRate.Enabled = isFormValid
            BtnUpdateRate.Enabled = isFormValid
            Exit Sub

        End If

        Dim typedValue As Decimal
        Decimal.TryParse(NormalizeMoneyText(NudMinimumAge.Text), typedValue)

        _isMinimumAgeValid = EvaluateNumericRangeLimits(NudMinimumAge, typedValue,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMinimumAge.Maximum)

        If _isMinimumAgeValid Then NudMinimumAge.Value = typedValue

        ValidateAgeRangeCoherence()

        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub


    Private Sub NudMaximumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMaximumAge.ValueChanged

        If _currentMode Is Nothing Then Exit Sub

        Dim paymentMethod As String = GetNamePaymentMethod()

        If paymentMethod = PaymentMethods.AgeDiscount Then UpdateDynamicTariffLabel()

        _isMaximumAgeValid = EvaluateNumericRangeLimits(NudMaximumAge, NudMaximumAge.Value,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMaximumAge.Maximum)

        ValidateAgeRangeCoherence()

        Dim isFormValid As Boolean = IsTariffConfigurationValid()
        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub
    Private Sub NudMaximumAge_Enter(sender As Object, e As EventArgs) Handles NudMaximumAge.Enter
        NudMaximumAge.Select(0, NudMaximumAge.Text.Length)
    End Sub
    Private Sub NudMaximumAge_KeyUp(sender As Object, e As KeyEventArgs) Handles NudMaximumAge.KeyUp

        Dim isFormValid As Boolean = IsTariffConfigurationValid()

        If String.IsNullOrWhiteSpace(NudMaximumAge.Text) Then

            NudMaximumAge.ForeColor = Color.Red
            NudMaximumAge.Font = New System.Drawing.Font(NudMaximumAge.Font, FontStyle.Bold)
            _isMaximumAgeValid = False

            BtnSaveRate.Enabled = isFormValid
            BtnUpdateRate.Enabled = isFormValid

            Exit Sub

        End If

        Dim typedValue As Decimal
        Decimal.TryParse(NormalizeMoneyText(NudMaximumAge.Text), typedValue)

        _isMaximumAgeValid = EvaluateNumericRangeLimits(NudMaximumAge, typedValue,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMaximumAge.Maximum)

        If _isMaximumAgeValid Then NudMaximumAge.Value = typedValue

        ValidateAgeRangeCoherence()

        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub


    Private Sub DgvPriceList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPriceList.CellContentClick
    End Sub
    Private Sub DgvPriceList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPriceList.CellClick

        If e.RowIndex < 0 Then Exit Sub

        _isCalculating = True

        Try
            Dim selectedTariff = DirectCast(DgvPriceList.CurrentRow.DataBoundItem, TariffDTO)

            ' Captura de identificadores y estado de la tarifa seleccionada desde las propiedades del objeto
            _currentTariffId = selectedTariff.IdTariff
            _selectedTariffId = _currentTariffId
            _currentPrice = selectedTariff.Price
            _currentDiscount = selectedTariff.Discount

            ' Actualizamos los cuadros de texto con la info de la tarifa.
            TxtPrice.Text = selectedTariff.Price.ToString("C2")
            TxtTotal.Text = selectedTariff.Total.ToString("C2")
            TxtDiscount.Text = selectedTariff.Discount.ToString("C2")
            TxtToPay.Text = selectedTariff.TotalToPay.ToString("C2")

            LblPaymentMethod.Text = selectedTariff.PaymentMethod.ToString().Trim()
            NudMinimumAge.Value = selectedTariff.MinimumAge
            NudMaximumAge.Value = selectedTariff.MaximumAge
            NudNumberMembers.Value = selectedTariff.NumberMembers

        Catch ex As Exception
            MessageBox.Show($"ERROR AL SELECCIONAR : {vbCrLf}{ex.Message}")
        Finally
            _isCalculating = False
        End Try

    End Sub


    Private Sub BtnNewRate_Click(sender As Object, e As EventArgs) Handles BtnNewRate.Click

        _currentMode = TransactionMode.NewRecord

        ClearInputControls()

        ResetStateVariables()

        SetInterfaceVisualState(isEditing:=True)

        DgvPriceList.CurrentCell = Nothing

        CmbPaymentMethod.Focus()

        If DgvPriceList.RowCount = 0 Then ApplyFirstTariffDefaultRules()

    End Sub


    Private Sub BtnSaveRate_Click(sender As Object, e As EventArgs) Handles BtnSaveRate.Click

        '  DETECCIÓN DE DUPLICADOS EN EL DATAGRIDVIEW
        If FindAndSelectRowByName(LblPaymentMethod.Text) Then
            MessageBox.Show(DuplicatedTariffNameWarning("GUARDAR la nueva", LblPaymentMethod.Text),
                            "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtPrice.Focus()
            Exit Sub
        End If

        ' GUARDAR USANDO DTO Y TARIFFMANAGER
        Try
            Dim newTariffDto As New TariffDTO() With
                {
                    .IdTariff = _currentTariffId,
                    .PaymentMethod = LblPaymentMethod.Text,
                    .Price = _currentPrice,
                    .MinimumAge = CInt(NudMinimumAge.Value),
                    .MaximumAge = CInt(NudMaximumAge.Value),
                    .NumberMembers = CInt(NudNumberMembers.Value),
                    .Discount = _currentDiscount
                }

            _currentTariffId = _tariffManager.UpsertTariff(newTariffDto)

        Catch ex As Exception
            MsgBox($"ERROR AL GUARDAR : {vbCrLf}{ex.Message}")
            Exit Sub
        End Try

        Dim paymentMethod As String = GetNamePaymentMethod()
        Dim messageBody As String = String.Empty

        ' CONSTRUIR EL CUERPO DEL MENSAJE
        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses, PaymentMethods.MonthlyFeeSupplies

                Dim currentPrice = _currentPrice.ToString("C2")

                messageBody = TariffTransactionReport(paymentMethod, LblPaymentMethod.Text,
                                                      currentPrice, DialogMessages.RecordSavedSuccessfully)

            Case PaymentMethods.AgeDiscount, PaymentMethods.FamilyGroup

                Dim additionalInfo As String = GetAdditionalTariffInfo(paymentMethod)

                Dim discount As String = CDec(NormalizeMoneyText(TxtDiscount.Text)).ToString("C2")

                Dim toPay As String = CDec(NormalizeMoneyText(TxtToPay.Text)).ToString("C2")

                messageBody = TariffTransactionReport(paymentMethod, LblPaymentMethod.Text, additionalInfo,
                                                      TxtTotal.Text, discount, toPay,
                                                      DialogMessages.RecordSavedSuccessfully)

            Case PaymentMethods.Monthly

                Dim currentPrice = _currentPrice.ToString("C2")

                messageBody = TariffTransactionReport(currentPrice, AppMessages.GeneralPriceDescription,
                                                      DialogMessages.RecordSavedSuccessfully)

        End Select

        MessageBox.Show(messageBody, "Nuevo registro", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' =======================================================
        ' | SI LA TARIFA FUE SOLICITADA DESDE GRUPOS FAMILIARES |
        ' =======================================================
        If IsGroupRateRequest Then

            Me.DialogResult = DialogResult.OK
            Me.Close()
            Return

        End If

        ' =====================================================
        ' | CONTINUA EL FLUJO NORMAL DE FrmPricesAndDiscounts |
        ' =====================================================

        _currentMode = Nothing

        FetchAndRenderTariffsGridUI()

        FindAndSelectRowByName(LblPaymentMethod.Text)

        SetInterfaceVisualState(isEditing:=False)

        DisableInputControls()

        CmbPaymentMethod.Text = String.Empty

    End Sub


    Private Sub BtnModifyRate_Click(sender As Object, e As EventArgs) Handles BtnModifyRate.Click

        ' COMPROBAR SI HAY REGISTRO SELECCIONADO
        If _currentTariffId = 0 Then

            MessageBox.Show(SelectRecordWarning("modificar"), "Verificar selección",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvPriceList.Focus()
            Exit Sub

        End If

        _currentMode = TransactionMode.EditRecord

        Dim paymentMethod As String = GetNamePaymentMethod()

        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses, PaymentMethods.MonthlyFeeSupplies

                TxtPrice.Enabled = True
                TxtPrice.Focus()

            Case PaymentMethods.AgeDiscount

                TxtDiscount.Enabled = True
                TxtToPay.Enabled = True
                NudMinimumAge.Enabled = True
                NudMaximumAge.Enabled = True
                TxtDiscount.Focus()

                _tempAgeMin = Convert.ToInt32(NudMinimumAge.Value)
                _tempAgeMax = Convert.ToInt32(NudMaximumAge.Value)

            Case PaymentMethods.FamilyGroup

                TxtDiscount.Enabled = True
                TxtToPay.Enabled = True
                NudNumberMembers.Enabled = True
                TxtDiscount.Focus()

                _tempAgeMax = Convert.ToInt32(NudNumberMembers.Value)

            Case PaymentMethods.Monthly

                Dim messageBody = TariffTransactionReport(TxtPrice.Text, AppMessages.ModifyRelatedRatesWarning,
                                                          AppMessages.BasePriceModificationConfirmation)

                Dim userResponse As MsgBoxResult = MessageBox.Show(messageBody, "Advertencia", MessageBoxButtons.YesNo,
                                                                   MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
                If userResponse = vbYes Then
                    TxtPrice.Enabled = True
                    TxtPrice.Focus()
                Else
                    _currentMode = Nothing
                    Exit Sub
                End If

        End Select

        ConfigureValidationState(paymentMethod, TransactionMode.EditRecord)

        SetInterfaceVisualState(isEditing:=True)

    End Sub


    Private Sub BtnUpdateRate_Click(sender As Object, e As EventArgs) Handles BtnUpdateRate.Click

        ' DETECCIÓN DE DUPLICADOS EN EL DATAGRIDVIEW.
        If FindAndSelectRowByName(LblPaymentMethod.Text, currentTariffId:=_currentTariffId) Then
            MessageBox.Show(DuplicatedTariffNameWarning("ACTUALIZAR la", LblPaymentMethod.Text),
                            "Error de registro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtPrice.Focus()
            Exit Sub
        End If

        Dim paymentMethod As String = GetNamePaymentMethod()

        ' ACTUALIZAR USANDO DTO Y TARIFFMANAGER
        Try
            Dim tariffToUpdateDto As New TariffDTO() With
            {
                .IdTariff = _currentTariffId,
                .PaymentMethod = LblPaymentMethod.Text,
                .Price = _currentPrice,
                .MinimumAge = CInt(NudMinimumAge.Value),
                .MaximumAge = CInt(NudMaximumAge.Value),
                .NumberMembers = CInt(NudNumberMembers.Value),
                .Discount = _currentDiscount
            }

            Dim isUpdated As Boolean = _tariffManager.UpsertTariff(tariffToUpdateDto)

            If Not isUpdated Then
                MsgBox("ERROR AL ACTUALIZAR")
                Exit Sub
            End If

            ' SI ES LA TARIFA MENSUAL BASE, DISPARAMOS EL RECALCULO EN CASCADA
            If paymentMethod = PaymentMethods.Monthly Then _tariffManager.UpdateDerivedTariffsPrice(_currentPrice)

        Catch ex As Exception
            MsgBox($"ERROR AL ACTUALIZAR : {vbCrLf}{ex.Message}")
            Exit Sub
        End Try

        ' CONSTRUIR EL CUERPO DEL MENSAJE

        Dim messageBody As String = String.Empty

        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses, PaymentMethods.MonthlyFeeSupplies

                Dim currentPrice = _currentPrice.ToString("C2")

                messageBody = TariffTransactionReport(paymentMethod, LblPaymentMethod.Text,
                                                      currentPrice, DialogMessages.RecordUpdatedSuccessfully)

            Case PaymentMethods.AgeDiscount, PaymentMethods.FamilyGroup


                Dim additionalInfo As String = GetAdditionalTariffInfo(paymentMethod)

                Dim discount As String = CDec(NormalizeMoneyText(TxtDiscount.Text)).ToString("C2")

                Dim toPay As String = CDec(NormalizeMoneyText(TxtToPay.Text)).ToString("C2")

                messageBody = TariffTransactionReport(paymentMethod, LblPaymentMethod.Text, additionalInfo,
                                                      TxtTotal.Text, discount, toPay,
                                                      DialogMessages.RecordUpdatedSuccessfully)

            Case PaymentMethods.Monthly

                Dim currentPrice = _currentPrice.ToString("C2")

                messageBody = TariffTransactionReport(currentPrice, AppMessages.GeneralPriceDescription,
                                                      DialogMessages.RecordUpdatedSuccessfully)

        End Select

        _currentMode = Nothing

        FetchAndRenderTariffsGridUI()

        FindAndSelectRowByName(LblPaymentMethod.Text)

        SetInterfaceVisualState(isEditing:=False)

        DisableInputControls()

        CmbPaymentMethod.Text = String.Empty

        MessageBox.Show(messageBody, "Registro modificado", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub


    Private Sub BtnDeleteRate_Click(sender As Object, e As EventArgs) Handles BtnDeleteRate.Click

        ' COMPROBAR SI HAY REGISTRO SELECCIONADO
        If _currentTariffId = 0 Then

            MessageBox.Show(SelectRecordWarning("eliminar"), "Verificar selección",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            DgvPriceList.Focus()
            Exit Sub

        End If

        _currentMode = TransactionMode.DeleteRecord

        Try

            ' CONSTRUCCIÓN INTELIGENTE DEL MENSAJE CONFIRMACIÓN
            Dim paymentMethod As String = GetNamePaymentMethod()
            Dim messageBody As String = String.Empty

            Select Case paymentMethod

                Case PaymentMethods.IndividualClasses, PaymentMethods.MonthlyFeeSupplies

                    messageBody = TariffTransactionReport(paymentMethod, LblPaymentMethod.Text,
                                                          TxtTotal.Text, DialogMessages.RecordDeletionConfirmation)

                Case PaymentMethods.AgeDiscount, PaymentMethods.FamilyGroup

                    Dim additionalInfo As String = GetAdditionalTariffInfo(paymentMethod)

                    messageBody = TariffTransactionReport(paymentMethod, LblPaymentMethod.Text, additionalInfo,
                                                          TxtTotal.Text, TxtDiscount.Text, TxtToPay.Text,
                                                          DialogMessages.RecordDeletionConfirmation)

                Case PaymentMethods.Monthly

                    messageBody = TariffTransactionReport(TxtTotal.Text, AppMessages.BaseRateCannotBeDeleted,
                                                          AppMessages.BaseRateCanBeModified)

                    MessageBox.Show(messageBody, "Operación Restringida", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    DgvPriceList.Focus()
                    Exit Sub

            End Select

            ' PREGUNTA DE SEGURIDAD (El botón 'No' viene enfocado por defecto para evitar accidentes)
            Dim userResponse As MsgBoxResult = MessageBox.Show(messageBody, "Confirmar Eliminación", MessageBoxButtons.YesNo,
                                                               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)

            If userResponse = vbYes Then

                _tariffManager.DeleteTariff(_currentTariffId)

                _currentMode = Nothing

                ClearInputControls()

                ResetStateVariables()

                SetInterfaceVisualState(isEditing:=False)

                FetchAndRenderTariffsGridUI()

                DgvPriceList.CurrentCell = Nothing

                MessageBox.Show(DialogMessages.RecordDeletedSuccessfully, "Registro eliminado",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If

        Catch ex As Exception
            MsgBox($"ERROR AL ELIMINAR :{vbCrLf}{ex.Message}")
        End Try

    End Sub


    Private Sub BtnCancelRegistration_Click(sender As Object, e As EventArgs) Handles BtnCancelRegistration.Click

        _currentMode = Nothing

        SetInterfaceVisualState(isEditing:=False)

        DisableInputControls()

        CmbPaymentMethod.Text = String.Empty

        ClearInputControls()

        DgvPriceList.CurrentCell = Nothing

        ResetStateVariables()

    End Sub


    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles BtnCloseWindow.Click
        Me.Close()
    End Sub

#End Region


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " 1. INICIALIZACIÓN Y ORQUESTACIÓN DE CARGA "
    ' Funciones mayores que coordinan la carga o preparación de datos general.

    ''' <summary>
    ''' Calcula dinámicamente las fronteras comerciales permitidas (mínimo y máximo) para el descuento en dinero.
    ''' </summary>
    ''' <remarks>
    ''' Las reglas varían según la estrategia activa obtenida del búnker lógico:
    ''' <list type="bullet">
    ''' <item>
    ''' <description><bold>Descuento por Edad:</bold> Los límites porcentuales se aplican directamente sobre la mensualidad base fijada.</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Grupo Familiar:</bold> Los límites porcentuales se calculan sobre el subtotal acumulado del número de integrantes (Precio Base x Integrantes).</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Otras Tarifas:</bold> Se inicializan a cero (0D) al no admitir descuentos en su estructura.</description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub CalculateDiscountLimits()

        Dim paymentMethod As String = GetNamePaymentMethod()

        ' Determinamos los límites del descuento según la selección del combo (Sustituye a "DSCTO" o "GRUPO")
        Select Case paymentMethod

            Case PaymentMethods.AgeDiscount

                _allowedDiscountMin = _fixedMonthlyPrice * AGE_DISCOUNT_MIN_PCT
                _allowedDiscountMax = _fixedMonthlyPrice * AGE_DISCOUNT_MAX_PCT

            Case PaymentMethods.FamilyGroup

                Dim totalGroupBase As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                _allowedDiscountMin = totalGroupBase * FAMILY_GROUP_MIN_PCT
                _allowedDiscountMax = totalGroupBase * FAMILY_GROUP_MAX_PCT

            Case Else

                _allowedDiscountMin = 0D
                _allowedDiscountMax = 0D
        End Select

    End Sub


    ''' <summary>
    ''' Calcula dinámicamente los rangos de precio mínimos y máximos permitidos en función del tipo de pago seleccionado.
    ''' </summary>
    Private Sub CalculatePriceLimits()

        ' Regla del Sistema: Si es la primera tarifa o es la Mensualidad base, los límites son fijos
        If DgvPriceList.RowCount = 0 OrElse
            LblPaymentMethod.Text.Trim() = PaymentMethods.Monthly Then

            _allowedPriceMin = MINIMUM_PRICE_LIMIT
            _allowedPriceMax = MAXIMUM_PRICE_LIMIT
            Exit Sub

        End If

        Dim paymentMethod As String = GetNamePaymentMethod()

        ' Determinamos los límites comerciales según la selección del combo
        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses
                _allowedPriceMin = _fixedMonthlyPrice * INDIVIDUAL_CLASS_MIN_PCT
                _allowedPriceMax = _fixedMonthlyPrice * INDIVIDUAL_CLASS_MAX_PCT

            Case PaymentMethods.MonthlyFeeSupplies
                _allowedPriceMin = _fixedMonthlyPrice + (_fixedMonthlyPrice / 2D)
                _allowedPriceMax = _fixedMonthlyPrice * 3D

            Case Else
                _allowedPriceMin = 0D
                _allowedPriceMax = _fixedMonthlyPrice

        End Select

    End Sub


    ''' <summary>
    ''' Calcula dinámicamente los límites financieros permitidos (mínimo y máximo) para el neto final a pagar.
    ''' </summary>
    ''' <remarks>
    ''' Establece las fronteras de cobro aplicando las deducciones correspondientes según la estrategia comercial activa:
    ''' <list type="bullet">
    ''' <item>
    ''' <description><bold>Descuento por Edad:</bold> Resta los topes de descuento (mínimo y máximo) al precio de la mensualidad base.</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Grupo Familiar:</bold> Multiplica la mensualidad base por el número de integrantes para hallar el subtotal y resta los topes de descuento grupal.</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Otras Tarifas / Caso Base:</bold> El mínimo permitido se establece en cero (0D) y el máximo queda topado estrictamente por la mensualidad general fija.</description>
    ''' </item>
    ''' </list>
    ''' Nota: Por pura lógica matemática, el valor de <italic>_allowedToPayMin</italic> se calcula usando el porcentaje máximo de descuento, mientras que <italic>_allowedToPayMax</italic> usa el porcentaje mínimo.
    ''' </remarks>
    Private Sub CalculateToPayLimits()

        Dim paymentMethod As String = GetNamePaymentMethod()

        Select Case paymentMethod

            Case PaymentMethods.AgeDiscount
                _allowedToPayMin = _fixedMonthlyPrice - (_fixedMonthlyPrice * AGE_DISCOUNT_MAX_PCT)
                _allowedToPayMax = _fixedMonthlyPrice - (_fixedMonthlyPrice * AGE_DISCOUNT_MIN_PCT)

            Case PaymentMethods.FamilyGroup
                Dim totalGroupBase As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                _allowedToPayMin = totalGroupBase - (totalGroupBase * FAMILY_GROUP_MAX_PCT)
                _allowedToPayMax = totalGroupBase - (totalGroupBase * FAMILY_GROUP_MIN_PCT)

            Case Else
                _allowedToPayMin = 0D
                _allowedToPayMax = _fixedMonthlyPrice

        End Select

    End Sub


    ''' <summary>
    ''' Genera la información complementaria que se mostrará
    ''' en el cuadro de confirmación de la operación,
    ''' adaptándola al tipo de tarifa seleccionado.
    ''' </summary>
    ''' <param name="paymentMethod">
    ''' Método de pago o tarifa que determina el contenido
    ''' de la información adicional.
    ''' </param>
    ''' <returns>
    ''' Texto descriptivo correspondiente a la tarifa seleccionada,
    ''' o una cadena vacía si no aplica.
    ''' </returns>
    Private Function GetAdditionalTariffInfo(paymentMethod As String) As String

        Select Case paymentMethod

            Case PaymentMethods.AgeDiscount
                Return $"Rango de edad : Entre {NudMinimumAge.Value} y {NudMaximumAge.Value} años"

            Case PaymentMethods.FamilyGroup
                Return $"Cupo máximo : {NudNumberMembers.Value} INTEGRANTES"

            Case Else
                Return String.Empty

        End Select

    End Function


    ''' <summary>
    ''' Obtiene el nombre comercial limpio del método de pago actual de forma unificada,
    ''' abstrayendo si estamos creando un registro nuevo o editando uno existente.
    ''' </summary>
    Private Function GetNamePaymentMethod() As String

        If _currentMode Is Nothing Then Return String.Empty

        ' Modo Nuevo: Texto del Combobox con el nombre de la tarifa.
        If _currentMode = TransactionMode.NewRecord AndAlso
            Not String.IsNullOrEmpty(CmbPaymentMethod.Text.Trim()) Then

            Return CmbPaymentMethod.Text.Trim()

        End If

        ' Modo Edición: Desmenuzamos la etiqueta con el nombre de la tarifa.
        Dim currentLabel As String = LblPaymentMethod.Text.Trim()

        Select Case True

            Case currentLabel.StartsWith(PaymentMethods.Daily)
                Return PaymentMethods.IndividualClasses

            Case currentLabel.StartsWith(PaymentMethods.AgeDscnt)
                Return PaymentMethods.AgeDiscount

            Case currentLabel.StartsWith(PaymentMethods.FmlGroup)
                Return PaymentMethods.FamilyGroup

            Case currentLabel.StartsWith(PaymentMethods.MonthImp)
                Return PaymentMethods.MonthlyFeeSupplies

        End Select

        ' Si no entra en ninguna, por descarte es la mensualidad base (Precio Fijo)
        Return PaymentMethods.Monthly

    End Function


    ''' <summary>
    ''' Determina si la configuración actual de precios y descuentos
    ''' cumple todas las reglas de validación requeridas.
    ''' </summary>
    ''' <returns>
    ''' <c>True</c> si todos los datos necesarios son válidos según
    ''' el tipo de tarifa configurada; de lo contrario, <c>False</c>.
    ''' </returns>
    ''' <remarks>
    ''' Las validaciones aplicadas dependen del tipo de configuración:
    ''' tarifas individuales, descuentos por edad o grupos familiares.
    ''' El resultado suele utilizarse para habilitar o deshabilitar
    ''' la acción principal del formulario.
    ''' </remarks>
    Private Function IsTariffConfigurationValid() As Boolean

        ' El modo de transacción no está activo, protegemos el botón.
        If _currentMode Is Nothing Then Return False

        Dim paymentMethod As String = GetNamePaymentMethod()

        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses, PaymentMethods.MonthlyFeeSupplies, PaymentMethods.Monthly
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid

            Case PaymentMethods.AgeDiscount
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid AndAlso _isMinimumAgeValid AndAlso _isMaximumAgeValid

            Case PaymentMethods.FamilyGroup
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid AndAlso _isNumberMembersValid

            Case Else
                Return False

        End Select

    End Function


    ''' <summary>
    ''' Valida la coherencia lógica y comercial del rango de edades establecido para la tarifa.
    ''' </summary>
    ''' <remarks>
    ''' Esta regla de negocio exige estrictamente que la edad máxima sea mayor que la edad mínima (Max > Min).
    ''' Si el rango entra en conflicto, el control visual se tiñe de rojo y se aplica negrita; si es correcto, 
    ''' se restablece al color azul marino del búnker y texto normal, actualizando la bandera de estado global.
    ''' </remarks>
    Private Sub ValidateAgeRangeCoherence()

        Dim minAge As Decimal = NudMinimumAge.Value
        Dim maxAge As Decimal = NudMaximumAge.Value

        ' COMPROBACIÓN : La edad máxima DEBE ser mayor estricta que la mínima (Max > Min)
        If minAge >= maxAge Then

            NudMaximumAge.ForeColor = Color.Red
            NudMaximumAge.Font = New System.Drawing.Font(NudMaximumAge.Font, FontStyle.Bold)

            _isMaximumAgeValid = False
        Else
            NudMaximumAge.ForeColor = Color.MediumBlue
            NudMaximumAge.Font = New System.Drawing.Font(NudMaximumAge.Font, NudMaximumAge.Font.Style And Not FontStyle.Bold)

            _isMaximumAgeValid = True
        End If

    End Sub

#End Region

#Region " 2. CONFIGURACIÓN VISUAL (Estrategia UI) "
    ' Métodos dedicados exclusivamente a la cosmética y mutación de controles.

    ''' <summary>
    ''' Aplica las restricciones y configuraciones por defecto requeridas para la tarifa inicial del sistema.
    ''' </summary>
    Private Sub ApplyFirstTariffDefaultRules()

        CmbPaymentMethod.Enabled = False
        NudNumberMembers.Value = 1
        LblPaymentMethod.Text = PaymentMethods.Monthly

        TxtPrice.Enabled = True
        TxtPrice.Text = "0"
        TxtPrice.Focus()

        TxtDiscount.Text = "0"

        _allowedPriceMin = MINIMUM_PRICE_LIMIT
        _allowedPriceMax = MAXIMUM_PRICE_LIMIT

        ConfigureValidationState(PaymentMethods.Monthly, TransactionMode.NewRecord)

        ' El botón solo se encenderá si la tarifa es correcta.
        Dim isFormValid As Boolean = IsTariffConfigurationValid()
        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub


    ''' <summary>
    ''' Limpia todas las cajas de texto y restablece los selectores numéricos a sus valores base.
    ''' </summary>
    Private Sub ClearInputControls()

        NudNumberMembers.Minimum = 0
        NudNumberMembers.Value = 0
        NudMinimumAge.Value = 0
        NudMaximumAge.Value = 0

        TxtPrice.Clear()
        TxtTotal.Clear()
        TxtDiscount.Clear()
        TxtToPay.Clear()

        LblPaymentMethod.Text = String.Empty

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Descuentos por Edad.
    ''' </summary>
    Private Sub ConfigureAgeDiscountUI()

        TxtPrice.Text = _fixedMonthlyPrice.ToString("N2")
        TxtTotal.Text = TxtPrice.Text '_fixedMonthlyPrice.ToString("N2")

        TxtDiscount.Enabled = True
        TxtDiscount.Text = 0
        TxtDiscount.Focus()

        TxtToPay.Enabled = True

        LblPaymentMethod.Text = PaymentMethods.AgeDscnt

        NudNumberMembers.Value = 1

        NudMinimumAge.Enabled = True
        NudMinimumAge.Value = MINIMUM_AGE_FOR_DISCOUNT

        NudMaximumAge.Enabled = True
        NudMaximumAge.Value = MINIMUM_AGE_FOR_DISCOUNT

        ConfigureValidationState(PaymentMethods.AgeDiscount, TransactionMode.NewRecord)

        ' El botón solo se encenderá si la tarifa es correcta.
        Dim isFormValid As Boolean = IsTariffConfigurationValid()
        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para las estrategias de precio directo 
    ''' (Clases Sueltas o Mensualidad más Implementos).
    ''' </summary>
    ''' <param name="paymentMethodPrefix">El prefijo del método de pago que se asignará a la etiqueta (Daily o MonthImp).</param>
    Private Sub ConfigureDirectPriceTariffUI(paymentMethodPrefix As String, paymentMethods As String)

        TxtPrice.Enabled = True
        TxtPrice.Text = 0
        TxtPrice.Focus()

        TxtTotal.Text = TxtPrice.Text
        TxtDiscount.Text = 0
        TxtToPay.Text = 0

        LblPaymentMethod.Text = paymentMethodPrefix

        NudNumberMembers.Value = 1

        ConfigureValidationState(paymentMethods, TransactionMode.NewRecord)

        ' El botón solo se encenderá si la tarifa es correcta.
        Dim isFormValid As Boolean = IsTariffConfigurationValid()
        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Grupo Familiar.
    ''' </summary>
    Private Sub ConfigureFamilyGroupTariffUI()

        TxtPrice.Text = _fixedMonthlyPrice.ToString("N2")

        TxtDiscount.Enabled = True
        TxtDiscount.Text = 0

        TxtToPay.Enabled = True

        LblPaymentMethod.Text = PaymentMethods.FmlGroup

        NudNumberMembers.Enabled = True
        NudNumberMembers.Minimum = 3

        ' NÚMERO DE INTEGRANTES POR DEFECTO O SUGERIDO DESDE OTRO FORMULARIO
        Dim suggestedMembers As Integer = If(SuggestedNumberMembers.HasValue, SuggestedNumberMembers.Value, CInt(NudNumberMembers.Minimum))

        If suggestedMembers < NudNumberMembers.Minimum Then suggestedMembers = CInt(NudNumberMembers.Minimum)

        If suggestedMembers > NudNumberMembers.Maximum Then suggestedMembers = CInt(NudNumberMembers.Maximum)

        NudNumberMembers.Value = suggestedMembers
        ' NudNumberMembers.Value = 3

        ConfigureValidationState(PaymentMethods.FamilyGroup, TransactionMode.NewRecord)

        ' El botón solo se encenderá si la tarifa es correcta.
        Dim isFormValid As Boolean = IsTariffConfigurationValid()

        BtnSaveRate.Enabled = isFormValid
        BtnUpdateRate.Enabled = isFormValid

        TxtDiscount.Focus()

    End Sub


    ''' <summary>
    ''' Deshabilita de forma general todas las entradas de datos numéricos y cajas de texto.
    ''' </summary>
    Private Sub DisableInputControls()

        TxtPrice.Enabled = False
        TxtTotal.Enabled = False
        TxtDiscount.Enabled = False
        TxtToPay.Enabled = False

        NudNumberMembers.Enabled = False
        NudMinimumAge.Enabled = False
        NudMaximumAge.Enabled = False

    End Sub


    ''' <summary>
    ''' Reinicializa todas las variables lógicas de estado y banderas de validación interna a sus valores neutros de fábrica.
    ''' </summary>
    ''' <remarks>
    ''' Este método purga la memoria RAM del formulario desactivando el modo de transacción (<italic>_currentMode = Nothing</italic>) 
    ''' y poniendo en <italic>False</italic> los semáforos de validación. Se invoca de forma estratégica durante los procesos 
    ''' de limpieza general, cancelaciones o inmediatamente después de una persistencia exitosa en la base de datos.
    ''' </remarks>
    Private Sub ResetStateVariables()

        _selectedTariffId = 0
        _currentTariffId = 0
        _tempDiscount = 0
        _tempAgeMin = 0
        _tempAgeMax = 0

    End Sub


    ''' <summary>
    ''' Restablece todos los cerrojos lógicos de validación a su estado base.
    ''' </summary>
    ''' <remarks>
    ''' Esta función limpia cualquier estado de validación previo antes de
    ''' configurar las reglas correspondientes al método de pago y al modo
    ''' de transacción actual.
    ''' 
    ''' Su ejecución garantiza que ninguna validación residual de una
    ''' estrategia anterior afecte al comportamiento del formulario.
    ''' </remarks>
    Private Sub ResetValidationState()

        _isPriceValid = False
        _isDiscountValid = False
        _isToPayValid = False

        _isMinimumAgeValid = False
        _isMaximumAgeValid = False

        _isNumberMembersValid = False

    End Sub


    ''' <summary>
    ''' Configura el estado inicial de las validaciones del formulario
    ''' según el método de pago seleccionado y el modo de transacción actual.
    ''' </summary>
    ''' <param name="paymentMethod">
    ''' Método de pago o estrategia tarifaria que determina las reglas de validación.
    ''' </param>
    ''' <param name="mode">
    ''' Modo de operación del formulario (nuevo registro o edición).
    ''' </param>
    ''' <remarks>
    ''' Esta función centraliza los cerrojos lógicos de validación utilizados
    ''' para habilitar o bloquear las acciones principales del formulario.
    ''' 
    ''' Dependiendo del método de pago y del modo de trabajo, establece qué
    ''' campos deben considerarse válidos inicialmente y cuáles requieren
    ''' intervención del usuario antes de permitir guardar o actualizar.
    ''' </remarks>
    Private Sub ConfigureValidationState(paymentMethod As String, mode As TransactionMode)

        ' Devolvemos todas las variables lógicas a FALSE para arrancar sin arrastrar estados previos.
        ResetValidationState()

        ' Reglas de negocio según la estrategia del método de pago
        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses, PaymentMethods.MonthlyFeeSupplies, PaymentMethods.Monthly

                ' NUEVO: FALSE (obliga al usuario a teclear un precio diferente de 0).
                ' EDICIÓN: TRUE (el precio cargado desde la BBDD ya es correcto de entrada).
                _isPriceValid = (mode = TransactionMode.EditRecord)

                ' Estas estrategias no aplican descuentos ni cálculos.
                _isDiscountValid = True
                _isToPayValid = True

            Case PaymentMethods.AgeDiscount

                ' El precio base para ya está establecido por el sistema.
                _isPriceValid = True

                ' NUEVO: FALSE (exige ingresar un descuento inicial valido).
                ' EDICIÓN: TRUE (el descuento guardado en el registro es válido de origen).
                _isDiscountValid = (mode = TransactionMode.EditRecord)

                ' Se evaluará cuando cambie el descuento o el total.
                _isToPayValid = True

                ' La edad mínima por defecto (ej: 5 años) siempre arranca como un valor inicial correcto.
                _isMinimumAgeValid = True

                ' NUEVO: FALSE (la edad máxima arranca igual a la mínima, lo cual es un rango incorrecto).
                ' EDICIÓN: TRUE (el rango de edades recuperado de la BBDD ya pasó los filtros de consistencia).
                _isMaximumAgeValid = (mode = TransactionMode.EditRecord)

            Case PaymentMethods.FamilyGroup

                ' El precio base para la estructura de grupos familiares está validado.
                _isPriceValid = True

                ' NUEVO: FALSE (obliga a establecer o calcular el descuento del grupo).
                ' EDICIÓN: TRUE (el descuento del registro existente es correcto).
                _isDiscountValid = (mode = TransactionMode.EditRecord)

                ' El desglose total a pagar para el grupo familiar ya viene calculado de entrada.
                _isToPayValid = True

                ' NUEVO: Arranca con el valor por defecto válido (ej: "3 integrantes").
                ' EDICIÓN: Viene correcto de la BBDD.
                _isNumberMembersValid = True

        End Select

    End Sub


    ''' <summary>
    ''' Gestiona de forma centralizada la visibilidad y disponibilidad de los controles de la pantalla 
    ''' según el estado de la transacción actual.
    ''' </summary>
    ''' <param name="isEditing">
    ''' TRUE si el formulario entra en modo Creación/Edición.
    ''' FALSE para modo Consulta/Lectura.</param>
    Private Sub SetInterfaceVisualState(isEditing As Boolean)

        ' 1. EVALUAMOS LA EXISTENCIA DE DATOS EN LA TABLA
        Dim hasRows As Boolean = (DgvPriceList.RowCount > 0)

        ' 2. CONTROLES DE NAVEGACIÓN Y SELECCIÓN
        DgvPriceList.Enabled = Not isEditing AndAlso hasRows 'If(isEditing, False, hasRows)
        CmbPaymentMethod.Enabled = isEditing AndAlso (_currentMode = TransactionMode.NewRecord)

        ' 3. BOTONES DE ACCIÓN PRINCIPAL (Nuevo, Modificar, Eliminar)
        BtnNewRate.Visible = Not isEditing
        BtnModifyRate.Visible = Not isEditing AndAlso hasRows 'If(isEditing, False, hasRows)
        BtnDeleteRate.Visible = Not isEditing AndAlso hasRows 'If(isEditing, False, hasRows)

        ' 4. BOTONES DE TRANSACCIÓN (Guardar, Actualizar, Cancelar)
        BtnCancelRegistration.Visible = isEditing

        ' 5. GUARDAR VS. ACTUALIZAR
        If isEditing Then
            BtnSaveRate.Visible = (_currentMode = TransactionMode.NewRecord)
            BtnUpdateRate.Visible = (_currentMode = TransactionMode.EditRecord)
        Else
            BtnSaveRate.Visible = False
            BtnUpdateRate.Visible = False
        End If

        ' 5. FOCOS ESTATÉGICOS AUTOMÁTICOS
        If Not isEditing Then BtnNewRate.Focus()

    End Sub


    ''' <summary>
    ''' Recalcula y renderiza en la interfaz el neto final a pagar a partir del valor de descuento ingresado por el usuario.
    ''' </summary>
    ''' <param name="currentDiscountValue">El valor numérico limpio del descuento que se está aplicando en la RAM.</param>
    ''' <remarks>
    ''' Esta función reacciona en caliente a los cambios del descuento aplicando las siguientes estrategias matemáticas:
    ''' <list type="bullet">
    ''' <item>
    ''' <description><bold>Descuento por Edad:</bold> Resta el descuento directamente de la mensualidad fija establecida.</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Grupo Familiar:</bold> Calcula el precio total del grupo (Mensualidad x Integrantes), lo renderiza en <italic>TxtTotal</italic>, y luego le resta el descuento para pintar el neto en <italic>TxtToPay</italic>.</description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub UpdateDiscountCalculationsAndTotals(currentDiscountValue As Decimal)

        Dim paymentMethod As String = GetNamePaymentMethod()

        Select Case paymentMethod

            Case PaymentMethods.AgeDiscount
                TxtToPay.Text = (_fixedMonthlyPrice - currentDiscountValue).ToString("C2")

            Case PaymentMethods.FamilyGroup
                Dim total As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                TxtTotal.Text = total.ToString("C2")

                Dim totalToPay As Decimal = total - currentDiscountValue
                TxtToPay.Text = totalToPay.ToString("C2")

        End Select

    End Sub


    ''' <summary>
    ''' Actualiza la etiqueta informativa agregando el precio actual en tiempo real según el tipo de tarifa.
    ''' </summary>
    Private Sub UpdateDynamicTariffLabel()

        Dim paymentMethod As String = GetNamePaymentMethod()
        Dim prefix As String

        Select Case paymentMethod

            Case PaymentMethods.IndividualClasses
                prefix = PaymentMethods.Daily

            Case PaymentMethods.MonthlyFeeSupplies
                prefix = PaymentMethods.MonthImp

            Case PaymentMethods.AgeDiscount
                prefix = $"{PaymentMethods.AgeDscnt} {NudMinimumAge.Value}-{NudMaximumAge.Value}"

            Case PaymentMethods.FamilyGroup
                prefix = $"{PaymentMethods.FmlGroup} {NudNumberMembers.Value}"

            Case Else
                prefix = PaymentMethods.Monthly

        End Select

        Dim currentPriceText As String = TxtPrice.Text.Replace("€", "").Trim()

        ' Si está vacío o es cero, mostramos solo el prefijo.
        If String.IsNullOrEmpty(currentPriceText) OrElse
            currentPriceText = "0" OrElse currentPriceText = "0,00" Then

            LblPaymentMethod.Text = prefix
            Exit Sub

        End If

        ' Solo las clases sueltas y mensualidad+implementos muestran el precio.
        If paymentMethod = PaymentMethods.IndividualClasses OrElse
            paymentMethod = PaymentMethods.MonthlyFeeSupplies Then

            LblPaymentMethod.Text = $"{prefix} {currentPriceText}"
        Else
            LblPaymentMethod.Text = prefix

        End If

    End Sub


    ''' <summary>
    ''' Recalcula de forma inversa y renderiza en la interfaz el descuento otorgado a partir del importe neto final a pagar ingresado por el usuario.
    ''' </summary>
    ''' <param name="currentToPayValue">El valor numérico limpio del total a pagar que se está evaluando en la RAM.</param>
    ''' <remarks>
    ''' Esta función permite la edición bidireccional en la pantalla. Si el usuario decide digitar directamente cuánto quiere cobrar, el sistema deduce el descuento implícito según el método activo:
    ''' <list type="bullet">
    ''' <item>
    ''' <description><bold>Descuento por Edad:</bold> Halla el descuento restando el valor digitado a la mensualidad fija y lo inyecta formateado en <italic>TxtDiscount</italic>.</description>
    ''' </item>
    ''' <item>
    ''' <description><bold>Grupo Familiar:</bold> Determina el subtotal base del grupo, calcula la diferencia con respecto al neto digitado y renderiza el descuento resultante en <italic>TxtDiscount</italic>.</description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub UpdateToPayCalculationsAndDiscounts(currentToPayValue As Decimal)

        Dim paymentMethod As String = GetNamePaymentMethod()

        Select Case paymentMethod

            Case PaymentMethods.AgeDiscount
                Dim calculatedDiscount As Decimal = _fixedMonthlyPrice - currentToPayValue
                TxtDiscount.Text = calculatedDiscount.ToString("C2")

            Case PaymentMethods.FamilyGroup
                Dim totalGroupBase As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                Dim calculatedDiscount As Decimal = totalGroupBase - currentToPayValue
                TxtDiscount.Text = calculatedDiscount.ToString("C2")

        End Select

    End Sub

#End Region

#Region " 3. REFRESCO DE LISTAS Y GRIDS (Renderizado) "
    ' Encargados de pintar el DataGridView con los datos de la lista.

    ''' <summary>
    ''' Solicita las tarifas vigentes al gestor de negocio
    ''' y las renderiza la información en la cuadrícula.
    ''' </summary>
    Private Sub FetchAndRenderTariffsGridUI()

        Try
            Dim tariffsList As List(Of TariffDTO) = _tariffManager.FetchAllTariffs()

            ' Capturar el precio de la tarifa fija mes (IdTariff = 1),
            ' usamos una consulta LINQ semántica.
            Dim fixedMonthlyTariff = tariffsList.FirstOrDefault(Function(t) t.IdTariff = 1)

            If fixedMonthlyTariff IsNot Nothing Then _fixedMonthlyPrice = fixedMonthlyTariff.Price

            ' Limpiamos y enlazamos la lista directamente al Grid.
            DgvPriceList.DataSource = Nothing
            DgvPriceList.AutoGenerateColumns = False
            DgvPriceList.DataSource = tariffsList

        Catch ex As Exception
            MessageBox.Show($"NO SE PUEDE CARGAR : {ex.Message}")
        End Try

    End Sub


    ''' <summary>
    ''' Busca una tarifa en el DataGridView por su nombre (dinámico). 
    ''' Si encuentra el mismo nombre en OTRA tarifa diferente, activa la alerta de duplicado.
    ''' </summary>
    ''' <param name="tariffName">El nombre comercial generado en la interfaz (ej: LblPaymentMethod.Text)</param>
    ''' <param name="currentTariffId">Opcional: El ID único de la tarifa en edición.
    ''' Si se omite (valor 0), busca duplicados globales (Modo Guardar).</param>
    ''' <returns>True si el nombre ya está siendo usado por otra tarifa; False si el camino está limpio.</returns>
    Private Function FindAndSelectRowByName(tariffName As String,
                                            Optional currentTariffId As Integer = 0) As Boolean

        Try
            For Each row As DataGridViewRow In DgvPriceList.Rows
                ' Extraemos el objeto de negocio directo de la fila del Grid
                Dim filaTarifa = DirectCast(row.DataBoundItem, TariffDTO)

                ' 1. Comprobamos si coincide el nombre de la tarifa en el Grid con el nombre que queremos poner
                If filaTarifa.PaymentMethod.ToString().Trim() = tariffName.Trim() Then

                    ' 2. LA MAGIA INTELIGENTE:
                    ' Si estamos ACTUALIZANDO (currentTariffId > 0) y el ID de la fila coincide con el nuestro,
                    ' significa que el nombre no ha cambiado (soy yo mismo). ¡Lo ignoramos y seguimos buscando!
                    If currentTariffId > 0 AndAlso filaTarifa.IdTariff = currentTariffId Then
                        Continue For
                    End If

                    ' 3. Si llegó aquí, es un duplicado real:
                    ' - O estamos Guardando (currentTariffId es 0) y encontró un nombre idéntico.
                    ' - O estamos Actualizando y encontró otra fila diferente con ese mismo nombre.
                    DgvPriceList.CurrentCell = row.Cells("ColPaymentMethod") ' Ajusta al nombre real de tu columna
                    row.Selected = True
                    Return True ' Conflicto de duplicado detectado
                End If
            Next

        Catch ex As Exception
            MsgBox($"ERROR AL VERIFICAR DUPLICADOS: {vbCrLf}{ex.Message}")
        End Try

        Return False ' No hay conflicto, todo limpio para operar

    End Function

#End Region

#Region " 4. ESTRUCTURAS Y ENUMS AUXILIARES "
    ' Tipos de datos personalizados que definen los estados y reglas del formulario.

    Public Enum TransactionMode
        NewRecord
        EditRecord
        DeleteRecord
    End Enum

#End Region

End Class