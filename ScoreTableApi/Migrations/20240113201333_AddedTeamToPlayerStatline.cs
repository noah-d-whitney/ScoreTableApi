using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoreTableApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamToPlayerStatline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a4116da-5d3a-4dcb-b6cd-6025c3f2e16d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1239e2f3-24ae-419c-9623-ace8836fb6d8");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "PlayerStatlines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c1bf870-5394-494a-b1b0-fa606c3bf541", null, "User", "USER" },
                    { "55aa46b7-a468-40dd-a4d2-c8f1d0d698c2", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatlines_TeamId",
                table: "PlayerStatlines",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatlines_Teams_TeamId",
                table: "PlayerStatlines",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatlines_Teams_TeamId",
                table: "PlayerStatlines");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatlines_TeamId",
                table: "PlayerStatlines");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c1bf870-5394-494a-b1b0-fa606c3bf541");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55aa46b7-a468-40dd-a4d2-c8f1d0d698c2");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "PlayerStatlines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a4116da-5d3a-4dcb-b6cd-6025c3f2e16d", null, "Administrator", "ADMINISTRATOR" },
                    { "1239e2f3-24ae-419c-9623-ace8836fb6d8", null, "User", "USER" }
                });
        }
    }
}
