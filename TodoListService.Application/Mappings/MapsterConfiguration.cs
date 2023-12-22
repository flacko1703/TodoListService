using Mapster;
using Microsoft.Extensions.DependencyInjection;
using TodoListProj.Domain.Aggregates.TodoListAggregate;
using TodoListProj.Domain.Aggregates.TodoListAggregate.Entities;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.Application.Mappings;

public static class MapsterConfiguration
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<TodoList, TodoListResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Title, src => src.Title.Value)
            .Map(dest => dest.Notes, src => src.Notes.Adapt<List<NoteResponseDto>>())
            .TwoWays();
        
        TypeAdapterConfig<Note, NoteResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Title, src => src.Title.Value)
            .Map(dest => dest.Text, src => src.Text.Value)
            .Map(dest => dest.Tags, src => src.Tags.Adapt<List<TagResponseDto>>())
            .Map(dest => dest.IsDone, src => src.IsDone.Value)
            .TwoWays();
        
        TypeAdapterConfig<Tag, TagResponseDto>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.TagName, src => src.TagName.Value)
            .TwoWays();
            
    }
}