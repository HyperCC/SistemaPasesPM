using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class NombrePersona
    {
        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }
        public Guid NombreId { get; set; }

        // obtener los modelos relacionados 
        public Persona PersonaRel { get; set; }
        public Nombre NombreRel { get; set; }
    }
}