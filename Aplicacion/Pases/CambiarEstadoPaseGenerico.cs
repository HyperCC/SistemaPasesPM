using Aplicacion.ExcepcionesPersonalizadas;
using Dominio.Entidades;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Pases
{
    /// <summary>
    // cambiar el estado actual de un pase generico
    /// </summary>
    public class CambiarEstadoPaseGenerico
    {
        public class Ejecuta : IRequest
        {
            public string PaseId { get; set; }
            public string NuevoEstado { get; set; }
            public string Observacion { get; set; }
        }

        /// <summary>
        /// validaciones de contenido
        /// </summary>
        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                this.RuleFor(x => x.PaseId).NotEmpty();
                this.RuleFor(x => x.NuevoEstado).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            // referencia a base de datos
            private readonly SistemaPasesContext _context;
            public Manejador(SistemaPasesContext context)
            {
                this._context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                Guid idRecibida = Guid.Parse(request.PaseId);

                // buscar un pase por ID 
                Pase currentPase = await this._context.Pase.FindAsync(idRecibida);
                var estadoNuevo = request.NuevoEstado.ToUpper();

                // verificar que exista el enum de estado entregado
                bool existeEstadoNuevo = Enum.IsDefined(typeof(EstadoPase), estadoNuevo);
                if (!existeEstadoNuevo)
                    throw new EstadoNoExisteException(HttpStatusCode.BadRequest,
                           new
                           {
                               mensaje = $"El estado entregado {request.NuevoEstado} no existe en el sistema.",
                               status = HttpStatusCode.BadRequest,
                               tipoError = "adv-enee00"
                           });

                Console.WriteLine("------------------------------------------");
                Console.WriteLine(estadoNuevo);
                Console.WriteLine("------------------------------------------");

                // mapear el estado para el pase
                EstadoPase enumEstadoNuevo = EstadoPase.PENDIENTE.ToString() == estadoNuevo ? EstadoPase.PENDIENTE
                    : EstadoPase.APROBADO.ToString() == estadoNuevo ? EstadoPase.APROBADO
                    : EstadoPase.PREAPROBADO.ToString() == estadoNuevo ? EstadoPase.PREAPROBADO
                    : EstadoPase.FINALIZADO.ToString() == estadoNuevo ? EstadoPase.FINALIZADO
                    : EstadoPase.RECHAZADO;

                currentPase.Estado = enumEstadoNuevo;
                currentPase.Observacion = request.Observacion;

                var result = await this._context.SaveChangesAsync();
                if (result > 0)
                    return Unit.Value;

                throw new Exception("No se guardo el nuevo estado para el pase seleccionado.");
            }
        }
    }
}