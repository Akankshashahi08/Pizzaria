using System.Collections.Generic;

namespace Pizzaria.Entities
{
    public class Menu
    {
        public ICollection<ProductWrapper> Products { get; set; }
    }
}
