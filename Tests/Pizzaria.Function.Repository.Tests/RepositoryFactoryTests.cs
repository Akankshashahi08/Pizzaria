using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzaria.DataAccess.Sql;
using Pizzaria.Entities.DataModels;
using Pizzaria.Repository;

namespace Pizzaria.Function.Repository.Tests
{
    [TestClass]
    public class RepositoryFactoryTests
    {
        private Mock<ISqlDataContext> mockSqlDataContext;
        private RepositoryFactory repositoryFactory;

        [TestInitialize]
        public void Initialize()
        {
            this.mockSqlDataContext = new Mock<ISqlDataContext>();

            var productMock = new Mock<DbSet<Product>>();

            this.mockSqlDataContext.Setup(a => a.Set<Product>()).Returns(productMock.Object);
            this.repositoryFactory = new RepositoryFactory(this.mockSqlDataContext.Object);
        }

        [TestMethod]
        public void RepositoryFactory_ShouldCreateRepository_WhenInvoked()
        {
            var productRepository = this.repositoryFactory.CreateRepository<Product>();

            Assert.IsNotNull(productRepository);
            this.mockSqlDataContext.Verify(a => a.Set<Product>(), Times.Once);
        }
    }
}
