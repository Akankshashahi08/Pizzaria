using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pizzaria.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IDbEntity
    {
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties);
        Task<TEntity> GetByIdAsync(object id);
        void Insert(TEntity entity);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties);
        void Update(TEntity entity);
    }
}