﻿using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Application.Repositories.Abstractions;

namespace TicTacToe.Application.Features.Games.Queries;

public record GetGamesQuery() : IRequest<IEnumerable<GameDto>>;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GameDto>>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GetGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = Guard.Against.Null(gameRepository);
        _mapper = Guard.Against.Null(mapper);
    }

    public async Task<IEnumerable<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var games = await _gameRepository.GetGamesAsync(cancellationToken);
        var result = _mapper.Map<IEnumerable<GameDto>>(games);
        return result;
    }
}
