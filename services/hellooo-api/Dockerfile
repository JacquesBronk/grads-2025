FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/hellooo-api/Retro.Greeter.csproj", "services/hellooo-api/"]
COPY ["services/common/Retro.Configuration/Retro.Configuration.csproj", "services/common/Retro.Configuration/"]
RUN dotnet restore "services/hellooo-api/Retro.Greeter.csproj"
COPY . .
WORKDIR "/src/services/hellooo-api"
RUN dotnet build "Retro.Greeter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Retro.Greeter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retro.Greeter.dll"]
