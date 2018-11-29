using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class AddedFullAddressToSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "Sites",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteAreaId",
                table: "People",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "People",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SiteAreas",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteAreas_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteAreas_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_People_SiteAreaId",
                table: "People",
                column: "SiteAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_People_SiteId",
                table: "People",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAreas_CreatedByUserId",
                table: "SiteAreas",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteAreas_SiteId",
                table: "SiteAreas",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_SiteAreas_SiteAreaId",
                table: "People",
                column: "SiteAreaId",
                principalTable: "SiteAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_People_Sites_SiteId",
                table: "People",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_SiteAreas_SiteAreaId",
                table: "People");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Sites_SiteId",
                table: "People");

            migrationBuilder.DropTable(
                name: "SiteAreas");

            migrationBuilder.DropIndex(
                name: "IX_People_SiteAreaId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_SiteId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "SiteAreaId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "People");
        }
    }
}
