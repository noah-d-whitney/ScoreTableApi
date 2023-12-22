using ScoreTableApi.Data;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;
using ScoreTableApi.Services;

namespace ScoreTableApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;
    private readonly IUserService _userService;
    private IBaseRepository<Game>? _games;
    private IBaseRepository<Player>? _players;
    private IBaseRepository<Team>? _teams;
    private IGenericRepository<PlayerStatline>? _playerStatlines;
    private IGenericRepository<GameFormat>? _gameFormats;
    private IGenericRepository<GameStatus>? _gameStatuses;

    public UnitOfWork(DatabaseContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    public IBaseRepository<Game> Games => _games ??= new
        GameRepository(_context, _userService);

    public IBaseRepository<Player> Players => _players ??= new
        PlayerRepository(_context, _userService);

    public IBaseRepository<Team> Teams => _teams ??= new
        TeamRepository(_context, _userService);

    public IGenericRepository<PlayerStatline> PlayerStatlines => _playerStatlines ??=
        new GenericRepository<PlayerStatline>(_context);

    public IGenericRepository<GameFormat> GameFormats => _gameFormats ??= new
        GenericRepository<GameFormat>(_context);

    public IGenericRepository<GameStatus> GameStatuses => _gameStatuses ??=
        new GenericRepository<GameStatus>(_context);

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}