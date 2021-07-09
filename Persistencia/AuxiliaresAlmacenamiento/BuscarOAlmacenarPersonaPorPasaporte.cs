using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// Buscar una persona por su pasaporte y agregarla si no existe
    /// </summary>
    public static class BuscarOAlmacenarPersonaPorPasaporte
    {
        public static async Task<Persona> BuscarOAgregarPersonaPorPasaporteAsync(SistemaPasesContext context,
            string pasaporte,
            string nombres,
            string apellidos)
        {
            // buscar si la persona existe
            var buscarPersona = await context.Persona
                .FirstOrDefaultAsync(p => p.Pasaporte == pasaporte);

            if (buscarPersona == null)
            {
                // si no existe la persona se generar una nueva
                buscarPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Pasaporte = pasaporte
                };
                await context.Persona.AddAsync(buscarPersona);

                // agregar los nombres respectivos de la persona
                await AlmacenarNombres.AgregarNombres(nombres,
                    apellidos,
                    context,
                    buscarPersona.PersonaId);
            }

            // siempre se retorna la persona encontrada o una nueva
            return buscarPersona;
        }
    }
}
