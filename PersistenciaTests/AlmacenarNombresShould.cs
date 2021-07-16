using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistencia;
using Persistencia.AuxiliaresAlmacenamiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PersistenciaTests
{
    /// <summary>
    /// Test para AlmacenarNombres
    /// </summary>
    public class AlmacenarNombresShould
    {
        /// <summary>
        /// verificar que los nombres se enlazan a una Persona
        /// </summary>
        [Fact]
        public async Task EnlazarNombres()
        {
            // --- Arrange ---
            // persona de prueba 
            Persona persona = new Persona
            {
                PersonaId = new Guid(),
                Rut = "11223344-7",
                Pasaporte = ""
            };

            // base de datos en memoria 
            var options = new DbContextOptionsBuilder<SistemaPasesContext>()
                .UseInMemoryDatabase(databaseName: "dbTest", new InMemoryDatabaseRoot())
                .Options;
            var context = new SistemaPasesContext(options);

            await context.Persona.AddAsync(persona);
            await context.SaveChangesAsync();

            // --- Act ---
            // ejecucion de la funcion en cuetion
            await AlmacenarNombres
                // nombres para persona ya existente
                .AgregarNombres("manuel javier"
                , "dominguez davila"
                , context
                , persona.PersonaId);
            await context.SaveChangesAsync();

            // ---- Assert ---
            var personaYNombres = await context.Persona
                .Include(p => p.NombresRel)
                .ThenInclude(n => n.NombreRel)
                .Include(p => p.ApellidosRel)
                .ThenInclude(n => n.ApellidoRel)
                .FirstAsync(r => r.Rut == persona.Rut);

            // verificar que se devolvieron nombres
            Assert.NotEmpty(personaYNombres.NombresRel);

            // verificar que se devolvieron nombres/apellidos correctamente
            Assert.Equal("manuel"
                , personaYNombres.NombresRel.First().NombreRel.Titulo);
            Assert.Equal("dominguez"
                , personaYNombres.ApellidosRel.First().ApellidoRel.Titulo);
        }

        /// <summary>
        /// verificar que los nombres vacios no se enlazan a una Persona
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task NoEnlazarNombresNulos()
        {
            // --- Arrange ---
            // persona de prueba 
            Persona persona = new Persona
            {
                PersonaId = new Guid(),
                Rut = "11223344-7",
                Pasaporte = ""
            };

            // base de datos en memoria 
            var options = new DbContextOptionsBuilder<SistemaPasesContext>()
                .UseInMemoryDatabase(databaseName: "dbTest", new InMemoryDatabaseRoot())
                .Options;
            var context = new SistemaPasesContext(options);

            await context.Persona.AddAsync(persona);
            await context.SaveChangesAsync();

            // --- Act ---
            // ---- Assert ---
            // ejecucion de la funcion en cuetion
            Assert.Throws<AggregateException>(() =>
            {
                AlmacenarNombres
                // nombres para persona ya existente
                .AgregarNombres(null
                , null
                , context
                , persona.PersonaId).Wait();
            });
        }
    }
}
