using Discount.API.IoC.Factories;
using Discount.API.Repositories.MigrationQueries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Discount.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDataBase(this IHost host, int retry = default)
        {
            var retryFor = retry;

            using (var scope = host.Services.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                try
                {
                    using (var connection = NpgsqlConnectionFactory.GetInstance(configuration))
                    {
                        connection.Open();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = Migration.Query;

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                    if(retryFor < 10)
                    {
                        retryFor++;
                        System.Threading.Thread.Sleep(2000); // two seconds
                        MigrateDataBase(host, retryFor);
                    }
                }
            }

            return host;
        }
    }
}
