using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class PersonaExterna
    {
        public Guid PersonaExternaId { get; set; }

        // atributos de la entidad
        public string Nacionalidad { get; set; }

        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }

        // obtener los modelos relacionados 
        public ICollection<Persona> PersonasRel { get; set; }
    }
}