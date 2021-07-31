using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    /// <summary>
    /// pases correspondientes a un perfil
    /// </summary>
    public class PasesUsuarioData
    {
        public ICollection<PasePerfil> PasesRel { get; set; }
    }
}
