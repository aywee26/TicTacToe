using Ardalis.GuardClauses;

namespace TicTacToe.Domain.Entities;

public class Player
{
    private Player()
    {
    }

    public Player(Guid id, string name)
    {
        Id = id;
        Name = Guard.Against.Null(name);
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;
}
