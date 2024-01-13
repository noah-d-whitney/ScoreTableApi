using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoreTableApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamIdToPlayerStatlines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a456ed37-0b1b-4054-9d3d-3541c5c5c13f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1bf34d5-a62b-479e-85e9-6b8e550e6498");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PlayerStatlines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a4116da-5d3a-4dcb-b6cd-6025c3f2e16d", null, "Administrator", "ADMINISTRATOR" },
                    { "1239e2f3-24ae-419c-9623-ace8836fb6d8", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a4116da-5d3a-4dcb-b6cd-6025c3f2e16d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1239e2f3-24ae-419c-9623-ace8836fb6d8");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PlayerStatlines");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a456ed37-0b1b-4054-9d3d-3541c5c5c13f", null, "User", "USER" },
                    { "c1bf34d5-a62b-479e-85e9-6b8e550e6498", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
