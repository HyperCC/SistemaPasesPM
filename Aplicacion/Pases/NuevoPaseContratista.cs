using Aplicacion.ConfiguracionLogin.Contratos;
using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Auxiliares.Pases;
using Dominio.Auxiliares.ModelosPaseContratista;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Persistencia.AuxiliaresAlmacenamiento;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Aplicacion.Pases
{
    public class NuevoPaseContratista
    {
        public class Ejecuta : IRequest
        {
            public string Area { get; set; } //nullable
            public string Motivo { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }
            public string Tipo { get; set; }
            public string Observacion { get; set; }
            public string ServicioAdjudicado { get; set; } // nullable
            public bool Completitud { get; set; }
            public string FechaInicio { get; set; }
            public string FechaTermino { get; set; }
            public AsesorDePrevencionContratista AsesorDePrevencion { get; set; }

            // Personas para pase contratista
            public ICollection<PersonaExternaContratista> PersonasContratista { get; set; }
            // documentos de empresa para pase contratista
            public ICollection<DocumentoEmpresaContratista> SeccionDocumentosEmpresa { get; set; }

        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.Motivo).NotEmpty();
                this.RuleFor(x => x.RutEmpresa).NotEmpty();
                this.RuleFor(x => x.NombreEmpresa).NotEmpty();
                this.RuleFor(x => x.Tipo).NotEmpty();
                this.RuleFor(x => x.FechaInicio).NotEmpty();
                this.RuleFor(x => x.FechaTermino).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly SistemaPasesContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly IHostingEnvironment _env;

            public Manejador(SistemaPasesContext context,
                UserManager<Usuario> userManager,
                IUsuarioSesion sesion,
                IHostingEnvironment env)
            {
                this._context = context;
                this._userManager = userManager;
                this._usuarioSesion = sesion;
                this._env = env;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                // usuario en sesion actual
                var usuarioActual = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                // buscar si la empresa existe 
                var buscarEmpresa = await this._context.Empresa
                    .FirstOrDefaultAsync(x => x.Rut == request.RutEmpresa);

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
                    : tipoPaseRecibido == TipoPase.CONTRATISTA.ToString() ? TipoPase.CONTRATISTA
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
                    Observacion = request.Observacion,
                    EmpresaId = buscarEmpresa.EmpresaId,
                    UsuarioId = usuarioActual.Id
                };
                // agregar el nuevo pase 
                await this._context.Pase.AddAsync(paseGenerado);

                // Agregar Documentos de la Empresa
                if(request.SeccionDocumentosEmpresa != null)
                {
                    foreach(var docEmpresa in request.SeccionDocumentosEmpresa)
                    {
                        await AlmacenarDocumentosEmpresa.AgregarDocumentosEmpresa(docEmpresa, request.AsesorDePrevencion, _context, _env, paseGenerado.PaseId, buscarEmpresa.EmpresaId);
                    }
                }

                // Agregar Personas
                if ( request.PersonasContratista != null)
                {
                    foreach (var persona in request.PersonasContratista)
                    {

                        // Buscar o almacenar la persona por rut
                        Persona buscarPersona = await BuscarOAlmacenarPersona.BuscarOAgregarPersona(this._context,
                            persona.Rut,
                            persona.Nombres,
                            (persona.PrimerApellido + " " + persona.SegundoApellido));

                        // Buscar por la persona externa segun la Persona ya encontrada
                        var buscarPersonaExterna = await BuscarOAlmacenarPersonaExterna.BuscarOAgregarPersonaExterna(this._context,
                            buscarPersona.PersonaId,
                            paseGenerado.PaseId,
                            persona.Pasaporte,
                            persona.Nacionalidad);

                        //Agregar Documentos:
                        foreach ( var doc in persona.DocumentosPersona)
                        {
                            await AlmacenarDocumentoPersonaContratista.AgregarDocumento(doc, _context, _env, paseGenerado.PaseId, buscarPersonaExterna.PersonaId);
                        }
                    }
                }

                //Lista de Personas
                var result = await this._context.SaveChangesAsync();
                if (result > 0)
                    return Unit.Value;
                return Unit.Value;
            }
        }
    }
}
