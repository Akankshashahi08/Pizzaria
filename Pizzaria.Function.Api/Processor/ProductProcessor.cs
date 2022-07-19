using Microsoft.Extensions.Logging;
using Pizzaria.Entities;
using Pizzaria.Entities.DataModels;
using Pizzaria.Function.Api.Processor.Interface;
using Pizzaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Processor
{
    public class ProductProcessor : IProductProcessor
    {
        private readonly ILogger<ProductProcessor> logger;

        private readonly IRepositoryFactory repositoryFactory;

        public ProductProcessor(
            ILogger<ProductProcessor> logger,
            IRepositoryFactory repositoryFactory)
        {
            this.logger = logger;
            this.repositoryFactory = repositoryFactory;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categoryRepository = this.repositoryFactory.CreateRepository<Category>();
            return await categoryRepository.GetAllAsync(x => x.IsActive).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Crust>> GetAllCrustsAsync()
        {
            var crustRepository = this.repositoryFactory.CreateRepository<Crust>();
            return await crustRepository.GetAllAsync(x => x.IsActive).ConfigureAwait(false);
        }

        public async Task<Menu> GetMenuAsync()
        {
            var menu = new Menu
            {
                Products = new List<ProductWrapper>(),
            };

            var productRepository = this.repositoryFactory.CreateRepository<Product>();
            var products = await productRepository.GetAllAsync(x => x.IsActive, "Crust", "Size", "ProductCategory").ConfigureAwait(false);

            var productGrouping = products.GroupBy(a => a.ProductCategoryId);
            foreach (var group in productGrouping)
            {
                var productWrapper = new ProductWrapper
                {
                    ProductCategory = group.FirstOrDefault().ProductCategory,
                    Products = group.ToList(),
                };

                menu.Products.Add(productWrapper);
            }

            return menu;
        }

        public async Task<IEnumerable<Size>> GetAllSizesAsync()
        {
            var sizeRepository = this.repositoryFactory.CreateRepository<Size>();
            return await sizeRepository.GetAllAsync(x => x.IsActive).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Topping>> GetAllToppingsAsync()
        {
            var toppingRepository = this.repositoryFactory.CreateRepository<Topping>();
            return await toppingRepository.GetAllAsync(x => x.IsActive).ConfigureAwait(false);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var productRepository = this.repositoryFactory.CreateRepository<Product>();
            return await productRepository.FirstOrDefaultAsync(x => x.ProductId == productId && x.IsActive, "Crust", "Size", "ProductCategory").ConfigureAwait(false);
        }
    }
}
