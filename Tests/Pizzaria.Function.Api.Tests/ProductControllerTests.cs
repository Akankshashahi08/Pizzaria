using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzaria.Entities;
using Pizzaria.Entities.DataModels;
using Pizzaria.Function.Api.Controller;
using Pizzaria.Function.Api.Processor.Interface;
using Pizzaria.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<ILogger<ProductController>> mockLogger;
        private Mock<IProductProcessor> mockProductProcessor;

        private ProductController productController;

        [TestInitialize]
        public void Initialize()
        {
            this.mockLogger = new Mock<ILogger<ProductController>>();
            this.mockProductProcessor = new Mock<IProductProcessor>();

            this.productController = new ProductController(this.mockLogger.Object, this.mockProductProcessor.Object);
        }

        [TestMethod]
        public async Task ProductController_ShouldGetProductById_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockProductProcessor.Setup(a => a.GetProductByIdAsync(It.IsAny<int>())).ReturnsAsync(new Product());

            var result = await productController.GetProductByIdAsync(mockRequest.Object, It.IsAny<int>()).ConfigureAwait(false);

            mockProductProcessor.Verify(a => a.GetProductByIdAsync(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductController_ShouldGetMenu_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockProductProcessor.Setup(a => a.GetMenuAsync()).ReturnsAsync(new Menu());

            var result = await productController.GetMenuAsync(mockRequest.Object).ConfigureAwait(false);

            mockProductProcessor.Verify(a => a.GetMenuAsync(), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductController_ShouldGetAllCategories_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockProductProcessor.Setup(a => a.GetAllCategoriesAsync()).ReturnsAsync(new List<Category> { new Category() });

            var result = await productController.GetAllCategoriesAsync(mockRequest.Object).ConfigureAwait(false);

            mockProductProcessor.Verify(a => a.GetAllCategoriesAsync(), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductController_ShouldGetAllCrust_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockProductProcessor.Setup(a => a.GetAllCrustsAsync()).ReturnsAsync(new List<Crust> { new Crust() });

            var result = await productController.GetAllCrustAsync(mockRequest.Object).ConfigureAwait(false);

            mockProductProcessor.Verify(a => a.GetAllCrustsAsync(), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductController_ShouldGetAllToppings_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockProductProcessor.Setup(a => a.GetAllToppingsAsync()).ReturnsAsync(new List<Topping> { new Topping() });

            var result = await productController.GetAllToppingsAsync(mockRequest.Object).ConfigureAwait(false);

            mockProductProcessor.Verify(a => a.GetAllToppingsAsync(), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductController_ShouldGetAllSize_WhenInvokedAsync()
        {
            var mockRequest = new Mock<HttpRequest>();
            this.mockProductProcessor.Setup(a => a.GetAllSizesAsync()).ReturnsAsync(new List<Size> { new Size() });

            var result = await productController.GetAllSizesAsync(mockRequest.Object).ConfigureAwait(false);

            mockProductProcessor.Verify(a => a.GetAllSizesAsync(), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}
