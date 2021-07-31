using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// Excepcion general para los metodos Manejador de los mediadores
    /// </summary>
    public class ManejadorException : Exception
    {
        // codigo http a devolver 
        public HttpStatusCode Codigo { get; }

        // mensajes con los errores obtenidos
        public object Errores { get; }

        public ManejadorException(HttpStatusCode codigo, object errores = null)
        {
            this.Codigo = codigo;
            this.Errores = errores;
        }
    }
}
