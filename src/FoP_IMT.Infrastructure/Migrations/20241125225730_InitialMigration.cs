using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoP_IMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Data");

            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.EnsureSchema(
                name: "App");

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    OrientationNumber = table.Column<string>(type: "text", nullable: true),
                    DescriptiveNumber = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: false),
                    ZIPCode = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryCodes",
                schema: "App",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCodes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                schema: "App",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    MimeType = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskConfigRepeatedMessages",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    RepeatedMessageType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskConfigRepeatedMessages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                name: "Resources",
                schema: "App",
                columns: table => new
                {
                    ResourceCode = table.Column<string>(type: "text", nullable: false),
                    CountryCodeID = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.ResourceCode);
                    table.ForeignKey(
                        name: "FK_Resources_CountryCodes_CountryCodeID",
                        column: x => x.CountryCodeID,
                        principalSchema: "App",
                        principalTable: "CountryCodes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Auth",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    AddressID = table.Column<Guid>(type: "uuid", nullable: true),
                    ImageID = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameAbbreviation = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "Data",
                        principalTable: "Addresses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Companies_Images_ImageID",
                        column: x => x.ImageID,
                        principalSchema: "App",
                        principalTable: "Images",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TaskConfigRepeatedMessageItems",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    RepeatedMessageID = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskConfigRepeatedMessageItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskConfigRepeatedMessageItems_TaskConfigRepeatedMessages_R~",
                        column: x => x.RepeatedMessageID,
                        principalSchema: "Data",
                        principalTable: "TaskConfigRepeatedMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskConfigurations",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    RepeatedMessageID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FeedbackFromSolution = table.Column<bool>(type: "boolean", nullable: false),
                    MaxContextSize = table.Column<int>(type: "integer", nullable: false),
                    SystemMessage = table.Column<string>(type: "text", nullable: true),
                    InitMessage = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskConfigurations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskConfigurations_TaskConfigRepeatedMessages_RepeatedMessa~",
                        column: x => x.RepeatedMessageID,
                        principalSchema: "Data",
                        principalTable: "TaskConfigRepeatedMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferenceTokens",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    UserPreferencesID = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ConnectorType = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferenceTokens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPreferenceTokens_UserPreferences_UserPreferencesID",
                        column: x => x.UserPreferencesID,
                        principalSchema: "Data",
                        principalTable: "UserPreferences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ImageID = table.Column<Guid>(type: "uuid", nullable: true),
                    UserPreferencesID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Images_ImageID",
                        column: x => x.ImageID,
                        principalSchema: "App",
                        principalTable: "Images",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_UserPreferences_UserPreferencesID",
                        column: x => x.UserPreferencesID,
                        principalSchema: "Data",
                        principalTable: "UserPreferences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskModules",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    TaskConfigID = table.Column<Guid>(type: "uuid", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    PackageType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModules", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskModules_TaskConfigurations_TaskConfigID",
                        column: x => x.TaskConfigID,
                        principalSchema: "Data",
                        principalTable: "TaskConfigurations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigID = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorID = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    CurrentIteration = table.Column<int>(type: "integer", nullable: false),
                    IterationsValid = table.Column<int>(type: "integer", nullable: false),
                    IterationsInvalidConsecutive = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskConfigurations_ConfigID",
                        column: x => x.ConfigID,
                        principalSchema: "Data",
                        principalTable: "TaskConfigurations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCompany",
                schema: "Auth",
                columns: table => new
                {
                    CompaniesID = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCompany", x => new { x.CompaniesID, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCompany_Companies_CompaniesID",
                        column: x => x.CompaniesID,
                        principalSchema: "Auth",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCompany_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskModuleParameterEnumValues",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    ModuleValueID = table.Column<Guid>(type: "uuid", nullable: true),
                    StringValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleParameterEnumValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskModuleParameterEnumValues_TaskModules_ModuleValueID",
                        column: x => x.ModuleValueID,
                        principalSchema: "Data",
                        principalTable: "TaskModules",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTask",
                columns: table => new
                {
                    MembersId = table.Column<string>(type: "text", nullable: false),
                    TasksID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTask", x => new { x.MembersId, x.TasksID });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTask_Tasks_TasksID",
                        column: x => x.TasksID,
                        principalSchema: "Data",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserTask_Users_MembersId",
                        column: x => x.MembersId,
                        principalSchema: "Auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTask",
                columns: table => new
                {
                    MemberGroupsID = table.Column<Guid>(type: "uuid", nullable: false),
                    TasksID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTask", x => new { x.MemberGroupsID, x.TasksID });
                    table.ForeignKey(
                        name: "FK_CompanyTask_Companies_MemberGroupsID",
                        column: x => x.MemberGroupsID,
                        principalSchema: "Auth",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyTask_Tasks_TasksID",
                        column: x => x.TasksID,
                        principalSchema: "Data",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMessages",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskID = table.Column<Guid>(type: "uuid", nullable: false),
                    Tokens = table.Column<int>(type: "integer", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Model = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskMessages_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalSchema: "Data",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskModuleParameterValues",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    EnumValueID = table.Column<Guid>(type: "uuid", nullable: true),
                    FloatValue = table.Column<float>(type: "real", nullable: true),
                    IntValue = table.Column<int>(type: "integer", nullable: true),
                    StringValue = table.Column<string>(type: "text", nullable: true),
                    BoolValue = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleParameterValues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskModuleParameterValues_TaskModuleParameterEnumValues_Enu~",
                        column: x => x.EnumValueID,
                        principalSchema: "Data",
                        principalTable: "TaskModuleParameterEnumValues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TaskSolutions",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskID = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskMessageID = table.Column<Guid>(type: "uuid", nullable: false),
                    Fitness = table.Column<float>(type: "real", nullable: true),
                    Feedback = table.Column<string>(type: "text", nullable: true),
                    Metadata = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskSolutions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskSolutions_TaskMessages_TaskMessageID",
                        column: x => x.TaskMessageID,
                        principalSchema: "Data",
                        principalTable: "TaskMessages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskSolutions_Tasks_TaskID",
                        column: x => x.TaskID,
                        principalSchema: "Data",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskModuleParameters",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    ModuleID = table.Column<Guid>(type: "uuid", nullable: false),
                    ValueID = table.Column<Guid>(type: "uuid", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleParameters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskModuleParameters_TaskModuleParameterValues_ValueID",
                        column: x => x.ValueID,
                        principalSchema: "Data",
                        principalTable: "TaskModuleParameterValues",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TaskModuleParameters_TaskModules_ModuleID",
                        column: x => x.ModuleID,
                        principalSchema: "Data",
                        principalTable: "TaskModules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCompany_UsersId",
                schema: "Auth",
                table: "ApplicationUserCompany",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserTask_TasksID",
                table: "ApplicationUserTask",
                column: "TasksID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressID",
                schema: "Auth",
                table: "Companies",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ImageID",
                schema: "Auth",
                table: "Companies",
                column: "ImageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTask_TasksID",
                table: "CompanyTask",
                column: "TasksID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CountryCodeID",
                schema: "App",
                table: "Resources",
                column: "CountryCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskConfigRepeatedMessageItems_RepeatedMessageID",
                schema: "Data",
                table: "TaskConfigRepeatedMessageItems",
                column: "RepeatedMessageID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskConfigurations_RepeatedMessageID",
                schema: "Data",
                table: "TaskConfigurations",
                column: "RepeatedMessageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskMessages_DateCreated",
                schema: "Data",
                table: "TaskMessages",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMessages_TaskID",
                schema: "Data",
                table: "TaskMessages",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameterEnumValues_ModuleValueID",
                schema: "Data",
                table: "TaskModuleParameterEnumValues",
                column: "ModuleValueID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameters_ModuleID",
                schema: "Data",
                table: "TaskModuleParameters",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameters_ValueID",
                schema: "Data",
                table: "TaskModuleParameters",
                column: "ValueID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameterValues_EnumValueID",
                schema: "Data",
                table: "TaskModuleParameterValues",
                column: "EnumValueID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModules_TaskConfigID",
                schema: "Data",
                table: "TaskModules",
                column: "TaskConfigID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ConfigID",
                schema: "Data",
                table: "Tasks",
                column: "ConfigID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskSolutions_TaskID",
                schema: "Data",
                table: "TaskSolutions",
                column: "TaskID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSolutions_TaskMessageID",
                schema: "Data",
                table: "TaskSolutions",
                column: "TaskMessageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferenceTokens_UserPreferencesID",
                schema: "Data",
                table: "UserPreferenceTokens",
                column: "UserPreferencesID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Auth",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ImageID",
                schema: "Auth",
                table: "Users",
                column: "ImageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPreferencesID",
                schema: "Auth",
                table: "Users",
                column: "UserPreferencesID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Auth",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCompany",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "ApplicationUserTask");

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
                name: "CompanyTask");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "App");

            migrationBuilder.DropTable(
                name: "TaskConfigRepeatedMessageItems",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskModuleParameters",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskSolutions",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "UserPreferenceTokens",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "CountryCodes",
                schema: "App");

            migrationBuilder.DropTable(
                name: "TaskModuleParameterValues",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskMessages",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "UserPreferences",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "Images",
                schema: "App");

            migrationBuilder.DropTable(
                name: "TaskModuleParameterEnumValues",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskModules",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskConfigurations",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskConfigRepeatedMessages",
                schema: "Data");
        }
    }
}
