using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addlinkdispatchers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendToEmail",
                schema: "screeningexchange",
                table: "BuildQuestion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendToWhatsApp",
                schema: "screeningexchange",
                table: "BuildQuestion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LinkDispatcher",
                schema: "screeningexchange",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    BuildQuestionId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: false),
                    SendToEmail = table.Column<bool>(type: "bit", nullable: false),
                    SendToWhatsApp = table.Column<bool>(type: "bit", nullable: false),
                    EmailSentSuccess = table.Column<bool>(type: "bit", nullable: false),
                    WhatsappSentSuccess = table.Column<bool>(type: "bit", nullable: false),
                    WasRead = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Customer_Email_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer_Phone_Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logs_Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkDispatcher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkDispatcher_BuildQuestion_BuildQuestionId",
                        column: x => x.BuildQuestionId,
                        principalSchema: "screeningexchange",
                        principalTable: "BuildQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkDispatcher_BuildQuestionId",
                schema: "screeningexchange",
                table: "LinkDispatcher",
                column: "BuildQuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkDispatcher",
                schema: "screeningexchange");

            migrationBuilder.DropColumn(
                name: "SendToEmail",
                schema: "screeningexchange",
                table: "BuildQuestion");

            migrationBuilder.DropColumn(
                name: "SendToWhatsApp",
                schema: "screeningexchange",
                table: "BuildQuestion");
        }
    }
}
