using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addschool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeFinished",
                schema: "screeningexchange",
                table: "Destination",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "School",
                schema: "screeningexchange",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "School",
                schema: "screeningexchange");

            migrationBuilder.DropColumn(
                name: "DateTimeFinished",
                schema: "screeningexchange",
                table: "Destination");
        }
    }
}
