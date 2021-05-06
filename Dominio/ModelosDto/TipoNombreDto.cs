using System;
using System.Collections.Generic;
using System.Text;
using static Dominio.Entidades.TipoNombre;

namespace Dominio.ModelosDto
{
    public class TipoNombreDto
    {
        public Guid TipoNombreId { get; set; }
        public string Nombre { get; set; }
        public TipoIdentificador Tipo { get; set; } // nombre o apellido
        public int Posicion { get; set; } // primero, segundo, tercero.
        public ICollection<PersonaDto> PersonasDtoRel { get; set; }
    }
}
