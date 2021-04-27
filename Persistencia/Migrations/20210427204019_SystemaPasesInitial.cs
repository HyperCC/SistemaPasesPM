using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class SystemaPasesInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AsesorPrevencion",
                columns: table => new
                {
                    AsesorPrevencionId = table.Column<Guid>(nullable: false),
                    RegistroSns = table.Column<string>(nullable: true),
                    PersonaIDPersona = table.Column<string>(nullable: true),
                    PersonaId = table.Column<Guid>(nullable: false),
                    PaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsesorPrevencion", x => x.AsesorPrevencionId);
                    table.ForeignKey(
                        name: "FK_AsesorPrevencion_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaExterna",
                columns: table => new
                {
                    PersonaExternaId = table.Column<Guid>(nullable: false),
                    nacionalidad = table.Column<string>(nullable: true),
                    pasaporte = table.Column<string>(nullable: true),
                    PersonaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaExterna", x => x.PersonaExternaId);
                    table.ForeignKey(
                        name: "FK_PersonaExterna_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoDocumento",
                columns: table => new
                {
                    TipoDocumentoId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Obligatoriedad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDocumento", x => x.TipoDocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "AnexoContrato",
                columns: table => new
                {
                    AnexoContratoId = table.Column<Guid>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    DocumentoId = table.Column<Guid>(nullable: false),
                    PersonaExternaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoContrato", x => x.AnexoContratoId);
                    table.ForeignKey(
                        name: "FK_AnexoContrato_PersonaExterna_PersonaExternaId",
                        column: x => x.PersonaExternaId,
                        principalTable: "PersonaExterna",
                        principalColumn: "PersonaExternaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pase",
                columns: table => new
                {
                    PaseId = table.Column<Guid>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    FechaTermino = table.Column<DateTime>(nullable: false),
                    tipo = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    Motivo = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    EmpresaId = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    UsuarioRelId = table.Column<string>(nullable: true),
                    PersonaExternaRelPersonaExternaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pase", x => x.PaseId);
                    table.ForeignKey(
                        name: "FK_Pase_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pase_PersonaExterna_PersonaExternaRelPersonaExternaId",
                        column: x => x.PersonaExternaRelPersonaExternaId,
                        principalTable: "PersonaExterna",
                        principalColumn: "PersonaExternaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pase_AspNetUsers_UsuarioRelId",
                        column: x => x.UsuarioRelId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistroPersona",
                columns: table => new
                {
                    RegistroPersonaId = table.Column<Guid>(nullable: false),
                    FechaRegistro = table.Column<DateTime>(nullable: false),
                    PersonaExternaId = table.Column<Guid>(nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroPersona", x => x.RegistroPersonaId);
                    table.ForeignKey(
                        name: "FK_RegistroPersona_PersonaExterna_PersonaExternaId",
                        column: x => x.PersonaExternaId,
                        principalTable: "PersonaExterna",
                        principalColumn: "PersonaExternaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    DocumentoId = table.Column<Guid>(nullable: false),
                    RutaDocumento = table.Column<string>(nullable: true),
                    FechaCaducidad = table.Column<DateTime>(nullable: false),
                    TipoDocumentoId = table.Column<Guid>(nullable: false),
                    PaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.DocumentoId);
                    table.ForeignKey(
                        name: "FK_Documento_Pase_PaseId",
                        column: x => x.PaseId,
                        principalTable: "Pase",
                        principalColumn: "PaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documento_TipoDocumento_TipoDocumentoId",
                        column: x => x.TipoDocumentoId,
                        principalTable: "TipoDocumento",
                        principalColumn: "TipoDocumentoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PasePersonaExterna",
                columns: table => new
                {
                    PaseId = table.Column<Guid>(nullable: false),
                    PersonaExternaId = table.Column<Guid>(nullable: false),
                    PasePersonaExternaPaseId = table.Column<Guid>(nullable: true),
                    PasePersonaExternaPersonaExternaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasePersonaExterna", x => new { x.PaseId, x.PersonaExternaId });
                    table.ForeignKey(
                        name: "FK_PasePersonaExterna_Pase_PaseId",
                        column: x => x.PaseId,
                        principalTable: "Pase",
                        principalColumn: "PaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PasePersonaExterna_PersonaExterna_PersonaExternaId",
                        column: x => x.PersonaExternaId,
                        principalTable: "PersonaExterna",
                        principalColumn: "PersonaExternaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PasePersonaExterna_PasePersonaExterna_PasePersonaExternaPaseId_PasePersonaExternaPersonaExternaId",
                        columns: x => new { x.PasePersonaExternaPaseId, x.PasePersonaExternaPersonaExternaId },
                        principalTable: "PasePersonaExterna",
                        principalColumns: new[] { "PaseId", "PersonaExternaId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamenesCompetencia",
                columns: table => new
                {
                    ExamenesCompetenciaId = table.Column<Guid>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: false),
                    PersonaExternaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamenesCompetencia", x => x.ExamenesCompetenciaId);
                    table.ForeignKey(
                        name: "FK_ExamenesCompetencia_Documento_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documento",
                        principalColumn: "DocumentoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamenesCompetencia_PersonaExterna_PersonaExternaId",
                        column: x => x.PersonaExternaId,
                        principalTable: "PersonaExterna",
                        principalColumn: "PersonaExternaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnexoContrato_PersonaExternaId",
                table: "AnexoContrato",
                column: "PersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsesorPrevencion_PersonaId",
                table: "AsesorPrevencion",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_PaseId",
                table: "Documento",
                column: "PaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_TipoDocumentoId",
                table: "Documento",
                column: "TipoDocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesCompetencia_DocumentoId",
                table: "ExamenesCompetencia",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesCompetencia_PersonaExternaId",
                table: "ExamenesCompetencia",
                column: "PersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pase_EmpresaId",
                table: "Pase",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pase_PersonaExternaRelPersonaExternaId",
                table: "Pase",
                column: "PersonaExternaRelPersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pase_UsuarioRelId",
                table: "Pase",
                column: "UsuarioRelId");

            migrationBuilder.CreateIndex(
                name: "IX_PasePersonaExterna_PersonaExternaId",
                table: "PasePersonaExterna",
                column: "PersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_PasePersonaExterna_PasePersonaExternaPaseId_PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna",
                columns: new[] { "PasePersonaExternaPaseId", "PasePersonaExternaPersonaExternaId" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroPersona_PersonaExternaId",
                table: "RegistroPersona",
                column: "PersonaExternaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnexoContrato");

            migrationBuilder.DropTable(
                name: "AsesorPrevencion");

            migrationBuilder.DropTable(
                name: "ExamenesCompetencia");

            migrationBuilder.DropTable(
                name: "PasePersonaExterna");

            migrationBuilder.DropTable(
                name: "RegistroPersona");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Pase");

            migrationBuilder.DropTable(
                name: "TipoDocumento");

            migrationBuilder.DropTable(
                name: "PersonaExterna");
        }
    }
}
