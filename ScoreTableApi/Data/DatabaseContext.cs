using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScoreTableApi.Models;

namespace ScoreTableApi.Data;

public partial class DatabaseContext : IdentityDbContext
{

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameFormat> GameFormats { get; set; }

    public virtual DbSet<GameStatus> GameStatuses { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerStatline> PlayerStatlines { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasIndex(e => e.GameFormatId, "IX_Games_GameFormatId");

            entity.HasIndex(e => e.GameStatusId, "IX_Games_GameStatusId");

            entity.HasOne(d => d.GameFormat).WithMany(p => p.Games).HasForeignKey(d => d.GameFormatId);

            entity.HasOne(d => d.GameStatus).WithMany(p => p.Games).HasForeignKey(d => d.GameStatusId);

            entity.HasMany(d => d.Teams).WithMany(p => p.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GameTeam",
                    r => r.HasOne<Team>().WithMany().HasForeignKey("TeamsId"),
                    l => l.HasOne<Game>().WithMany().HasForeignKey("GamesId"),
                    j =>
                    {
                        j.HasKey("GamesId", "TeamsId");
                        j.ToTable("GameTeam");
                        j.HasIndex(new[] { "TeamsId" }, "IX_GameTeam_TeamsId");
                    });
        });

        modelBuilder.Entity<GameFormat>(entity =>
        {
            entity.ToTable("GameFormat");
        });

        modelBuilder.Entity<GameStatus>(entity =>
        {
            entity.ToTable("GameStatus");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasMany(d => d.Teams).WithMany(p => p.Players)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerTeam",
                    r => r.HasOne<Team>().WithMany().HasForeignKey("TeamsId"),
                    l => l.HasOne<Player>().WithMany().HasForeignKey("PlayersId"),
                    j =>
                    {
                        j.HasKey("PlayersId", "TeamsId");
                        j.ToTable("PlayerTeam");
                        j.HasIndex(new[] { "TeamsId" }, "IX_PlayerTeam_TeamsId");
                    });
        });

        modelBuilder.Entity<PlayerStatline>(entity =>
        {
            entity.HasIndex(e => e.GameId, "IX_PlayerStatlines_GameId");

            entity.HasIndex(e => e.PlayerId, "IX_PlayerStatlines_PlayerId");

            entity.HasOne(d => d.Game).WithMany(p => p.PlayerStatlines).HasForeignKey(d => d.GameId);

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerStatlines).HasForeignKey(d => d.PlayerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
