using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GameController>  _logger;
    private readonly IMapper _mapper;

    public GameController(IUnitOfWork unitOfWork, ILogger<GameController>
            logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGames()
    {
        try
        {
            var games = await  _unitOfWork.Games.GetAll(includes: new
                List<String> {"GameStatus", "GameFormat"});
            var results = _mapper.Map<IList<GameDto>>(games);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGames)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpGet("{id:int}", Name = "GetGame")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGame(int id)
    {
        try
        {
            var game = await _unitOfWork.Games.Get(q => q.Id == id, new List<string> {"Teams", "GameFormat", "GameStatus", "Teams.Players"});
            var result = _mapper.Map<GameDto>(game);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGame)
                }");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateGame(
        [FromBody] CreateGameDto gameDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError($"Invalid POST attempted in {nameof(CreateGame)}");
            return BadRequest(ModelState);
        }

        try
        {
            var game = _mapper.Map<Game>(gameDto);
            await _unitOfWork.Games.Insert(game);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetGame", new { id = game.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(CreateGame)}");
            return StatusCode(500, "Internal Server Error. Please Try Again.");
        }
    }
}