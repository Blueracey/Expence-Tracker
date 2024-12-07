using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expenceTracker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

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
