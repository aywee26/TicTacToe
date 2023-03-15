using AutoMapper;
using TicTacToe.Application.Features.Players.Models;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Features.Players.Profiles;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDto>()
            .ConstructUsing(p => new PlayerDto(p.Id, p.Name));
    }
}
