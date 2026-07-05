Namespace UIHelpers

    Public Module DialogMessages

#Region " Mensajes de FrmClientsPayments "

        ''' <summary>
        ''' Genera un mensaje de advertencia antes de eliminar un cliente
        ''' o cambiar su estado a INACTIVO.
        ''' </summary>
        ''' <param name="fullName">
        ''' Nombre completo del cliente que se mostrará en el mensaje.
        ''' </param>
        ''' <param name="customerCode">
        ''' Código identificador del cliente que se mostrará en el mensaje.
        ''' </param>
        ''' <returns>
        ''' Texto formateado para mostrar en un MessageBox de confirmación.
        ''' </returns>
        ''' <remarks>
        ''' Este mensaje informa que eliminar un cliente también eliminará
        ''' todo su historial de pagos registrado.
        '''
        ''' Opciones disponibles:
        ''' Sí: Eliminar permanentemente el cliente y sus pagos.
        ''' No: Cambiar el estado del cliente a INACTIVO.
        ''' Cancelar: No realizar ninguna acción.
        ''' </remarks>
        Public Function DeleteOrInactivateCustomerWarning(fullName As String, customerCode As String) As String

            Return "                              ¡¡¡ ADVERTENCIA !!!" & Environment.NewLine &
                   "   __________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "   Eliminar este cliente también eliminará todo su historial" & Environment.NewLine &
                   "   de pagos registrado." & Environment.NewLine & Environment.NewLine &
                   $"     NOMBRE  :  {fullName}" & Environment.NewLine &
                   $"     CÓDIGO   :  {customerCode}" & Environment.NewLine &
                   "   __________________________________________________________" & Environment.NewLine & Environment.NewLine &
                   "                  ¿Qué deseas hacer con el cliente?" & Environment.NewLine & Environment.NewLine &
                   "                  Sí   : Eliminar cliente e historial de pagos." & Environment.NewLine &
                   "                  No : Cambiar el estado del cliente a INACTIVO." & Environment.NewLine & Environment.NewLine &
                   "                  Cancelar : No realizar ninguna acción."

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

#End Region


