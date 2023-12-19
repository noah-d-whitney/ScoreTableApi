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

public class CreateGameDto
{
    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    [Range(1,4, ErrorMessage = "Period count must be between 1 and 4")]
    public int PeriodCount { get; set; }

    [Required]
    [Range(1,12, ErrorMessage = "Period length must be between 1 and 12")]
    public int PeriodLength { get; set; }

    [Required]
    [Length(2, 2, ErrorMessage = "You must assign two teams to a game")]
    public List<int> TeamIds { get; set; }

    public int GameFormatId { get; set; }
}