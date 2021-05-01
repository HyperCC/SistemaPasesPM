using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.ConfiguracionLogin
{
    /// <summary>
    /// Operaciones generar tokens de login para el Usuario
    /// </summary>
    public class Login
    {
        public class Ejecuta : IRequest<Usuario>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {
            private readonly SistemaPasesContext _context;
            private readonly UserManager<Usuario> _usuarioManager;
            private readonly SignInManager<Usuario> _signInManager;

            public Manejador(SistemaPasesContext context,
                UserManager<Usuario> usuarioManager,
                SignInManager<Usuario> signInManager)
            {
                this._context = context;
                this._usuarioManager = usuarioManager;
                this._signInManager = signInManager;
            }

            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
