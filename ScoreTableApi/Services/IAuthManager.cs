using ScoreTableApi.Dto;

namespace ScoreTableApi.Services;

public interface IAuthManager
{
    Task<bool> ValidateUser(UserLoginDto userDto);
    Task<string> CreateToken();
}