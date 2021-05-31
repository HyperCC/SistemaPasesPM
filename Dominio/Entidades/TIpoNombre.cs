using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{

    // tipos de nombre
    public enum TipoIdentificador
    {
        NOMBRE, APELLIDO
    }

    public class TipoNombre
    {
        public Guid TipoNombreId { get; set; }

        // atributos de la entidad
        public string Nombre { get; set; }
        public TipoIdentificador Tipo { get; set; } // nombre o apellido
        public int Posicion { get; set; } // primero, segundo, tercero.

        // obtener los modelos relacionados 
        public ICollection<PersonaTipoNombre> PersonasRel { get; set; }
    }
}