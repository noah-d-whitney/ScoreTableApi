using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;
using ScoreTableApi.Services;

namespace ScoreTableApi.Repository;

public class PlayerRepository : IBaseRepository<Player>
{
    private readonly DatabaseContext _context;
    private readonly IUserService _userService;

    public PlayerRepository(DatabaseContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    public async Task<ICollection<Player>> UserGetAll()
    {
        var players = await _context.Players
            .Where(p => p.UserId == _userService.GetUserId())
            .OrderBy(p => p.LastName)
            .ToListAsync();
        return players;
    }

    public async Task<Player> UserGet(int id)
    {
        var player = await _context.Players
            .Where(p => p.UserId == _userService.GetUserId())
            .Where(p => p.Id == id)
            .Include(p => p.Teams)
            .Include(p => p.PlayerStatlines)
            .SingleOrDefaultAsync();
        return player!;
    }

    public async Task<EntityEntry<Player>> Create(Player player)
    {
        var userId = _userService.GetUserId();
        player.UserId = userId;
        return await _context.Players.AddAsync(player);
    }

    public async Task<bool> UserExists(int id)
    {
        var player = await _context.Players
            .Where(p => p.UserId == _userService.GetUserId())
            .Where(p => p.Id == id)
            .SingleOrDefaultAsync();
        return player != null;
    }

    public async Task<EntityEntry<Player>> UserDelete(int id)
    {
        var player = await _context.Players
            .Where(p => p.UserId == _userService.GetUserId())
            .Where(p => p.Id == id)
            .SingleOrDefaultAsync();
        return _context.Players.Remove(player!);
    }
}