using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_Phone_Value",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Customer_Phone");

            migrationBuilder.RenameColumn(
                name: "Customer_Name_Value",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Customer_Name");

            migrationBuilder.RenameColumn(
                name: "Customer_Email_Value",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Customer_Email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer_Phone",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Customer_Phone_Value");

            migrationBuilder.RenameColumn(
                name: "Customer_Name",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Customer_Name_Value");

            migrationBuilder.RenameColumn(
                name: "Customer_Email",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                newName: "Customer_Email_Value");
        }
    }
}
