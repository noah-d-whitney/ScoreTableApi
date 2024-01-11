using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    private readonly int _notStartedGameStatusId;

    public GameService(DatabaseContext context, ILogger<GameController> logger,
        IMapper mapper, IUserService userService)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
        _notStartedGameStatusId = 1;
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

    public async Task<EntityEntry<Game>> CreateGame(CreateGameDto game)
    {
        try
        {
            var gameTeams = await GetTeamListFromIds(game.TeamIds);
            var team1Players = gameTeams[0].Players;
            var team2Players = gameTeams[1].Players;

            if (team1Players.Intersect(team2Players).FirstOrDefault() == null)
                throw new Exception("Game teams must not share players");

            var gameFormat = await GetGameFormatById(game.GameFormatId);
            var gameStatus = await GetGameStatusById(_notStartedGameStatusId);

            var createdGame = new Game
            {
                UserId = _userService.GetUserId(),
                Teams = gameTeams,
                GameFormat = gameFormat,
                GameStatus = gameStatus,
                DateTime = game.DateTime,
                PeriodLength = game.PeriodLength,
                PeriodCount = game.PeriodCount
            };

            var createdGameEntityEntry =  await _context.Games.AddAsync(createdGame);

            await CreatePlayerStatlinesByGame(createdGameEntityEntry.Entity);

            return createdGameEntityEntry;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static List<TeamStatlineDto> GetTeamStatlines(Game game)
    {
        var teamStatlines = new List<TeamStatlineDto>();
        foreach (var team in game.Teams)
        {
            teamStatlines.Add(new TeamStatlineDto(team.Name, team.Id));
        }

        return teamStatlines;
    }

    private async Task<List<Team>> GetTeamListFromIds(IEnumerable<int> teamIds)
    {
        try
        {
            var teams = new List<Team>();
            foreach (var id in teamIds)
            {
                var fetchedTeam = await _context.Teams.FindAsync(id);
                if (fetchedTeam == null)
                    throw new Exception($"Could not find team with ID {id}");
                teams.Add(fetchedTeam);
            }

            return teams;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<GameFormat> GetGameFormatById(int id)
    {
        try
        {
            var gameFormat = await _context.GameFormats.FindAsync(id);
            if (gameFormat == null)
                throw new Exception($"Could not find game format with ID {id}");

            return gameFormat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task<GameStatus> GetGameStatusById(int id)
    {
        try
        {
            var gameStatus = await _context.GameStatus.FindAsync(id);
            if (gameStatus == null)
                throw new Exception($"Could not find game status with ID {id}");

            return gameStatus;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task CreatePlayerStatlinesByGame(Game game)
    {
        try
        {
            var createdStatlines = new List<PlayerStatline>();

            foreach (var team in game.Teams)
            {
                foreach (var player in team.Players)
                {
                    var statline = new PlayerStatline
                    {
                        PlayerId = player.Id,
                        GameId = game.Id,
                        Points = 0,
                        Assists = 0,
                        Blocks = 0,
                        Fga = 0,
                        Fgm = 0,
                        Fouls = 0,
                        Fta = 0,
                        Ftm = 0,
                        Turnovers = 0,
                        Steals = 0,
                        IsStarter = false,
                        Tpa = 0,
                        Tpm = 0
                    };

                    createdStatlines.Add(statline);
                }
            }

            _context.PlayerStatlines.AddRange(createdStatlines);

            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}