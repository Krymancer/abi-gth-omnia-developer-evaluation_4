using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.IoC;

public interface IModuleInitializer
{
    void Initialize(WebApplicationBuilder builder, IConfiguration configuration);
}
