namespace TicTacToe.Domain.Exceptions;

public class GameOverException : Exception
{
    public GameOverException()
        : base("The game is over and cannot be modified.")
    {
    }
}
