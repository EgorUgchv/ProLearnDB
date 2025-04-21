using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLearnDB.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswer",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerId",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CorrectAnswerId",
                table: "Questions",
                column: "CorrectAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestTitleId",
                table: "Questions",
                column: "TestTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_CorrectAnswers_CorrectAnswerId",
                table: "Questions",
                column: "CorrectAnswerId",
                principalTable: "CorrectAnswers",
                principalColumn: "CorrectAnswerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_TestTitles_TestTitleId",
                table: "Questions",
                column: "TestTitleId",
                principalTable: "TestTitles",
                principalColumn: "TestTitleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_CorrectAnswers_CorrectAnswerId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_TestTitles_TestTitleId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CorrectAnswerId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestTitleId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CorrectAnswerId",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "CorrectAnswer",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
