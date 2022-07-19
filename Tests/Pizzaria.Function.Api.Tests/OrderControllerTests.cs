using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Pizzaria.Entities.DataModels;
using Pizzaria.Function.Api.Controller;
using Pizzaria.Function.Api.Processor.Interface;
using System.IO;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<ILogger<OrderController>> mockLogger;
        private Mock<IOrderProcessor> mockOrderProcessor;

        private OrderController orderController;

        [TestInitialize]
        public void Initialize()
        {
            this.mockLogger = new Mock<ILogger<OrderController>>();
            this.mockOrderProcessor = new Mock<IOrderProcessor>();

            this.orderController = new OrderController(this.mockLogger.Object, this.mockOrderProcessor.Object);
        }

        [TestMethod]
        public async Task OrderController_ShouldGetOrderById_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockOrderProcessor.Setup(a => a.GetOrderByIdAsync(It.IsAny<int>())).ReturnsAsync(new Order());

            var result = await orderController.GetOrderByIdAsync(mockRequest.Object, It.IsAny<int>()).ConfigureAwait(false);

            mockOrderProcessor.Verify(a => a.GetOrderByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task OrderController_ShouldGetMenuSaveOrder_WhenInvokedAsync()
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            var json = JsonConvert.SerializeObject(new Order());
            sw.Write(json);
            sw.Flush();
            ms.Position = 0;

            var mockRequest = new Mock<HttpRequest>();
            mockRequest.Setup(x => x.Body).Returns(ms);

            this.mockOrderProcessor.Setup(a => a.SaveOrderAsync(It.IsAny<Order>())).ReturnsAsync(new Order());

            var result = await orderController.SaveOrderAsync(mockRequest.Object).ConfigureAwait(false);

            mockOrderProcessor.Verify(a => a.SaveOrderAsync(It.IsAny<Order>()), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}
