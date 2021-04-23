using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class TipoNombre
    {
        public Guid TipoNombreId { get; set; }

        // atributos de la entidad
        public string Nombre { get; set; }
        public string Tipo { get; set; } // nombre o apellido
        public string Posicion { get; set; } // primero, segundo, tercero.

        // obtener los modelos relacionados 
        public ICollection<PersonaTipoNombre> PersonasRel { get; set; }
    }
}
