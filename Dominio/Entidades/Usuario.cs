using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public Guid UsuarioId { get; set; }

        // atributos de la entidad
        public string Correo { get; set; }
        public string Contrasena { get; set; }

        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }

        // obtener los modelos relacionados 
        public Persona PersonaRel { get; set; }
        public ICollection<Rol> RolsRel { get; set; }
    }
}
