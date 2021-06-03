using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    class PasswordSintaxisException : ManejadorException
    {
        public PasswordSintaxisException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}