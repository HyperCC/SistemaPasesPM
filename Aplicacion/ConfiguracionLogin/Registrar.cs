using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using Dominio.ModelosDto;
using FluentValidation;
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
        public class Ejecuta : IRequest<UsuarioData>
        {
            // datos recibidos por formulario
            public string Rut { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Correo { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }

            // por defecto el capcha viene no validado
            public bool Captcha { get; set; }
            public bool NoPerteneceEmpresa { get; set; }
            public Ejecuta()
            {
                this.Rut = null;
                this.Nombres = null;
                this.Apellidos = null;
                this.Correo = null;
                this.RutEmpresa = null;
                this.NombreEmpresa = null;
                this.Captcha = false;
                this.NoPerteneceEmpresa = false;
            }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.Correo).NotEmpty().NotNull();
                this.RuleFor(x => x.Rut).NotEmpty().NotNull();
            }
        }

        /// <summary>
        /// Logica principal del mediador
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
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
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // verificar que el email sea unico o no exista ya en la DB
                var correoExiste = await this._context.Usuario.Where(x => x.Email == request.Correo).AnyAsync();

                if (correoExiste)
                    throw new CorreoExisteException(HttpStatusCode.BadRequest,
                      new
                      {
                          mensaje = $"Ya existe un usuario registrado con el Email {request.Correo}",
                          status = HttpStatusCode.BadRequest,
                          tipoError = "re-ce"
                      });

                // verificar que el rut sea unico o no exista ya en la DB
                var rutExiste = await this._context.Persona.Where(x => x.Rut == request.Rut).AnyAsync();

                if (rutExiste)
                    throw new RutExisteException(HttpStatusCode.BadRequest,
                        new
                        {
                            mensaje = $"Ya existe un usuario registrado con el Rut {request.Rut}",
                            status = HttpStatusCode.BadRequest,
                            tipoError = "re-re"
                        });

                // creacion del nuevo usuario y los datos relacionados
                var usuarioGenerado = new Usuario
                {
                    UsuarioId = new Guid(),
                    Email = request.Correo,
                    UserName = request.Correo,
                    NoPerteneceEmpresa = request.NoPerteneceEmpresa
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
                    var nuevoNombre = new TipoNombre();
                    if (nombreExiste == null)
                    {
                        nuevoNombre.TipoNombreId = new Guid();
                        nuevoNombre.Nombre = nombre;
                        nuevoNombre.Tipo = TipoNombre.TipoIdentificador.NOMBRE;
                        nuevoNombre.Posicion = currentIteration;
                    }
                    this._context.TipoNombre.Add(nuevoNombre);

                    // agregar tabla con nombres y personas 
                    var nuevoPersonaTipoNombre = new PersonaTipoNombre
                    {
                        PersonaId = personaGenerada.PersonaId,
                        TipoNombreId = (nombreExiste == null) ?
                        nuevoNombre.TipoNombreId : nombreExiste.TipoNombreId
                    };
                    this._context.PersonaTipoNombre.Add(nuevoPersonaTipoNombre);

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
                    var nuevoApellido = new TipoNombre();
                    if (apellidoExiste == null)
                    {
                        nuevoApellido.TipoNombreId = new Guid();
                        nuevoApellido.Nombre = apellido;
                        nuevoApellido.Tipo = TipoNombre.TipoIdentificador.APELLIDO;
                        nuevoApellido.Posicion = currentIteration;
                    }
                    this._context.TipoNombre.Add(nuevoApellido);

                    // agregar tabla con nombres y personas 
                    var nuevoPersonaTipoNombre = new PersonaTipoNombre
                    {
                        PersonaId = personaGenerada.PersonaId,
                        TipoNombreId = (apellidoExiste == null) ?
                        nuevoApellido.TipoNombreId : apellidoExiste.TipoNombreId
                    };
                    this._context.PersonaTipoNombre.Add(nuevoPersonaTipoNombre);

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
                //await this._context.Usuario.AddAsync(usuarioGenerado);

                // verificar si se pudo crear el UsuarioData y retornarlo
                var resultado = await this._context.SaveChangesAsync();
                if (resultado > 0)
                {
                    // guardar el usuario creado
                    var resultadoUser = await this._userManager.CreateAsync(usuarioGenerado);

                    if (resultadoUser.Succeeded)
                        //TODO: agregar devolucion de usuario DTO
                        return new UsuarioData
                        {
                            Nombres = "nombres",
                            Apellidos = "apellidos",
                            UserName = usuarioGenerado.Email,
                            Token = null,
                            Email = usuarioGenerado.Email
                        };
                }

                throw new Exception("Error en el servidor - No se pudo agregar el nuevo usuario..");
            }
        }
    }
}