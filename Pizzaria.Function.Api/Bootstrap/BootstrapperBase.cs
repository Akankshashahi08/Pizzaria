using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pizzaria.Function.Api.Bootstrap
{
    public class BootstrapperBase
    {
        /// <summary>
        /// The services.
        /// </summary>
        private readonly IServiceCollection services;

        /// <summary>
        /// The provider.
        /// </summary>
        private ServiceProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrapperBase"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        protected BootstrapperBase(IServiceCollection services)
        {
            this.services = services;
            this.provider = services.BuildServiceProvider();
        }

        protected void ConfigureServiceCollection()
        {
            this.provider = services.BuildServiceProvider();
        }
        protected T Get<T>()
        {
            return this.provider.GetRequiredService<T>();
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        protected void RegisterTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            this.services.AddTransient<TInterface, TImplementation>();
        }

        /// <summary>
        /// Registers the transient.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation.</param>
        protected void RegisterTransient(Type service, Type implementation)
        {
            this.services.AddTransient(service, implementation);
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        protected void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            this.services.AddSingleton<TInterface, TImplementation>();
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation.</param>
        protected void RegisterSingleton(Type service, Type implementation)
        {
            this.services.AddSingleton(service, implementation);
        }

        /// <summary>
        /// Registers the scoped.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        protected void RegisterScoped<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            this.services.AddScoped<TInterface, TImplementation>();
        }

        /// <summary>
        /// Registers the scoped.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="implementation">The implementation.</param>
        protected void RegisterScoped(Type service, Type implementation)
        {
            this.services.AddScoped(service, implementation);
        }
    }
}
