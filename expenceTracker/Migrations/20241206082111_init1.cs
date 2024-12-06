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
                    expenceId = table.Column<int>(type: "int", nullable: false),
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
                name: "expectedExpence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    predictedCost = table.Column<double>(type: "float", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    expenceId = table.Column<int>(type: "int", nullable: false),
                    dateDue = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expectedExpence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expectedExpence_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "monthlyExpence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monthlyExpence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_monthlyExpence_Users_userId",
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
                name: "IX_expectedExpence_userId",
                table: "expectedExpence",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_monthlyExpence_userId",
                table: "monthlyExpence",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actualExpences");

            migrationBuilder.DropTable(
                name: "expectedExpence");

            migrationBuilder.DropTable(
                name: "monthlyExpence");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
