using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoreTableApi.Configurations.Entities;
using ScoreTableApi.Models;

namespace ScoreTableApi.Data;

public class DatabaseContext : IdentityDbContext<User>
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<PlayerStatline> PlayerStatlines { get; set; }
}