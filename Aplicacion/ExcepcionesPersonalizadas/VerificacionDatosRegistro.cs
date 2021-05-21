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

    /// <summary>
    /// Excepcion para un rut existente
    /// </summary>
    public class RutExisteException : ManejadorException
    {
        public RutExisteException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }
}