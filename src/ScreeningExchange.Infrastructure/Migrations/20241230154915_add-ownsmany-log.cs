using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addownsmanylog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logs_Capacity",
                schema: "screeningexchange",
                table: "LinkDispatcher");

            migrationBuilder.AddColumn<string>(
                name: "Logs",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logs",
                schema: "screeningexchange",
                table: "LinkDispatcher");

            migrationBuilder.AddColumn<int>(
                name: "Logs_Capacity",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
