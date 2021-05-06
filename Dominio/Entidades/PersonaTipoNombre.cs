using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class PersonaTipoNombre
    {
        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }
        public Guid TipoNombreId { get; set; }

        // obtener los modelos relacionados 
        public Persona PersonaRel { get; set; }
        public TipoNombre TipoNombreRel { get; set; }
    }
}