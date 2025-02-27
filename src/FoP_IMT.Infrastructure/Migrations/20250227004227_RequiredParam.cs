using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoP_IMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequiredParam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_UserPreferenceGeneralOptions_GeneralOptions~",
                schema: "Data",
                table: "UserPreferences");

            migrationBuilder.AlterColumn<Guid>(
                name: "GeneralOptionsID",
                schema: "Data",
                table: "UserPreferences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_UserPreferenceGeneralOptions_GeneralOptions~",
                schema: "Data",
                table: "UserPreferences",
                column: "GeneralOptionsID",
                principalSchema: "Data",
                principalTable: "UserPreferenceGeneralOptions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPreferences_UserPreferenceGeneralOptions_GeneralOptions~",
                schema: "Data",
                table: "UserPreferences");

            migrationBuilder.AlterColumn<Guid>(
                name: "GeneralOptionsID",
                schema: "Data",
                table: "UserPreferences",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPreferences_UserPreferenceGeneralOptions_GeneralOptions~",
                schema: "Data",
                table: "UserPreferences",
                column: "GeneralOptionsID",
                principalSchema: "Data",
                principalTable: "UserPreferenceGeneralOptions",
                principalColumn: "ID");
        }
    }
}
