using ScoreTableApi.Models;

namespace ScoreTableApi.Services;

public interface IUserService
{
    Task<User> GetUserData();
    string GetUserId();
}