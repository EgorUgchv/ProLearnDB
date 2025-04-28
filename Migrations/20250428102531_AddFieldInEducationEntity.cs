using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProLearnDB.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInEducationEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Education",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Education");
        }
    }
}
