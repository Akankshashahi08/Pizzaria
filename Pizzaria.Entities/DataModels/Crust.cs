using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Entities.DataModels
{
    public class Crust : IDbEntity
    {
        public int CrustId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
