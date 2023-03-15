using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Contexts;
using TicTacToe.Repositories;
using TicTacToe.Repositories.Abstractions;

namespace TicTacToe;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
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

        return services;
    }
}
