using ScoreTableApi.Dto.Statline;

namespace ScoreTableApi.Dto.Game;

public class GameSummaryDto
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public int PeriodCount { get; set; }
    public int PeriodLength { get; set; }
    public GameFormatDto GameFormat { get; set; }
    public GameStatusDto GameStatus { get; set; }
    public List<GameTeamDto> Teams { get; set; }
    public List<TeamStatlineDto> TeamStats { get; set; }
}