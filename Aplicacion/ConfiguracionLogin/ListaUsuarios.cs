using Dominio.Entidades;
using MediatR;
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
        {}

        public class Manejador : IRequestHandler<Ejecuta, List<Usuario>>
        {
            private readonly SistemaPasesContext _context;
            public Manejador(SistemaPasesContext context) => this._context = context;


            public async Task<List<Usuario>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                await this._context.PersonaExterna.FindAsync(new Guid());
                var usuarios = this._context.Usuario.ToList();
                return usuarios;
            }
        }

    }
}
