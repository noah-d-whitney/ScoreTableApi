using Microsoft.AspNetCore.Identity;

namespace ScoreTableApi.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public virtual ICollection<Game> Games { get; set; }
    public virtual ICollection<Player> Players { get; set; }
    public virtual ICollection<Team> Teams { get; set; }
    public virtual ICollection<PlayerStatline> PlayerStatlines { get; set; }
}