using Aplicacion.ConfiguracionLogin;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.ControllerPersonalizado;

namespace WebApi.Controllers
{
    public class RolController : PersonalController
    {
        /// <summary>
        /// Crear un nuevo Rol si no existe
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(NuevoRol.Ejecuta parametros)
        {
            return await MediadorHerencia.Send(parametros);
        }
    }
}
