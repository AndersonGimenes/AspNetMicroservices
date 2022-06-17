using AutoMapper;
using Discount.Grpc.Mapper;
using Discount.Grpc.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Grpc.IoC
{
    public static class InjectionDependecyExtension
    {
        public static void InjectionDenpencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            
            services.AddSingleton(
                new MapperConfiguration(opts => opts.AddProfile(new DiscountProfile()))
                .CreateMapper()
            );
        }
    }
}
