using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddingInferredChain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InferredChainFromSelf",
                table: "Sites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InferredChainFromUserName",
                table: "Sites",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InferredFromUserClientId",
                table: "People",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_InferredFromUserClientId",
                table: "People",
                column: "InferredFromUserClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Clients_InferredFromUserClientId",
                table: "People",
                column: "InferredFromUserClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Clients_InferredFromUserClientId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_InferredFromUserClientId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "InferredChainFromSelf",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "InferredChainFromUserName",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "InferredFromUserClientId",
                table: "People");
        }
    }
}
