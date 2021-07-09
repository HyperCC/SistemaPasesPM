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
        /// <param name="_personaAdjunta"></param>
        /// <param name="_pase"></param>
        /// <param name="pasaporte"></param>
        /// <param name="nacionalidad"></param>
        /// <returns></returns>
        public static async Task<PersonaExterna> BuscarOAgregarPersonaExterna(SistemaPasesContext _context,
            Persona _personaAdjunta,
            Pase _pase,
            string nacionalidad)
        {

            // buscar por la persona externa (la nacionalidad)
            PersonaExterna buscarPersonaExt = await _context.PersonaExterna
                .FirstOrDefaultAsync(p => p.Nacionalidad == nacionalidad);

            if (buscarPersonaExt == null)
            {
                // si no existe la persona externa se crea y se almacena
                buscarPersonaExt = new PersonaExterna
                {
                    PersonaExternaId = new Guid(),
                    Nacionalidad = nacionalidad,
                };
                await _context.PersonaExterna.AddAsync(buscarPersonaExt);
            }

            // ya registrada o encontrada la nacionalidad se adiere a Persona correspondiente
            _personaAdjunta.PersonaExternaId = buscarPersonaExt.PersonaExternaId;

            // una vez encontrada/creada la persona externa se crea la relacion
            PasePersona nuevaRelacion = new PasePersona
            {
                PaseId = _pase.PaseId,
                PersonaId = _personaAdjunta.PersonaId,
            };
            await _context.PasePersonaExterna.AddAsync(nuevaRelacion);
            //_context.SaveChanges();

            // siempre se retorna la persona externa encontrada o una nueva
            return buscarPersonaExt;
        }
    }
}