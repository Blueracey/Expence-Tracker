using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace expenceTracker.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actualExpences_Users_userId",
                table: "actualExpences");

            migrationBuilder.DropForeignKey(
                name: "FK_expectedExpence_Users_userId",
                table: "expectedExpence");

            migrationBuilder.DropForeignKey(
                name: "FK_monthlyExpence_Users_userId",
                table: "monthlyExpence");

            migrationBuilder.DropIndex(
                name: "IX_monthlyExpence_userId",
                table: "monthlyExpence");

            migrationBuilder.DropIndex(
                name: "IX_expectedExpence_userId",
                table: "expectedExpence");

            migrationBuilder.DropIndex(
                name: "IX_actualExpences_userId",
                table: "actualExpences");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_monthlyExpence_userId",
                table: "monthlyExpence",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_expectedExpence_userId",
                table: "expectedExpence",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_actualExpences_userId",
                table: "actualExpences",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_actualExpences_Users_userId",
                table: "actualExpences",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_expectedExpence_Users_userId",
                table: "expectedExpence",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_monthlyExpence_Users_userId",
                table: "monthlyExpence",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
