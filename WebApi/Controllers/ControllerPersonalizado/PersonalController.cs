using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.ControllerPersonalizado
{
    /// <summary>
    /// Controlador personalizado con el constructor y MediatR ya instanciado
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private IMediator _mediator;

        // en caso de que _mediator es null, su valor será HttpContext.RequestServices.GetService<IMediator>()
        protected IMediator MediadorHerencia
            => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
