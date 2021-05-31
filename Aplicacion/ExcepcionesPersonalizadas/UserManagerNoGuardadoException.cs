using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    public class UserManagerNoGuardadoException : ManejadorException
    {
        public UserManagerNoGuardadoException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }
}