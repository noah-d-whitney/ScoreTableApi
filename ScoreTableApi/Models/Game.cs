namespace ScoreTableApi.Models;

public class Game
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime DateTime { get; set; }
    public ICollection<Team> Teams { get; set; }
    public int PeriodCount { get; set; }
    public int PeriodLength { get; set; }
    public int GameStatusId { get; set; }
    public GameStatus GameStatus { get; set; }
    public int GameFormatId { get; set; }
    public GameFormat GameFormat { get; set; }
}