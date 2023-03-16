using AutoMapper;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Features.Games.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>()
            .ConstructUsing(g => new GameDto(
                g.Id,
                g.CreatedAt,
                g.GamePlayers.Select(gp => new GamePlayerDto(gp.Player.Id, gp.Player.Name)),
                g.Status,
                g.State
            ));
    }
}
