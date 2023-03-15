﻿using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Repositories.Abstractions;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly AppDbContext _context;

    public GameRepository(AppDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public async Task<IEnumerable<Game>> GetGamesAsync(CancellationToken cancellationToken = default)
    {
        IQueryable<Game> query = _context.Games
            .Include(g => g.GamePlayers)
            .ThenInclude(gp => gp.Player);
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Game?> GetGameByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        IQueryable<Game> query = _context.Games
            .Where(g => g.Id == id)
            .Include(g => g.GamePlayers)
            .ThenInclude(gp => gp.Player);
        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}
