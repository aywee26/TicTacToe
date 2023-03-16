using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Infrastructure;

public class AppDbContextInitializer
{
    private readonly AppDbContext _context;

    public AppDbContextInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task ApplyMigrations()
    {
        await _context.Database.MigrateAsync();
    }
}
