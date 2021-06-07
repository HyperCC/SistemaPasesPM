using Aplicacion.Cuentas;
using Dominio.ModelosDto;
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
    /// Cotrolador para los distintos usuarios
    /// </summary>
    [AllowAnonymous]
    public class CuentaController : PersonalController
    {
        /// <summary>
        /// Cuenta de un usuario segun rol 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<CuentaUsuarioData>> CuentaUsuario()
        {
            return await this.MediadorHerencia.Send(new CuentaUsuario.Ejecuta());
        }
    }
}