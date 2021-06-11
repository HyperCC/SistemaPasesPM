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

namespace Aplicacion.Cuentas
{
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
                // obtencion de las entidades relacionadas
                var usuario = await this._context.Usuario
                    .Include(x => x.PasesRel)
                    .ThenInclude(z => z.PersonaExternasRel)
                    .ThenInclude(y => y.PersonaExternaRel)
                    .FirstOrDefaultAsync(x => x.UserName == this._usuarioSesion.ObtenerUsuarioSesion());

                Console.WriteLine("LARGO DE PASES " + usuario.PasesRel.Count());
                Console.WriteLine("CUETA RECONOCIDA COMO " + this._usuarioSesion.ObtenerUsuarioSesion());

                // si en algun caso el Email del usuario ingresado no este almacenado
                if (usuario == null)
                    throw new CorreoNoExisteException(HttpStatusCode.Unauthorized,
                       new
                       {
                           mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                           status = HttpStatusCode.Unauthorized,
                           tipoError = "adv-cnee00"
                       });

                // obtencion de los pases
                ICollection<PasePerfil> pasesPerfil = new List<PasePerfil>();

                foreach (var pase in usuario.PasesRel.Reverse())
                {
                    ICollection<PersonaExternaPase> personasExternasPase = new List<PersonaExternaPase>();
                    if (pase.PersonaExternasRel != null)
                        foreach (var personaExterna in pase.PersonaExternasRel.Reverse())
                        {
                            // asociar las personas externas correspondientes
                            var personaExternaEncontrada = await this._context.PersonaExterna
                                .Include(x => x.PersonaRel.NombresRel)
                                .ThenInclude(z => z.NombreRel)
                                .Include(x => x.PersonaRel.ApellidosRel)
                                .ThenInclude(z => z.ApellidoRel)
                                .FirstOrDefaultAsync(x => x.PersonaExternaId == personaExterna.PersonaExternaId);

                            // obtencion del nombre 
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

                    // agregar los pases mapeados
                    pasesPerfil.Add(new PasePerfil
                    {
                        FechaInicio = pase.FechaInicio.ToString(),
                        FechaTermino = pase.FechaTermino.ToString(),
                        Motivo = pase.Motivo,
                        Area = pase.Area,
                        Tipo = pase.Tipo.ToString(),
                        Estado = pase.Estado.ToString(),
                        PersonaExternasRel = personasExternasPase
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