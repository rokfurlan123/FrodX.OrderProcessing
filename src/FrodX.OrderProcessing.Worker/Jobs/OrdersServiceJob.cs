using FrodX.OrderProcessing.EFCore.Data;
using FrodX.OrderProcessing.Infrastructure.Repositories;
using Quartz;

namespace FrodX.OrderProcessing.Worker.Jobs
{
    public class OrdersServiceJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        public OrdersServiceJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<OrderProcessingDbContext>();
                var orderService = scope.ServiceProvider.GetService<IOrderService>();
                if (orderService == null)
                {
                    //TODO
                    //log
                }
                else
                {
                    await orderService.ProcessOrders((string)context.JobDetail.JobDataMap.Get("apiUrl"), 
                        (string)context.JobDetail.JobDataMap.Get("connectionString"));
                }
            }

            Console.WriteLine($"Job completed! time: {DateTime.Now}");
        }
    }
}
