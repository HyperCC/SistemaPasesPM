﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia;

namespace Persistencia.Migrations
{
    [DbContext(typeof(SistemaPasesContext))]
    partial class SistemaPasesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dominio.Entidades.AnexoContrato", b =>
                {
                    b.Property<Guid>("AnexoContratoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<Guid>("DocumentoId");

                    b.HasKey("AnexoContratoId");

                    b.HasIndex("DocumentoId")
                        .IsUnique();

                    b.ToTable("AnexoContrato");
                });

            modelBuilder.Entity("Dominio.Entidades.Apellido", b =>
                {
                    b.Property<Guid>("ApellidoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Posicion");

                    b.Property<string>("Titulo");

                    b.HasKey("ApellidoId");

                    b.ToTable("Apellido");
                });

            modelBuilder.Entity("Dominio.Entidades.ApellidoPersona", b =>
                {
                    b.Property<Guid>("PersonaId");

                    b.Property<Guid>("ApellidoId");

                    b.Property<Guid?>("NombreId");

                    b.HasKey("PersonaId", "ApellidoId");

                    b.HasIndex("ApellidoId");

                    b.HasIndex("NombreId");

                    b.ToTable("ApellidoPersona");
                });

            modelBuilder.Entity("Dominio.Entidades.AsesorPrevencion", b =>
                {
                    b.Property<Guid>("AsesorPrevencionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PersonaId");

                    b.Property<string>("RegistroSns");

                    b.HasKey("AsesorPrevencionId");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("AsesorPrevencion");
                });

