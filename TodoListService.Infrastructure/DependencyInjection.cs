using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TodoListService.Application;
using TodoListProj.Domain;
using TodoListProj.Domain.Repositories;
using TodoListService.Infrastructure.EF.Repositories;
using TodoListService.Infrastructure.Extensions;

namespace TodoListService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg 
            => cfg.RegisterServicesFromAssembly(typeof(InfrastructureAssemblyReference).Assembly));
        services.AddPersistence(configuration);
        
        services.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureAssemblyReference).Assembly,
                typeof(DomainAssemblyReference).Assembly)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        return services;
    }
}