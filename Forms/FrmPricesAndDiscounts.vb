
Imports System.ComponentModel
Imports GymPaymentControl.Models
Imports GymPaymentControl.Services

Public Class FrmPricesAndDiscounts

#Region " VARIABLES DE ESTADO Y CONSTANTES "

    ' --- Servicios de Negocio (Managers) ---
    Private ReadOnly _tariffManager As New TariffManager()

    ' --- Modo de Transacción y Control de Flujo ---
    Private _currentMode As TransactionMode
    Private _selectedTariffId As Integer

    ' --- Valores de Reglas de Negocio Comerciales ---
    Private _fixedMonthlyPrice As Decimal
    Private _allowedPriceMin As Decimal
    Private _allowedPriceMax As Decimal

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
        InitializeFormContext()
    End Sub


    Private Sub CmbPaymentMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbPaymentMethod.SelectedIndexChanged

        ' 1. Reseteo y limpieza base ante cualquier cambio de selección
        ClearInputControls()
        ResetStateVariables()
        DisableInputControls()

        ' 2. Orquestación semántica según la selección
        Select Case CmbPaymentMethod.Text.Trim().ToUpper()

            Case "CLASES SUELTAS"
                ConfigureDailyTariffUI()

            Case "DESCUENTO POR EDAD"
                ConfigureAgeDiscountUI()

            Case "GRUPO FAMILIAR"
                ConfigureFamilyGroupTariffUI()

            Case "MENSUALIDAD + IMPLEMENTOS"
                ConfigureMonthlyWithEquipmentTariffUI()

        End Select

    End Sub

    Private Sub TxtPrice_TextChanged(sender As Object, e As EventArgs) Handles TxtPrice.TextChanged
        '
    End Sub

    Private Sub TxtTotal_TextChanged(sender As Object, e As EventArgs) Handles TxtTotal.TextChanged
        '
    End Sub

    Private Sub TxtDiscount_TextChanged(sender As Object, e As EventArgs) Handles TxtDiscount.TextChanged
        '
    End Sub

    Private Sub TxtToPay_TextChanged(sender As Object, e As EventArgs) Handles TxtToPay.TextChanged
        '
    End Sub

    Private Sub LblPaymentMethod_Click(sender As Object, e As EventArgs) Handles LblPaymentMethod.Click
        '
    End Sub

    Private Sub NudNumberMembers_ValueChanged(sender As Object, e As EventArgs) Handles NudNumberMembers.ValueChanged
        '
    End Sub

    Private Sub NudMinimumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMinimumAge.ValueChanged
        '
    End Sub

    Private Sub NudMaximumAge_ValueChanged(sender As Object, e As EventArgs) Handles NudMaximumAge.ValueChanged
        '
    End Sub

    Private Sub DgvPriceList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvPriceList.CellContentClick
        '
    End Sub

    Private Sub BtnNewRate_Click(sender As Object, e As EventArgs) Handles BtnNewRate.Click
        '
        PrepareFormForNewRecord()
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
    ''' Inicializa los datos de la pantalla y el estado visual por defecto.
    ''' </summary>
    Private Sub InitializeFormContext()

        FetchAndRenderTariffsGridUI()
        ConfigureVisualStateForConsultation()

    End Sub


    ''' <summary>
    ''' Orquesta el flujo completo para iniciar la creación de una nueva tarifa o descuento.
    ''' </summary>
    Private Sub PrepareFormForNewRecord()

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


    ''' <summary>
    ''' Aplica las restricciones y configuraciones por defecto requeridas para la tarifa inicial del sistema.
    ''' </summary>
    Private Sub ApplyFirstTariffDefaultRules()

        CmbPaymentMethod.Enabled = False
        NudNumberMembers.Value = 1
        LblPaymentMethod.Text = "MENSUAL"
        TxtTotal.Clear()

        TxtPrice.Enabled = True
        TxtPrice.Focus()

        ' Configuración de límites o parámetros comerciales iniciales
        _allowedPriceMin = 10D
        _allowedPriceMax = 100D

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
        LblPaymentMethod.Text = "DIARIO"

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

        LblPaymentMethod.Text = "DSCTO EDAD"

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

        LblPaymentMethod.Text = "GRUPO FAM"

        NudNumberMembers.Focus()

    End Sub


    ''' <summary>
    ''' Configura los controles visuales para la estrategia de Mensualidad más Implementos.
    ''' </summary>
    Private Sub ConfigureMonthlyWithEquipmentTariffUI()

        NudNumberMembers.Value = 1

        TxtTotal.Clear()

        LblPaymentMethod.Text = "MES + IMPLE"

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