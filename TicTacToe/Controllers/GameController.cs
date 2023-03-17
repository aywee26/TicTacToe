using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using TicTacToe.Application.Features.Games.Commands;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Application.Features.Games.Queries;
using TicTacToe.Application.Features.Players.Models;

namespace TicTacToe.Controllers;

/// <summary>
/// Game Controller.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly ISender _mediator;

    /// <summary>
    /// Game Controller Constructer. Here MediatR gets injected.
    /// </summary>
    /// <param name="mediator"></param>
    public GameController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get information on all Games.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Array of Games is returned. If no Games exist, empty array is returned.</response>
    /// <returns></returns>
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PlayerDto>))]
    public async Task<IEnumerable<GameDto>> GetGames(CancellationToken cancellationToken)
    {
        var games = await _mediator.Send(new GetGamesQuery(), cancellationToken);
        return games;
    }

    /// <summary>
    /// Get information on the Game by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Game is returned.</response>
    /// <response code="404">Game is not found.</response>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDto>> GetGameById(Guid id, CancellationToken cancellationToken)
    {
        var game = await _mediator.Send(new GetGameByIdQuery(id), cancellationToken);
        return (game is not null) ? Ok(game) : NotFound();
    }

    /// <summary>
    /// Create Game. Body should contain IDs of two Players.
    /// </summary>
    /// <param name="players"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="201">Game is created.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="404">Players are not found.</response>
    /// <response code="422">Unprocessable entity.</response>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<GameDto>> CreateGame(PlayerRequestDto players, CancellationToken cancellationToken)
    {
        var game = await _mediator.Send(new CreateGameCommand(players), cancellationToken);
        return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
    }

    /// <summary>
    /// Update Game's state. Body should contain Row, Column = {0, 1, 2}.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Game is updated and returned.</response>
    /// <response code="400">Bad request.</response>
    /// <response code="404">Game is not found.</response>
    /// <response code="422">Unprocessable entity.</response>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PlayerDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<GameDto>> UpdateGame(Guid id, int row, int column, CancellationToken cancellationToken)
    {
        var updatedGame = await _mediator.Send(new UpdateGameCommand(id, row, column), cancellationToken);
        return Ok(updatedGame);
    }
}
