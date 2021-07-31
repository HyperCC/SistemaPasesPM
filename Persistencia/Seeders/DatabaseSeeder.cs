using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Seeders
{
    /// <summary>
    /// Manejador principal de los seeder a ejecutar
    /// </summary>
    public class DatabaseSeeder
    {
        public static void IniciarSeederGlobal(SistemaPasesContext context, UserManager<Usuario> usuarioManager, RoleManager<IdentityRole> roleManager)
        {
            // eleccion de los seeder a emplear
            RoleSeeder.InsertarData(roleManager).Wait();
            UsuarioSeeder.InsertarData(context, usuarioManager).Wait();
            TipoDocumentoSeeder.InsertarData(context).Wait();
        }
    }
}
