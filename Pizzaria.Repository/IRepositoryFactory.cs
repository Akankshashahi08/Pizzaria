using Pizzaria.Entities.DataModels;

namespace Pizzaria.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class, IDbEntity;
    }
}