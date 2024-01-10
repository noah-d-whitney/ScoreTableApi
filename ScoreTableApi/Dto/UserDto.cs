using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ScoreTableApi.Dto;

public class UserDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [StringLength(15, ErrorMessage = "Your password is limited from {2} to {1} characters", MinimumLength = 5)]
    public string Password { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    public ICollection<string> Roles { get; set; }
}