#Region " Mensajes de FrmListDebtors "

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
        ''' Texto mostrado en el ErrorProvider cuando el grupo familiar esta lleno.
        ''' </summary>
        ''' <param name="groupName">
        ''' Variable que muestra el nombre del grupo familiar.
        ''' </param>
        Public Function FullFamilyGroup(groupName As String) As String

            Return "El grupo " & groupName & " está lleno." & Environment.NewLine &
                   "Haga clic en el botón [Ampliar cupo] para agregar un integrante."

        End Function

#End Region


#Region " Mensajes de FrmNewModifyClient "

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

#End Region


#Region " Mensajes de Tarifas y Precios "

        ' =========================================================================
        ' CONSTANTES DE ACCIÓN EN TariffTransactionReport (parametro actionMessage)
        ' =========================================================================
        Public Const RecordSavedSuccessfully As String = "Se ha GUARDADO el registro correctamente."

        Public Const RecordUpdatedSuccessfully As String = "Se ha ACTUALIZADO el registro correctamente."

        Public Const RecordDeletedSuccessfully As String = "Se ha ELIMINADO el registro correctamente."

        Public Const RecordDeletionConfirmation As String = "¿Está seguro de ELIMINAR este registro?"

        ''' <summary>
        ''' Genera un mensaje indicando que debe seleccionarse un registro
        ''' antes de realizar una acción sobre él.
        ''' </summary>
        ''' <param name="actionText">
        ''' Acción que el usuario intenta realizar
        ''' (por ejemplo: "MODIFICAR" o "ELIMINAR").
        ''' </param>
        ''' <returns>
        ''' Mensaje de advertencia para mostrar al usuario.
        ''' </returns>
        Public Function SelectRecordWarning(actionText As String) As String

            Return $"Selecciona un registro de la lista para {actionText.Trim().ToUpper()}."

        End Function

        ''' <summary>
        ''' Plantilla unificada en formato de reporte/ticket con el desglose de los costos de una tarifa, 
        ''' para mostrar en cuadros de diálogo de Guardar, Actualizar o Eliminar.
        ''' </summary>
        ''' <param name="paymentType">El tipo de pago general (ej: CLASES SUELTAS, MENSUALIDAD + IMPLEMENTOS).</param>
        ''' <param name="paymentName">El nombre del registro guardado en BBDD (ej: DIARIO 10, MES + IMPLE 50).</param>
        ''' <param name="price">El precio base o total a pagar formateado.</param>
        ''' <param name="actionMessage">El mensaje de cierre o instrucción (ej: '¿Deseas eliminar este registro?' o 'Guardado correctamente.').</param>
        ''' <returns>Cuerpo de texto estructurado y alineado para el MessageBox.</returns>
        Public Function TariffTransactionReport(paymentType As String, paymentName As String,
                                                price As String, actionMessage As String) As String

            Return $"  Tipo de pago : {paymentType}" & Environment.NewLine &
                   $"  Nombre pago : {paymentName}" & Environment.NewLine & Environment.NewLine &
                   "  ------------------------------------------------------------" & Environment.NewLine &
                   $"      Precio          --->  {price}" & Environment.NewLine &
                   $"      Descuento  --->  0.00 €" & Environment.NewLine &
                   $"      A pagar       --->  {price}" & Environment.NewLine &
                   "  ------------------------------------------------------------" & Environment.NewLine & Environment.NewLine &
                   $"  {actionMessage}"

        End Function


        ''' <summary>
        ''' Plantilla unificada en formato de reporte/ticket con el desglose de los costos de una tarifa, 
        ''' para mostrar en cuadros de diálogo de Guardar, Actualizar o Eliminar.
        ''' </summary>
        ''' <param name="paymentType">El tipo de pago general (ej: DESCUENTO POR EDAD, GRUPO FAMILIAR).</param>
        ''' <param name="paymentName">El nombre base registrado en BBDD (ej: DSCTO EDAD 5-9).</param>
        ''' <param name="additionalInfo">La regla específica formateada (ej: 'Rango: Desde 5 hasta 9 años' o 'Integrantes: 4 personas').</param>
        ''' <param name="basePrice">El precio base o subtotal formateado.</param>
        ''' <param name="discount">El descuento total aplicado formateado.</param>
        ''' <param name="toPay">El monto neto final a pagar formateado.</param>
        ''' <param name="actionMessage">El mensaje de cierre o instrucción (ej: '¿Deseas eliminar este registro?' o 'Guardado correctamente.').</param>
        ''' <returns>Cuerpo de texto estructurado y alineado para el MessageBox.</returns>
        Public Function TariffTransactionReport(paymentType As String, paymentName As String, additionalInfo As String,
                                                basePrice As String, discount As String, toPay As String,
                                                actionMessage As String) As String

            Return $"  Tipo de pago : {paymentType}" & Environment.NewLine &
                   $"  Nombre pago : {paymentName}" & Environment.NewLine & Environment.NewLine &
                   $"  {additionalInfo}" & Environment.NewLine & Environment.NewLine &
                   "  ------------------------------------------------------------" & Environment.NewLine &
                   $"      Precio          --->  {basePrice}" & Environment.NewLine &
                   $"      Descuento  --->   {discount}" & Environment.NewLine &
                   $"      A pagar       --->  {toPay}" & Environment.NewLine &
                   "  ------------------------------------------------------------" & Environment.NewLine & Environment.NewLine &
                   $"  {actionMessage}"

        End Function


        ''' <summary>
        ''' Plantilla unificada en formato de reporte/ticket con la tarifa única mensual, 
        ''' para mostrar en cuadros de diálogo de Guardar, Actualizar o Eliminar.
        ''' Genera un mensaje de advertencia antes de modificar el precio de la tarifa única mensual.
        ''' o precio base de las tarifas familiares y descuentos por edad.
        ''' </summary>
        ''' <param name="currentPrice">El precio base o tarifa única mensual.</param>
        ''' <param name="additionalInfo">La regla específica formateada (ej: 'Rango: Desde 5 hasta 9 años' o 'Integrantes: 4 personas').</param>
        ''' <param name="actionMessage">El mensaje de cierre o instrucción (ej: '¿Deseas eliminar este registro?' o 'Guardado correctamente.').</param>
        ''' <returns>Cuerpo de texto estructurado y alineado para el MessageBox.</returns>
        Public Function TariffTransactionReport(currentPrice As String, additionalInfo As String,
                                                     actionMessage As String) As String

            Return "   Tipo de pago : TARIFA UNICA MENSUAL" & Environment.NewLine &
                   "   Nombre pago : MENSUAL" & Environment.NewLine & Environment.NewLine &
                   "  ----------------------------------------------------" & Environment.NewLine &
                   $"      Precio actual  --->  {currentPrice}" & Environment.NewLine &
                   "  ----------------------------------------------------" & Environment.NewLine & Environment.NewLine &
                   $"  ⚠️ {additionalInfo} ⚠️" & Environment.NewLine & Environment.NewLine &
                   $"  {actionMessage}"

        End Function


        ''' <summary>
        ''' Genera un mensaje de error unificado cuando se detecta que el método de pago ya existe en la base de datos, 
        ''' adaptando los textos informativos y correctivos según el modo de la transacción actual.
        ''' </summary>
        ''' <param name="actionText">Hace referencia a la acción que se está ejecutando (GUARDAR o ACTUALIZAR).</param>
        ''' <param name="paymentMethod">El nombre del método de pago duplicado (ej: LblPaymentMethod.Text).</param>
        ''' <returns>Texto formateado y estructurado para el cuadro de diálogo de error crítico.</returns>
        Public Function DuplicatedTariffNameWarning(actionText As String, paymentMethod As String) As String

            Return $"   No se puede {actionText} tarifa, ya existe un registro" & Environment.NewLine &
                   "   con este nombre :" & Environment.NewLine & Environment.NewLine &
                   $"         MÉTODO PAGO : {paymentMethod}" & Environment.NewLine & Environment.NewLine &
                   "   Puedes MODIFICAR los datos de la tarifa o ELIMINAR el" & Environment.NewLine &
                   "   registro duplicado."

        End Function

#End Region


#Region " Mensajes de FormHelpers "

        ''' <summary>
        ''' Mensaje mostrado cuando no se cumple el formato del correo electrónico.
        ''' </summary>
        Public Function InvalidEmailMessage() As String

            Return "  Ingresa un formato de E-Mail válido." & Environment.NewLine & Environment.NewLine &
                   "  Por ejemplo:" & Environment.NewLine &
                   "     usuario@dominio.com"

        End Function

#End Region

    End Module
End Namespace