using System.ComponentModel.DataAnnotations;

namespace ScoreTableApi.Dto;

public class GameDto
{
    public int Id { get; set; }
    public GameStatusDto GameStatus { get; set; }
    public DateTime DateTime { get; set; }
    public int PeriodCount { get; set; }
    public int PeriodLength { get; set; }
    public GameFormatDto GameFormat { get; set; }
    public List<GameTeamDto> Teams { get; set; }
}

public class GameSummaryDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int PeriodCount { get; set; }
    public int PeriodLength { get; set; }
    public string GameFormat { get; set; }
    public string GameStatus { get; set; }
    public List<GameTeamDto> Teams { get; set; }
    public List<GameTeamStatsDto> TeamStats { get; set; }
}

public class GameTeamStatsDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public int Score { get; set; }
}

public class CreateGameDto
{
    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    [Range(1,4, ErrorMessage = "Period count must be between 1 and 4")]
    public int PeriodCount { get; set; }

    [Required]
    [Range(1,20, ErrorMessage = "Period length must be between 1 and 20")]
    public int PeriodLength { get; set; }

    [Required]
    [Length(2, 2,  ErrorMessage = "A game must be assigned two teams")]
    public List<int> TeamIds { get; set; }

    [Required]
    public int GameFormatId { get; set; }
}