using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// En caso de intentar actualizar un rol con el mismo
    /// </summary>
    public class RolIdenticoException : ManejadorException
    {
        public RolIdenticoException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}