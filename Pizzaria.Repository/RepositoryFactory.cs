using Pizzaria.DataAccess.Sql;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly ISqlDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactory" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="businessContext">The business context.</param>
        public RepositoryFactory(ISqlDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <inheritdoc/>
        public IRepository<TEntity> CreateRepository<TEntity>()
            where TEntity : class, IDbEntity
        {
            return new Repository<TEntity>(new SqlDataAccess<TEntity>(this.dataContext));
        }
    }
}
