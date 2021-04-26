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
        public TipoIdentificador Tipo { get; set; } // nombre o apellido
        public int Posicion { get; set; } // primero, segundo, tercero.

        public enum TipoIdentificador
        {
            NOMBRE, APELLIDO
        }

        // obtener los modelos relacionados 
        public ICollection<PersonaTipoNombre> PersonasRel { get; set; }
    }
}
