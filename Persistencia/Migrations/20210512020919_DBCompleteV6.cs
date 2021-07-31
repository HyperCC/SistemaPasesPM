using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pase_AspNetUsers_UsuarioRelId",
                table: "Pase");

            migrationBuilder.DropForeignKey(
                name: "FK_Rol_AspNetUsers_UsuarioRelId",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Rol_UsuarioRelId",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Pase_UsuarioRelId",
                table: "Pase");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsuarioRelId",
                table: "Rol");

            migrationBuilder.DropColumn(
                name: "UsuarioRelId",
                table: "Pase");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Rol",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Pase",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Rol_UsuarioId",
                table: "Rol",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Pase_UsuarioId",
                table: "Pase",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pase_AspNetUsers_UsuarioId",
                table: "Pase",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rol_AspNetUsers_UsuarioId",
                table: "Rol",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pase_AspNetUsers_UsuarioId",
                table: "Pase");

            migrationBuilder.DropForeignKey(
                name: "FK_Rol_AspNetUsers_UsuarioId",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Rol_UsuarioId",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Pase_UsuarioId",
                table: "Pase");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Rol",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioRelId",
                table: "Rol",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Pase",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioRelId",
                table: "Pase",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UId",
                table: "AspNetUsers",
                column: "UId");

            migrationBuilder.CreateIndex(
                name: "IX_Rol_UsuarioRelId",
                table: "Rol",
                column: "UsuarioRelId");

            migrationBuilder.CreateIndex(
                name: "IX_Pase_UsuarioRelId",
                table: "Pase",
                column: "UsuarioRelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pase_AspNetUsers_UsuarioRelId",
                table: "Pase",
                column: "UsuarioRelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rol_AspNetUsers_UsuarioRelId",
                table: "Rol",
                column: "UsuarioRelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
