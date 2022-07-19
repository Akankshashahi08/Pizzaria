using Microsoft.EntityFrameworkCore;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
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
        public void InsertAll(IEnumerable<TEntity> entities)
        {
            this.dataset.AddRange(entities);
        }

        /// <inheritdoc/>
        public void UpdateAll(IEnumerable<TEntity> entities)
        {
            this.dataset.UpdateRange(entities);
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
        public void DeleteAll(IEnumerable<TEntity> entities)
        {
            dataset.RemoveRange(entities);
        }

        /// <inheritdoc/>
        public Task<TEntity> GetByIdAsync(object id)
        {
            return this.dataset.FindAsync(id);
        }

        /// <inheritdoc/>
        public Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate == null ? this.dataset.LongCountAsync() : this.dataset.LongCountAsync(predicate);
        }

        /// <inheritdoc/>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null
                ? this.dataset.FirstOrDefaultAsync(predicate)
                : this.dataset.FirstOrDefaultAsync();
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
        public Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Select(selector).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return predicate != null
                ? this.dataset.SingleOrDefaultAsync(predicate)
                : this.dataset.SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return query.Select(selector).SingleOrDefaultAsync();
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

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TResult, bool>> predicate, params string[] includeProperties)
            where TResult : class, IDbEntity
        {
            IQueryable<TResult> query = this.Set<TResult>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return await query.Select(selector).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> OrderByAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, int? take)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> OrderByDescendingAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy, int? take)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = query.OrderByDescending(orderBy);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<IQueryable<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
        {
            IQueryable<TEntity> query = this.dataset;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return Task.FromResult(query);
        }

        /// <inheritdoc/>
        public Task<IQueryable<TEntity>> ExecuteViewAsync()
        {
            return Task.FromResult(this.dataContext.Query<TEntity>().AsQueryable());
        }

        private static SqlParameter BuildParameters(KeyValuePair<string, object> parameter)
        {
            var direction = parameter.Key.StartsWith("@out_", StringComparison.OrdinalIgnoreCase) ? ParameterDirection.Output : ParameterDirection.Input;
            var p = new SqlParameter
            {
                ParameterName = parameter.Key,
                Direction = direction,
            };

            // Output parameters of int type are only supported as of now
            if (direction == ParameterDirection.Output)
            {
                p.SqlDbType = SqlDbType.Int;
            }

            if (direction == ParameterDirection.Input)
            {
                p.Value = parameter.Value ?? DBNull.Value;
            }

            if (!(parameter.Value is DataTable dt))
            {
                return p;
            }

            p.TypeName = dt.TableName;
            p.SqlDbType = SqlDbType.Structured;

            return p;
        }
    }
}
