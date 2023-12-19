namespace ScoreTableApi.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
