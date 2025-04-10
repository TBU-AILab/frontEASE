using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateAccessOptimizationIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Resources_CountryCodeID",
                schema: "App",
                table: "Resources");

            migrationBuilder.CreateIndex(
                name: "IX_Task_DateCreated",
                schema: "Data",
                table: "Tasks",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Task_DateUpdated",
                schema: "Data",
                table: "Tasks",
                column: "DateUpdated");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IsDeleted",
                schema: "Data",
                table: "Tasks",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IsDeleted_State",
                schema: "Data",
                table: "Tasks",
                columns: new[] { "IsDeleted", "State" });

            migrationBuilder.CreateIndex(
                name: "IX_Task_State",
                schema: "Data",
                table: "Tasks",
                column: "State");

            migrationBuilder.CreateIndex(
                name: "IX_Task_State_DateCreated",
                schema: "Data",
                table: "Tasks",
                columns: new[] { "State", "DateCreated" });

            migrationBuilder.CreateIndex(
                name: "IX_Resource_CountryCodeID_ResourceCode",
                schema: "App",
                table: "Resources",
                columns: new[] { "CountryCodeID", "ResourceCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Company_IsDeleted",
                schema: "Auth",
                table: "Companies",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Task_DateCreated",
                schema: "Data",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Task_DateUpdated",
                schema: "Data",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Task_IsDeleted",
                schema: "Data",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Task_IsDeleted_State",
                schema: "Data",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Task_State",
                schema: "Data",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Task_State_DateCreated",
                schema: "Data",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Resource_CountryCodeID_ResourceCode",
                schema: "App",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Company_IsDeleted",
                schema: "Auth",
                table: "Companies");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CountryCodeID",
                schema: "App",
                table: "Resources",
                column: "CountryCodeID");
        }
    }
}
