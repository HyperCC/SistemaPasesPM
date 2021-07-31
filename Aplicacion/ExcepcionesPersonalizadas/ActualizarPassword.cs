using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// validacion en caso de que se presenten errores de la sintaxis para el nuevo password
    /// </summary>
    public class PasswordSintaxisException : ManejadorException
    {
        public PasswordSintaxisException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }

    /// <summary>
    /// validacion en caso de que la nueva password sea identica a la anterior
    /// </summary>
    public class PasswordDuplicadaException : ManejadorException
    {
        public PasswordDuplicadaException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }

    /// <summary>
    /// verificar que la nueva password y su confirmacion coincidan
    /// </summary>
    public class PasswordNuevaMalConfirmadaException : ManejadorException
    {
        public PasswordNuevaMalConfirmadaException(HttpStatusCode codigo, object errores = null)
               : base(codigo, errores)
        { }
    }
}
