using Discount.API.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.API.IoC
{
    public static class InjectionDependecyExtension
    {
        public static void InjectionDenpencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
        }
    }
}
