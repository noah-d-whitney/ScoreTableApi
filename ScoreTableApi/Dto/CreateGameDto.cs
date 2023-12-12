using System.ComponentModel.DataAnnotations;
using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class CreateGameDto
{
    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    [Range(1,4)]
    public int PeriodCount { get; set; }

    [Required]
    [Range(1,12)]
    public int PeriodLength { get; set; }

    [Required]
    [MaxLength(2)]
    public List<Team> Teams { get; set; }

    [Required]
    public GameFormat GameFormat { get; set; }
}