using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScoreTableApi.Models;

namespace ScoreTableApi.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly UserManager<User> _userManager;

    public UserService(IHttpContextAccessor accessor, UserManager<User> userManager)
    {
        _accessor = accessor;
        _userManager = userManager;
    }

    public async Task<User> GetUserData()
    {
        var id = GetUserId();
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        return user!;
    }

    public string GetUserId()
    {
        var result = string.Empty;
        if (_accessor.HttpContext is not null)
        {
            result = _userManager.GetUserId(_accessor.HttpContext.User);
        }
        return result!;
    }
}