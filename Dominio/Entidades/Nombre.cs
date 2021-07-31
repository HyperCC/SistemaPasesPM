using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Nombre
    {
        public Guid NombreId { get; set; }

        // atributos de la entidad
        public string Titulo { get; set; }
        public int Posicion { get; set; } // primero, segundo, tercero.

        // obtener los modelos relacionados 
        public ICollection<ApellidoPersona> PersonasRel { get; set; }
    }
}