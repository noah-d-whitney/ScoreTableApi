using AutoMapper;
using ScoreTableApi.Dto;
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
        CreateMap<Game, GameSummaryDto>()
            .ForMember(dest => dest.GameStatus,
                opt => opt.MapFrom(
                    src => src.GameStatus.Name))
            .ForMember(dest => dest.GameFormat,
                opt => opt.MapFrom(
                    src => src.GameFormat.Name));
        CreateMap<Player, CreatePlayerDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<Team, GameTeamDto>().ReverseMap();
        CreateMap<Player, GamePlayerDto>().ReverseMap();
        CreateMap<PlayerStatline, PlayerStatlineDto>().ReverseMap();
        CreateMap<PlayerStatline, CreatePlayerStatlineDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}