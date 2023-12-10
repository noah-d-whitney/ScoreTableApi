namespace ScoreTableApi.Models;

public class Statline
{
    public string Id { get; set; }
    public string GameId { get; set; }
    public string PlayerId { get; set; }
    public int Points { get; set; } = 0;
    public int Rebounds { get; set; } = 0;
    public int Assists { get; set; } = 0;
    public int Steals { get; set; } = 0;
    public int Blocks { get; set; } = 0;
    public int Fouls { get; set; } = 0;
    public int Turnovers { get; set; } = 0;
    public int Fga { get; set; } = 0;
    public int Fgm { get; set; } = 0;
    public int Fta { get; set; } = 0;
    public int Ftm { get; set; } = 0;
    public int Tpa { get; set; } = 0;
    public int Tpm { get; set; } = 0;
}
