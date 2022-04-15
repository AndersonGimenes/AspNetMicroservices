using Basket.API.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.API.IoC
{
    public static class InjectionDependecyExtension
    {
        public static void InjectionDenpencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
        }
    }
}
