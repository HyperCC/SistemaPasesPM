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

namespace Aplicacion.Pases
{
    /// <summary>
    /// Creacion de un pase de cualuier tipo
    /// </summary>
    public class NuevoPaseGenerico
    {
        /// <summary>
        /// atributos recibidos por el request
        /// </summary>
        public class Ejecuta : IRequest
        {
            public string Area { get; set; } //nullable
            public string Motivo { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }
            public string Tipo { get; set; }
            public string ServicioAdjudicado { get; set; } // nullable
            public bool Completitud { get; set; }
            public string FechaInicio { get; set; }
            public string FechaTermino { get; set; }

            // Personas para pase generico
            public ICollection<PersonaExternaGenericaRequest> PersonasExternas { get; set; } // nullable
            // Personas para pase contratista
            public ICollection<PersonaExternaContratistaRequest> PersonasContratista { get; set; } //nullable
            // documentos de empresa para pase contratista
            public ICollection<DocumentosEmpresaContratistaRequest> SeccionDocumentosEmpresa { get; set; } // nulable
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
                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"VALOR DE LA COMPLETITUD {request.Completitud}");
                Console.WriteLine("---------------------------------------");

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
                var usuarioActual = await this._userManager.FindByNameAsync("admin@gmail.com");

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
                    EmpresaId = buscarEmpresa.EmpresaId,
                    UsuarioId = usuarioActual.Id
                };
                // agregar el nuevo pase 
                await this._context.Pase.AddAsync(paseGenerado);


                // agregar los documentos relacionados a una empresa en pase contratista
                /*
                if (request.SeccionDocumentosEmpresa != null && tipoPaseRecibido.CompareTo(TipoPase.CONTRATISTA) == 0)
                    await AlmacenarDocumentosEmpresa.AgregarDocumentosEmpresa(request.SeccionDocumentosEmpresa,
                        this._context,
                        paseGenerado.PaseId,
                        buscarEmpresa.EmpresaId);
                */


                if (request.PersonasExternas != null)
                    // agregar las personas externas aderidas al pase 
                    foreach (var personaIndividual in request.PersonasExternas)
                    {
                        // buscar o almacenar la persona por rut
                        Persona buscarPersona = await BuscarOAlmacenarPersona.BuscarOAgregarPersona(this._context,
                            personaIndividual.Rut,
                            personaIndividual.Nombres,
                            (personaIndividual.PrimerApellido + " " + personaIndividual.SegundoApellido));

                        // buscar por la persona externa segun la Persona ya encontrada
                        var buscarPersonaExterna = await BuscarOAlmacenarPersonaExterna.BuscarOAgregarPersonaExterna(this._context,
                            buscarPersona.PersonaId,
                            paseGenerado.PaseId,
                            personaIndividual.Pasaporte,
                            personaIndividual.Nacionalidad);
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