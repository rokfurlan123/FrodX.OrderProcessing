using FrodX.OrderProcessing.EFCore.Data;

namespace FrodX.OrderProcessing.Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly OrderProcessingDbContext _context;
        public OrderService(OrderProcessingDbContext context)
        {
            _context = context;
        }
        public async Task ProcessOrders(string apiUrl, string connectionString)
        {
            var orders = await GetOrdersFromApi(apiUrl);

            if(orders == null)
            {
                //log
            }
            else
            {
                InsertOrdersInDb(orders);
            }    
        }

        private void InsertOrdersInDb(List<Order> orders)
        {
            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }

        private async Task<List<Order>?> GetOrdersFromApi(string apiUrl)
        {
            return await ApiClient.FetchApiData(apiUrl);
        }

       
    }
}
