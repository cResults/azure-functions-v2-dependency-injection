using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjector.AzureFunction
{
    public class InjectBindingProvider : IBindingProvider
    {
        public readonly IServiceCollection ServiceCollection;

        public InjectBindingProvider(IServiceCollection serviceProvider)
        {
            ServiceCollection = serviceProvider;
        }
        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            LoadConfiguredDependencies(context);

            ServiceProvider serviceProvider = ServiceCollection.BuildServiceProvider(true);

            IBinding binding = new InjectBinding(serviceProvider, context.Parameter.ParameterType);
            return Task.FromResult(binding);
        }

        private void LoadConfiguredDependencies(BindingProviderContext context)
        {
            var implementationType = context.Parameter.Member.DeclaringType.Assembly.GetExportedTypes()
                                            .SingleOrDefault(t => typeof(IDependencyConfiguration).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            if (implementationType == null)
                return;

            var dependencyConfig = (IDependencyConfiguration)Activator.CreateInstance(implementationType);
            dependencyConfig.RegisterServices(ServiceCollection);
        }
    }
}
