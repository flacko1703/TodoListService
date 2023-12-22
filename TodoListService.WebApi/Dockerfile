FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TodoListService.WebApi/TodoListService.WebApi.csproj", "TodoListService.WebApi/"]
COPY ["TodoListService.Infrastructure/TodoListService.Infrastructure.csproj", "TodoListService.Infrastructure/"]
COPY ["TodoListService.Application/TodoListService.Application.csproj", "TodoListService.Application/"]
COPY ["TodoListService.Contracts/TodoListService.Contracts.csproj", "TodoListService.Contracts/"]
COPY ["TodoListService.Domain/TodoListService.Domain.csproj", "TodoListService.Domain/"]
RUN dotnet restore "TodoListService.WebApi/TodoListService.WebApi.csproj"
COPY . .
WORKDIR "/src/TodoListService.WebApi"
RUN dotnet build "TodoListService.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoListService.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoListService.WebApi.dll"]
