using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Repository;

public class GameRepository(Data.DatabaseContext _context) : IGameRepository
{
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
            var game = await _context.Games
                .Where(g => g.Id == id)
                .Include(g => g.GameFormat)
                .Include(g => g.GameStatus)
                .SingleAsync();
            return game;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<EntityEntry<Game>> CreateGame(Game game)
    {
        try
        {
            return await _context.Games.AddAsync(game);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> GameExists(int id)
    {
        try
        {
            var game = await _context.Games.FindAsync(id);
            return game != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task DeleteGame(int id)
    {
        try
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
                throw new ArgumentException(
                    $"Could not find game with ID '{id}' for delete in {nameof(DeleteGame)}");

            _context.Games.Remove(game);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}