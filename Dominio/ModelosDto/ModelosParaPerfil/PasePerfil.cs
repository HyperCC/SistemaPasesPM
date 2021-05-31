using Dominio.ModelosDto.ModelosParaPerfil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    /// <summary>
    /// pases en los perfiles de algun usuario
    /// </summary>
    public class PasePerfil
    {
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Motivo { get; set; }
        public string Area { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public ICollection<PersonaExternaPase> PersonaExternasRel { get; set; }
    }
}
