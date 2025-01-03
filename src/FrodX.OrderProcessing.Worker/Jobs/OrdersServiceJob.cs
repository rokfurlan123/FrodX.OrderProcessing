using Quartz;

namespace FrodX.OrderProcessing.Worker.Jobs
{
    public class OrdersServiceJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //TODO
            //call order service to fetch data from infrastructure layer
            Console.WriteLine("Test");
            return Task.CompletedTask;
        }
    }
}
