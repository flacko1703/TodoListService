using Mapster;
using Microsoft.Extensions.DependencyInjection;
using TodoListService.Domain.Aggregates.TodoListAggregate;
using TodoListService.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.Mappings;


public static class MapsterConfiguration
{
    public static void ConfigureMapster(this IServiceCollection services)
    {
        TypeAdapterConfig<TodoList, TodoListResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, 
                src => src.Id)
            .Map(dest => dest.Title, 
                src => src.Title.Value)
            .Map(dest => dest.Notes, 
                src => src.TaskEntries
                    .Select(x => x.Adapt<TaskEntryResponseDto>()).ToList())
            .TwoWays();
        
        TypeAdapterConfig<TaskEntry, TaskEntryResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, 
                src => src.Id)
            .Map(dest => dest.Title, 
                src => src.Title.Value)
            .Map(dest => dest.Text, 
                src => src.Text.Value)
            .Map(dest => dest.Tags, 
                src => src.Tags.Select(x => x.Adapt<TagResponseDto>()).ToList())
            .Map(dest => dest.Status, 
                src => src.Status.ToString())
            .TwoWays();
        
        TypeAdapterConfig<Tag, TagResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, 
                src => src.Id)
            .Map(dest => dest.Name, 
                src => src.Name.Value)
            .TwoWays();
    }
}