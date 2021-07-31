using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class PasePersona
    {
        // relacionamiento segun modelo R
        public Guid PaseId { get; set; }
        public Guid PersonaId { get; set; }

        // obtener los modelos relacionados 
        public Pase PaseRel { get; set; }
        public Persona PersonaRel { get; set; }
    }
}