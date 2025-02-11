using Ambev.DeveloperEvaluation.IoC.ModuleInitializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.IoC;

public static class DependencyResolver
{
    public static void RegisterDependencies(this WebApplicationBuilder builder, IConfiguration  configuration)
    {
        new ApplicationModuleInitializer().Initialize(builder, configuration);
        new InfrastructureModuleInitializer().Initialize(builder, configuration);
        new WebApiModuleInitializer().Initialize(builder,configuration);
    }
}