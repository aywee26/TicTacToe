using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Application.Repositories.Abstractions;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Features.Games.Commands;

public record UpdateGameCommand(Guid Id, int Row, int Column) : IRequest<GameDto>;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, GameDto>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public UpdateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = Guard.Against.Null(gameRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<GameDto> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));

        var game = await _gameRepository.GetGameByIdAsync(request.Id, cancellationToken)
            ?? throw new ArgumentException("Game not found!");

        if (!game.Status.Contains("Turn"))
            throw new InvalidOperationException("Game is over!");

        if (request.Row < 0 || request.Row > 2)
            throw new ArgumentException("Row is incorrect!");
        if (request.Column < 0 || request.Column > 2)
            throw new ArgumentException("Column is incorrect!");

        var gameState = game.State.ToCharArray();
        var symbol = (game.Turn % 2 == 1) ? 'X' : 'O';
        ModifyState(gameState, request.Row, request.Column, symbol);
        ChangeGameProperties(game, gameState);

        var updateResult = await _gameRepository.UpdateGameAsync(game, cancellationToken);
        var mapperResult = _mapper.Map<GameDto>(updateResult);
        return mapperResult;
    }

    private void ModifyState(char[] state, int row, int column, char symbol)
    {
        int index = row * 3 + column;
        if (state[index] != '.')
            throw new InvalidOperationException("The field is taken!");
        state[index] = symbol;
    }

    private bool IsGameVictorious(char[] state, char symbol)
    {
        // check rows
        for (int row = 0; row < 3; row++)
        {
            if (Enumerable.Range(0, 3).All(col => state[row * 3 + col] == symbol))
                return true;
        }

        // check columns
        for (int row = 0; row < 3; row++)
        {
            if (Enumerable.Range(0, 3).All(col => state[row + col * 3] == symbol))
                return true;
        }

        // check main diagonal
        if (Enumerable.Range(0, 3).All(x => state[x * 4] == symbol))
            return true;

        // check antidiagonal
        if (Enumerable.Range(1, 3).All(x => state[x * 2] == symbol))
            return true;

        return false;
    }

    private void ChangeGameProperties(Game game, char[] currentGameState)
    {
        game.State = new string(currentGameState);

        if (IsGameVictorious(currentGameState, 'X'))
        {
            ChangeGameStatus(game, GameStatus.Player1Victory);
            return;
        }
        if (IsGameVictorious(currentGameState, 'O'))
        {
            ChangeGameStatus(game, GameStatus.Player2Victory);
            return;
        }
        if (game.Turn >= 9)
        {
            ChangeGameStatus(game, GameStatus.Draw);
            return;
        }

        game.Turn++;
        if (game.Turn % 2 == 1)
            ChangeGameStatus(game, GameStatus.Player1Turn);
        else
            ChangeGameStatus(game, GameStatus.Player2Turn);
    }

    private void ChangeGameStatus(Game game, GameStatus newStatus)
    {
        switch (newStatus)
        {
            case GameStatus.Player1Turn:
                game.Status = "Player1 Turn";
                break;
            case GameStatus.Player2Turn:
                game.Status = "Player2 Turn";
                break;
            case GameStatus.Player1Victory:
                game.Status = "Player1 won";
                break;
            case GameStatus.Player2Victory:
                game.Status = "Player1 won";
                break;
            case GameStatus.Draw:
                game.Status = "Draw";
                break;
        }
    }
}
