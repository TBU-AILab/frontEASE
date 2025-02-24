using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoP_IMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserPreferenceGeneralOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GeneralOptionsID",
                schema: "Data",
                table: "UserPreferences",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserPreferenceGeneralOptions",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    ColorScheme = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferenceGeneralOptions", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferences_GeneralOptionsID",
                schema: "Data",
                table: "UserPreferences",
                column: "GeneralOptionsID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_UserPreferenceGeneralOptions_GeneralOptions~",
                schema: "Data",
                table: "UserPreferences",
                column: "GeneralOptionsID",
                principalSchema: "Data",
                principalTable: "UserPreferenceGeneralOptions",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_UserPreferenceGeneralOptions_GeneralOptions~",
                schema: "Data",
                table: "UserPreferences");

            migrationBuilder.DropTable(
                name: "UserPreferenceGeneralOptions",
                schema: "Data");

            migrationBuilder.DropIndex(
                name: "IX_UserPreferences_GeneralOptionsID",
                schema: "Data",
                table: "UserPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralOptionsID",
                schema: "Data",
                table: "UserPreferences");
        }
    }
}
