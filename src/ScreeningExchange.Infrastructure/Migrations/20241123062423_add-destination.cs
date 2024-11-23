using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddestination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
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
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Destination",
                schema: "screeningexchange",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    StudentId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    BuildQuestionId = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    QuestionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Awnser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Destination_BuildQuestion_BuildQuestionId",
                        column: x => x.BuildQuestionId,
                        principalSchema: "screeningexchange",
                        principalTable: "BuildQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Destination_Student_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "screeningexchange",
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Destination_BuildQuestionId",
                schema: "screeningexchange",
                table: "Destination",
                column: "BuildQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Destination_StudentId",
                schema: "screeningexchange",
                table: "Destination",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destination",
                schema: "screeningexchange");

            migrationBuilder.DropTable(
                name: "Student",
                schema: "screeningexchange");
        }
    }
}
