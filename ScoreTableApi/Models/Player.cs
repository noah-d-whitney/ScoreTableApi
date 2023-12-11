namespace ScoreTableApi.Models;

public class Player
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Number { get; set; }
    public DateTime CreatedDate { get; set; }
    public string[] TeamIds { get; set; }
    public string[] GameIds { get; set; }
    public string[] TournamentIds { get; set; }
    public string[] StatlineIds { get; set; }
}