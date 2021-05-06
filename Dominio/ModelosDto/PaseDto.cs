using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    public class PaseDto
    {
        public Guid PaseId { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Motivo { get; set; }
        public string Area { get; set; }

        public UsuarioDto UsuarioDtoRel { get; set; }
        public EmpresaDto EmpresaDtoRel { get; set; }
    }
}
