namespace Global.TradingPlatform.Streamer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer consumer;

        public Worker(ILogger<Worker> logger, IConsumer consumer)
        {
            _logger = logger;
            this.consumer = consumer;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
                consumer.StartListening();
            }
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
            }
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
        }
    }
}
