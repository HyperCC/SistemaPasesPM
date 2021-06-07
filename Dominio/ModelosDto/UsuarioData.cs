using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto
{
    /// <summary>
    /// Datos a retornar para el usuario en sesion
    /// </summary>
    public class UsuarioData
    {
        public string Rol { get; set; }
        public string NombreCompleto { get; set; }
        public string Rut { get; set; }
        public string NombreEmpresa { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}