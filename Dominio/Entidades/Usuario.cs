using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Dominio.Entidades
{
    public class Usuario : IdentityUser // herencia para login
    {
        public Guid UsuarioId { get; set; }

        // atributos de la entidad
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public bool Captcha { get; set; } = false;
        public bool NoPerteneceEmpresa { get; set; } = false;

        // relacionamiento segun modelo R
        public Guid PersonaId { get; set; }
        public Guid EmpresaId { get; set; }

        // obtener los modelos relacionados 
        public Persona PersonaRel { get; set; }
        public ICollection<Rol> RolsRel { get; set; }
        public Empresa EmpresaRel { get; set; }
    }
}