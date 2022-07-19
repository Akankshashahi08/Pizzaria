using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Function.Api.Bootstrap
{
    public abstract class Bootstrapper : BootstrapperBase
    {
        /// <summary>
        /// The data access.
        /// </summary>
        private readonly DataAccessBootstrapper dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        protected Bootstrapper(IServiceCollection services)
            : base(services)
        {
            this.dataAccess = new DataAccessBootstrapper(services);
        }

        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        public void RegisterDependencies()
        {
            this.dataAccess.Register();

            this.RegisterServices();
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        protected abstract void RegisterServices();
    }
}
