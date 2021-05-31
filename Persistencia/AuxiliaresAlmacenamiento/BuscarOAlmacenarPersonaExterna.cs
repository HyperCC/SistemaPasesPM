using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// clase auxiliar para buscar o almacenar personas externas en los pases
    /// </summary>
    public static class BuscarOAlmacenarPersonaExterna
    {

        /// <summary>
        /// agregar las personas externas recibidas a la base de datos
        /// </summary>
        /// <param name="_context"></param>
        /// <param name="_personaId"></param>
        /// <param name="_paseId"></param>
        /// <param name="pasaporte"></param>
        /// <param name="nacionalidad"></param>
        /// <returns></returns>
        public static async Task<PersonaExterna> BuscarOAgregarPersonaExterna(SistemaPasesContext _context,
            Guid _personaId,
            Guid _paseId,
            string pasaporte,
            string nacionalidad)
        {

            // buscar por la persona externa
            PersonaExterna buscarPersonaExt = await _context.PersonaExterna
                .FirstOrDefaultAsync(p => p.PersonaId == _personaId);

            if (buscarPersonaExt == null)
            {
                // si no existe la persona externa se crea y se almacena
                buscarPersonaExt = new PersonaExterna
                {
                    PersonaExternaId = new Guid(),
                    Nacionalidad = nacionalidad,
                    Pasaporte = pasaporte,
                    PersonaId = _personaId
                };
                await _context.PersonaExterna.AddAsync(buscarPersonaExt);
            }

            // una vez encontrada/creada la persona externa se crea la relacion
            PasePersonaExterna nuevaRelacion = new PasePersonaExterna
            {
                PaseId = _paseId,
                PersonaExternaId = buscarPersonaExt.PersonaExternaId,
            };
            await _context.PasePersonaExterna.AddAsync(nuevaRelacion);

            // siempre se retorna la persona externa encontrada o una nueva
            return buscarPersonaExt;
        }
    }
}