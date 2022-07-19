using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzaria.DataAccess.Sql.Configurations.Base;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.DataAccess.Sql.Configurations
{
    public class CrustConfiguration : EntityConfiguration<Crust>
    {
        public CrustConfiguration()
          : base(x => x.CrustId, "dbo", true)
        {
        }

        protected override void DoConfigure(EntityTypeBuilder<Crust> builder)
        {
            builder.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18, 0)").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        }
    }
}
