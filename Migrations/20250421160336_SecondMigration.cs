using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProLearnDB.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorrectAnswers",
                columns: table => new
                {
                    CorrectAnswerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Answer = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrectAnswers", x => x.CorrectAnswerId);
                });

            migrationBuilder.CreateTable(
                name: "TestTitles",
                columns: table => new
                {
                    TestTitleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTitles", x => x.TestTitleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TestTitleId = table.Column<int>(type: "integer", nullable: false),
                    CorrectAnswerId = table.Column<int>(type: "integer", nullable: false),
                    Issue = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IssueChoice1 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IssueChoice2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IssueChoice3 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IssueChoice4 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_CorrectAnswers_CorrectAnswerId",
                        column: x => x.CorrectAnswerId,
                        principalTable: "CorrectAnswers",
                        principalColumn: "CorrectAnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_TestTitles_TestTitleId",
                        column: x => x.TestTitleId,
                        principalTable: "TestTitles",
                        principalColumn: "TestTitleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgresses",
                columns: table => new
                {
                    UserProgressId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TestTitleId = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgresses", x => x.UserProgressId);
                    table.ForeignKey(
                        name: "FK_UserProgresses_TestTitles_TestTitleId",
                        column: x => x.TestTitleId,
                        principalTable: "TestTitles",
                        principalColumn: "TestTitleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CorrectAnswerId",
                table: "Questions",
                column: "CorrectAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestTitleId",
                table: "Questions",
                column: "TestTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_TestTitleId",
                table: "UserProgresses",
                column: "TestTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgresses_UserId",
                table: "UserProgresses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "UserProgresses");

            migrationBuilder.DropTable(
                name: "CorrectAnswers");

            migrationBuilder.DropTable(
                name: "TestTitles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
