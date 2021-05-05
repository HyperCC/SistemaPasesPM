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
        public string Pasaporte { get; set; }

        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }

        // obtener los modelos relacionados 
        public Persona PersonaRel { get; set; }
        public ICollection<RegistroPersona> RegistroPersonasRel { get; set; }
        public ICollection<AnexoContrato> AnexoContratosRel { get; set; }
        public ICollection<ExamenesCompetencia> ExamenesCompetenciasRel { get; set; }
        public ICollection<PasePersonaExterna> PasesRel { get; set; }
    }
}