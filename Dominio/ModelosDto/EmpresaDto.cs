using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    public class EmpresaDto
    {
        public Guid EmpresaId { get; set; }
        public string Nombre { get; set; }
        public string Rut { get; set; }

        public ICollection<UsuarioDto> UsuariosDtoRel { get; set; }
        public ICollection<PaseDto> PasesDtoRel { get; set; }
    }
}
