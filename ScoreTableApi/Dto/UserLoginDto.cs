using System.ComponentModel.DataAnnotations;

namespace ScoreTableApi.Dto;

public class UserLoginDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "Your password is limited from {2} to {1} characters", MinimumLength = 5)]
    public string Password { get; set; }
}