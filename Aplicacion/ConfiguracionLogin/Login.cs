using Aplicacion.ConfiguracionLogin.Contratos;
using Aplicacion.ConfiguracionLogin.TokenSeguridad;
using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using Dominio.ModelosDto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.ConfiguracionLogin
{
    /// <summary>
    /// Operaciones generar tokens de login para el Usuario
    /// </summary>
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _usuarioManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;

            public Manejador(UserManager<Usuario> usuarioManager,
                SignInManager<Usuario> signInManager,
                IJwtGenerador jwtGenerador)
            {
                this._usuarioManager = usuarioManager;
                this._signInManager = signInManager;
                this._jwtGenerador = jwtGenerador;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // verificar que el email del usuario existe 
                var usuario = await this._usuarioManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                    throw new ManejadorException(HttpStatusCode.Unauthorized);

                // verificar que la password sea correcta
                var resultado = await this._signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                // devolver datos del usuario logueado o lanzar 401
                if (resultado.Succeeded)
                    return new UsuarioData
                    {
                        Nombres = "nombres",
                        Apellidos = "apellidos",
                        Token = this._jwtGenerador.CreateToken(usuario, new List<string>()),
                        Email = usuario.Email
                    };

                throw new ManejadorException(HttpStatusCode.Unauthorized);
            }
        }

    }
}
