using System;
using Battleships.Api.Controllers;
using Battleships.Api.Mvc;
using Battleships.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.Test.Fixtures
{
    public class TestServiceProvider
    {
        public static IServiceProvider CreateProvider(Action<IServiceCollection> overrides)
        {
            var services = new ServiceCollection()
                //.AddLogging()
                .AddApiServices()
                .AddTransient(
                    provider => new BoardController(
                        provider.GetRequiredService<IBoardService>()))
                .AddTransient(
                    provider => new ShipController(
                        provider.GetRequiredService<IShipService>()));

            overrides?.Invoke(services);

            return services.BuildServiceProvider(true);
        }
    }
}