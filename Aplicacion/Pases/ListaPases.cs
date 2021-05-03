using Dominio.Entidades;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Pases
{
    public class ListaPases
    {
        public class Ejecuta : IRequest<List<Pase>>
        { }


        public class Manejador : IRequestHandler<Ejecuta, List<Pase>>
        {
            private readonly SistemaPasesContext _context;

            public Manejador(SistemaPasesContext context)
                => this._context = context;

            public async Task<List<Pase>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                await this._context.TipoDocumento.FindAsync(new Guid());
                var pases = this._context.Pase.ToList();
                return pases;
            }
        }
    }
}
