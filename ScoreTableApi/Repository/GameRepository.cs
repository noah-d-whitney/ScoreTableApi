using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;
using ScoreTableApi.Services;

namespace ScoreTableApi.Repository;

public class GameRepository : IBaseRepository<Game>
{
    private readonly DatabaseContext _context;
    private readonly IUserService _userService;

    public GameRepository(DatabaseContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<ICollection<Game>> UserGetAll()
    {
        try
        {
            return await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Include(g => g.GameStatus)
                .Include(g => g.GameFormat)
                .Include(g => g.Teams)
                .OrderBy(g => g.DateTime)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<Game> UserGet(int id)
    {
        try
        {
            var game = await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Where(g => g.Id == id)
                .Include(g => g.GameFormat)
                .Include(g => g.GameStatus)
                .SingleOrDefaultAsync();
            return game!;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<EntityEntry<Game>> Create(Game game)
    {
        try
        {
            game.UserId =  _userService.GetUserId();
            return await _context.Games.AddAsync(game);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<bool> UserExists(int id)
    {
        try
        {
            var game = await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Where(g => g.Id == id)
                .SingleOrDefaultAsync();
            return game != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task<EntityEntry<Game>> UserDelete(int id)
    {
        try
        {
            var game = await _context.Games
                .Where(g => g.UserId == _userService.GetUserId())
                .Where(g => g.Id == id)
                .SingleOrDefaultAsync();

            if (game == null)
                throw new ArgumentException(
                    $"Could not find game with ID '{id}' for delete in {nameof(UserDelete)}");

            return _context.Games.Remove(game);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}