using Microsoft.EntityFrameworkCore;
using TicTacToe.Entities;

namespace TicTacToe.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Player> Players => Set<Player>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var player = modelBuilder.Entity<Player>();
        player.HasKey(p => p.Id);
        player.Property(p => p.Name).IsRequired().HasMaxLength(128);

        base.OnModelCreating(modelBuilder);
    }
}
