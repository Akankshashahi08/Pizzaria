using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pizzaria.Entities.DataModels;
using System;
using System.Linq.Expressions;

namespace Pizzaria.DataAccess.Sql.Configurations.Base
{
    public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
         where TEntity : class, IDbEntity
    {
        /// <summary>
        /// The schema name.
        /// </summary>
        private readonly string schemaName;

        /// <summary>
        /// The identity column.
        /// </summary>
        private readonly Expression<Func<TEntity, object>> propertyExpression;

        /// <summary>
        /// The has identity.
        /// </summary>
        private readonly bool hasIdentity;

        /// <summary>
        /// The table name.
        /// </summary>
        private readonly string tableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration{TEntity}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="hasIdentity">if set to <c>true</c> [has identity].</param>
        protected EntityConfiguration(Expression<Func<TEntity, object>> propertyExpression, string schemaName, bool hasIdentity)
        {
            this.propertyExpression = propertyExpression;
            this.schemaName = schemaName;
            this.hasIdentity = hasIdentity;
            this.tableName = typeof(TEntity).Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration{TEntity}"/> class.
        /// </summary>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="hasIdentity">if set to <c>true</c> [has identity].</param>
        /// <param name="tableName">The table name.</param>
        protected EntityConfiguration(Expression<Func<TEntity, object>> propertyExpression, string schemaName, bool hasIdentity, string tableName)
        {
            this.propertyExpression = propertyExpression;
            this.schemaName = schemaName;
            this.hasIdentity = hasIdentity;
            this.tableName = tableName;
        }

        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(this.tableName, this.schemaName);
            builder.HasKey(this.propertyExpression);

            if (this.hasIdentity)
            {
                builder.Property(this.propertyExpression)
                    .HasColumnName(this.propertyExpression.GetPropertyInfo()?.Name)
                    .HasColumnType("int")
                    .IsRequired()
                    .UseSqlServerIdentityColumn();
            }

            this.DoConfigure(builder);
        }

        /// <summary>
        /// Does the configure.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected abstract void DoConfigure(EntityTypeBuilder<TEntity> builder);
    }
}
