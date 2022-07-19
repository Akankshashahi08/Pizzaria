using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Entities.DataModels
{
    public class Order : IDbEntity
    {
        public int OrderId { get; set; }
        public string OrderDetails { get; set; }
        public string CustomerNumber { get; set; }
        public Guid SessionId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public Status Status { get; set; }

        public OrderedProduct Products { get; set; }
    }
}
