namespace ScoreTableApi.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Player> Players { get; set; }
    public IList<Game> Games { get; set; }
}