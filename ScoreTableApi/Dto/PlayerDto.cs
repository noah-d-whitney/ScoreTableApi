using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class PlayerDto : CreatePlayerDto
{
    public int Id { get; set; }
    public List<PlayerStatline>? PlayerStatlines { get; set; }
    public List<Team>? Teams { get; set; }
}