using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Entities.DataModels
{
    public class Category : IDbEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
