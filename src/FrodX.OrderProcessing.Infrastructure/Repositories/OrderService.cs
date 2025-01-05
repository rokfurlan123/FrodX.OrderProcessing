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
                Console.WriteLine("API hasn't fetched any orders");
            }
            else
            {
                try
                {
                    InsertOrdersInDb(orders);
                    Console.WriteLine("Orders have been successfuly added to the database!");
                }
                catch(Exception ex) 
                {
                    Console.WriteLine($"{ex.Message}");
                }
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
