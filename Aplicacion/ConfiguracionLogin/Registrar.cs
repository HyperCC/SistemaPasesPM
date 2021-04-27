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
                    Correo = request.CorreoElectronico,
                };

                // persona y relacion directa
                var personaGenerada = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = request.Rut
                };

                // almacenamiento y relacion
                await this._context.Persona.AddAsync(personaGenerada);
                usuarioGenerado.PersonaId = personaGenerada.PersonaId;

                // obtencion de nombres de la cadena bruta
                string[] nombres = request.Nombres.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                // asignacion de los nuevos nombres
                int currentIteration = 1;
                foreach (var nombre in nombres)
                {
                    // buscar si existe el nombre
                    var nombreExiste = await this._context.TipoNombre
                        .Where(x => x.Nombre == nombre
                        && x.Tipo == TipoNombre.TipoIdentificador.NOMBRE
                        && x.Posicion == currentIteration)
                        .FirstOrDefaultAsync();

                    // agregar el nuevo nombre si no existe
                    if (nombreExiste == null)
                    {
                        var guidNuevoNombre = new Guid();

                        this._context.TipoNombre.Add(
                            new TipoNombre
                            {
                                TipoNombreId = guidNuevoNombre,
                                Nombre = nombre,
                                Tipo = TipoNombre.TipoIdentificador.NOMBRE,
                                Posicion = currentIteration
                            });

                        // agregar tabla con nombres y personas 
                        this._context.PersonaTipoNombre.Add(
                            new PersonaTipoNombre
                            {
                                PersonaId = personaGenerada.PersonaId,
                                TipoNombreId = guidNuevoNombre
                            });
                    }
                    else
                    {
                        // agregar tabla con nombres y personas 
                        this._context.PersonaTipoNombre.Add(
                            new PersonaTipoNombre
                            {
                                PersonaId = personaGenerada.PersonaId,
                                TipoNombreId = nombreExiste.TipoNombreId
                            });
                    }

                    currentIteration++;
                }

                // obtencion de apellidos de la cadena bruta
                string[] apellidos = request.Apellidos.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                // asignacion de los nuevos apellidos
                currentIteration = 1;
                foreach (var apellido in apellidos)
                {
                    // buscar si existe el apellido
                    var apellidoExiste = await this._context.TipoNombre
                        .Where(x => x.Nombre == apellido
                        && x.Tipo == TipoNombre.TipoIdentificador.APELLIDO
                        && x.Posicion == currentIteration)
                        .FirstOrDefaultAsync();

                    // agregar el nuevo nombre si no existe
                    if (apellidoExiste == null)
                    {
                        var guidNuevoApellido = new Guid();

                        this._context.TipoNombre.Add(
                            new TipoNombre
                            {
                                TipoNombreId = guidNuevoApellido,
                                Nombre = apellido,
                                Tipo = TipoNombre.TipoIdentificador.APELLIDO,
                                Posicion = currentIteration
                            });

                        // agregar tabla con nombres y personas 
                        this._context.PersonaTipoNombre.Add(
                            new PersonaTipoNombre
                            {
                                PersonaId = personaGenerada.PersonaId,
                                TipoNombreId = guidNuevoApellido
                            });
                    }
                    else
                    {
                        // agregar tabla con nombres y personas 
                        this._context.PersonaTipoNombre.Add(
                            new PersonaTipoNombre
                            {
                                PersonaId = personaGenerada.PersonaId,
                                TipoNombreId = apellidoExiste.TipoNombreId
                            });
                    }
                    currentIteration++;
                }

                // buscar empresa perteneciente.
                var empresaExiste = await this._context.Empresa.Where(x => x.Rut == request.RutEmpresa).FirstOrDefaultAsync();

                // si no existe la empresa se crea una nueva
                if (empresaExiste == null)
                {
                    var empresaGenerada = new Empresa
                    {
                        EmpresaId = new Guid(),
                        Rut = request.RutEmpresa,
                        Nombre = request.NombreEmpresa
                    };

                    // agregar la nueva empresa
                    await this._context.Empresa.AddAsync(empresaGenerada);
                    // relacionar empresa con usuario
                    usuarioGenerado.EmpresaId = empresaGenerada.EmpresaId;
                }
                else
                {
                    usuarioGenerado.EmpresaId = empresaExiste.EmpresaId;
                }

                // guardar el usuario creado
                await this._context.Usuario.AddAsync(usuarioGenerado);

                // verificar si se pudo crear el UsuarioData y retornarlo
                var resultado = await this._context.SaveChangesAsync();
                if (resultado > 0)
                {
                    //TODO: agregar devolucion de usuario DTO
                    return usuarioGenerado;
                }

                throw new Exception("No se pudo agregar el nuevo usuario");
            }
        }
    }
}
