using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzaria.Entities.DataModels;
using Pizzaria.Function.Api.Processor;
using Pizzaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Tests
{
    [TestClass]
    public class OrderProcessorTests
    {
        private Mock<ILogger<OrderProcessor>> mockLogger;
        private Mock<IRepositoryFactory> mockRepositoryFactory;
        private Mock<IRepository<Order>> orderRepository;

        private OrderProcessor orderProcessor;

        [TestInitialize]
        public void Initialize()
        {
            this.mockLogger = new Mock<ILogger<OrderProcessor>>();
            this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
            this.orderRepository = new Mock<IRepository<Order>>();

            this.orderProcessor = new OrderProcessor(this.mockLogger.Object, this.mockRepositoryFactory.Object);
        }

        [TestMethod]
        public async Task OrderProcessor_ShouldGetOrderById_WhenInvokedAsync()
        {
            this.orderRepository.Setup(a => a.SingleOrDefaultAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(new Order { OrderId = 1 });
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Order>()).Returns(this.orderRepository.Object);

            var result = await orderProcessor.GetOrderByIdAsync(It.IsAny<int>()).ConfigureAwait(false);

            this.orderRepository.Verify(a => a.SingleOrDefaultAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Order>(), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.OrderId == 1);
        }

        [TestMethod]
        public async Task OrderProcessor_ShouldGetAllOrdersByCustomerNumber_WhenInvokedAsync()
        {
            this.orderRepository.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(new List<Order>());
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Order>()).Returns(this.orderRepository.Object);

            var result = await orderProcessor.GetAllOrdersByCustomerNumberAsync(It.IsAny<string>()).ConfigureAwait(false);

            this.orderRepository.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Order>(), Times.Once);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task OrderProcessor_ShouldSaveOrder_WhenInvokedAsync()
        {
            this.orderRepository.Setup(a => a.Insert(It.IsAny<Order>()));
            this.orderRepository.Setup(a => a.SingleOrDefaultAsync(It.IsAny<Expression<Func<Order, bool>>>())).ReturnsAsync(new Order { OrderId = 2 });
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Order>()).Returns(this.orderRepository.Object);

            var result = await orderProcessor.SaveOrderAsync(It.IsAny<Order>()).ConfigureAwait(false);

            this.orderRepository.Verify(a => a.Insert(It.IsAny<Order>()), Times.Once);
            this.orderRepository.Verify(a => a.SingleOrDefaultAsync(It.IsAny<Expression<Func<Order, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Order>(), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.OrderId == 2);
        }
    }
}
