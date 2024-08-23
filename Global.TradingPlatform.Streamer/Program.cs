namespace Global.TradingPlatform.Streamer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddSingleton<IConsumer, Consumer>();
            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}