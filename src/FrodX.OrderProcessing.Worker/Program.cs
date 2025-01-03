using System.Collections.Specialized;
using FrodX.OrderProcessing.EFCore.Data;
using FrodX.OrderProcessing.Infrastructure.Repositories;
using FrodX.OrderProcessing.Worker;
using FrodX.OrderProcessing.Worker.Jobs;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddHostedService<OrdersWorker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OrderProcessingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

NameValueCollection properties = new()
{
    ["quartz.jobStore.tablePrefix"] = "QRTZ_",
};

var scheduler = await SchedulerBuilder
    .Create(properties)
    .UseDefaultThreadPool(x => x.MaxConcurrency = 3)
    .WithMisfireThreshold(TimeSpan.FromSeconds(10))
    .UsePersistentStore(x =>
    {
        x.UseProperties = true;
        x.UseSqlServer(connectionString!);
        x.UseSystemTextJsonSerializer();
        x.PerformSchemaValidation = true;
    }).BuildScheduler();

//not for production - only for demo project
var previousJobKey = new JobKey(builder.Configuration.GetSection("JobConfiguration:Name").Value!);
await scheduler.DeleteJob(previousJobKey);

await scheduler.Start();

var jobKey = new JobKey(builder.Configuration.GetSection("JobConfiguration:Name").Value!);

var jobTrigger = TriggerBuilder.Create()
    .WithIdentity(builder.Configuration.GetSection("TriggerConfiguration:Name").Value!)
    .ForJob(jobKey)
    .StartAt(DateTimeOffset.Now)
    .WithSimpleSchedule(x => x.WithIntervalInSeconds(builder.Configuration.GetValue<int>("JobMinuteInterval"))
        .RepeatForever())
    .Build();

var jobDetail = JobBuilder
    .Create<OrdersServiceJob>()
    .WithIdentity(jobKey)
    .StoreDurably()
    .Build();

await scheduler.ScheduleJob(jobDetail, jobTrigger);

var host = builder.Build();

host.Run();
