using System.ComponentModel.DataAnnotations;
using ScoreTableApi.Dto.Statline;
using ScoreTableApi.Dto.Team;

namespace ScoreTableApi.Dto.Player;

public class PlayerDto : CreatePlayerDto
{
    public int Id { get; set; }
    public List<PlayerStatlineDto>? PlayerStatlines { get; set; }
    public List<TeamDto>? Teams { get; set; }
}

public class CreatePlayerDto
{
    [Required]
    [StringLength(maximumLength: 20, ErrorMessage = "First name must be 20 characters or less")]
    public string FirstName { get; set; }

    [StringLength(maximumLength: 20, ErrorMessage = "Last name must be 20 characters or less")]
    public string? LastName { get; set; }

    [Range(0, 99, ErrorMessage = "Player number must be between 0-99")]
    public int? Number { get; set; }
}