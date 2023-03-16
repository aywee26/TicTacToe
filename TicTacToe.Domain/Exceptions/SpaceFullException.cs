namespace TicTacToe.Domain.Exceptions;

public class SpaceFullException : Exception
{
    public SpaceFullException(int row, int column)
        : base($"The space is taken. Index: [{row}, {column}]")
    {
    }
}
