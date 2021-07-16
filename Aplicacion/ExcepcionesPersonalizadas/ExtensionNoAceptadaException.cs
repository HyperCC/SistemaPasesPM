using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// Error al intentar almacenar un archivo con extension no reconocida o permitida
    /// </summary>
    public class ExtensionNoAceptadaException : ManejadorException
    { 
        public ExtensionNoAceptadaException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}
