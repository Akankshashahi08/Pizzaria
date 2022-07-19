using Microsoft.Extensions.Logging;
using Pizzaria.Entities.DataModels;
using Pizzaria.Function.Api.Processor.Interface;
using Pizzaria.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Processor
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly ILogger<OrderProcessor> logger;

        private readonly IRepositoryFactory repositoryFactory;

        public OrderProcessor(
            ILogger<OrderProcessor> logger,
            IRepositoryFactory repositoryFactory)
        {
            this.logger = logger;
            this.repositoryFactory = repositoryFactory;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var orderRepository = this.repositoryFactory.CreateRepository<Order>();
            return await orderRepository.SingleOrDefaultAsync(x => x.OrderId == orderId).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByCustomerNumberAsync(string customerNumber)
        {
            var orderRepository = this.repositoryFactory.CreateRepository<Order>();
            return await orderRepository.GetAllAsync(x => x.CustomerNumber == customerNumber).ConfigureAwait(false);
        }

        public async Task<Order> SaveOrderAsync(Order order)
        {
            var orderRepository = this.repositoryFactory.CreateRepository<Order>();
            orderRepository.Insert(order);

            return await orderRepository.SingleOrDefaultAsync(x => x.SessionId == order.SessionId).ConfigureAwait(false);
        }
    }
}
