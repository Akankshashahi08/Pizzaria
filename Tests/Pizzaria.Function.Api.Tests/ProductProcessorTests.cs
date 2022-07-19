using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzaria.Entities;
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
    public class ProductProcessorTests
    {
        private Mock<ILogger<ProductProcessor>> mockLogger;
        private Mock<IRepositoryFactory> mockRepositoryFactory;

        private ProductProcessor productProcessor;

        [TestInitialize]
        public void Initialize()
        {
            this.mockLogger = new Mock<ILogger<ProductProcessor>>();
            this.mockRepositoryFactory = new Mock<IRepositoryFactory>();
            this.productProcessor = new ProductProcessor(this.mockLogger.Object, this.mockRepositoryFactory.Object);
        }

        [TestMethod]
        public async Task ProductProcessor_ShouldGetAllCategories_WhenInvokedAsync()
        {
            var categoryRepository = new Mock<IRepository<Category>>();
            categoryRepository.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>())).ReturnsAsync(new List<Category> { new Category() });
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Category>()).Returns(categoryRepository.Object);

            var result = await productProcessor.GetAllCategoriesAsync().ConfigureAwait(false);

            categoryRepository.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Category>(), Times.Once);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductProcessor_ShouldGetAllCrusts_WhenInvokedAsync()
        {
            var crustRepository = new Mock<IRepository<Crust>>();
            crustRepository.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Crust, bool>>>())).ReturnsAsync(new List<Crust> { new Crust() });
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Crust>()).Returns(crustRepository.Object);

            var result = await productProcessor.GetAllCrustsAsync().ConfigureAwait(false);

            crustRepository.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Crust, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Crust>(), Times.Once);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductProcessor_ShouldGetAllSizes_WhenInvokedAsync()
        {
            var sizeRepository = new Mock<IRepository<Size>>();
            sizeRepository.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Size, bool>>>())).ReturnsAsync(new List<Size> { new Size() });
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Size>()).Returns(sizeRepository.Object);

            var result = await productProcessor.GetAllSizesAsync().ConfigureAwait(false);

            sizeRepository.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Size, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Size>(), Times.Once);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductProcessor_ShouldGetAllToppings_WhenInvokedAsync()
        {
            var toppingRepository = new Mock<IRepository<Topping>>();
            toppingRepository.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Topping, bool>>>())).ReturnsAsync(new List<Topping> { new Topping() });
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Topping>()).Returns(toppingRepository.Object);

            var result = await productProcessor.GetAllToppingsAsync().ConfigureAwait(false);

            toppingRepository.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Topping, bool>>>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Topping>(), Times.Once);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductProcessor_ShouldGetProductById_WhenInvokedAsync()
        {
            var productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(a => a.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(new Product());
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Product>()).Returns(productRepository.Object);

            var result = await productProcessor.GetProductByIdAsync(It.IsAny<int>()).ConfigureAwait(false);

            productRepository.Verify(a => a.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Product>(), Times.Once);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task ProductProcessor_ShouldGetMenu_WhenInvokedAsync()
        {
            var productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>()))
                .ReturnsAsync(new List<Product>());
            this.mockRepositoryFactory.Setup(a => a.CreateRepository<Product>()).Returns(productRepository.Object);

            var result = await productProcessor.GetMenuAsync().ConfigureAwait(false);

            productRepository.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>()), Times.Once);
            this.mockRepositoryFactory.Verify(a => a.CreateRepository<Product>(), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.GetType() == typeof(Menu));
        }
    }
}
