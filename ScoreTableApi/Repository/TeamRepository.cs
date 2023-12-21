using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Repository;

public class TeamRepository : IBaseRepository<Team>
{
    private readonly DatabaseContext _context;

    public TeamRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Team>> GetAll()
    {
        var teams = await _context.Teams
            .OrderBy(t => t.Id)
            .ToListAsync();
        return teams;
    }

    public async Task<Team> Get(int id)
    {
        var team = await _context.Teams
            .Where(t => t.Id == id)
            .Include(t => t.Players)
            .Include(t => t.Games)
            .SingleOrDefaultAsync();
        return team!;
    }

    public async Task<EntityEntry<Team>> Create(Team team)
    {
        return await _context.Teams.AddAsync(team);
    }

    public async Task<bool> Exists(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        return team != null;
    }

    public async Task<EntityEntry<Team>> Delete(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        return _context.Teams.Remove(team!);
    }
}