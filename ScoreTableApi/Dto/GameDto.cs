namespace ScoreTableApi.Dto;

public class GameDto : CreateGameDto
{
    public int Id { get; set; }
    public GameStatusDto GameStatus { get; set; }
}