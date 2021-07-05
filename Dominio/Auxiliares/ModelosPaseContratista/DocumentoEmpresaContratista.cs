using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.ModelosPaseContratista
{
    public class DocumentoEmpresaContratista
    {
        public IFormFile Documento { get; set; }
        public string TipoDocumento { get; set; }
        public string Obligariedad { get; set; }
        public string FechaVencimiento { get; set; }//Opcional?
    }
}
