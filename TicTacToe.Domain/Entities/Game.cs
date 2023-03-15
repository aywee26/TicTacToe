using Ardalis.GuardClauses;

namespace TicTacToe.Domain.Entities;

public class Game
{
    public Game(Guid id, DateTime createdAt, Player playerX, Player playerO)
    {
        Id = id;
        CreatedAt = createdAt;
        PlayerX = Guard.Against.Null(playerX);
        PlayerO = Guard.Against.Null(playerO);
    }

    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Player PlayerX { get; private set; }

    public Player PlayerO { get; private set; }
}
