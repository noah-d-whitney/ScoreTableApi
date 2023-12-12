namespace ScoreTableApi.Models;

public class GameStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Game> Games { get; set; }
}