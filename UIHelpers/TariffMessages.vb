Namespace UIHelpers

    ''' <summary>
    ''' Contiene los textos y mensajes utilizados exclusivamente
    ''' por el formulario de gestión de tarifas y descuentos.
    ''' </summary>
    ''' <remarks>
    ''' Centraliza las cadenas de texto relacionadas con las
    ''' operaciones de alta, modificación, eliminación y
    ''' configuración de tarifas para facilitar su mantenimiento
    ''' y evitar valores literales repetidos en el código.
    ''' </remarks>
    Public Module TariffMessages

        Public Const GeneralPriceDescription As String = "Precio GENERAL para todos los clientes."

        Public Const ModifyRelatedRatesWarning As String = "Se modificarán todas las tarifas con el nuevo precio."

        Public Const BaseRateCannotBeDeleted As String = "No se permite ELIMINAR la tarifa base."

        Public Const BaseRateCanBeModified As String = "Si lo deseas, puedes MODIFICAR su precio establecido."

        Public Const BasePriceModificationConfirmation As String = "¿Está seguro de MODIFICAR el precio base?"

    End Module

End Namespace