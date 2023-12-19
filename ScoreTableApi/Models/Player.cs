namespace ScoreTableApi.Models;

public partial class Player
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int? Number { get; set; }

    public virtual ICollection<PlayerStatline> PlayerStatlines { get; set; } = new List<PlayerStatline>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
