using Basket.API.Repositories;
using Basket.API.Services;
using Discount.Grpc.Protos;
using MassTransit;
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
            services.AddAutoMapper(typeof(Startup));
        }

        public static IServiceCollection RedisConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
                options.Configuration = configuration.GetValue<string>("RedisConfiguration:ConnectionString"));

            return services;
        }

        public static IServiceCollection GrpcClientConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
                   options.Address = new Uri(configuration.GetValue<string>("GrpcSettings:DiscountUrl"))
               );

            return services;
        }

        public static void RabbitMqConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);
                });
            });
            services.AddMassTransitHostedService();
        }
    }
}
