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

#End Region

#Region " EVENTOS DEL FORMULARIO (Handlers) "
    ' Los disparadores nativos de los componentes de Windows Forms.

    Private Sub FrmPricesAndDiscounts_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        RestorePrincipalFormNavigation()
    End Sub
    Private Sub FrmPricesAndDiscounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        FetchAndRenderTariffsGridUI()
        SetInterfaceVisualState(isEditing:=False)

    End Sub


    Private Sub CmbPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPaymentMethod.SelectedIndexChanged

        ' 1. Reseteo y limpieza base ante cualquier cambio de selección
        ClearInputControls()
        ResetStateVariables()
        DisableInputControls()

        ' 2. Orquestación semántica según la selección
        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.IndividualClasses '"CLASES SUELTAS"
                ConfigureDailyTariffUI()

            Case PaymentMethods.AgeDiscount '"DESCUENTO POR EDAD"
                ConfigureAgeDiscountUI()

            Case PaymentMethods.FamilyGroup '"GRUPO FAMILIAR"
                ConfigureFamilyGroupTariffUI()

            Case PaymentMethods.MonthlyFeeSupplies '"MENSUALIDAD + IMPLEMENTOS"
                ConfigureMonthlyWithEquipmentTariffUI()

        End Select

    End Sub


    Private Sub TxtPrice_TextChanged(sender As Object, e As EventArgs) Handles TxtPrice.TextChanged

        ' Si no estamos editando o creando, ignoramos el evento.
        If _currentMode Is Nothing Then Exit Sub

        ' Desconectamos el evento para evitar el bucle infinito al formatear en caliente.
        RemoveHandler TxtPrice.TextChanged, AddressOf TxtPrice_TextChanged
        RemoveHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged

        Try
            ' Extraemos el texto limpio sin el símbolo " €".
            Dim cleanText As String = NormalizeMoneyText(TxtPrice.Text)

            ' Ponemos el formato de moneda automáticamente.
            ApplyMoneyTextboxFormat(TxtPrice)

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

            ' El botón de guardar solo se encenderá si el precio es correcto Y ADEMÁS el descuento es correcto.
            BtnSaveRate.Enabled = ChangeStateButtonSave()

            ' Si el descuento es correcto, guardamos el número limpio en la variable de estado.
            If _isPriceValid Then _currentPrice = currentPriceValue

        Finally
            ' Volvemos a conectar el interruptor del evento siempre
            AddHandler TxtPrice.TextChanged, AddressOf TxtPrice_TextChanged
            AddHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
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
    Private Sub TxtPrice_Leave(sender As Object, e As EventArgs) Handles TxtPrice.Leave
        ' Coalescencia: Si el usuario borra todo, aseguramos un cero por defecto
        'If String.IsNullOrWhiteSpace(TxtPrice.Text) Then TxtPrice.Text = "0"
    End Sub


    Private Sub TxtDiscount_TextChanged(sender As Object, e As EventArgs) Handles TxtDiscount.TextChanged

        ' Si no estamos editando o creando, ignoramos el evento.
        If _currentMode Is Nothing Then Exit Sub

        ' Desconectamos el evento para evitar el bucle infinito al formatear en caliente.
        RemoveHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
        RemoveHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged

        Try
            ' Extraemos el texto limpio sin el símbolo " €".
            Dim cleanDiscountText As String = NormalizeMoneyText(TxtDiscount.Text)

            ' Ponemos el formato de moneda automáticamente.
            ApplyMoneyTextboxFormat(TxtDiscount)

            ' Parseo numérico seguro
            Dim currentDiscountValue As Decimal
            Dim isDecimalValid As Boolean = Decimal.TryParse(cleanDiscountText, currentDiscountValue)


            ' Calculamos los límites Minimo y Maximo del descuento.
            CalculateDiscountLimits()

            ' Calcula el total a pagar según el tipo de pago
            UpdateDiscountCalculationsAndTotals(currentDiscountValue)

            '====================
            ' 🚨 LA PIEZA FALTANTE: Actualizamos los límites comerciales permitidos para el TOTAL A PAGAR
            ' para que _allowedToPayMin y _allowedToPayMax sepan que el 35 es legal.
            CalculateToPayLimits()
            '====================

            ' El precio solo es válido si es un número real Y ADEMÁS está dentro de los límites.
            _isDiscountValid = isDecimalValid AndAlso
                               EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue, _allowedDiscountMin, _allowedDiscountMax)

            Dim cleanToPayText As String = NormalizeMoneyText(TxtToPay.Text)
            Dim currentToPayValue As Decimal
            Decimal.TryParse(cleanToPayText, currentToPayValue)
            _isToPayValid = EvaluateNumericRangeLimits(TxtToPay, currentToPayValue, _allowedToPayMin, _allowedToPayMax)

            ' El botón de guardar solo se encenderá si el precio es correcto Y ADEMÁS el descuento es correcto.
            BtnSaveRate.Enabled = ChangeStateButtonSave()

            ' Si el descuento es correcto, guardamos el número limpio en la variable de estado.
            If _isDiscountValid Then _currentDiscount = currentDiscountValue

        Finally
            ' Volvemos a conectar el interruptor del evento.
            AddHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
            AddHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged
        End Try

    End Sub
    Private Sub TxtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtDiscount.KeyPress
        AllowDecimalInput(sender, e)
    End Sub
    Private Sub TxtDiscount_Enter(sender As Object, e As EventArgs) Handles TxtDiscount.Enter
        TxtDiscount.SelectAll()
    End Sub
    Private Sub TxtDiscount_Leave(sender As Object, e As EventArgs) Handles TxtDiscount.Leave
        '' Si se quedó vacío o solo con la coma por error, lo auto-repara a un bonito "0,00 €"
        'Dim cleanDiscountText As String = NormalizeMoneyText(TxtDiscount.Text)
        'If String.IsNullOrWhiteSpace(cleanDiscountText) OrElse cleanDiscountText = "," Then
        '    TxtDiscount.Text = "0,00 €"
        '    TxtToPay.Text = ""
        'End If
    End Sub


    Private Sub TxtToPay_TextChanged(sender As Object, e As EventArgs) Handles TxtToPay.TextChanged

        ' Si no estamos editando o creando, ignoramos el evento
        If _currentMode Is Nothing Then Exit Sub

        ' Desconectamos temporalmente para evitar el bucle infinito al formatear y calcular a la inversa
        RemoveHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged
        RemoveHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged

        Try
            ' Extraemos el texto limpio y aplicamos el formato visual de moneda.
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

            ' El botón de guardar vigila el precio, el descuento Y el total a pagar.
            BtnSaveRate.Enabled = ChangeStateButtonSave()

            ' uardamos el número limpio en la variable de estado
            If _isToPayValid Then _currentToPay = currentToPayValue

        Finally
            ' Volvemos a conectar SIEMPRE ambos interruptores de eventos
            AddHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
            AddHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged
        End Try

    End Sub
    Private Sub TxtToPay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtToPay.KeyPress
        AllowDecimalInput(sender, e)
    End Sub
    Private Sub TxtToPay_Enter(sender As Object, e As EventArgs) Handles TxtToPay.Enter
        TxtToPay.SelectAll()
    End Sub
    Private Sub TxtToPay_Leave(sender As Object, e As EventArgs) Handles TxtToPay.Leave
        'Dim cleanDiscountText As String = NormalizeMoneyText(TxtToPay.Text)
        'If String.IsNullOrWhiteSpace(cleanDiscountText) OrElse cleanDiscountText = "," Then
        '    TxtToPay.Text = "0,00 €"
        '    TxtDiscount.Text = ""
        'End If
    End Sub


    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged

        If _currentMode Is Nothing Then Exit Sub

        ' Apagamos temporalmente los eventos.
        RemoveHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
        RemoveHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged

        Try
            If CmbPaymentMethod.Text.Trim() = PaymentMethods.FamilyGroup Then

                ' 1. Evaluamos el propio control numérico, pintamos sus letras en tiempo real
                '    y actualizamos su bandera de estado.
                _isNumberMembersValid = EvaluateNumericRangeLimits(NudNumberMembers, NudNumberMembers.Value,
                                                                   NudNumberMembers.Minimum, NudNumberMembers.Maximum)

                ' 2. Actualizamos la etiqueta informativa
                UpdateDynamicTariffLabel()

                ' 3. Extraemos el valor actual del descuento en formato numérico para no perderlo
                Dim cleanText As String = NormalizeMoneyText(TxtDiscount.Text)
                Dim currentDiscountValue As Decimal
                Decimal.TryParse(cleanText, currentDiscountValue)

                ' 4. Calculamos todo en cadena.
                CalculateDiscountLimits()
                UpdateDiscountCalculationsAndTotals(currentDiscountValue)
                CalculateToPayLimits()

                ' 5. Comprobamos si el descuento sigue siendo válido con los nuevos límites de personas
                _isDiscountValid = EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue,
                                                              _allowedDiscountMin, _allowedDiscountMax)

                BtnSaveRate.Enabled = ChangeStateButtonSave()

            End If

        Finally
            ' Reconectamos siempre los interruptores al salir
            AddHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
            AddHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged
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

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub


    Private Sub NudMinimumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMinimumAge.ValueChanged

        If _currentMode Is Nothing Then Exit Sub

        If CmbPaymentMethod.Text.Trim() = PaymentMethods.AgeDiscount Then UpdateDynamicTariffLabel()

        _isMinimumAgeValid = EvaluateNumericRangeLimits(NudMinimumAge, NudMinimumAge.Value,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMinimumAge.Maximum)

        ValidateAgeRangeCoherence() ' Validación cruzada para el descuento por edad

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub
    Private Sub NudMinimumAge_Enter(sender As Object, e As EventArgs) Handles NudMinimumAge.Enter
        NudMinimumAge.Select(0, NudMinimumAge.Text.Length)
    End Sub
    Private Sub NudMinimumAge_KeyUp(sender As Object, e As KeyEventArgs) Handles NudMinimumAge.KeyUp

        If String.IsNullOrWhiteSpace(NudMinimumAge.Text) Then

            NudMinimumAge.ForeColor = Color.Red
            NudMinimumAge.Font = New System.Drawing.Font(NudMinimumAge.Font, FontStyle.Bold)
            _isMinimumAgeValid = False

            BtnSaveRate.Enabled = ChangeStateButtonSave()
            Exit Sub

        End If

        Dim typedValue As Decimal
        Decimal.TryParse(NormalizeMoneyText(NudMinimumAge.Text), typedValue)

        _isMinimumAgeValid = EvaluateNumericRangeLimits(NudMinimumAge, typedValue,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMinimumAge.Maximum)

        If _isMinimumAgeValid Then NudMinimumAge.Value = typedValue

        ValidateAgeRangeCoherence() ' Validación cruzada para el descuento por edad

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub


    Private Sub NudMaximumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMaximumAge.ValueChanged

        If _currentMode Is Nothing Then Exit Sub

        If CmbPaymentMethod.Text.Trim() = PaymentMethods.AgeDiscount Then UpdateDynamicTariffLabel()

        _isMaximumAgeValid = EvaluateNumericRangeLimits(NudMaximumAge, NudMaximumAge.Value,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMaximumAge.Maximum)

        ValidateAgeRangeCoherence() ' Validación cruzada para el descuento por edad

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub
    Private Sub NudMaximumAge_Enter(sender As Object, e As EventArgs) Handles NudMaximumAge.Enter
        NudMaximumAge.Select(0, NudMaximumAge.Text.Length)
    End Sub
    Private Sub NudMaximumAge_KeyUp(sender As Object, e As KeyEventArgs) Handles NudMaximumAge.KeyUp

        If String.IsNullOrWhiteSpace(NudMaximumAge.Text) Then

            NudMaximumAge.ForeColor = Color.Red
            NudMaximumAge.Font = New System.Drawing.Font(NudMaximumAge.Font, FontStyle.Bold)
            _isMaximumAgeValid = False

            BtnSaveRate.Enabled = ChangeStateButtonSave()
            Exit Sub

        End If

        Dim typedValue As Decimal
        Decimal.TryParse(NormalizeMoneyText(NudMaximumAge.Text), typedValue)

        _isMaximumAgeValid = EvaluateNumericRangeLimits(NudMaximumAge, typedValue,
                                                        MINIMUM_AGE_FOR_DISCOUNT, NudMaximumAge.Maximum)

        If _isMaximumAgeValid Then NudMaximumAge.Value = typedValue

        ValidateAgeRangeCoherence() ' Validación cruzada para el descuento por edad

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub


    Private Sub DgvPriceList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPriceList.CellContentClick
        '
    End Sub
    Private Sub DgvPriceList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPriceList.CellClick

        If e.RowIndex < 0 Then Exit Sub

        ' Extraemos el objeto de negocio completo directo de la memoria RAM.
        Dim selectedTariff = DirectCast(DgvPriceList.CurrentRow.DataBoundItem, TariffDTO)

        ' Captura de identificadores y estado de la tarifa seleccionada desde las propiedades del objeto
        _currentTariffId = selectedTariff.IdTariff
        _selectedTariffId = _currentTariffId

        ' Actualizamos los cuadros de texto con la info de la tarifa.
        TxtPrice.Text = selectedTariff.Price.ToString("C2")
        TxtTotal.Text = selectedTariff.Total.ToString("C2")
        TxtDiscount.Text = selectedTariff.Discount.ToString("C2")
        TxtToPay.Text = selectedTariff.TotalToPay.ToString("C2")

        LblPaymentMethod.Text = selectedTariff.PaymentMethod.ToString().Trim()
        NudMinimumAge.Value = selectedTariff.MinimumAge
        NudMaximumAge.Value = selectedTariff.MaximumAge
        NudNumberMembers.Value = selectedTariff.NumberMembers

    End Sub


    Private Sub BtnNewRate_Click(sender As Object, e As EventArgs) Handles BtnNewRate.Click

        ' El ID de la memoria RAM debe nacer en 0 pase lo que pase.
        _currentTariffId = 0

        ' 1. Establecemos el modo de la transacción actual
        _currentMode = TransactionMode.NewRecord

        ' 2. Limpieza absoluta de la interfaz y los datos temporales
        ClearInputControls()
        ResetStateVariables()

        ' 3. Transición visual del formulario al "Modo Edición"
        SetInterfaceVisualState(isEditing:=True)

        ' 4. Regla de Negocio Especial: Evaluar si es la primera tarifa del sistema
        If DgvPriceList.RowCount = 0 Then
            ApplyFirstTariffDefaultRules()
        Else
            CmbPaymentMethod.Focus()
        End If

    End Sub


    Private Sub BtnSaveRate_Click(sender As Object, e As EventArgs) Handles BtnSaveRate.Click

        Dim selectedMethod As String = CmbPaymentMethod.Text.Trim()
        Dim confirmationMessage As String

        ' 1. MENSAJE : Construir cuerpo del mensaje
        Select Case selectedMethod

            Case PaymentMethods.IndividualClasses
                confirmationMessage = $"La TARIFA fijada es de {TxtPrice.Text} por día.{vbCrLf}{vbCrLf}" &
                                  $"El precio fijado se usará en los pagos de las Clases Sueltas."

            Case PaymentMethods.AgeDiscount
                confirmationMessage = $"Se ha guardado la tarifa correctamente.{vbCrLf}{vbCrLf}" &
                                  $"El intervalo de edad es de {NudMinimumAge.Value} a {NudMaximumAge.Value} años."

            Case PaymentMethods.FamilyGroup
                confirmationMessage = $"El precio de la tarifa familiar es de {TxtToPay.Text}.{vbCrLf}{vbCrLf}" &
                                  $"El descuento aplicado para {NudNumberMembers.Value} personas es de {TxtDiscount.Text}.{vbCrLf}{vbCrLf}" &
                                  $"Se ha guardado la tarifa correctamente."

            Case PaymentMethods.MonthlyFeeSupplies
                confirmationMessage = $"El precio del bono se ha establecido en {TxtPrice.Text}.{vbCrLf}{vbCrLf}" &
                                  $"El bono incluye la mensualidad más implementos."

            Case Else
                If DgvPriceList.RowCount = 0 Then
                    confirmationMessage = $"La TARIFA fijada es de {TxtPrice.Text} mensuales.{vbCrLf}{vbCrLf}" &
                                      $"El precio se usará en todos los pagos de los clientes."
                Else
                    MsgBox($"No se puede guardar ninguna tarifa.{vbCrLf}{vbCrLf}Selecciona un Tipo de Pago de la lista.", vbCritical, "Tabla de precios y descuentos")
                    Exit Sub
                End If

        End Select

        ' 2. DETECCIÓN DE DUPLICADOS EN EL DATAGRIDVIEW : Si nos devuelve True, significa que ya existe,
        '    pintó la fila y debemos detener el guardado.
        If FindAndSelectRowByName(LblPaymentMethod.Text) Then

            MsgBox($"No se puede GUARDAR la nueva tarifa.{vbCrLf}{vbCrLf}" &
               $"Ya existe un registro con este nombre: {LblPaymentMethod.Text}{vbCrLf}{vbCrLf}" &
               $"Puedes ELIMINAR o MODIFICAR los datos del registro.", vbCritical, "Error de registro")

            TxtPrice.Focus()

            Exit Sub
        End If

        ' 3. GUARDAR USANDO DTO Y TARIFFMANAGER
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

            _currentTariffId = _tariffManager.Save(newTariffDto)

        Catch ex As Exception
            MsgBox($"Error al guardar en el sistema a través de TariffManager: {vbCrLf}{ex.Message}", vbCritical, "Error de Persistencia")
            Exit Sub
        End Try

        ' 4. REFRESCAR LA INTERFAZ DE USUARIO
        FetchAndRenderTariffsGridUI()

        FindAndSelectRowByName(LblPaymentMethod.Text)

        _currentMode = Nothing
        SetInterfaceVisualState(isEditing:=False)

        DisableInputControls()

        CmbPaymentMethod.Text = String.Empty

        MsgBox(confirmationMessage, vbInformation, "Tabla de precios y descuentos")

    End Sub


    Private Sub BtnModifyRate_Click(sender As Object, e As EventArgs) Handles BtnModifyRate.Click

        ' 1. COMPROBAR SI HAY REGISTRO SELECCIONADO
        If _currentTariffId = 0 Then
            MsgBox("Selecciona un registro de la lista para MODIFICAR.", vbCritical, "Verificar")
            DgvPriceList.Focus()
            Exit Sub
        End If

        ' 2. ENCHUFAMOS EL MODO EDICIÓN
        _currentMode = TransactionMode.EditRecord

        ' 3. EVALUAMOS EL MÉTODO DE PAGO USANDO TUS CONSTANTES DE NEGOCIO
        Dim currentPayment As String = LblPaymentMethod.Text.Trim()

        Select Case True

            Case currentPayment.StartsWith(PaymentMethods.Daily) OrElse
                currentPayment.StartsWith(PaymentMethods.MonthImp)
                TxtPrice.Enabled = True
                TxtPrice.Focus()

            Case currentPayment.StartsWith(PaymentMethods.AgeDscnt)
                TxtDiscount.Enabled = True
                TxtToPay.Enabled = True
                NudMinimumAge.Enabled = True
                NudMaximumAge.Enabled = True
                TxtDiscount.Focus()

                _tempAgeMin = Convert.ToInt32(NudMinimumAge.Value)
                _tempAgeMax = Convert.ToInt32(NudMaximumAge.Value)

            Case currentPayment.StartsWith(PaymentMethods.FmlGroup)
                TxtDiscount.Enabled = True
                TxtToPay.Enabled = True
                NudNumberMembers.Enabled = True
                TxtDiscount.Focus()

                _tempAgeMax = Convert.ToInt32(NudNumberMembers.Value)

            Case Else
                Dim response As MsgBoxResult = MsgBox("Vas a modificar el PRECIO FIJO GENERAL." & vbCr &
                                                     "Se modificarán todas las tarifas asociadas con el nuevo precio." & vbCr & vbCr &
                                                     "¿Estás seguro de modificar el precio fijo?",
                                                     vbQuestion + vbYesNo + vbDefaultButton2, "Advertencia")

                If response = vbYes Then
                    TxtPrice.Enabled = True
                    TxtPrice.Focus()
                Else
                    _currentMode = Nothing
                    Exit Sub
                End If

        End Select

        ' 4. CONTROL DE INTERFAZ Y BOTONES
        SetInterfaceVisualState(isEditing:=True)

    End Sub


    Private Sub BtnUpdateRate_Click(sender As Object, e As EventArgs) Handles BtnUpdateRate.Click
        '
    End Sub
    Private Sub BtnCancelRegistration_Click(sender As Object, e As EventArgs) Handles BtnCancelRegistration.Click
        '
    End Sub

    Private Sub BtnRemoveRate_Click(sender As Object, e As EventArgs) Handles BtnDeleteRate.Click
        '
    End Sub

    Private Sub BtnCloseWindow_Click(sender As Object, e As EventArgs) Handles BtnCloseWindow.Click
        '
    End Sub

