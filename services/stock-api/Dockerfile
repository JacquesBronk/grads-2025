FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY services/stock-api/Retro.Stock.csproj services/stock-api/
COPY services/common/Retro.Configuration/Retro.Configuration.csproj services/common/Retro.Configuration/
COPY services/common/Retro.Persistence.Mongo/Retro.Persistence.Mongo.csproj services/common/Retro.Persistence.Mongo/
COPY services/common/Retro/Retro.csproj services/common/Retro/
COPY services/libs/Retro.Stock.Domain/Retro.Stock.Domain.csproj services/libs/Retro.Stock.Domain/
COPY services/libs/Retro.Stock.Infrastructure/Retro.Stock.Infrastructure.csproj services/libs/Retro.Stock.Infrastructure/
COPY services/libs/Retro.Stock.Contracts/Retro.Stock.Contracts.csproj services/libs/Retro.Stock.Contracts/
RUN dotnet restore "services/stock-api/Retro.Stock.csproj"
COPY . .
WORKDIR "/src/services/stock-api"
RUN dotnet build "Retro.Stock.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Retro.Stock.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Retro.Stock.dll"]
