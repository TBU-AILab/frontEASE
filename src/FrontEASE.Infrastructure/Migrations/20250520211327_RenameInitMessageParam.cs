using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameInitMessageParam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitMessage",
                schema: "Data",
                table: "TaskConfigurations",
                newName: "InitialMessage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InitialMessage",
                schema: "Data",
                table: "TaskConfigurations",
                newName: "InitMessage");
        }
    }
}
