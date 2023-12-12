using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class PlayerDto : CreatePlayerDto
{
    public int Id { get; set; }
    public IList<PlayerStatlineDto>? PlayerStatlines { get; set; }
    public IList<TeamDto>? Teams { get; set; }
}