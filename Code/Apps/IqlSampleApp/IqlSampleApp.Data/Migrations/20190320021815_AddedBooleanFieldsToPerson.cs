using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedBooleanFieldsToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPaid",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "People",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPaid",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "People");
        }
    }
}
