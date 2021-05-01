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
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
