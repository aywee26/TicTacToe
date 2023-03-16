using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.Features.Games.Commands;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Application.Features.Games.Queries;

namespace TicTacToe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly ISender _mediator;

    public GameController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IEnumerable<GameDto>> GetGames(CancellationToken cancellationToken)
    {
        var games = await _mediator.Send(new GetGamesQuery(), cancellationToken);
        return games;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGameById(Guid id, CancellationToken cancellationToken)
    {
        var game = await _mediator.Send(new GetGameByIdQuery(id), cancellationToken);
        return (game is not null) ? Ok(game) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateGame(PlayerRequestDto players, CancellationToken cancellationToken)
    {
        var game = await _mediator.Send(new CreateGameCommand(players), cancellationToken);
        return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
    }
}
