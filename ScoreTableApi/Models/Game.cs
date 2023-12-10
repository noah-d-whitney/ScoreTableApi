using ScoreTableApi.Models.Enums;

namespace ScoreTableApi.Models;

public class Game
{
    public string Id { get; set; }
    public DateTime DateTime { get; set; }
    public string Team1Id { get; set; }
    public string Team2Id { get; set; }
    public string? TournamentId { get; set; }
    public string? LeagueId { get; set; }
    public int PeriodCount { get; set; }
    public int PeriodLength { get; set; }
    public GameStatus GameStatus { get; set; }
    public GameFormat GameFormat { get; set; }
}