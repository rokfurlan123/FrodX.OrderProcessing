using FrodX.OrderProcessing.EFCore.Data;

namespace FrodX.OrderProcessing.Infrastructure
{
    public class OrderGenerator
    {
        private static Random _random = new Random();
        public static List<Order> GenerateRandomOrders()
        {
            int orderCount = _random.Next(1, 6);
            var orders = new List<Order>();

            for (int i = 0; i < orderCount; i++)
            {
                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    CustomerName = GenerateRandomCustomerName(),
                    OrderDate = GenerateRandomOrderDate(),
                    Status = GenerateRandomStatus()
                };
                orders.Add(order);
            }

            return orders;
        }

        private static string GenerateRandomCustomerName()
        {
            string[] firstNames = { "Rok", "Janez", "Micka", "Jozef", "Lojze", "Dana" };
            string[] lastNames = { "Konj", "Novak", "Furlan", "Krneki", "Kavcic", "Hozntregr" };

            string firstName = firstNames[_random.Next(firstNames.Length)];
            string lastName = lastNames[_random.Next(lastNames.Length)];

            return $"{firstName} {lastName}";
        }

        private static DateTime GenerateRandomOrderDate()
        {
            DateTime startDate = DateTime.Now.AddMonths(-12);
            int range = (DateTime.Now - startDate).Days;
            return startDate.AddDays(_random.Next(range));
        }

        private static string GenerateRandomStatus()
        {
            string[] statuses = { "Pending", "Shipped", "Delivered", "Cancelled", "Returned" };
            return statuses[_random.Next(statuses.Length)];
        }
    }
}
