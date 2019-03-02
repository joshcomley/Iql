using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedInferredWhenKeyChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InferredWhenKeyChanges",
                table: "People",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InferredWhenKeyChanges",
                table: "People");
        }
    }
}
