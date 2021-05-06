using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// Excepcion para un correo de usuario existente
    /// </summary>
    public class CorreoExisteException : ManejadorException
    {
        public CorreoExisteException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }
}
