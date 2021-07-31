using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class RegistroPersona
    {
        public Guid RegistroPersonaId { get; set; }

        // atributos de la entidad
        public DateTime FechaRegistro { get; set; }

        // relacionamiento segun modelo R
        public Guid DocumentoId { get; set; }

        // obtener los modelos relacionados 
        public Documento DocumentoRel { get; set; }
    }
}