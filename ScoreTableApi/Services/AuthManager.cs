using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using ScoreTableApi.Dto;
using ScoreTableApi.Models;

namespace ScoreTableApi.Services;

public class AuthManager : IAuthManager
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthManager(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<bool> ValidateUser(UserLoginDto userDto)
    {
        var user = await _userManager.FindByNameAsync(userDto.Email);
        return user != null && await _userManager.CheckPasswordAsync(user, userDto.Password);
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}