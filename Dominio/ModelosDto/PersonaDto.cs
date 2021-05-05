using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    public class PersonaDto
    {
        public Guid PersonaId { get; set; }
        public string Rut { get; set; }

        public ICollection<TipoNombreDto> TipoNombres { get; set; }
    }
}
