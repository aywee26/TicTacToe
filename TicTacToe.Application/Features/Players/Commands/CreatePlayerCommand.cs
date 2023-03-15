using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Players.Models;
using TicTacToe.Domain.Entities;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Application.Features.Players.Commands;

public record CreatePlayerCommand(string Name) : IRequest<PlayerDto>;

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, PlayerDto>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public CreatePlayerCommandHandler(IPlayerRepository playerRepository, IMapper mapper)
    {
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request));
        var playerEntity = new Player(Guid.NewGuid(), request.Name);
        var creationResult = await _playerRepository.CreatePlayerAsync(playerEntity, cancellationToken);
        var dto = _mapper.Map<PlayerDto>(creationResult);
        return dto;
    }
}
