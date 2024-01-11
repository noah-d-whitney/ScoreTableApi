using AutoMapper;
using ScoreTableApi.Dto.Game;
using ScoreTableApi.Dto.Player;
using ScoreTableApi.Dto.Statline;
using ScoreTableApi.Dto.Team;
using ScoreTableApi.Dto.User;
using ScoreTableApi.Models;

namespace ScoreTableApi.Configurations;

public class MapperInitializer : Profile
{
    private readonly Data.DatabaseContext _context;

    public MapperInitializer(Data.DatabaseContext context)
    {
        _context = context;
    }

    public MapperInitializer()
    {
        CreateMap<Game, GameSummaryDto>().ReverseMap();
        CreateMap<Game, GameDto>().ReverseMap();
        CreateMap<Player, GamePlayerDto>().ReverseMap();
        CreateMap<Player, CreatePlayerDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<GameFormat, GameFormatDto>().ReverseMap();
        CreateMap<GameStatus, GameStatusDto>().ReverseMap();
        CreateMap<Team, GameTeamDto>().ReverseMap();
        CreateMap<PlayerStatline, PlayerStatlineDto>().ReverseMap();
        CreateMap<PlayerStatline, CreatePlayerStatlineDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}