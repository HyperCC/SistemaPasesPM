using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class BaseDeDatosCompletaV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pase_PersonaExterna_PersonaExternaRelPersonaExternaId",
                table: "Pase");

            migrationBuilder.DropIndex(
                name: "IX_Pase_PersonaExternaRelPersonaExternaId",
                table: "Pase");

            migrationBuilder.DropColumn(
                name: "PersonaExternaRelPersonaExternaId",
                table: "Pase");

            migrationBuilder.AddColumn<bool>(
                name: "Captcha",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NoPerteneceEmpresa",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AsesorPrevencion_PaseId",
                table: "AsesorPrevencion",
                column: "PaseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AsesorPrevencion_Pase_PaseId",
                table: "AsesorPrevencion",
                column: "PaseId",
                principalTable: "Pase",
                principalColumn: "PaseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsesorPrevencion_Pase_PaseId",
                table: "AsesorPrevencion");

            migrationBuilder.DropIndex(
                name: "IX_AsesorPrevencion_PaseId",
                table: "AsesorPrevencion");

            migrationBuilder.DropColumn(
                name: "Captcha",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NoPerteneceEmpresa",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExternaRelPersonaExternaId",
                table: "Pase",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pase_PersonaExternaRelPersonaExternaId",
                table: "Pase",
                column: "PersonaExternaRelPersonaExternaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pase_PersonaExterna_PersonaExternaRelPersonaExternaId",
                table: "Pase",
                column: "PersonaExternaRelPersonaExternaId",
                principalTable: "PersonaExterna",
                principalColumn: "PersonaExternaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
