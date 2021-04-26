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
    /// Registro comun de Usuarios
    /// </summary>
    public class Registrar
    {
        public class Ejecuta : IRequest<Usuario>
        {
            // principales datos recibidos por formulario
            public string Rut { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string CorreoElectronico { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }

            // por defecto el capcha viene no validado
            public bool Captcha { get; set; } = false;
        }

        public class Manejador : IRequestHandler<Ejecuta, Usuario>
        {
            // atributos iniciales
            private readonly SistemaPasesContext _context;
            private readonly UserManager<Usuario> _userManager;

            // constructor con DI
            public Manejador(SistemaPasesContext context, UserManager<Usuario> userManager)
            {
                this._context = context;
                this._userManager = userManager;
            }

            public Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
