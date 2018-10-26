using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedSiteArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Polygon>(
                name: "Area",
                table: "Sites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "Sites");
        }
    }
}
