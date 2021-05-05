using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Rol
    {
        public Guid RolId { get; set; }

        // atributos de la entidad
        public string Titulo { get; set; }

        // relacionamiento segun modelo R
        public Guid UsuarioId { get; set; }

        // obtener los modelos relacionados 
        public Usuario UsuarioRel { get; set; }
    }
}