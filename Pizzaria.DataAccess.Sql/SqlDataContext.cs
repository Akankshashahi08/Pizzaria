using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pizzaria.DataAccess.Sql
{
    public class SqlDataContext : DbContext, ISqlDataContext
    {
        public SqlDataContext()
        {
        }

        public SqlDataContext(
            DbContextOptions<SqlDataContext> options)
           : base(options)
        {
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await this.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = "Your SQL conn string";
                var retryCount = 1;
                var retryInterval = TimeSpan.FromSeconds(10);
                var timeout = 30;

                optionsBuilder.UseSqlServer(connStr, b => b.CommandTimeout(timeout).EnableRetryOnFailure(retryCount, retryInterval, null));
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            var clearables = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                    e.State == EntityState.Modified ||
                    e.State == EntityState.Deleted).ToList();

            clearables.ForEach(x => x.State = EntityState.Detached);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
