using Aplicacion.Auxiliares;
using Aplicacion.ConfiguracionLogin.Contratos;
using Aplicacion.ConfiguracionLogin.TokenSeguridad;
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
    /// Operaciones generar tokens de login para el Usuario
    /// </summary>
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _usuarioManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly SistemaPasesContext _context;

            public Manejador(UserManager<Usuario> usuarioManager,
                SignInManager<Usuario> signInManager,
                IJwtGenerador jwtGenerador,
                SistemaPasesContext context)
            {
                this._usuarioManager = usuarioManager;
                this._signInManager = signInManager;
                this._jwtGenerador = jwtGenerador;
                this._context = context;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // validacion del formato del request
                EjecutaValidacion validator = new EjecutaValidacion();
                var validacionesRes = validator.Validate(request);

                // en caso de no obtener datos validos
                if (!validacionesRes.IsValid)
                {
                    List<string> erroresFV = new List<string>();
                    // listar los mensajes de error obtenidos
                    foreach (var failure in validacionesRes.Errors)
                        erroresFV.Add(failure.ErrorMessage);

                    // devolver una excepcion y los erroes encontrados
                    throw new FormatoIncorrectoException(HttpStatusCode.BadRequest,
                     new
                     {
                         mensaje = $"Los datos recibidos por el usuario no cumplen con el formato solicitado.",
                         status = HttpStatusCode.BadRequest,
                         tipoError = "adv-fie000",
                         listaErrores = erroresFV
                     });
                }

                // verificar que el email del usuario existe 
                var usuario = await this._usuarioManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                    throw new CorreoNoExisteException(HttpStatusCode.Unauthorized,
                        new
                        {
                            mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                            status = HttpStatusCode.Unauthorized,
                            tipoError = "adv-cnee00"
                        });

                // verificar que la password sea correcta
                var resultado = await this._signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                // devolver datos del usuario logueado o lanzar 401
                if (resultado.Succeeded)
                {
                    // obtener los roles del usuario
                    var roles = await this._usuarioManager.GetRolesAsync(usuario);

                    if (roles[0] == "SOLICITANTE")
                    {
                        // obtener datos de la cuenta 
                        var usuarioDatosComplementarios = await this._context.Usuario
                            .Include(x => x.EmpresaRel)
                            .Include(x => x.PersonaRel.TipoNombresRel)
                            .ThenInclude(z => z.TipoNombreRel)
                            .FirstOrDefaultAsync(x => x.Email == request.Email);

                        string nombres = string.Empty, apellidos = string.Empty;

                        // obtencion del nombre completo
                        foreach (var nomb in usuario.PersonaRel.TipoNombresRel.OrderBy(x => x.TipoNombreRel.Posicion))
                        {
                            // concatenacion de nombres y apellidos
                            if (nomb.TipoNombreRel.Tipo == TipoIdentificador.NOMBRE)
                                nombres += nomb.TipoNombreRel.Nombre + " ";
                            else
                                apellidos += nomb.TipoNombreRel.Nombre + " ";
                        }

                        // perfil del usuario solicitante
                        return new UsuarioData
                        {
                            NombreCompleto = (nombres + ((apellidos.Length > 0) ? apellidos.Remove(apellidos.Length - 1) : apellidos)),
                            Rut = usuarioDatosComplementarios.PersonaRel.Rut,
                            NombreEmpresa = usuarioDatosComplementarios.EmpresaRel.Nombre,
                            Rol = roles[0],
                            Token = this._jwtGenerador.CreateToken(usuario, new List<string>()),
                            Email = usuario.Email
                        };
                    }

                    // perfil del resto de los roles
                    return new UsuarioData
                    {
                        NombreCompleto = "",
                        Rut = "",
                        NombreEmpresa = "",
                        Rol = roles[0],
                        Token = this._jwtGenerador.CreateToken(usuario, new List<string>()),
                        Email = usuario.Email
                    };
                }

                // usuario no autorizado
                throw new PasswordIncorrectoException(HttpStatusCode.Unauthorized,
                    new
                    {
                        mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                        status = HttpStatusCode.Unauthorized,
                        tipoError = "adv-pie000"
                    });
            }
        }

    }
}