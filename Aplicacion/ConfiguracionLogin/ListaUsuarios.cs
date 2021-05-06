using Aplicacion.ExcepcionesPersonalizadas;
using AutoMapper;
using Dominio.Entidades;
using Dominio.ModelosDto;
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

namespace Aplicacion.ConfiguracionLogin
{
    public class ListaUsuarios
    {
        public class Ejecuta : IRequest<List<UsuarioDto>>
        { }

        public class Manejador : IRequestHandler<Ejecuta, List<UsuarioDto>>
        {
            private readonly SistemaPasesContext _context;
            private readonly IMapper _mapper;

            public Manejador(SistemaPasesContext context,
                UserManager<Usuario> usuarioManager,
                IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public async Task<List<UsuarioDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuarios = await this._context.Usuario
                    .Include(x => x.PersonaRel)
                    .Include(x => x.PersonaRel.TipoNombresRel)
                    .ThenInclude(z => z.TipoNombreRel)
                    .ToListAsync();

                foreach (var usu in usuarios)
                {
                    Console.WriteLine($"usuario email: {usu.Email}");

                    foreach (var per in usu.PersonaRel.TipoNombresRel)
                    {
                        Console.WriteLine($"Nombre: {per.TipoNombreRel.Nombre}");
                    }
                    Console.WriteLine();
                }
                   

                var usuariosDto = this._mapper.Map<List<Usuario>, List<UsuarioDto>>(usuarios);

                return usuariosDto;
            }
        }
    }
}