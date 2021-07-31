using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia
{
    /// <summary>
    /// Base de datos principal para todo el sistema
    /// </summary>
    public class SistemaPasesContext : IdentityDbContext<Usuario>
    {
        // extension por configuracion para DbContextOptions
        public SistemaPasesContext(DbContextOptions options) : base(options)
        {
        }

        // metodo heredado para re configurar relaciones
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // genera las entiades declaradas con las migraciones
            base.OnModelCreating(modelBuilder);

            // declaracion de clave primaria compuesta para relaciones N-N
            modelBuilder.Entity<NombrePersona>().HasKey(ci => new { ci.PersonaId, ci.NombreId });
            modelBuilder.Entity<ApellidoPersona>().HasKey(ci => new { ci.PersonaId, ci.ApellidoId });
            modelBuilder.Entity<PasePersona>().HasKey(ci => new { ci.PaseId, ci.PersonaId });
            modelBuilder.Entity<Pase>().Property(p => p.Estado).HasConversion<string>();
            modelBuilder.Entity<Pase>().Property(p => p.Tipo).HasConversion<string>();
            modelBuilder.Entity<Pase>().HasOne(u => u.UsuarioRel).WithMany(p => p.PasesRel);
            modelBuilder.Entity<TipoDocumento>().Property(t => t.Obligatoriedad).HasConversion<bool>();
            modelBuilder.Entity<TipoDocumento>().Property(t => t.IsUnique).HasConversion<bool>();

        }

        // conversion de los modelos a entidades en la Solucion
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Nombre> Nombre { get; set; }
        public DbSet<NombrePersona> NombrePersona { get; set; }
        public DbSet<Apellido> Apellido { get; set; }
        public DbSet<ApellidoPersona> ApellidoPersona { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<AnexoContrato> AnexoContrato { get; set; }
        public DbSet<AsesorPrevencion> AsesorPrevencion { get; set; }
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Pase> Pase { get; set; }
        public DbSet<PasePersona> PasePersonaExterna { get; set; }
        public DbSet<PersonaExterna> PersonaExterna { get; set; }
        public DbSet<RegistroPersona> RegistroPersona { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
    }
}
