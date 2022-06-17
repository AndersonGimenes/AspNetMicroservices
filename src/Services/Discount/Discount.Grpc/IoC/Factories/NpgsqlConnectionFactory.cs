using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc.IoC.Factories
{
    public static class NpgsqlConnectionFactory
    {
        public static NpgsqlConnection GetInstance(IConfiguration configuration) => 
            new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
    }
}
