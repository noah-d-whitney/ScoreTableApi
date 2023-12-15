using System.ComponentModel.DataAnnotations;

namespace ScoreTableApi.Dto;

public class UserDto : UserLoginDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}