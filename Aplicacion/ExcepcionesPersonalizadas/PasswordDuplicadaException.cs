using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    public class PasswordDuplicadaException : ManejadorException
    {
        public PasswordDuplicadaException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}