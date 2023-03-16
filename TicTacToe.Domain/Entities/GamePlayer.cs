using Ardalis.GuardClauses;

namespace TicTacToe.Domain.Entities;

public class GamePlayer
{
    private GamePlayer()
    { 
    }

    public GamePlayer(Game game, Player player)
    {
        Game = Guard.Against.Null(game);
        Player = Guard.Against.Null(player);
    }

    public Game Game { get; private set; } = default!;

    public Player Player { get; private set; } = default!;
}
