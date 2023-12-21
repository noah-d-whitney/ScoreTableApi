using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Repository;

public class PlayerRepository(Data.DatabaseContext context) :
    IBaseRepository<Player>
{
    public async Task<ICollection<Player>> GetAll()
    {
        return await context.Players.OrderBy(p => p.Id).ToListAsync();
    }

    public async Task<Player> Get(int id)
    {
        var player = await context.Players
            .Where(p => p.Id == id)
            .Include(p => p.Teams)
            .Include(p => p.PlayerStatlines)
            .SingleAsync();
        return player;
    }

    public async Task<EntityEntry<Player>> Create(Player player)
    {
        return await context.Players.AddAsync(player);
    }

    public async Task<bool> Exists(int id)
    {
        var player = await context.Players.FindAsync(id);
        return player != null;
    }

    public async Task<EntityEntry<Player>> Delete(int id)
    {
        var player = await context.Players.FindAsync(id);
        return context.Players.Remove(player!);
    }
}