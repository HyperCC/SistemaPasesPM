using Aplicacion.ExcepcionesPersonalizadas;
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
    /// Creacion de nuevos roles 
    /// </summary>
    public class NuevoRol
    {
        public class Ejecuta : IRequest
        {
            public string Titulo { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Titulo).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> roleManager)
                => this._roleManager = roleManager;

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
                         mensaje = $"Los datos recibidos por el usaurio no cumplen con el formato solicitado.",
                         status = HttpStatusCode.BadRequest,
                         tipoError = erroresFV
                     });
                }

                // buscar si existe el rola agregar
                var role = await this._roleManager.FindByNameAsync(request.Titulo);
                if (role != null)
                    throw new RolExisteException(HttpStatusCode.BadRequest,
                       new
                       {
                           mensaje = $"Ya existe un Rol registrado con el titulo {request.Titulo}",
                           status = HttpStatusCode.BadRequest,
                           tipoError = "role-role"
                       });

                var resultado = await this._roleManager.CreateAsync(new IdentityRole(request.Titulo));
                if (resultado.Succeeded)
                    return Unit.Value;

                throw new Exception("No se pudo guardar el rol");
            }
        }
    }
}
