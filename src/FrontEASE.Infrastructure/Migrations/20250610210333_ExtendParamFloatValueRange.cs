using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FrontEASE.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExtendParamFloatValueRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "FloatValue",
                schema: "Data",
                table: "TaskModuleParameterValues",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FloatValue",
                schema: "Data",
                table: "TaskModuleParameterValues",
                type: "real",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
