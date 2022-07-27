using Microsoft.EntityFrameworkCore.Migrations;

namespace Back_End_Final_Project.Migrations
{
    public partial class addCategoriesIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Infoormation",
                table: "ClothesInformations");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Clothes");

            migrationBuilder.AddColumn<string>(
                name: "Information",
                table: "ClothesInformations",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clothes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoiesId",
                table: "Clothes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Information",
                table: "ClothesInformations");

            migrationBuilder.DropColumn(
                name: "CategoiesId",
                table: "Clothes");

            migrationBuilder.AddColumn<string>(
                name: "Infoormation",
                table: "ClothesInformations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clothes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Clothes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
