using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto.ModelosParaPerfil
{
    /// <summary>
    /// Modelo con los pases asignados a un 
    /// </summary>
    public class CuentaAprobadorData
    {
        public ICollection<PasePerfil> PasesAll { get; set; }
    }
}