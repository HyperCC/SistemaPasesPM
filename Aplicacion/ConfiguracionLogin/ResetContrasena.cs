using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.ExcepcionesPersonalizadas;
using FluentValidation;
using MediatR;
using Dominio.Entidades;
using Persistencia;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Aplicacion.Auxiliares;
using System.Net;

namespace Aplicacion.ConfiguracionLogin
{
    /// <summary>
    /// Cambiar la contraseña actual de una cuenta por una autogenerada
    /// </summary>
    public class ResetContrasena
    {
        public class Ejecuta : IRequest<Usuario>
        {
            public string Email;
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.Email).NotEmpty().NotNull();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {

            private readonly SistemaPasesContext _context;
            private readonly UserManager<Usuario> _userManager;

            public Manejador(SistemaPasesContext context, UserManager<Usuario> userManager)
            {
                this._context = context;
                this._userManager = userManager;
            }
            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // generar la contraseña nueva
                var pass = PasswordGenerator.GeneratePassword(true, true, true, true, 16);

                // buscar al usuario para cambiar la contraseña
                var user = await this._userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new CorreoNoExisteException(HttpStatusCode.Unauthorized,
                        new
                        {
                            mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                            status = HttpStatusCode.Unauthorized,
                            tipoError = "adv-cnee00"
                        });

                //var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
                //await this._userManager.ResetPasswordAsync(user, token, pass);

                // cambiar la contraseña actual del usuario
                await this._userManager.RemovePasswordAsync(user);
                var result = await this._userManager.AddPasswordAsync(user, pass);

                // TODO: agregar esta nueva clave como dato temporal, ya
                //que si el usuario no ingresa al sisitema para cambiar
                //la contraseña despues de haberla resetado, la contraseña
                //deberia volver al estado anterior al reset.

                // TODO: envair EMAIL al usuario con la nueva clave desde aqui

                if (result.Succeeded)
                    return user;

                throw new Exception("no se ha podido generar una nueva contraseña para el usuario.");
            }
        }
    }
}
