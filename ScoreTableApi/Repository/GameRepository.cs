using ScoreTableApi.Data;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;
using ScoreTableApi.Models;

namespace ScoreTableApi.Repository;

public class GameRepository : IGameRepository
{
    private readonly Data.DatabaseContext _context;

    public GameRepository(Data.DatabaseContext context)
    {
        _context = context;
    }

    public ICollection<Game> GetGames()
    {
        return _context.Games.OrderBy(g => g.Id).ToList();
    }

    public Game GetGame(int id)
    {
        return _context.Games.Find(id);
    }
}