﻿using System;
using System.Collections.Generic;
using System.Text;
using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using Dominio.ModelosDto;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Aplicacion.ConfiguracionLogin.Contratos;
using System.Threading;
using System.Threading.Tasks;


namespace Aplicacion.ConfiguracionLogin
{
    /// <summary>
    /// cambiar contraseña de un usuario validando la contraseña actual
    /// </summary>
    public class CambiarContrasena
    {
        public class Ejecuta : IRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
            public string ConfirmedNewPassword { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.NewPassword).NotEmpty();
                RuleFor(x => x.ConfirmedNewPassword).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _usuarioManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;

            public Manejador(UserManager<Usuario> usuarioManager,
                SignInManager<Usuario> signInManager,
                IJwtGenerador jwtGenerador)
            {
                this._usuarioManager = usuarioManager;
                this._signInManager = signInManager;
                this._jwtGenerador = jwtGenerador;
            }

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
                         mensaje = $"Los datos recibidos por el usuario no cumplen con el formato solicitado.",
                         status = HttpStatusCode.BadRequest,
                         tipoError = "adv-fie000",
                         listaErrores = erroresFV
                     });
                }

                // verificar si la confirmacion del nuevo password es correcta
                if (request.NewPassword != request.ConfirmedNewPassword)
                    throw new PasswordNuevaMalConfirmadaException(HttpStatusCode.BadRequest,
                            new
                            {
                                mensaje = "Las confirmacion de el nuevo password no coincide con el nuevo password.",
                                status = HttpStatusCode.Unauthorized,
                                tipoError = "adv-pnmce0"
                            });

                // verificar que el email del usuario existe 
                var usuario = await this._usuarioManager.FindByEmailAsync(request.Email);
                if (usuario == null)
                    throw new CorreoNoExisteException(HttpStatusCode.BadRequest,
                        new
                        {
                            mensaje = "Las credenciales de acceso entregadas no coinciden con los registros.",
                            status = HttpStatusCode.Unauthorized,
                            tipoError = "adv-cnee00"
                        });

                // verificar que la password sea correcta
                var resultado = await this._signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if (resultado.Succeeded == false)
                    throw new PasswordIncorrectoException(HttpStatusCode.Unauthorized,
                    new
                    {
                        mensaje = $"Las credenciales de acceso entregadas no coinciden con los registros.",
                        status = HttpStatusCode.BadRequest,
                        tipoError = "adv-pie000"
                    });

                // en caso de que la password nueva sea igual a la anterior
                if (request.Password == request.NewPassword)
                    throw new PasswordDuplicadaException(HttpStatusCode.Unauthorized,
                   new
                   {
                       mensaje = "La contraseña ingresada es identica a la actual.",
                       status = HttpStatusCode.BadRequest,
                       tipoError = "adv-pde000"
                   });

                // lista con errores de sintaxis especificos
                List<string> erroresUMP = new List<string>();

                if (request.NewPassword.Length < 32)
                {
                    var result = await this._usuarioManager.ChangePasswordAsync(usuario, request.Password, request.NewPassword);

                    // devolver datos del usuario o lanzar 400
                    if (result.Succeeded)
                    {
                        // TODO: envair EMAIL al usuario con la nueva clave desde aqui
                        return Unit.Value;
                    }

                    // obtener todos los errores en las password
                    foreach (var err in result.Errors)
                    {
                        switch (err.Code)
                        {
                            case "PasswordTooShort":
                                erroresUMP.Add("La password debe tener al menos 6 caracteres.");
                                break;

                            case "PasswordRequiresNonAlphanumeric":
                                erroresUMP.Add("La password debe tener al menos un caracter no alfanumerico.");
                                break;

                            case "PasswordRequiresDigit":
                                erroresUMP.Add("La password debe tener al menos un numero.");
                                break;

                            case "PasswordRequiresLower":
                                erroresUMP.Add("La password debe tener al menos una letra minúscula.");
                                break;

                            case "PasswordRequiresUpper":
                                erroresUMP.Add("La password debe tener al menos una letra mayúscula.");
                                break;

                            case "PasswordRequiresUniqueChars":
                                erroresUMP.Add("La password debe estar compuesta por caracteres no repetidos.");
                                break;

                            default:
                                erroresUMP.Add("error de formato en password desconocido: " + err.Description);
                                break;
                        }
                    }
                }
                else erroresUMP.Add("La password no debe ser de más de 32 carácteres.");

                // errores de sintaxis durante el cambio de clave
                throw new PasswordSintaxisException(HttpStatusCode.BadRequest,
                    new
                    {
                        mensaje = "La nueva contraseña ingresada no cumple con los requisitos de sintaxis.",
                        status = HttpStatusCode.BadRequest,
                        tipoError = "adv-pse000",
                        listaErrores = erroresUMP
                    });
            }
        }
    }
}