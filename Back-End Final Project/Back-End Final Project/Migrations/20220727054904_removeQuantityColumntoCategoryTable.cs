using Microsoft.EntityFrameworkCore.Migrations;

namespace Back_End_Final_Project.Migrations
{
    public partial class removeQuantityColumntoCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Quantity",
                table: "Categories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
