using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Repositories.Abstractions;
using TicTacToe.Infrastructure.Repositories;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.Against.Null(services, nameof(services));

        services.AddScoped(provider =>
        {
            var configurationString = configuration.GetConnectionString(nameof(AppDbContext));
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(configurationString);
            var options = optionsBuilder.Options;
            return new AppDbContext(options);
        });

        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<IGameRepository, GameRepository>();

        return services;
    }
}
