using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskDataGridColumnPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPreferenceTaskDataGridColumns",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    GeneralOptionsID = table.Column<Guid>(type: "uuid", nullable: false),
                    Column = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferenceTaskDataGridColumns", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPreferenceTaskDataGridColumns_UserPreferenceGeneralOpti~",
                        column: x => x.GeneralOptionsID,
                        principalSchema: "Data",
                        principalTable: "UserPreferenceGeneralOptions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferenceTaskDataGridColumns_GeneralOptionsID",
                schema: "Data",
                table: "UserPreferenceTaskDataGridColumns",
                column: "GeneralOptionsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPreferenceTaskDataGridColumns",
                schema: "Data");
        }
    }
}