#End Region


    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " 1. INICIALIZACIÓN Y ORQUESTACIÓN DE CARGA "
    ' Funciones mayores que coordinan la carga o preparación de datos general.


    ''' <summary>
    ''' Aplica las restricciones y configuraciones por defecto requeridas para la tarifa inicial del sistema.
    ''' </summary>
    Private Sub ApplyFirstTariffDefaultRules()

        CmbPaymentMethod.Enabled = False
        NudNumberMembers.Value = 1
        LblPaymentMethod.Text = PaymentMethods.Monthly
        TxtTotal.Clear()

        TxtPrice.Enabled = True
        TxtPrice.Text = "0"
        TxtPrice.Focus()

        _allowedPriceMin = MINIMUM_PRICE_LIMIT
        _allowedPriceMax = MAXIMUM_PRICE_LIMIT

        _isPriceValid = False   ' FALSE : El texto es "0" y debe cambiarlo obligatoriamente.
        _isDiscountValid = True ' TRUE : La primera mensualidad no lleva descuento.
        _isToPayValid = True    ' TRUE : No hay cálculos de total cruzados.

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub


    ''' <summary>
    ''' Restablece el estado de las variables internas utilizadas durante la transacción.
    ''' </summary>
    Private Sub ResetStateVariables()

        _selectedTariffId = 0
        _tempDiscount = 0
        _tempAgeMin = 0
        _tempAgeMax = 0

    End Sub


    ''' <summary>
    ''' Restaura los permisos de navegación en el formulario principal al cerrar la pantalla actual.
    ''' </summary>
    Private Sub RestorePrincipalFormNavigation()
        ' TODO: En el futuro esto debería sustituirse por una arquitectura basada en eventos (Events/Actions)
        If Not FrmMdiMain.BtnClientPayments.Enabled AndAlso DgvPriceList.RowCount > 0 Then
            FrmMdiMain.BtnClientPayments.Enabled = True
            FrmMdiMain.BtnOutstandingPayments.Enabled = True
        End If
    End Sub

#End Region

#Region " 2. CONFIGURACIÓN VISUAL (Estrategia UI) "
    ' Métodos dedicados exclusivamente a la cosmética y mutación de controles.

    ''' <summary>
    ''' Gestiona de forma centralizada la visibilidad y disponibilidad de los controles de la pantalla 
    ''' según el estado de la transacción actual.
    ''' </summary>
    ''' <param name="isEditing">TRUE si el formulario entra en modo Creación/Edición. FALSE para modo Consulta/Lectura.</param>
    Private Sub SetInterfaceVisualState(isEditing As Boolean)

        ' 1. EVALUAMOS LA EXISTENCIA DE DATOS EN LA TABLA
        ' Esto determina si se pueden pulsar los botones de Modificar/Eliminar en modo consulta
        Dim hasRows As Boolean = (DgvPriceList.RowCount > 0)

        ' 2. CONTROLES DE NAVEGACIÓN Y SELECCIÓN
        ' Si estamos editando, bloqueamos la grilla para evitar clics accidentales. 
        ' Si estamos en consulta, solo habilitamos la grilla si tiene filas.
        DgvPriceList.Enabled = If(isEditing, False, hasRows)
        CmbPaymentMethod.Enabled = isEditing

        ' 3. BOTONES DE ACCIÓN PRINCIPAL (Nuevo, Modificar, Eliminar)
        ' Aparecen en modo consulta (Modificar/Eliminar condicionados a si hay filas) y desaparecen al editar.
        BtnNewRate.Visible = Not isEditing
        BtnModifyRate.Visible = If(isEditing, False, hasRows)
        BtnDeleteRate.Visible = If(isEditing, False, hasRows)

        ' 4. BOTONES DE TRANSACCIÓN (Guardar, Actualizar, Cancelar)
        ' Aparecen únicamente cuando estamos editando o creando un registro
        BtnCancelRegistration.Visible = isEditing

        ' 🎯 El truco maestro para Guardar vs Actualizar:
        ' Evaluamos tu variable global _currentMode para saber exactamente cuál de los dos botones mostrar al editar
        If isEditing Then
            BtnSaveRate.Visible = (_currentMode = TransactionMode.NewRecord)
            BtnUpdateRate.Visible = (_currentMode = TransactionMode.EditRecord)
        Else
            ' Si no estamos editando, ambos botones transaccionales se ocultan
            BtnSaveRate.Visible = False
            BtnUpdateRate.Visible = False
        End If

        ' 5. FOCOS ESTATÉGICOS AUTOMÁTICOS
        ' Colocamos el foco inicial para que el usuario no tenga que usar el ratón
        If Not isEditing Then
            BtnNewRate.Focus()
        End If

    End Sub

    '''' <summary>
    '''' Cambia los controles de la interfaz al modo de consulta general, bloqueando ediciones inactivas.
    '''' </summary>
    'Private Sub ConfigureVisualStateForConsultation()

    '    CmbPaymentMethod.Enabled = False

    '    ' Control de visibilidad de botones (Modo Lectura)
    '    BtnNewRate.Visible = True
    '    BtnModifyRate.Visible = True
    '    BtnDeleteRate.Visible = True
    '    BtnSaveRate.Visible = False
    '    BtnUpdateRate.Visible = False
    '    BtnCancelRegistration.Visible = False

    '    ' Evaluamos si hay datos en la rejilla para permitir acciones de edición
    '    Dim hasRows As Boolean = DgvPriceList.RowCount > 0
    '    DgvPriceList.Enabled = hasRows
    '    BtnModifyRate.Visible = hasRows
    '    BtnDeleteRate.Visible = hasRows

    '    BtnNewRate.Focus()

    'End Sub


    '''' <summary>
    '''' Configura los componentes visuales para bloquear la rejilla y permitir la edición en los controles de entrada.
    '''' </summary>
    'Private Sub ConfigureVisualStateForEdition()

    '    ' Ocultamos acciones principales de lectura
    '    BtnNewRate.Visible = False
    '    BtnModifyRate.Visible = False
    '    BtnDeleteRate.Visible = False

    '    ' Mostramos los controladores de la transacción activa
    '    BtnSaveRate.Visible = True
    '    BtnSaveRate.Enabled = False
    '    BtnCancelRegistration.Visible = True

    '    ' Habilitamos selectores e inhabilitamos la tabla para evitar cambios de foco bruscos
    '    CmbPaymentMethod.Enabled = True
    '    DgvPriceList.Enabled = False

    'End Sub


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
    ''' Configura los controles visuales para la estrategia de Clases Sueltas (Diario).
    ''' </summary>
    Private Sub ConfigureDailyTariffUI()

        TxtPrice.Enabled = True
        TxtPrice.Text = 0
        TxtPrice.Focus()

        LblPaymentMethod.Text = PaymentMethods.Daily

        NudNumberMembers.Value = 1

        _isPriceValid = False   ' FALSE : Obliga a teclear un precio válido diferente de 0.
        _isDiscountValid = True ' TRUE : No aplica descuento.
        _isToPayValid = True    ' TRUE : No aplica total a pagar.

        BtnSaveRate.Enabled = ChangeStateButtonSave()

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

        _isPriceValid = True       ' TRUE : El precio base ya está establecido por el sistema.
        _isDiscountValid = False   ' FALSE : Obliga a ingresar un descuento válido.
        _isToPayValid = True       ' TRUE : Se evaluará cuando cambie el descuento o el total.
        _isMinimumAgeValid = True  ' TRUE : La edad mínima es 5 y es un valor correcto.
        _isMaximumAgeValid = False ' FALSE : La edad máxima es 5 igual que _isMinimumAgeValid y es un valor incorrecto.

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Grupo Familiar.
    ''' </summary>
    Private Sub ConfigureFamilyGroupTariffUI()

        TxtPrice.Text = _fixedMonthlyPrice.ToString("N2")

        TxtDiscount.Enabled = True
        TxtDiscount.Text = 0
        TxtDiscount.Focus()

        TxtToPay.Enabled = True

        LblPaymentMethod.Text = PaymentMethods.FmlGroup

        NudNumberMembers.Enabled = True
        NudNumberMembers.Minimum = 3
        NudNumberMembers.Value = 3

        _isPriceValid = True         ' TRUE : El precio base está validado.
        _isDiscountValid = False     ' FALSE : Exige interactuar o se calcule el rango correcto.
        _isToPayValid = True         ' TRUE : El precio a pagar ya está calculado.
        _isNumberMembersValid = True ' TRUE : Arranca con el valor por defecto "3 integrantes" que es válido.

        BtnSaveRate.Enabled = ChangeStateButtonSave()

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Mensualidad más Implementos.
    ''' </summary>
    Private Sub ConfigureMonthlyWithEquipmentTariffUI()

        TxtPrice.Enabled = True
        TxtPrice.Text = 0
        TxtPrice.Focus()

        LblPaymentMethod.Text = PaymentMethods.MonthImp

        NudNumberMembers.Value = 1

        _isPriceValid = False   ' FALSE : Obliga a teclear un precio válido diferente de 0.
        _isDiscountValid = True ' TRUE : No aplica descuento.
        _isToPayValid = True    ' TRUE : No aplica total a pagar.

        BtnSaveRate.Enabled = ChangeStateButtonSave()

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
    ''' Calcula dinámicamente los rangos de precio mínimos y máximos permitidos en función del tipo de pago seleccionado.
    ''' </summary>
    Private Sub CalculatePriceLimits()

        ' Regla del Sistema: Si es la primera tarifa o es la Mensualidad base, los límites son fijos
        If DgvPriceList.RowCount = 0 OrElse LblPaymentMethod.Text.Trim() = PaymentMethods.Monthly Then

            _allowedPriceMin = MINIMUM_PRICE_LIMIT
            _allowedPriceMax = MAXIMUM_PRICE_LIMIT

            Exit Sub

        End If

        ' Determinamos los límites comerciales según la selección del combo
        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.IndividualClasses '"CLASES SUELTAS"
                _allowedPriceMin = _fixedMonthlyPrice * INDIVIDUAL_CLASS_MIN_PCT
                _allowedPriceMax = _fixedMonthlyPrice * INDIVIDUAL_CLASS_MAX_PCT

            Case PaymentMethods.MonthlyFeeSupplies '"MENSUALIDAD + IMPLEMENTOS"
                _allowedPriceMin = _fixedMonthlyPrice + (_fixedMonthlyPrice / 2D)
                _allowedPriceMax = _fixedMonthlyPrice * 3D

            Case Else
                _allowedPriceMin = 0D
                _allowedPriceMax = _fixedMonthlyPrice

        End Select

    End Sub


    Private Sub CalculateDiscountLimits()

        ' Determinamos los límites del descuento según la selección del combo (Sustituye a "DSCTO" o "GRUPO")
        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.AgeDiscount '"DESCUENTO POR EDAD"

                _allowedDiscountMin = _fixedMonthlyPrice * AGE_DISCOUNT_MIN_PCT
                _allowedDiscountMax = _fixedMonthlyPrice * AGE_DISCOUNT_MAX_PCT

            Case PaymentMethods.FamilyGroup '"GRUPO FAMILIAR"

                Dim totalGroupBase As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                _allowedDiscountMin = totalGroupBase * FAMILY_GROUP_MIN_PCT
                _allowedDiscountMax = totalGroupBase * FAMILY_GROUP_MAX_PCT

            Case Else

                _allowedDiscountMin = 0D
                _allowedDiscountMax = 0D
        End Select

    End Sub


    Private Sub CalculateToPayLimits()

        Select Case CmbPaymentMethod.Text.Trim()

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
    ''' Actualiza la etiqueta informativa agregando el precio actual en tiempo real según el tipo de tarifa.
    ''' </summary>
    Private Sub UpdateDynamicTariffLabel()

        Dim prefix As String

        Select Case CmbPaymentMethod.Text.Trim()

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
        If CmbPaymentMethod.Text.Trim() = PaymentMethods.IndividualClasses OrElse
            CmbPaymentMethod.Text.Trim() = PaymentMethods.MonthlyFeeSupplies Then

            LblPaymentMethod.Text = $"{prefix} {currentPriceText}"
        Else
            LblPaymentMethod.Text = prefix

        End If

    End Sub


    Private Sub UpdateDiscountCalculationsAndTotals(currentDiscountValue As Decimal)

        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.AgeDiscount
                TxtToPay.Text = (_fixedMonthlyPrice - currentDiscountValue).ToString("C2")

            Case PaymentMethods.FamilyGroup
                Dim total As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                TxtTotal.Text = total.ToString("C2")

                Dim totalToPay As Decimal = total - currentDiscountValue
                TxtToPay.Text = totalToPay.ToString("C2")

            Case Else
                TxtToPay.Text = String.Empty
        End Select

    End Sub


    Private Sub UpdateToPayCalculationsAndDiscounts(currentToPayValue As Decimal)

        Select Case CmbPaymentMethod.Text.Trim()

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
            MessageBox.Show($"Error loading tariffs layout: {ex.Message}", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

#End Region

#Region " 4. ESTRUCTURAS Y ENUMS AUXILIARES "
    ' Tipos de datos personalizados que definen los estados y reglas del formulario.

    Public Enum TransactionMode
        NewRecord
        EditRecord
    End Enum

#End Region



    ''' <summary>
    ''' Busca una tarifa en el DataGridView por su nombre. Si la encuentra, la selecciona visualmente en pantalla.
    ''' </summary>
    ''' <param name="tariffName">Nombre de la tarifa a buscar (ej: LblPaymentMethod.Text)</param>
    ''' <returns>True si la tarifa ya existía en la lista; False si no se encontró.</returns>
    Private Function FindAndSelectRowByName(tariffName As String) As Boolean

        Try
            For Each row As DataGridViewRow In DgvPriceList.Rows

                If row.Cells("ColPaymentMethod").Value?.ToString() = tariffName Then

                    DgvPriceList.CurrentCell = row.Cells("ColPaymentMethod")
                    row.Selected = True
                    Return True

                End If
            Next

        Catch ex As Exception
            MsgBox($"Error visual al buscar la tarifa en la lista: {vbCrLf}{ex.Message}", vbCritical, "Error de Interfaz")
        End Try

        Return False

    End Function

    '=====================================================
    Private Function ChangeStateButtonSave() As Boolean

        ' El modo de transacción no está activo, protegemos el botón.
        If _currentMode Is Nothing Then Return False

        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.IndividualClasses  ' "CLASES SUELTAS"
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid

            Case PaymentMethods.AgeDiscount        ' "DESCUENTO POR EDAD"
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid AndAlso _isMinimumAgeValid AndAlso _isMaximumAgeValid

            Case PaymentMethods.FamilyGroup        ' "GRUPO FAMILIAR"
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid AndAlso _isNumberMembersValid

            Case PaymentMethods.MonthlyFeeSupplies ' "MENSUALIDAD + IMPLEMENTOS"
                Return _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid

            Case Else                              ' "TARIFA DESCONOCIDA O NEUTRA"
                Return False

        End Select

    End Function
    '=====================================================

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


End Class