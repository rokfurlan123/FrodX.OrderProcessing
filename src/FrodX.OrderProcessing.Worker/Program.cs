using FrodX.OrderProcessing.EFCore.Data;
using FrodX.OrderProcessing.Infrastructure.Repositories;
using FrodX.OrderProcessing.Worker;
using FrodX.OrderProcessing.Worker.Jobs;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<OrderProcessingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

IServiceCollection services;
IServiceProvider provider;
ISchedulerFactory schedulerFactory;
IScheduler _scheduler;

services = new ServiceCollection();
schedulerFactory = new StdSchedulerFactory();
_scheduler = await schedulerFactory.GetScheduler();

//Register services for job factory
services.AddTransient<OrdersServiceJob>();
services.AddScoped<IOrderService, OrderService>();
services.AddDbContext<OrderProcessingDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
provider = services.BuildServiceProvider();

//.Net DI services 
builder.Services.AddHostedService<OrdersWorker>();

//Setup job factory
_scheduler.JobFactory = new CustomJobFactory(provider);
await _scheduler.Start();

IJobDetail job = JobBuilder.Create<OrdersServiceJob>()
                            .UsingJobData("apiUrl", builder.Configuration.GetSection("ApiUrl").Value!)
                            .UsingJobData("connectionString", connectionString)
                            .WithIdentity(builder.Configuration.GetSection("JobConfiguration:Name").Value!)
                            .StoreDurably()
                            .RequestRecovery()
                            .Build();

ITrigger trigger = TriggerBuilder.Create()
                                 .WithIdentity(builder.Configuration.GetSection("TriggerConfiguration:Name").Value!)
                                 .StartNow()
                                 .WithSimpleSchedule(z => z.WithIntervalInMinutes(builder.Configuration.GetValue<int>("JobInterval"))
                                 .RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires())
                                 .Build();

await _scheduler.ScheduleJob(job, trigger);

var host = builder.Build();

host.Run();
