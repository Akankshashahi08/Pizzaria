using Pizzaria.Entities.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Processor.Interface
{
    public interface IOrderProcessor
    {
        Task<IEnumerable<Order>> GetAllOrdersByCustomerNumberAsync(string customerNumber);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> SaveOrderAsync(Order order);
    }
}