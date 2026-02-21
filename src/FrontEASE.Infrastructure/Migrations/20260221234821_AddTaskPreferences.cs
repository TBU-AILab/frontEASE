using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPreferenceTags",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    UserPreferencesID = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferenceTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPreferenceTags_UserPreferences_UserPreferencesID",
                        column: x => x.UserPreferencesID,
                        principalSchema: "Data",
                        principalTable: "UserPreferences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskUserPreferenceTagOption",
                schema: "Data",
                columns: table => new
                {
                    TagsID = table.Column<Guid>(type: "uuid", nullable: false),
                    TasksID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUserPreferenceTagOption", x => new { x.TagsID, x.TasksID });
                    table.ForeignKey(
                        name: "FK_TaskUserPreferenceTagOption_Tasks_TasksID",
                        column: x => x.TasksID,
                        principalSchema: "Data",
                        principalTable: "Tasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUserPreferenceTagOption_UserPreferenceTags_TagsID",
                        column: x => x.TagsID,
                        principalSchema: "Data",
                        principalTable: "UserPreferenceTags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskUserPreferenceTagOption_TasksID",
                schema: "Data",
                table: "TaskUserPreferenceTagOption",
                column: "TasksID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferenceTags_UserPreferencesID",
                schema: "Data",
                table: "UserPreferenceTags",
                column: "UserPreferencesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskUserPreferenceTagOption",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "UserPreferenceTags",
                schema: "Data");
        }
    }
}
