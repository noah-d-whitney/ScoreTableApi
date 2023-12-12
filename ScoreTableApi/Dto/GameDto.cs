using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class GameDto : CreateGameDto
{
    public int Id { get; set; }
    public List<PlayerStatlineDto> PlayerStatlines { get; set; }
    public GameStatusDto GameStatus { get; set; }
}