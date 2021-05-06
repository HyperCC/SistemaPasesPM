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

namespace Aplicacion.Cuentas
{
    public class CuentaUsuario
    {
        public class Ejecuta : IRequest<UsuarioDto>
        { }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioDto>
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

            public async Task<UsuarioDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // obtener al ususario con sesion actual 
                var usuario = await this._userManager.FindByNameAsync(this._usuarioSesion.ObtenerUsuarioSesion());

                var usuar = await this._context.Usuario.Where(x => x.UserName == this._usuarioSesion.ObtenerUsuarioSesion())
                    .Include(x => x.PersonaRel)
                    .Include(x => x.EmpresaRel)
                    .Include(x => x.PasesRel)
                    .FirstOrDefaultAsync();

                var usuariosDto = (usuar == null) ? new UsuarioDto() : this._mapper.Map<Usuario, UsuarioDto>(usuar);
                return usuariosDto;
            }
        }
    }
}
