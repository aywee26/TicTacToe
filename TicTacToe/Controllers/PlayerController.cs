using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.Features.Players.Commands;
using TicTacToe.Application.Features.Players.Models;
using TicTacToe.Application.Features.Players.Queries;

namespace TicTacToe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ISender _mediator;

    public PlayerController(ISender mediator)
    {
       _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<IEnumerable<PlayerDto>> GetPlayers(CancellationToken cancellationToken)
    {
        var players = await _mediator.Send(new GetPlayersQuery(), cancellationToken);
        return players;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlayerDto>> GetPlayer(Guid id, CancellationToken cancellationToken)
    {
        var player = await _mediator.Send(new GetPlayerByIdQuery(id), cancellationToken);
        return (player is not null) ? Ok(player) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlayerDto>> CreatePlayer(string name, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreatePlayerCommand(name), cancellationToken);
        return CreatedAtAction(nameof(GetPlayer), new { id = result.Id }, result);
    }
}
