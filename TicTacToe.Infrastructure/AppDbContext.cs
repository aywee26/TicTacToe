using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    #region DbSets
    public DbSet<Player> Players => Set<Player>();

    public DbSet<Game> Games => Set<Game>();
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var player = modelBuilder.Entity<Player>();
        player.HasKey(p => p.Id);
        player.Property(p => p.Name).IsRequired().HasMaxLength(128);
        player.HasIndex(p => p.Name);

        var game = modelBuilder.Entity<Game>();
        game.HasKey(g => g.Id);
        game.Property(g => g.CreatedAt).IsRequired();
        game.HasIndex(g => g.CreatedAt);

        var gamePlayer = modelBuilder.Entity<GamePlayer>();
        gamePlayer.HasKey("GameId", "PlayerId");
        gamePlayer.HasOne(gp => gp.Game).WithMany(g => g.GamePlayers);

        base.OnModelCreating(modelBuilder);
    }
}
