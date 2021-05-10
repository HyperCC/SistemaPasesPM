using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    public class UsuarioDto
    {
        public Guid UsuarioId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public PersonaDto PersonaDtoRel { get; set; }
        public EmpresaDto EmpresaDtoRel { get; set; }
        public ICollection<PaseDto> PasesDtoRel { get; set; }
    }
}
