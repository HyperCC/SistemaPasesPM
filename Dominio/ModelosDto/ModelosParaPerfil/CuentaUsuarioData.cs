using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    /// <summary>
    /// Datos asociados a el perfil de un usaurio comun
    /// </summary>
    public class CuentaUsuarioData
    {
        public string NombreCompleto { get; set; }
        public string Rut { get; set; }
        public string NombreEmpresa { get; set; }
        public string Rol { get; set; }
        public ICollection<PasePerfil> PasesRel { get; set; }
    }
}
