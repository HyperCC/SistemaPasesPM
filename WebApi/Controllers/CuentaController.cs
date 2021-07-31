using Aplicacion.Cuentas;
using Dominio.ModelosDto;
using Dominio.ModelosDto.ModelosParaPerfil;
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
        /// Cuenta de un usuario solicitante 
        /// </summary>
        /// <returns></returns>
        [HttpGet("solicitante")]
        public async Task<ActionResult<PasesUsuarioData>> CuentaSolicitante()
        {
            return await this.MediadorHerencia.Send(new CuentaUsuario.Ejecuta());
        }

        /// <summary>
        /// Cuenta de un usuario administrador
        /// </summary>
        /// <returns></returns>
        [HttpGet("admin")]
        public async Task<ActionResult<CuentaAdminData>> CuentaAdmin()
        {
            return await this.MediadorHerencia.Send(new CuentaAdmin.Ejecuta());
        }

        /// <summary>
        /// Cuenta de un usuario administrador
        /// </summary>
        /// <returns></returns>
        [HttpGet("aprobador")]
        public async Task<ActionResult<PasesUsuarioData>> CuentaAprobador()
        {
            return await this.MediadorHerencia.Send(new CuentaAprobador.Ejecuta());
        }
    }
}