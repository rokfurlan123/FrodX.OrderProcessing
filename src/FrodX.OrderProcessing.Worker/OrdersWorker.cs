namespace FrodX.OrderProcessing.Worker
{
    public class OrdersWorker : BackgroundService
    {
        private readonly ILogger<OrdersWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public OrdersWorker(ILogger<OrdersWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
