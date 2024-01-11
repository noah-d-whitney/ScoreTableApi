using ScoreTableApi.Dto;
using ScoreTableApi.Dto.User;

namespace ScoreTableApi.Services;

public interface IAuthManager
{
    Task<bool> ValidateUser(UserLoginDto userDto);
    Task<string> CreateToken();
}