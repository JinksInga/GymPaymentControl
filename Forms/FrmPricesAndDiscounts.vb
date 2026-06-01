Imports System.ComponentModel
Imports GymPaymentControl.Constants
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports GymPaymentControl.Utils

Public Class FrmPricesAndDiscounts

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Servicios de Negocio (Managers) ---
    Private ReadOnly _tariffManager As New TariffManager()

    ' --- Modo de Transacción y Control de Flujo ---
    Private _currentMode As TransactionMode?
    Private _selectedTariffId As Integer

    ' --- Variables de estado para el control de errores lógicos ---
    Private _isPriceValid As Boolean = False  ' Nace en False para obligar a validar el precio
    Private _isDiscountValid As Boolean = True ' Nace en True para que no bloquee el inicio si no hay descuento
    Private _isToPayValid As Boolean = True ' Nace en True por defecto para tarifas normales

    ' --- Valores de Reglas de Negocio Comerciales ---
    Private _currentPrice As Decimal
    Private _currentDiscount As Decimal
    Private _currentToPay As Decimal

    Private _fixedMonthlyPrice As Decimal

    Private _allowedPriceMin As Decimal
    Private _allowedPriceMax As Decimal
    Private _allowedDiscountMin As Decimal
    Private _allowedDiscountMax As Decimal
    Private _allowedToPayMin As Decimal
    Private _allowedToPayMax As Decimal

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
        ConfigureVisualStateForConsultation()

    End Sub


    Private Sub CmbPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPaymentMethod.SelectedIndexChanged

        ' 1. Reseteo y limpieza base ante cualquier cambio de selección
        ClearInputControls()
        ResetStateVariables()
        DisableInputControls()

        ' 2. Orquestación semántica según la selección
        Select Case CmbPaymentMethod.Text.Trim().ToUpper()

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
            BtnSaveRate.Enabled = _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid

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
            Dim cleanText As String = NormalizeMoneyText(TxtDiscount.Text)

            ' Ponemos el formato de moneda automáticamente.
            ApplyMoneyTextboxFormat(TxtDiscount)

            ' Parseo numérico seguro
            Dim currentDiscountValue As Decimal
            Dim isDecimalValid As Boolean = Decimal.TryParse(cleanText, currentDiscountValue)

            ' Calculamos los límites Minimo y Maximo del descuento.
            CalculateDiscountLimits()

            ' Calcula el total a pagar según el tipo de pago
            UpdateDiscountCalculationsAndTotals(currentDiscountValue)

            ' El precio solo es válido si es un número real Y ADEMÁS está dentro de los límites.
            _isDiscountValid = isDecimalValid AndAlso
                               EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue, _allowedDiscountMin, _allowedDiscountMax)

            ' El botón de guardar solo se encenderá si el precio es correcto Y ADEMÁS el descuento es correcto.
            BtnSaveRate.Enabled = _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid

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

        'Limpia el " €" visual para que el usuario escriba cómodo y selecciona todo.
        Dim cleanText As String = TxtDiscount.Text.Replace("€", "").Trim()
        Dim value As Decimal = 0D

        If Decimal.TryParse(cleanText, value) Then
            TxtDiscount.Text = If(value = 0D, String.Empty, value.ToString("N2"))
        End If

        TxtDiscount.SelectAll()

    End Sub
    Private Sub TxtDiscount_Leave(sender As Object, e As EventArgs) Handles TxtDiscount.Leave

        ' Si se quedó vacío o solo con la coma por error, lo auto-repara a un bonito "0,00 €"
        Dim cleanText As String = NormalizeMoneyText(TxtDiscount.Text)

        If String.IsNullOrWhiteSpace(cleanText) OrElse cleanText = "," Then
            TxtDiscount.Text = "0,00 €"
            TxtToPay.Text = ""
        End If

    End Sub


    Private Sub TxtToPay_TextChanged(sender As Object, e As EventArgs) Handles TxtToPay.TextChanged

        ' Si no estamos editando o creando, ignoramos el evento
        If _currentMode Is Nothing Then Exit Sub

        ' Desconectamos temporalmente para evitar el bucle infinito al formatear y calcular a la inversa
        RemoveHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged
        RemoveHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged

        Try
            ' Extraemos el texto limpio y aplicamos el formato visual de moneda.
            Dim cleanText As String = NormalizeMoneyText(TxtToPay.Text)
            ApplyMoneyTextboxFormat(TxtToPay)

            ' Parseo numérico seguro
            Dim currentToPayValue As Decimal
            Dim isDecimalValid As Boolean = Decimal.TryParse(cleanText, currentToPayValue)

            ' Calculamos los límites inversos y el descuento resultante
            CalculateToPayLimits()

            ' Calcula el total a pagar según el tipo de pago
            UpdateToPayCalculationsAndDiscounts(currentToPayValue)

            ' Evaluamos los rangos comerciales permitidos para el total a pagar
            _isToPayValid = isDecimalValid AndAlso
                            EvaluateNumericRangeLimits(TxtToPay, currentToPayValue, _allowedToPayMin, _allowedToPayMax)

            ' El botón de guardar vigila el precio, el descuento Y el total a pagar.
            BtnSaveRate.Enabled = _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid

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

        Dim cleanText As String = TxtToPay.Text.Replace("€", "").Trim()
        Dim value As Decimal = 0D

        If Decimal.TryParse(cleanText, value) Then
            TxtToPay.Text = If(value = 0D, String.Empty, value.ToString("N2"))
        End If

        TxtToPay.SelectAll()

    End Sub
    Private Sub TxtToPay_Leave(sender As Object, e As EventArgs) Handles TxtToPay.Leave

        Dim cleanText As String = NormalizeMoneyText(TxtToPay.Text)

        If String.IsNullOrWhiteSpace(cleanText) OrElse cleanText = "," Then
            TxtToPay.Text = "0,00 €"
            TxtDiscount.Text = ""
        End If

    End Sub


    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged
        ' 🛡️ Guarda de seguridad: Si no estamos editando o creando, ignoramos el evento
        If _currentMode Is Nothing Then Exit Sub

        ' 🔌 OJO: Como este control va a provocar que se recalculen los descuentos y totales,
        ' apagamos temporalmente los manejadores de las cajas de dinero para que no salten por error
        RemoveHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
        RemoveHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged

        Try
            ' 1. Validamos que la tarifa actual del combo sea la de Grupo Familiar
            If CmbPaymentMethod.Text.Trim() = PaymentMethods.FamilyGroup Then

                ' 2. Actualizamos la etiqueta informativa (Usando tu función ya simplificada)
                UpdateDynamicTariffLabel()

                ' 3. Extraemos el valor actual del descuento en formato numérico para no perderlo
                Dim cleanText As String = NormalizeMoneyText(TxtDiscount.Text)
                Dim currentDiscountValue As Decimal
                Decimal.TryParse(cleanText, currentDiscountValue)

                ' 4. 🧮 RECALCULAMOS TODO EN CADENA:
                ' Al cambiar el número de personas, cambian los límites del descuento, 
                ' cambian las matemáticas finales y cambia el total a pagar.
                CalculateDiscountLimits()
                UpdateDiscountCalculationsAndTotals(currentDiscountValue)
                CalculateToPayLimits()

                ' 5. 🛡️ RE-EVALUAMOS LA SEGURIDAD: 
                ' Comprobamos si el descuento sigue siendo válido con los nuevos límites de personas
                _isDiscountValid = EvaluateNumericRangeLimits(TxtDiscount, currentDiscountValue,
                                                              _allowedDiscountMin, _allowedDiscountMax)

                ' 🔓 EL CANDADO DEL BOTÓN DIRECTO Y A LA VISTA:
                BtnSaveRate.Enabled = _isPriceValid AndAlso _isDiscountValid AndAlso _isToPayValid
            End If

        Finally
            ' 🔌 Reconectamos siempre los interruptores al salir
            AddHandler TxtToPay.TextChanged, AddressOf TxtToPay_TextChanged
            AddHandler TxtDiscount.TextChanged, AddressOf TxtDiscount_TextChanged
        End Try
    End Sub
    Private Sub NudNumberMembers_Enter(sender As Object, e As EventArgs) Handles NudNumberMembers.Enter
        ' Selecciona el texto completo del control (desde la posición 0 hasta el final)
        NudNumberMembers.Select(0, NudNumberMembers.Text.Length)
    End Sub


    Private Sub NudMinimumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMinimumAge.ValueChanged
        If _currentMode Is Nothing Then Exit Sub
        If CmbPaymentMethod.Text.Trim() = PaymentMethods.AgeDiscount Then UpdateDynamicTariffLabel()
    End Sub
    Private Sub NudMinimumAge_Enter(sender As Object, e As EventArgs) Handles NudMinimumAge.Enter
        NudMinimumAge.Select(0, NudMinimumAge.Text.Length)
    End Sub


    Private Sub NudMaximumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMaximumAge.ValueChanged
        If _currentMode Is Nothing Then Exit Sub
        If CmbPaymentMethod.Text.Trim() = PaymentMethods.AgeDiscount Then UpdateDynamicTariffLabel()
    End Sub
    Private Sub NudMaximumAge_Enter(sender As Object, e As EventArgs) Handles NudMaximumAge.Enter
        NudMaximumAge.Select(0, NudMaximumAge.Text.Length)
    End Sub


    Private Sub DgvPriceList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPriceList.CellContentClick
        '
    End Sub

    Private Sub BtnNewRate_Click(sender As Object, e As EventArgs) Handles BtnNewRate.Click

        ' 1. Establecemos el modo de la transacción actual
        _currentMode = TransactionMode.NewRecord

        ' 2. Limpieza absoluta de la interfaz y los datos temporales
        ClearInputControls()
        ResetStateVariables()

        ' 3. Transición visual del formulario al "Modo Edición"
        ConfigureVisualStateForEdition()

        ' 4. Regla de Negocio Especial: Evaluar si es la primera tarifa del sistema
        If DgvPriceList.RowCount = 0 Then
            ApplyFirstTariffDefaultRules()
        Else
            CmbPaymentMethod.Focus()
        End If

    End Sub

    Private Sub BtnSaveRate_Click(sender As Object, e As EventArgs) Handles BtnSaveRate.Click
        '
    End Sub

    Private Sub BtnUpdateRate_Click(sender As Object, e As EventArgs) Handles BtnUpdateRate.Click
        '
    End Sub

    Private Sub BtnModifyRate_Click(sender As Object, e As EventArgs) Handles BtnModifyRate.Click
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
        TxtPrice.Focus()

        ' Configuración de límites o parámetros comerciales iniciales
        _allowedPriceMin = 10D
        _allowedPriceMax = 90D

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
    ''' Cambia los controles de la interfaz al modo de consulta general, bloqueando ediciones inactivas.
    ''' </summary>
    Private Sub ConfigureVisualStateForConsultation()

        CmbPaymentMethod.Enabled = False

        ' Control de visibilidad de botones (Modo Lectura)
        BtnNewRate.Visible = True
        BtnModifyRate.Visible = True
        BtnDeleteRate.Visible = True
        BtnSaveRate.Visible = False
        BtnUpdateRate.Visible = False
        BtnCancelRegistration.Visible = False

        ' Evaluamos si hay datos en la rejilla para permitir acciones de edición
        Dim hasRows As Boolean = DgvPriceList.RowCount > 0
        DgvPriceList.Enabled = hasRows
        BtnModifyRate.Visible = hasRows
        BtnDeleteRate.Visible = hasRows

        BtnNewRate.Focus()

    End Sub


    ''' <summary>
    ''' Configura los componentes visuales para bloquear la rejilla y permitir la edición en los controles de entrada.
    ''' </summary>
    Private Sub ConfigureVisualStateForEdition()

        ' Ocultamos acciones principales de lectura
        BtnNewRate.Visible = False
        BtnModifyRate.Visible = False
        BtnDeleteRate.Visible = False

        ' Mostramos los controladores de la transacción activa
        BtnSaveRate.Visible = True
        BtnCancelRegistration.Visible = True

        ' Habilitamos selectores e inhabilitamos la tabla para evitar cambios de foco bruscos
        CmbPaymentMethod.Enabled = True
        DgvPriceList.Enabled = False

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
    ''' Configura los controles visuales para la estrategia de Clases Sueltas (Diario).
    ''' </summary>
    Private Sub ConfigureDailyTariffUI()

        NudNumberMembers.Value = 1
        TxtTotal.Clear()
        LblPaymentMethod.Text = PaymentMethods.Daily

        TxtPrice.Enabled = True
        TxtPrice.Focus()

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Descuentos por Edad.
    ''' </summary>
    Private Sub ConfigureAgeDiscountUI()

        ' Asignamos el valor numérico de respaldo (puedes usar ToString si tus cajas manejan texto base)
        TxtPrice.Text = _fixedMonthlyPrice.ToString("N2")
        TxtTotal.Text = _fixedMonthlyPrice.ToString("N2")

        TxtDiscount.Enabled = True
        TxtToPay.Enabled = True

        NudNumberMembers.Value = 1
        NudMinimumAge.Enabled = True
        NudMaximumAge.Enabled = True

        LblPaymentMethod.Text = PaymentMethods.AgeDscnt '"DSCTO EDAD"

        TxtDiscount.Focus()

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Grupo Familiar.
    ''' </summary>
    Private Sub ConfigureFamilyGroupTariffUI()

        TxtPrice.Text = _fixedMonthlyPrice.ToString("N2")

        TxtDiscount.Enabled = True
        TxtToPay.Enabled = True

        NudNumberMembers.Enabled = True
        NudNumberMembers.Value = 1

        LblPaymentMethod.Text = PaymentMethods.FmlGroup '"GRUPO FAM"

        NudNumberMembers.Focus()

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Mensualidad más Implementos.
    ''' </summary>
    Private Sub ConfigureMonthlyWithEquipmentTariffUI()

        NudNumberMembers.Value = 1

        TxtTotal.Clear()

        LblPaymentMethod.Text = PaymentMethods.MonthImp '"MES + IMPLE"

        TxtPrice.Enabled = True
        TxtPrice.Focus()

    End Sub


    ''' <summary>
    ''' Limpia todas las cajas de texto y restablece los selectores numéricos a sus valores base.
    ''' </summary>
    Private Sub ClearInputControls()

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

            _allowedPriceMin = 10D
            _allowedPriceMax = 90D

            Exit Sub

        End If

        ' Determinamos los límites comerciales según la selección del combo
        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.IndividualClasses '"CLASES SUELTAS"
                _allowedPriceMin = _fixedMonthlyPrice * 0.1D
                _allowedPriceMax = _fixedMonthlyPrice * 0.3D

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

                _allowedDiscountMin = _fixedMonthlyPrice * 0.1D
                _allowedDiscountMax = _fixedMonthlyPrice * 0.4D

            Case PaymentMethods.FamilyGroup '"GRUPO FAMILIAR"

                Dim totalGroupBase As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                _allowedDiscountMin = totalGroupBase * 0.05D
                _allowedDiscountMax = totalGroupBase * 0.25D

            Case Else

                _allowedDiscountMin = 0D
                _allowedDiscountMax = 0D
        End Select

    End Sub


    Private Sub CalculateToPayLimits()

        Select Case CmbPaymentMethod.Text.Trim()
            Case PaymentMethods.AgeDiscount
                _allowedToPayMin = _fixedMonthlyPrice - (_fixedMonthlyPrice * 0.4D)
                _allowedToPayMax = _fixedMonthlyPrice - (_fixedMonthlyPrice * 0.1D)

            Case PaymentMethods.FamilyGroup
                Dim totalGroupBase As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                _allowedToPayMin = totalGroupBase - (totalGroupBase * 0.25D)
                _allowedToPayMax = totalGroupBase - (totalGroupBase * 0.05D)

            Case Else
                _allowedToPayMin = 0D
                _allowedToPayMax = _fixedMonthlyPrice

        End Select

    End Sub


    ''' <summary>
    ''' Actualiza la etiqueta informativa agregando el precio actual en tiempo real según el tipo de tarifa.
    ''' </summary>
    Private Sub UpdateDynamicTariffLabel()
        Dim prefix As String = String.Empty

        ' 1. Averiguamos el texto base según el combo (Un solo Select)
        Select Case CmbPaymentMethod.Text.Trim()

            Case PaymentMethods.IndividualClasses
                prefix = PaymentMethods.Daily '"DIARIO"

            Case PaymentMethods.MonthlyFeeSupplies
                prefix = PaymentMethods.MonthImp '"MES + IMPLE"

            Case PaymentMethods.AgeDiscount
                prefix = $"{PaymentMethods.AgeDscnt} {NudMinimumAge.Value}-{NudMaximumAge.Value}" '"DSCTO EDAD"

            Case PaymentMethods.FamilyGroup
                prefix = $"{PaymentMethods.FmlGroup} {NudNumberMembers.Value}" '"GRUPO FAM"

            Case Else
                prefix = PaymentMethods.Monthly

        End Select

        ' Obtenemos el texto del precio y le BORRAMOS el símbolo de euro.
        Dim currentPriceText As String = TxtPrice.Text.Replace("€", "").Trim()

        ' Si está vacío o es cero, mostramos solo el prefijo.
        If String.IsNullOrEmpty(currentPriceText) OrElse currentPriceText = "0" OrElse currentPriceText = "0,00" Then
            LblPaymentMethod.Text = prefix
            Exit Sub

        End If

        ' Solo las clases sueltas y mensualidad+implementos muestran el precio en tu lógica
        If CmbPaymentMethod.Text.Trim() = PaymentMethods.IndividualClasses OrElse
            CmbPaymentMethod.Text.Trim() = PaymentMethods.MonthlyFeeSupplies Then

            LblPaymentMethod.Text = $"{prefix} {currentPriceText}"
        Else
            LblPaymentMethod.Text = prefix

        End If

    End Sub


    Private Sub UpdateDiscountCalculationsAndTotals(currentDiscountValue As Decimal)

        ' Evaluamos directamente el ComboBox para calcular lo que el cliente tiene que pagar
        Select Case CmbPaymentMethod.Text.Trim()'.ToUpper()

            Case PaymentMethods.AgeDiscount '"DESCUENTO POR EDAD"

                'Dim totalToPay As Decimal = 
                TxtToPay.Text = (_fixedMonthlyPrice - currentDiscountValue).ToString("C2")

            Case PaymentMethods.FamilyGroup '"GRUPO FAMILIAR"

                Dim total As Decimal = _fixedMonthlyPrice * NudNumberMembers.Value
                TxtTotal.Text = total.ToString("C2")

                Dim totalToPay As Decimal = total - currentDiscountValue
                TxtToPay.Text = totalToPay.ToString("C2")

            Case Else
                ' Si es otra tarifa sin descuentos estructurados, limpiamos la caja del total a pagar
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

                'Case Else
                ' Si no aplica, no alteramos la caja del descuento
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
            ' 1. 📥 NEGOCIO: Solicitamos los datos puros al mánager experto
            Dim tariffsList As List(Of TariffDTO) = _tariffManager.FetchAllTariffs()

            ' 2. 🧮 REGLA DE NEGOCIO ANTIGUA: Capturar el precio de la tarifa fija mes (Id = 1)
            ' En lugar de evaluar fila a fila en un bucle visual, usamos una consulta LINQ semántica muy elegante
            Dim fixedMonthlyTariff = tariffsList.FirstOrDefault(Function(t) t.Id = 1)

            If fixedMonthlyTariff IsNot Nothing Then
                _fixedMonthlyPrice = fixedMonthlyTariff.Price
            End If

            ' 3. 🎨 INTERFAZ: Limpiamos y enlazamos la lista directamente a la cuadrícula (Grid)
            DgvPriceList.DataSource = Nothing ' Rompemos cualquier enlace antiguo para refrescar de forma segura
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
        UpdateRecord
    End Enum

#End Region


End Class