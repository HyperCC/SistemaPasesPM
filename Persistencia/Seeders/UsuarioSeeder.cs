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
        public static async Task InsertarData(SistemaPasesContext context, UserManager<Usuario> usuarioManager)
        {
            // de no haber usaurios en la base de datos se crea el inicial 
            if (await usuarioManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                Console.WriteLine("NO HAY USUARIO ADMIN EN LA BASE DE DATOS..");
                Guid userId = new Guid();
                Console.WriteLine($"EL id del usuario sera {userId}");
                // nombres
                var nombre1 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "camilo",
                    Posicion = 1,
                    Tipo = TipoNombre.TipoIdentificador.NOMBRE
                };
                var nombre2 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "andres",
                    Posicion = 2,
                    Tipo = TipoNombre.TipoIdentificador.NOMBRE
                };

                // apellidos
                var apellido1 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "moraga",
                    Posicion = 1,
                    Tipo = TipoNombre.TipoIdentificador.APELLIDO
                };
                var apellido2 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "martinez",
                    Posicion = 2,
                    Tipo = TipoNombre.TipoIdentificador.APELLIDO
                };
                // agregar los respectivos nombres
                await context.TipoNombre.AddRangeAsync(nombre1, nombre2, apellido1, apellido2);

                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "1.222.333-4"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agrupar todas las instancias de PersonaTipoNombre correspondientes
                Guid[] idsNombres = {nombre1.TipoNombreId,
                    nombre2.TipoNombreId,
                    apellido1.TipoNombreId,
                    apellido2.TipoNombreId };

                // generar las instancias 
                foreach (Guid idnombre in idsNombres)
                {
                    await context.PersonaTipoNombre.AddAsync(new PersonaTipoNombre
                    {
                        PersonaId = nuevaPersona.PersonaId,
                        TipoNombreId = idnombre
                    });
                }

                var nuevaEmpresa = new Empresa
                {
                    EmpresaId = new Guid(),
                    Nombre = "PMEJ",
                    Rut = "1.222.333-0"
                };
                await context.Empresa.AddAsync(nuevaEmpresa);

                var result = await context.SaveChangesAsync();
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((result > 0) ? "TODO FUE OK" : "NADA FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    UId = userId,
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    PersonaId = nuevaPersona.PersonaId,
                    EmpresaId = nuevaEmpresa.EmpresaId,
                    NoPerteneceEmpresa = true
                };

                // agregar el usuario y generar el hash de al clave
                var resultUsuario = await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((resultUsuario.Succeeded) ? "CREAR EL USUARIO FUE OK" : "CREAR EL USUARIO NO FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }
            else
                Console.WriteLine("YA HAY USUARIO ADMIN EN LA BASE DE DATOS..");
        }
    }
}
