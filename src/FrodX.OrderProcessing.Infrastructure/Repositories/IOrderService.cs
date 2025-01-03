using FrodX.OrderProcessing.EFCore.Data;

namespace FrodX.OrderProcessing.Infrastructure.Repositories
{
    public interface IOrderService
    {
        void InsertOrdersInDb(List<Order> orders);
        /// <summary>
        /// Removes the already pre-existing job. Enables me to use the same job name on start
        /// </summary>
        /// <param name="name"></param>
    }
}