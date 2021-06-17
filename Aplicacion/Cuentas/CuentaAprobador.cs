using Aplicacion.ConfiguracionLogin.Contratos;
using Dominio.Entidades;
using Dominio.ModelosDto;
using Dominio.ModelosDto.ModelosParaPerfil;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
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
                IJwtGenerador jwtGenerador,
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
                    pasesPorRol = pasesPorRol.Include(x => x.PersonaExternasRel).ThenInclude(z => z.PersonaExternaRel);

                    foreach (var pase in pasesPorRol)
                    {
                        ICollection<PersonaExternaPase> personasExternasPase = new List<PersonaExternaPase>();
                        if (pase.PersonaExternasRel != null)
                            foreach (var personaExterna in pase.PersonaExternasRel)
                            {
                                // asociar las personas externas correspondientes
                                var personaExternaEncontrada = await this._context.PersonaExterna
                                    .Include(x => x.PersonaRel.NombresRel)
                                    .ThenInclude(z => z.NombreRel)
                                    .Include(x => x.PersonaRel.ApellidosRel)
                                    .ThenInclude(z => z.ApellidoRel)
                                    .FirstOrDefaultAsync(x => x.PersonaExternaId == personaExterna.PersonaExternaId);

                                // obtencion de nombres
                                string nombresPE = string.Join(" "
                                    , personaExternaEncontrada.PersonaRel.NombresRel
                                    .Select(x => x.NombreRel.Titulo));

                                var apellidos = personaExternaEncontrada.PersonaRel.ApellidosRel
                                    .Select(x => x.ApellidoRel.Titulo).ToList();
                                string primerApellidoPE = apellidos.FirstOrDefault();

                                apellidos.Remove(apellidos.First());
                                string segundoApellidoPE = apellidos.Count() > 0 ?
                                    apellidos.FirstOrDefault()
                                    : string.Empty;

                                personasExternasPase.Add(new PersonaExternaPase
                                {
                                    Nombres = nombresPE,
                                    PrimerApellido = primerApellidoPE,
                                    SegundoApellido = segundoApellidoPE,
                                    Rut = personaExternaEncontrada.PersonaRel.Rut,
                                    Pasaporte = personaExternaEncontrada.Pasaporte,
                                    Nacionalidad = personaExternaEncontrada.Nacionalidad
                                });
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
                            PersonaExternasRel = personasExternasPase
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