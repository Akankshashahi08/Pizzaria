using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Entities
{
    public class ProductWrapper
    {
        public Category ProductCategory { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
