﻿using Aplicacion.ConfiguracionLogin;
using Dominio.Entidades;
using Dominio.ModelosDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.ControllerPersonalizado;

namespace WebApi.Controllers
{
    /// <summary>
    /// Controladores para las operaciones con Usuarios
    /// </summary>
    [AllowAnonymous]
    public class UsuarioController : PersonalController
    {
        /// <summary>
        /// Login de usaurios
        /// </summary>
        /// <param name="parametros">correo y contraseña</param>
        /// <returns>codigo de estado http y datos del usuario</returns>
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        /// <summary>
        /// Registro de usuarios
        /// </summary>
        /// <param name="parametros">datos del formulario del cliente</param>
        /// <returns>codigo de estado http y datos relacionados</returns>
        [HttpPost("registrar")]
        public async Task<ActionResult<Unit>> Registrar(Registrar.Ejecuta parametros)
        {
            return await this.MediadorHerencia.Send(parametros);
        }

        /// <summary>
        /// Obtener el usuario actual en sesion 
        /// </summary>
        /// <returns>usuario con sesion</returns>
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> UsuarioActual()
        {
            return await this.MediadorHerencia.Send(new UsuarioActual.Ejecuta());
        }

        [HttpPost("reset")]
        public async Task<ActionResult<Usuario>> ResetPass(ResetContrasena.Ejecuta parametros)
        {
            return await this.MediadorHerencia.Send(parametros);
        }

        /// <summary>
        /// Cambiar la contraseña actual
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [HttpPost("change")]
        public async Task<ActionResult<Unit>> ChangePass(CambiarContrasena.Ejecuta parametros)
        {
            return await this.MediadorHerencia.Send(parametros);
        }
    }
}
