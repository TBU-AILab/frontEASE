using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTokenVisibilityPreference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TokenVisibility",
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
                name: "TokenVisibility",
                schema: "Data",
                table: "UserPreferenceGeneralOptions");
        }
    }
}
