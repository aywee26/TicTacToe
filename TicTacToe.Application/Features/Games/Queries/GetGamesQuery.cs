using AutoMapper;
using MediatR;
using TicTacToe.Application.Features.Games.Models;

namespace TicTacToe.Application.Features.Games.Queries;

public record GetGamesQuery() : IRequest<IEnumerable<GameDto>>;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GameDto>>
{
    private readonly IMapper _mapper;

    public GetGamesQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<IEnumerable<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
