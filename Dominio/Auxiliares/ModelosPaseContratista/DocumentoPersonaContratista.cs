using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.ModelosPaseContratista
{
    public class DocumentoPersonaContratista
    {
        public string Documento { get; set; }
        public string TipoDocumento { get; set; }
        public bool Obligatoriedad { get; set; }
        public string Extension { get; set; }
        public string FechaVencimiento { get; set; }// opcional
        public string FechaRegistro { get; set; }// opcional
        public string Descripcion { get; set; }// opcional
    }
}
