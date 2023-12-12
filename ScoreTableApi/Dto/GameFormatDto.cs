using System.Collections;
using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class GameFormatDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<GameDto> Games { get; set; }
}