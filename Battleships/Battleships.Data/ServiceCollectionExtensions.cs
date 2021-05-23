using Battleships.Data.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBattleshipRepository(this IServiceCollection services)
        {
            services.AddSingleton<IBattleshipsRepository, BattleshipRepository>();

            return services;
        }
    }
}