            modelBuilder.Entity("Dominio.Entidades.Documento", b =>
                {
                    b.Property<Guid>("DocumentoId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("EmpresaId");

                    b.Property<string>("Extension");

                    b.Property<DateTime>("FechaVencimiento");

                    b.Property<Guid?>("PaseId");

                    b.Property<Guid?>("PersonaId");

                    b.Property<string>("RutaDocumento");

                    b.Property<Guid>("TipoDocumentoId");

                    b.HasKey("DocumentoId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("PaseId");

                    b.HasIndex("PersonaId");

                    b.HasIndex("TipoDocumentoId");

                    b.ToTable("Documento");
                });

            modelBuilder.Entity("Dominio.Entidades.Empresa", b =>
                {
                    b.Property<Guid>("EmpresaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<string>("Rut");

                    b.HasKey("EmpresaId");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Dominio.Entidades.Nombre", b =>
                {
                    b.Property<Guid>("NombreId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Posicion");

                    b.Property<string>("Titulo");

                    b.HasKey("NombreId");

                    b.ToTable("Nombre");
                });

            modelBuilder.Entity("Dominio.Entidades.NombrePersona", b =>
                {
                    b.Property<Guid>("PersonaId");

                    b.Property<Guid>("NombreId");

                    b.Property<Guid?>("ApellidoId");

                    b.HasKey("PersonaId", "NombreId");

                    b.HasIndex("ApellidoId");

                    b.HasIndex("NombreId");

                    b.ToTable("NombrePersona");
                });

            modelBuilder.Entity("Dominio.Entidades.Pase", b =>
                {
                    b.Property<Guid>("PaseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Area");

                    b.Property<Guid?>("AsesorPrevencionId");

                    b.Property<Guid>("EmpresaId");

                    b.Property<string>("Estado")
                        .IsRequired();

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime>("FechaTermino");

                    b.Property<string>("Motivo");

                    b.Property<string>("Observacion");

                    b.Property<string>("ServicioAdjudicado");

                    b.Property<string>("Tipo")
                        .IsRequired();

                    b.Property<string>("UsuarioId");

                    b.HasKey("PaseId");

                    b.HasIndex("AsesorPrevencionId")
                        .IsUnique()
                        .HasFilter("[AsesorPrevencionId] IS NOT NULL");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pase");
                });

            modelBuilder.Entity("Dominio.Entidades.PasePersona", b =>
                {
                    b.Property<Guid>("PaseId");

                    b.Property<Guid>("PersonaId");

                    b.HasKey("PaseId", "PersonaId");

                    b.HasIndex("PersonaId");

                    b.ToTable("PasePersonaExterna");
                });

            modelBuilder.Entity("Dominio.Entidades.Persona", b =>
                {
                    b.Property<Guid>("PersonaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Pasaporte");

                    b.Property<Guid?>("PersonaExternaId");

                    b.Property<string>("Rut");

                    b.HasKey("PersonaId");

                    b.HasIndex("PersonaExternaId");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("Dominio.Entidades.PersonaExterna", b =>
                {
                    b.Property<Guid>("PersonaExternaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nacionalidad");

                    b.HasKey("PersonaExternaId");

                    b.ToTable("PersonaExterna");
                });

            modelBuilder.Entity("Dominio.Entidades.RegistroPersona", b =>
                {
                    b.Property<Guid>("RegistroPersonaId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DocumentoId");

                    b.Property<DateTime>("FechaRegistro");

                    b.HasKey("RegistroPersonaId");

                    b.HasIndex("DocumentoId")
                        .IsUnique();

                    b.ToTable("RegistroPersona");
                });

            modelBuilder.Entity("Dominio.Entidades.Rol", b =>
                {
                    b.Property<Guid>("RolId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Titulo");

                    b.Property<string>("UsuarioId");

                    b.HasKey("RolId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Rol");
                });

            modelBuilder.Entity("Dominio.Entidades.TipoDocumento", b =>
                {
                    b.Property<Guid>("TipoDocumentoId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Obligatoriedad");

                    b.Property<string>("Titulo");

                    b.HasKey("TipoDocumentoId");

                    b.ToTable("TipoDocumento");
                });

            modelBuilder.Entity("Dominio.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Contrasena");

                    b.Property<string>("Correo");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<Guid>("EmpresaId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<bool>("NoPerteneceEmpresa");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<Guid>("PersonaId");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Dominio.Entidades.AnexoContrato", b =>
                {
                    b.HasOne("Dominio.Entidades.Documento", "DocumentoRel")
                        .WithOne("AnexoContratoRel")
                        .HasForeignKey("Dominio.Entidades.AnexoContrato", "DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.ApellidoPersona", b =>
                {
                    b.HasOne("Dominio.Entidades.Apellido", "ApellidoRel")
                        .WithMany()
                        .HasForeignKey("ApellidoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Nombre")
                        .WithMany("PersonasRel")
                        .HasForeignKey("NombreId");

                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithMany("ApellidosRel")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.AsesorPrevencion", b =>
                {
                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithOne("AsesorPrevencionRel")
                        .HasForeignKey("Dominio.Entidades.AsesorPrevencion", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Documento", b =>
                {
                    b.HasOne("Dominio.Entidades.Empresa", "EmpresaRel")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.HasOne("Dominio.Entidades.Pase", "PaseRel")
                        .WithMany("DocumentosRel")
                        .HasForeignKey("PaseId");

                    b.HasOne("Dominio.Entidades.Persona", "PersonasRel")
                        .WithMany("DocumentosRel")
                        .HasForeignKey("PersonaId");

                    b.HasOne("Dominio.Entidades.TipoDocumento", "TipoDocumentoRel")
                        .WithMany("DocumentosRel")
                        .HasForeignKey("TipoDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.NombrePersona", b =>
                {
                    b.HasOne("Dominio.Entidades.Apellido")
                        .WithMany("PersonasRel")
                        .HasForeignKey("ApellidoId");

                    b.HasOne("Dominio.Entidades.Nombre", "NombreRel")
                        .WithMany()
                        .HasForeignKey("NombreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithMany("NombresRel")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Pase", b =>
                {
                    b.HasOne("Dominio.Entidades.AsesorPrevencion", "AsesorPrevencionRel")
                        .WithOne("PaseRel")
                        .HasForeignKey("Dominio.Entidades.Pase", "AsesorPrevencionId");

                    b.HasOne("Dominio.Entidades.Empresa", "EmpresaRel")
                        .WithMany("PasesRel")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Usuario", "UsuarioRel")
                        .WithMany("PasesRel")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Dominio.Entidades.PasePersona", b =>
                {
                    b.HasOne("Dominio.Entidades.Pase", "PaseRel")
                        .WithMany("PersonasRel")
                        .HasForeignKey("PaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithMany("PersonasRel")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Persona", b =>
                {
                    b.HasOne("Dominio.Entidades.PersonaExterna", "PersonaExternaRel")
                        .WithMany("PersonasRel")
                        .HasForeignKey("PersonaExternaId");
                });

            modelBuilder.Entity("Dominio.Entidades.RegistroPersona", b =>
                {
                    b.HasOne("Dominio.Entidades.Documento", "DocumentoRel")
                        .WithOne("RegistroPersonaRel")
                        .HasForeignKey("Dominio.Entidades.RegistroPersona", "DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Rol", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario", "UsuarioRel")
                        .WithMany("RolsRel")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Dominio.Entidades.Usuario", b =>
                {
                    b.HasOne("Dominio.Entidades.Empresa", "EmpresaRel")
                        .WithMany("UsuariosRel")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithOne("UsuarioRel")
                        .HasForeignKey("Dominio.Entidades.Usuario", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
