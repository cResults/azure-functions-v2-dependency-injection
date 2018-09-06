using Microsoft.Azure.WebJobs.Description;
using System;

namespace DependencyInjector.AzureFunction
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class InjectAttribute : Attribute
    {
    }
}
