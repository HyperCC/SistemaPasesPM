using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class BaseDeDatosCompletaV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "AspNetUsers",
                newName: "UId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UId",
                table: "AspNetUsers",
                column: "UId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UId",
                table: "AspNetUsers",
                newName: "UsuarioId");
        }
    }
}
