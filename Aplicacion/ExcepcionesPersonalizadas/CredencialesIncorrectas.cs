using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// correo ingresado en login no reconocido
    /// </summary>
    public class CorreoNoExisteException : ManejadorException
    {
        public CorreoNoExisteException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }

    /// <summary>
    /// Password ingresada en login incorrecta
    /// </summary>
    public class PasswordIncorrectoException : ManejadorException
    {
        public PasswordIncorrectoException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }
}