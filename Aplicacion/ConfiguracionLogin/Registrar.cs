﻿using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using Dominio.ModelosDto;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using Persistencia.AuxiliaresAlmacenamiento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public class Ejecuta : IRequest
        {
            // datos recibidos por formulario
            public string Rut { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Correo { get; set; }
            public string RutEmpresa { get; set; }
            public string NombreEmpresa { get; set; }

            // por defecto el capcha viene no validado
            public bool Captcha { get; set; } = false;
            public bool NoPerteneceEmpresa { get; set; } = false;
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.Rut).NotEmpty();
                this.RuleFor(x => x.Nombres).NotEmpty();
                this.RuleFor(x => x.Apellidos).NotEmpty();
                this.RuleFor(x => x.Correo).NotEmpty();
                this.RuleFor(x => x.NombreEmpresa).NotEmpty();
                this.RuleFor(x => x.RutEmpresa).NotEmpty();
                this.RuleFor(x => x.Captcha).NotEmpty();
                this.RuleFor(x => x.NoPerteneceEmpresa).NotNull();
            }
        }

        /// <summary>
        /// Logica principal del mediador
        /// </summary>
        public class Manejador : IRequestHandler<Ejecuta>
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
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
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
                         mensaje = $"Los datos recibidos por el usaurio no cumplen con el formato solicitado.",
                         status = HttpStatusCode.BadRequest,
                         tipoError = "adv-fie000",
                         listaErrores = erroresFV
                     });
                }

                // verificar que el email sea unico o no exista ya en la DB
                var correoExiste = await this._context.Usuario.Where(x => x.Email == request.Correo).AnyAsync();

                if (correoExiste)
                    throw new CorreoExisteException(HttpStatusCode.BadRequest,
                      new
                      {
                          mensaje = $"Ya existe un usuario registrado con el Email {request.Correo}",
                          status = HttpStatusCode.BadRequest,
                          tipoError = "adv-cee000"
                      });

                // verificar que el rut sea unico o no exista ya en la DB
                var rutExiste = await this._context.Persona.Where(x => x.Rut == request.Rut).AnyAsync();

                if (rutExiste)
                    throw new RutExisteException(HttpStatusCode.BadRequest,
                        new
                        {
                            mensaje = $"Ya existe un usuario registrado con el Rut {request.Rut}",
                            status = HttpStatusCode.BadRequest,
                            tipoError = "adv-ree000"
                        });

                // creacion del nuevo usuario y los datos relacionados

                var usuarioGenerado = new Usuario
                {
                    Email = request.Correo,
                    UserName = request.Correo,
                    NoPerteneceEmpresa = request.NoPerteneceEmpresa
                };

                // persona y relacion directa
                var personaGenerada = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = request.Rut,
                    Pasaporte = ""
                };

                // almacenamiento y relacion
                await this._context.Persona.AddAsync(personaGenerada);
                usuarioGenerado.PersonaId = personaGenerada.PersonaId;

                // proceso completo de registrar nombres en la base de datos
                await AlmacenarNombres.AgregarNombres(request.Nombres, request.Apellidos, this._context, personaGenerada.PersonaId);

                // buscar empresa perteneciente.
                var empresaExiste = await BuscarOAlmacenarEmpresa.BuscarOAgregarEmpresa(this._context
                    , request.RutEmpresa
                    , request.NombreEmpresa);

                // relacionar empresa con usuario
                usuarioGenerado.EmpresaId = empresaExiste.EmpresaId;

                // verificar si se pudo crear el UsuarioData y retornarlo
                var resultado = await this._context.SaveChangesAsync();
                if (resultado > 0)
                {
                    // guardar el usuario creado
                    var resultadoUser = await this._userManager.CreateAsync(usuarioGenerado, "Hol@Hol@123");

                    if (resultadoUser.Succeeded)
                    {
                        // agregar el rol correspondiente
                        var usuarioNuevoActual = await this._userManager.FindByNameAsync(usuarioGenerado.UserName);
                        await this._userManager.AddToRoleAsync(usuarioNuevoActual, "SOLICITANTE");

                        //TODO: enviar un EMAIL al nuevo usuario notificando la creacion de la cuenta
                        return Unit.Value;
                    }

                    else
                    {
                        throw new UserManagerNoGuardadoException(HttpStatusCode.BadRequest,
                            new
                            {
                                mensaje = $"El sistema no pudo registrar al usuario {request.Correo}",
                                status = HttpStatusCode.BadRequest,
                                tipoError = "err-umnge0"
                            });
                    }
                }
                throw new DbContextNoGuardadoException(HttpStatusCode.BadRequest,
                    new
                    {
                        mensaje = $"El sistema no pudo registrar los datos relacionados al usaurio {request.Correo}",
                        status = HttpStatusCode.BadRequest,
                        tipoError = "err-dbcng0"
                    });
            }
        }
    }
}