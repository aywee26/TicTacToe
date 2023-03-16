namespace TicTacToe.Domain.Exceptions;

public class GameNotFoundException : Exception
{
    public GameNotFoundException(Guid id) 
        : base($"Game is not found. ID: '{id}'")
    {
    }
}
