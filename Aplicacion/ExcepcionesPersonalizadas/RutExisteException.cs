using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// Excepcion para un rut existente
    /// </summary>
    public class RutExisteException : Exception
    {
        // codigo http a devolver 
        public HttpStatusCode Codigo { get; }

        // mensajes con los errores obtenidos
        public object Errores { get; }

        public RutExisteException(HttpStatusCode codigo, object errores = null)
        {
            this.Codigo = codigo;
            this.Errores = errores;
        }
    }
}
