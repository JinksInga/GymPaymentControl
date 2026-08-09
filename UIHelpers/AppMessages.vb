Namespace UIHelpers

    Public Module AppMessages

#Region " VALIDACIÓN "

        Public Const EmptyField As String = "El campo no puede estar vacío."

        Public Const WrongAge As String = "Verifica la edad del cliente."

        Public Const SelectRecord As String = "Selecciona una fila que contenga un PAGO."

#End Region


#Region " BÚSQUEDA "

        Public Const SearchSingleResult As String = "Registro que coincide con la búsqueda."

        Public Const SearchMultipleResults As String = "Registros que coinciden con la búsqueda."

        Public Const SearchingGroup As String = "Buscando grupo ..."

        Public Const SelectSearchFilter As String = "SELECCIONA UN FILTRO PARA LA BUSQUEDA"

#End Region


#Region " TARIFAS "

        Public Const GeneralPriceDescription As String = "Precio GENERAL para todos los clientes."

        Public Const ModifyRelatedRatesWarning As String = "Se modificarán todas las tarifas con el nuevo precio."

        Public Const BaseRateCannotBeDeleted As String = "No se permite ELIMINAR la tarifa base."

        Public Const BaseRateCanBeModified As String = "Si lo deseas, puedes MODIFICAR su precio establecido."

        Public Const BasePriceModificationConfirmation As String = "¿Está seguro de MODIFICAR el precio base?"

#End Region


#Region " GRUPOS FAMILIARES "

        Public Const FamilyGroupNameRequired As String = "El nombre del grupo es obligatorio."

        Public Const FamilyGroupNameAlreadyExists As String = "El nombre de este grupo familiar ya existe."

        Public Const FamilyGroupNameNotExist As String = "El nombre del grupo familiar no existe."

        Public Const InvalidFamilyGroupSelection As String = "Debe ingresar o seleccionar un grupo válido."

        Public Const CientAlreadyAddedToGroup As String = "ya se encuentra agregado en este grupo."

        Public Const NumberMembersNotMatchListMembers As String = "La cantidad de integrantes no coincide con la lista de integrantes."


        ''' <summary>
        ''' Genera el mensaje de validación mostrado cuando el nombre
        ''' de un grupo familiar ya está registrado.
        ''' </summary>
        ''' <param name="groupName">
        ''' Nombre del grupo familiar que se encuentra duplicado.
        ''' </param>
        ''' <returns>
        ''' Mensaje de validación con el nombre duplicado y las instrucciones
        ''' para seleccionar un nombre diferente.
        ''' </returns>
        Public Function FamilyGroupNameDuplicated(groupName As String) As String

            Return $"NOMBRE DUPLICADO : {groupName}" & Environment.NewLine &
                   "El nombre de este grupo familiar ya existe." & Environment.NewLine &
                   "Elija otro nombre para continuar."

        End Function

#End Region


#Region " CLIENTES "
#End Region

#Region " GENERAL "
#End Region

    End Module

End Namespace