using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;
using ScoreTableApi.Services;

namespace ScoreTableApi.Repository;

public class TeamRepository : IBaseRepository<Team>
{
    private readonly DatabaseContext _context;
    private readonly IUserService _userService;

    public TeamRepository(DatabaseContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public async Task<ICollection<Team>> UserGetAll()
    {
        var teams = await _context.Teams
            .Where(t => t.UserId == _userService.GetUserId())
            .OrderBy(t => t.Id)
            .ToListAsync();
        return teams;
    }

    public async Task<Team> UserGet(int id)
    {
        var team = await _context.Teams
            .Where(t => t.UserId == _userService.GetUserId())
            .Where(t => t.Id == id)
            .Include(t => t.Players)
            .Include(t => t.Games)
            .SingleOrDefaultAsync();
        return team!;
    }

    public async Task<EntityEntry<Team>> Create(Team team)
    {
        var userId = _userService.GetUserId();
        team.UserId = userId;
        return await _context.Teams.AddAsync(team);
    }

    public async Task<bool> UserExists(int id)
    {
        var team = await _context.Teams
            .Where(t => t.UserId == _userService.GetUserId())
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync();
        return team != null;
    }

    public async Task<EntityEntry<Team>> UserDelete(int id)
    {
        var team = await _context.Teams
            .Where(t => t.UserId == _userService.GetUserId())
            .Where(t => t.Id == id)
            .SingleOrDefaultAsync();
        return _context.Teams.Remove(team!);
    }
}