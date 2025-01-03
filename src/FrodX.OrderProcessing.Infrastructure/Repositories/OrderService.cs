using FrodX.OrderProcessing.EFCore;
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
        public void InsertOrdersInDb(List<Order> orders)
        {
            _context.Orders.AddRange(orders);
            _context.SaveChanges();
        }
    }
}
