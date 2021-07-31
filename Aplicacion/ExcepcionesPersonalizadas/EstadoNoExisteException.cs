using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Aplicacion.ExcepcionesPersonalizadas
{
    /// <summary>
    /// en caso de recibir un estado no reconocido
    /// </summary>
    public class EstadoNoExisteException : ManejadorException
    {
        public EstadoNoExisteException(HttpStatusCode codigo, object errores = null)
            : base(codigo, errores)
        { }
    }
}