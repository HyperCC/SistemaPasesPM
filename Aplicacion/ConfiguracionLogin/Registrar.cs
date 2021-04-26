using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        /// <summary>
        /// Declaracion del mediador
        /// </summary>
        public class Ejecuta : IRequest<Usuario>
        {
            // datos recibidos por formulario
            public string Rut { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string CorreoElectronico { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }

            // por defecto el capcha viene no validado
            public bool Captcha { get; set; } = false;
        }

        /// <summary>
        /// Logica principal del mediador
        /// </summary>
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

            /// <summary>
            /// Operacion registrar
            /// </summary>
            /// <param name="request">datos recibidos por el controlador</param>
            /// <param name="cancellationToken">indicador de cancelacion de solicitud</param>
            /// <returns>codigo de estado http</returns>
            public async Task<Usuario> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // verificar que el email sea unico o no exista ya en la DB
                var correoExiste = await this._context.Usuario.Where(x => x.Correo == request.CorreoElectronico).AnyAsync();

                if (correoExiste)
                    throw new ManejadorException(HttpStatusCode.BadRequest,
                        new { mensaje = $"Ya existe un usuario registrado con el Email {request.CorreoElectronico}" });

                // verificar que el rut sea unico o no exista ya en la DB
                var rutExiste = await this._context.Persona.Where(x => x.Rut == request.Rut).AnyAsync();

                if (rutExiste)
                    throw new ManejadorException(HttpStatusCode.BadRequest,
                        new { mensaje = $"Ya existe un usuario registrado con el Rut {request.Rut}" });

                // creacion del nuevo usuario y los datos relacionados
                var usuarioGenerado = new Usuario
                {
                    Correo = request.CorreoElectronico
                };

                var personaGenerada = new Persona
                {
                    Rut = request.Rut
                };

                // obtencion de nombres 
                string[] nombres = request.Nombres.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach(var nombre in nombres)
                {
                    var nom = new TipoNombre
                    {
                        Nombre=nombre,

                    };
                }


            }
        }
    }
}
