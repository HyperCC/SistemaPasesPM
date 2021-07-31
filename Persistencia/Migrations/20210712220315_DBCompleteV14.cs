using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Persona_PersonasRelPersonaId",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "Persona",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "PersonasRelPersonaId",
                table: "Documento",
                newName: "PersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_Documento_PersonasRelPersonaId",
                table: "Documento",
                newName: "IX_Documento_PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Persona_PersonaId",
                table: "Documento",
                column: "PersonaId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documento_Persona_PersonaId",
                table: "Documento");

            migrationBuilder.RenameColumn(
                name: "PersonaId",
                table: "Documento",
                newName: "PersonasRelPersonaId");

            migrationBuilder.RenameIndex(
                name: "IX_Documento_PersonaId",
                table: "Documento",
                newName: "IX_Documento_PersonasRelPersonaId");

            migrationBuilder.AddColumn<Guid>(
                name: "Persona",
                table: "Documento",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documento_Persona_PersonasRelPersonaId",
                table: "Documento",
                column: "PersonasRelPersonaId",
                principalTable: "Persona",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
