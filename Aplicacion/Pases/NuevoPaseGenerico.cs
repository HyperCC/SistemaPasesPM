using Aplicacion.ConfiguracionLogin.Contratos;
using Aplicacion.ExcepcionesPersonalizadas;
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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Dominio.Auxiliares.ModelosPaseContratista;

namespace Aplicacion.Pases
{
    /// <summary>
    /// Creacion de un pase de cualquier tipo
    /// </summary>
    public class NuevoPaseGenerico
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
            public bool Completitud { get; set; }
            public string FechaInicio { get; set; }
            public string FechaTermino { get; set; }

            // Personas para pase generico
            public ICollection<PersonaExternaGenericaRequest> PersonasExternas { get; set; } // nullable
        }

        /// <summary>
        /// vaidacion de los datos recibidos en el request
        /// </summary>
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
                         tipoError = "adv-fie000",
                         listaErrores = erroresFV
                     });
                }

                // usuario en sesion actual
                var usuarioActual = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                // buscar si la empresa existe 
                var buscarEmpresa = await BuscarOAlmacenarEmpresa.BuscarOAgregarEmpresa(this._context
                    , request.RutEmpresa
                    , request.NombreEmpresa);

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

                // agregar las personas externas
                if (request.PersonasExternas != null)
                    foreach (var personaIndividual in request.PersonasExternas)
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

                        // buscar por la persona externa segun la Persona ya encontrada
                        await BuscarOAlmacenarPersonaExterna.BuscarOAgregarPersonaExterna(this._context,
                            buscarPersona,
                            paseGenerado,
                            personaIndividual.Nacionalidad);

                        // guardar documentos de induccion si los posee
                        if (personaIndividual.DocumentosInduccion.Count > 0)
                            // agregar documentos
                            foreach (var docPersona in personaIndividual.DocumentosInduccion)
                                await AlmacenarDocumentoPersonaGenerico.AgregarDocumento(
                                    docPersona
                                    , this._context
                                    , _env
                                    , buscarPersona.PersonaId);

                    }

                // guarar los cambios hechos en el context
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