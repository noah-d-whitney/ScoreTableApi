namespace ScoreTableApi.Dto;

public class GamePlayerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Number { get; set; }
    public PlayerStatlineDto Statline { get; set; }
}