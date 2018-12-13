using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedSiteKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Sites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "Sites");
        }
    }
}
