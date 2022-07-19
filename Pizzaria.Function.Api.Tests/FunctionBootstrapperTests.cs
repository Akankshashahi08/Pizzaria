using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizzaria.Function.Api.Bootstrap;
using System.Collections.Generic;

namespace Pizzaria.Function.Api.Tests
{
    [TestClass]
    public class FunctionBootstrapperTests
    {
        private ServiceCollection serviceCollection;
        private FunctionBootstrapper functionBootstrapper;
        private IConfiguration configuration;

        [TestInitialize]
        public void Initialize()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"SQL_ConnectionString", "Server= .; Database=Pizzaria; Integrated Security=True;"},
            };

            this.configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfiguration)
            .Build();

            this.serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IConfiguration>(provider => configuration);
            this.functionBootstrapper = new FunctionBootstrapper(this.serviceCollection);
        }

        [TestMethod]
        public void Bootstrap_ShouldBootstrapDIRegistration_WhenInvoked()
        {
            this.functionBootstrapper.RegisterDependencies();

            Assert.IsNotNull(this.serviceCollection);
            Assert.IsTrue(this.serviceCollection.Count > 0);
        }
    }
}
