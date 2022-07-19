using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pizzaria.DataAccess.Sql;
using Pizzaria.Repository;

namespace Pizzaria.Function.Api.Bootstrap
{
    public class DataAccessBootstrapper : BootstrapperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessBootstrapper"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public DataAccessBootstrapper(IServiceCollection services)
            : base(services)
        {
            var config = this.Get<IConfiguration>();
            var connec = config.GetValue<string>("SQL_ConnectionString");
            services.AddDbContext<SqlDataContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connec));
        }

        /// <summary>
        /// Registers all DAL dependencies.
        /// </summary>
        public void Register()
        {
            this.RegisterTransient<ISqlDataContext, SqlDataContext>();
            this.RegisterTransient(typeof(ISqlDataAccess<>), typeof(SqlDataAccess<>));
            this.RegisterTransient(typeof(IDataAccess<>), typeof(SqlDataAccess<>));
            this.RegisterTransient<IRepositoryFactory, RepositoryFactory>();
            this.RegisterTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
