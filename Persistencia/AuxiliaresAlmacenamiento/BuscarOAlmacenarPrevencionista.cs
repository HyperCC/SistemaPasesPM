using Dominio.Auxiliares.ModelosPaseContratista;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    public static class BuscarOAlmacenarPrevencionista
    {
        public static async Task<AsesorPrevencion> BuscarOAgregarPrevencionista(AsesorDePrevencionRiesgos prevencionista
            , SistemaPasesContext context)
        {
            // buscar si existe el asesor con el registro SNS indicado
            var asesorPrevencion = await context.AsesorPrevencion
                .FirstOrDefaultAsync(
                    p => p.RegistroSns == prevencionista.RegistroSNS
                );

            if (asesorPrevencion == null)
            {
                // encontrar a la persona correspondiente
                var persona = await BuscarOAlmacenarPersona.BuscarOAgregarPersona(context
                    , prevencionista.Rut
                    , prevencionista.Nombres
                    , prevencionista.Apellidos);

                // agregar y registrar el prevencionista si no xiste
                asesorPrevencion = new AsesorPrevencion
                {
                    AsesorPrevencionId = new Guid(),
                    RegistroSns = prevencionista.RegistroSNS,
                    PersonaId = persona.PersonaId,
                };
                await context.AsesorPrevencion.AddAsync(asesorPrevencion);
            }

            return asesorPrevencion;
        }
    }
}
