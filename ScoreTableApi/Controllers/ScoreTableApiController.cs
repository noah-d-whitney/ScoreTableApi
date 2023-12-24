using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Dto;
using ScoreTableApi.Models;
using ScoreTableApi.Services;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("/ScoreTableApi")]
public class ScoreTableApi : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;

    public ScoreTableApi(SignInManager<User> signInManager, IUserService userService)
    {
        _signInManager = signInManager;
        _userService = userService;
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

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("/userData")]
    public async Task<IActionResult> GetUserData()
    {
        try
        {
            //Todo Handle errors
            var user = await _userService.GetUserData();

            if (user is null) return NotFound("Could not get user data!");

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }
}