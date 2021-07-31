using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.ModelosDto.ModelosParaPerfil
{
    // modelo para mostrar los usuarios en el perfil administrador
    public class CuentaAdminData
    {
        public ICollection<UsuarioData> UsuariosAll;
    }
}