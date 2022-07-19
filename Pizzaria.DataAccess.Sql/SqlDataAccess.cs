using Microsoft.EntityFrameworkCore;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pizzaria.DataAccess.Sql
{
    public class SqlDataAccess<TEntity> : ISqlDataAccess<TEntity>
      where TEntity : class, IDbEntity
    {
        /// <summary>
        /// The database context.
        /// </summary>
        private readonly ISqlDataContext dataContext;

        /// <summary>
        /// The data set.
        /// </summary>
        private readonly DbSet<TEntity> dataset;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDataAccess{TEntity}" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="businessContext">The business context.</param>
        public SqlDataAccess(ISqlDataContext dataContext)
        {
            this.dataContext = dataContext;
            this.dataset = this.dataContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public DbSet<TEntity> EntitySet()
        {
            return this.dataset;
        }

        /// <inheritdoc/>
        public DbSet<T> Set<T>()
            where T : class, IDbEntity
        {
            return this.dataContext.Set<T>();
        }

        /// <inheritdoc/>
        public void Insert(TEntity entity)
        {
            this.dataset.Add(entity);
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            this.dataset.Attach(entity);
            this.dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public void Delete(TEntity entity)
        {
            this.dataset.Remove(entity);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetByIdAsync(object id)
        {
            return this.dataset.FindAsync(id);
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null
                ? this.dataset.SingleOrDefaultAsync(predicate)
                : this.dataset.SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync().ConfigureAwait(false);
        }
    }
}
