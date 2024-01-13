using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreTableApi.Data;
using ScoreTableApi.Dto.Game;
using ScoreTableApi.Dto.Statline;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;
using ScoreTableApi.Services;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DatabaseContext _context;
    private readonly IUserService _userService;
    private readonly ILogger<GameController>  _logger;
    private readonly IMapper _mapper;
    private readonly IGameService _gameService;

    public GameController(IUnitOfWork unitOfWork, DatabaseContext context,
        ILogger<GameController> logger, IMapper mapper, IUserService userService,
        IGameService gameService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
        _gameService = gameService;

    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGames()
    {
        try
        {
            var gameSummaries = await _gameService.GetAllGameSummaryDtos();

            return Ok(gameSummaries);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGames)}");
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize]
    [HttpGet("{id:int}", Name = "GetGame")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGame(int id)
    {
        try
        {
            var game = await _gameService.GetGameDto(id);
            if (game == null)
                return NotFound($"Game with ID {id} does not exist");

            return Ok(game);
        } catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGame)}");
            return StatusCode(500, ex);
        }
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto gameDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            var createdGame = await _gameService.CreateGame(gameDto);

            await _unitOfWork.Save();

            return CreatedAtRoute("GetGame", new { id = createdGame.Entity.Id },
                await _gameService.GetGameDto(createdGame.Entity.Id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(CreateGame)}");
            return StatusCode(500, ex);
        }
    }

    [Authorize]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteGame(int id)
    {
        try
        {
            var exists = await _unitOfWork.Games.UserExists(id);
            if (!exists) ModelState.AddModelError("id", $"Could not find game with ID '{id}'");

            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            await _unitOfWork.Games.UserDelete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteGame)}");
            return StatusCode(500, ex);
        }
    }
}