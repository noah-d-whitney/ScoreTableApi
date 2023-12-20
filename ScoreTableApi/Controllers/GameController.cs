using System.Data;
using AutoMapper;
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

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGames()
    {
        try
        {
            var games = await _unitOfWork.Games.GetGames();
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
            var game = await _unitOfWork.Games.GetGame(id);
            var result = _mapper.Map<GameDto>(game);
            return Ok(result);
        } catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGame)
                }");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetGame([FromBody] CreateGameDto gameDto)
    {
        try
        {
            var gameTeams = new List<Team>();


            var createdGame = await _unitOfWork.Games.CreateGame(gameDto);
            if (createdGame == null) return StatusCode(500);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetGame", new { id = createdGame.Entity.Id },
                createdGame.Entity);
        }
        catch (NoNullAllowedException ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGame)
            }");
            return StatusCode(400, ex);
        } catch (FileNotFoundException ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetGame)
            }");
            return StatusCode(404, ex);
        }
    }
}