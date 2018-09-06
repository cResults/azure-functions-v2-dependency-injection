using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DependencyInjector.AzureFunction;
using System;


[assembly: WebJobsStartup(typeof(Startup), "A Web Jobs Startup Extension")]

namespace DependencyInjector.AzureFunction
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var serviceCollection = builder.Services;
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            //Registering a filter
            serviceCollection.AddSingleton<IFunctionFilter, ScopeCleanupFilter>();

            serviceCollection.AddSingleton(new InjectBindingProvider(serviceCollection));

            builder.AddExtension<InjectConfiguration>();
        }
    }
}