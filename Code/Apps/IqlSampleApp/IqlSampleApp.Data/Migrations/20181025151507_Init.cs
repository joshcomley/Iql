using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DCodeBrandlessIqlCodeAppsIqlSampleApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypes",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.UniqueConstraint("AK_Clients_Guid", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Clients_ClientTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ClientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: true),
                    FullName = table.Column<string>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCategories_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonLoadings",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLoadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonLoadings_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypes",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonTypes_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportCategories",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportCategories_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportDefaultRecommendations",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDefaultRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportDefaultRecommendations_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiskAssessmentQuestions",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAssessmentQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskAssessmentQuestions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Left = table.Column<int>(nullable: false),
                    Right = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sites_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sites_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sites_Sites_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false, defaultValueSql: "newid()"),
                    Key = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: true),
                    LoadingId = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.UniqueConstraint("AK_People_Guid", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_People_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_PersonLoadings_LoadingId",
                        column: x => x.LoadingId,
                        principalTable: "PersonLoadings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_People_PersonTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PersonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportTypes_ReportCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ReportCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportTypes_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiskAssessmentAnswers",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    SpecificHazard = table.Column<string>(nullable: true),
                    PrecautionsToControlHazard = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAssessmentAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskAssessmentAnswers_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiskAssessmentAnswers_RiskAssessmentQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "RiskAssessmentQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportReceiverEmailAddresses",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReceiverEmailAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportReceiverEmailAddresses_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportReceiverEmailAddresses_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteDocuments",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteDocuments_DocumentCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "DocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteDocuments_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteDocuments_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteInspections",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteInspections_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SiteInspections_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserSites",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SiteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSites", x => new { x.SiteId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserSites_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserSites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonTypesMap",
                columns: table => new
                {
                    Notes = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    Guid = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonTypesMap", x => new { x.PersonId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_PersonTypesMap_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonTypesMap_PersonTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PersonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonReports",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonReports_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonReports_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonReports_ReportTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PersonInspections",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    InspectionStatus = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    EndTime = table.Column<DateTimeOffset>(nullable: false),
                    ReasonForFailure = table.Column<int>(nullable: false),
                    SiteInspectionId = table.Column<int>(nullable: false),
                    IsDesignRequired = table.Column<bool>(nullable: false),
                    DrawingNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInspections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonInspections_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonInspections_SiteInspections_SiteInspectionId",
                        column: x => x.SiteInspectionId,
                        principalTable: "SiteInspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiskAssessments",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    SiteInspectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskAssessments_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiskAssessments_SiteInspections_SiteInspectionId",
                        column: x => x.SiteInspectionId,
                        principalTable: "SiteInspections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportActionsTaken",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    FaultReportId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportActionsTaken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportActionsTaken_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportActionsTaken_PersonReports_FaultReportId",
                        column: x => x.FaultReportId,
                        principalTable: "PersonReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportRecommendations",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    ReportId = table.Column<int>(nullable: false),
                    RecommendationId = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRecommendations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportRecommendations_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRecommendations_ReportDefaultRecommendations_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "ReportDefaultRecommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRecommendations_PersonReports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "PersonReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskAssessmentSolutions",
                columns: table => new
                {
                    PersistenceKey = table.Column<Guid>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    RevisionKey = table.Column<string>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Guid = table.Column<Guid>(nullable: false),
                    RiskAssessmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskAssessmentSolutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RiskAssessmentSolutions_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiskAssessmentSolutions_RiskAssessments_RiskAssessmentId",
                        column: x => x.RiskAssessmentId,
                        principalTable: "RiskAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientId",
                table: "AspNetUsers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedByUserId",
                table: "AspNetUsers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CreatedByUserId",
                table: "Clients",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TypeId",
                table: "Clients",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategories_CreatedByUserId",
                table: "DocumentCategories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_People_ClientId",
                table: "People",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_People_CreatedByUserId",
                table: "People",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_People_LoadingId",
                table: "People",
                column: "LoadingId");

            migrationBuilder.CreateIndex(
                name: "IX_People_TypeId",
                table: "People",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInspections_CreatedByUserId",
                table: "PersonInspections",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInspections_SiteInspectionId",
                table: "PersonInspections",
                column: "SiteInspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLoadings_CreatedByUserId",
                table: "PersonLoadings",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonReports_CreatedByUserId",
                table: "PersonReports",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonReports_PersonId",
                table: "PersonReports",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonReports_TypeId",
                table: "PersonReports",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTypes_CreatedByUserId",
                table: "PersonTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonTypesMap_TypeId",
                table: "PersonTypesMap",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedByUserId",
                table: "Projects",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportActionsTaken_CreatedByUserId",
                table: "ReportActionsTaken",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportActionsTaken_FaultReportId",
                table: "ReportActionsTaken",
                column: "FaultReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCategories_CreatedByUserId",
                table: "ReportCategories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDefaultRecommendations_CreatedByUserId",
                table: "ReportDefaultRecommendations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReceiverEmailAddresses_CreatedByUserId",
                table: "ReportReceiverEmailAddresses",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReceiverEmailAddresses_SiteId",
                table: "ReportReceiverEmailAddresses",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecommendations_CreatedByUserId",
                table: "ReportRecommendations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecommendations_RecommendationId",
                table: "ReportRecommendations",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRecommendations_ReportId",
                table: "ReportRecommendations",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTypes_CategoryId",
                table: "ReportTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportTypes_CreatedByUserId",
                table: "ReportTypes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessmentAnswers_CreatedByUserId",
                table: "RiskAssessmentAnswers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessmentAnswers_QuestionId",
                table: "RiskAssessmentAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessmentQuestions_CreatedByUserId",
                table: "RiskAssessmentQuestions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessments_CreatedByUserId",
                table: "RiskAssessments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessments_SiteInspectionId",
                table: "RiskAssessments",
                column: "SiteInspectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessmentSolutions_CreatedByUserId",
                table: "RiskAssessmentSolutions",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskAssessmentSolutions_RiskAssessmentId",
                table: "RiskAssessmentSolutions",
                column: "RiskAssessmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SiteDocuments_CategoryId",
                table: "SiteDocuments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteDocuments_CreatedByUserId",
                table: "SiteDocuments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteDocuments_SiteId",
                table: "SiteDocuments",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteInspections_CreatedByUserId",
                table: "SiteInspections",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteInspections_SiteId",
                table: "SiteInspections",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ClientId",
                table: "Sites",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_CreatedByUserId",
                table: "Sites",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_ParentId",
                table: "Sites",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSites_UserId",
                table: "UserSites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_CreatedByUserId",
                table: "Clients",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_CreatedByUserId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PersonInspections");

            migrationBuilder.DropTable(
                name: "PersonTypesMap");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ReportActionsTaken");

            migrationBuilder.DropTable(
                name: "ReportReceiverEmailAddresses");

            migrationBuilder.DropTable(
                name: "ReportRecommendations");

            migrationBuilder.DropTable(
                name: "RiskAssessmentAnswers");

            migrationBuilder.DropTable(
                name: "RiskAssessmentSolutions");

            migrationBuilder.DropTable(
                name: "SiteDocuments");

            migrationBuilder.DropTable(
                name: "UserSites");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ReportDefaultRecommendations");

            migrationBuilder.DropTable(
                name: "PersonReports");

            migrationBuilder.DropTable(
                name: "RiskAssessmentQuestions");

            migrationBuilder.DropTable(
                name: "RiskAssessments");

            migrationBuilder.DropTable(
                name: "DocumentCategories");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "SiteInspections");

            migrationBuilder.DropTable(
                name: "PersonLoadings");

            migrationBuilder.DropTable(
                name: "PersonTypes");

            migrationBuilder.DropTable(
                name: "ReportCategories");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ClientTypes");
        }
    }
}
