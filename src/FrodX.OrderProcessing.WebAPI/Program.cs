using FrodX.OrderProcessing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/orders", () =>
{
    return OrderGenerator.GenerateRandomOrders().ToArray();
});

app.Run();
