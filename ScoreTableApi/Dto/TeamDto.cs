using System.ComponentModel.DataAnnotations;
using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class TeamDto
{
    public string Name { get; set; }
    public List<GamePlayerDto> Players { get; set; }
    public int Id { get; set; }
    public List<GameDto> Games { get; set; }
}

public class CreateTeamDto
{
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Team name must be 50 characters or less")]
    public string Name { get; set; }

    [Required]
    [Length(1, 15, ErrorMessage = "There can only be between 1 and 15 players on a team")]
    public List<int> PlayerIds { get; set; }
}

public class GameTeamDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlayerDto> Players { get; set; }
}