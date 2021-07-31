using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnexoContrato_PersonaExterna_PersonaExternaId",
                table: "AnexoContrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Pase_PaseId",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_PasePersonaExterna_PersonaExterna_PersonaExternaId",
                table: "PasePersonaExterna");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonaExterna_Persona_PersonaId",
                table: "PersonaExterna");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroPersona_PersonaExterna_PersonaExternaId",
                table: "RegistroPersona");

            migrationBuilder.DropTable(
                name: "ExamenesCompetencia");

            migrationBuilder.DropIndex(
                name: "IX_RegistroPersona_PersonaExternaId",
                table: "RegistroPersona");

            migrationBuilder.DropIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna");

            migrationBuilder.DropIndex(
                name: "IX_AnexoContrato_PersonaExternaId",
                table: "AnexoContrato");

            migrationBuilder.DropColumn(
                name: "PersonaExternaId",
                table: "RegistroPersona");

            migrationBuilder.DropColumn(
                name: "Pasaporte",
                table: "PersonaExterna");

            migrationBuilder.DropColumn(
                name: "PersonaExternaId",
                table: "AnexoContrato");

            migrationBuilder.RenameColumn(
                name: "PersonaExternaId",
                table: "PasePersonaExterna",
                newName: "PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_PasePersonaExterna_PersonaExternaId",
                table: "PasePersonaExterna",
                newName: "IX_PasePersonaExterna_PersonaId");

            migrationBuilder.RenameColumn(
                name: "PersonaExternaId",
                table: "Documento",
                newName: "PersonasRelPersonaId");

            migrationBuilder.RenameColumn(
                name: "FechaCaducidad",
                table: "Documento",
                newName: "FechaVencimiento");

            migrationBuilder.AddColumn<string>(
                name: "Pasaporte",
                table: "Persona",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExterna",
                table: "Persona",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExternaRelPersonaExternaId",
                table: "Persona",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacion",
                table: "Pase",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaseId",
                table: "Documento",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "Persona",
                table: "Documento",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_PersonaExternaRelPersonaExternaId",
                table: "Persona",
                column: "PersonaExternaRelPersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_EmpresaId",
                table: "Documento",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_PersonasRelPersonaId",
                table: "Documento",
                column: "PersonasRelPersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Empresa_EmpresaId",
                table: "Documento",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Pase_PaseId",
                table: "Documento",
                column: "PaseId",
                principalTable: "Pase",
                principalColumn: "PaseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Persona_PersonasRelPersonaId",
                table: "Documento",
                column: "PersonasRelPersonaId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PasePersonaExterna_Persona_PersonaId",
                table: "PasePersonaExterna",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_PersonaExterna_PersonaExternaRelPersonaExternaId",
                table: "Persona",
                column: "PersonaExternaRelPersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Empresa_EmpresaId",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Pase_PaseId",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Persona_PersonasRelPersonaId",
                table: "Documento");

            migrationBuilder.DropForeignKey(
                name: "FK_PasePersonaExterna_Persona_PersonaId",
                table: "PasePersonaExterna");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_PersonaExterna_PersonaExternaRelPersonaExternaId",
                table: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_Persona_PersonaExternaRelPersonaExternaId",
                table: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_Documento_EmpresaId",
                table: "Documento");

            migrationBuilder.DropIndex(
                name: "IX_Documento_PersonasRelPersonaId",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Pasaporte",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "PersonaExterna",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "PersonaExternaRelPersonaExternaId",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "Observacion",
                table: "Pase");

            migrationBuilder.DropColumn(
                name: "Persona",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "PasePersonaExterna",
                newName: "PersonaExternaId");

            migrationBuilder.RenameIndex(
                name: "IX_PasePersonaExterna_PersonaId",
                table: "PasePersonaExterna",
                newName: "IX_PasePersonaExterna_PersonaExternaId");

            migrationBuilder.RenameColumn(
                name: "PersonasRelPersonaId",
                table: "Documento",
                newName: "PersonaExternaId");

            migrationBuilder.RenameColumn(
                name: "FechaVencimiento",
                table: "Documento",
                newName: "FechaCaducidad");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExternaId",
                table: "RegistroPersona",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Pasaporte",
                table: "PersonaExterna",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PaseId",
                table: "Documento",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExternaId",
                table: "AnexoContrato",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExamenesCompetencia",
                columns: table => new
                {
                    ExamenesCompetenciaId = table.Column<Guid>(nullable: false),
                    DocumentoId = table.Column<Guid>(nullable: false),
                    FechaVencimiento = table.Column<DateTime>(nullable: false),
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
                name: "IX_RegistroPersona_PersonaExternaId",
                table: "RegistroPersona",
                column: "PersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnexoContrato_PersonaExternaId",
                table: "AnexoContrato",
                column: "PersonaExternaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesCompetencia_DocumentoId",
                table: "ExamenesCompetencia",
                column: "DocumentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesCompetencia_PersonaExternaId",
                table: "ExamenesCompetencia",
                column: "PersonaExternaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnexoContrato_PersonaExterna_PersonaExternaId",
                table: "AnexoContrato",
                column: "PersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Pase_PaseId",
                table: "Documento",
                column: "PaseId",
                principalTable: "Pase",
                principalColumn: "PaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PasePersonaExterna_PersonaExterna_PersonaExternaId",
                table: "PasePersonaExterna",
                column: "PersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonaExterna_Persona_PersonaId",
                table: "PersonaExterna",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroPersona_PersonaExterna_PersonaExternaId",
                table: "RegistroPersona",
                column: "PersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
