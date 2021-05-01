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
            if (context.Usuario.Any())
            {
                // nombres
                var nombre1 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "camilo",
                    Tipo = TipoNombre.TipoIdentificador.NOMBRE
                };
                var nombre2 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "andres",
                    Tipo = TipoNombre.TipoIdentificador.NOMBRE
                };

                // apellidos
                var apellido1 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "moraga",
                    Tipo = TipoNombre.TipoIdentificador.APELLIDO
                };
                var apellido2 = new TipoNombre
                {
                    TipoNombreId = new Guid(),
                    Nombre = "martinez",
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

                var result = await context.SaveChangesAsync();
                Console.WriteLine((result > 0) ? "TODO FUE OK" : "NADA FUNCIONO");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    UsuarioId = new Guid(),
                    Email = "admin@gmail.com",
                    PersonaId = nuevaPersona.PersonaId
                };
                // agregar el usuario y generar el hash de al clave
                await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

            }
        }
    }
}
