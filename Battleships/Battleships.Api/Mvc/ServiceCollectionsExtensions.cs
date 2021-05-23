using Battleships.Api.Filters;
using Battleships.Data;
using Battleships.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.Api.Mvc
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddApplicationServices();
            services.AddBattleshipRepository();
            services.AddMvcCore(options => options.Filters.Add<ExceptionFilter>());

            return services;
        }
    }
}