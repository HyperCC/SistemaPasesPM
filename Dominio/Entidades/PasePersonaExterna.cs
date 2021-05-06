using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class PasePersonaExterna
    {
        // relacionamiento segun modelo R
        public Guid PaseId { get; set; }
        public Guid PersonaExternaId { get; set; }

        // obtener los modelos relacionados 
        public Pase PaseRel { get; set; }
        public PersonaExterna PersonaExternaRel { get; set; }
    }
}