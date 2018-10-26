using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedGeographicLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RiskAssessments_SiteInspectionId",
                table: "RiskAssessments");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Sites",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Sites",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "PersonTypesMap",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PersonTypesMap",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessments_SiteInspectionId",
                table: "RiskAssessments",
                column: "SiteInspectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RiskAssessments_SiteInspectionId",
                table: "RiskAssessments");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Sites");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Sites",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "PersonTypesMap",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PersonTypesMap",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessments_SiteInspectionId",
                table: "RiskAssessments",
                column: "SiteInspectionId",
                unique: true);
        }
    }
}
