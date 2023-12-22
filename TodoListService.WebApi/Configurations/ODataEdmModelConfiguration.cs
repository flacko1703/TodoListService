using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using TodoListProj.Application.DTOs.Response;
using TodoListService.Infrastructure.EF.Models;

namespace TodoListService.WebApi.Configurations;

public static class ODataEdmModelConfiguration
{
    
    //EDM Model Configuration
    public static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();

        odataBuilder.EntityType<TodoListResponseDto>();
        
        odataBuilder.EntitySet<TodoListResponseDto>("TodoLists");
        
        return odataBuilder.GetEdmModel();
    }
}