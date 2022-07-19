using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzaria.DataAccess.Sql.Configurations.Base;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.DataAccess.Sql.Configurations
{
    public class CategoryConfiguration : EntityConfiguration<Category>
    {
        public CategoryConfiguration()
          : base(x => x.CategoryId, "dbo", true)
        {
        }

        protected override void DoConfigure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        }
    }
}
