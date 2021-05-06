using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class ExamenesCompetencia
    {
        public Guid ExamenesCompetenciaId { get; set; }

        // atributos de la entidad
        public DateTime FechaVencimiento { get; set; }

        // relacionamiento segun modelo R
        public Guid DocumentoId { get; set; }
        public Guid PersonaExternaId { get; set; }

        // obtener los modelos relacionados 
        public Documento DocumentoRel { get; set; }
        public PersonaExterna PersonaExternaRel { get; set; }
    }
}