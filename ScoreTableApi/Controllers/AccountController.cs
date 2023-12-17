using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Dto;
using ScoreTableApi.Models;

namespace ScoreTableApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    // private readonly SignInManager<User> _signInManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;

    public AccountController(UserManager<User> userManager, ILogger<AccountController> logger,
        IMapper mapper)
    {
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        _logger.LogInformation($"Registration attempt for {userDto.Email}");
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var user = _mapper.Map<User>(userDto);
            user.UserName = userDto.Email;
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userDto.Roles);
            return Accepted();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
            return Problem($"Something went wrong in the {nameof(Register)}",
                statusCode: 500);
        }
    }

    // [HttpPost]
    // [Route("Login")]
    // [ProducesResponseType(StatusCodes.Status202Accepted)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // public async Task<IActionResult> Login([FromBody] UserLoginDto userDto)
    // {
    //     _logger.LogInformation($"Login attempt for {userDto.Email}");
    //     if (!ModelState.IsValid) return BadRequest(ModelState);
    //
    //     try
    //     {
    //         var result = await _signInManager.PasswordSignInAsync(userDto
    //             .Email, userDto.Password, false, false);
    //
    //         if (!result.Succeeded) return Unauthorized(userDto);
    //
    //         return Accepted();
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
    //         return Problem($"Something went wrong in the {nameof(Login)}",
    //             statusCode: 500);
    //     }
    // }
}