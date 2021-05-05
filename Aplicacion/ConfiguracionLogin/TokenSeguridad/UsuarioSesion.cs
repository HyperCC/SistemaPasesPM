using Aplicacion.ConfiguracionLogin.Contratos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Aplicacion.ConfiguracionLogin.TokenSeguridad
{
    /// <summary>
    /// Obtener el usuario actual
    /// </summary>
    public class UsuarioSesion : IUsuarioSesion
    {
        // context http
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
            => this._httpContextAccessor = httpContextAccessor;

        /// <summary>
        /// Obtener el usuario por Username
        /// </summary>
        /// <returns>Usuario actual</returns>
        public string ObtenerUsuarioSesion()
        {
            // buscar el usuario actual por UserName
            var userName = this._httpContextAccessor.HttpContext.User?.Claims?.
                FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            return userName;
        }
    }
}
