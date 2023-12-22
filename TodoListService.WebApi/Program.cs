using Microsoft.AspNetCore.OData;
using TodoListService.Application;
using TodoListService.Infrastructure;
using TodoListService.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers()
    .AddOData(opt=>
    {
        opt.AddRouteComponents(
            routePrefix: "odata",
            model: ODataEdmModelConfiguration.GetEdmModel());
        opt.EnableQueryFeatures();
        opt.SetMaxTop(100).Expand().Select().OrderBy().Filter();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options 
        => options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .Build());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI((config) =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Odata Demo Api");
    });
}

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .Build());

app.UseHttpsRedirection();
app.UseODataBatching();
app.UseODataQueryRequest();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();