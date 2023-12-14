using System.Collections;

namespace ScoreTableApi.Dto;

public class GameTeamDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<PlayerDto> Players { get; set; }
}