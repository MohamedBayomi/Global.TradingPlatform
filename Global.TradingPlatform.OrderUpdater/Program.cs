using Microsoft.EntityFrameworkCore;

namespace Global.TradingPlatform.OrderUpdater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Consumer>(); // Register the background worker
                    services.AddDbContext<TradingPlatformContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("BrokerageConnection")));

                });
    }
}