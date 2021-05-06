using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Documento
    {
        // atributos de la entidad
        public Guid DocumentoId { get; set; }
        public string RutaDocumento { get; set; }

        public DateTime FechaCaducidad { get; set; }

        // relacionamiento segun modelo R
        public Guid TipoDocumentoId { get; set; }
        public Guid PaseId { get; set; }

        // obtener los modelos relacionados 
        public TipoDocumento TipoDocumentoRel { get; set; }
        public Pase PaseRel { get; set; }
        public ExamenesCompetencia ExamenesCompetenciaRel { get; set; }
        public AnexoContrato AnexoContratoRel { get; set; }
        public RegistroPersona RegistroPersonaRel { get; set; }
    }
}