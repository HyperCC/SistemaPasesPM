using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var existenRoles = roleManager.Roles.Any();
            if (!existenRoles)
            {
                // todos los roles solicitados hasta el momento 
                string[] roles = {
                    "ADMIN",
                    "GUARDIA",
                    "SOLICITANTE",
                    "CONTACTO",
                    "HSEQ",
                    "JEFE_OPERACIONES",
                    "OPIP"
                };

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
            else
                // TODO: estos mensajes deben ser por Logger
                Console.WriteLine("YA EXISTEN LOS ROLES INICIALES");
        }
    }
}