using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    /// <summary>
    /// Datos asociados a el perfil de un usaurio comun
    /// </summary>
    public class PasesCuentaUsuario
    {
        public ICollection<PasePerfil> PasesRel { get; set; }
    }
}
