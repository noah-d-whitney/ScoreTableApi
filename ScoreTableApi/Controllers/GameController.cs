using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Data;
using ScoreTableApi.Dto;
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

    public GameController(IUnitOfWork unitOfWork, DatabaseContext context,
        ILogger<GameController> logger, IMapper mapper, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
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
            var games = await _unitOfWork.Games.UserGetAll();

            if (games.Count == 0) return NoContent();

            var results = _mapper.Map<List<GameSummaryDto>>(games);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGames)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
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
            var game = await _unitOfWork.Games.UserGet(id);

            if (game == null)
                return NotFound($"Game with ID '{id}' does not exist");

            var result = _mapper.Map<GameSummaryDto>(game);
            return Ok(result);
        } catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGame)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
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
            var gameTeams = new List<Team>();
            for (var i = 0; i < gameDto.TeamIds.Count; i++)
            {
                var id = gameDto.TeamIds[i];
                var team = await _context.Teams.FindAsync(id);
                if (team == null) ModelState.AddModelError("TeamIds", $"Could not find Team {i + 1} with ID '{id}'");
                gameTeams.Add(team!);
            }

            var gameFormat =
                await _context.GameFormats.FindAsync(gameDto.GameFormatId);
            if (gameFormat == null) ModelState.AddModelError("GameFormatId", $"Could not find game format with ID '{gameDto.GameFormatId}'");

            var gameStatusId = 1;
            var gameStatus = await _context.GameStatus.FindAsync(gameStatusId);
            if (gameStatus == null)
                ModelState.AddModelError("GameStatusId",
                    $"Could not find game status with ID '{gameStatusId}'");


            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            var game = new Game
            {
                Teams = gameTeams,
                GameFormat = gameFormat!,
                GameStatus = gameStatus!,
                DateTime = gameDto.DateTime,
                PeriodCount = gameDto.PeriodCount,
                PeriodLength = gameDto.PeriodLength
            };

            var createdGame = await _unitOfWork.Games.Create(game);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetGame", new { id = createdGame.Entity.Id },
                createdGame.Entity);
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