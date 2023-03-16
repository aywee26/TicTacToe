namespace TicTacToe.Domain.Exceptions;

public class PlayerNotFoundException : Exception
{
    public PlayerNotFoundException(Guid id)
        : base($"Player is not found. ID: '{id}'")
    {
    }
}
