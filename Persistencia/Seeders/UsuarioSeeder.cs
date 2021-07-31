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
            var existenUsuarios = await usuarioManager.Users.AnyAsync();
            if (existenUsuarios)
            {
                Console.WriteLine("YA EXISTEN LOS USUARIOS INICIALES");
                return;
            }

            // USUARIO AMIN 
            // datos de la persona inicial
            var nuevaEmpresa = new Empresa
            {
                EmpresaId = new Guid(),
                Nombre = "PMEJ",
                Rut = "1222333-9"
            };
            await context.Empresa.AddAsync(nuevaEmpresa);

            var nuevoPersonaAdmin = new Persona
            {
                PersonaId = new Guid(),
                Rut = "1222333-4"
            };
            await context.Persona.AddAsync(nuevoPersonaAdmin);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("camilo andres"
               , "moraga martinez"
               , context
               , nuevoPersonaAdmin.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuarioAdmin = new Usuario
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                PersonaId = nuevoPersonaAdmin.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = false
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuarioAdmin, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuarioAdmin, "ADMIN");


            // USUARIO GUARDIA 
            // datos de la persona inicial
            var nuevoPersonaGuardia = new Persona
            {
                PersonaId = new Guid(),
                Rut = "44999000-3"
            };
            await context.Persona.AddAsync(nuevoPersonaGuardia);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("ramiro todd"
               , "harris vega"
               , context
               , nuevoPersonaGuardia.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuarioGuardia = new Usuario
            {
                Email = "guardia@gmail.com",
                UserName = "guardia@gmail.com",
                PersonaId = nuevoPersonaGuardia.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = false
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuarioGuardia, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuarioGuardia, "GUARDIA");


            //USUARIO SOLICITANTE
            // datos de la persona inicial
            var nuevaPersonaSolicitante = new Persona
            {
                PersonaId = new Guid(),
                Rut = "55444333-6"
            };
            await context.Persona.AddAsync(nuevaPersonaSolicitante);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("cristian elias"
               , "vergara vargas"
               , context
               , nuevaPersonaSolicitante.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuarioSolicitante = new Usuario
            {
                Email = "usuario@gmail.com",
                UserName = "usuario@gmail.com",
                PersonaId = nuevaPersonaSolicitante.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = false
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuarioSolicitante, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuarioSolicitante, "SOLICITANTE");


            //USUARIO CONTACTO
            // datos de la persona inicial
            var nuevaPersonaContacto = new Persona
            {
                PersonaId = new Guid(),
                Rut = "4222333-1"
            };
            await context.Persona.AddAsync(nuevaPersonaContacto);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("daniel tomas"
               , "gutierrez alanes"
               , context
               , nuevaPersonaContacto.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuarioContacto = new Usuario
            {
                Email = "contacto@gmail.com",
                UserName = "contacto@gmail.com",
                PersonaId = nuevaPersonaContacto.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = true
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuarioContacto, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuarioContacto, "CONTACTO");


            //USUARIO HSEQ
            // datos de la persona inicial
            var nuevaPersonaHSEQ = new Persona
            {
                PersonaId = new Guid(),
                Rut = "4999333-k"
            };
            await context.Persona.AddAsync(nuevaPersonaHSEQ);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("romina andrea"
               , "alcallaga martinez"
               , context
               , nuevaPersonaHSEQ.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuarioHSEQ = new Usuario
            {
                Email = "hseq@gmail.com",
                UserName = "hseq@gmail.com",
                PersonaId = nuevaPersonaHSEQ.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = true
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuarioHSEQ, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuarioHSEQ, "HSEQ");


            //USUARIO OPIP
            // datos de la persona inicial
            var nuevaPersonaOPIP = new Persona
            {
                PersonaId = new Guid(),
                Rut = "88555333-1"
            };
            await context.Persona.AddAsync(nuevaPersonaOPIP);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("manuel andres"
               , "tapia barrios"
               , context
               , nuevaPersonaOPIP.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuarioOPIP = new Usuario
            {
                Email = "opip@gmail.com",
                UserName = "opip@gmail.com",
                PersonaId = nuevaPersonaOPIP.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = true
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuarioOPIP, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuarioOPIP, "OPIP");


            //USUARIO JEFE OPERACIONES
            // datos de la persona inicial
            var nuevaPersonaJO = new Persona
            {
                PersonaId = new Guid(),
                Rut = "66222333-9"
            };
            await context.Persona.AddAsync(nuevaPersonaJO);

            // agregar y relacionar nombres y apellidos
            await AlmacenarNombres.AgregarNombres("elias danilo"
               , "alcallaga martinez"
               , context
               , nuevaPersonaJO.PersonaId);
            await context.SaveChangesAsync();

            // usuario principal
            var nuevoUsuario = new Usuario
            {
                Email = "jo@gmail.com",
                UserName = "jo@gmail.com",
                PersonaId = nuevaPersonaJO.PersonaId,
                EmpresaId = nuevaEmpresa.EmpresaId,
                NoPerteneceEmpresa = true
            };

            // agregar el usuario y generar el hash de al clave
            await usuarioManager.CreateAsync(nuevoUsuario, "P@ssw0rd");
            // agregar rol correspondiente al admin
            await usuarioManager.AddToRoleAsync(nuevoUsuario, "JEFE_OPERACIONES");
        }
    }
}