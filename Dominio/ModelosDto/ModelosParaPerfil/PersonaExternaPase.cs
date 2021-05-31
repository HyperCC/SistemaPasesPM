using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto.ModelosParaPerfil
{
    /// <summary>
    /// Persona externa adjuntada a un pase
    /// </summary>
    public class PersonaExternaPase
    {
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Rut { get; set; }
        public string Pasaporte { get; set; }
        public string Nacionalidad { get; set; }
    }
}
