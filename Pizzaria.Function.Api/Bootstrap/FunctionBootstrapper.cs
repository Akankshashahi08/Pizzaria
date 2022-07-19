using Microsoft.Extensions.DependencyInjection;
using Pizzaria.Function.Api.Processor;
using Pizzaria.Function.Api.Processor.Interface;

namespace Pizzaria.Function.Api.Bootstrap
{
    public class FunctionBootstrapper : Bootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FunctionBootstrapper"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public FunctionBootstrapper(IServiceCollection services)
            : base(services)
        {
        }

        /// <summary>
        /// Registers the dependencies.
        /// </summary>
        protected override void RegisterServices()
        {
            this.RegisterScoped<IOrderProcessor, OrderProcessor>();
            this.RegisterScoped<IProductProcessor, ProductProcessor>();
        }
    }
}
