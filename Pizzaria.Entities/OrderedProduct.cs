using Pizzaria.Entities.DataModels;
using System.Collections.Generic;

namespace Pizzaria.Entities
{
    public class OrderedProduct
    {
        public ICollection<Product> Pizzas { get; set; }
        public ICollection<Product> Beverages { get; set; }
        public ICollection<Product> Sides { get; set; }
    }
}
