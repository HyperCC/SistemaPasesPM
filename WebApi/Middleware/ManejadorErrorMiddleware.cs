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
            
        }
    }
}
