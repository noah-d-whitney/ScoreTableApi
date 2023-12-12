using System.ComponentModel.DataAnnotations;
using ScoreTableApi.Models;

namespace ScoreTableApi.Dto;

public class CreateGameDto
{
    [Required]
    public DateTime DateTime { get; set; }

    [Required]
    [Range(1,4, ErrorMessage = "Period count must be between 1 and 4")]
    public int PeriodCount { get; set; }

    [Required]
    [Range(1,12, ErrorMessage = "Period length must be between 1 and 12")]
    public int PeriodLength { get; set; }

    [Required]
    [Length(2, 2,  ErrorMessage = "A game must be assigned two teams")]
    public List<int> TeamIds { get; set; }

    [Required]
    public int GameFormatId { get; set; }
}