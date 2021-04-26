using Aplicacion.ConfiguracionLogin;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
            => this._mediator = mediator;

        /// <summary>
        /// Registro de usuarios
        /// </summary>
        /// <param name="parametros">datos del formulario del cliente</param>
        /// <returns>codigo de estado http y datos relacionados</returns>
        public async Task<ActionResult<Usuario>> Registrar(Registrar.Ejecuta parametros)
        {
            return await this._mediator.Send(parametros);
        }
    }
}
