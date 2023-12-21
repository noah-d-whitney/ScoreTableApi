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