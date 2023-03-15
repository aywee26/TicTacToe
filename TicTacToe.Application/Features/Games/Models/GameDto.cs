namespace TicTacToe.Application.Features.Games.Models;

public record GameDto(Guid Id, DateTime CreatedAt, IEnumerable<GamePlayerDto> GamePlayers);
