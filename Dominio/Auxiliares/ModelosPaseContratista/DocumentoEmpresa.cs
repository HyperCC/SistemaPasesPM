using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.ModelosPaseContratista
{
    /// <summary>
    /// Documento completo relacionado a la empresa solicitante
    /// </summary>
    public class DocumentoEmpresa
    {
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public bool Obligatoriedad { get; set; }
        public string FechaVencimiento { get; set; }
        public string Extension { get; set; }
    }
}
