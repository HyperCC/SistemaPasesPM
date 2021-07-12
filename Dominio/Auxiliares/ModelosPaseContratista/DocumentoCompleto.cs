using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.ModelosPaseContratista
{
    /// <summary>
    /// Documento completo en base 64 con los campos opcionales 
    /// </summary>
    public class DocumentoCompleto
    {
        public string DocumentoBase64 { get; set; }
        public string Extension { get; set; }
        public string FechaVencimiento { get; set; }
        public string FechaRegistro { get; set; }
        public string Descripcion { get; set; }
        public string TituloDocumento { get; set; }
    }
}