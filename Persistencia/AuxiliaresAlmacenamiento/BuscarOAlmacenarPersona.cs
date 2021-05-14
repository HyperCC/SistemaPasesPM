using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    public static class BuscarOAlmacenarPersona
    {
        public static async Task<Persona> BuscarOAgregarPersona(SistemaPasesContext context,
            string rut,
            string nombres,
            string apellidos)
        {
            // buscar si la persona existe
            var buscarPersona = await context.Persona.FirstOrDefaultAsync(p => p.Rut == rut);

            if (buscarPersona == null)
            {
                // si no existe la persona se generar una nueva
                buscarPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = rut
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