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
    /// Seeder para insertar Usuarios y entidades complementarias iniciales.
    /// </summary>
    public class UsuarioSeeder
    {

        /// <summary>
        /// Poblacion de Usuarios y entidades relacionadas.
        /// </summary>
        /// <param name="context">base de datos completa</param>
        /// <param name="usuarioManager">posible usuario actual</param>
        /// <returns>creacion de un nuevo Usuario</returns>
        public static void InsertarData(SistemaPasesContext context)
        {
            // en caso de no haber usuarios en la DB
            if (!context.Persona.Any())
            {
                context.Persona.AddRange(
                    new Persona
                    {
                        Rut = "11222333-4"
                    },

                    new Persona
                    {
                        Rut = "55666777-8"
                    },

                     new Persona
                     {
                         Rut = "99111222-3"
                     },

                      new Persona
                      {
                          Rut = "4555666-7"
                      }
                );

                context.SaveChanges();

                /*
                // creacion de nuevo usuario para poblar la DB
                var usuario = new Usuario
                {
                    Correo = "kjkjkjk",
                    Contrasena = "kmkmk",
                    UserName = "domainB",
                    Email = "belchello@gmail.com"
                };

                await usuarioManager.CreateAsync(usuario, "P@ssw0rd");*/
            }
        }
    }
}
