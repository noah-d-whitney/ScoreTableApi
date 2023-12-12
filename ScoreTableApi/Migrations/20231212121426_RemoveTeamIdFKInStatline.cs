using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreTableApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTeamIdFKInStatline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "PlayerStatlines");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "PlayerStatlines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
