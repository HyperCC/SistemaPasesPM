using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.ConfiguracionLogin.Contratos
{
    /// <summary>
    /// obtencion del usuario actual
    /// </summary>
    public interface IUsuarioSesion
    {
        string ObtenerUsuarioSesion();
    }
}
