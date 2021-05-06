using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    public class PersonaExternaDto
    {
        public Guid PersonaExternaId { get; set; }
        public string Nacionalidad { get; set; }
        public string Pasaporte { get; set; }

    }
}
