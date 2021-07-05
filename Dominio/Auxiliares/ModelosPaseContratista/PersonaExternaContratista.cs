using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.ModelosPaseContratista
{
    public class PersonaExternaContratista
    {
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Pasaporte { get; set; }
        public string Rut { get; set; }
        public string Nacionalidad { get; set; }

        public ICollection<DocumentoPersonaContratista> DocumentosPersona { get; set; }
    }
}
