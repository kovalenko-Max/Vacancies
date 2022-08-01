using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddWage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vacancy",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WageFrom",
                table: "Vacancy",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WageTo",
                table: "Vacancy",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WageFrom",
                table: "Vacancy");

            migrationBuilder.DropColumn(
                name: "WageTo",
                table: "Vacancy");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Vacancy",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
