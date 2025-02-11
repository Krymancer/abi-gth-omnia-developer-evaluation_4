using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Infrastructure.Messaging;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();

        builder.Services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQ"));
        builder.Services.AddTransient<EventPublisher>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<EventPublisher>>();
                var options = sp.GetRequiredService<IOptions<RabbitMQOptions>>();
                return EventPublisher.CreateAsync(logger, options).GetAwaiter().GetResult();
            });
    }
}