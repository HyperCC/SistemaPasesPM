using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistencia.Migrations
{
    public partial class DBCompleteV17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMultiple",
                table: "TipoDocumento",
                newName: "IsUnique");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUnique",
                table: "TipoDocumento",
                newName: "IsMultiple");
        }
    }
}
