using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Players.Models;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Application.Features.Players.Queries;

public record GetPlayerByIdQuery(Guid Id) : IRequest<PlayerDto?>;

public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDto?>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<PlayerDto?> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var player = await _playerRepository.GetPlayerByIdAsync(request.Id, cancellationToken);
        var result = _mapper.Map<PlayerDto>(player);
        return result;
    }
}