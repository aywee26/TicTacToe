using Microsoft.AspNetCore.Mvc;
using TicTacToe.Entities;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerController(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    [HttpGet("")]
    public async Task<IEnumerable<Player>> GetPlayers(CancellationToken cancellationToken)
    {
        var players = await _playerRepository.GetAllPlayersAsync(cancellationToken);
        return players;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(Guid id, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetPlayerByIdAsync(id, cancellationToken);
        return (player is not null) ? Ok(player) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<Player>> CreatePlayer(string name, CancellationToken cancellationToken)
    {
        var newPlayer = new Player(Guid.NewGuid(), name);
        var result = await _playerRepository.CreatePlayerAsync(newPlayer, cancellationToken);
        return CreatedAtAction(nameof(GetPlayer), new { id = result.Id }, result);
    }
}
