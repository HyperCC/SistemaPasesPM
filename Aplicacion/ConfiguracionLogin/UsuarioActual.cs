using Aplicacion.ConfiguracionLogin.Contratos;
using Dominio.Entidades;
using Dominio.ModelosDto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.ConfiguracionLogin
{
    /// <summary>
    /// Mediador para obtener el usuario en sesion
    /// </summary>
    public class UsuarioActual
    {
        public class Ejecuta : IRequest<UsuarioData> { }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            // instancia de variables necesarias para obtener la sesion de un usuario
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(UserManager<Usuario> userManager, 
                IJwtGenerador jwtGenerador, 
                IUsuarioSesion sesion)
            {
                this._userManager = userManager;
                this._jwtGenerador = jwtGenerador;
                this._usuarioSesion = sesion;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                Console.WriteLine("ACCESO DIRECTO AL USUSARIO ACTUAL");
                // obtener lista de roles para el objeto usuario "usuario"
                var listaRoles = new List<string>(await this._userManager.GetRolesAsync(usuario));

                return new UsuarioData
                {
                    Nombres = "nombres",
                    Apellidos = "apellidos",
                    UserName = usuario.UserName,
                    Token = this._jwtGenerador.CreateToken(usuario, listaRoles),
                    Email = usuario.Email
                };
            }
        }
    }
}
