using FluentValidation;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using TodoListService.Application.Mappings;
using TodoListProj.Domain.Repositories;

namespace TodoListService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMapster();
        services.RegisterMapsterConfiguration();
        
        services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>());

        services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            cfg.AddConsumers(typeof(ApplicationAssemblyReference).Assembly);
            
            cfg.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host("rabbitmq://localhost");
                configurator.ConfigureEndpoints(context);
            });
        });
        
        services.Scan(scan => scan
            .FromExecutingAssembly()
            .AddClasses(filter => filter.AssignableTo(typeof(ITodoListRepository)))
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        return services;
    }
}