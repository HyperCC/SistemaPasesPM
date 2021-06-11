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
    public class CuentaAdmin
    {
        public class Ejecuta : IRequest<CuentaAdminData>
        { }

        public class Manejador : IRequestHandler<Ejecuta, CuentaAdminData>
        {
            private readonly SistemaPasesContext _context;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly UserManager<Usuario> _usuarioManager;

            public Manejador(SistemaPasesContext context
                , IUsuarioSesion usuarioSesion
                , UserManager<Usuario> usuarioManager)
            {
                this._context = context;
                this._usuarioSesion = usuarioSesion;
                this._usuarioManager = usuarioManager;
            }

            public async Task<CuentaAdminData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // obtener los usuarios existentes
                var usuarios = await this._context.Usuario
                    .Include(x => x.EmpresaRel)
                    .Include(x => x.PersonaRel.NombresRel)
                    .ThenInclude(z => z.NombreRel)
                    .Include(x => x.PersonaRel.ApellidosRel)
                    .ThenInclude(z => z.ApellidoRel)
                    .ToListAsync();

                List<UsuarioData> usuariosModelados = new List<UsuarioData>();

                // modelar los usuarios obtenidos
                foreach (var usuario in usuarios)
                {
                    // obtener rol del usuario
                    var roles = await this._usuarioManager.GetRolesAsync(usuario);
                    if (roles.Count() > 0)
                        if (roles[0] == "ADMIN")
                            continue;

                    // obtencion de nombre completo
                    string nombres = string.Join(" ",
                        usuario.PersonaRel.NombresRel
                        .Select(x => x.NombreRel.Titulo));

                    string apellidos = string.Join(" ",
                        usuario.PersonaRel.ApellidosRel
                        .Select(x => x.ApellidoRel.Titulo));

                    // usuario modelado
                    UsuarioData usuarioModelado = new UsuarioData
                    {
                        NombreCompleto = nombres + " " + apellidos,
                        Rut = usuario.PersonaRel.Rut,
                        Pasaporte = "",
                        Email = usuario.Email,
                        NombreEmpresa = usuario.EmpresaRel.Nombre,
                        Rol = roles.Count() > 0 ? roles[0] : "sin rol"
                    };

                    usuariosModelados.Add(usuarioModelado);
                }

                // lista con todos los usuarios para administrar
                CuentaAdminData cuentaAdmin = new CuentaAdminData
                {
                    UsuariosAll = usuariosModelados
                };

                return cuentaAdmin;
            }
        }
    }
}