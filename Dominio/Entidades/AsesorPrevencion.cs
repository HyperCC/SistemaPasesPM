using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class AsesorPrevencion
    {
        public Guid AsesorPrevencionId { get; set; }

        // atributos de la entidad

        public string RegistroSns { get; set; }

        public string PersonaIDPersona { get; set; }

        // relacionamiento segun modelo R

        public Guid PersonaId { get; set; }

        public Guid PaseId { get; set; }

        // obtener los modelos relacionados 

        public Persona PersonaRel { get; set; }
        public Pase PaseRel { get; set; }
    }
}
