using Basket.API.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.IoC
{
    public static class InjectionDependecyExtension
    {
        public static void InjectionDenpencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();            
        }

        public static void RedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetValue<string>("RedisConfiguration:ConnectionString"));
        }
    }
}
