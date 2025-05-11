using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModuleLinkNullability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModuleParameterListValueItems_TaskModuleParameterListVa~",
                schema: "Data",
                table: "TaskModuleParameterListValueItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskConfigID",
                schema: "Data",
                table: "TaskModules",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ModuleID",
                schema: "Data",
                table: "TaskModuleParameters",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModuleParameterListValueItems_TaskModuleParameterListVa~",
                schema: "Data",
                table: "TaskModuleParameterListValueItems",
                column: "ListParamValueID",
                principalSchema: "Data",
                principalTable: "TaskModuleParameterListValues",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskModuleParameterListValueItems_TaskModuleParameterListVa~",
                schema: "Data",
                table: "TaskModuleParameterListValueItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaskConfigID",
                schema: "Data",
                table: "TaskModules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ModuleID",
                schema: "Data",
                table: "TaskModuleParameters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskModuleParameterListValueItems_TaskModuleParameterListVa~",
                schema: "Data",
                table: "TaskModuleParameterListValueItems",
                column: "ListParamValueID",
                principalSchema: "Data",
                principalTable: "TaskModuleParameterListValues",
                principalColumn: "ID");
        }
    }
}
