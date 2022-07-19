using Pizzaria.DataAccess.Sql;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pizzaria.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntity
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly IDataAccess<TEntity> dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public Repository(IDataAccess<TEntity> dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        /// <inheritdoc/>
        public virtual void Insert(TEntity entity)
        {
            this.dataAccess.Insert(entity);
        }

        /// <inheritdoc/>
        public virtual void Update(TEntity entity)
        {
            this.dataAccess.Update(entity);
        }

        /// <inheritdoc/>
        public virtual void Delete(TEntity entity)
        {
            this.dataAccess.Delete(entity);
        }

        /// <inheritdoc/>
        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return this.dataAccess.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public virtual Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            return this.dataAccess.GetAllAsync(predicate, includeProperties);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate, params string[] includeProperties)
            where T : class, IDbEntity
        {
            return this.dataAccess.GetAllAsync(predicate, includeProperties);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            return this.dataAccess.FirstOrDefaultAsync(predicate, includeProperties);
        }

        public virtual Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return this.dataAccess.SingleOrDefaultAsync(predicate);
        }
    }
}
