using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class json : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flows_Capacity",
                schema: "screeningexchange",
                table: "BuildQuestion");

            migrationBuilder.DropColumn(
                name: "Questions_Capacity",
                schema: "screeningexchange",
                table: "BuildQuestion");

            migrationBuilder.AddColumn<string>(
                name: "Flows",
                schema: "screeningexchange",
                table: "BuildQuestion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Questions",
                schema: "screeningexchange",
                table: "BuildQuestion",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flows",
                schema: "screeningexchange",
                table: "BuildQuestion");

            migrationBuilder.DropColumn(
                name: "Questions",
                schema: "screeningexchange",
                table: "BuildQuestion");

            migrationBuilder.AddColumn<int>(
                name: "Flows_Capacity",
                schema: "screeningexchange",
                table: "BuildQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Questions_Capacity",
                schema: "screeningexchange",
                table: "BuildQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
