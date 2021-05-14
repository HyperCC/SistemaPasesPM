using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.Pases
{
    /// <summary>
    /// modelo para recibir documentos para la empresa por el request
    /// </summary>
    public class DocumentoEmpresaContratistaRequest
    {
        //TODO: UNA EMPRESA DEBE TENER ASOCIADOS DOCUMENTOS 
        // pestaña general y legal
        public string CronogramaTrabajo { get; set; }
        public string CertificadoMutualidad { get; set; }
        public string CertificadoAccidentabilidad { get; set; }
        public string ReglamentoInterno { get; set; }

        // pestaña asesor de prevencion
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rut { get; set; }
        public string ReistroSNS { get; set; }

        // pestaña riesgos
        public string MatrizRiesgos { get; set; }
        public ICollection<string> ProcedimientosTrabajoSeguro { get; set; }
        public string ProgramaPrevencionRiesgos { get; set; }
        public ICollection<string> HDSSustanciasPeligrosas { get; set; }
    }
}