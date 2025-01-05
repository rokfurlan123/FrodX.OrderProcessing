namespace FrodX.OrderProcessing.Infrastructure.Repositories
{
    public interface IOrderService
    {
        Task ProcessOrders(string apiUrl, string connectionString);
    }
}