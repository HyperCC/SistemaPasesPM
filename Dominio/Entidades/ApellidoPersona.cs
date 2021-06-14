using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class ApellidoPersona
    {
        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }
        public Guid ApellidoId { get; set; }

        // obtener los modelos relacionados 
        public Persona PersonaRel { get; set; }
        public Apellido ApellidoRel { get; set; }
    }
}