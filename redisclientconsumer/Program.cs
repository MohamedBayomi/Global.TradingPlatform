using Microsoft.Extensions.Configuration;

using StackExchange.Redis;


namespace redisclientconsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Redis!");

            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Read settings from configuration
            string redisConnectionString = configuration["Redis:ConnectionString"];
            string key = configuration["Settings:Key"];
            string value = configuration["Settings:Value"];
            int delay = int.Parse(configuration["Settings:Delay"]);
            int TTL = int.Parse(configuration["Settings:TTL"]);
            int e = int.Parse(configuration["Settings:Trials"]);

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);

            IDatabase db = redis.GetDatabase();
            Console.WriteLine();
            db.StringSet(key, value, new TimeSpan(0, 0, TTL));
            Console.WriteLine($"{0,4}: {DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.fff} << {value} (saved)");
            Console.WriteLine();
            for (int i = 1; i <= 10000 && e > 0; i++)
            {
                var data = db.StringGet(key);
                Console.WriteLine($"{i,4}: {DateTime.Now.TimeOfDay:hh\\:mm\\:ss\\.fff} >> {data}");
                Task.Delay(delay).Wait();
                if (string.IsNullOrEmpty(data))
                    e--;
            }

            Console.ReadLine();
        }
    }
}
