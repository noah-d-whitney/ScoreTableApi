using AutoMapper;
using AutoMapper.QueryableExtensions;
using ScoreTableApi.Data;
using ScoreTableApi.Dto;
using ScoreTableApi.IRepository;
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
        CreateMap<Team, TeamDto>().ReverseMap();
        CreateMap<Team, CreateTeamDto>().ReverseMap();
        CreateMap<Team, GameTeamDto>().ReverseMap();
        CreateMap<Player, PlayerDto>().ReverseMap();
        CreateMap<Player, CreatePlayerDto>().ReverseMap();
        CreateMap<Player, GamePlayerDto>().ReverseMap();
        CreateMap<Game, GameDto>().ReverseMap();
        CreateMap<PlayerStatline, PlayerStatlineDto>().ReverseMap();
        CreateMap<PlayerStatline, CreatePlayerStatlineDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
    // public List<Team> GetTeamsFromIds(List<int> ids)
    // {
    //     var configuration = new MapperConfiguration(cfg =>
    //     {
    //         cfg.CreateProjection<CreateGameDto, Game>().ForMember(
    //             game => game.Teams,
    //             conf => conf.MapFrom(dto => dto.Teams));
    //     });
    //
    //     var teams = _context.Teams.Where(t => ids.Contains(t.Id))
    //         .ProjectTo<Team>(configuration).ToList();
    //     return teams;
    // }
}