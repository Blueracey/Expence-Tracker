using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expenceTracker.Migrations
{
    /// <inheritdoc />
    public partial class expectedExpenseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateDue",
                table: "expectedExpence");

            migrationBuilder.AddColumn<string>(
                name: "frequency",
                table: "expectedExpence",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "expectedExpence",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_expectedExpence_expenceId",
                table: "expectedExpence",
                column: "expenceId");

            migrationBuilder.CreateIndex(
                name: "IX_expectedExpence_userId",
                table: "expectedExpence",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_expectedExpence_Users_userId",
                table: "expectedExpence",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expectedExpence_monthlyExpence_expenceId",
                table: "expectedExpence",
                column: "expenceId",
                principalTable: "monthlyExpence",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_expectedExpence_Users_userId",
                table: "expectedExpence");

            migrationBuilder.DropForeignKey(
                name: "FK_expectedExpence_monthlyExpence_expenceId",
                table: "expectedExpence");

            migrationBuilder.DropIndex(
                name: "IX_expectedExpence_expenceId",
                table: "expectedExpence");

            migrationBuilder.DropIndex(
                name: "IX_expectedExpence_userId",
                table: "expectedExpence");

            migrationBuilder.DropColumn(
                name: "frequency",
                table: "expectedExpence");

            migrationBuilder.DropColumn(
                name: "type",
                table: "expectedExpence");

            migrationBuilder.AddColumn<DateOnly>(
                name: "dateDue",
                table: "expectedExpence",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
