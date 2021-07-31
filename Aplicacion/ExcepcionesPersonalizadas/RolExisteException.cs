using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// Excepcion para un rol existente
    /// </summary>
    public class RolExisteException : ManejadorException
    {
        public RolExisteException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}