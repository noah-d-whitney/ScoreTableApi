using System.ComponentModel.DataAnnotations.Schema;

namespace ScoreTableApi.Models;

public class Game
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }

    public int TeamHomeId { get; set; }
    public Team TeamHome { get; set; }

    public int TeamAwayId { get; set; }
    public Team TeamAway { get; set; }

    public List<PlayerStatline> PlayerStatlines { get; set; }

    public int PeriodCount { get; set; }
    public int PeriodLength { get; set; }

    public int GameStatusId { get; set; }
    public GameStatus GameStatus { get; set; }

    public int GameFormatId { get; set; }
    public GameFormat GameFormat { get; set; }
}