using Catalog.API.Data;
using Catalog.API.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.API.IoC
{
    public static class InjectionDependecyExtension
    {
        public static void InjectionDenpencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
