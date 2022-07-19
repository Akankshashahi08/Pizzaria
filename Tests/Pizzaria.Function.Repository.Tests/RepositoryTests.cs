using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzaria.DataAccess.Sql;
using Pizzaria.Entities.DataModels;
using Pizzaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pizzaria.Function.Repository.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private Repository<Product> repository;
        private Mock<IDataAccess<Product>> mockDataAccess;

        [TestInitialize]
        public void Initialize()
        {
            this.mockDataAccess = new Mock<IDataAccess<Product>>();
            this.repository = new Repository<Product>(this.mockDataAccess.Object);
        }

        [TestMethod]
        public void Repository_ShouldInsert_WhenInvoked()
        {
            var product = new Product();
            this.mockDataAccess.Setup(a => a.Insert(It.IsAny<Product>()));

            this.repository.Insert(product);

            this.mockDataAccess.Verify(a => a.Insert(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void Repository_ShouldUpdate_WhenInvoked()
        {
            var product = new Product();
            this.mockDataAccess.Setup(a => a.Update(It.IsAny<Product>()));

            this.repository.Update(product);

            this.mockDataAccess.Verify(a => a.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void Repository_ShouldDelete_WhenInvoked()
        {
            var product = new Product();
            this.mockDataAccess.Setup(a => a.Delete(It.IsAny<Product>()));

            this.repository.Delete(product);

            this.mockDataAccess.Verify(a => a.Delete(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public async Task Repository_ShouldGetById_WhenInvokedAsync()
        {
            var product = new Product { ProductId = 1 };
            this.mockDataAccess.Setup(a => a.GetByIdAsync(It.IsAny<object>())).ReturnsAsync(product);

            var result = await this.repository.GetByIdAsync(product).ConfigureAwait(false);

            this.mockDataAccess.Verify(a => a.GetByIdAsync(It.IsAny<object>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(product.ProductId == result.ProductId);
        }

        [TestMethod]
        public async Task Repository_ShouldGetAll_WhenInvokedAsync()
        {
            var product = new Product { ProductId = 1 };
            this.mockDataAccess.Setup(a => a.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>())).ReturnsAsync(new List<Product> { product });

            var result = await this.repository.GetAllAsync(a => a.ProductId == 1, "crust").ConfigureAwait(false);

            this.mockDataAccess.Verify(a => a.GetAllAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ToList().Count > 0);
        }

        [TestMethod]
        public async Task Repository_ShouldFirstOrDefault_WhenInvokedAsync()
        {
            var product = new Product { ProductId = 1 };
            this.mockDataAccess.Setup(a => a.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>())).ReturnsAsync(product);

            var result = await this.repository.FirstOrDefaultAsync(a => a.ProductId == 1, "crust").ConfigureAwait(false);

            this.mockDataAccess.Verify(a => a.FirstOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<string[]>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ProductId == product.ProductId);
        }

        [TestMethod]
        public async Task Repository_ShouldSingleOrDefault_WhenInvokedAsync()
        {
            var product = new Product { ProductId = 1 };
            this.mockDataAccess.Setup(a => a.SingleOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(product);

            var result = await this.repository.SingleOrDefaultAsync(a => a.ProductId == 1).ConfigureAwait(false);

            this.mockDataAccess.Verify(a => a.SingleOrDefaultAsync(It.IsAny<Expression<Func<Product, bool>>>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ProductId == product.ProductId);
        }
    }
}
