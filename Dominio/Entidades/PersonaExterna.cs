using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class PersonaExterna
    {
        public Guid PersonaExternaId { get; set; }

        // atributos de la entidad

        public string nacionalidad { get; set; }

        public string pasaporte { get; set; }

        // relacionamiento segun modelo R

        public Guid PersonaId { get; set; }

        // obtener los modelos relacionados 

        public Persona PersonaRel { get; set; }
        //public PasePersonaExterna PasePersonaExternaRel { get; set; }
        public ICollection<PasePersonaExterna> PasesRel { get; set; }


    }
}
