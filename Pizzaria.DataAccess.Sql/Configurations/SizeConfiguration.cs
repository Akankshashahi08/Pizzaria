using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzaria.DataAccess.Sql.Configurations.Base;
using Pizzaria.Entities.DataModels;

namespace Pizzaria.DataAccess.Sql.Configurations
{
    public class SizeConfiguration : EntityConfiguration<Size>
    {
        public SizeConfiguration()
          : base(x => x.SizeId, "dbo", true)
        {
        }

        protected override void DoConfigure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(x => x.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal(18, 0)").IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        }
    }
}
