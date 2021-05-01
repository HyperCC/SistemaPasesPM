using Aplicacion.ConfiguracionLogin;
using Dominio.Entidades;
using MediatR;
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
    public class UsuarioController : PersonalController
    {
        /// <summary>
        /// Login de usaurios
        /// </summary>
        /// <param name="parametros">correo y contraseña</param>
        /// <returns>codigo de estado http y datos del usuario</returns>
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login(Login.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }

        /// <summary>
        /// Registro de usuarios
        /// </summary>
        /// <param name="parametros">datos del formulario del cliente</param>
        /// <returns>codigo de estado http y datos relacionados</returns>
        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar([FromForm] Registrar.Ejecuta parametros)
        {
            return await this.MediadorHerencia.Send(parametros);
        }
    }
}
