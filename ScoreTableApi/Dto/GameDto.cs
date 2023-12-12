using System.ComponentModel.DataAnnotations;
using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class GameDto : CreateGameDto
{
    public int Id { get; set; }
    public List<PlayerStatline> PlayerStatlines { get; set; }
    public GameStatus GameStatus { get; set; }
    public GameFormat GameFormat { get; set; }
}