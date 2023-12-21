using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GameController>  _logger;
    private readonly IMapper _mapper;

    public PlayerController(IUnitOfWork unitOfWork, ILogger<GameController>
        logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPlayers()
    {
        try
        {
            var players = await _unitOfWork.Players.GetAll();

            if (players.Count == 0) return NoContent();

            var results = _mapper.Map<List<PlayerDto>>(players);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetPlayers)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpGet("{id:int}", Name = "GetPlayer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPlayer(int id)
    {
        try
        {
            var player = await _unitOfWork.Players.Get(id);

            if (player == null)
                return NotFound($"Player with ID '{id}' does not exist");

            var result = _mapper.Map<PlayerDto>(player);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetPlayer)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto playerDto)
    {
        try
        {
            var player = _mapper.Map<Player>(playerDto);

            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            var createdPlayer = await _unitOfWork.Players.Create(player);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetPlayer", new { id = createdPlayer.Entity.Id },
                createdPlayer.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(CreatePlayer)}");
            return StatusCode(500, ex);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        try
        {
            var exists = await _unitOfWork.Players.Exists(id);
            if (!exists) ModelState.AddModelError("id", $"Could not find player with ID '{id}'");

            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            await _unitOfWork.Players.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(DeletePlayer)}");
            return StatusCode(500, ex);
        }
    }
}