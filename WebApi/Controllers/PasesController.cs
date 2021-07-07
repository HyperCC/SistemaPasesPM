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
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class PasesController : PersonalController
    {
        [HttpPost("Upload")]
        public async Task<ActionResult<Documento>> GuardarDoc([FromForm] AlmacenarDocumentoEmpresa.Ejecuta parametros)
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

        /// <summary>
        /// registrar un pase de tipo contratista
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [HttpPost("ingresarContratista/{id}")]
        public async Task<ActionResult<Unit>> IngresarContratista([FromBody] NuevoPaseContratista.Ejecuta parametros)
           => await this.MediadorHerencia.Send(parametros);

        /// <summary>
        /// registrar un pase generico
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [HttpPost("ingresar")]
        public async Task<ActionResult<Unit>> Ingresar(NuevoPaseGenerico.Ejecuta parametros)
            => await this.MediadorHerencia.Send(parametros);

        /// <summary>
        /// cambiar el estado de un pase por un estado existente
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        [HttpPost("cambiarEstado")]
        public async Task<ActionResult<Unit>> CambiarEstado(CambiarEstadoPaseGenerico.Ejecuta parametros)
            => await this.MediadorHerencia.Send(parametros);

    }
}