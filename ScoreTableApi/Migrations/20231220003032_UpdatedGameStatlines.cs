using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScoreTableApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGameStatlines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameFormat_GameFormatId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameFormat",
                table: "GameFormat");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0798ae4b-13ab-423e-bd28-cff382076212");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4229747-4c9a-4d91-8f21-11e4caf6f0f6");

            migrationBuilder.RenameTable(
                name: "GameFormat",
                newName: "GameFormats");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameFormats",
                table: "GameFormats",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8f673568-1924-4680-b0aa-ef4095acbc72", null, "User", "USER" },
                    { "c8eb57fe-ec03-47c9-9e29-8ba5e8f86d92", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameFormats_GameFormatId",
                table: "Games",
                column: "GameFormatId",
                principalTable: "GameFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameFormats_GameFormatId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameFormats",
                table: "GameFormats");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f673568-1924-4680-b0aa-ef4095acbc72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8eb57fe-ec03-47c9-9e29-8ba5e8f86d92");

            migrationBuilder.RenameTable(
                name: "GameFormats",
                newName: "GameFormat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameFormat",
                table: "GameFormat",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0798ae4b-13ab-423e-bd28-cff382076212", null, "User", "USER" },
                    { "d4229747-4c9a-4d91-8f21-11e4caf6f0f6", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameFormat_GameFormatId",
                table: "Games",
                column: "GameFormatId",
                principalTable: "GameFormat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
