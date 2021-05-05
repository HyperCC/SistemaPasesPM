using Microsoft.AspNetCore.Http;
using Aplicacion.Pases;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Controllers.ControllerPersonalizado;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class PasesController : PersonalController
    {
        [HttpPost("registrar")]
        public async Task<ActionResult<Pase>> Registrar(NuevoPaseVisita.Ejecuta parametros)
        {
            return await this.MediadorHerencia.Send(parametros);
        }


        /// <summary>
        /// Obtener todos los pases
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Pase>>> All()
            => await this.MediadorHerencia.Send(new ListaPases.Ejecuta());

    }
}
