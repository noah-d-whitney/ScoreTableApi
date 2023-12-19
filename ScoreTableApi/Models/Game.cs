namespace ScoreTableApi.Models;

public partial class Game
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public int PeriodCount { get; set; }

    public int PeriodLength { get; set; }

    public int GameStatusId { get; set; }

    public int GameFormatId { get; set; }

    public virtual GameFormat GameFormat { get; set; } = null!;

    public virtual GameStatus GameStatus { get; set; } = null!;

    public virtual ICollection<PlayerStatline> PlayerStatlines { get; set; } = new List<PlayerStatline>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
