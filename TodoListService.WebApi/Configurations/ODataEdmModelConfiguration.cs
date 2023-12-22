using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using TodoListService.Application.DTOs.Response;

namespace TodoListService.WebApi.Configurations;

public static class ODataEdmModelConfiguration
{
    
    //EDM Model Configuration
    public static IEdmModel GetEdmModel()
    {
        var odataBuilder = new ODataConventionModelBuilder();

        odataBuilder.EntityType<TodoListResponseDto>();
        
        odataBuilder.EntitySet<TodoListResponseDto>("TodoListsResponse");
        
        return odataBuilder.GetEdmModel();
    }
}