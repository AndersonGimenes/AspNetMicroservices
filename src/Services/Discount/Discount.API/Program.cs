using Discount.API.Extensions;
using Discount.API.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDataBase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureServices(services => services.InjectionDenpencyConfiguration());
    }
}
