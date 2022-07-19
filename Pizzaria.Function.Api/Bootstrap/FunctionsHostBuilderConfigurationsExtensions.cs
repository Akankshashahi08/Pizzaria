using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;

namespace Pizzaria.Function.Api.Bootstrap
{
    public static class FunctionsHostBuilderConfigurationsExtensions
    {
        /// <summary>
        /// Adds the configuration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="configBuilderFunc">The configuration builder function.</param>
        /// <returns>The IFunctionsHostBuilder.</returns>
        public static IFunctionsHostBuilder AddConfiguration(this IFunctionsHostBuilder builder, Func<IConfigurationBuilder, IConfiguration> configBuilderFunc)
        {
            var configurationBuilder = builder.GetBaseConfigurationBuilder();

            var configuration = configBuilderFunc(configurationBuilder);

            builder.Services.Replace(ServiceDescriptor.Singleton(typeof(IConfiguration), configuration));

            return builder;
        }

        /// <summary>
        /// Gets the current configuration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The IConfiguration.</returns>
        public static IConfiguration GetCurrentConfiguration(this IFunctionsHostBuilder builder)
        {
            var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            return configuration;
        }

        public static void ConfigureOptions<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
            where T : class
        {
            services.Configure<T>(options =>
                configuration.GetSection(sectionName)
                    .Bind(options));
        }

        /// <summary>
        /// Gets the base configuration builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The IConfigurationBuilder.</returns>
        private static IConfigurationBuilder GetBaseConfigurationBuilder(this IFunctionsHostBuilder builder)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var descriptor = builder.Services.FirstOrDefault(d => d.ServiceType == typeof(IConfiguration));
            if (descriptor?.ImplementationInstance is IConfiguration configRoot)
            {
                configurationBuilder.AddConfiguration(configRoot);
            }

            var rootConfigurationBuilder = configurationBuilder.SetBasePath(GetCurrentDirectory());

            return rootConfigurationBuilder;
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <returns>The current directory path.</returns>
        private static string GetCurrentDirectory()
        {
            var currentDirectory = "/home/site/wwwroot";
            var isLocal = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID"));
            if (isLocal)
            {
                currentDirectory = Environment.CurrentDirectory;
            }

            return currentDirectory;
        }
    }
}
