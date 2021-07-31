using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    // en caso de in gresar un rol para actualizar inexistente
    public class RolNoExisteException : ManejadorException
    {
        public RolNoExisteException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}