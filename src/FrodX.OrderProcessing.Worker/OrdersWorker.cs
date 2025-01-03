using FrodX.OrderProcessing.EFCore.Data;
using FrodX.OrderProcessing.Infrastructure.Repositories;

namespace FrodX.OrderProcessing.Worker
{
    public class OrdersWorker : BackgroundService
    {
        private readonly ILogger<OrdersWorker> _logger;

        public OrdersWorker(ILogger<OrdersWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}
                //await Task.Delay(15000, stoppingToken);
            }
        }
    }
}
