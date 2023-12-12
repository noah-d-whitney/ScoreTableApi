namespace ScoreTableApi.Dto;

public class GameStatusDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IList<GameDto> Games { get; set; }
}