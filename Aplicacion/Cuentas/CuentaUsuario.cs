using Aplicacion.ConfiguracionLogin.Contratos;
using Dominio.Entidades;
using Dominio.ModelosDto;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Persistencia;
using AutoMapper;
using Aplicacion.ExcepcionesPersonalizadas;
using System.Net;
using Dominio.ModelosDto.ModelosParaPerfil;
using Aplicacion.ConfiguracionLogin;
using Dominio.Auxiliares.ModelosPaseContratista;
using System.IO;

namespace Aplicacion.Cuentas
{
    /// <summary>
    /// Datos del perfil de las personas con rol solicitante
    /// </summary>
    public class CuentaUsuario
    {
        public class Ejecuta : IRequest<PasesUsuarioData>
        { }

        public class Manejador : IRequestHandler<Ejecuta, PasesUsuarioData>
        {
            // variables para obtener los pases generados
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
                // obtencion de las entidades relacionadas
                var usuario = await this._context.Usuario
                    .Include(x => x.PasesRel)
                    .ThenInclude(z => z.PersonasRel)
                    .ThenInclude(y => y.PersonaRel)
                    .Include(x => x.PasesRel)
                    .ThenInclude(z => z.DocumentosRel)
                    .Include(x => x.PasesRel)
                    .ThenInclude(z => z.AsesorPrevencionRel)
                    .FirstOrDefaultAsync(x => x.UserName == this._usuarioSesion.ObtenerUsuarioSesion());

                Console.WriteLine("CUETA RECONOCIDA COMO " + this._usuarioSesion.ObtenerUsuarioSesion());

                // si en algun caso el Email del usuario ingresado no este almacenado
                if (usuario == null)
                    throw new CorreoNoExisteException(HttpStatusCode.Unauthorized,
                       new
                       {
                           mensaje = "Las credenciales de acceso entregadas no coinciden con los registros.",
                           status = HttpStatusCode.Unauthorized,
                           tipoError = "adv-cnee00"
                       });

                // obtencion de los datos por pase
                ICollection<PasePerfil> pasesPerfil = new List<PasePerfil>();

                foreach (var pase in usuario.PasesRel.Reverse())
                {
                    ICollection<PersonaExternaPase> personasExternasPase = new List<PersonaExternaPase>();

                    // en caso de haber personas relacionadas al pase
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

                                .Include(x => x.DocumentosRel)
                                .ThenInclude(z => z.AnexoContratoRel)
                                .Include(x => x.DocumentosRel)
                                .ThenInclude(z => z.RegistroPersonaRel)

                                .FirstOrDefaultAsync(x => x.PersonaId == personaExterna.PersonaId);

                            // obtencion del nombre 
                            string nombresPE = string.Join(" "
                                , personaExternaEncontrada.NombresRel
                                .Select(x => x.NombreRel.Titulo));

                            var apellidos = personaExternaEncontrada.ApellidosRel
                                    .Select(x => x.ApellidoRel.Titulo).ToList();
                            string primerApellidoPE = apellidos.Count() > 0 ?
                                apellidos.FirstOrDefault()
                                : string.Empty;

                            if (primerApellidoPE.Length > 0)
                                apellidos.Remove(apellidos.First());

                            string segundoApellidoPE = apellidos.Count() > 0 ?
                                apellidos.FirstOrDefault()
                                : string.Empty;

                            // listar documentos relacionados a usuario 
                            ICollection<DocumentoCompleto> documentosCompletosPersona = new List<DocumentoCompleto>();

                            if (personaExternaEncontrada.DocumentosRel != null)
                                foreach (var documentoIndividual in personaExternaEncontrada.DocumentosRel)
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

                                    // dato relacionado a documentos anexo de contrato
                                    string descripcionExiste = documentoIndividualEncontrado.AnexoContratoRel != null
                                        ? documentoIndividualEncontrado.AnexoContratoRel.Descripcion
                                        : "";
                                    // dato relacionado a documentos registros de persona
                                    string fechaRegistroExiste = documentoIndividualEncontrado.RegistroPersonaRel != null
                                        ? documentoIndividualEncontrado.RegistroPersonaRel.FechaRegistro.ToString()
                                        : "";

                                    // agregar el modelo mapeado
                                    documentosCompletosPersona.Add(new DocumentoCompleto
                                    {
                                        DocumentoBase64 = archivoEnBase64,
                                        Descripcion = descripcionExiste,
                                        FechaRegistro = fechaRegistroExiste,
                                        FechaVencimiento = documentoIndividualEncontrado.FechaVencimiento.ToString(),
                                        Extension = documentoIndividualEncontrado.Extension,
                                        TituloDocumento = documentoIndividualEncontrado.TipoDocumentoRel.Titulo
                                    });
                                }

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
                                Nacionalidad = personaExternaEncontrada.PersonaExternaRel.Nacionalidad != null ?
                                    personaExternaEncontrada.PersonaExternaRel.Nacionalidad
                                    : "",
                                DocumentoCompletosRel = documentosCompletosPersona
                            });
                        }

                    // documentos para la empresa
                    ICollection<DocumentoCompleto> documentosEmpresaPase = new List<DocumentoCompleto>();

                    // en caso de haber documentos relacionados a la empresa
                    if (pase.DocumentosRel != null)
                        foreach (var documentoIndividual in pase.DocumentosRel)
                        {
                            // asociar las personas externas correspondientes
                            var documentoIndividualEncontrado = await this._context.Documento
                                .Include(x => x.TipoDocumentoRel)
                                //.Include(y => y.AnexoContratoRel)
                                //.Include(z => z.RegistroPersonaRel)
                                .FirstOrDefaultAsync(x => x.DocumentoId == documentoIndividual.DocumentoId);

                            // conversion del archivo hacia base64
                            byte[] documentoEnBytes = File.ReadAllBytes(documentoIndividualEncontrado.RutaDocumento);
                            string archivoEnBase64 = Convert.ToBase64String(documentoEnBytes);

                            // dato relacionado a documentos anexo de contrato
                            //string descripcionExiste = documentoIndividualEncontrado.AnexoContratoRel.Descripcion;
                            // dato relacionado a documentos registros de persona
                            //string fechaRegistroExiste = documentoIndividualEncontrado.RegistroPersonaRel.FechaRegistro.ToString();

                            // agregar el modelo mapeado
                            documentosEmpresaPase.Add(new DocumentoCompleto
                            {
                                DocumentoBase64 = archivoEnBase64,
                                //Descripcion = descripcionExiste,
                                //FechaRegistro = fechaRegistroExiste,
                                FechaVencimiento = documentoIndividualEncontrado.FechaVencimiento.ToString(),
                                Extension = documentoIndividualEncontrado.Extension,
                                TituloDocumento = documentoIndividualEncontrado.TipoDocumentoRel.Titulo
                            });
                        }

                    // asesor de prevencion para mapear
                    AsesorDePrevencionRiesgos asesorPrevencion = new AsesorDePrevencionRiesgos();

                    if (pase.AsesorPrevencionRel != null)
                    {
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("EL PASE TIENE ASESOR DE PREVENCION ");
                        Console.WriteLine("-------------------------");

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

                    // agregar los pases mapeados
                    pasesPerfil.Add(new PasePerfil
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

                // modelo con los datos a usar en la cuenta del usuario comun
                var pasesUsuarioAll = new PasesUsuarioData
                {
                    PasesRel = pasesPerfil
                };

                return pasesUsuarioAll;
            }
        }
    }
}