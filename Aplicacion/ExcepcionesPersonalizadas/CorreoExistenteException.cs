using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// Excepcion para un correo de usuario existente
    /// </summary>
    public class CorreoExistenteException : ManejadorException
    {
        public CorreoExistenteException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }
}
