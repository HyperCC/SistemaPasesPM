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
    public class ResetContrasena
    {
        public class Ejecuta: IRequest<Usuario>
        {
            public string correo;
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.correo).NotEmpty().NotNull();
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
                var pass = PasswordGenerator.GeneratePassword(true, true, true, true, 10);
                var user = await this._userManager.FindByEmailAsync(request.correo);

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
                
                await this._userManager.RemovePasswordAsync(user);
                var result = await this._userManager.AddPasswordAsync(user, pass);

                if (result.Succeeded)
                {
                    return user;
                }

                throw new NotImplementedException();
            }
        }
    }
}
