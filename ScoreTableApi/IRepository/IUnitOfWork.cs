using ScoreTableApi.Models;

namespace ScoreTableApi.IRepository;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Game> Games { get; }
    IGenericRepository<Player> Players { get; }
    IGenericRepository<Team> Teams { get; }
    IGenericRepository<PlayerStatline> PlayerStatlines { get; }
    IGenericRepository<GameFormat> GameFormats { get; }
    IGenericRepository<GameStatus> GameStatuses { get; }
    Task Save();
}