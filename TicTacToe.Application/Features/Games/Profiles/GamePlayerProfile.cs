using AutoMapper;
using TicTacToe.Application.Features.Games.Models;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Features.Games.Profiles;

public class GamePlayerProfile : Profile
{
    public GamePlayerProfile()
    {
        CreateMap<GamePlayer, GamePlayerDto>()
            .ConstructUsing(gp => new GamePlayerDto(gp.Player.Id, gp.Player.Name));
    }
}
