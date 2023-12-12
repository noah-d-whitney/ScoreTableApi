namespace ScoreTableApi.Models;

public class GameStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Game> Games { get; set; }
}