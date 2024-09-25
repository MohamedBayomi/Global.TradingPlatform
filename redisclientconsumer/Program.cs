using StackExchange.Redis;


namespace redisclientconsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            IDatabase db = redis.GetDatabase();
            db.StringSet("first", "Omar", new TimeSpan(0,0,5));
            db.StringSet("second", "Ahmed", new TimeSpan(0,0,5));


            Console.ReadLine();
        }
    }
}
