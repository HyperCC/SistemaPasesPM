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
        public string Extension { get; set; }

        public DateTime FechaVencimiento { get; set; }

        // relacionamiento segun modelo R
        public Guid TipoDocumentoId { get; set; }
        public Guid? PaseId { get; set; }
        public Guid? Persona { get; set; }
        public Guid? EmpresaId { get; set; } // TODO: agregar relacion en modelo R

        // obtener los modelos relacionados 
        public TipoDocumento TipoDocumentoRel { get; set; }
        public Pase PaseRel { get; set; }
        public AnexoContrato AnexoContratoRel { get; set; }
        public RegistroPersona RegistroPersonaRel { get; set; }
        public Persona PersonasRel { get; set; }
        public Empresa EmpresaRel { get; set; }
    }
}