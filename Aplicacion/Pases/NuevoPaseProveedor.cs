using Aplicacion.ConfiguracionLogin.Contratos;
using Dominio.Auxiliares.Pases;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Persistencia.AuxiliaresAlmacenamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Pases
{
    /// <summary>
    /// Creacion de pases tipo Proveedor 
    /// </summary>
    public class NuevoPaseProveedor
    {
        public class Ejecuta : IRequest
        {
            public string Area { get; set; }
            public string Motivo { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }
            public string Tipo { get; set; }
            public bool Completo { get; set; }
            public string FechaInicio { get; set; }
            public string FechaTermino { get; set; }
            public ICollection<PersonaExternaGenericaRequest> Personas { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.Area).NotEmpty();
                this.RuleFor(x => x.Motivo).NotEmpty();
                this.RuleFor(x => x.RutEmpresa).NotEmpty();
                this.RuleFor(x => x.NombreEmpresa).NotEmpty();
                this.RuleFor(x => x.FechaInicio).NotEmpty();
                this.RuleFor(x => x.FechaTermino).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly SistemaPasesContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;

            public Manejador(SistemaPasesContext context,
                UserManager<Usuario> userManager,
                IUsuarioSesion sesion)
            {
                this._context = context;
                this._userManager = userManager;
                this._usuarioSesion = sesion;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine(request.RutEmpresa);
                Console.WriteLine("--------------------------------");

                // usuario en sesion actual
                var usuarioActual = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                // buscar si la empresa existe 
                var buscarEmpresa = await this._context.Empresa.FirstOrDefaultAsync(x => x.Rut == request.RutEmpresa);

                // en caso se no existir la empresa
                if (buscarEmpresa == null)
                {
                    buscarEmpresa = new Empresa
                    {
                        EmpresaId = new Guid(),
                        Rut = request.RutEmpresa,
                        Nombre = request.NombreEmpresa
                    };
                    // si no existe la empresa se guarda
                    await this._context.Empresa.AddAsync(buscarEmpresa);
                }

                // generar el tipo del pase ingresado
                string tipoPaseRecibido = request.Tipo.ToString().ToUpper();

                TipoPase tipoPaseValidado = tipoPaseRecibido == TipoPase.VISITA.ToString() ? TipoPase.VISITA
                    : tipoPaseRecibido == TipoPase.PROVEEDOR.ToString() ? TipoPase.PROVEEDOR
                    : tipoPaseRecibido == TipoPase.USOMUELLE.ToString() ? TipoPase.USOMUELLE
                    : TipoPase.TRIPULANTE;

                // generar el nuevo pase visita.
                var paseGenerado = new Pase
                {
                    PaseId = new Guid(),
                    FechaInicio = Convert.ToDateTime(request.FechaInicio),
                    FechaTermino = Convert.ToDateTime(request.FechaTermino),
                    Tipo = tipoPaseValidado,
                    Estado = EstadoPase.PENDIENTE,
                    Area = request.Area,
                    Motivo = request.Motivo,
                    EmpresaId = buscarEmpresa.EmpresaId,
                    UsuarioId = usuarioActual.Id
                };
                // agregar el nuevo pase 
                await this._context.Pase.AddAsync(paseGenerado);

                if (request.Personas != null)
                {
                    // agregar las personas externas aderidas al pase 
                    foreach (var personaIndividual in request.Personas)
                    {
                        // buscar si ya existe la persona 
                        var buscarPersona = await this._context.Persona.FirstOrDefaultAsync(p => p.Rut == personaIndividual.Rut);

                        if (buscarPersona == null)
                        {
                            // si no existe la persona se crea una y se almacena
                            buscarPersona = new Persona
                            {
                                Rut = personaIndividual.Rut
                            };
                            await this._context.Persona.AddAsync(buscarPersona);

                            // agregar los nombres respectivos de la persona
                            await AlmacenarNombres.AgregarNombres(personaIndividual.Nombres,
                                (personaIndividual.PrimerApellido + " " + personaIndividual.SegundoApellido),
                                this._context,
                                buscarPersona.PersonaId);
                        }

                        // buscar por la persona externa
                        var buscarPersonaExterna = await this._context.PersonaExterna.FirstOrDefaultAsync(p => p.PersonaId == buscarPersona.PersonaId);

                        if (buscarPersonaExterna == null)
                        {
                            // si no existe la persona externa se crea y se almacena
                            buscarPersonaExterna = new PersonaExterna
                            {
                                PersonaExternaId = new Guid(),
                                Nacionalidad = personaIndividual.Nacionalidad,
                                //Pasaporte = personaIndividual.PasaporteORut == "PASAPORTE" ? personaIndividual.Pasaporte : null,
                                PersonaId = buscarPersona.PersonaId
                            };
                            await this._context.PersonaExterna.AddAsync(buscarPersonaExterna);
                        }
                    }
                }

                // guarar los cambios hechos en el context
                var result = await this._context.SaveChangesAsync();
                if (result > 0)
                    return Unit.Value;

                throw new Exception();
            }
        }
    }
}
