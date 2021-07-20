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
    /// <summary>
    /// Creacion de un pase de tipo Contratista
    /// </summary>
    public class NuevoPaseContratista
    {
        /// <summary>
        /// Atributos recibidos por el request
        /// </summary>
        public class Ejecuta : IRequest
        {
            public string Area { get; set; } //nullable
            public string Motivo { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }
            public string Tipo { get; set; }
            public string Observacion { get; set; }
            public string ServicioAdjudicado { get; set; } // nullable
            public bool Completitud { get; set; } = false;
            public string FechaInicio { get; set; }
            public string FechaTermino { get; set; }

            // Personas para pase contratista
            public ICollection<PersonasExternasContratista> PersonasContratista { get; set; }
            // documentos de empresa para pase contratista
            public ICollection<DocumentoEmpresa> SeccionDocumentosEmpresa { get; set; }

            // prevencionista
            public AsesorDePrevencionRiesgos AsesorDePrevencion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                /*
                this.RuleFor(x => x.Motivo).NotEmpty();
                this.RuleFor(x => x.RutEmpresa).NotEmpty();
                this.RuleFor(x => x.NombreEmpresa).NotEmpty();
                this.RuleFor(x => x.Tipo).NotEmpty();
                this.RuleFor(x => x.FechaInicio).NotEmpty();
                this.RuleFor(x => x.FechaTermino).NotEmpty();
                */
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
                var usuarioActual = await this._userManager
                    .FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                // buscar si la empresa existe 
                var buscarEmpresa = await BuscarOAlmacenarEmpresa.BuscarOAgregarEmpresa(this._context
                    , request.RutEmpresa
                    , request.NombreEmpresa);

                // generar el nuevo pase visita.
                var paseGenerado = new Pase
                {
                    PaseId = new Guid(),
                    FechaInicio = Convert.ToDateTime(request.FechaInicio),
                    FechaTermino = Convert.ToDateTime(request.FechaTermino),
                    Tipo = TipoPase.CONTRATISTA,
                    Estado = EstadoPase.PENDIENTE,
                    Area = request.Area,
                    Motivo = request.Motivo,
                    Observacion = request.Observacion,
                    EmpresaId = buscarEmpresa.EmpresaId,
                    UsuarioId = usuarioActual.Id
                };

                // agregar el prevencionista
                if (request.AsesorDePrevencion.Rut.Length > 0)
                {
                    var asesor = await BuscarOAlmacenarPrevencionista
                     .BuscarOAgregarPrevencionista(request.AsesorDePrevencion
                     , this._context);

                    paseGenerado.AsesorPrevencionId = asesor.AsesorPrevencionId;
                }

                // agregar el pase con el prevencionista relaconado
                await this._context.Pase.AddAsync(paseGenerado);

                // agregar Documentos de la Empresa
                if (request.SeccionDocumentosEmpresa != null)
                    foreach (var docEmpresa in request.SeccionDocumentosEmpresa)
                        await AlmacenarDocumentosEmpresa.AgregarDocumentosEmpresa(docEmpresa
                            , _context
                            , _env
                            , paseGenerado.PaseId
                            , buscarEmpresa.EmpresaId);

                // Agregar Personas
                if (request.PersonasContratista != null)
                {
                    foreach (var personaIndividual in request.PersonasContratista)
                    {
                        // buscar o almacenar la persona por rut o pasaporte
                        Persona buscarPersona = personaIndividual.Rut.Length > 0 ?

                            await BuscarOAlmacenarPersona.BuscarOAgregarPersona(this._context,
                            personaIndividual.Rut,
                            personaIndividual.Nombres,
                            (personaIndividual.PrimerApellido + " " + personaIndividual.SegundoApellido))

                            : await BuscarOAlmacenarPersonaPorPasaporte.BuscarOAgregarPersonaPorPasaporte(this._context,
                            personaIndividual.Pasaporte,
                            personaIndividual.Nombres,
                            (personaIndividual.PrimerApellido + " " + personaIndividual.SegundoApellido));

                        // Buscar por la persona externa segun la Persona ya encontrada
                        await BuscarOAlmacenarPersonaExterna.BuscarOAgregarPersonaExterna(this._context,
                            buscarPersona,
                            paseGenerado,
                            personaIndividual.Nacionalidad);

                        //Agregar Documentos:

                        foreach (var docPersona in personaIndividual.DocumentosPersona)

                            await AlmacenarDocumentoPersonaContratista.AgregarDocumento(
                                docPersona
                                , this._context
                                , _env
                                , paseGenerado.PaseId
                                , buscarPersona.PersonaId);
                    }
                }

                // Resultado de la operacion 
                var result = await this._context.SaveChangesAsync();
                if (result > 0)
                    return Unit.Value;

                // si por algun motivo la base de datos no almacena los cambios
                throw new DbContextNoGuardadoException(HttpStatusCode.BadRequest,
                    new
                    {
                        mensaje = $"El sistema no pudo registrar el nuevo pase enviado por el usuario {usuarioActual.Email}",
                        status = HttpStatusCode.BadRequest,
                        tipoError = "err-dbcng0"
                    });
            }
        }
    }
}