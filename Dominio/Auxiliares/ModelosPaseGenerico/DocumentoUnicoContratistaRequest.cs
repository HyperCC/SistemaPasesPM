using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.Pases
{
    /// <summary>
    /// modelo para almacenar un unico documento
    /// </summary>
    public class DocumentoUnicoContratistaRequest
    {
        public string Titulo { get; set; }
        public string RutaGuardado { get; set; }
        public string FechaVencimiento { get; set; }
    }
}