using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    public class CuentaUsuarioData
    {
        public string NombreCompleto { get; set; }
        public string Rut { get; set; }
        public string NombreEmpresa { get; set; }
        public ICollection<Pase> PasesRel { get; set; }
    }
}
