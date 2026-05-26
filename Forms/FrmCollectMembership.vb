Imports GymPaymentControl.Constants
Imports GymPaymentControl.Interfaces
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services
Imports GymPaymentControl.UIHelpers
Imports MySql.Data.MySqlClient

Public Class FrmCollectMembership

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Reglas de Negocio Fijas (Configuración) ---
    Private Const MARGIN_DAILY As Decimal = 2D
    Private Const MARGIN_MONTHLY_GRUPAL As Decimal = 5D

    ' --- Servicios de Negocio (Managers) ---
    Private ReadOnly _paymentManager As New PaymentManager()

    ' --- Contexto de Datos Actual ---
    Private _selectedPayment As IPaymentCalculable
    Private _currentMode As TransactionMode

    ' --- Copias de Respaldo para Validación (Snapshot) ---
    Private _originalPrice As Decimal
    Private _originalDiscount As Decimal

    ' --- Banderas de Control de Flujo (UI Flags) ---
    Private _isLoading As Boolean = False
    Private _isUpdatedText As Boolean = False

#End Region

#Region " EVENTOS DEL FORMULARIO "
    Private Sub FrmCollectMembership_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CmbFrmPgs.Text = PaymentForms.Cash
    End Sub
    Private Sub FrmCollectMembership_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Me.Close()
    End Sub


    Private Sub DtpFdiPgs_ValueChanged(sender As Object, e As EventArgs) Handles DtpFdiPgs.ValueChanged

        ' Filtro de seguridad: si estamos cargando el form o no hay objeto, no hacemos nada
        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

        ' Sincronizamos la nueva fecha al objeto de negocio
        _selectedPayment.FdiPgs = DtpFdiPgs.Value

        ' Forzamos el recálculo
        HandleMoneyInputChanged(TxtPrcPgs, EventArgs.Empty)
        HandleMoneyInputChanged(TxtDscPgs, EventArgs.Empty)

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


    Private Sub ChkFdiPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFdiPgs.CheckedChanged

        ToggleControl(DtpFdiPgs, ChkFdiPgs, ToolTip,
                      "Desactiva la fecha de inicio del mes.", "Activa la fecha de inicio del mes.")
    End Sub
    Private Sub ChkFdpPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFdpPgs.CheckedChanged

        ToggleControl(DtpFdpPgs, ChkFdpPgs, ToolTip,
                      "Desactiva la fecha de pago.", "Activa la fecha de pago.")
    End Sub
    Private Sub ChkMtdPgs_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMtdPgs.CheckedChanged

        ToggleControl(CmbMtdPgs, ChkMtdPgs, ToolTip,
                      "Desactiva el método de pago.", "Activa el método de pago.")
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

            ' 2.1. Capturamos el método de pago del DTO actual
            Dim payMethod As String = _selectedPayment.MtdPgs.ToUpper()
            Dim isDaily As Boolean = payMethod.Contains(PaymentMethods.Daily)
            Dim isPaid As Boolean

            Using connection As New MySqlConnection(_paymentManager.ConnectionString)
                connection.Open()
                Dim generator As New PaymentGenerator()

                ' 2.2. Consultamos si ya existe el pago en la Base de Datos
                isPaid = generator.PaymentExists(connection, Nothing, DtpFdiPgs.Value, idClient, idGroup, isDaily)

                ' 2.3. Si el generador dice que ya existe, preparamos el mensaje según el tipo de pago
                If isPaid Then

                    Dim nameMonth As String = DtpFdiPgs.Value.ToString("MMMM").ToUpper
                    Dim year As Integer = DtpFdiPgs.Value.Year

                    Dim message As String = If(isDaily,
                        $"Ya existe un pago diario registrado para este cliente el día {DtpFdiPgs.Value.ToShortDateString()}.",
                        $"Ya existe un pago registrado para este periodo {nameMonth} de {year}.")

                    MessageBox.Show(message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

#End Region

    '| ============================================================ |'
    '|                FUNCIONES Y MÉTODOS AUXILIARES                |'
    '| ============================================================ |'

#Region " 1. CICLO DE VIDA E INICIALIZACIÓN "
    ' Son los métodos que "despiertan" la pantalla
    ' y preparan el escenario al cargar.

    ''' <summary>
    ''' Prepara e inicializa el formulario de cobro cargando
    ''' el contexto del pago, los controles visuales y el estado inicial
    ''' de cálculo según el tipo de transacción recibida.
    ''' </summary>
    ''' <param name="payment">
    ''' DTO que contiene la información del pago a procesar.
    ''' Puede representar pagos individuales o grupales.
    ''' </param>
    ''' <param name="mode">
    ''' Modo de transacción que determina el comportamiento
    ''' operativo del formulario.
    ''' </param>
    ''' <remarks>
    ''' Flujo interno:
    ''' 
    ''' 1. Activa el modo de carga para bloquear eventos secundarios.
    ''' 2. Inicializa el contexto interno del pago.
    ''' 3. Configura encabezados y controles visuales.
    ''' 4. Guarda los valores originales para validaciones futuras.
    ''' 5. Ajusta la interfaz según el método de pago.
    ''' 6. Inicializa los cálculos y el estado visual del formulario.
    ''' 
    ''' El modo de carga se restablece automáticamente mediante
    ''' un bloque Try / Finally para garantizar estabilidad.
    ''' </remarks>
    Public Sub PreparePayment(payment As IPaymentCalculable, mode As TransactionMode)

        ' Encendemos el escudo de carga para congelar eventos secundarios
        _isLoading = True

        Try
            ' Ejecutamos las tareas especializadas en estricto orden cronológico
            SetCurrentPaymentContext(payment, mode)
            ConfigureDisplayName()
            LoadPaymentControls()
            StoreOriginalValues()
            ConfigurePaymentMethodUI()

        Finally
            ' Apagamos el escudo de carga justo antes del disparo visual
            _isLoading = False

        End Try

        ' Despertamos al motor matemático y visual
        RefreshPaymentCalculation()

    End Sub


    ''' <summary>
    ''' Inicializa el estado del formulario cargando el DTO del pago y el modo de transacción.
    ''' </summary>
    Private Sub SetCurrentPaymentContext(payment As IPaymentCalculable, mode As TransactionMode)
        _currentMode = mode
        _selectedPayment = payment
    End Sub


    ''' <summary>
    ''' Asigna los valores iniciales de fecha y montos del DTO a los controles del formulario.
    ''' </summary>
    Private Sub LoadPaymentControls()
        DtpFdiPgs.Value = _selectedPayment.FdiPgs
        TxtPrcPgs.Text = $"{_selectedPayment.PrcPgs} €"
        TxtDscPgs.Text = $"{_selectedPayment.DscPgs} €"
    End Sub

    ''' <summary>
    ''' Guarda una copia inmutable de los montos iniciales para la posterior validación de rangos.
    ''' </summary>
    Private Sub StoreOriginalValues()
        _originalPrice = _selectedPayment.PrcPgs
        _originalDiscount = _selectedPayment.DscPgs
    End Sub


    ''' <summary>
    ''' Fuerza la ejecución de los eventos de cambio de texto para inicializar los cálculos y colores en pantalla.
    ''' </summary>
    Private Sub RefreshPaymentCalculation()
        HandleMoneyInputChanged(TxtPrcPgs, EventArgs.Empty)
        HandleMoneyInputChanged(TxtDscPgs, EventArgs.Empty)
    End Sub

#End Region

#Region " 2. CONFIGURACIÓN VISUAL DE TARIFAS (Estrategia UI) "
    ' Métodos dedicados exclusivamente a pintar la cabecera
    ' y el panel de ayuda comercial según el tipo de contrato.

    ''' <summary>
    ''' Configura la etiqueta de encabezado adaptando el nombre según si el pago es individual o grupal.
    ''' </summary>
    Private Sub ConfigureDisplayName()
        If TypeOf _selectedPayment Is GroupPaymentDTO Then
            Dim grpName As String = DirectCast(_selectedPayment, GroupPaymentDTO).GroupName
            LblDisplayName.Text = "GRUPO: " & If(Not String.IsNullOrEmpty(grpName), grpName, "FAMILIAR")
        Else
            LblDisplayName.Text = _selectedPayment.DisplayName
        End If
    End Sub


    ''' <summary>
    ''' Configura la interfaz visual relacionada con el método de pago
    ''' actual cargado en el DTO seleccionado.
    ''' </summary>
    ''' <remarks>
    ''' Esta función determina el tipo de contrato o modalidad de pago
    ''' asociada al cliente y adapta automáticamente:
    ''' 
    ''' - El valor mostrado en el ComboBox de método de pago.
    ''' - El texto descriptivo informativo.
    ''' - El color visual asociado al tipo de tarifa.
    ''' 
    ''' La configuración específica de cada modalidad se delega
    ''' en funciones especializadas para mantener el código modular
    ''' y fácil de mantener.
    ''' </remarks>
    Private Sub ConfigurePaymentMethodUI()

        Dim paymentMethod As String = _selectedPayment.MtdPgs

        Select Case True

            Case paymentMethod.Contains(PaymentMethods.Daily)
                ConfigureDailyPaymentUI()

            Case paymentMethod.Contains(PaymentMethods.Monthly)
                ConfigureMonthlyPaymentUI()

            Case paymentMethod.Contains(PaymentMethods.Grupal)
                ConfigureGroupPaymentUI()

            Case Else
                CmbMtdPgs.SelectedIndex = -1
                TxtDetailMethod.Text = String.Empty

        End Select

    End Sub


    ''' <summary>
    ''' Configura la interfaz gráfica con los textos descriptivos y estilos para la modalidad de pago diario.
    ''' </summary>
    Private Sub ConfigureDailyPaymentUI()

        CmbMtdPgs.Text = PaymentMethods.Daily
        TxtDetailMethod.Text = "CLASES SUELTAS : Pago por jornada individual."
        TxtDetailMethod.ForeColor = Color.DarkOrange

    End Sub


    ''' <summary>
    ''' Configura la interfaz gráfica con los textos descriptivos y estilos para la modalidad de pago mensual.
    ''' </summary>
    Private Sub ConfigureMonthlyPaymentUI()

        CmbMtdPgs.Text = PaymentMethods.Monthly

        If _selectedPayment.DscPgs = 0 Then
            TxtDetailMethod.Text = "TARIFA INDIVIDUAL : Sin Descuento."
        Else
            TxtDetailMethod.Text = $"TARIFA INDIVIDUAL : Con descuento aplicado por edad {_selectedPayment.DscPgs:C2}"
        End If

        TxtDetailMethod.ForeColor = Color.RoyalBlue

    End Sub


    ''' <summary>
    ''' Configura la interfaz gráfica con los textos descriptivos y estilos para la modalidad de pago grupal o familiar.
    ''' </summary>
    Private Sub ConfigureGroupPaymentUI()

        CmbMtdPgs.Text = PaymentMethods.FamilyGroup

        If TypeOf _selectedPayment Is IndividualPaymentDTO Then
            Dim ind = DirectCast(_selectedPayment, IndividualPaymentDTO)
            Dim nGrupo = If(Not String.IsNullOrEmpty(ind.GroupName), ind.GroupName, "un grupo familiar")
            TxtDetailMethod.Text = $"NOTA: Este cobro aplica tarifa reducida por pertenecer a: {nGrupo}."
        Else
            TxtDetailMethod.Text = "INTEGRANTES : " & _selectedPayment.Members
        End If

        TxtDetailMethod.ForeColor = Color.Indigo

    End Sub

#End Region

#Region " 3. CONTROLADORES DE EVENTOS (Handlers) "
    ' El punto de entrada de los componentes de Windows Forms cuando el usuario interactúa con ellos.
    ' Separarlo ayuda a identificar rápido qué eventos están mapeados.

    ''' <summary>
    ''' Procesa los cambios realizados en los controles monetarios,
    ''' calcula los rangos permitidos y actualiza el estado visual
    ''' y matemático del formulario.
    ''' </summary>
    ''' <remarks>
    ''' Esta función actúa como punto de entrada principal para los eventos
    ''' de cambio de texto asociados a los importes de precio y descuento.
    ''' 
    ''' Según el control que origine el evento:
    ''' - Determina el margen permitido.
    ''' - Calcula los límites válidos.
    ''' - Procesa la validación y actualización visual.
    ''' </remarks>
    Private Sub HandleMoneyInputChanged(sender As Object, e As EventArgs) _
        Handles TxtPrcPgs.TextChanged, TxtDscPgs.TextChanged

        '  Filtro de seguridad
        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

        Dim textBox As TextBox = TryCast(sender, TextBox)
        If textBox Is Nothing Then Exit Sub

        If textBox Is TxtPrcPgs Then
            ' Determinamos el margen según el método de pago
            Dim margin As Decimal = If(_selectedPayment.MtdPgs.Contains(PaymentMethods.Daily), MARGIN_DAILY, MARGIN_MONTHLY_GRUPAL)

            ' Obtenemos los límites usando la nueva función tupleada
            Dim range = CalculateAllowedRange(_originalPrice, margin)

            ProcessMoneyInputChange(textBox, Color.DarkOrange, range.Min, range.Max)

        ElseIf textBox Is TxtDscPgs Then
            Dim marginDsc As Decimal = 5D
            Dim rangeDsc = CalculateAllowedRange(_originalDiscount, marginDsc)

            ProcessMoneyInputChange(textBox, Color.DarkOrange, rangeDsc.Min, rangeDsc.Max)
        End If

    End Sub

#End Region

#Region " 4. PROCESAMIENTO, FORMATEO Y VALIDACIÓN DE ENTRADA "
    ' El núcleo duro del "Director de Orquesta" y sus micro-legos de limpieza,
    ' parsing y reglas de rangos numéricos.

    ''' <summary>
    ''' Procesa los cambios realizados en un TextBox monetario,
    ''' aplicando validación, formateo visual, sincronización de datos
    ''' y actualización de cálculos relacionados al pago actual.
    ''' </summary>
    ''' <param name="textBox">
    ''' Control TextBox que contiene el importe modificado por el usuario.
    ''' </param>
    ''' <param name="zeroColor">
    ''' Color utilizado cuando el valor ingresado es igual a cero.
    ''' </param>
    ''' <param name="minValue">
    ''' Valor mínimo permitido para el importe ingresado.
    ''' </param>
    ''' <param name="maxValue">
    ''' Valor máximo permitido para el importe ingresado.
    ''' </param>
    ''' <remarks>
    ''' Flujo interno del proceso:
    ''' 
    ''' 1. Desactiva temporalmente el evento TextChanged para evitar bucles.
    ''' 2. Normaliza y parsea el valor monetario ingresado.
    ''' 3. Aplica el formato visual de moneda (€).
    ''' 4. Valida el rango permitido del importe.
    ''' 5. Actualiza el estado visual de controles y etiquetas.
    ''' 6. Sincroniza el valor válido con el DTO actual.
    ''' 7. Recalcula los importes usando el motor PaymentCalculator.
    ''' 8. Refresca los resultados mostrados en pantalla.
    ''' 
    ''' La reactivación del evento TextChanged se garantiza mediante
    ''' un bloque Try / Finally.
    ''' </remarks>
    Private Sub ProcessMoneyInputChange(textBox As TextBox, zeroColor As Color,
                                        minValue As Decimal, maxValue As Decimal)

        ' Desactivamos temporalmente el evento para evitar bucles infinitos
        RemoveHandler textBox.TextChanged, AddressOf HandleMoneyInputChanged

        Try
            ' 1. PARSEAR: Extraemos el resultado numérico de forma pura
            Dim parseResult As MoneyParseResult = ParseMoneyInput(textBox.Text)

            ' 2. FORMATEAR TEXTBOX: Aplicamos el sufijo "€" y gestionamos el cursor
            ApplyMoneyTextboxFormat(textBox)

            ' 3. VALIDACIÓN DE FORMATO
            If Not parseResult.IsValid Then

                UpdateCalculationVisualState(False, "FORMATO", Color.DarkOrange)
                Exit Sub

            End If

            ' 4. VALIDACIÓN DE RANGO
            Dim isInRange As Boolean = ValidateMoneyRange(parseResult.Value, minValue, maxValue)

            ' 5. ESTADO VISUAL
            UpdateMoneyTextboxColor(textBox, isInRange, parseResult.Value, zeroColor)
            UpdateCalculationVisualState(isInRange)

            ' 6. NEGOCIO
            If isInRange Then

                SyncPaymentValueFromTextbox(textBox, parseResult.Value)
                UpdatePaymentCalculation()
                RefreshCalculationUI()

            End If

        Finally
            ' Reactivamos el evento antes de salir
            AddHandler textBox.TextChanged, AddressOf HandleMoneyInputChanged
        End Try

    End Sub


    ''' <summary>
    ''' Extrae y limpia el valor numérico de la cadena de texto sin interactuar con los controles.
    ''' </summary>
    Private Function ParseMoneyInput(text As String) As MoneyParseResult

        Dim raw As String = NormalizeMoneyText(text)

        If String.IsNullOrEmpty(raw) Then
            Return New MoneyParseResult(True, 0D)
        End If

        Dim value As Decimal
        Dim isValid As Boolean = Decimal.TryParse(raw, value)

        Return New MoneyParseResult(isValid, value)

    End Function


    ''' <summary>
    ''' Normaliza una cadena monetaria eliminando símbolos, espacios
    ''' y unificando el separador decimal para facilitar su conversión numérica.
    ''' </summary>
    ''' <param name="text">
    ''' Texto monetario ingresado por el usuario.
    ''' Puede contener el símbolo € y espacios adicionales.
    ''' </param>
    ''' <returns>
    ''' Cadena limpia y preparada para procesos de parseo decimal.
    ''' </returns>
    Private Function NormalizeMoneyText(text As String) As String
        Return text.Replace("€", "").Trim().Replace(".", ",")
    End Function


    ''' <summary>
    ''' Aplica el formato visual de moneda (€) al TextBox
    ''' manteniendo la posición del cursor.
    ''' </summary>
    Private Sub ApplyMoneyTextboxFormat(textBox As TextBox)

        Dim cursorPos As Integer = textBox.SelectionStart
        Dim raw As String = NormalizeMoneyText(textBox.Text)

        textBox.Text = $"{raw} €"
        textBox.SelectionStart = Math.Min(cursorPos, textBox.Text.Length)

    End Sub


    ''' <summary>
    ''' Calcula el rango mínimo y máximo permitido para un valor monetario
    ''' tomando como referencia un valor original y un margen configurable.
    ''' </summary>
    ''' <param name="originalValue">
    ''' Valor base utilizado como referencia para el cálculo.
    ''' </param>
    ''' <param name="margin">
    ''' Margen permitido de variación sobre el valor original.
    ''' </param>
    ''' <returns>
    ''' Una tupla con el valor mínimo y máximo permitido.
    ''' </returns>
    Private Function CalculateAllowedRange(originalValue As Decimal, margin As Decimal) _
        As (Min As Decimal, Max As Decimal)

        Dim minAllowed As Decimal = Math.Max(0D, originalValue - margin)
        Dim maxAllowed As Decimal = originalValue + margin
        Return (minAllowed, maxAllowed)

    End Function


    ''' <summary>
    ''' Valida si un importe decimal
    ''' se encuentra dentro del rango mínimo y máximo permitido.
    ''' </summary>
    Private Function ValidateMoneyRange(value As Decimal,
                                        minValue As Decimal,
                                        maxValue As Decimal) As Boolean
        Return value >= minValue AndAlso value <= maxValue
    End Function


    ''' <summary>
    ''' Actualiza el color de fuente del TextBox
    ''' según el estado de la validación y su valor.
    ''' </summary>
    Private Sub UpdateMoneyTextboxColor(textBox As TextBox, isInRange As Boolean,
                                        value As Decimal, zeroColor As Color)
        If Not isInRange Then
            textBox.ForeColor = Color.Red

        ElseIf value = 0D Then
            textBox.ForeColor = zeroColor

        Else
            textBox.ForeColor = Color.Green

        End If
    End Sub

#End Region

#Region " 5. SINCRONIZACIÓN, NEGOCIO Y RENDERIZADO DE CÁLCULOS "

    ''' <summary>
    ''' Sincroniza el valor numérico limpio directamente en la propiedad
    ''' correspondiente del DTO actual.
    ''' </summary>
    Private Sub SyncPaymentValueFromTextbox(textBox As TextBox, value As Decimal)

        If textBox Is TxtPrcPgs Then
            _selectedPayment.PrcPgs = value

        ElseIf textBox Is TxtDscPgs Then
            _selectedPayment.DscPgs = value

        End If

    End Sub


    ''' <summary>
    ''' Actualiza el cálculo interno del pago seleccionado utilizando
    ''' los valores actuales de la interfaz y el motor de cálculo.
    ''' </summary>
    ''' <remarks>
    ''' Esta función:
    ''' 
    ''' 1. Verifica que el formulario no esté en modo carga.
    ''' 2. Sincroniza la fecha seleccionada con el DTO actual.
    ''' 3. Ejecuta el cálculo centralizado del pago mediante PaymentCalculator.
    ''' 
    ''' El resultado matemático queda almacenado directamente
    ''' en el objeto _selectedPayment.
    ''' </remarks>
    Private Sub UpdatePaymentCalculation()

        If _isLoading OrElse _selectedPayment Is Nothing Then Exit Sub

        _selectedPayment.FdiPgs = DtpFdiPgs.Value
        PaymentCalculator.CalculatePaymentAmount(_selectedPayment)

    End Sub


    ''' <summary>
    ''' Toma los valores matemáticos procesados dentro del DTO
    ''' y los plasma en las etiquetas en formato moneda.
    ''' </summary>
    Private Sub RefreshCalculationUI()

        With _selectedPayment
            LblTotal.Text = .Total.ToString("C2")
            LblNumberOfDays.Text = .DaysOfMonth.ToString()
            LblTotalToPay.Text = .TotalToPay.ToString("C2")

            Dim priceDay As Decimal = CalculateDailyPrice(.Total, .FdiPgs)
            LblPriceDay.Text = priceDay.ToString("C2")
        End With

    End Sub


    ''' <summary>
    ''' Calcula el precio proporcional por día basado en el total del pago
    ''' y el mes de la fecha asignada.
    ''' </summary>
    Private Function CalculateDailyPrice(totalAmount As Decimal,
                                         paymentDate As DateTime) As Decimal

        Dim daysInMonth As Integer = DateTime.DaysInMonth(paymentDate.Year, paymentDate.Month)
        If daysInMonth = 0 Then Return 0D
        Return totalAmount / daysInMonth

    End Function


    ''' <summary>
    ''' Actualiza el estado visual de las etiquetas de cálculo
    ''' según el resultado de la validación.
    ''' </summary>
    Private Sub UpdateCalculationVisualState(isValid As Boolean,
                                             Optional errorText As String = "ERROR",
                                             Optional errorColor As Color = Nothing)

        If errorColor = Nothing Then errorColor = Color.Red

        If Not isValid Then

            UpdateLabelsState({LblTotal, LblPriceDay, LblTotalToPay},
                              errorColor, errorText)
        Else

            UpdateLabelsState({LblTotal, LblPriceDay}, Color.Green)
            UpdateLabelsState({LblTotalToPay}, Color.Black)

        End If

    End Sub

#End Region

#Region " ESTRUCTURAS Y HELPERS PRIVADOS "

    ''' <summary>
    ''' Define las modalidades de transacción permitidas al gestionar un pago en el formulario.
    ''' </summary>
    Public Enum TransactionMode
        ''' <summary>Representa el flujo para la creación de un nuevo registro de pago.</summary>
        NewPayment
        ''' <summary>Representa el flujo para la modificación de un pago ya existente.</summary>
        UpdatePayment
    End Enum


    ''' <summary>
    ''' Representa el resultado encapsulado de intentar parsear una cadena de texto monetaria.
    ''' </summary>
    Private Structure MoneyParseResult
        Public ReadOnly Property IsValid As Boolean
        Public ReadOnly Property Value As Decimal

        Public Sub New(isValid As Boolean, value As Decimal)
            Me.IsValid = isValid
            Me.Value = value
        End Sub
    End Structure

#End Region

End Class