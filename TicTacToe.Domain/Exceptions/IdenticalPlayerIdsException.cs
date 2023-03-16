namespace TicTacToe.Domain.Exceptions;

public class IdenticalPlayerIdsException : Exception
{
    public IdenticalPlayerIdsException()
        : base("Player IDs should not be identical.")
    {
    }
}
