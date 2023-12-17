using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ScoreTableApi.Dto;
using ScoreTableApi.Models;

namespace ScoreTableApi.Services;

public class AuthManager : IAuthManager
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private  User? _user;

    public AuthManager(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials
        signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var expiration = DateTime.Now.AddMinutes(
                Convert.ToDouble(jwtSettings.GetSection("Lifetime").Value));

        var token = new JwtSecurityToken(
            issuer: jwtSettings.GetSection("validIssuer").Value,
            claims: claims,
            expires: expiration,
            signingCredentials: signingCredentials,
            audience: "ScoreTable"
            );

        return token;
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim(ClaimTypes.UserData, _user.Id)
        };

        var roles = await _userManager.GetRolesAsync(_user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private static SigningCredentials GetSigningCredentials()
    {
        var key = Environment.GetEnvironmentVariable("SCORETABLEJWTKEY");
        var secret = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<bool> ValidateUser(UserLoginDto userDto)
    {
        _user = await _userManager.FindByNameAsync(userDto.Email);
        return _user != null && await _userManager.CheckPasswordAsync(_user, userDto.Password);
    }

}