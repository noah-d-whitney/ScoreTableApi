using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScoreTableApi.Data;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly DatabaseContext _context;
    private readonly ILogger<GameController>  _logger;
    private readonly IMapper _mapper;

    public TeamController(IUnitOfWork unitOfWork, DatabaseContext context,
        ILogger<GameController> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTeams()
    {
        try
        {
            var teams = await _unitOfWork.Teams.GetAll();

            if (teams.Count == 0) return NoContent();

            var results = _mapper.Map<List<TeamDto>>(teams);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetTeams)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpGet("{id:int}", Name = "GetTeam")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTeam(int id)
    {
        try
        {
            var team = await _unitOfWork.Teams.Get(id);

            if (team == null)
                return NotFound($"Team with ID '{id}' does not exist");

            var result = _mapper.Map<TeamDto>(team);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(GetTeam)}");
            return StatusCode(500,
                "Internal Sever Error. Please try Again Later.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto teamDto)
    {
        try
        {
            var teamPlayers = new List<Player>();
            for (var i = 0; i < teamDto.PlayerIds.Count; i++)
            {
                var id = teamDto.PlayerIds[i];
                var player = await _context.Players.FindAsync(id);
                if (player == null) ModelState.AddModelError("PlayerIds",
                    $"Could not find Player {i + 1} with ID '{id}'");
                teamPlayers.Add(player!);
            }
            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            var team = new Team
            {
                Name = teamDto.Name,
                Players = teamPlayers
            };

            var createdTeam = await _unitOfWork.Teams.Create(team);
            await _unitOfWork.Save();

            return CreatedAtRoute("GetTeam", new { id = createdTeam.Entity.Id },
                createdTeam.Entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(CreateTeam)}");
            return StatusCode(500, ex);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        try
        {
            var exists = await _unitOfWork.Teams.Exists(id);
            if (!exists) ModelState.AddModelError("id", $"Could not find team with ID '{id}'");

            if (!ModelState.IsValid)
                return BadRequest(ValidationProblem(ModelState));

            await _unitOfWork.Teams.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteTeam)}");
            return StatusCode(500, ex);
        }
    }
}