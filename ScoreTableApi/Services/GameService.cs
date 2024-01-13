using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    public async Task<List<GameSummaryDto>> GetAllGameSummaryDtos()
    {
        try
        {
            var games = await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Include(g => g.GameStatus)
                .Include(g => g.GameFormat)
                .Include(g => g.Teams)
                .ToListAsync();

            var gameSummaries = new List<GameSummaryDto>();

            foreach (var game in games)
            {
                gameSummaries.Add(ConvertToSummaryDto(game));
            }

            return gameSummaries;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private GameSummaryDto ConvertToSummaryDto(Game game)
    {
        var gameSummaryDto = _mapper.Map<Game, GameSummaryDto>(game);
        gameSummaryDto.TeamStats = GetTeamStatlines(game);

        return gameSummaryDto;
    }

    private GameDto ConvertToDto(Game game)
    {
        var gameDto = _mapper.Map<Game, GameDto>(game);
        gameDto.TeamStats = GetTeamStatlines(game);
        gameDto.Players = GetGamePlayers(game);

        return gameDto;
    }

    public async Task<GameDto?> GetGameDto(int gameId)
    {
        try
        {
            var game = await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Where(g => g.Id == gameId)
                .Include(g => g.GameStatus)
                .Include(g => g.GameFormat)
                .Include(g => g.Teams)
                    .ThenInclude(t => t.Players)
                    .AsSplitQuery()
                .SingleOrDefaultAsync();

            if (game == null) return null;

            var gameDto = ConvertToDto(game);

            return gameDto;
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

            if (team1Players.Intersect(team2Players).FirstOrDefault() != null)
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

            await _context.SaveChangesAsync();

            await CreatePlayerStatlinesByGame(createdGameEntityEntry.Entity);


            return createdGameEntityEntry;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // TODO Calculate statline from player statlines
    private static List<TeamStatlineDto> GetTeamStatlines(Game game)
    {
        var teamStatlines = new List<TeamStatlineDto>();
        foreach (var team in game.Teams)
        {
            teamStatlines.Add(new TeamStatlineDto(team.Name, team.Id));
        }

        return teamStatlines;
    }

    private List<GamePlayerDto> GetGamePlayers(Game game)
    {
        var gamePlayers = new List<GamePlayerDto>();
        foreach (var team in game.Teams)
        {
            foreach (var player in team.Players)
            {
                var gamePlayer = _mapper.Map<Player, GamePlayerDto>(player);
                gamePlayer.Team = _mapper.Map<Team, GameTeamDto>(team);
                var statline = _context.PlayerStatlines
                    .Where(ps => ps.UserId == _userService.GetUserId())
                    .Where(ps => ps.GameId == game.Id)
                    .Single(ps => ps.PlayerId == player.Id);
                gamePlayer.Statline = _mapper.Map<PlayerStatline, PlayerStatlineDto>(statline);
                gamePlayers.Add(gamePlayer);
            }
        }

        return gamePlayers;
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
            var gameTeams = await _context.Teams
                .Where(t => game.Teams.Contains(t))
                .Include(t => t.Players)
                .ToListAsync();

            foreach (var team in gameTeams)
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
                        Tpm = 0,
                        UserId = _userService.GetUserId()
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