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

        // evalua el tipo de excepcion ue pueden surgir y al respuesta
        private async Task ManejadorExceptionAsincrono(HttpContext context,
            Exception ex,
            ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;

            // verificar el tipo de excepcion
            switch (ex)
            {
                // si se lanza ManejadorExcepcion (excepcion personalizada), error de validacion para solicitudes http
                case ManejadorException me:
                    logger.LogError(ex, "Manejador Error en clase ManejadorExcepcion");
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
