using System.ComponentModel.DataAnnotations;
using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class CreateTeamDto
{
    [Required]
    [StringLength(maximumLength: 50, ErrorMessage = "Team name must be 50 characters or less")]
    public string Name { get; set; }

    [Required]
    [Length(1, 15, ErrorMessage = "There can only be between 1 and 15 players on a team")]
    public IList<int> PlayerIds { get; set; }
}