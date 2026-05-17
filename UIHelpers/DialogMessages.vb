Namespace UIHelpers

    Public Module DialogMessages

        ''' <summary>
        ''' Genera el mensaje de error cuando el usuario selecciona
        ''' una fila incorrecta. Ejemplo: Fila Resumen.
        ''' </summary>
        Public Function SelectCorrectRow() As String

            Return "   Para cobrar la cuota mensual a un cliente" & Environment.NewLine & Environment.NewLine &
                   "   Selecciona un registro válido de la lista de morosos" & Environment.NewLine &
                   "   _____________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "                     La fila RESUMEN no es un registro válido"

        End Function


        ''' <summary>
        ''' Genera el mensaje de confirmación mostrado después de registrar o actualizar un cliente.
        ''' </summary>
        Public Function ClientOperationSuccess(firstName As String,
                                               lastName As String,
                                               idClientFormatted As String,
                                               actionText As String) As String

            Return "DATOS DEL CLIENTE" & Environment.NewLine & Environment.NewLine &
                   "   NOMBRE   :  " & firstName & " " & lastName & Environment.NewLine &
                   "   CÓDIGO   :  " & idClientFormatted & Environment.NewLine &
                   "   -----------------------------------------------" & Environment.NewLine &
                   "   Datos " & actionText & " correctamente."

        End Function


        ''' <summary>
        ''' Mensaje mostrado cuando existen cambios pendientes sin guardar en el formulario.
        ''' </summary>
        Public Function UnsavedChangesWarning(titleText As String, bodyText As String) As String

            Return "                              ¡ ¡ ¡  ATENCIÓN  ! ! !" & Environment.NewLine &
                   "   Hay cambios en el formulario que no han sido " & titleText & "." & Environment.NewLine &
                   "  ______________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "   ¿Deseas RECUPERAR la información?" & Environment.NewLine & Environment.NewLine &
                   "                         Sí : Muestrame el formulario para " & bodyText & "." & Environment.NewLine &
                   "                         No : Descartar los cambios y cerrar la ventana."

        End Function


        ''' <summary>
        ''' Mensaje mostrado para informar al usuario que el cliente tiene una deuda pendiente.
        ''' </summary>
        Public Function PendingDebtWarning(actionText As String) As String

            Return "                            CONTROL FINANCIERO" & Environment.NewLine &
                   "  ____________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "   El cliente tiene pagos pendientes :" & Environment.NewLine & Environment.NewLine &
                   "   * " & actionText & " primero tiene" & Environment.NewLine &
                   "     que saldar la deuda."

        End Function


        ''' <summary>
        ''' Mensaje mostrado para informar que ya existen pagos antes de crear masivamente para evitar duplicados.
        ''' </summary>
        ''' <param name="newMonth">
        ''' Mes que tiene pagos masivos registrados.
        ''' </param>
        Public Function DoNotDuplicatePayments(newMonth As String) As String

            Return $"   Las membresías de {newMonth} ya están registradas en" & Environment.NewLine &
                   "   la base de datos." & Environment.NewLine & Environment.NewLine &
                   "   No es posible duplicar pagos existentes." & Environment.NewLine &
                   "   ________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "                                                          Operación cancelada."

        End Function


        ''' <summary>
        ''' Mensaje mostrado para preguntar al usuario si está seguro de crear pagos masivos.
        ''' </summary>
        ''' <param name="newMonth">
        ''' Mes al que corresponde los nuevos pagos masivos.
        ''' </param>
        Public Function AskBeforeRegisteringPayments(newMonth As String) As String

            Return "                            ¡ ¡ ¡  ATENCIÓN  ! ! !" & Environment.NewLine & Environment.NewLine &
                   "   Se crearán nuevos pagos de " & newMonth & " para todos los" & Environment.NewLine &
                   "   clientes y grupos familiares en actividad." & Environment.NewLine &
                   "   __________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "      ¿Desea continuar con la creación masiva de registros?"

        End Function


        ''' <summary>
        ''' Mensaje mostrado para informar al usuario que no puede cambiar el método
        ''' de pago de un cliente si este pertenece a un grupo familiar.
        ''' </summary>
        Public Function GroupPaymentChangeNotAllowed() As String

            Return "     NO SE PUEDE CAMBIAR EL MÉTODO DE PAGO" & Environment.NewLine &
                   "     El cliente pertenece a un grupo familiar." & Environment.NewLine &
                   "   ___________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "     Si quieres hacer el cambio tienes dos opciones:" & Environment.NewLine & Environment.NewLine &
                   "       * MODIFICAR el grupo familiar." & Environment.NewLine &
                   "       * ELIMINAR el grupo familiar."

        End Function


        ''' <summary>
        ''' Mensaje mostrado para informar al usuario que se va a expandir
        ''' el límite de vacantes de un grupo familiar.
        ''' </summary>
        Public Function ConfirmAddExtraGroupMember(groupName As String, groupMemberLimit As Integer) As String

            Return "    Nombre del grupo  : " & groupName & Environment.NewLine &
                   "    Nº de Integrantes   : " & groupMemberLimit & Environment.NewLine & Environment.NewLine &
                   "    El grupo seleccionado ya tiene los integrantes completos." & Environment.NewLine &
                   "    ___________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "                        ¿Seguro que quieres añadir otro integrante?"

        End Function


        ''' <summary>
        ''' Mensaje mostrado cuando no se cumple el formato del correo electrónico.
        ''' </summary>
        Public Function InvalidEmailMessage() As String

            Return "  Ingresa un formato de E-Mail válido." & Environment.NewLine & Environment.NewLine &
                   "  Por ejemplo:" & Environment.NewLine &
                   "     usuario@dominio.com"

        End Function


        ''' <summary>
        ''' Texto mostrado en el ErrorProvider cuando el grupo familiar esta lleno.
        ''' </summary>
        ''' <param name="groupName">
        ''' Variable que muestra el nombre del grupo familiar.
        ''' </param>
        Public Function FullFamilyGroup(groupName As String) As String

            Return "El grupo " & groupName & " está lleno." & Environment.NewLine &
                   "Haga clic en el botón [Ampliar cupo] para agregar un integrante."

        End Function


    End Module
End Namespace