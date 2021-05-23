using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    public static class BuscarOAlmacenarPersonaExterna
    {
        public static async Task<PersonaExterna> BuscarOAgregarPersonaExterna(SistemaPasesContext context,
            Guid personaId,
            Guid paseId,
            string pasaporte,
            string nacionalidad)
        {

            // buscar por la persona externa
            PersonaExterna buscarPersonaExt = await context.PersonaExterna
                .FirstOrDefaultAsync(p => p.PersonaId == personaId);

            if (buscarPersonaExt == null)
            {
                // si no existe la persona externa se crea y se almacena
                buscarPersonaExt = new PersonaExterna
                {
                    PersonaExternaId = new Guid(),
                    Nacionalidad = nacionalidad,
                    Pasaporte = pasaporte,
                    PersonaId = personaId
                };
                await context.PersonaExterna.AddAsync(buscarPersonaExt);
            }

            // una vez encontrada/creada la persona externa se crea la relacion
            PasePersonaExterna nuevaRelacion = new PasePersonaExterna
            {
                PaseId = paseId,
                PersonaExternaId = buscarPersonaExt.PersonaExternaId,
            };
            await context.PasePersonaExterna.AddAsync(nuevaRelacion);

            // siempre se retorna la persona externa encontrada o una nueva
            return buscarPersonaExt;
        }
    }
}