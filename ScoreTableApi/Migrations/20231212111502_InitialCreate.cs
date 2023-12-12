using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScoreTableApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodCount = table.Column<int>(type: "int", nullable: false),
                    PeriodLength = table.Column<int>(type: "int", nullable: false),
                    GameStatusId = table.Column<int>(type: "int", nullable: false),
                    GameFormatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameFormat_GameFormatId",
                        column: x => x.GameFormatId,
                        principalTable: "GameFormat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_GameStatus_GameStatusId",
                        column: x => x.GameStatusId,
                        principalTable: "GameStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerTeam",
                columns: table => new
                {
                    PlayersId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerTeam", x => new { x.PlayersId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_PlayerTeam_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameTeam",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    TeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameTeam", x => new { x.GamesId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_GameTeam_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatlines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsStarter = table.Column<bool>(type: "bit", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Rebounds = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    Steals = table.Column<int>(type: "int", nullable: false),
                    Blocks = table.Column<int>(type: "int", nullable: false),
                    Fouls = table.Column<int>(type: "int", nullable: false),
                    Turnovers = table.Column<int>(type: "int", nullable: false),
                    Fga = table.Column<int>(type: "int", nullable: false),
                    Fgm = table.Column<int>(type: "int", nullable: false),
                    Fta = table.Column<int>(type: "int", nullable: false),
                    Ftm = table.Column<int>(type: "int", nullable: false),
                    Tpa = table.Column<int>(type: "int", nullable: false),
                    Tpm = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatlines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatlines_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerStatlines_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameFormatId",
                table: "Games",
                column: "GameFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GameStatusId",
                table: "Games",
                column: "GameStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_GameTeam_TeamsId",
                table: "GameTeam",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatlines_GameId",
                table: "PlayerStatlines",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatlines_PlayerId",
                table: "PlayerStatlines",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerTeam_TeamsId",
                table: "PlayerTeam",
                column: "TeamsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameTeam");

            migrationBuilder.DropTable(
                name: "PlayerStatlines");

            migrationBuilder.DropTable(
                name: "PlayerTeam");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "GameFormat");

            migrationBuilder.DropTable(
                name: "GameStatus");
        }
    }
}
