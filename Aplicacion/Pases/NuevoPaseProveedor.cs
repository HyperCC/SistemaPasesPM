using Dominio.Auxiliares.Pases;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;
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
            public DateTime FechaInicio { get; set; }
            public DateTime FechaTermino { get; set; }
            public ICollection<PersonaExternaRequest> Personas { get; set; }
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
            public Manejador(SistemaPasesContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // buscar si la empresa existe 
                var empresa = await this._context.Empresa.FirstOrDefaultAsync(x => x.Rut == request.RutEmpresa);

                // en caso se no existir la empresa
                if (empresa == null)
                {
                    empresa = new Empresa
                    {
                        EmpresaId = new Guid(),
                        Rut = request.RutEmpresa,
                        Nombre = request.NombreEmpresa
                    };
                    // si no existe la empresa se guarda
                    await this._context.Empresa.AddAsync(empresa);
                }

                // generar el nuevo pase visita.
                var paseGenerado = new Pase
                {
                    PaseId = new Guid(),
                    FechaInicio = request.FechaInicio,
                    FechaTermino = request.FechaTermino,
                    Tipo = "PROVEEDOR",
                    Estado = "PENDIENTE",
                    Area = request.Area,
                    Motivo = request.Motivo,
                    EmpresaId = empresa.EmpresaId
                };
                // agregar el nuevo pase 
                await this._context.Pase.AddAsync(paseGenerado);

                // agregar las personas externas aderidas al pase 
                foreach (var persona in request.Personas)
                {
                    // generar el tipo persona y almacenarlo
                    var personaPersona = new Persona
                    {
                        Rut = persona.Rut
                    };
                    await this._context.Persona.AddAsync(personaPersona);

                    // agregar los nombres 


                    // generar la personaExterna
                    var personaExterna = new PersonaExterna
                    {
                        PersonaExternaId = new Guid(),
                        Nacionalidad = persona.Nacionalidad,
                        Pasaporte = persona.Rut,
                        PersonaId = personaPersona.PersonaId
                    };
                    await this._context.PersonaExterna.AddAsync(personaExterna);
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
