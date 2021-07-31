using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class BaseDeDatosCompletaV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PasePersonaExterna_PasePersonaExterna_PasePersonaExternaPaseId_PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna");

            migrationBuilder.DropIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna");

            migrationBuilder.DropIndex(
                name: "IX_PasePersonaExterna_PasePersonaExternaPaseId_PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna");

            migrationBuilder.DropIndex(
                name: "IX_ExamenesCompetencia_DocumentoId",
                table: "ExamenesCompetencia");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AsesorPrevencion_PersonaId",
                table: "AsesorPrevencion");

            migrationBuilder.DropColumn(
                name: "PasePersonaExternaPaseId",
                table: "PasePersonaExterna");

            migrationBuilder.DropColumn(
                name: "PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna");

            migrationBuilder.DropColumn(
                name: "PersonaIDPersona",
                table: "AsesorPrevencion");

            migrationBuilder.RenameColumn(
                name: "pasaporte",
                table: "PersonaExterna",
                newName: "Pasaporte");

            migrationBuilder.RenameColumn(
                name: "nacionalidad",
                table: "PersonaExterna",
                newName: "Nacionalidad");

            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "Pase",
                newName: "Tipo");

            migrationBuilder.AlterColumn<bool>(
                name: "Obligatoriedad",
                table: "TipoDocumento",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegistroPersona_DocumentoId",
                table: "RegistroPersona",
                column: "DocumentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesCompetencia_DocumentoId",
                table: "ExamenesCompetencia",
                column: "DocumentoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonaId",
                table: "AspNetUsers",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AsesorPrevencion_PersonaId",
                table: "AsesorPrevencion",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnexoContrato_DocumentoId",
                table: "AnexoContrato",
                column: "DocumentoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnexoContrato_Documento_DocumentoId",
                table: "AnexoContrato",
                column: "DocumentoId",
                principalTable: "Documento",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroPersona_Documento_DocumentoId",
                table: "RegistroPersona",
                column: "DocumentoId",
                principalTable: "Documento",
                principalColumn: "DocumentoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnexoContrato_Documento_DocumentoId",
                table: "AnexoContrato");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistroPersona_Documento_DocumentoId",
                table: "RegistroPersona");

            migrationBuilder.DropIndex(
                name: "IX_RegistroPersona_DocumentoId",
                table: "RegistroPersona");

            migrationBuilder.DropIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna");

            migrationBuilder.DropIndex(
                name: "IX_ExamenesCompetencia_DocumentoId",
                table: "ExamenesCompetencia");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AsesorPrevencion_PersonaId",
                table: "AsesorPrevencion");

            migrationBuilder.DropIndex(
                name: "IX_AnexoContrato_DocumentoId",
                table: "AnexoContrato");

            migrationBuilder.RenameColumn(
                name: "Pasaporte",
                table: "PersonaExterna",
                newName: "pasaporte");

            migrationBuilder.RenameColumn(
                name: "Nacionalidad",
                table: "PersonaExterna",
                newName: "nacionalidad");

            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Pase",
                newName: "tipo");

            migrationBuilder.AlterColumn<string>(
                name: "Obligatoriedad",
                table: "TipoDocumento",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<Guid>(
                name: "PasePersonaExternaPaseId",
                table: "PasePersonaExterna",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonaIDPersona",
                table: "AsesorPrevencion",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaExterna_PersonaId",
                table: "PersonaExterna",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_PasePersonaExterna_PasePersonaExternaPaseId_PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna",
                columns: new[] { "PasePersonaExternaPaseId", "PasePersonaExternaPersonaExternaId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamenesCompetencia_DocumentoId",
                table: "ExamenesCompetencia",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonaId",
                table: "AspNetUsers",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_AsesorPrevencion_PersonaId",
                table: "AsesorPrevencion",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PasePersonaExterna_PasePersonaExterna_PasePersonaExternaPaseId_PasePersonaExternaPersonaExternaId",
                table: "PasePersonaExterna",
                columns: new[] { "PasePersonaExternaPaseId", "PasePersonaExternaPersonaExternaId" },
                principalTable: "PasePersonaExterna",
                principalColumns: new[] { "PaseId", "PersonaExternaId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
