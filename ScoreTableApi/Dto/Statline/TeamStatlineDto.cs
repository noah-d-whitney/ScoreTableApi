using ScoreTableApi.Dto.Game;

namespace ScoreTableApi.Dto.Statline;

public class TeamStatlineDto
{
    public int TeamId { get; set; }
    public string TeamName { get; set; }
    public int Score { get; set; }
    public int Rebounds { get; set; }
    public int Assists { get; set; }
    public int Steals { get; set; }
    public int Blocks { get; set; }
    public int Fouls { get; set; }
    public int Turnovers { get; set; }
    public int Fga { get; set; }
    public int Fgm { get; set; }
    public int Fta { get; set; }
    public int Ftm { get; set; }
    public int Tpa { get; set; }
    public int Tpm { get; set; }

    public TeamStatlineDto(string teamName, int teamId)
    {
        TeamId = teamId;
        TeamName = teamName;
        Score = 0;
        Rebounds = 0;
        Assists = 0;
        Steals = 0;
        Blocks = 0;
        Fouls = 0;
        Turnovers = 0;
        Fga = 0;
        Fgm = 0;
        Fta = 0;
        Ftm = 0;
        Tpa = 0;
        Tpm = 0;
    }
}