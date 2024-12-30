using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class logs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogJson",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Logs",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "LogJson");
        }
    }
}
