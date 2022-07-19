using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzaria.DataAccess.Sql.Configurations.Base;
using Pizzaria.Entities.DataModels;

namespace Pizzaria.DataAccess.Sql.Configurations
{
    public class ProductConfiguration : EntityConfiguration<Product>
    {
        public ProductConfiguration()
          : base(x => x.ProductId, "dbo", true)
        {
        }

        protected override void DoConfigure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Image).HasColumnType("nvarchar(MAX)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18, 0)").IsRequired();

            builder.HasOne(t => t.ProductCategory)
               .WithMany(p => p.Products)
               .HasForeignKey(d => d.ProductCategoryId)
               .IsRequired();

            builder.HasOne(t => t.Size)
               .WithMany(p => p.Products)
               .HasForeignKey(d => d.SizeId);

            builder.HasOne(t => t.Crust)
               .WithMany(p => p.Products)
               .HasForeignKey(d => d.CrustId);

            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
            builder.Property(x => x.Quantity).HasColumnType("int").IsRequired();

            builder.Ignore(x => x.PizzaToppings);
        }
    }
}
