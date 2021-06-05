using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Seeders
{
    /// <summary>
    /// Roles iniciales a crear para la base de datos
    /// </summary>
    public class RoleSeeder
    {
        /// <summary>
        /// generar los registros de roles
        /// </summary>
        /// <param name="roleManager">servicio de roles</param>
        /// <returns></returns>
        public static async Task InsertarData(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "ADMIN", "SOLICITANTE", "HSEQ", "JEFEO_PERACIONES", "CONTACTO", "OPIP" };

            // agregar los roles siempre que no existan
            foreach (var rol in roles)
            {
                var searchRol = await roleManager.FindByNameAsync(rol);

                if (searchRol == null)
                    await roleManager.CreateAsync(new IdentityRole(rol));

                else
                    Console.WriteLine($"YA EXISTE EL ROL {rol}");
            }
        }
    }
}