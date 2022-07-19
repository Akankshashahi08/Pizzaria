using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pizzaria.DataAccess.Sql
{
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Saves the context asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Number of rows effected.
        /// </returns>
        Task<int> SaveAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}