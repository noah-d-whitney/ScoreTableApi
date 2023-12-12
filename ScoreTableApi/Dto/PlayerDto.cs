using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class PlayerDto : CreatePlayerDto
{
    public int Id { get; set; }
    public List<PlayerStatlineDto>? PlayerStatlines { get; set; }
    public List<TeamDto>? Teams { get; set; }
}