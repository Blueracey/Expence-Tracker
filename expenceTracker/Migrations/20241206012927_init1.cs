using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expenceTracker.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "actualExpences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    finalCost = table.Column<double>(type: "float", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false),
                    expenceID = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    datePayed = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actualExpences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_actualExpences_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expenceMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    predictedCost = table.Column<double>(type: "float", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    dateDue = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenceMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expenceMonths_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "expenceRecurringAndVariables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expenceRecurringAndVariables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expenceRecurringAndVariables_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_actualExpences_userId",
                table: "actualExpences",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_expenceMonths_userId",
                table: "expenceMonths",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_expenceRecurringAndVariables_userId",
                table: "expenceRecurringAndVariables",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actualExpences");

            migrationBuilder.DropTable(
                name: "expenceMonths");

            migrationBuilder.DropTable(
                name: "expenceRecurringAndVariables");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
