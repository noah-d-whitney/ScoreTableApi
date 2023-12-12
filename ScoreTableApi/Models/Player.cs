namespace ScoreTableApi.Models;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Number { get; set; }

    public List<PlayerStatline> PlayerStatlines { get; set; }
    public List<Team> Teams { get; set; }
}