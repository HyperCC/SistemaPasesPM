using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.ConfiguracionLogin.Contratos
{
    /// <summary>
    /// generacion de token jwt
    /// </summary>
    public interface IJwtGenerador
    {
        string CreateToken(Usuario usuario, List<string> roles);
    }
}
