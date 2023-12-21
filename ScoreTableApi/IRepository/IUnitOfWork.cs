using ScoreTableApi.Models;

namespace ScoreTableApi.IRepository;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Game> Games { get; }
    IBaseRepository<Player> Players { get; }
    IBaseRepository<Team> Teams { get; }
    IGenericRepository<PlayerStatline> PlayerStatlines { get; }
    IGenericRepository<GameFormat> GameFormats { get; }
    IGenericRepository<GameStatus> GameStatuses { get; }
    Task Save();
}