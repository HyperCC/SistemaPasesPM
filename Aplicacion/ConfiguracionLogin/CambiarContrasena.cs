using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using Dominio.ModelosDto;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Aplicacion.ConfiguracionLogin.Contratos;
using System.Threading;
using System.Threading.Tasks;


namespace Aplicacion.ConfiguracionLogin
{
    public class CambiarContrasena
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.NewPassword).NotEmpty();
            }
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
                    throw new CorreoNoExisteException(HttpStatusCode.Unauthorized,
                        new
                        {
                            mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                            status = HttpStatusCode.Unauthorized,
                            tipoError = "adv-cnee00"
                        });

                // verificar que la password sea correcta
                var resultado = await this._signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (resultado.Succeeded == false)
                {
                    throw new PasswordIncorrectoException(HttpStatusCode.Unauthorized,
                    new
                    {
                        mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                        status = HttpStatusCode.Unauthorized,
                        tipoError = "adv-pie000"
                    });
                }

                var result = await this._usuarioManager.ChangePasswordAsync(usuario, request.Password, request.NewPassword);

                // devolver datos del usuario o lanzar 401
                if (result.Succeeded)
                    return new UsuarioData
                    {
                        Nombres = "Contraseña cambiada correctamente",
                        Apellidos = "apellidos",
                        UserName = usuario.UserName,
                        Token = this._jwtGenerador.CreateToken(usuario, new List<string>()),
                        Email = usuario.Email
                    };

                // usuario no autorizado
                throw new ManejadorException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
