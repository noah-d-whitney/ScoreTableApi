using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ScoreTableApi.Controllers;
using ScoreTableApi.Data;
using ScoreTableApi.Dto.Game;
using ScoreTableApi.Dto.Statline;
using ScoreTableApi.Models;

namespace ScoreTableApi.Services;

public class GameService: IGameService
{
    private readonly DatabaseContext _context;
    private readonly IUserService _userService;
    private readonly ILogger<GameController>  _logger;
    private readonly IMapper _mapper;


    public GameService(DatabaseContext context, ILogger<GameController>
            logger,
        IMapper mapper, IUserService userService)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<GameSummaryDto> GetGameSummaryDto(int gameId)
    {
        try
        {
            var game = await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Where(g => g.Id == gameId)
                .Include(g => g.GameStatus)
                .Include(g => g.GameFormat)
                .Include(g => g.Teams)
                .SingleOrDefaultAsync();

            if (game == null)
            {
                throw new Exception(
                    $"Game with ID {gameId} could not be found");
            }

            var gameSummaryDto = _mapper.Map<Game, GameSummaryDto>(game);
            gameSummaryDto.TeamStats = GetTeamStatlines(game);

            return gameSummaryDto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private List<TeamStatlineDto> GetTeamStatlines(Game game)
    {
        var teamStatlines = new List<TeamStatlineDto>();
        foreach (var team in game.Teams)
        {
            teamStatlines.Add(new TeamStatlineDto(team.Name, team.Id));
        }

        return teamStatlines;
    }
}