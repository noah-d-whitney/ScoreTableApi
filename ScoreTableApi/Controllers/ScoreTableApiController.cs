using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Models;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("/ScoreTableApi")]
public class ScoreTableApi : ControllerBase
{
    private readonly SignInManager<User> _signInManager;

    public ScoreTableApi(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("/logout")]
    public async Task<IActionResult> Logout([FromBody] object empty)
    {
        if (empty != null)
        {
            await _signInManager.SignOutAsync();
            return Ok("User Signed Out");
        }

        return Unauthorized();
    }
}