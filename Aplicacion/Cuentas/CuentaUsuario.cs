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

namespace Aplicacion.Cuentas
{
    public class CuentaUsuario
    {
        public class Ejecuta : IRequest<CuentaUsuarioData>
        { }

        public class Manejador : IRequestHandler<Ejecuta, CuentaUsuarioData>
        {
            // instancia de variables necesarias para obtener la sesion de un usuario
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly SistemaPasesContext _context;
            private readonly IMapper _mapper;

            public Manejador(UserManager<Usuario> userManager,
                IJwtGenerador jwtGenerador,
                IUsuarioSesion sesion,
                SistemaPasesContext context,
                IMapper mapper)
            {
                this._userManager = userManager;
                this._jwtGenerador = jwtGenerador;
                this._usuarioSesion = sesion;
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<CuentaUsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // obtener al ususario con sesion actual 
                //var usuario = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                // obtencion de las entidades relacionadas
                var usuario = await this._context.Usuario
                    .Include(x => x.EmpresaRel)
                    .Include(x => x.PasesRel)
                    .ThenInclude(z => z.PersonaExternasRel)
                    .Include(x => x.PersonaRel.TipoNombresRel)
                    .ThenInclude(z => z.TipoNombreRel)
                    .FirstOrDefaultAsync(x => x.UserName == this._usuarioSesion.ObtenerUsuarioSesion());

                // si en algun caso el Email del usuario ingresado no este almacenado
                if (usuario == null)
                {
                    throw new CorreoNoExisteException(HttpStatusCode.Unauthorized,
                       new
                       {
                           mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                           status = HttpStatusCode.Unauthorized,
                           tipoError = "adv-cnee00"
                       });
                }

                //var usuarioDto = this._mapper.Map<Usuario, UsuarioDto>(usuario);

                string nombres = string.Empty, apellidos = string.Empty;

                // obtencion del nombre completo
                foreach (var nomb in usuario.PersonaRel.TipoNombresRel.OrderBy(x => x.TipoNombreRel.Posicion))
                {
                    // concatenacion de nombres y apellidos
                    if (nomb.TipoNombreRel.Tipo == TipoIdentificador.NOMBRE)
                        nombres += nomb.TipoNombreRel.Nombre + " ";
                    else
                        apellidos += nomb.TipoNombreRel.Nombre + " ";
                }

                // obtencion de los pases a devolver
                ICollection<PasePerfil> pasesPerfil = new List<PasePerfil>();

                foreach (var pase in usuario.PasesRel)
                {
                    ICollection<PersonaExternaPase> personasExternasPase = new List<PersonaExternaPase>();
                    foreach (var personaExterna in pase.PersonaExternasRel)
                    {
                        var personaExternaEncontrada = await this._context.PersonaExterna
                            .Include(x => x.PersonaRel.TipoNombresRel)
                            .ThenInclude(z => z.TipoNombreRel)
                            .FirstOrDefaultAsync(x => x.PersonaExternaId == personaExterna.PersonaExternaId);

                        personasExternasPase.Add(new PersonaExternaPase
                        {
                            Nombres = "",
                            PrimerApellido = "",
                            SegundoApellido = "",
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
                        Estado = pase.Estado.ToString()
                    });
                }

                // modelo con los datos a usar en la cuenta del usuario comun
                var cuentaUsuarioata = new CuentaUsuarioData
                {
                    NombreCompleto = (nombres + ((apellidos.Length > 0) ? apellidos.Remove(apellidos.Length - 1) : apellidos)),
                    Rut = usuario.PersonaRel.Rut,
                    NombreEmpresa = usuario.EmpresaRel.Nombre,
                    PasesRel = pasesPerfil
                };

                return cuentaUsuarioata;
            }
        }
    }
}
