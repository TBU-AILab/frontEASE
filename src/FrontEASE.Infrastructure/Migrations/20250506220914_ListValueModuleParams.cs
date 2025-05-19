using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ListValueModuleParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ListValueID",
                schema: "Data",
                table: "TaskModuleParameterValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ListValueID",
                schema: "Data",
                table: "TaskModuleParameters",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskModuleParameterListValues",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleParameterListValues", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskModuleParameterListValueItems",
                schema: "Data",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid ()"),
                    ListParamValueID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskModuleParameterListValueItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskModuleParameterListValueItems_TaskModuleParameterListVa~",
                        column: x => x.ListParamValueID,
                        principalSchema: "Data",
                        principalTable: "TaskModuleParameterListValues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameterValues_ListValueID",
                schema: "Data",
                table: "TaskModuleParameterValues",
                column: "ListValueID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameters_ListValueID",
                schema: "Data",
                table: "TaskModuleParameters",
                column: "ListValueID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskModuleParameterListValueItems_ListParamValueID",
                schema: "Data",
                table: "TaskModuleParameterListValueItems",
                column: "ListParamValueID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModuleParameters_TaskModuleParameterListValueItems_List~",
                schema: "Data",
                table: "TaskModuleParameters",
                column: "ListValueID",
                principalSchema: "Data",
                principalTable: "TaskModuleParameterListValueItems",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModuleParameterValues_TaskModuleParameterListValues_Lis~",
                schema: "Data",
                table: "TaskModuleParameterValues",
                column: "ListValueID",
                principalSchema: "Data",
                principalTable: "TaskModuleParameterListValues",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModuleParameters_TaskModuleParameterListValueItems_List~",
                schema: "Data",
                table: "TaskModuleParameters");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskModuleParameterValues_TaskModuleParameterListValues_Lis~",
                schema: "Data",
                table: "TaskModuleParameterValues");

            migrationBuilder.DropTable(
                name: "TaskModuleParameterListValueItems",
                schema: "Data");

            migrationBuilder.DropTable(
                name: "TaskModuleParameterListValues",
                schema: "Data");

            migrationBuilder.DropIndex(
                name: "IX_TaskModuleParameterValues_ListValueID",
                schema: "Data",
                table: "TaskModuleParameterValues");

            migrationBuilder.DropIndex(
                name: "IX_TaskModuleParameters_ListValueID",
                schema: "Data",
                table: "TaskModuleParameters");

            migrationBuilder.DropColumn(
                name: "ListValueID",
                schema: "Data",
                table: "TaskModuleParameterValues");

            migrationBuilder.DropColumn(
                name: "ListValueID",
                schema: "Data",
                table: "TaskModuleParameters");
        }
    }
}
