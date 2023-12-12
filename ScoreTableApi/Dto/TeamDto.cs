using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class TeamDto : CreateTeamDto
{
    public int Id { get; set; }
    public IList<GameDto> Games { get; set; }
}