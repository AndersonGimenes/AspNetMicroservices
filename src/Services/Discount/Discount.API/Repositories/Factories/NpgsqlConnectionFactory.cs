using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.API.Repositories.Factories
{
    public static class NpgsqlConnectionFactory
    {
        public static NpgsqlConnection GetInstance(IConfiguration configuration) => 
            new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
    }
}
