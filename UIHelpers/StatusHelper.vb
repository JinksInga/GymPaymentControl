Imports GymPaymentControl.Constants
Imports GymPaymentControl.Enums

Public Module StatusHelper


    Public Function GetStatusDescription(status As EntityStatus) As String

        Return If(status = EntityStatus.Active, CustomerStates.Active, CustomerStates.Inactive)

    End Function

    ''| Si en algún punto lees directamente un Byte de la BD
    'Public Function GetStatusDescription(statusValue As Byte) As String
    '    Return If(statusValue = 1, CustomerStates.Active, CustomerStates.Inactive)
    'End Function

    Public Function GetStatusFromDescription(description As String) As EntityStatus

        If String.Equals(description?.Trim(), CustomerStates.Active, StringComparison.OrdinalIgnoreCase) Then

            Return EntityStatus.Active

        End If

        Return EntityStatus.Inactive

    End Function


End Module