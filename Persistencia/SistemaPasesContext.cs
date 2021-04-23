using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia
{
    public class SistemaPasesContext : IdentityDbContext<Usuario>
    {
        // extension por configuracion para DbContextOptions
        public SistemaPasesContext(DbContextOptions options) : base(options)
        {
        }

        // metodo heredado para re configurar relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // genera las tablas donde se almacenaran los datos
            base.OnModelCreating(modelBuilder);

            // declaracion de clave primaria compuesta para relaciones N-N
            modelBuilder.Entity<PersonaTipoNombre>().HasKey(ci => new { ci.PersonaId, ci.TipoNombreId });
        }

        // conversion de los modelos a entidades en la Solucion
        public DbSet<Persona> Persona { get; set; }
        public DbSet<PersonaTipoNombre> PersonaTipoNombre { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<TipoNombre> TipoNombre { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}
