using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBComplete8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonaTipoNombre");

            migrationBuilder.DropTable(
                name: "TipoNombre");

            migrationBuilder.CreateTable(
                name: "Apellido",
                columns: table => new
                {
                    ApellidoId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Posicion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apellido", x => x.ApellidoId);
                });

            migrationBuilder.CreateTable(
                name: "Nombre",
                columns: table => new
                {
                    NombreId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    Posicion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nombre", x => x.NombreId);
                });

            migrationBuilder.CreateTable(
                name: "ApellidoPersona",
                columns: table => new
                {
                    PersonaId = table.Column<Guid>(nullable: false),
                    ApellidoId = table.Column<Guid>(nullable: false),
                    NombreId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApellidoPersona", x => new { x.PersonaId, x.ApellidoId });
                    table.ForeignKey(
                        name: "FK_ApellidoPersona_Apellido_ApellidoId",
                        column: x => x.ApellidoId,
                        principalTable: "Apellido",
                        principalColumn: "ApellidoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApellidoPersona_Nombre_NombreId",
                        column: x => x.NombreId,
                        principalTable: "Nombre",
                        principalColumn: "NombreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApellidoPersona_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NombrePersona",
                columns: table => new
                {
                    PersonaId = table.Column<Guid>(nullable: false),
                    NombreId = table.Column<Guid>(nullable: false),
                    ApellidoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NombrePersona", x => new { x.PersonaId, x.NombreId });
                    table.ForeignKey(
                        name: "FK_NombrePersona_Apellido_ApellidoId",
                        column: x => x.ApellidoId,
                        principalTable: "Apellido",
                        principalColumn: "ApellidoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NombrePersona_Nombre_NombreId",
                        column: x => x.NombreId,
                        principalTable: "Nombre",
                        principalColumn: "NombreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NombrePersona_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApellidoPersona_ApellidoId",
                table: "ApellidoPersona",
                column: "ApellidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ApellidoPersona_NombreId",
                table: "ApellidoPersona",
                column: "NombreId");

            migrationBuilder.CreateIndex(
                name: "IX_NombrePersona_ApellidoId",
                table: "NombrePersona",
                column: "ApellidoId");

            migrationBuilder.CreateIndex(
                name: "IX_NombrePersona_NombreId",
                table: "NombrePersona",
                column: "NombreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApellidoPersona");

            migrationBuilder.DropTable(
                name: "NombrePersona");

            migrationBuilder.DropTable(
                name: "Apellido");

            migrationBuilder.DropTable(
                name: "Nombre");

            migrationBuilder.CreateTable(
                name: "TipoNombre",
                columns: table => new
                {
                    TipoNombreId = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Posicion = table.Column<int>(nullable: false),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoNombre", x => x.TipoNombreId);
                });

            migrationBuilder.CreateTable(
                name: "PersonaTipoNombre",
                columns: table => new
                {
                    PersonaId = table.Column<Guid>(nullable: false),
                    TipoNombreId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaTipoNombre", x => new { x.PersonaId, x.TipoNombreId });
                    table.ForeignKey(
                        name: "FK_PersonaTipoNombre_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaTipoNombre_TipoNombre_TipoNombreId",
                        column: x => x.TipoNombreId,
                        principalTable: "TipoNombre",
                        principalColumn: "TipoNombreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTipoNombre_TipoNombreId",
                table: "PersonaTipoNombre",
                column: "TipoNombreId");
        }
    }
}
