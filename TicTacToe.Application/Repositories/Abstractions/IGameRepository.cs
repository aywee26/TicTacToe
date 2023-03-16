using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Repositories.Abstractions;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetGamesAsync(CancellationToken cancellationToken = default);

    Task<Game?> GetGameByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Game> CreateGameAsync(Game game, CancellationToken cancellationToken = default);
}
