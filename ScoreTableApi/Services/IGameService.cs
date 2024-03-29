using Microsoft.EntityFrameworkCore.ChangeTracking;
using ScoreTableApi.Dto.Game;
using ScoreTableApi.Dto.Statline;
using ScoreTableApi.Models;

namespace ScoreTableApi.Services;

public interface IGameService
{
    public Task<GameDto?> GetGameDto(int gameId);
    public Task<EntityEntry<Game>> CreateGame(CreateGameDto game);
    public Task<List<GameSummaryDto>> GetAllGameSummaryDtos();
}