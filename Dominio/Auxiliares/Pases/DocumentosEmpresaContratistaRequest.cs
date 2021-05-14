using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.Pases
{
    /// <summary>
    /// modelo para recibir documentos para la empresa por el request
    /// </summary>
    public class DocumentosEmpresaContratistaRequest
    {
        //TODO: UNA EMPRESA DEBE TENER ASOCIADOS DOCUMENTOS 

        // pestaña asesor de prevencion
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Rut { get; set; }
        public string ReistroSNS { get; set; }

        // demas pestañas con documentos
        public ICollection<DocumentoUnicoContratistaRequest> Documentos { get; set; }
    }
}