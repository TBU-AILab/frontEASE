using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageRenderModePreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SystemMessageDisplayFormat",
                schema: "Data",
                table: "UserPreferenceGeneralOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserMessageDisplayFormat",
                schema: "Data",
                table: "UserPreferenceGeneralOptions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemMessageDisplayFormat",
                schema: "Data",
                table: "UserPreferenceGeneralOptions");

            migrationBuilder.DropColumn(
                name: "UserMessageDisplayFormat",
                schema: "Data",
                table: "UserPreferenceGeneralOptions");
        }
    }
}
