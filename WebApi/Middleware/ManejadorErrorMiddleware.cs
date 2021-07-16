using Aplicacion.ExcepcionesPersonalizadas;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Middleware
{
    /// <summary>
    /// Configuracion para la excepcion personalizada ManejadorException
    /// </summary>
    public class ManejadorErrorMiddleware
    {
        // Función que puede procesar una solicitud HTTP.
        private readonly RequestDelegate _next;
        // logger para entregar los mensajes
        private readonly ILogger<ManejadorErrorMiddleware> _logger;

        public ManejadorErrorMiddleware(RequestDelegate next,
            ILogger<ManejadorErrorMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        // en caso de tener todos los datos correctos se pasa al siguiente procedimiento. LA PALABRA INVOKE DEBE ESTAR EN INGLES
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // procesar la solicitu http
                await this._next(context);
            }
            catch (Exception ex)
            {
                // si hay errores en las validaciones se crea una excepcion
                await ManejadorExceptionAsincrono(context, ex, this._logger);
            }
        }

        // evalua el tipo de excepcion que pueden surgir y al respuesta
        private async Task ManejadorExceptionAsincrono(HttpContext context,
            Exception ex,
            ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;
            // verificar el tipo de excepcion
            switch (ex)
            {
                // throw excepcion para un rut ya existente
                case RutExisteException ree:
                    logger.LogError(ex, "EL RUT INGRESADO YA EXISTE EN EL SISTEMA Y NO SE PUEDE REGISTRAR..");
                    errores = ree.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)ree.Codigo;
                    break;

                // throw excepcion para un correo ya existente
                case CorreoExisteException cee:
                    logger.LogError(ex, "EL CORREO INGRESADO YA EXISTE EN EL SISTEMA Y NO SE PUEDE REGISTRAR..");
                    errores = cee.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)cee.Codigo;
                    break;

                case RolExisteException rolee:
                    logger.LogError(ex, "EL ROL INGRESADO YA EXISTE EN EL SISTEMA Y NO SE PUEDE REGISTRAR..");
                    errores = rolee.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)rolee.Codigo;
                    break;

                case RolNoExisteException rnee:
                    logger.LogError(ex, "EL ROL INGRESADO PARA ACTUALIZAR NO EXISTE EN EL SISTEMA..");
                    errores = rnee.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)rnee.Codigo;
                    break;

                case RolIdenticoException rie:
                    logger.LogError(ex, "EL ROL INGRESADO PARA ACTUALIZAR ES IDENTICO AL ANTERIOR..");
                    errores = rie.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)rie.Codigo;
                    break;

                case FormatoIncorrectoException fie:
                    logger.LogError(ex, "LOS DATOS ENTREGADOS POR EL CLIENTE NO SON VALIDOS..");
                    errores = fie.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)fie.Codigo;
                    break;

                case UserManagerNoGuardadoException ume:
                    logger.LogError(ex, "LOS DATOS A REGISTRAR COMO USER MANAGER NO FUERON ALMACENADOS CORRECTAMENTE..");
                    errores = ume.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)ume.Codigo;
                    break;

                case CorreoNoExisteException cnee:
                    logger.LogError(ex, "EL CORREO INGRESADO PARA EL LOGIN NO TIENE REGISTROS ALMACENADOS..");
                    errores = cnee.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)cnee.Codigo;
                    break;

                case PasswordIncorrectoException pic:
                    logger.LogError(ex, "LA CLAVE INGRESADA PARA EL LOGIN NO COINCIDE CON EL CORREO SUMINISTRADO..");
                    errores = pic.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)pic.Codigo;
                    break;

                case DbContextNoGuardadoException dbe:
                    logger.LogError(ex, "LOS DATOS NO PUDIERON SER ALMACENADOS EN LA BASE DE DATOS..");
                    errores = dbe.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)dbe.Codigo;
                    break;

                case PasswordDuplicadaException pde:
                    logger.LogError(ex, "EL PASSWORD INGRESADO PARA ACTUALIZAR ES IDENTICO AL ANTERIOR..");
                    errores = pde.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)pde.Codigo;
                    break;

                case PasswordSintaxisException pie:
                    logger.LogError(ex, "EL PASSWORD INGRESADO PARA ACTUALIZAR PRESENTA MULTIPLES ERRORES DE SINTAXIS EN SUS CARACTERES..");
                    errores = pie.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)pie.Codigo;
                    break;

                case PasswordNuevaMalConfirmadaException pnmce:
                    logger.LogError(ex, "LA CONFIRMACION PARA EL NUEVO PASSWORD NO COINCIDE CON EL NUEVO PASSWORD INGRESADO..");
                    errores = pnmce.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)pnmce.Codigo;
                    break;

                case EstadoNoExisteException enee:
                    logger.LogError(ex, "EL ESTADO RECIBIDO POR EL CLIENTE NO COINCIDE CON ALGUN REGISTRO EXISTENTE..");
                    errores = enee.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)enee.Codigo;
                    break;

                case ExtensionNoAceptadaException enae:
                    logger.LogError(ex, "LA EXTENSION DEL ARCHIVO RECIBIDA POR EL CLIENTE NO ES RECONOCIDA O NO ESTA PERMITIDA..");
                    errores = enae.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)enae.Codigo;
                    break;

                // si se lanza ManejadorExcepcion (excepcion personalizada), error de validacion para solicitudes http
                case ManejadorException me:
                    logger.LogError(ex, "ERROR GENERICO EN LOS MANEJADORES..");
                    errores = me.Errores;
                    // lanzar codigo de error especifico
                    context.Response.StatusCode = (int)me.Codigo;
                    break;

                // error generico o desconocido
                case Exception e:
                    logger.LogError(ex, "Error del servidor - clase Exception");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error Exception - mensaje generico se Exception" : e.Message;
                    // error en el servidor tipo 5xx
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            // transformacion de los errores encontrados a datos en json
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                // serializacion de los datos
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}
