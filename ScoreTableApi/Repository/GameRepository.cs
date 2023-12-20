using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Repository;

public class GameRepository : IGameRepository
{
    private readonly Data.DatabaseContext _context;

    public GameRepository(Data.DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Game>> GetGames()
    {
        try
        {
            return await _context.Games.OrderBy(g => g.Id).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Game> GetGame(int id)
    {
        try
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) throw new FileNotFoundException();
            return game;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<EntityEntry<Game>> CreateGame(CreateGameDto gameDto)
    {
        try
        {
            var gameTeams = new List<Team>();
            foreach (var id in gameDto.TeamIds)
            {
                var team = await _context.Teams.FindAsync(id);
                if (team == null) throw new NoNullAllowedException("Could Not Successfully Map Team Ids");
                gameTeams.Add(team);
            }

            var gameFormat = await _context.GameFormats.FindAsync(gameDto
                .GameFormatId);
            var gameStatus = await _context.GameStatus.FindAsync(1);

            if (gameStatus == null) throw new NoNullAllowedException("Could Not Successfully Map Game Status Id");
            if (gameFormat == null) throw new NoNullAllowedException("Could Not Successfully Map Game Format Id");

            var createdGame = new Game
            {
                Teams = gameTeams,
                GameFormat = gameFormat,
                GameStatus = gameStatus,
                DateTime = gameDto.DateTime,
                PeriodCount = gameDto.PeriodCount,
                PeriodLength = gameDto.PeriodLength
            };

            return await _context.Games.AddAsync(createdGame);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}