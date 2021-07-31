using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.ConfiguracionLogin
{
    /// <summary>
    /// Actualizar un usuario en la lista del admin con otro existente 
    /// </summary>
    public class ActualizaRolUsuario
    {
        public class Ejecuta : IRequest
        {
            public string RolActualizar { get; set; }
            public string RolAnterior { get; set; }
            public string EmailUsuario { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.RolActualizar).NotEmpty();
                RuleFor(x => x.RolAnterior).NotEmpty();
                RuleFor(x => x.EmailUsuario).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public Manejador(UserManager<Usuario> userManager
                , RoleManager<IdentityRole> roleManager)
            {
                this._userManager = userManager;
                this._roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // validacion del formato del request
                EjecutaValidacion validator = new EjecutaValidacion();
                var validacionesRes = validator.Validate(request);

                // en caso de no obtener datos validos
                if (!validacionesRes.IsValid)
                {
                    List<string> erroresFV = new List<string>();
                    // listar los mensajes de error obtenidos
                    foreach (var failure in validacionesRes.Errors)
                        erroresFV.Add(failure.ErrorMessage);

                    // devolver una excepcion y los erroes encontrados
                    throw new FormatoIncorrectoException(HttpStatusCode.BadRequest,
                     new
                     {
                         mensaje = $"Los datos recibidos por el usuario no cumplen con el formato solicitado.",
                         status = HttpStatusCode.BadRequest,
                         tipoError = "adv-fie000",
                         listaErrores = erroresFV
                     });
                }

                // verifica si los roles nuevo y anterior son identicos
                if (request.RolActualizar == request.RolAnterior)
                    throw new RolIdenticoException(HttpStatusCode.NotFound,
                        new
                        {
                            mensaje = $"El nuevo rol \"{request.RolActualizar}\" para actualizar el usuario es identico al anterior.",
                            status = HttpStatusCode.BadRequest,
                            tipoError = "adv-rie000"
                        });

                // buscar el usuario
                var usuario = await this._userManager.FindByNameAsync(request.EmailUsuario);
                if (usuario == null)
                    throw new CorreoNoExisteException(HttpStatusCode.NotFound,
                        new
                        {
                            mensaje = "Las credenciales de acceso entregadas no coinciden con los registros.",
                            status = HttpStatusCode.NotFound,
                            tipoError = "adv-cnee00"
                        });

                // verificar la existencia del nuevo rol
                var resultRol = await this._roleManager.FindByNameAsync(request.RolActualizar);
                if (resultRol == null || request.RolActualizar == "sin rol")
                    throw new RolNoExisteException(HttpStatusCode.BadRequest,
                       new
                       {
                           mensaje = $"No existe un Rol registrado con el titulo {request.RolActualizar}",
                           status = HttpStatusCode.BadRequest,
                           tipoError = "adv-rnee00"
                       });

                // elimiar el rol actual
                if (request.RolAnterior != "sin rol")
                    await this._userManager.RemoveFromRoleAsync(usuario, request.RolAnterior);

                // agregar el nuevo rol 
                var resultAgregar = await this._userManager.AddToRoleAsync(usuario, request.RolActualizar);

                if (resultAgregar.Succeeded)
                {
                    // TODO: envair EMAIL al usuario "usuario" informando del cambio de rol
                    return Unit.Value;
                }

                throw new Exception("No se pudo cambiar el rol");
            }
        }
    }
}