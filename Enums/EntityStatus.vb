Namespace Enums

    ''' <summary>
    ''' Representa los estados globales de Clientes y Grupos Familiares.
    ''' Se mapea con columnas TINYINT(1) en MySQL (0 = Inactivo, 1 = Activo).
    ''' </summary>
    Public Enum EntityStatus As Byte

        Inactive = 0
        Active = 1

    End Enum

End Namespace