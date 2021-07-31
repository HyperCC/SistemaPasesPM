using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_PersonaExterna_PersonaExternaRelPersonaExternaId",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "PersonaExterna",
                table: "Persona");

            migrationBuilder.RenameColumn(
                name: "PersonaExternaRelPersonaExternaId",
                table: "Persona",
                newName: "PersonaExternaId");

            migrationBuilder.RenameIndex(
                name: "IX_Persona_PersonaExternaRelPersonaExternaId",
                table: "Persona",
                newName: "IX_Persona_PersonaExternaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_PersonaExterna_PersonaExternaId",
                table: "Persona",
                column: "PersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_PersonaExterna_PersonaExternaId",
                table: "Persona");

            migrationBuilder.RenameColumn(
                name: "PersonaExternaId",
                table: "Persona",
                newName: "PersonaExternaRelPersonaExternaId");

            migrationBuilder.RenameIndex(
                name: "IX_Persona_PersonaExternaId",
                table: "Persona",
                newName: "IX_Persona_PersonaExternaRelPersonaExternaId");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExterna",
                table: "Persona",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_PersonaExterna_PersonaExternaRelPersonaExternaId",
                table: "Persona",
                column: "PersonaExternaRelPersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
