using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : Controller
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
    public async Task<IActionResult> GetGames()
    {
        try
        {
            var games = await  _unitOfWork.Games.GetAll();
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
}