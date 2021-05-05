using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Pase
    {
        public Guid PaseId { get; set; }

        // atributos de la entidad
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string Area { get; set; }

        // relacionamiento segun modelo R
        public Guid EmpresaId { get; set; }
        public Guid UsuarioId { get; set; }

        // obtener los modelos relacionados 
        public AsesorPrevencion AsesorPrevencionRel { get; set; }
        public Empresa EmpresaRel { get; set; }
        public Usuario UsuarioRel { get; set; }
        public ICollection<PasePersonaExterna> PersonaExternasRel { get; set; }
        public ICollection<Documento> DocumentosRel { get; set; }
    }
}