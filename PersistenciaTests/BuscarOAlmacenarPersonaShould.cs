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
    /// Test para BuscarOAlmacenarPersona
    /// </summary>
    public class BuscarOAlmacenarPersonaShould
    {
        /// <summary>
        /// Comprobar que una persona existente es encontrada
        /// </summary>
        [Fact]
        public async Task EncontrarPersonaAsync()
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
            // cantidad actual de peronas es 1
            var cantPersonas = context.Persona.Count();
            Assert.Equal(1, cantPersonas);

            // ejecucion de la funcion en cuetion
            Persona personaProceada = await BuscarOAlmacenarPersona
                // persona ya existente
                .BuscarOAgregarPersona(context
                , "11223344-7"
                , "carly elisa"
                , "dominguez davila");
            await context.SaveChangesAsync();

            // ---- Assert ---
            var nuevaCantPersonas = context.Persona.Count();

            // verificar mismo numero de peronas
            Assert.Equal(cantPersonas, nuevaCantPersonas);
            // verificar misma persona
            Assert.Equal(context.Persona.First().Rut, personaProceada.Rut);
        }

        /// <summary>
        /// almacenar una nueva persona 
        /// </summary>
        [Fact]
        public async Task AlmacenarPersonaAsync()
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
            // cantidad actual de peronas es 1
            var cantPersonas = context.Persona.Count();
            Assert.Equal(1, cantPersonas);

            // ejecucion de la funcion en cuetion
            var personaProceada = await BuscarOAlmacenarPersona
                // persona ya existente
                .BuscarOAgregarPersona(context
                , "99337744-k"
                , "daniel alex"
                , "martinez villanueva");
            await context.SaveChangesAsync();

            // --- Assert ---
            var nuevaCantPersonas = context.Persona.Count();

            // verificar mismo nuero de peronas
            Assert.Equal(2, nuevaCantPersonas);
            Assert.Equal(personaProceada.Rut, context.Persona.Last().Rut);
        }
    }
}
