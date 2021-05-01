using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Seeders
{
    public class DatabaseSeeder
    {
        public static void IniciarSeederGlobal(SistemaPasesContext context, UserManager<Usuario> usuarioManager)
        {
            UsuarioSeeder.InsertarData(context, usuarioManager);
        }
    }
}
