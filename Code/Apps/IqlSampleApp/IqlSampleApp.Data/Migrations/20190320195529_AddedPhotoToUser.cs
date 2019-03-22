using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedPhotoToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoRevisionKey",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoRevisionKey",
                table: "People");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "People");
        }
    }
}
