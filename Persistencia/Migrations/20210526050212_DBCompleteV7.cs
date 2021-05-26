using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Pase",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Pase",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServicioAdjudicado",
                table: "Pase",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaId",
                table: "Documento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonaExternaId",
                table: "Documento",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicioAdjudicado",
                table: "Pase");

            migrationBuilder.DropColumn(
                name: "EmpresaId",
                table: "Documento");

            migrationBuilder.DropColumn(
                name: "PersonaExternaId",
                table: "Documento");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "Pase",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Pase",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
