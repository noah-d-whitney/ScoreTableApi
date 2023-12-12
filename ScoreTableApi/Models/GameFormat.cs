namespace ScoreTableApi.Models;

public class GameFormat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<Game> Games { get; set; }
}