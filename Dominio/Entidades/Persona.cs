using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Persona
    {
        public Guid PersonaId { get; set; }

        // atributos de la entidad
        public string Rut { get; set; }

        // obtener los modelos relacionados 
        public ICollection<NombrePersona> NombresRel { get; set; }
        public ICollection<ApellidoPersona> ApellidosRel { get; set; }

        public Usuario UsuarioRel { get; set; }
        public PersonaExterna PersonaExternaRel { get; set; }
        public AsesorPrevencion AsesorPrevencionRel { get; set; }
    }
}