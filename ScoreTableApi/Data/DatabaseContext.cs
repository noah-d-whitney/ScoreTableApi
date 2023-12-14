using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoreTableApi.Models;

namespace ScoreTableApi.Data;

public class DatabaseContext : IdentityDbContext
{
    public IConfiguration _config { get; set; }
    public DatabaseContext(DbContextOptions options, IConfiguration config) :
        base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            _config.GetConnectionString("DatabaseConnection"));
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<PlayerStatline> PlayerStatlines { get; set; }
}