namespace TicTacToe.Domain.Entities;

public class Game
{
    private Game()
    {
    }

    public Game(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ICollection<GamePlayer> GamePlayers { get; private set; } = new GamePlayer[2];
}
