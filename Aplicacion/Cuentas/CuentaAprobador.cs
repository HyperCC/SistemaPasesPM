using Aplicacion.ConfiguracionLogin.Contratos;
using Dominio.Auxiliares.ModelosPaseContratista;
using Dominio.Entidades;
using Dominio.ModelosDto;
using Dominio.ModelosDto.ModelosParaPerfil;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cuentas
{
    public class CuentaAprobador
    {
        public class Ejecuta : IRequest<PasesUsuarioData>
        { }

        public class Manejador : IRequestHandler<Ejecuta, PasesUsuarioData>
        {
            // variables para obtener los pases de un aprobador
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly SistemaPasesContext _context;

            public Manejador(UserManager<Usuario> userManager,
                IUsuarioSesion sesion,
                SistemaPasesContext context)
            {
                this._userManager = userManager;
                this._usuarioSesion = sesion;
                this._context = context;
            }

            public async Task<PasesUsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // obtener el usuario en sesion y su rol
                var usuario = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());
                var currentRol = await this._userManager.GetRolesAsync(usuario);

                // obtener los pases asignados segun el rol
                var pasesPorRol = currentRol[0] == "CONTACTO" ? this._context.Pase.Where(p => p.Tipo == TipoPase.VISITA
                    || p.Tipo == TipoPase.PROVEEDOR || (p.Tipo == TipoPase.CONTRATISTA && (p.Estado != EstadoPase.FINALIZADO && p.Estado == EstadoPase.PENDIENTE)))

                    : currentRol[0] == "GUARDIA" ?  this._context.Pase
                    : currentRol[0] == "HSEQ" ? this._context.Pase.Where(p => p.Tipo == TipoPase.CONTRATISTA)
                    : currentRol[0] == "JEFE_OPERACIONES" ? this._context.Pase.Where(p => p.Tipo == TipoPase.USOMUELLE)

                    : currentRol[0] == "OPIP" ? this._context.Pase.Where(p => p.Tipo == TipoPase.TRIPULANTE
                    || (p.Tipo == TipoPase.USOMUELLE && (p.Estado != EstadoPase.FINALIZADO && p.Estado != EstadoPase.PENDIENTE)))
                    : null;

                // dar formato a los pases
                ICollection<PasePerfil> allPasesPerfil = new List<PasePerfil>();
                if (pasesPorRol != null)
                {
                    // obtener las personas externas relacionadas
                    pasesPorRol = pasesPorRol
                        .Include(x => x.PersonasRel)
                        .ThenInclude(z => z.PersonaRel)
                        .Include(x => x.DocumentosRel)
                        .Include(x => x.AsesorPrevencionRel);

                    foreach (var pase in pasesPorRol)
                    {
                        ICollection<PersonaExternaPase> personasExternasPase = new List<PersonaExternaPase>();
                        if (pase.PersonasRel != null)
                            foreach (var personaExterna in pase.PersonasRel)
                            {
                                // asociar las personas externas correspondientes
                                var personaExternaEncontrada = await this._context.Persona
                                    .Include(x => x.NombresRel)
                                    .ThenInclude(z => z.NombreRel)
                                    .Include(x => x.ApellidosRel)
                                    .ThenInclude(z => z.ApellidoRel)
                                    .Include(x => x.PersonaExternaRel)
                                    .FirstOrDefaultAsync(x => x.PersonaId == personaExterna.PersonaId);

                                // obtencion de nombres
                                string nombresPE = string.Join(" "
                                    , personaExternaEncontrada.NombresRel
                                    .Select(x => x.NombreRel.Titulo));

                                // obtencion de apellidos
                                var apellidos = personaExternaEncontrada.ApellidosRel
                                    .Select(x => x.ApellidoRel.Titulo).ToList();
                                string primerApellidoPE = apellidos[0];

                                //apellidos.Remove(apellidos.First());
                                string segundoApellidoPE = apellidos.Count() > 1 ?
                                    apellidos[1]
                                    : string.Empty;

                                personasExternasPase.Add(new PersonaExternaPase
                                {
                                    Nombres = nombresPE,
                                    PrimerApellido = primerApellidoPE,
                                    SegundoApellido = segundoApellidoPE,
                                    Rut = personaExternaEncontrada.Rut != null
                                        ? personaExternaEncontrada.Rut
                                        : "",
                                    Pasaporte = personaExternaEncontrada.Pasaporte != null
                                        ? personaExternaEncontrada.Pasaporte
                                        : "",
                                    Nacionalidad = personaExternaEncontrada.PersonaExternaRel.Nacionalidad != null
                                        ? personaExternaEncontrada.PersonaExternaRel.Nacionalidad
                                        : ""
                                });
                            }

                        // documentos para la empresa
                        ICollection<DocumentoCompleto> documentosEmpresaPase = new List<DocumentoCompleto>();

                        // en caso de haber personas relacionadas al pase
                        if (pase.DocumentosRel != null)
                            foreach (var documentoIndividual in pase.DocumentosRel)
                            {
                                // asociar las personas externas correspondientes
                                var documentoIndividualEncontrado = await this._context.Documento
                                    .Include(x => x.TipoDocumentoRel)
                                    .Include(y => y.AnexoContratoRel)
                                    .Include(z => z.RegistroPersonaRel)
                                    .FirstOrDefaultAsync(x => x.DocumentoId == documentoIndividual.DocumentoId);

                                // conversion del archivo hacia base64
                                byte[] documentoEnBytes = File.ReadAllBytes(documentoIndividualEncontrado.RutaDocumento);
                                string archivoEnBase64 = Convert.ToBase64String(documentoEnBytes);

                                // agregar el modelo mapeado
                                documentosEmpresaPase.Add(new DocumentoCompleto
                                {
                                    DocumentoBase64 = archivoEnBase64,
                                    Descripcion = documentoIndividualEncontrado.AnexoContratoRel != null
                                        ? documentoIndividualEncontrado.AnexoContratoRel.Descripcion
                                        : "",
                                    FechaRegistro = documentoIndividualEncontrado.RegistroPersonaRel != null
                                        ? documentoIndividualEncontrado.RegistroPersonaRel.FechaRegistro.ToString()
                                        : "",
                                    FechaVencimiento = documentoIndividualEncontrado.FechaVencimiento.ToString(),
                                    Extension = documentoIndividualEncontrado.Extension,
                                    TituloDocumento = documentoIndividualEncontrado.TipoDocumentoRel.Titulo
                                });
                            }

                        // asesor de prevencion para mapear
                        AsesorDePrevencionRiesgos asesorPrevencion = new AsesorDePrevencionRiesgos();

                        if (pase.AsesorPrevencionRel != null)
                        {
                            // asociar las personas externas correspondientes
                            var asesorPrevencionEncontrado = await this._context.AsesorPrevencion
                                .Include(x => x.PersonaRel)
                                .ThenInclude(y => y.NombresRel)
                                .ThenInclude(z => z.NombreRel)
                                .Include(x => x.PersonaRel)
                                .ThenInclude(y => y.ApellidosRel)
                                .ThenInclude(z => z.ApellidoRel)
                                .FirstOrDefaultAsync(x => x.AsesorPrevencionId == pase.AsesorPrevencionRel.AsesorPrevencionId);

                            // obtencion de nombres y apellidos 
                            string nombres = string.Join(" "
                                    , asesorPrevencionEncontrado.PersonaRel.NombresRel
                                    .Select(x => x.NombreRel.Titulo));

                            string apellidos = string.Join(" "
                                    , asesorPrevencionEncontrado.PersonaRel.ApellidosRel
                                    .Select(x => x.ApellidoRel.Titulo));

                            asesorPrevencion.Nombres = nombres;
                            asesorPrevencion.Apellidos = apellidos;
                            asesorPrevencion.Rut = asesorPrevencionEncontrado.PersonaRel.Rut;
                            asesorPrevencion.RegistroSNS = asesorPrevencionEncontrado.RegistroSns;
                        }

                        // agregar pases formateados al resultado final
                        allPasesPerfil.Add(new PasePerfil
                        {
                            PaseId = pase.PaseId.ToString(),
                            FechaInicio = pase.FechaInicio.ToString(),
                            FechaTermino = pase.FechaTermino.ToString(),
                            Motivo = pase.Motivo,
                            Area = pase.Area,
                            Tipo = pase.Tipo.ToString(),
                            Estado = pase.Estado.ToString(),
                            PersonaExternasRel = personasExternasPase,
                            DocumentoEmpresasRel = documentosEmpresaPase,
                            PrevencionistaRiesgos = asesorPrevencion
                        });
                    }
                }

                // devolver el modelo con los pases
                var pases = new PasesUsuarioData
                {
                    PasesRel = allPasesPerfil
                };

                return pases;
            }
        }
    }
}