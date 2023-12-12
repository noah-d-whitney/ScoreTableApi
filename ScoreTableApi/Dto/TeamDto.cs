using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class TeamDto : CreateTeamDto
{
    public int Id { get; set; }
    public List<Game> Games { get; set; }
}