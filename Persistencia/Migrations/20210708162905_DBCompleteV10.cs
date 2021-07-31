using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AsesorPrevencion_Pase_PaseId",
                table: "AsesorPrevencion");

            migrationBuilder.DropIndex(
                name: "IX_AsesorPrevencion_PaseId",
                table: "AsesorPrevencion");

            migrationBuilder.DropColumn(
                name: "PaseId",
                table: "AsesorPrevencion");

            migrationBuilder.AddColumn<Guid>(
                name: "AsesorPrevencionId",
                table: "Pase",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pase_AsesorPrevencionId",
                table: "Pase",
                column: "AsesorPrevencionId",
                unique: true,
                filter: "[AsesorPrevencionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pase_AsesorPrevencion_AsesorPrevencionId",
                table: "Pase",
                column: "AsesorPrevencionId",
                principalTable: "AsesorPrevencion",
                principalColumn: "AsesorPrevencionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pase_AsesorPrevencion_AsesorPrevencionId",
                table: "Pase");

            migrationBuilder.DropIndex(
                name: "IX_Pase_AsesorPrevencionId",
                table: "Pase");

            migrationBuilder.DropColumn(
                name: "AsesorPrevencionId",
                table: "Pase");

            migrationBuilder.AddColumn<Guid>(
                name: "PaseId",
                table: "AsesorPrevencion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
