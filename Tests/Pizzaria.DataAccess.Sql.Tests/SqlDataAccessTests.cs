using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzaria.DataAccess.Sql;
using Pizzaria.Entities.DataModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pizzaria.Function.Repository.Tests
{
    [TestClass]
    public class SqlDataAccessTests
    {
        private Mock<ISqlDataContext> mockSqlDataContext;
        private SqlDataContext dataContext;
        private SqlDataAccess<Size> dataAccess;

        [TestInitialize]
        public void Initialize()
        {
            this.mockSqlDataContext = new Mock<ISqlDataContext>();
            var options = new DbContextOptionsBuilder<SqlDataContext>()
                .UseInMemoryDatabase(databaseName: $"InMemoryDatabase_{Guid.NewGuid()}")
                .Options;

            this.dataContext = new SqlDataContext(options);
            this.dataAccess = new SqlDataAccess<Size>(this.dataContext);
        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldInsertProduct_ToDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);
            var rows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(1, rows);

            var result = await this.dataAccess.GetByIdAsync(size.SizeId).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.AreEqual(size.Name, result.Name);
            Assert.AreEqual(size.Price, result.Price);
            Assert.AreEqual(size.IsActive, result.IsActive);

            await this.CleanUpEntitiesAsync(size).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldUpdateProduct_ToDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);
            await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);
            var result = await this.dataAccess.GetByIdAsync(size.SizeId).ConfigureAwait(false);

            result.Name = "large";

            this.dataAccess.Update(result);
            var updatedRows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(1, updatedRows);

            var updatedResult = await this.dataAccess.GetByIdAsync(result.SizeId).ConfigureAwait(false);

            Assert.IsNotNull(updatedResult);
            Assert.AreEqual(result.Price, updatedResult.Price);
            Assert.AreEqual(result.Price, updatedResult.Price);
            Assert.AreEqual(result.IsActive, updatedResult.IsActive);
            Assert.AreEqual(result.SizeId, updatedResult.SizeId);

            await this.CleanUpEntitiesAsync(size).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldDeleteProduct_ToDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);
            await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            var result = await this.dataAccess.GetByIdAsync(size.SizeId).ConfigureAwait(false);

            this.dataAccess.Delete(result);
            var deletedRows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(1, deletedRows);

            var deletedResult = await this.dataAccess.GetByIdAsync(size.SizeId).ConfigureAwait(false);

            Assert.IsNull(deletedResult);

        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldGetProductById_fromDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);

            var savedRows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(1, savedRows);

            var result = await this.dataAccess.GetByIdAsync(size.SizeId).ConfigureAwait(false);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldGetFirstOrDefaultAsync_fromDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);
            var rows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            var deletedRows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(1, deletedRows);

            var result = await this.dataAccess.FirstOrDefaultAsync(x => x.SizeId == 1).ConfigureAwait(false);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldGetSingleOrDefaultAsync_fromDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);
            var savedRows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);

            Assert.AreEqual(1, savedRows);

            var result = await this.dataAccess.SingleOrDefaultAsync(x => x.SizeId == 1).ConfigureAwait(false);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task SqlDataAccess_ShouldGetAllAsync_fromDataBase_WhenInvokedAsync()
        {
            var size = new Size
            {
                Name = "Medium",
                Price = 200,
                IsActive = true,
            };

            this.dataAccess.Insert(size);
            await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);
            var sizeone = new Size
            {
                Name = "Small",
                Price = 200,
                IsActive = true,
            };
            this.dataAccess.Insert(sizeone);

            await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);
            var sizetwo = new Size
            {
                Name = "Large",
                Price = 200,
                IsActive = true,
            };
            this.dataAccess.Insert(sizetwo);

            var rows = await this.dataContext.SaveAsync(CancellationToken.None).ConfigureAwait(false);
            var result = await this.dataAccess.GetAllAsync(x => x.IsActive).ConfigureAwait(false);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToList().Count, 3);
        }

        private Task CleanUpEntitiesAsync(params Size[] entities)
        {
            this.dataContext.RemoveRange(entities);
            return this.dataContext.SaveChangesAsync();
        }
    }
}
