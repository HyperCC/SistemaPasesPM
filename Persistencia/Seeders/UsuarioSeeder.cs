using Dominio.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia.AuxiliaresAlmacenamiento;
using System;
using System.Linq;
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
            // USUARIO AMIN 
            if (await usuarioManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "1.222.333-4"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agregar y relacionar nombres y apellidos
                await AlmacenarNombres.AgregarNombres("camilo andres"
                   , "moraga martinez"
                   , context
                   , nuevaPersona.PersonaId);

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

                // agregar rol correspondiente al admin
                await usuarioManager.AddToRoleAsync(nuevoUsuario, "ADMIN");
            }

            //USUARIO SOLICITANTE
            if (await usuarioManager.FindByEmailAsync("usuario@gmail.com") == null)
            {
                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "55444333-6"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agregar y relacionar nombres y apellidos
                await AlmacenarNombres.AgregarNombres("cristian elias"
                   , "vergara vargas"
                   , context
                   , nuevaPersona.PersonaId);

                var result = await context.SaveChangesAsync();
                Empresa currentEmpresa = await context.Empresa.FirstOrDefaultAsync(x => x.Nombre == "PMEJ");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    Email = "usuario@gmail.com",
                    UserName = "usuario@gmail.com",
                    PersonaId = nuevaPersona.PersonaId,
                    EmpresaId = currentEmpresa.EmpresaId,
                    NoPerteneceEmpresa = true
                };

                // agregar el usuario y generar el hash de al clave
                var resultUsuario = await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

                // agregar rol correspondiente al admin
                await usuarioManager.AddToRoleAsync(nuevoUsuario, "SOLICITANTE");

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((resultUsuario.Succeeded) ? "CREAR EL USUARIO FUE OK" : "CREAR EL USUARIO NO FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }

            //USUARIO CONTACTO
            if (await usuarioManager.FindByEmailAsync("contacto@gmail.com") == null)
            {
                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "4222333-1"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agregar y relacionar nombres y apellidos
                await AlmacenarNombres.AgregarNombres("daniel tomas"
                   , "gutierrez alanes"
                   , context
                   , nuevaPersona.PersonaId);

                var result = await context.SaveChangesAsync();
                Empresa currentEmpresa = await context.Empresa.FirstOrDefaultAsync(x => x.Nombre == "PMEJ");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    Email = "contacto@gmail.com",
                    UserName = "contacto@gmail.com",
                    PersonaId = nuevaPersona.PersonaId,
                    EmpresaId = currentEmpresa.EmpresaId,
                    NoPerteneceEmpresa = true
                };

                // agregar el usuario y generar el hash de al clave
                var resultUsuario = await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

                // agregar rol correspondiente al admin
                await usuarioManager.AddToRoleAsync(nuevoUsuario, "CONTACTO");

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((resultUsuario.Succeeded) ? "CREAR EL USUARIO FUE OK" : "CREAR EL USUARIO NO FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }

            //USUARIO HSEQ
            if (await usuarioManager.FindByEmailAsync("hseq@gmail.com") == null)
            {
                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "4999333-k"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agregar y relacionar nombres y apellidos
                await AlmacenarNombres.AgregarNombres("romina andrea"
                   , "alcallaga martinez"
                   , context
                   , nuevaPersona.PersonaId);

                var result = await context.SaveChangesAsync();
                Empresa currentEmpresa = await context.Empresa.FirstOrDefaultAsync(x => x.Nombre == "PMEJ");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    Email = "hseq@gmail.com",
                    UserName = "hseq@gmail.com",
                    PersonaId = nuevaPersona.PersonaId,
                    EmpresaId = currentEmpresa.EmpresaId,
                    NoPerteneceEmpresa = true
                };

                // agregar el usuario y generar el hash de al clave
                var resultUsuario = await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

                // agregar rol correspondiente al admin
                await usuarioManager.AddToRoleAsync(nuevoUsuario, "HSEQ");

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((resultUsuario.Succeeded) ? "CREAR EL USUARIO FUE OK" : "CREAR EL USUARIO NO FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }

            //USUARIO OPIP
            if (await usuarioManager.FindByEmailAsync("opip@gmail.com") == null)
            {
                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "88555333-1"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agregar y relacionar nombres y apellidos
                await AlmacenarNombres.AgregarNombres("manuel andres"
                   , "tapia barrios"
                   , context
                   , nuevaPersona.PersonaId);

                var result = await context.SaveChangesAsync();
                Empresa currentEmpresa = await context.Empresa.FirstOrDefaultAsync(x => x.Nombre == "PMEJ");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    Email = "opip@gmail.com",
                    UserName = "opip@gmail.com",
                    PersonaId = nuevaPersona.PersonaId,
                    EmpresaId = currentEmpresa.EmpresaId,
                    NoPerteneceEmpresa = true
                };

                // agregar el usuario y generar el hash de al clave
                var resultUsuario = await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

                // agregar rol correspondiente al admin
                await usuarioManager.AddToRoleAsync(nuevoUsuario, "OPIP");

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((resultUsuario.Succeeded) ? "CREAR EL USUARIO FUE OK" : "CREAR EL USUARIO NO FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }

            //USUARIO JEFE OPERACIONES
            if (await usuarioManager.FindByEmailAsync("jo@gmail.com") == null)
            {
                // datos de la persona inicial
                var nuevaPersona = new Persona
                {
                    PersonaId = new Guid(),
                    Rut = "66222333-9"
                };
                await context.Persona.AddAsync(nuevaPersona);

                // agregar y relacionar nombres y apellidos
                await AlmacenarNombres.AgregarNombres("elias danilo"
                   , "alcallaga martinez"
                   , context
                   , nuevaPersona.PersonaId);

                var result = await context.SaveChangesAsync();
                Empresa currentEmpresa = await context.Empresa.FirstOrDefaultAsync(x => x.Nombre == "PMEJ");

                // usuario principal
                var nuevoUsuario = new Usuario
                {
                    Email = "jo@gmail.com",
                    UserName = "jo@gmail.com",
                    PersonaId = nuevaPersona.PersonaId,
                    EmpresaId = currentEmpresa.EmpresaId,
                    NoPerteneceEmpresa = true
                };

                // agregar el usuario y generar el hash de al clave
                var resultUsuario = await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");

                // agregar rol correspondiente al admin
                await usuarioManager.AddToRoleAsync(nuevoUsuario, "JEFE_OPERACIONES");

                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine((resultUsuario.Succeeded) ? "CREAR EL USUARIO FUE OK" : "CREAR EL USUARIO NO FUNCIONO");
                Console.WriteLine("---------------------------------------------------------------------------------");
            }


            var tipoDocumento1 = new TipoDocumento
            {
                TipoDocumentoId = new Guid(),
                Titulo = "Induccion"
            };

            await context.TipoDocumento.AddAsync(tipoDocumento1);
        }
    }
}