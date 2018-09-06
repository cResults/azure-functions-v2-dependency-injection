using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjector.AzureFunction
{
    /// <summary>
    /// Defines the interface of builder that creates instances of an <see cref="IServiceProvider"/>.
    /// </summary>
    public interface IDependencyConfiguration
    {
        /// <summary>
        /// Creates an instance of an <see cref="IServiceProvider"/>.
        /// </summary>
        /// <returns></returns>
        void RegisterServices(IServiceCollection collection);
    }
}
