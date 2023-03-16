using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Application.Repositories.Abstractions;
using TicTacToe.Domain.Entities;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Application.Features.Games.Commands;

public record CreateGameCommand(PlayerRequestDto Players) : IRequest<GameDto>;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameDto>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public CreateGameCommandHandler(IPlayerRepository playerRepository, IGameRepository gameRepository, IMapper mapper)
    {
        _playerRepository = Guard.Against.Null(playerRepository);
        _gameRepository = Guard.Against.Null(gameRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        if (request.Players.Player1Id == request.Players.Player2Id)
            throw new ArgumentException(nameof(request.Players));

        var player1 = await _playerRepository.GetPlayerByIdAsync(request.Players.Player1Id, cancellationToken);
        if (player1 is null)
            throw new ArgumentException(nameof(player1));

        var player2 = await _playerRepository.GetPlayerByIdAsync(request.Players.Player2Id, cancellationToken);
        if (player2 is null)
            throw new ArgumentException(nameof(player2));

        var game = new Game(Guid.NewGuid(), DateTime.UtcNow);

        var gamePlayer1 = new GamePlayer(game, player1);
        var gamePlayer2 = new GamePlayer(game, player2);

        game.GamePlayers.Add(gamePlayer1);
        game.GamePlayers.Add(gamePlayer2);

        var createdGame = await _gameRepository.CreateGameAsync(game, cancellationToken);
        var result = _mapper.Map<GameDto>(createdGame);
        return result;
    }
}
