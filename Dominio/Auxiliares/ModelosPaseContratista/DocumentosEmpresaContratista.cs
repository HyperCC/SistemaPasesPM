using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.ModelosPaseContratista
{
    public class DocumentosEmpresaContratista
    {
        public IFormFile Documento { get; set; }
        public string TipoDocumento { get; set; }
        public bool Obligatoriedad { get; set; }
        public string FechaVencimiento { get; set; }//Opcional?
    }
}
