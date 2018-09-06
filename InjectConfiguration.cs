using Microsoft.Azure.WebJobs.Host.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DependencyInjector.AzureFunction
{
    public class InjectConfiguration : IExtensionConfigProvider
    {
        private readonly InjectBindingProvider _InjectBindingProvider;
        private readonly ILoggerFactory _LoggerFactory;
        public InjectConfiguration(InjectBindingProvider injectBindingProvider, ILoggerFactory loggerFactory)
        {
            _InjectBindingProvider = injectBindingProvider;
            _LoggerFactory = loggerFactory;
        }

        public void Initialize(ExtensionConfigContext context)
        {
            var logger = _LoggerFactory.CreateLogger("FunctionApp.DI");
            _InjectBindingProvider.ServiceCollection.AddSingleton(logger);

            context
                .AddBindingRule<InjectAttribute>()
                .Bind(_InjectBindingProvider);
        }
    }
}
