using Microsoft.EntityFrameworkCore;
using Pizzaria.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.DataAccess.Sql
{
    public interface ISqlDataAccess<TEntity> : IDataAccess<TEntity>
       where TEntity : class, IDbEntity
    {
        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>The DB Set.</returns>
        DbSet<TEntity> EntitySet();

        /// <summary>
        /// Sets this instance.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <returns>The DB Set.</returns>
        DbSet<T> Set<T>()
            where T : class, IDbEntity;
    }
}
