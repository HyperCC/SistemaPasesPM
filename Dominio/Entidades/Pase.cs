using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    /// <summary>
    /// tipos de pase existentes
    /// </summary>
    public enum TipoPase
    {
        VISITA, CONTRATISTA, PROVEEDOR, USOMUELLE, TRIPULANTE
    }

    /// <summary>
    /// estado actual del pase
    /// </summary>
    public enum EstadoPase
    {
        APROBADO, FINALIZADO, PENDIENTE, RECHAZADO
    }

    /// <summary>
    /// modelo para entidad Pase
    /// </summary>
    public class Pase
    {
        public Guid PaseId { get; set; }

        // atributos de la entidad
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public TipoPase Tipo { get; set; }
        public EstadoPase Estado { get; set; }
        public string Motivo { get; set; }
        public string Area { get; set; }

        // relacionamiento segun modelo R
        public Guid EmpresaId { get; set; }
        public string UsuarioId { get; set; }

        // obtener los modelos relacionados 
        public AsesorPrevencion AsesorPrevencionRel { get; set; }
        public Empresa EmpresaRel { get; set; }
        public Usuario UsuarioRel { get; set; }
        public ICollection<PasePersonaExterna> PersonaExternasRel { get; set; }
        public ICollection<Documento> DocumentosRel { get; set; }
    }
}