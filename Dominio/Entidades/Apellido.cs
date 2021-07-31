using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Apellido
    {
        public Guid ApellidoId { get; set; }

        // atributos de la entidad
        public string Titulo { get; set; }
        public int Posicion { get; set; } // primero, segundo, tercero.

        // obtener los modelos relacionados 
        public ICollection<NombrePersona> PersonasRel { get; set; }
    }
}