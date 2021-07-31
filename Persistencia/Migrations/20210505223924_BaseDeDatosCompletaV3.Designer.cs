﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistencia;

namespace Persistencia.Migrations
{
    [DbContext(typeof(SistemaPasesContext))]
    [Migration("20210505223924_BaseDeDatosCompletaV3")]
    partial class BaseDeDatosCompletaV3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("PersonaExternaId");

                    b.HasKey("AnexoContratoId");

                    b.HasIndex("DocumentoId")
                        .IsUnique();

                    b.HasIndex("PersonaExternaId");

                    b.ToTable("AnexoContrato");
                });

            modelBuilder.Entity("Dominio.Entidades.AsesorPrevencion", b =>
                {
                    b.Property<Guid>("AsesorPrevencionId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PaseId");

                    b.Property<Guid>("PersonaId");

                    b.Property<string>("RegistroSns");

                    b.HasKey("AsesorPrevencionId");

                    b.HasIndex("PaseId")
                        .IsUnique();

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("AsesorPrevencion");
                });

            modelBuilder.Entity("Dominio.Entidades.Documento", b =>
                {
                    b.Property<Guid>("DocumentoId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaCaducidad");

                    b.Property<Guid>("PaseId");

                    b.Property<string>("RutaDocumento");

                    b.Property<Guid>("TipoDocumentoId");

                    b.HasKey("DocumentoId");

                    b.HasIndex("PaseId");

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

            modelBuilder.Entity("Dominio.Entidades.ExamenesCompetencia", b =>
                {
                    b.Property<Guid>("ExamenesCompetenciaId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DocumentoId");

                    b.Property<DateTime>("FechaVencimiento");

                    b.Property<Guid>("PersonaExternaId");

                    b.HasKey("ExamenesCompetenciaId");

                    b.HasIndex("DocumentoId")
                        .IsUnique();

                    b.HasIndex("PersonaExternaId");

                    b.ToTable("ExamenesCompetencia");
                });

            modelBuilder.Entity("Dominio.Entidades.Pase", b =>
                {
                    b.Property<Guid>("PaseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Area");

                    b.Property<Guid>("EmpresaId");

                    b.Property<string>("Estado");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime>("FechaTermino");

                    b.Property<string>("Motivo");

                    b.Property<string>("Tipo");

                    b.Property<Guid>("UsuarioId");

                    b.Property<string>("UsuarioRelId");

                    b.HasKey("PaseId");

                    b.HasIndex("EmpresaId");

                    b.HasIndex("UsuarioRelId");

                    b.ToTable("Pase");
                });

            modelBuilder.Entity("Dominio.Entidades.PasePersonaExterna", b =>
                {
                    b.Property<Guid>("PaseId");

                    b.Property<Guid>("PersonaExternaId");

                    b.HasKey("PaseId", "PersonaExternaId");

                    b.HasIndex("PersonaExternaId");

                    b.ToTable("PasePersonaExterna");
                });

            modelBuilder.Entity("Dominio.Entidades.Persona", b =>
                {
                    b.Property<Guid>("PersonaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Rut");

                    b.HasKey("PersonaId");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("Dominio.Entidades.PersonaExterna", b =>
                {
                    b.Property<Guid>("PersonaExternaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nacionalidad");

                    b.Property<string>("Pasaporte");

                    b.Property<Guid>("PersonaId");

                    b.HasKey("PersonaExternaId");

                    b.HasIndex("PersonaId")
                        .IsUnique();

                    b.ToTable("PersonaExterna");
                });

            modelBuilder.Entity("Dominio.Entidades.PersonaTipoNombre", b =>
                {
                    b.Property<Guid>("PersonaId");

                    b.Property<Guid>("TipoNombreId");

                    b.HasKey("PersonaId", "TipoNombreId");

                    b.HasIndex("TipoNombreId");

                    b.ToTable("PersonaTipoNombre");
                });

            modelBuilder.Entity("Dominio.Entidades.RegistroPersona", b =>
                {
                    b.Property<Guid>("RegistroPersonaId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DocumentoId");

                    b.Property<DateTime>("FechaRegistro");

                    b.Property<Guid>("PersonaExternaId");

                    b.HasKey("RegistroPersonaId");

                    b.HasIndex("DocumentoId")
                        .IsUnique();

                    b.HasIndex("PersonaExternaId");

                    b.ToTable("RegistroPersona");
                });

            modelBuilder.Entity("Dominio.Entidades.Rol", b =>
                {
                    b.Property<Guid>("RolId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Titulo");

                    b.Property<Guid>("UsuarioId");

                    b.Property<string>("UsuarioRelId");

                    b.HasKey("RolId");

                    b.HasIndex("UsuarioRelId");

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

            modelBuilder.Entity("Dominio.Entidades.TipoNombre", b =>
                {
                    b.Property<Guid>("TipoNombreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<int>("Posicion");

                    b.Property<int>("Tipo");

                    b.HasKey("TipoNombreId");

                    b.ToTable("TipoNombre");
                });

            modelBuilder.Entity("Dominio.Entidades.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Captcha");

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

                    b.Property<Guid>("UsuarioId");

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

                    b.HasOne("Dominio.Entidades.PersonaExterna", "PersonaExternaRel")
                        .WithMany("AnexoContratosRel")
                        .HasForeignKey("PersonaExternaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.AsesorPrevencion", b =>
                {
                    b.HasOne("Dominio.Entidades.Pase", "PaseRel")
                        .WithOne("AsesorPrevencionRel")
                        .HasForeignKey("Dominio.Entidades.AsesorPrevencion", "PaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithOne("AsesorPrevencionRel")
                        .HasForeignKey("Dominio.Entidades.AsesorPrevencion", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Documento", b =>
                {
                    b.HasOne("Dominio.Entidades.Pase", "PaseRel")
                        .WithMany("DocumentosRel")
                        .HasForeignKey("PaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.TipoDocumento", "TipoDocumentoRel")
                        .WithMany("DocumentosRel")
                        .HasForeignKey("TipoDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.ExamenesCompetencia", b =>
                {
                    b.HasOne("Dominio.Entidades.Documento", "DocumentoRel")
                        .WithOne("ExamenesCompetenciaRel")
                        .HasForeignKey("Dominio.Entidades.ExamenesCompetencia", "DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.PersonaExterna", "PersonaExternaRel")
                        .WithMany("ExamenesCompetenciasRel")
                        .HasForeignKey("PersonaExternaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Pase", b =>
                {
                    b.HasOne("Dominio.Entidades.Empresa", "EmpresaRel")
                        .WithMany("PasesRel")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.Usuario", "UsuarioRel")
                        .WithMany()
                        .HasForeignKey("UsuarioRelId");
                });

            modelBuilder.Entity("Dominio.Entidades.PasePersonaExterna", b =>
                {
                    b.HasOne("Dominio.Entidades.Pase", "PaseRel")
                        .WithMany("PersonaExternasRel")
                        .HasForeignKey("PaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.PersonaExterna", "PersonaExternaRel")
                        .WithMany("PasesRel")
                        .HasForeignKey("PersonaExternaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.PersonaExterna", b =>
                {
                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithOne("PersonaExternaRel")
                        .HasForeignKey("Dominio.Entidades.PersonaExterna", "PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.PersonaTipoNombre", b =>
                {
                    b.HasOne("Dominio.Entidades.Persona", "PersonaRel")
                        .WithMany("TipoNombresRel")
                        .HasForeignKey("PersonaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.TipoNombre", "TipoNombreRel")
                        .WithMany("PersonasRel")
                        .HasForeignKey("TipoNombreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.RegistroPersona", b =>
                {
                    b.HasOne("Dominio.Entidades.Documento", "DocumentoRel")
                        .WithOne("RegistroPersonaRel")
                        .HasForeignKey("Dominio.Entidades.RegistroPersona", "DocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Entidades.PersonaExterna", "PersonaExternaRel")
                        .WithMany("RegistroPersonasRel")
                        .HasForeignKey("PersonaExternaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Entidades.Rol", b =>
                {
                    b.HasOne("Dominio.Entidades.Usuario", "UsuarioRel")
                        .WithMany("RolsRel")
                        .HasForeignKey("UsuarioRelId");
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
