using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace FrodX.OrderProcessing.Worker.Jobs
{
    public class CustomJobFactory : SimpleJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomJobFactory(IServiceProvider serviceFactory)
        {
            _serviceProvider = serviceFactory;
        }

        public override IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                // injects dependencies
                return (IJob)_serviceProvider.GetService(bundle.JobDetail.JobType)!;
            }
            catch (Exception ex)
            {
                throw new SchedulerException(string.Format("Can't instantiate a job", bundle.JobDetail.Key), ex);
            }
        }
    }
}
