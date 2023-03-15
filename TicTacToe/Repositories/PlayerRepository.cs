using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Entities;
using TicTacToe.Contexts;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly AppDbContext _context;

    public PlayerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken = default)
    {
        var players = await _context.Players.ToListAsync(cancellationToken);
        return players;
    }

    public async Task<Player?> GetPlayerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var player = await _context.Players.FindAsync(new object[] { id }, cancellationToken);
        return player;
    }

    public async Task<Player> CreatePlayerAsync(Player player, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(player, nameof(player));
        var result = await _context.Set<Player>().AddAsync(player, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
}
