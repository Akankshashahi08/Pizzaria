using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzaria.DataAccess.Sql.Configurations.Base;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.DataAccess.Sql.Configurations
{
    public class OrderConfiguration : EntityConfiguration<Order>
    {
        public OrderConfiguration()
          : base(x => x.OrderId, "dbo", true)
        {
        }

        protected override void DoConfigure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderDetails).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(x => x.CustomerNumber).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.SessionId).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18, 0)").IsRequired();
            builder.Property(x => x.OrderDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.DeliveryDateTime).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Status).HasColumnType("int").IsRequired();

            builder.Ignore(x => x.Products);
        }
    }
}
