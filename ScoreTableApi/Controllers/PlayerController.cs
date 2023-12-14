using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;

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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPlayers()
    {
        try
        {
            var players = await  _unitOfWork.Players.GetAll();
            var results = _mapper.Map<IList<PlayerDto>>(players);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetPlayers)
                }");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPlayer(int id)
    {
        try
        {
            var player = await _unitOfWork.Players.Get(q => q.Id == id, new List<string>{"PlayerStatlines", "Teams"});
            var result = _mapper.Map<PlayerDto>(player);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetPlayer)
            }");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }
}