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
        Turn = 1;
        State = ".........";
        Status = "Player1 Turn";
    }

    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public int Turn { get; set; }

    public string State { get; set; } = default!;

    public string Status { get; set; } = default!;

    public ICollection<GamePlayer> GamePlayers { get; private set; } = new List<GamePlayer>(2);
}
