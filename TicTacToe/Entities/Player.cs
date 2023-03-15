namespace TicTacToe.Entities;

public class Player
{
    public Player(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
}
