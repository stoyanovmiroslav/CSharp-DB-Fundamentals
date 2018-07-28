using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealer.Data.Migrations
{
    public partial class ChangeEtityName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInporter",
                table: "Suppliers",
                newName: "IsImporter");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsImporter",
                table: "Suppliers",
                newName: "IsInporter");
        }
    }
}
