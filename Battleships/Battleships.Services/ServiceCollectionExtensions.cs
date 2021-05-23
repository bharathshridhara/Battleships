using Battleships.Services.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IBoardService, BoardService>();
            services.AddSingleton<IShipService, ShipService>();

            return services;
        }
    }
}