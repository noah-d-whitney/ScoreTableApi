using ScoreTableApi.Models;

namespace ScoreTableApi.IRepository;

public interface IGameRepository
{
    ICollection<Game> GetGames();
}