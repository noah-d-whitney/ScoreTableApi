namespace ScoreTableApi.Models;

public class Player
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Number { get; set; }
    public IList<PlayerStatline>? PlayerStatlines { get; set; }
    public IList<Team>? Teams { get; set; }
}