using TicTacToe.Domain.Entities;

namespace TicTacToe.Repositories.Abstractions;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Player>> GetAllPlayersAsync(CancellationToken cancellationToken = default);

    Task<Player> CreatePlayerAsync(Player player, CancellationToken cancellationToken = default);
}
