using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Application.Repositories.Abstractions;

namespace TicTacToe.Application.Features.Games.Queries;

public record GetGameByIdQuery(Guid Id) : IRequest<GameDto?>;

public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameDto?>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GetGameByIdQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = Guard.Against.Null(gameRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<GameDto?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var game = await _gameRepository.GetGameByIdAsync(request.Id, cancellationToken);
        var result = _mapper.Map<GameDto>(game);
        return result;
    }
}
