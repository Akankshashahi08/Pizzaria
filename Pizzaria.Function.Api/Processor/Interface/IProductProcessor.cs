using Pizzaria.Entities;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria.Function.Api.Processor.Interface
{
    public interface IProductProcessor
    {
        Task<Product> GetProductByIdAsync(int productId);

        Task<Menu> GetMenuAsync();

        Task<IEnumerable<Size>> GetAllSizesAsync();

        Task<IEnumerable<Topping>> GetAllToppingsAsync();

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<Crust>> GetAllCrustsAsync();
    }
}
