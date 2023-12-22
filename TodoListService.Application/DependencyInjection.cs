using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TodoListService.Application.Mappings;
using TodoListService.Domain;

namespace TodoListService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMapster();
        
        services.ConfigureMapster();

        services.ConfigureMediatR();
        
        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        
        services.Scan(scan => scan
            .FromAssemblies(typeof(ApplicationAssemblyReference).Assembly,
                typeof(DomainAssemblyReference).Assembly)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        // services.Scan(scan => scan
        //     .FromExecutingAssembly()
        //     .AddClasses(filter => filter.AssignableTo(typeof(ITodoListRepository)))
        //     .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        //     .AsMatchingInterface()
        //     .WithScopedLifetime());
        return services;
    }
    
    private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationAssemblyReference).Assembly));
        
        return services;
    }
    
}