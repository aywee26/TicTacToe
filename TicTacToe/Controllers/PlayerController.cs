using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.Features.Players.Commands;
using TicTacToe.Application.Features.Players.Models;
using TicTacToe.Application.Features.Players.Queries;

namespace TicTacToe.Controllers;

/// <summary>
/// Player Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Player Controller Constructor. Here MediatR gets injected.
    /// </summary>
    /// <param name="mediator"></param>
    public PlayerController(ISender mediator)
    {
       _mediator = mediator;
    }

    /// <summary>
    /// Get information on all Players.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Array of Players is returned. If no Players exist, empty array is returned.</response>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDto>))]
    public async Task<IEnumerable<PlayerDto>> GetPlayers(CancellationToken cancellationToken)
    {
        var players = await _mediator.Send(new GetPlayersQuery(), cancellationToken);
        return players;
    }

    /// <summary>
    /// Get information on the Player by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Player is returned.</response>
    /// <response code="404">Player is not found.</response>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlayerDto>> GetPlayer(Guid id, CancellationToken cancellationToken)
    {
        var player = await _mediator.Send(new GetPlayerByIdQuery(id), cancellationToken);
        return (player is not null) ? Ok(player) : NotFound();
    }

    /// <summary>
    /// Create Player. Body should contain new player's Name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Player is created.</response>
    /// <response code="400">Bad request.</response>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PlayerDto>> CreatePlayer(string name, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreatePlayerCommand(name), cancellationToken);
        return CreatedAtAction(nameof(GetPlayer), new { id = result.Id }, result);
    }
}
