using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basket.API.IoC
{
    public static class InjectionDependecyExtension
    {
        public static void InjectionDenpencyConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();  
            services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();

            
        }

        public static IServiceCollection RedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetValue<string>("RedisConfiguration:ConnectionString"));

            return services;
        }

        public static void GrpcClientConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
                   options.Address = new Uri(configuration.GetValue<string>("GrpcSettings:DiscountUrl"))
               );
        }
    }
}
