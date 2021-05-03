using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.ConfiguracionLogin
{
    public class ListaUsuarios
    {
        public class Ejecuta : IRequest<List<Usuario>>
        { }

        public class Manejador : IRequestHandler<Ejecuta, List<Usuario>>
        {
            private readonly SistemaPasesContext _context;
            private readonly UserManager<Usuario> _usuarioManager;

            public Manejador(SistemaPasesContext context, UserManager<Usuario> usuarioManager)
            {
                this._context = context;
                this._usuarioManager = usuarioManager;
            }

            public async Task<List<Usuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                await this._context.TipoDocumento.FindAsync(new Guid());
                var usuarios = _usuarioManager.Users.ToList();
                return usuarios;
            }
        }

    }
}
