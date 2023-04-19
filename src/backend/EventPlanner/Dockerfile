FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5094
EXPOSE 7120

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventPlanner.App/EventPlanner.App.csproj", "EventPlanner.App/"]
COPY ["EventPlanner.Data/EventPlanner.Data.csproj", "EventPlanner.Data/"]
RUN dotnet restore "EventPlanner.App/EventPlanner.App.csproj"
COPY . .
WORKDIR "/src/EventPlanner.App"
RUN dotnet build "EventPlanner.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventPlanner.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventPlanner.App.dll"]