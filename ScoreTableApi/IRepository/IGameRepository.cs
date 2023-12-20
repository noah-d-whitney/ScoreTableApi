using System.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Dto;
using ScoreTableApi.Models;

namespace ScoreTableApi.IRepository;

public interface IGameRepository
{
    Task<ICollection<Game>> GetGames();
    Task<Game> GetGame(int id);
    Task<EntityEntry<Game>> CreateGame(CreateGameDto gameDto);
}