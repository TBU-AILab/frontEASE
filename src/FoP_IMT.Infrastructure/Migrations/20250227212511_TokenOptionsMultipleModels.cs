using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoP_IMT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TokenOptionsMultipleModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectorType",
                schema: "Data",
                table: "UserPreferenceTokens");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "Data",
                table: "UserPreferenceTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "UserPreferenceTokenConnectorTypes",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    TokenOptionID = table.Column<Guid>(type: "uuid", nullable: false),
                    ConnectorType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPreferenceTokenConnectorTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserPreferenceTokenConnectorTypes_UserPreferenceTokens_Toke~",
                        column: x => x.TokenOptionID,
                        principalSchema: "Data",
                        principalTable: "UserPreferenceTokens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPreferenceTokenConnectorTypes_TokenOptionID",
                schema: "Data",
                table: "UserPreferenceTokenConnectorTypes",
                column: "TokenOptionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPreferenceTokenConnectorTypes",
                schema: "Data");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                schema: "Data",
                table: "UserPreferenceTokens",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "ConnectorType",
                schema: "Data",
                table: "UserPreferenceTokens",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
