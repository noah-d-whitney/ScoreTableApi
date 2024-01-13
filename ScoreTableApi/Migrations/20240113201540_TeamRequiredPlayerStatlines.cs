using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoreTableApi.Migrations
{
    /// <inheritdoc />
    public partial class TeamRequiredPlayerStatlines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatlines_Teams_TeamId",
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
                    { "2276a1eb-9a32-476f-998a-89a3cbf79eac", null, "User", "USER" },
                    { "c0c537bd-f583-4c6a-9163-288d513d7946", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatlines_Teams_TeamId",
                table: "PlayerStatlines",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatlines_Teams_TeamId",
                table: "PlayerStatlines");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2276a1eb-9a32-476f-998a-89a3cbf79eac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0c537bd-f583-4c6a-9163-288d513d7946");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatlines_Teams_TeamId",
                table: "PlayerStatlines",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
