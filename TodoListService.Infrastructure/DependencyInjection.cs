using System.Reflection;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Scrutor;
using TodoListService.Application;
using TodoListService.Domain;
using TodoListService.Infrastructure.BackgroundJobs;
using TodoListService.Infrastructure.EF;
using TodoListService.Infrastructure.EF.Contexts;
using TodoListService.Infrastructure.EF.Interceptors;
using TodoListService.Infrastructure.EF.Options;
using TodoListService.Infrastructure.Extensions;
using TodoListService.Infrastructure.Idempotency;
using TodoListService.Shared.Abstractions;
using TodoListService.Shared.Messaging;

namespace TodoListService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.ConfigureOptions(configuration);

        services.ConfigureMassTransit(configuration);

        services.ConfigureMediatR();

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.ConfigureQuartz();
        
        services.ScanAssemblyForDependenciesWithScrutor();

        return services;
    }

    private static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetOptions<MqOptions>("RabbitMQ");
        
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            
            x.AddConsumers(typeof(ApplicationAssemblyReference).Assembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                
                cfg.Host(settings.Host, "/", "MyRabbit", h =>
                {
                    h.Username(settings.Username);
                    h.Password(settings.Password);
                });

                
            });

            
        });
        
        services.AddTransient<IEventBus, EventBus>();

        return services;
    }

    private static IServiceCollection ConfigureQuartz(this IServiceCollection services)
    {
        services.AddQuartz(cfg =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            cfg.AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever()));

            cfg.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        return services;
    }

    private static IServiceCollection ScanAssemblyForDependenciesWithScrutor(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureAssemblyReference).Assembly,
                typeof(ApplicationAssemblyReference).Assembly,
                typeof(DomainAssemblyReference).Assembly)
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        return services;
    }

    private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg
            => cfg.RegisterServicesFromAssemblies(
                typeof(InfrastructureAssemblyReference).Assembly));
        
        return services;
    }


    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<MqOptions>("RabbitMQ");
        
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetOptions<DbOptions>("Postgres");

        services.AddSingleton<DomainEventsToOutboxMessagesInterceptor>();
        
        services.AddDbContext<ApplicationDbContext>((sp, ctx) =>
        {
            ctx.UseNpgsql(options.ConnectionString);
            
            var interceptor = sp.GetRequiredService<DomainEventsToOutboxMessagesInterceptor>();
            
            ctx.AddInterceptors(interceptor);
        });
        
        
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

}