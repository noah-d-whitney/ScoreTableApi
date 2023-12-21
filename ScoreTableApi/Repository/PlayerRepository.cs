using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Repository;

public class PlayerRepository : IBaseRepository<Player>
{
    private readonly DatabaseContext _context;

    public PlayerRepository(DatabaseContext context)
    {
        _context = context;
    }
    public async Task<ICollection<Player>> GetAll()
    {
        return await _context.Players.OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Player> Get(int id)
    {
        var player = await _context.Players
            .Where(p => p.Id == id)
            .Include(p => p.Teams)
            .Include(p => p.PlayerStatlines)
            .SingleOrDefaultAsync();
        return player!;
    }

    public async Task<EntityEntry<Player>> Create(Player player)
    {
        return await _context.Players.AddAsync(player);
    }

    public async Task<bool> Exists(int id)
    {
        var player = await _context.Players.FindAsync(id);
        return player != null;
    }

    public async Task<EntityEntry<Player>> Delete(int id)
    {
        var player = await _context.Players.FindAsync(id);
        return _context.Players.Remove(player!);
    }
}