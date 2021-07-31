using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "PersonaExterna");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonaExterna",
                table: "Persona",
                nullable: true,
                oldClrType: typeof(Guid));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonaId",
                table: "PersonaExterna",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonaExterna",
                table: "Persona",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
