using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Players.Models;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Application.Features.Players.Queries;

public record GetPlayersQuery() : IRequest<IEnumerable<PlayerDto>>;

public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, IEnumerable<PlayerDto>>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public GetPlayersQueryHandler(IPlayerRepository playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlayerDto>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var players = await _playerRepository.GetAllPlayersAsync(cancellationToken);
        var result = _mapper.Map<IEnumerable<PlayerDto>>(players);
        return result;
    }
}
