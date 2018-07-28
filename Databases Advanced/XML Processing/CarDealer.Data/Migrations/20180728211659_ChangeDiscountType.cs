using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealer.Data.Migrations
{
    public partial class ChangeDiscountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Sales",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
