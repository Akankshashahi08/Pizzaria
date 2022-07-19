using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Entities.DataModels
{
    public class Product : IDbEntity
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public int? SizeId { get; set; }
        public int? CrustId { get; set; }
        public bool IsActive { get; set; }
        public int Quantity { get; set; }

        public virtual Size Size { get; set; }
        public virtual Crust Crust { get; set; }
        public virtual Category ProductCategory { get; set; }

        public ICollection<Topping> PizzaToppings { get; set; }
    }
}
